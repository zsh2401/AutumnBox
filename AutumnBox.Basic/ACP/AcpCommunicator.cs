/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/29 8:48:54 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Util;
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Basic.ACP
{
    public sealed class AcpCommunicator : IDisposable
    {
        private static readonly List<AcpCommunicator> communicators;
        static AcpCommunicator()
        {
            communicators = new List<AcpCommunicator>();
        }
        public static void DisconnectAll()
        {
            communicators.ForEach((c) =>
            {
                c.Dispose();
            });
        }
        public static AcpCommunicator GetAcpCommunicator(DeviceSerial device)
        {
            var communicator = communicators.Find((c) =>
            {
                return c.device.Equals(device);
            });
            return communicator ?? new AcpCommunicator(device);
        }
        public event EventHandler Connected;
        public event EventHandler Disconnected;
        private readonly Object _locker = new object();
        private readonly DeviceSerial device;
        private Socket socket;
        public bool IsInitiativeDisconnect { get; set; } = false;
        private AcpCommunicator(DeviceSerial device, bool connectAfterCreate = true)
        {
            this.device = device;
            if (connectAfterCreate) { Connect(); }
            communicators.Add(this);
        }
        public void Connect()
        {
            AndroidClientController.AwakeAcpService(device);
            socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(GetEndPoint(device));
            Connected?.Invoke(this, new EventArgs());
            AliveChecking();
            Logger.T($"{device} communicator connected");
        }
        public void Disconnect()
        {
            AndroidClientController.StopAcpService(device);
            Disconnect(true);
        }
        private void Disconnect(bool isInitiative) {
            IsInitiativeDisconnect = isInitiative;
            try
            {
                SendCommand(ACPConstants.CMD_EXIT);
                socket.Disconnect(false);
                socket.Dispose();
            }
            catch
            {
            }
            Logger.T($"{device} communicator disconnected");
            Disconnected?.Invoke(this, new EventArgs());
        }
        public ACPResponseData SendCommand(String baseCommand, params string[] args)
        {
            lock (_locker)
            {
                var builder = new ACPCommand.Builder
                {
                    BaseCommand = baseCommand,
                    Args = args
                };
                return AcpFlow(builder.ToCommand());
            }
        }
        public ACPResponseData SendCommand(ACPCommand command)
        {
            lock (_locker)
            {
                return AcpFlow(command);
            }
        }
        private ACPResponseData AcpFlow(ACPCommand command)
        {
            AliveCheck();
            byte fCode = ACPConstants.FCODE_NO_RESPONSE;
            byte[] dataBuffer = new byte[0];
            try
            {
                AcpStandardProcess.Send(socket, command.ToBytes());
                AcpStandardProcess.Receive(socket, out fCode, out dataBuffer);
            }
            catch (ACPTimeOutException)
            {
                Logger.T("timeout..");
                fCode = ACPConstants.FCODE_TIMEOUT;
            }
            catch (SocketException e)
            {
                Logger.T("acp socket exception", e);
                fCode = ACPConstants.FCODE_NO_RESPONSE;
            }
            catch (IOException e)
            {
                Logger.T("acp exception(client reading)", e);
                fCode = ACPConstants.FCODE_ERR_WITH_EX;
                dataBuffer = Encoding.UTF8.GetBytes(e.ToString() + " " + e.Message);
            }
            return new ACPResponseData()
            {
                FirstCode = fCode,
                Data = dataBuffer,
            };

        }
        public void AliveCheck(bool tryReconnnect = true)
        {
            if (IsAlive()) return;
            if (tryReconnnect)
            {
                Logger.T($"{device} disconnected..reconnecting...");
                Connect();
            };
            if (IsAlive())
            {
                Logger.T($"{device} reconnected...");
                return;
            }
            Logger.T($"{device} connection lost....");
            throw new ACPConnectionLostException();
        }
        private async void AliveChecking()
        {
            while (true)
            {
                try
                {
                    await Task.Run(() =>
                    {
                        Thread.Sleep(5000);
                    });
                    AliveCheck(true);
                }
                catch (Exception ex)
                {
                    Disconnect(false);
                    break;
                }
            }
        }
        public bool IsAlive()
        {
            try
            {
                AcpStandardProcess.Send(socket, Encoding.UTF8.GetBytes(ACPConstants.CMD_TEST));
                AcpStandardProcess.Receive(socket, out byte fCode, out byte[] data, 5000);
                return fCode == ACPConstants.FCODE_SUCCESS;
            }
            catch (SocketException e)
            {
                Logger.T("a exception happend on test ACPService on android is running", e);
                return false;
            }
            catch (IOException e)
            {
                Logger.T("a exception happend on test ACPService on android is running", e);
                return false;
            }
        }
        private static IPEndPoint GetEndPoint(DeviceSerial serial)
        {
            return new IPEndPoint(IPAddress.Parse("127.0.0.1"),
                ForwardManager.GetForwardByRemotePort(serial, ACPConstants.STD_PORT));
        }
        public void Dispose()
        {
            try
            {
                Disconnect();
                socket.Dispose();
                communicators.Remove(this);
            }
            catch { }
            Logger.D("disposed");
        }
        ~AcpCommunicator()
        {
            try { communicators.Remove(this); } catch { }
            Logger.D("~AcpCommunivator() executed......");
        }
    }
}

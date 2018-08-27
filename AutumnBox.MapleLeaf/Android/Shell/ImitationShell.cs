/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/27 20:06:01 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.MapleLeaf.Adb;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace AutumnBox.MapleLeaf.Android.Shell
{
    public class ImitationShell
    {
        private const int TOTAL_CAPACITY = 100;
        private const int HEAD_SIZE = sizeof(byte) + sizeof(int);
        private const int DATA_CAPACITY = TOTAL_CAPACITY - HEAD_SIZE;

        private readonly byte[] buffer = new byte[TOTAL_CAPACITY];
        public int DataLen { get; private set; } = 0;
        public byte Id => buffer[0];
        public int DataLength => BitConverter.ToInt32(buffer, 1);
        public byte[] Data
        {
            get
            {
                return buffer;
            }
        }
        private int leftBytes = 0;
        private Stream stream;
        private Socket socket;
        public ImitationShell(string serialNuber)
        {
            var warpper = new AdbClientWarpper(AdbService.Instance.CreateClient());
            warpper.SetDevice(serialNuber);
            warpper.SendRequest("shell:", false);
            socket = warpper.AdbClient.InnerSocket;
            stream = new NetworkStream(socket);
        }

        public bool Read()
        {

            if (leftBytes == 0)
            {
                Array.Clear(buffer, 0, buffer.Length);
                if (socket.Receive(buffer, 0, HEAD_SIZE, SocketFlags.None) == 0)
                {
                    return false;
                }
                byte id = buffer[0];
                int pktLen = BitConverter.ToInt32(buffer, 1);
                leftBytes = pktLen;
                DataLen = 0;
            }
            int readLen = Math.Min(leftBytes, DATA_CAPACITY);
            if (readLen != 0 && socket.Receive(buffer, HEAD_SIZE, readLen, SocketFlags.None) == 0)
            {
                return false;
            }
            leftBytes -= readLen;
            DataLen = readLen;
            return true;
        }

        public int Write(ShellPacket pkt)
        {
            return socket.Send(pkt.ToBytes());
        }
        public int StdInput(string something)
        {
            return socket.Send(something.ToStdInput());
        }
    }
}

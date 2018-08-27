/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/27 23:47:28 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AutumnBox.MapleLeaf.Android.Shell
{
    public class ShellPacket
    {
        public PacketId Id { get; set; }
        public int Length { get; set; }
        public string Message { get; set; }
        public ShellPacket() { }
        public static ShellPacket Parse(byte[] buffer)
        {
            ShellPacket pkt = new ShellPacket
            {
                Id = (PacketId)buffer[0],
                Length = BitConverter.ToInt32(buffer, 1)
            };
            pkt.Message = Encoding.UTF8.GetString(buffer, 5, pkt.Length);
            return pkt;
        }
        public byte[] ToBytes()
        {
            byte[] buffer = new byte[5 + Length];
            byte[] len = BitConverter.GetBytes(Length);
            Array.Reverse(len);
            buffer[0] = (byte)Id;
            Array.Copy(len, 0, buffer, 1, 4);
            Array.Copy(Encoding.UTF8.GetBytes(Message), 0, buffer, 5, Length);
            return buffer;
        }
    }
}

/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/22 1:50:56 (UTC +8:00)
** desc： ...
*************************************************/

using System.Text;

namespace AutumnBox.Basic.ACP
{
    public struct ACPResponseData
    {
        public bool IsSuccessful
        {
            get
            {
                return FirstCode == ACPConstants.FCODE_SUCCESS;
            }
        }
        public byte FirstCode { get; set; }
        public byte[] Data { get; set; }
        public override string ToString()
        {
            return Encoding.UTF8.GetString(Data);
        }
    }
}

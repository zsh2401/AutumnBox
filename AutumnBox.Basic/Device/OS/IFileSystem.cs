/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/29 4:29:37 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device.OS
{
    public interface IFileSystem
    {
        void Move(string src,string target);
        void Copy(string src, string target);
        void Delete(string file);
        void Mkdir(string file);
    }
}

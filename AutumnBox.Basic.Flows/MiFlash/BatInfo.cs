/********************************************************************************
** auth： zsh2401@163.com
** date： 2018/1/11 22:25:39
** filename: BatInfo.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Flows.MiFlash
{
    public class BatInfo
    {
        public string FullPath { get; private set; }
        public string Desc { get; private set; }
        public BatInfo(string fullPath, string desc)
        {
            FullPath = fullPath;
            Desc = desc;
        }
    }
}

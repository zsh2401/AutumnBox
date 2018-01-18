/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/17 21:00:19 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Executer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.FlowFramework
{
    /// <summary>
    /// 去除了所有泛型特征的FunctionFlow接口
    /// </summary>
    public interface INoGenericFlow : ICompletable,IOutSender
    {
        bool IsClosed { get; }
        bool MustTiggerAnyFinishedEvent { set; }
        void RunAsync();
    }
}

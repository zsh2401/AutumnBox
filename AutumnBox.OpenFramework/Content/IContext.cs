/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/24 17:18:13 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Content
{
    public interface IContext
    {
        string LoggingTag { get; }
        IAppManager App { get; }
        ILogger Logger { get; }
        CtxPer CtxPer { get; }
        IUx Ux { get; }
        IEmbeddedFileManager EmbeddedManager { get; }
    }
}

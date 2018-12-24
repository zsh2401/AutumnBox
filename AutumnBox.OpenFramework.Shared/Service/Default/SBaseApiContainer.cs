/*************************************************
** auth： zsh2401@163.com
** date:  2018/10/31 22:41:22 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Management;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Service.Default
{
    [ServiceName(NAME)]
    internal class SBaseApiContainer : AtmbService
    {
        public const string NAME = "baseApiManager";
        IBaseApi _baseApi;
        public void LoadApi(IBaseApi baseApi)
        {
            _baseApi = baseApi ?? throw new ArgumentNullException(nameof(baseApi));
        }
        public IBaseApi GetApi(Context ctx)
        {
            return _baseApi;
        }
    }
}

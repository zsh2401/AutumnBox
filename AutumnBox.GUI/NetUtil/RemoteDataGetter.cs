/* =============================================================================*\
*
* Filename: RemoteDataGetter
* Description: 
*
* Version: 1.0
* Created: 2017/10/16 14:08:40(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Support.Log;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
namespace AutumnBox.GUI.NetUtil
{
    internal abstract class RemoteDataGetter<TResult>
        where TResult : class
    {
        protected readonly WebClient webClient;
        public RemoteDataGetter()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            webClient = new WebClient
            {
                Encoding = Encoding.UTF8
            };
        }
        public async void RunAsync(Action<TResult> finishedHandler)
        {
            TResult result = await Task.Run(() =>
            {
                return Run();
            });
            if (result != null)
            {
                finishedHandler(result);
            }
        }
        protected string DownloadString(string url)
        {
            return webClient.DownloadString(url);
        }
        public TResult Run()
        {
            try
            {
                return Get();
            }
            catch (Exception e)
            {
                Logger.Warn(this, "获取失败", e);
                return null;
            }
        }
        public abstract TResult Get();
    }
}

using AutumnBox.Reporter.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Reporter.Util
{
    class Uploader
    {
        private const string API = "what the fuck";
        private readonly ReportHeader header;

        public Uploader(ReportHeader header)
        {
            this.header = header;
        }
        public void Upload(Log log)
        {
            WebRequest request = WebRequest.CreateHttp(API);
            request.Method = "POST";
            request.Headers["Id"] = header.UUID;
            request.Headers["UserName"] = header.UserName ?? "";
            request.Headers["UserMail"] = header.UserMail.ToString() ?? "";
            request.Headers["Remark"] = header.Remark ?? "";
            request.Headers["LogName"] = log.LogName;
            request.ContentType = "text";
            byte[] data = Encoding.UTF8.GetBytes(log.Content);
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            using (var response = request.GetResponse())
            {

            }
        }
        public void Upload(IEnumerable<Log> logs, Action<Exception> uploadedOneCallback)
        {
            foreach (Log log in logs)
            {
                try
                {
                    Upload(log);
                    uploadedOneCallback(null);
                }
                catch (Exception e)
                {
                    uploadedOneCallback(e);
                }
            }
        }
    }
}

using AutumnBox.Reporter.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AutumnBox.Reporter.Util
{
    class Uploader
    {
        private const string LOG_POST_API = "https://www.baidu.com";
        private const string FIRST_POST_API = "https://www.baidu.com";
        private readonly ReportHeader header;

        public Uploader(ReportHeader header)
        {
            this.header = header;
        }
        public void Upload(IEnumerable<Log> logs, Action<Exception> uploadedOneCallback)
        {
            PostHeader(header);
            foreach (Log log in logs)
            {
                try
                {
                    PostLog(log);
                    uploadedOneCallback(null);
                }
                catch (Exception e)
                {
                    uploadedOneCallback(e);
                }
            }
        }

        private void PostHeader(ReportHeader header)
        {
            WebRequest request = WebRequest.CreateHttp(FIRST_POST_API);
            request.Method = "POST";
            request.Headers["Id"] = header.UUID;
            request.ContentType = "text";
            string xml = Serializer(typeof(ReportHeader), header);
            Trace.WriteLine(xml);
            byte[] data = Encoding.UTF8.GetBytes(xml);
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            using (var response = request.GetResponse())
            {
                Trace.WriteLine(response);
            }
        }
        private void PostLog(Log log)
        {
            WebRequest request = WebRequest.CreateHttp(LOG_POST_API);
            request.Method = "POST";
            request.Headers["Id"] = header.UUID;
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
                Trace.WriteLine(response);
            }
        }

        public static string Serializer(Type type, object obj)
        {
            MemoryStream Stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(type);
            //序列化对象
            xml.Serialize(Stream, obj);
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();

            sr.Dispose();
            Stream.Dispose();

            return str;
        }
    }
}

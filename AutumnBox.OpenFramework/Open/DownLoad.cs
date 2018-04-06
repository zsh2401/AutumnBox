using System.Net;


namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 下载文件
    /// </summary>
    public class DownLoad
    {
        /// <summary>
        /// 下载文件，下载大文件不建议使用，同时推荐使用异步
        /// </summary>
        /// <param name="url">下载文件地址</param>
        /// <param name="filename">下载后的存放地址</param>
        public void DownloadFile(string url, string filename)
        {
            try
            {
                var myrq = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse myrp = (HttpWebResponse)myrq.GetResponse();

                System.IO.Stream st = myrp.GetResponseStream();
                System.IO.Stream so = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                var totalDownloadedByte = 0;
                var by = new byte[1024];
                if (st != null)
                {
                    var osize = st.Read(by, 0, by.Length);
                    while (osize > 0)
                    {
                        totalDownloadedByte = osize + totalDownloadedByte;
                        so.Write(by, 0, osize);
                        osize = st.Read(by, 0, by.Length);
                    }
                }

                so.Close();
                st?.Close();
            }
            catch
            {
                // ignored
            }
        }
    }
}

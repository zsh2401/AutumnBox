
using System.Collections;
/*此文件中的代码为core类的private方法,不可供外界调用*/
namespace AutumnBox.Basic
{

    public partial class Core
    {
        public const string GET_FAIL = "...";
//        /// <summary>
//        /// 获取设备的build信息
//        /// </summary>
//        /// <param name="id">设备id</param>
//        /// <returns>一个存储着设备build信息的hashtable</returns>
//        private Hashtable GetBuildInfo(string id)
//        {
//            Hashtable ht = new Hashtable();
//            try { ht.Add("name", ae($" -s {id} shell \"cat /system/build.prop | grep \"product.name\"\"").output[0].Split('=')[1]); }
//            catch { ht.Add("name", GET_FAIL); }

//            try { ht.Add("brand", ae($" -s {id} shell \"cat /system/build.prop | grep \"product.brand\"\"").output[0].Split('=')[1]); }
//            catch { ht.Add("brand", GET_FAIL); }

//            try { ht.Add("androidVersion", ae($" -s {id} shell \"cat /system/build.prop | grep \"build.version.release\"\"").output[0].Split('=')[1]); }
//            catch { ht.Add("androidVersion", GET_FAIL); }

//            try { ht.Add("model", ae($" -s {id} shell \"cat /system/build.prop | grep \"product.model\"\"").output[0].Split('=')[1]); }
//            catch { ht.Add("model", GET_FAIL); }

//#if DEBUG
//            try { ht.Add("model", ae($" -s {id} shell \"cat /system/build.prop").output); }
//            catch { ht.Add("all", GET_FAIL); }
//#endif
//            return ht;
//        }

    }
}

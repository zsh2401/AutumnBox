using System.Collections;

namespace AutumnBox.Basic
{

    public partial class Core
    {
        public const string GET_FAIL = "...";
        private Hashtable GetBuildInfo(string id)
        {
            Hashtable ht = new Hashtable();
            try{ ht.Add("name", ae($" -s {id} shell \"cat /system/build.prop | grep \"product.name\"\"").output[0].Split('=')[1]); }
            catch{ht.Add("name", GET_FAIL); }

            try { ht.Add("brand", ae($" -s {id} shell \"cat /system/build.prop | grep \"product.brand\"\"").output[0].Split('=')[1]); }
            catch { ht.Add("brand", GET_FAIL); }

            try { ht.Add("androidVersion", ae($" -s {id} shell \"cat /system/build.prop | grep \"build.version.release\"\"").output[0].Split('=')[1]); }
            catch { ht.Add("androidVersion", GET_FAIL); }

            try { ht.Add("model", ae($" -s {id} shell \"cat /system/build.prop | grep \"product.model\"\"").output[0].Split('=')[1]); }
            catch { ht.Add("model", GET_FAIL); }

            return ht;
        }

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Devices
{
    public class DevicesHashtable : Hashtable
    {
        public DevicesHashtable() : base()
        {

        }
        /// <summary>
        /// 重载相等判断的运算符
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(DevicesHashtable left, DevicesHashtable right)
        {
            if (left.Count != right.Count) return false;//如果不等于,直接排除
            foreach (DictionaryEntry i in left)
            {
                if (!right.Contains(i.Key.ToString())) return false;//如果b不包含a的任何一个,排除
                if (right[i.Value.ToString()] != left[i.Value.ToString()]) return false;//如果有一个值不同,也排除
            }

            return true;
        }
        /// <summary>
        /// 重载不等判断的运算符
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(DevicesHashtable left, DevicesHashtable right)
        {
            if (left == right)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static DevicesHashtable operator +(DevicesHashtable left, DevicesHashtable right)
        {
            foreach (DictionaryEntry entry in right)
            {
                left.Add(entry.Key.ToString(), entry.Value.ToString());
            }
            return left;
        }
        /// <summary>
        /// 重载Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return this == (DevicesHashtable)obj;
        }
        public override object this[object index]
        {
            get
            {

                try
                {
                    //Debug.WriteLine(index);
                    return base[index];
                }
                catch (System.NullReferenceException)
                {
                    return String.Empty;
                }
            }
            set
            {
                base[index] = value;
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

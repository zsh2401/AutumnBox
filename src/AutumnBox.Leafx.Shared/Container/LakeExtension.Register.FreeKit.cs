using AutumnBox.Leafx.ObjectManagement;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AutumnBox.Leafx.Container
{
    public static partial class LakeExtension
    {
        public static bool FreeKitStatus { get; private set; } = true;
        public static bool CanModify { get; private set; } = true;
        public static void EnableFreeKit(bool canNotModify = true)
        {
            if (CanModify)
            {
                FreeKitStatus = true;
                CanModify = canNotModify;
            }
        }
        public static void DisableFreeKit()
        {
            if (CanModify)
            {
                FreeKitStatus = false;
            }
        }


        private static readonly Dictionary<string, HashSet<object>> cache = new Dictionary<string, HashSet<object>>();
        private static readonly Type disposableType = typeof(IDisposable);
        private const string FREE_METHOD_NAME = "RFree";

        public static int FreeKitRecordSize => cache.Count;

        public static void FreeKitFree(this object instance, bool skipCheck = false)
        {
            if (!FreeKitStatus) return;
            try
            {
                if (skipCheck || FreeKitIsSupport(instance.GetType()))
                {
                    var disposeObj = (instance as IDisposable);
                    if (disposeObj != null)
                    {
                        disposeObj.Dispose();
                    }
                    else
                    {
                        MethodInfo methodInfo = instance.GetType().GetMethod(FREE_METHOD_NAME, ObjectManagementConstants.BINDING_FLAGS);
                        methodInfo.Invoke(instance, new object[0]);
                    }
                    FreeKitRemove(instance);
                }
            }
            catch
            {
            }
        }

        public static void FreeKitRemove(object instance)
        {
            if (!FreeKitStatus) return;
            foreach (var kv in cache)
            {
                var hashSet = kv.Value;
                if (hashSet.Contains(instance))
                {
                    hashSet.Remove(instance);
                    break;
                }
            }
        }

        public static bool FreeKitIsSupport(Type t)
        {
            if (!FreeKitStatus) return false;
            bool isDisposable = disposableType.IsAssignableFrom(t);
            bool haveFreeMethod = t.GetMethod(FREE_METHOD_NAME, ObjectManagementConstants.BINDING_FLAGS) != null;
            return isDisposable || haveFreeMethod;
        }

        public static void FreeKitFreeAll()
        {
            if (!FreeKitStatus) return;
            foreach (var set in cache)
            {
                foreach (var instance in set.Value)
                {
                    instance?.FreeKitFree();
                }
            }
        }

        public static void FreeKitRecord(string id, object instance)
        {
            if (!FreeKitStatus) return;
            if (FreeKitIsSupport(instance.GetType()))
            {
                var set = cache[id];
                if (set == null)
                {
                    cache[id] = new HashSet<object>();
                }
                set.Add(instance);
            }
        }
    }
}
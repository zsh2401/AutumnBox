using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 持久化存储API
    /// </summary>
    public interface IStorageManager
    {
        /// <summary>
        /// 将根据其计算并划分存储文件区域
        /// </summary>
        /// <param name="id">唯一识别</param>
        void Init(string id);
        /// <summary>
        /// 根据ID打开一个文件
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="createIfNotExist"></param>
        /// <returns></returns>
        FileStream OpenFile(string fileId, bool createIfNotExist = true);
        /// <summary>
        /// 删除一个文件
        /// </summary>
        /// <param name="fileId"></param>
        void DeleteFile(string fileId);
        /// <summary>
        /// 保存一个JsonObject
        /// </summary>
        /// <param name="jsonId"></param>
        /// <param name="jsonObject"></param>
        void SaveJsonObject(string jsonId, object jsonObject);
        /// <summary>
        /// 读取一个JsonObject
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="jsonId"></param>
        /// <returns></returns>
        TResult ReadJsonObject<TResult>(string jsonId);
        /// <summary>
        /// 缓存文件夹信息
        /// </summary>
        DirectoryInfo CacheDirectory { get; }
        /// <summary>
        /// 删除保存的Json
        /// </summary>
        void ClearJsonObjects();
        /// <summary>
        /// 清空缓存文件夹
        /// </summary>
        void ClearCache();
        /// <summary>
        /// 清空单文件存储系统
        /// </summary>
        void ClearFiles();
        /// <summary>
        /// 清空全部
        /// </summary>
        void Restore();
    }
}

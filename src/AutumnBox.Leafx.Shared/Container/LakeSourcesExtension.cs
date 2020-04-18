#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutumnBox.Leafx.Container
{
    /// <summary>
    /// LakeSources的帮助类
    /// </summary>
    public static class LakeSourcesExtension
    {
        /// <summary>
        /// 根据ID获取
        /// </summary>
        /// <param name="sources"></param>
        /// <param name="id"></param>
        /// <exception cref="IdNotFoundException">找不到ID</exception>
        /// <exception cref="ArgumentNullException">输入参数为空</exception>
        /// <returns></returns>
        public static object? Get(this IEnumerable<ILake> sources, string id)
        {
            if (sources is null)
            {
                throw new ArgumentNullException(nameof(sources));
            }

            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("message", nameof(id));
            }

            foreach (var source in sources.Reverse())
            {
                try
                {
                    return source.GetComponent(id);
                }
                catch (IdNotFoundException e)
                {
                    throw e;
                }
            }
            throw new IdNotFoundException($"Id {id} not found");
        }

        /// <summary>
        /// 根据type获取
        /// </summary>
        /// <param name="sources"></param>
        /// <param name="t"></param>
        /// <exception cref="TypeNotFoundException">没有对应类型的值</exception>
        /// <exception cref="ArgumentNullException">传入的值为空</exception>
        /// <returns></returns>
        public static object? Get(this IEnumerable<ILake> sources, Type t)
        {
            if (sources is null)
            {
                throw new ArgumentNullException(nameof(sources));
            }

            if (t is null)
            {
                throw new ArgumentNullException(nameof(t));
            }

            foreach (var source in sources.Reverse())
            {
                try
                {
                    return source.Get(t);
                }
                catch (TypeNotFoundException e)
                {
                    throw e;
                }
            }
            throw new TypeNotFoundException(t);
        }
    }
}

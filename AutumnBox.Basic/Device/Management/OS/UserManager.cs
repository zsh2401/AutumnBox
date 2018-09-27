/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/21 23:44:34 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Util;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AutumnBox.Basic.Device.Management.OS
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class User
    {
        /// <summary>
        /// UID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }
    }
    /// <summary>
    /// 用户管理器
    /// </summary>
    [Obsolete("等待重置")]
    public class UserManager
    {
        private static readonly Regex userInfoRegex;
        private readonly IDevice device;
        static UserManager()
        {
            userInfoRegex = new Regex(@"UserInfo{(?<id>\d+):(?<name>.+):", RegexOptions.Multiline);
        }
        /// <summary>
        /// 构造一个用户管理器
        /// </summary>
        /// <param name="device"></param>
        public UserManager(IDevice device)
        {
            this.device = device;
        }
        /// <summary>
        /// 获取设备上的所有用户
        /// </summary>
        /// <param name="ignoreZeroUser">是否忽略0号用户</param>
        /// <returns>用户</returns>
        public User[] GetUsers(bool ignoreZeroUser =true)
        {
            var output = device.Shell("pm list users");
            var matches = userInfoRegex.Matches(output.ToString());
            List<User> users = new List<User>();
            foreach (Match match in matches)
            {
                var user = new User()
                {
                    Id = int.Parse(match.Result("${id}")),
                    Name = match.Result("${name}")
                };
                if (ignoreZeroUser && user.Id == 0) continue;
                else users.Add(user);
            }
            return users.ToArray();
        }
        /// <summary>
        /// 移除某个用户
        /// </summary>
        /// <param name="uid">UID</param>
        /// <returns></returns>
        public void RemoveUser(int uid)
        {
            device.Shell($"pm remove-user {uid}").ThrowIfExitCodeNotEqualsZero();
        }
    }
}

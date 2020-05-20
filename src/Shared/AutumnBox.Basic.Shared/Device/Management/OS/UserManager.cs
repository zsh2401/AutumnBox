/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/21 23:44:34 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Util;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AutumnBox.Basic.Device.Management.OS
{

    /// <summary>
    /// 用户管理器
    /// </summary>
    public class UserManager : DeviceCommander
    {
        private const string PATTERN_USER_INFO = @"UserInfo{(?<id>\d+):(?<name>.+):";
        private static readonly Regex userInfoRegex;
        static UserManager()
        {
            userInfoRegex = new Regex(PATTERN_USER_INFO, RegexOptions.Multiline);
        }
        /// <summary>
        /// 从输出中获取用户数据
        /// </summary>
        /// <param name="output"></param>
        /// <param name="ignoreZeroUser"></param>
        /// <returns></returns>
        protected virtual UserInfo[] ParseOutput(Output output, bool ignoreZeroUser)
        {
            var matches = userInfoRegex.Matches(output.ToString());
            List<UserInfo> users = new List<UserInfo>();
            foreach (Match match in matches)
            {
                var user = new UserInfo()
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
        /// 构造一个用户管理器
        /// </summary>
        /// <param name="device"></param>
        public UserManager(IDevice device) : base(device)
        {
        }
        /// <summary>
        /// 获取设备上的所有用户
        /// </summary>
        /// <param name="ignoreZeroUser">是否忽略0号用户</param>
        /// <returns>用户</returns>
        public UserInfo[] GetUsers(bool ignoreZeroUser = true)
        {
            var executeResult = Executor.AdbShell(Device, "pm list users")
                .ThrowIfShellExitCodeNotEqualsZero();
            return ParseOutput(executeResult.Output, ignoreZeroUser);
        }
        /// <summary>
        /// 移除某个用户
        /// </summary>
        /// <param name="uid">UID</param>
        /// <returns></returns>
        public void RemoveUser(int uid)
        {
            Executor.AdbShell(Device, $"pm remove-user {uid}")
               .ThrowIfShellExitCodeNotEqualsZero();
        }
    }
}

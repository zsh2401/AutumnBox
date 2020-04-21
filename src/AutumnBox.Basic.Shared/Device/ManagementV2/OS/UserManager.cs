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

namespace AutumnBox.Basic.Device.ManagementV2.OS
{

    /// <summary>
    /// 用户管理器
    /// </summary>
    public sealed class UserManager
    {
        private const string PATTERN_USER_INFO = @"UserInfo{(?<id>\d+):(?<name>.+):";
        private static readonly Regex userInfoRegex
            = new Regex(PATTERN_USER_INFO, RegexOptions.Multiline | RegexOptions.Compiled);
        private readonly IDevice device;
        private readonly ICommandExecutor executor;

        /// <summary>
        /// 从输出中获取用户数据
        /// </summary>
        /// <param name="output"></param>
        /// <param name="ignoreZeroUser"></param>
        /// <returns></returns>
        private UserInfo[] ParseOutput(Output output, bool ignoreZeroUser)
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
        public UserManager(IDevice device, ICommandExecutor executor)
        {
            this.device = device ?? throw new ArgumentNullException(nameof(device));
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }
        /// <summary>
        /// 获取设备上的所有用户
        /// </summary>
        /// <param name="ignoreZeroUser">是否忽略0号用户</param>
        /// <returns>用户</returns>
        public UserInfo[] GetUsers(bool ignoreZeroUser = true)
        {
            var executeResult = executor.AdbShell(device, "pm list users");
            if (executeResult.ExitCode != 0)
                return null;
            else
                return ParseOutput(executeResult.Output, ignoreZeroUser);
        }
        /// <summary>
        /// 移除某个用户
        /// </summary>
        /// <param name="uid">UID</param>
        /// <returns></returns>
        public CommandResult RemoveUser(int uid)
        {
            return executor.AdbShell(device, $"pm remove-user {uid}");
        }
    }
}

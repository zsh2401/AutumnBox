/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/21 23:44:34 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Executer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device.PackageManage
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class UserManager
    {
        private static readonly Regex userInfoRegex;
        private readonly AndroidShellV2 shell;
        static UserManager()
        {
            userInfoRegex = new Regex(@"UserInfo{(?<id>\d+):(?<name>.+):", RegexOptions.Multiline);
        }
        public UserManager(DeviceSerial device)
        {
            this.shell = new AndroidShellV2(device);
        }

        public User[] GetUsers(bool ignoreZeroUser =true)
        {
            var output = shell.Execute("pm list users");
            output.PrintOnConsole();
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
        public AdvanceOutput RemoveUser(int uid)
        {
            return shell.Execute($"pm remove-user {uid}");
        }
    }
}

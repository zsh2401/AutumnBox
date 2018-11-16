/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/19 20:54:58 (UTC +8:00)
** desc： ...
*************************************************/

using System;
using AutumnBox.OpenFramework.Extension;
namespace AutumnBox.CoreModules.Attribute
{
    class DpmReceiverAttribute : SingleInfoAttribute
    {
        public const string KEY = "DpmReceiver";
        public override string Key => KEY;
        public DpmReceiverAttribute(string componentName) : base(componentName)
        {
            if (string.IsNullOrWhiteSpace(componentName))
            {
                throw new ArgumentException("message", nameof(componentName));
            }
        }
    }
}

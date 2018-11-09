using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

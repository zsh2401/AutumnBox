using AutumnBox.Basic.Calling;

namespace AutumnBox.CoreModules.Lib
{
    internal static class DpmFailedMessageParser
    {
        public static bool TryParse(CommandExecutor.Result result, out string tip, out string message)
        {
            var outputString = result.Output.ToString().ToLower();
            if (result.ExitCode == 0 && outputString.Contains("success"))
            {
                tip = EDpmSetterBase.TIP_OK;
                message = EDpmSetterBase.OK_MSG;
            }
            else if (outputString.Contains("already several accounts on the device"))
            {
                tip = EDpmSetterBase.TIP_FAIL;
                message = EDpmSetterBase.ERR_MSG_KEY_HAVE_ACCOUNTS;
            }
            else if (outputString.Contains("already several users on the device"))
            {
                tip = EDpmSetterBase.TIP_FAIL;
                message = EDpmSetterBase.ERR_MSG_KEY_HAVE_USERS;
            }
            else if (outputString.Contains("nor current process has android.permission.MANAGE_DEVICE_ADMINS"))
            {
                tip = EDpmSetterBase.TIP_FAIL;
                message = EDpmSetterBase.ERR_MSG_KEY_MIUI_SEC;
            }
            else if (outputString.Contains("but device owner is already set"))
            {
                tip = EDpmSetterBase.TIP_FAIL;
                message = EDpmSetterBase.ERR_MSG_KEY_DO_ALREADY_SET;
            }
            else if (result.ExitCode == 127)
            {
                tip = EDpmSetterBase.TIP_FAIL;
                message = EDpmSetterBase.ERR_MSG_KEY_DPM_NOT_FOUND;
            }
            else
            {
                tip = null;
                message = null;
                return false;
            }
            return true;
        }
    }
}

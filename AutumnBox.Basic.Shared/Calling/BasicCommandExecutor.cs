//using AutumnBox.Basic.Data;
//using System.Diagnostics;
//using System.Threading.Tasks;

//namespace AutumnBox.Basic.Calling
//{
//    class BasicCommandExecutor : INotifyOutput, IReceiveOutputByTo<BasicCommandExecutor>
//    {
//        public Task<ICommandResult> ExecuteAsync(string fileName, params string[] args)
//        {

//        }
//        public ICommandResult Execute(string fileName, params string[] args)
//        {
//            var pInfo = new ProcessStartInfo()
//            {
//                RedirectStandardError = true,
//                RedirectStandardOutput = true,
//                RedirectStandardInput = false,
//                UseShellExecute = false,
//                CreateNoWindow = CreateNoWindow,
//                FileName = fileName,
//                Arguments = string.Join(" ", args),
//            };
//        }
//    }
//}

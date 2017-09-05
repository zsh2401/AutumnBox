//using AutumnBox.Basic.AdbEnc;
//using AutumnBox.Basic.Arg;
//using AutumnBox.Basic.Functions;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AutumnBox.Basic.Util
//{
//    public class Tester
//    {
//        public static void Reboot(string id, RebootOptions option) {
//            RebootOperator ro = new RebootOperator();
//            ro.RebootFinish += new EventsHandlers.FinishEventHandler((o) => { Console.WriteLine("Reboot Finish"); });
//            ro.Run(new RebootArgs { deviceID = id,rebootOption = option,nowStatus = DevicesTools.GetDeviceStatus(id)});
//        }
//        public static void SendFileToSdcard(string id, string[] files) {
//            FileSender fs = new FileSender();
//            fs.sendAllFinish += new EventsHandlers.SimpleFinishEventHandler(() => { Console.WriteLine("Send finish"); });
//            fs.Run(new FileArgs { deviceID =id,files = files});
//        }
//        public static void FlashCustomRecovery(string id, string recoveryFilePath) {
//            CustomRecoveryFlasher flasher = new CustomRecoveryFlasher();
//            flasher.FlashFinish += new EventsHandlers.SimpleFinishEventHandler(() => { Console.WriteLine("Ok finish"); });
//            flasher.Run(new FileArgs { deviceID =id,files = new string[] { recoveryFilePath} });
//            new Fastboot().Execute(id,$" boot {recoveryFilePath}");
//            //Reboot(id,RebootOptions.Recovery);
//        }
//    }
//}

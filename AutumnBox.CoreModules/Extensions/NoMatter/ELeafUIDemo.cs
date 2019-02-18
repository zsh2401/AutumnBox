using AutumnBox.Basic.Calling;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Attributes;
using AutumnBox.OpenFramework.LeafExtension.Fast;
using AutumnBox.OpenFramework.LeafExtension.Kit;
using AutumnBox.OpenFramework.Open;
using System.Diagnostics;
using System.Threading;

namespace AutumnBox.CoreModules.Extensions.NoMatter
{
    [ExtName("Leaf UI Demo", "zh-cn:LeafUI官方演示")]
    [ExtRequiredDeviceStates(LeafConstants.NoMatter)]
    [ExtText("testkey","默认值,defaultvalue","zh-cn:中文值")]//本模块专用文本!
    [ExtDeveloperMode]
    public class ELeafUIDemo : LeafExtensionBase
    {
        /// <summary>
        /// 入口点函数
        /// 一个入口点函数要么名为Main,要么有LMain标记
        /// 这是LeafExtensionBase确定入口函数的方法
        /// 你要的API统统声明出来,LeafExtension系统将进行是注入,如果是无法注入的,将置null
        /// </summary>
        /// <param name="test">
        /// 该参数被标记为LFromData
        /// 也就是说,该参数将会从模块启动键值数据中获取
        /// </param>
        /// <param name="ui">需要获取的UI,将会被LeafExtensionBase进行注入</param>
        [LMain]
        public void Main(ILeafUI ui, Context context, TextAttrManager textManager)
        {
            /*
             * 一定要进行using,此处是为了确保LeafUI被正确释放
             * 否则当该函数内发生异常,LeafUI将无法被关闭
             */
            using (ui)
            {
                /*初始化UI,此时UI仍然不可见*/
                //将LeafUI的图标设置为本模块的图标
                ui.Icon = this.GetIconBytes();
                //将LeafUI的标题设置为本模块的名称
                ui.Title = this.GetName();
                //绑定LeafUI关闭事件,并非必要的操作
                ui.CloseButtonClicked += (s, e) =>
                {
                    /*
                     * 当用户点击停止按钮时发生
                     * 请在此处销毁你调用的资源
                     * 如果你不想你的模块被中途停止,请返回false
                    */
                    e.CanBeClosed = false;
                };

                /*完成初始化,调用展示方法后,UI将可见*/
                //显示UI
                ui.Show();

                if (ui.DoYN("是否需要参看本拓展源代码?", "是", "否"))
                {
                    Process.Start("https://github.com/zsh2401/AutumnBox/blob/master/AutumnBox.CoreModules/Extensions/NoMatter/ELeafUIDemo.cs");
                }

                if (ui.DoYN("是否需要开启右上角问号?", "是", "否"))
                {
                    ui.EnableHelpBtn(() =>
                    {
                        Process.Start("https://github.com/zsh2401/AutumnBox/blob/master/AutumnBox.CoreModules/Extensions/NoMatter/ELeafUIDemo.cs");
                    });
                }

                //在输出框打印东西
                ui.WriteLine("这是普通输出,用于对用户的提示等");

                /*设置进度与说明
                 当然,你也可以不进行设置
                 LeafUI默认进度条是无限循环的(进度值为-1)
                 默认提示是正在进行中等类似话语
                 */
                Thread.Sleep(1000);
                ui.ShowMessage("进度条以及提示信息的演示");

                ui.Tip = "第一步";
                ui.Progress = 10;
                Thread.Sleep(500);
                //设置进度与说明
                ui.Tip = "第二步";
                ui.Progress = 40;
                Thread.Sleep(500);
                //设置进度与说明
                ui.Tip = "第三步";
                ui.Progress = 60;
                Thread.Sleep(500);

                ui.ShowMessage("进度条将变回无限循环");
                ui.Progress = -1;

                //进行带有取消选项的选择
                var choiceResult = ui.DoChoice("这是一个选择示例,请做出选择", "ping测试", "启动别的拓展模块", "什么也不做");
                switch (choiceResult)
                {
                    case true:
                        var executor = new CommandExecutor();
                        executor.To(e => ui.WriteOutput(e.Text));//重定向输出
                        executor.Cmd("ping www.baidu.com");
                        //通过此函数,可以显示专业输出,但不建议调用,此权利应该尽可能交给用户
                        ui.WriteLine("显示专业输出!");
                        ui.ProOutputVisible = true;
                        executor.Dispose();
                        break;
                    case false:
                        var thread = context.NewExtensionThread("EHoldMyHand");
                        thread.Data["wtf"] = "aaa";//可以传参数哦
                        thread.Start();
                        thread.WaitForExit();
                        break;
                    default:
                        ui.ShowMessage("哼,气得我变大了!");
                        ui.WriteLine($"原大小:{ui.Width}x{ui.Height}");
                        ui.Width += 200;
                        ui.Height += 200;
                        ui.WriteLine($"后大小:{ui.Width}x{ui.Height}");
                        break;
                }

                ui.ShowMessage($"让我们来看看根据语言自动匹配的语言值: {textManager["testkey"]}");

                //进行单选,让用户决定用那个方式结束
                const string hint = "好了,该选择一个方式结束了";
                const string item0 = "默认的完成方式";
                const string item1 = "传入错误码1";
                const string item2 = "直接设定完成提示信息";
                const string item3 = "通过EFinish结束";
                var result = ui.SelectFrom(hint,
                    item0,
                    item1,
                   item2,
                   item3);
                /*
                 * 当完成函数流程时.一定要手动调用Finish()函数
                 * 如果不进行调用,那么当C# using将调用LeafUI的Dispose函数时
                 * LeafUI将视本模块执行过程出现问题,将直接关闭
                */
                switch (result as string)
                {
                    case item0:
                        ui.Finish();//LeafUI将视为本模块执行成功,显示成功提示
                        break;
                    case item1:
                        ui.Finish(1);//LeafUI将根据传入的错误码进行分析,并设置完成提示
                        break;
                    case item2:
                        ui.Finish("功能结束");//LeafUI不进行结果判断,而是将传入的字符串作为结束提示
                        break;
                    default:
                        /*EFinish调用后将抛出一个可以信号异常
                         也就是说会立刻停止当前函数的流程,而不像
                         普通的Finish那样继续执行之后的语句
                         */
                        ui.EFinish();
                        //由于EFinish抛出异常,下面的代码无法被执行
                        System.Environment.Exit(1);
                        break;
                }
            }
        }
    }
}

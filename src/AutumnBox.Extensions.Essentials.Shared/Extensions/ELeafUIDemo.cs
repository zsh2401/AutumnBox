/*

* ==============================================================================
*
* Filename: ELeafUIDemo
* Description: 
*
* Version: 1.0
* Created: 2020/8/12 4:59:50
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open.LKit;
using System.Threading;

namespace AutumnBox.Extensions.Essentials.Extensions
{
    [ExtDeveloperMode]
    [ExtName("Leaf UI Demo", "zh-cn:叶之界面基础演示")]
    [ExtIcon("Resources.Icons.icon.ico")]
    [ExtAuth("zsh2401")]
    [ExtRequiredDeviceStates(AutumnBoxExtension.NoMatter)]
    [ExtPriority(ExtPriority.HIGH)]
    class ELeafUIDemo : LeafExtensionBase
    {
        [LMain]
        public void Main(ILeafUI _ui)
        {
            //确保UI资源能够正确释放
            using var ui = _ui;
            ui.Closing += (s, e) =>
            {
                return true;
            };
            //显示窗口
            ui.Show();
            ui.Println("使用ILeafUI.Println()打印的输出");

            //获取用户输入 InputString()
            //取消则返回NULL
            //无输入则为String.Empty(空字符串)
            var userName = ui.InputString("你好，怎么称呼你?", "小明") ?? "佚名";
            ui.ShowMessage($"你好，{userName}");

            //ui.Title = "自定义";  //默认标题为拓展模块名称
            ui.StatusInfo = "执行中"; //拓展模块执行状态信息


            //单选 SelectOne()
            //选择结果是选项原值，NULL则为用户取消输入
            var selectResult = ui.SelectOne("秋之盒的开发主要使用了什么语言?", "A: C#", "B: Java", "C: Python");
            var msg = "你选择了" + (selectResult ?? "未选");

            ui.ShowMessage(msg);
            ui.Println(msg);

            switch (selectResult)
            {
                case "A: C#":
                    ui.ShowMessage("正确");
                    break;
                case "B: Java":
                case "C: Python":
                case null:
                default:
                    ui.ShowMessage("答错啦!");
                    break;
            }

            // DoYN()
            //询问是否设置进度条
            if (ui.DoYN("看看进度条样式?不设置进度条则进度条一直循环"))
            {
                ui.Progress = 50;
                bool cancel = false;
                while (!cancel)
                {
                    //LeafUI.DoChoice()
                    //让用户选择是或否
                    switch (ui.DoChoice("对进度条的操作？达到100退出操作\n大于100：进度条消失\n小于0：进度条无限循环", "增加10", "减少10", "退出操作"))
                    {
                        case true:
                            ui.Progress += 10;
                            break;
                        case false:
                            ui.Progress -= 10;
                            break;
                        default:
                            cancel = true;
                            break;
                    }
                }
            }

            Thread.Sleep(3000);

            // DoYN()
            if (ui.DoYN("流程结束，使用Finish还是Shutdown?\nFinsh:显示结果\nShutdown:直接关闭", "Finish", "Shutdown"))
            {
                // Finish()
                ui.Finish(ui.InputString("结果是啥?", "随便输入点啥"));
            }
            else
            {
                // Shutdown()
                ui.ShowMessage("将直接关闭窗口");
                ui.Shutdown(); // 不显示结果画面直接关闭窗口
            }
        }
    }
}

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

namespace AutumnBox.Extensions.Essentials.Extensions
{
    [ExtDeveloperMode]
    [ExtName("Leaf UI Demo", "zh-cn:叶之界面基础演示")]
    [ExtIcon("Resources.Icons.icon.ico")]
    class ELeafUIDemo : LeafExtensionBase
    {
        [LMain]
        public void Main(ILeafUI _ui)
        {
            //确保UI资源能够正确释放
            using var ui = _ui;

            //显示窗口
            ui.Show();
            //ui.Title = "自定义";  //默认标题为拓展模块名称
            ui.StatusInfo = "执行中"; //拓展模块执行状态信息

            //单选
            var selectResult = ui.SelectOne("秋之盒的开发主要使用了什么语言?", "A: C#", "B: Java", "C: Python");
            ui.ShowMessage("你选择了" + selectResult);
            ui.Println("你选择了" + selectResult);

            switch (selectResult)
            {
                case "A":
                    ui.ShowMessage("正确");
                    break;
                case "B":
                case "C":
                default:
                    ui.ShowMessage("答错啦!");
                    break;
            }

            //询问是否设置进度条
            if (ui.DoYN("是否设置进度条?不设置进度条则进度条一直循环"))
            {
                int? progress = null;
                while (true)
                {
                    try
                    {
                        //解析ui.InputString输入的数字
                        progress = int.Parse(ui.InputString("输入1-100的数字", "30"));
                        if (progress > 0 && progress < 100)
                        {
                            break;
                        }
                    }
                    catch { }
                }
            }

            ui.Finish();
            //ui.Shutdown(); 不显示结果画面直接关闭窗口
        }
    }
}

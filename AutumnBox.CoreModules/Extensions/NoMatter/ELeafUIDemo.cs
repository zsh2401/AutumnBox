using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Attributes;
using AutumnBox.OpenFramework.LeafExtension.Kit;

namespace AutumnBox.CoreModules.Extensions.NoMatter
{
    [ExtName("Leaf UI Demo")]
    [ExtRequiredDeviceStates(LeafConstants.NoMatter)]
    [ExtDeveloperMode]
    [ExtIcon("Icons.Usersir.png")]
    public class ELeafUIDemo : LeafExtensionBase
    {
        /// <summary>
        /// 入口点函数
        /// 一个入口点函数要么名为Main,要么有LMain标记
        /// 这是LeafExtensionBase确定入口函数的方法
        /// </summary>
        /// <param name="test">
        /// 该参数被标记为LFromData
        /// 也就是说,该参数将会从模块启动键值数据中获取
        /// </param>
        /// <param name="ui">需要获取的UI,将会被LeafExtensionBase进行注入</param>
        [LMain]
        public void Main(ILeafUI ui)
        {
            /*
             * 一定要进行using,此处是为了确保LeafUI被正确释放
             * 否则当该函数内发生异常,LeafUI将无法被关闭
             */
            using (ui)
            {
                //将LeafUI的图标设置为本模块的图标
                ui.Icon = this.GetIconBytes();
                //绑定LeafUI关闭事件
                ui.CloseButtonClicked += (s, e) =>
                {
                    /*
                     * 当用户点击停止按钮时发生
                     * 请在此处销毁你调用的资源
                     * 如果你不想你的模块被中途停止,请返回false
                    */
                    e.CanBeClosed = false;
                };
                //显示UI
                ui.Show();
                //在输出框打印东西
                ui.WriteLine("hello!");
                /*
                 * 一定要调用Finish函数,否则会出现严重的问题
                * 另外,你也可以传入一个int数字,LeafUI会根据数字设置Tip
                * ui.Finish(ERR);
                * 或直接传入一个Tip
                * ui.Finish("结束!");
                */
                ui.Finish();
            }
        }
    }
}

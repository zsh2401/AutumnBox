using AutumnBox.GUI.Model;
using AutumnBox.GUI.MVVM;
using System.Collections.Generic;

namespace AutumnBox.GUI.ViewModel
{
    class VMOpenSource : ViewModelBase
    {
        public IEnumerable<OpenSourceProject> Projects => projects;


        private static readonly List<OpenSourceProject> projects =
            new List<OpenSourceProject>()
            {
                new OpenSourceProject("Newtonsoft.Json","Json.NET is a popular high-performance JSON framework for .NET","MIT","https://www.newtonsoft.com/json","JamesNK"),
                new OpenSourceProject("MaterialDesignInXAML","Google's Material Design in XAML & WPF, for C# & VB.Net. ","MIT","http://materialdesigninxaml.net","MaterialDesignInXAML"),
                new OpenSourceProject("dpmpro","在 dpm 原有的基础上, 增加 remove-all-users 与 remove-all-accounts 功能. ","have no lisence","https://github.com/web1n/dpmpro","web1n"),
                new OpenSourceProject("Dragablz","Dragable and tearable tab control for WPF ","MIT","http://dragablz.net","ButchersBoy"),
                 new OpenSourceProject("WPF","WPF is a .NET Core UI framework for building Windows desktop applications.","MIT","https://github.com/dotnet/wpf","Microsoft"),
                  new OpenSourceProject(".Net standard",".NET Standard","MIT","https://github.com/dotnet/standard","Microsoft"),
            };
    }
}

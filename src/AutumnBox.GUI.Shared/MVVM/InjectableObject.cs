using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.ObjectManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutumnBox.GUI.MVVM
{
    /// <summary>
    /// 表示一个属性可自动注入的对象
    /// </summary>
    public class InjectableObject
    {
        /// <summary>
        /// 指示是否在创建时注入属性
        /// </summary>
        protected virtual bool InjectProperties => true;

        /// <summary>
        /// 构造一个可注入对象
        /// </summary>
        public InjectableObject()
        {
            if ((!IsDesignMode()) && InjectProperties)
            {
                App.Current.Lake.InjectPropertyTo(this);
            }
        }

        /// <summary>
        /// 判断当前是否是设计模式
        /// </summary>
        /// <returns></returns>
        public bool IsDesignMode()
        {
            return DesignerProperties.GetIsInDesignMode(new DependencyObject());
        }
    }
}

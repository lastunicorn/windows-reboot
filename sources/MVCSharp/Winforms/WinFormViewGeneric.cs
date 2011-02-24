//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Collections.Generic;
using System.Text;
using MVCSharp.Core;
using MVCSharp.Core.Views;
using System.ComponentModel;
using System.Windows.Forms;

namespace MVCSharp.Winforms
{
    #region Documentation
    /// <summary>
    /// Base generic implementation of the <see cref="IView{T}"/> interface
    /// for windows forms presentation mechanism. Has a strongly typed
    /// association to the controller (of the parameter type T).
    /// <para>It is recommended to inherit your Windows view classes from
    /// this one instead of manually implementing the <see cref="IView{T}"/>
    /// interface.</para>
    /// </summary>
    /// <remarks>
    /// This class also implements <see cref="INotifiedView"/> interface,
    /// which tells the views manager to notify the view on such events
    /// as view initialization and activation. Default implementation
    /// methods do nothing but descendant classes may override them to
    /// add some functionality.</remarks>
    /// <typeparam name="T">Specifies the expected type of the
    /// associated controller. Must be a subtype of <see cref="IController"/>
    /// </typeparam>
    /// <example>
    /// In this example we declare a view type by deriving it from the
    /// WinFormView&lt;T&gt; class without a need to manually implement
    /// the IView&lt;T&gt; interface.
    /// <code>
    /// [WinformsView(typeof(MyTask), MyTask.MyView)]
    /// partial class MyView : WinFormView&lt;MyController&gt;
    /// {
    ///     public MyView()
    ///     {
    ///         InitializeComponent();
    ///     }
    /// 
    ///     private buttonDoAction_Click(object sender, EventArgs e)
    ///     {
    ///         Controller.MyAction();
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="MVCSharp.Winforms.Configuration.WinformsViewAttribute"/>
    /// <seealso cref="WinFormView"/>
    #endregion
    public class WinFormView<T> : Form, INotifiedView, IView<T> where T : class, IController
    {
        private T controller;
        private string viewName;

        #region Documentation
        /// <summary>
        /// Simple <see cref="IView{T}.Controller">IView&lt;T&gt;.Controller</see>
        /// implementation with backing field. Marked as virtual, so can be
        /// overriden in subclasses.
        /// </summary>
        #endregion
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual T Controller
        {
            get { return controller; }
            set { controller = value; }
        }

        #region Documentation
        /// <summary>
        /// <see cref="IView.Controller">IView.Controller</see>
        /// implementation done as a gateway to the strongly typed
        /// <see cref="WinFormView{T}.Controller"/> property.
        /// </summary>
        #endregion
        IController IView.Controller
        {
            get { return Controller; }
            set { Controller = value as T; }
        }

        #region Documentation
        /// <summary>
        /// <see cref="IView.ViewName"/> property simple implementation with
        /// backing field. Marked as virtual to be overrideable in sublasses.
        /// </summary>
        #endregion
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual string ViewName
        {
            get { return viewName; }
            set { viewName = value; }
        }

        #region Documentation
        /// <summary>
        /// INotifiedView.Activate empty implementation.
        /// Marked as virtual, so can be overriden in subclasses.
        /// </summary>
        #endregion
        public virtual void Activate(bool activate)
        {
        }

        #region Documentation
        /// <summary>
        /// INotifiedView.Initialize empty implementation.
        /// Marked as virtual, so can be overriden in subclasses.
        /// </summary>
        #endregion
        public virtual void Initialize()
        {
        }
    }
}

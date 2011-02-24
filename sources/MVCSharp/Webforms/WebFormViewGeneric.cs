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
using System.Web.UI;
using System.ComponentModel;

namespace MVCSharp.Webforms
{
    #region Documentation
    /// <summary>
    /// Base web page implementation of the <see cref="IView{T}"/>
    /// interface. Has a strongly typed association to the controller
    /// (of the parameter type T).
    /// <para>It is recommended to inherit your web view classes from
    /// this one instead of manually implementing the <see cref="IView{T}"/>
    /// interface.</para>
    /// </summary>
    /// <typeparam name="T">Specifies the expected type of the
    /// associated controller. Must be a subtype of <see cref="IController"/>
    /// </typeparam>
    /// <example>
    /// In this example we declare a view type by deriving it from the
    /// WebFormView&lt;T&gt; class without a need to manually implement
    /// the IView&lt;T&gt; interface.
    /// <code>
    /// public partial class MyView : WebFormView&lt;MyController&gt;
    /// {
    ///     private DoActionButton_Click(object sender, EventArgs e)
    ///     {
    ///         Controller.MyAction();
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="WebFormView"/>
    #endregion
    public class WebFormView<T> : Page, IView<T> where T : class, IController
    {
        private T controller;
        private string viewName;

        #region Documentation
        /// <summary>
        /// <see cref="IView{T}.Controller"/> property simple implementation with
        /// backing field. Marked as virtual to be overrideable in sublasses.
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
        /// <see cref="WebFormView{T}.Controller"/> property.
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
    }
}

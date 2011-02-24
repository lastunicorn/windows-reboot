//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using MVCSharp.Core.Views;
using MVCSharp.Core;

namespace MVCSharp.Winforms
{
    #region Documentation
    /// <summary>
    /// A base generic user control class implementing the <see cref="IView{T}"/>
    /// interface. Has a strongly typed association to the controller (of the
    /// parameter type T).
    /// <para>It is recommended to inherit your user control view classes from
    /// this one instead of manually implementing the <see cref="IView{T}"/>
    /// interface. Neccessary <see cref="IView{T}"/> members may be overriden.</para>
    /// </summary>
    /// <remarks>This class also implements the <see cref="INotifiedView"/>
    /// interface (with empty methods) so in derived classes it is
    /// possible to override Activate and Initialize methods specifying
    /// response to (de)activation and initialization events.
    /// </remarks>
    /// <seealso cref="WinUserControlView"/>
    #endregion
    public class WinUserControlView<T> : UserControl, INotifiedView, IView<T> where T : class, IController
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
        /// <see cref="WinUserControlView{T}.Controller"/> property.
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
        /// Marked as virtual so that can be overriden in subclasses.
        /// </summary>
        #endregion
        public virtual void Activate(bool activate)
        {
        }

        #region Documentation
        /// <summary>
        /// INotifiedView.Initialize empty implementation.
        /// Marked as virtual so that can be overriden in subclasses.
        /// </summary>
        #endregion
        public virtual void Initialize()
        {
        }
    }
}

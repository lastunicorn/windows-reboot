//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MVCSharp.Core.Views;
using MVCSharp.Core;

namespace MVCSharp.Silverlight
{
    #region Documentation
    /// <summary>
    /// Base implementation of the <see cref="IView"/> interface for
    /// Silverlight presentation mechanism. Inherit your view classes
    /// from this one and override IView members if necessary.
    /// </summary>
    #endregion
    public class UserControlView : UserControl, INotifiedView, IView
    {
        private IController controller;
        private string viewName;

        #region Documentation
        /// <summary>
        /// Simple IView.Controller implementation with backing field.
        /// Marked as virtual, so can be overriden in subclasses.
        /// </summary>
        #endregion
        public virtual IController Controller
        {
            get { return controller; }
            set { controller = value; }
        }

        #region Documentation
        /// <summary>
        /// Simple IView.ViewName implementation with backing field.
        /// Marked as virtual, so can be overriden in subclasses.
        /// </summary>
        #endregion
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

        #region Documentation
        /// <summary>
        /// Gets the content of this user control and casts it to the Panel class.
        /// </summary>
        #endregion
        public virtual UIElement ContentElement
        {
            get { return Content; }
        }
    }
}

//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using MVCSharp.Core;
using MVCSharp.Core.Views;

namespace MVCSharp.Webforms
{
    #region Documentation
    /// <summary>
    /// Base web page implementation of the <see cref="IView"/> interface.
    /// Instead of implementing the IView interface your web page view may
    /// simply inherit the WebFormView class.
    /// </summary>
    /// <remarks>It is recommended to use the generic version of this class
    /// - <see cref="WebFormView{T}"/> - with a strongly typed association
    /// to the controller and, thus, higher type safety.</remarks>
    /// <seealso cref="WebFormView{T}"/>
    #endregion
    public class WebFormView : Page, IView
    {
        private IController controller;
        private string viewName;

        #region Documentation
        /// <summary>
        /// <see cref="IView.Controller"/> property simple implementation with
        /// backing field. Marked as virtual to be overrideable in sublasses.
        /// </summary>
        #endregion
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual IController Controller
        {
            get { return controller; }
            set { controller = value; }
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

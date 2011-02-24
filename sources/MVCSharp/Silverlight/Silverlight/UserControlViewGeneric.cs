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
    /// Base generic implementation of the <see cref="IView{T}"/> interface for
    /// Silverlight presentation mechanism. Inherit your view classes
    /// from this one and override <see cref="IView{T}"/> members if necessary.
    /// </summary>
    #endregion
    public class UserControlView<T> : UserControlView, INotifiedView, IView<T> where T : class, IController
    {
        #region Documentation
        /// <summary>
        /// Simple <see cref="IView{T}.Controller">IView&lt;T&gt;.Controller</see>
        /// implementation with backing field. Marked as virtual, so can be
        /// overriden in subclasses.
        /// </summary>
        #endregion
        public virtual new T Controller
        {
            get { return base.Controller as T; }
            set { base.Controller = value as T; }
        }
    }
}

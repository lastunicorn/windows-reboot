//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Text;

namespace MVCSharp.Core.Views
{
    #region Documentation
    /// <summary>
    /// If a view class implements this interface (in addition to IView)
    /// then the views manager will notify corresponding view objects
    /// on activation and initialization events through the operations
    /// of this interface.
    /// </summary>
    /// <remarks>Note that not every views manager sends notifications
    /// to a view which implements INotifiedView. This feature depends
    /// on the views manager implementation.</remarks>
    #endregion
    public interface INotifiedView
    {
        #region Documentation
        /// <summary>
        /// Through this operation views (which implement INotifiedView)
        /// are notified about their (de)activation.
        /// </summary>
        /// <param name="activate">If <c>true</c> then the view is
        /// activated, otherwise it is deactivated.</param>
        #endregion
        void Activate(bool activate);

        #region Documentation
        /// <summary>
        /// Through this operation views (which implement INotifiedView)
        /// are notified about their initialization.
        /// </summary>
        #endregion
        void Initialize();
    }
}

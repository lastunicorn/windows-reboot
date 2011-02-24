//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;

namespace MVCSharp.Core.Configuration.Views
{
    #region Documentation
    /// <summary>
    /// Views managers throw this exception if unable to find an
    /// appropriate view among their <see cref="MVCSharp.Core.Views.IViewsManager.ViewInfos"/>
    /// collection.
    /// </summary>
    #endregion
    public class ViewInfoNotFoundException : Exception
    {
        #region Documentation
        /// <summary>
        /// Constructor taking the name of the view with missing description.
        /// </summary>
        #endregion
        public ViewInfoNotFoundException(string viewName)
            : base("ViewInfo object not found for '" + viewName + "' view")
        { }
    }
}

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
    /// Thrown when a views manager finds it impossible to
    /// create a view instance.
    /// </summary>
    #endregion
    public class ViewCreationException : Exception
    {
        #region Documentation
        /// <summary>
        /// Constructor taking the view name and view type as parameters.
        /// </summary>
        #endregion
        public ViewCreationException(string viewName, Type viewType)
            : base("Could not create '" + viewName + "' view object by its type:"
                    + Environment.NewLine + viewType.ToString())
        { }
    }
}

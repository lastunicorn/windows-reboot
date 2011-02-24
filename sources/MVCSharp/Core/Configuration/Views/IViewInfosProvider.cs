//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Text;
using System.Reflection;

namespace MVCSharp.Core.Configuration.Views
{
    #region Documentation
    /// <summary>
    /// Classes which implement this interface can extract information
    /// on all views declared within an assembly. <see cref="MVCConfiguration"/>
    /// class uses this object to populate its <see cref="MVCConfiguration.ViewInfosByTask">
    /// MVCConfiguration.ViewInfosByTask</see> property.
    /// </summary>
    /// <remarks>
    /// Different IViewInfosProvider implementations account for different ways
    /// to describe views.
    /// </remarks>
    /// <seealso cref="MVCSharp.Winforms.Configuration.WinformsViewInfosProvider"/>
    /// <seealso cref="MVCSharp.Webforms.Configuration.WebformsViewInfosProvider"/>
    #endregion
    public interface IViewInfosProvider
    {
        #region Documentation
        /// <summary>
        /// Extracts a set of ViewInfoCollection objects for each task within
        /// the specified assebmly.
        /// </summary>
        #endregion
        ViewInfosByTaskCollection GetFromAssembly(Assembly assembly);
    }
}

﻿//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Text;

namespace MVCSharp.Core.Configuration.Views
{
    #region Documentation
    /// <summary>
    /// ViewInfo objects are used by views managers for view creation
    /// and activation. Base ViewInfo objects contain only information
    /// on view type and name (<see cref="ViewType"/> and <see cref="ViewName"/>
    /// properties), whereas different <c>ViewInfo</c> subclasses
    /// define properties specific to the corresponding
    /// presentation platforms.
    /// </summary>
    /// <remarks>
    /// It is unlikely you will need to manually create ViewInfo objects.
    /// Typically they are generated by the framework from <see cref="ViewAttribute"/>
    /// and derived attributes.
    /// </remarks>
    /// <seealso cref="ViewAttribute"/>
    #endregion
    public class ViewInfo
    {
        private string viewName;
        private Type viewType;

        #region Documentation
        /// <summary>
        /// ViewInfo constructor. Creates ViewInfo object with specified
        /// <c>ViewType</c> and <c>ViewName</c> values.
        /// </summary>
        #endregion
        public ViewInfo(string viewName, Type viewType)
        {
            ViewName = viewName;
            ViewType = viewType;
        }

        #region Documentation
        /// <summary>
        /// Specifies the view name.
        /// </summary>
        #endregion
        public string ViewName
        {
            get { return viewName; }
            set { viewName = value; }
        }

        #region Documentation
        /// <summary>
        /// Defines view type as a part of a view description.
        /// </summary>
        #endregion
        public Type ViewType
        {
            get { return viewType; }
            set { viewType = value; }
        }
    }
}

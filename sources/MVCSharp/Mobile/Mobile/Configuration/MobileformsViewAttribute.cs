//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using MVCSharp.Core.Configuration.Views;

namespace MVCSharp.Mobile.Configuration
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class MobileformsViewAttribute:ViewAttribute
    {
        private bool m_ShowModal;

        #region Documentation
        /// <summary>
        /// Constructor taking the task type, and the view name as parameters.
        /// </summary>
        #endregion
        public MobileformsViewAttribute(Type taskType, string viewName) : base(taskType, viewName)
        { }

        #region Documentation
        /// <summary>
        /// Specifies whether a view should be displayed as a modal dialog box.
        /// </summary>
        #endregion

        public bool ShowModal
        {
            get { return m_ShowModal; }
            set { m_ShowModal = value; }
        }
    }
}

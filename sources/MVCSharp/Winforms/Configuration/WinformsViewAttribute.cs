//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Text;
using MVCSharp.Core.Configuration.Views;

namespace MVCSharp.Winforms.Configuration
{
    #region Documentation
    /// <summary>
    /// This attribute should by applied to view classes intended
    /// for windows forms presentation mechanism.
    /// </summary>
    /// <remarks>
    /// For this attribute to be properly processed <see cref="MVCSharp.Core.Configuration.MVCConfiguration.ViewInfosProviderType"/>
    /// property should be equal to the <see cref="WinformsViewInfosProvider"/>  type.
    /// If so, then <see cref="WinformsViewInfo"/> objects will be generated for
    /// each WinformsViewAttribute occurrence.
    /// </remarks>
    #endregion
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class WinformsViewAttribute : ViewAttribute
    {
        private bool showModal;
        private bool isMdiParent;
        private string mdiParent;

        #region Documentation
        /// <summary>
        /// Constructor taking the task type, and the view name as parameters.
        /// </summary>
        #endregion
        public WinformsViewAttribute(Type taskType, string viewName) : base(taskType, viewName)
        { }

        #region Documentation
        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        #endregion
        public WinformsViewAttribute()
        { }

        #region Documentation
        /// <summary>
        /// Specifies whether a view should be displayed as a modal dialog box.
        /// </summary>
        #endregion
        public bool ShowModal
        {
            get { return showModal; }
            set { showModal = value; }
        }

        #region Documentation
        /// <summary>
        /// Specifies whether a view should act as an MDI parent form.
        /// </summary>
        #endregion
        public bool IsMdiParent
        {
            get { return isMdiParent; }
            set { isMdiParent = value; }
        }

        #region Documentation
        /// <summary>
        /// Specifies the name of the view that should be an MDI parent for this view.
        /// </summary>
        #endregion
        public string MdiParent
        {
            get { return mdiParent; }
            set { mdiParent = value; }
        }
    }
}

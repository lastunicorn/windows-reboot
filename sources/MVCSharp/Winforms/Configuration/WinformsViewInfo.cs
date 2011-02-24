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
    /// Instances of this type describe views within the Windows Forms
    /// presentation platform. In addition to the view type it intoduces
    /// several properties specific to the Windows Forms presentation
    /// mechanism.
    /// <para>When the <see cref="WinformsViewsManager"/>activates a view
    /// it looks to the properties of the corresponding WinformsViewInfo
    /// object. For example a form will be shown as modal if
    /// <see cref="WinformsViewInfo.ShowModal"/> is set to true.</para>
    /// </summary>
    #endregion
    public class WinformsViewInfo : ViewInfo
    {
        private bool showModal;
        private bool isMdiParent;
        private string mdiParent;

        #region Documentation
        /// <summary>
        /// Constructor recieving the view name and type as parameters.
        /// </summary>
        #endregion
        public WinformsViewInfo(string viewName, Type viewType) : base(viewName, viewType)
        { }

        #region Documentation
        /// <summary>
        /// Specifies if a view should be displayed as a modal dialog.
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
        /// Specifies an MDI parent view name.
        /// </summary>
        #endregion
        public string MdiParent
        {
            get { return mdiParent; }
            set { mdiParent = value; }
        }
    }
}

//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Text;

namespace MVCSharp.Core.Configuration.Tasks
{
    #region Documentation
    /// <summary>
    /// Describes interaction point (view-controller pair). Holds a view
    /// name, the corresponding controller type and navigation information
    /// for this interaction point.
    /// </summary>
    /// <remarks><see cref="TaskInfo"/> object contains a collection of
    /// InteractionPointInfo objects, thus describing the whole task.</remarks>
    /// <seealso cref="TaskInfo"/>
    #endregion
    public class InteractionPointInfo
    {
        private string viewName;
        private InteractionPointInfoCollection navTargets = new InteractionPointInfoCollection();
        private Type controllerType;
        private bool isCommonTarget;

        #region Documentation
        /// <summary>
        /// Gets or sets the name of the view for this interaction point.
        /// </summary>
        #endregion
        public string ViewName
        {
            get { return viewName; }
            set { viewName = value; }
        }

        #region Documentation
        /// <summary>
        /// Gets descriptions for target interaction points. A view name or
        /// a navigation trigger name could be used as a key.
        /// </summary>
        #endregion
        public InteractionPointInfoCollection NavTargets
        {
            get { return navTargets; }
        }

        #region Documentation
        /// <summary>
        /// Gets or sets the type of the controller for an interation point.
        /// </summary>
        #endregion
        public Type ControllerType
        {
            get { return controllerType; }
            set { controllerType = value; }
        }

        #region Documentation
        /// <summary>
        /// If <c>true</c> then it is possible to navigate to this interaction
        /// point from any other.
        /// </summary>
        #endregion
        public bool IsCommonTarget
        {
            get { return isCommonTarget; }
            set { isCommonTarget = value; }
        }
    }
}

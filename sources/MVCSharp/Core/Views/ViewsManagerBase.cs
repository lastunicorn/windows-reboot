//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Text;
using MVCSharp.Core.Configuration.Views;

namespace MVCSharp.Core.Views
{
    #region Documentation
    /// <summary>
    /// Base <see cref="IViewsManager"/> implementation. To avoid repetitive IViewsManager
    /// implementation views managers should inherit from this class
    /// and override neccessary <see cref="IViewsManager"/> members.
    /// </summary>
    #endregion
    public class ViewsManagerBase : IViewsManager
    {
        private Navigator navigator;
        private ViewInfoCollection viewInfos;

        #region Documentation
        /// <summary>
        /// IViewsManager.ViewInfos simple implementation with backing fields.
        /// Marked as virtual so that can be overriden in subclasses.
        /// </summary>
        #endregion
        public virtual ViewInfoCollection ViewInfos
        {
            get { return viewInfos; }
            set { viewInfos = value; }
        }

        #region Documentation
        /// <summary>
        /// IViewsManager.Navigator simple implementation with backing fields.
        /// Marked as virtual so that can be overriden in subclasses.
        /// </summary>
        #endregion
        public virtual Navigator Navigator
        {
            get { return navigator; }
            set { navigator = value; }
        }

        #region Documentation
        /// <summary>
        /// IViewsManager.ActivateView empty implementation.
        /// Marked as virtual so that can be overriden in subclasses.
        /// </summary>
        #endregion
        public virtual void ActivateView(string viewName)
        {
        }

        #region Documentation
        /// <summary>
        /// IViewsManager.GetView empty implementation.
        /// Marked as virtual so that can be overriden in subclasses.
        /// </summary>
        #endregion
        public virtual IView GetView(string viewName)
        {
            return null;
        }
    }
}

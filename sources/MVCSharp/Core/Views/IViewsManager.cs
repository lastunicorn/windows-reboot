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
    /// Abstracts out the view switching logic. Different IViewsManager
    /// implementations correspond to different presentation platforms.
    /// </summary>
    /// <remarks>
    /// Views managers work tightly in pair with <see cref="Navigator"/>
    /// objects. Each navigator is linked to a views manager to delegate
    /// the view activation job to it. And vice versa: any views manager has
    /// an association to its navigator (<see cref="IViewsManager.Navigator">
    /// IViewsManager.Navigator</see> property), needed to obtain controllers for views.
    /// </remarks>
    /// <seealso cref="MVCSharp.Webforms.WebformsViewsManager"/>
    /// <seealso cref="MVCSharp.Winforms.WinformsViewsManager"/>
    #endregion
    public interface IViewsManager
    {
        #region Documentation
        /// <summary>
        /// Activates a view with specified name.
        /// </summary>
        /// <remarks>Activating a view requires some information about
        /// it (e.g. its type, display method, etc.). Such information is
        /// taken from the <see cref="MVCSharp.Core.Configuration.Tasks.TaskInfo.ViewInfos">
        /// TaskInfo.ViewInfos</see> property.</remarks>
        /// <param name="viewName">The name of the view to activate.</param>
        #endregion
        void ActivateView(string viewName);

        #region Documentation
        /// <summary>
        /// Returns a view with specified name without activating it.
        /// </summary>
        #endregion
        IView GetView(string viewName);

        #region Documentation
        /// <summary>
        /// Connects a views manager to its navigator. A views manager requires
        /// a navigator to obtain controllers for views.
        /// </summary>
        #endregion
        Navigator Navigator
        {
            get;
            set;
        }

        #region Documentation
        /// <summary>
        /// Colection of <see cref="ViewInfo"/> objects describing the views within
        /// the task.
        /// <remarks>The value of this property is taken from the
        /// <see cref="MVCSharp.Core.Configuration.Tasks.TaskInfo.ViewInfos">
        /// TaskInfo.ViewInfos</see> property when the task is started.</remarks>
        /// </summary>
        #endregion
        ViewInfoCollection ViewInfos
        {
            get;
            set;
        }
    }
}

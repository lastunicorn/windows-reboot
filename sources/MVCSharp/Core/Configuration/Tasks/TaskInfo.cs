//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Text;
using MVCSharp.Core.Configuration.Views;
using MVCSharp.Core.Tasks;

namespace MVCSharp.Core.Configuration.Tasks
{
    #region Documentation
    /// <summary>
    /// Contains descriptions for all interaction points and views
    /// within the task. Has methods to obtain navigation information
    /// for the task (see <see cref="TaskInfo.GetNextViewName"/> and
    /// <see cref="TaskInfo.CanNavigateToView"/>).
    /// </summary>
    /// <remarks>
    /// During a task start a TaskInfo object is obtained from
    /// the <see cref="MVCConfiguration.TaskInfos">MVCConfiguration.TaskInfos</see> collection.
    /// Then the TaskInfo instance is linked to the navigator for that
    /// task through the <see cref="Navigator.TaskInfo">Navigator.TaskInfo</see> property.
    /// After that the navigator uses the linked TaskInfo for getting
    /// the navigation information.
    /// </remarks>
    #endregion
    public class TaskInfo
    {
        private ViewInfoCollection viewInfos;
        private InteractionPointInfoCollection iPoints = new InteractionPointInfoCollection();
        private Type navigatorType;

        #region Documentation
        /// <summary>
        /// Gets or sets the associated collection of view descriptions
        /// for the task.
        /// </summary>
        #endregion
        public ViewInfoCollection ViewInfos
        {
            get { return viewInfos; }
            set { viewInfos = value; }
        }

        #region Documentation
        /// <summary>
        /// Gets or sets the associated collection of interaction point
        /// descriptions for the task.
        /// </summary>
        #endregion
        public InteractionPointInfoCollection InteractionPoints
        {
            get { return iPoints; }
        }

        #region Documentation
        /// <summary>
        /// Looks through the <see cref="TaskInfo.InteractionPoints"/> collection
        /// to retrieve the name of the next view, given the current view
        /// name and the navigation trigger name.
        /// </summary>
        /// <param name="currViewName">The name of the current view.</param>
        /// <param name="triggerName">The name of the navigation trigger (may be
        /// equal to the target view name).</param>
        /// <returns>The name of the next view or <c>null</c> if
        /// not found.</returns>
        #endregion
        public string GetNextViewName(string currViewName, string triggerName)
        {
            InteractionPointInfo currPoint = InteractionPoints[currViewName];
            InteractionPointInfo target = currPoint.NavTargets[triggerName];
            if (target == null)
            {
                InteractionPointInfo possibleCommonTarget = iPoints[triggerName];
                if ((possibleCommonTarget != null) && possibleCommonTarget.IsCommonTarget)
                    target = possibleCommonTarget;
            }

            if (target == null) return null;
            return target.ViewName;
        }

        #region Documentation
        /// <summary>
        /// Looks through the <see cref="TaskInfo.InteractionPoints"/> collection
        /// to determine whether the navigation to the destination view
        /// is possible.
        /// </summary>
        /// <param name="currViewName">The name of the current view.</param>
        /// <param name="destViewName">The name of the destination view.</param>
        /// <returns><c>True</c> if the navigation is possible, otherwise
        /// <c>false</c>.</returns>
        #endregion
        public bool CanNavigateToView(string currViewName, string destViewName)
        {
            if ((InteractionPoints[destViewName] != null) &&
                 InteractionPoints[destViewName].IsCommonTarget) return true;
            InteractionPointInfo currPoint = InteractionPoints[currViewName];
            foreach (InteractionPointInfo targetPoint in currPoint.NavTargets)
                if (targetPoint.ViewName == destViewName) return true;
            return false;
        }

        #region Documentation
        /// <summary>
        /// Specifies the type of the <see cref="Navigator"/> descendant
        /// to be associated with the task instance.
        /// </summary>
        /// <seealso cref="ITask.Navigator">ITask.Navigator</seealso>
        #endregion
        public Type NavigatorType
        {
            get { return navigatorType; }
            set { navigatorType = value; }
        }
    }
}

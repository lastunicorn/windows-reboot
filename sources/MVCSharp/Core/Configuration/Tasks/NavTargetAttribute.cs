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
    /// Used to declare navigation triggers with targets for interaction points.
    /// </summary>
    /// <remarks>Should be used with <see cref="TaskInfoByAttributesProvider"/> or
    /// <see cref="DefaultTaskInfoProvider"/> task info providers. They
    /// convert this attribute occurrences into <see cref="TaskInfo"/>
    /// objects.</remarks>
    /// <example>
    /// Below we declare a task with two interaction points and a "Next" trigger
    /// navigating from "View 1" to "View 2":
    /// <code>
    /// class MyTask : TaskBase
    /// {
    ///     [InteractionPoint(typeof(MyController))]
    ///     [NavTarget("Next", View2)]
    ///     public const string View1 = "View 1";
    /// 
    ///     [InteractionPoint(typeof(MyController))]
    ///     public const string View2 = "View 2";
    /// 
    ///     public override void OnStart(object param)
    ///     {
    ///         Navigator.NavigateDirectly(View1); // Initial interaction point is "View 1"
    ///         Navigator.Navigate("Next"); // Navigate to "View 2"
    ///     }
    /// }
    /// </code>
    /// </example>
    #endregion
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class NavTargetAttribute : Attribute
    {
        private string triggerName;
        private string viewName;

        #region Documentation
        /// <summary>
        /// Contructor taking the trigger name and the view name of the
        /// target interaction point.
        /// </summary>
        #endregion
        public NavTargetAttribute(string triggerName, string viewName)
        {
            this.triggerName = triggerName;
            this.viewName = viewName;
        }

        #region Documentation
        /// <summary>
        /// The name of the trigger initiating the transition.
        /// </summary>
        #endregion
        public string TriggerName
        {
            get { return triggerName; }
            set { triggerName = value; }
        }

        #region Documentation
        /// <summary>
        /// The view name for the target interaction point.
        /// </summary>
        #endregion
        public string ViewName
        {
            get { return viewName; }
            set { viewName = value; }
        }
	
    }
}

//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Text;
using System.Collections.Generic;
using MVCSharp.Core.Configuration.Tasks;
using MVCSharp.Core.Configuration;
using MVCSharp.Core.Views;
using MVCSharp.Core.Tasks;

namespace MVCSharp.Core
{
    #region Documentation
    /// <summary>
    /// Basing on the navigation information stored in the <see cref="TaskInfo"/>
    /// property performs navigation to the required view.
    /// <para>View activation is delegated to the linked
    /// <see cref="ViewsManager"/> object.</para>
    /// </summary>
    /// <remarks>
    /// During a task start Navigator object gets connected to the
    /// <see cref="ITask"/> object and the proper <see cref="IViewsManager"/>
    /// object (<see cref="Navigator.Task">Navigator.Task</see> and
    /// <see cref="Navigator.ViewsManager">Navigator.ViewsManager</see>
    /// properties respectively).
    /// </remarks>
    /// <example>
    /// After a task is started the navigator is often accessed from
    /// controllers:
    /// <code>
    /// class MyController : ControllerBase
    /// {
    ///     public void MyOperation()
    ///     {
    ///         Task.Navigator.Navigate(MainTask.SomeView);
    ///     }
    /// }
    /// </code>
    /// </example>
    #endregion
    public class Navigator
    {
        private ITask task;
        private TaskInfo taskInfo;
        private IViewsManager viewsManager;
        private Dictionary<string, IController> controllers = new Dictionary<string, IController>();

        #region Documentation
        /// <summary>
        /// This property connects a navigator to a task. Through it the navigator
        /// accesses the <see cref="ITask.CurrViewName">ITask.CurrViewName</see> property,
        /// necessary to perform navigation.
        /// </summary>
        /// <seealso cref="ITask"/>
        #endregion
        public ITask Task
        {
            get { return task; }
            set { task = value; }
        }

        #region Documentation
        /// <summary>
        /// Links Navigator to a <see cref="MVCSharp.Core.Configuration.Tasks.TaskInfo"/> object
        /// containing navigation information and other data.
        /// </summary>
        #endregion
        public TaskInfo TaskInfo
        {
            get { return taskInfo; }
            set { taskInfo = value; }
        }

        #region Documentation
        /// <summary>
        /// Gets or sets a views manager used by the navigator. Navigator
        /// only decides what view should be activated next. Views manager
        /// activates that view.
        /// </summary>
        #endregion
        public IViewsManager ViewsManager
        {
            get { return viewsManager; }
            set { viewsManager = value; }
        }

        #region Documentation
        /// <summary>
        /// Navigates to a next view by a given navigation trigger name.
        /// </summary>
        /// <param name="triggerName">Either a navigation trigger name
        /// or a view name (see example below)</param>
        /// <example>
        /// Below we declare a task with an example navigation structure
        /// and show how the Navigator.Navigate(...) method behaves:
        /// <code>
        /// class MyTask : ITask
        /// {
        ///     [IPoint(typeof(MyController), true)]
        ///     [NavTarget("Next", View2)]
        ///     public const string View1 = "View1";
        /// 
        ///     [IPoint(typeof(MyController), View3)]
        ///     public const string View2 = "View2";
        /// 
        ///     [IPoint(typeof(MyController))]
        ///     public const string View3 = "View3";
        /// 
        ///     public void OnStart(object param)
        ///     {
        ///         // Assume View1 is active
        /// 
        ///         Naviagtor.Navigate("Next"); // Activates "View2", as it is a target for the "Next" trigger
        ///         Naviagtor.Navigate("View3"); // Activates "View3", as it is a target for the "View2" view
        ///         Naviagtor.Navigate("View1"); // Activates "View1", as it is a common target ("true" in [IPoint] attribute)
        ///         Navigator.Navigate("View3"); // Throws an exception due to impossible navigation from "View1" to "View3"
        ///     }
        /// 
        ///     ...
        /// }
        /// </code>
        /// </example>
        /// <seealso cref="NavigateDirectly"/>
        /// <seealso cref="TryNavigateToView"/>
        /// <seealso cref="ActivateView"/>
        #endregion
        public virtual void Navigate(string triggerName)
        {
            string nextViewName = TaskInfo.GetNextViewName(Task.CurrViewName, triggerName);
            NavigateDirectly(nextViewName);
        }

        #region Documentation
        /// <summary>
        /// Navigates directly to the desired view, regardless of possible
        /// navigation routes.
        /// </summary>
        /// <seealso cref="Navigate"/>
        /// <seealso cref="TryNavigateToView"/>
        /// <seealso cref="ActivateView"/>
        #endregion
        public virtual void NavigateDirectly(string viewName)
        {
            if (viewName == Task.CurrViewName) return;
            ActivateView(viewName);
        }
        
        #region Documentation
        /// <summary>
        /// Navigates to a view (makes it current and activates it)
        /// even if it is already known to be current.
        /// </summary>
        /// <seealso cref="Navigate"/>
        /// <seealso cref="NavigateDirectly"/>
        /// <seealso cref="TryNavigateToView"/>
        #endregion
        public virtual void ActivateView(string viewName)
        {
            Task.CurrViewName = viewName;
            ViewsManager.ActivateView(Task.CurrViewName);
        }

        #region Documentation
        /// <summary>
        /// Activates the desired view if the navigation is possible,
        /// otherwise activates the current view.
        /// <para>This method is used by views managers when an end-user
        /// manually activates some view (clicks on a form). In this
        /// case calling TryNavigateToView(...) will return him to
        /// the previous view if the navigation is impossible.</para>
        /// </summary>
        /// <seealso cref="Navigate"/>
        /// <seealso cref="NavigateDirectly"/>
        /// <seealso cref="ActivateView"/>
        #endregion
        public virtual void TryNavigateToView(string viewName)
        {
            if (Task.CurrViewName == viewName) return;
            if (TaskInfo.CanNavigateToView(Task.CurrViewName, viewName))
                Task.CurrViewName = viewName;
            ViewsManager.ActivateView(Task.CurrViewName);
        }

        #region Documentation
        /// <summary>
        /// It is the navigator's job to maintain all controllers within a task.
        /// GetController method returns a controller for a specific view.
        /// </summary>
        /// <param name="viewName">The name of the view to return controller for.</param>
        /// <returns>The desired <see cref="IController"/> instance, or <c>null</c>
        /// if it cannot be found or created.</returns>
        /// <remarks>If the controller does not exists yet, it is created with
        /// a type obtained from the <see cref="TaskInfo"/> object.</remarks>
        #endregion
        public virtual IController GetController(string viewName)
        {
            IController result;
            controllers.TryGetValue(viewName, out result);
            if (result == null)
            {
                InteractionPointInfo iPointInf = TaskInfo.InteractionPoints[viewName];
                if ((iPointInf == null) || (iPointInf.ControllerType == null)) return null;
                result = CreateHelper.Create(iPointInf.ControllerType) as IController;
                result.Task = Task;
                controllers[viewName] = result;
            }
            return result;
        }
    }
}

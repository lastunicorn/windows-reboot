//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Text;

namespace MVCSharp.Core.Tasks
{
    #region Documentation
    /// <summary>
    /// Simple implementation of <see cref="ITask">ITask</see>
    /// interface with backing fields. All members are marked as virtual
    /// so it is possible to override them in derived classes.
    /// </summary>
    /// <example>
    /// TaskBase class is commonly used as base for task classes,
    /// the latter overriding its members:
    /// <code>
    /// class MyTask : TaskBase
    /// {
    ///     public override void OnStart(object param)
    ///     {
    ///         // Some actions
    ///     }
    /// }
    /// </code>
    /// </example>
    #endregion
    public class TaskBase : ITask
    {
        private Navigator navigator;
        private TasksManager tasksManager;
        private string currViewName;

        #region Documentation
        /// <summary>
        /// <see cref="ITask.OnStart"/> empty implementation.
        /// Can be overriden in subclasses.
        /// </summary>
        #endregion
        public virtual void OnStart(object param)
        {
        }

        #region Documentation
        /// <summary>
        /// <see cref="ITask.Navigator"/> simple implementation with backing field.
        /// Can be overriden in subclasses.
        /// </summary>
        #endregion
        public virtual Navigator Navigator
        {
            get { return navigator; }
            set { navigator = value; }
        }

        #region Documentation
        /// <summary>
        /// <see cref="ITask.TasksManager"/> simple implementation with backing field.
        /// Can be overriden in subclasses.
        /// </summary>
        #endregion
        public virtual TasksManager TasksManager
        {
            get { return tasksManager; }
            set { tasksManager = value; }
        }

        #region Documentation
        /// <summary>
        /// <see cref="ITask.CurrViewName"/> simple implementation with backing field.
        /// Can be overriden in subclasses.
        /// </summary>
        #endregion
        public virtual string CurrViewName
        {
            get { return currViewName; }
            set { currViewName = value; }
        }
    }
}

//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Text;
using MVCSharp.Core.Configuration;
using MVCSharp.Core.Configuration.Tasks;
using MVCSharp.Core.Views;

namespace MVCSharp.Core.Tasks
{
    #region Documentation
    /// <summary>
    /// TasksManager class is responsible for starting tasks. Task
    /// descriptions are obtained from the associated
    /// <see cref="TasksManager.Config">TasksManager.Config</see> object.
    /// </summary>
    #endregion
    public class TasksManager
    {
        private MVCConfiguration config;

        #region Documentation
        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        #endregion
        public TasksManager() { }

        #region Documentation
        /// <summary>
        /// Creates a tasks manager and links it to the MVCConfiguration
        /// instance passed as parameter.
        /// </summary>
        #endregion
        public TasksManager(MVCConfiguration mvcConfig)
        {
            this.Config = mvcConfig;
        }

        #region Documentation
        /// <summary>
        /// Gets or sets the associated <see cref="MVCConfiguration"/> object.
        /// </summary>
        #endregion
        public MVCConfiguration Config
        {
            get { return config; }
            set { config = value; }
        }

        #region Documentation
        /// <summary>
        /// Starts a task passing null to the <see cref="ITask.OnStart"/> method.
        /// </summary>
        #endregion
        public ITask StartTask(Type taskType)
        {
            return StartTask(taskType, null);
        }

        #region Documentation
        /// <summary>
        /// Starts a task passing <c>param</c> to the <see cref="ITask.OnStart">
        /// ITask.OnStart</see> operation. Task description (<see cref="TaskInfo"/>
        /// instance) and views manager type are taken from the
        /// associated <see cref="TasksManager.Config">TasksManager.Config</see>
        /// object.</summary>
        /// <param name="taskType">The type of the task to start.</param>
        /// <param name="param">Parameter passed to the <see cref="ITask.OnStart">
        /// ITask.OnStart</see> method.</param>
        #endregion
        public ITask StartTask(Type taskType, object param)
        {
            TaskInfo ti = GetTaskInfo(taskType);
            Navigator n = CreateNavigator(ti);
            IViewsManager vm = CreateHelper.Create(Config.ViewsManagerType) as IViewsManager;
            ITask t = CreateHelper.Create(taskType) as ITask;

            n.TaskInfo = ti;
            n.ViewsManager = vm;
            n.Task = t;
            vm.Navigator = n;
            vm.ViewInfos = ti.ViewInfos;
            t.TasksManager = this;
            t.Navigator = n;
            t.OnStart(param);
            return t;
        }

        private Navigator CreateNavigator(TaskInfo ti)
        {
            if (ti.NavigatorType != null)
                return CreateHelper.Create(ti.NavigatorType) as Navigator;
            else if (Config.NavigatorType != null)
                return CreateHelper.Create(Config.NavigatorType) as Navigator;
            else
                return new Navigator();
        }

        private TaskInfo GetTaskInfo(Type taskType)
        {
            return Config.TaskInfos[taskType];
        }
    }
}

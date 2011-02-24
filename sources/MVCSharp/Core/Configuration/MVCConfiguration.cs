//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using MVCSharp.Core.Configuration.Views;
using MVCSharp.Core.Configuration.Tasks;
using MVCSharp.Core.Tasks;

namespace MVCSharp.Core.Configuration
{
    #region Documentation
    /// <summary>
    /// Represents configuration data for an MVC# application. It
    /// contains descriptions for all tasks and views (
    /// <see cref="MVCConfiguration.TaskInfos"/> and <see cref="MVCConfiguration.ViewInfosByTask"/>
    /// properties) and other configuration data.
    /// </summary>
    /// <remarks>Every <see cref="MVCSharp.Core.Tasks.TasksManager"/> instance has its
    /// own MVCConfiguration object and starts tasks accordingly
    /// to this configuration.
    /// </remarks>
    #endregion
    public class MVCConfiguration
    {
        private Type navigatorType = typeof(Navigator);
        private Type viewInfosProviderType;
        private Type taskInfoProviderType;
        private Type viewsManagerType;
        private Assembly viewsAssembly;
        private List<Assembly> viewsAssemblies = new List<Assembly>();
        private ViewInfosByTaskCollection viewInfosByTask;
        private ITaskInfoProvider taskInfoProvider;
        private TaskInfoCollection taskInfos;

        #region Documentation
        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        #endregion
        public MVCConfiguration()
        {
            TaskInfos = new TaskInfoCollection();;
            TaskInfos.MVCConfig = this;
        }

        #region Documentation
        /// <summary>
        /// Returns the default MVCConfiguration instance with
        /// <see cref="MVCConfiguration.ViewInfosProviderType"/> set to <see cref="DefaultViewInfosProvider"/>
        /// and <see cref="MVCConfiguration.TaskInfoProviderType"/> set to <see cref="DefaultTaskInfoProvider"/>
        /// and <see cref="MVCConfiguration.ViewsAssembly"/> pointing to the calling assembly.
        /// </summary>
        #endregion
        public static MVCConfiguration GetDefault()
        {
            MVCConfiguration result = new MVCConfiguration();
            result.viewInfosProviderType = typeof(DefaultViewInfosProvider);
            result.taskInfoProviderType = typeof(DefaultTaskInfoProvider);
            result.ViewsAssembly = Assembly.GetCallingAssembly();
            return result;
        }

        #region Documentation
        /// <summary>
        /// Specifies the type of the <see cref="Navigator"/> descendants
        /// associated with task objects by default.
        /// </summary>
        /// <seealso cref="ITask.Navigator">ITask.Navigator</seealso>
        #endregion
        public Type NavigatorType
        {
            get { return navigatorType; }
            set { navigatorType = value; }
        }

        #region Documentation
        /// <summary>
        /// Specifies the type of the <see cref="IViewInfosProvider"/> implementation
        /// used to populate view descriptions (
        /// <see cref="MVCConfiguration.ViewInfosByTask"/> property).
        /// </summary>
        #endregion
        public Type ViewInfosProviderType
        {
            get { return viewInfosProviderType; }
            set { viewInfosProviderType = value; }
        }

        #region Documentation
        /// <summary>
        /// Specifies the type of the <see cref="ITaskInfoProvider"/> implementation
        /// used to populate task descriptions (
        /// <see cref="MVCConfiguration.TaskInfos">MVCConfiguration.TaskInfos</see> property).
        /// </summary>
        #endregion
        public Type TaskInfoProviderType
        {
            get { return taskInfoProviderType; }
            set { taskInfoProviderType = value; }
        }

        #region Documentation
        /// <summary>
        /// Specifies a views manager type (<see cref="MVCSharp.Core.Views.IViewsManager"/> implementation)
        /// to be used.
        /// </summary>
        #endregion
        public Type ViewsManagerType
        {
            get { return viewsManagerType; }
            set { viewsManagerType = value; }
        }

        #region Documentation
        /// <summary>
        /// Gets or sets an assembly containing view descriptions. These
        /// descriptions are converted by the <see cref="MVCConfiguration.ViewInfosProviderType"/> class
        /// to <see cref="ViewInfo"/> objects and stored in <see cref="MVCConfiguration.ViewInfosByTask">
        /// MVCConfiguration.ViewInfosByTask</see> collections.</summary>
        #endregion
        public Assembly ViewsAssembly
        {
            get { return viewsAssembly; }
            set { viewsAssembly = value; }
        }

        #region Documentation
        /// <summary>
        /// Collection of assemblies containing view descriptions. These
        /// descriptions are converted by the <see cref="MVCConfiguration.ViewInfosProviderType"/> class
        /// to <see cref="ViewInfo"/> objects and stored in <see cref="MVCConfiguration.ViewInfosByTask">
        /// MVCConfiguration.ViewInfosByTask</see> collections.</summary>
        #endregion
        public ICollection<Assembly> ViewsAssemblies
        {
            get { return viewsAssemblies; }
        }

        #region Documentation
        /// <summary>
        /// Gets a set of <see cref="ViewInfoCollection"/> objects for
        /// every task. These objects are produced by an instance of the
        /// <see cref="MVCConfiguration.ViewInfosProviderType">
        /// MVCConfiguration.ViewInfosProviderType</see> type</summary>
        #endregion
        public ViewInfosByTaskCollection ViewInfosByTask
        {
            get
            {
                if (viewInfosByTask == null)
                {
                    IViewInfosProvider viewInfosProvider = CreateHelper.Create(
                                        ViewInfosProviderType) as IViewInfosProvider;
                    if (ViewsAssembly != null)
                        viewInfosByTask = viewInfosProvider.GetFromAssembly(ViewsAssembly);
                    if (viewInfosByTask == null)
                        viewInfosByTask = new ViewInfosByTaskCollection();
                    foreach (Assembly asm in ViewsAssemblies)
                        viewInfosByTask.Add(viewInfosProvider.GetFromAssembly(asm));
                }
                return viewInfosByTask;
            }
        }

        #region Documentation
        /// <summary>
        /// Gets or sets an associated <see cref="ITaskInfoProvider"/> instance used
        /// to convert task types to <see cref="TaskInfo"/> objects.
        /// </summary>
        #endregion
        public ITaskInfoProvider TaskInfoProvider
        {
            get
            {
                if (taskInfoProvider == null)
                    taskInfoProvider = CreateHelper.Create(TaskInfoProviderType)
                                                            as ITaskInfoProvider;
                return taskInfoProvider;
            }
            set
            {
                taskInfoProvider = value;
            }
        }

        #region Documentation
        /// <summary>
        /// Associates MVCConfiguration to task descriptions.
        /// </summary>
        #endregion
        public TaskInfoCollection TaskInfos
        {
            get { return taskInfos; }
            set { taskInfos = value; }
        }

    }
}

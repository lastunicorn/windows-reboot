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
    /// The interface all tasks should implement.
    /// See each members' descriptions for details.
    /// </summary>
    #endregion
    public interface ITask
    {
        #region Documentation
        /// <summary>
        /// This operation is called by the framework when it starts a task.
        /// Specific task classes implement this method to define actions
        /// performed on task start.
        /// </summary>
        /// <param name="param">
        /// Parameter object passed to a task when it gets started.
        /// </param>
        #endregion
        void OnStart(object param);

        #region Documentation
        /// <summary>
        /// Each task should be linked to a proper <see cref="MVCSharp.Core.Navigator"/> instance.
        /// This property is responsible for such linking.
        /// </summary>
        #endregion
        Navigator Navigator
        {
            get;
            set;
        }

        #region Documentation
        /// <summary>
        /// Read/write property for associating a task with its context
        /// <see cref="MVCSharp.Core.Tasks.TasksManager"/> object.
        /// </summary>
        #endregion
        TasksManager TasksManager
        {
            get;
            set;
        }

        #region Documentation
        /// <summary>
        /// Used for holding the current view (interaction point) name.
        /// </summary>
        #endregion
        string CurrViewName
        {
            get;
            set;
        }
	
    }
}

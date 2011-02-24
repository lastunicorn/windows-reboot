using System;
using System.Collections.Generic;
using System.Text;

namespace MVCSharp.Core.Configuration.Tasks
{
    #region Documentation
    /// <summary>
    /// Base abstract Implementation of the <see cref="ITaskInfoProvider"/>
    /// interface. Contains single <see cref="TaskInfoProviderBase.GetTaskAttribute"/>
    /// helper method.
    /// </summary>
    /// <seealso cref="ITaskInfoProvider"/>
    /// <seealso cref="TaskAttribute"/>
    #endregion
    public abstract class TaskInfoProviderBase : ITaskInfoProvider
    {
        #region Documentation
        /// <summary>
        /// Abstract Implementation of the <see cref="ITaskInfoProvider.GetTaskInfo"/>
        /// method. Does nothing.
        /// </summary>
        #endregion
        public abstract TaskInfo GetTaskInfo(Type taskType);

        #region Documentation
        /// <summary>
        /// Helper method for extracting <see cref="TaskAttribute"/> attribute applied
        /// to a task type.
        /// </summary>
        /// <seealso cref="TaskAttribute"/>
        #endregion
        protected TaskAttribute GetTaskAttribute(Type taskType)
        {
            object[] attributes = taskType.GetCustomAttributes(typeof(TaskAttribute), false);
            if (attributes.Length == 0) return null;
            return attributes[0] as TaskAttribute;
        }
    }
}

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
    /// To declare a task a user should equip the task type with
    /// descriptive information (e.g. in a form of .NET custom attributes).
    /// The system then will extract this information by using a
    /// ITaskInfoProvider object.
    /// <para>Different ITaskInfoProvider implementations account for
    /// varoius ways to supplement a task type with task information.</para>
    /// </summary>
    /// <remarks>
    /// The ITaskInfoProvider object used by the system is set via the
    /// <see cref="MVCConfiguration.TaskInfoProvider">MVCConfiguration.TaskInfoProvider</see> and
    /// <see cref="MVCConfiguration.TaskInfoProviderType">MVCConfiguration.TaskInfoProviderType</see> properties.
    /// </remarks>
    /// <seealso cref="TaskInfoByAttributesProvider"/>
    /// <seealso cref="TaskInfoByXmlAttributeProvider"/>
    /// <seealso cref="DefaultTaskInfoProvider"/>
    #endregion
    public interface ITaskInfoProvider
    {
        #region Documentation
        /// <summary>
        /// Operation to generate <see cref="TaskInfo"/> object from
        /// a task type passed as parameter.
        /// </summary>
        #endregion
        TaskInfo GetTaskInfo(Type taskType);
    }
}

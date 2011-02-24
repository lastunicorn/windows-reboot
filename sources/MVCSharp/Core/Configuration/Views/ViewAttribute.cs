//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Text;

namespace MVCSharp.Core.Configuration.Views
{
    #region Documentation
    /// <summary>
    /// A base attribute for declaring views. See example code below.
    /// </summary>
    /// <remarks>
    /// ViewAttribute attributes are converted to <see cref="ViewInfo"/> objects
    /// by an instance of the <see cref="MVCConfiguration.ViewInfosProviderType">
    /// MVCConfiguration.ViewInfosProviderType</see> type.
    /// <para>Note that different presentation mechanisms
    /// may require different ViewAttribute descendants to
    /// declare views. For instance see <see cref="MVCSharp.Winforms.Configuration.WinformsViewAttribute"/> and
    /// <see cref="MVCSharp.Webforms.Configuration.WebformsViewAttribute"/> attributes.</para>
    /// </remarks>
    /// <example>
    /// Below we declare a view type that should be used for the <c>MyTask.MyView</c>
    /// view within the <c>MyTask</c> task.
    /// <code>
    /// [WinformsView(typeof(MyTask), MyTask.MyView)]
    /// class MyView : IView
    /// {
    ///     ...
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="MVCSharp.Winforms.Configuration.WinformsViewAttribute"/>
    /// <seealso cref="MVCSharp.Webforms.Configuration.WebformsViewAttribute"/>
    #endregion
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ViewAttribute : Attribute
    {
        private Type taskType;
        private string viewName;

        #region Documentation
        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        #endregion
        public ViewAttribute()
        {
        }

        #region Documentation
        /// <summary>
        /// Constructor which sets TaskType and ViewName taking
        /// values from parameters.
        /// </summary>
        #endregion
        public ViewAttribute(Type taskType, string viewName)
        {
            TaskType = taskType;
            ViewName = viewName;
        }

        #region Documentation
        /// <summary>
        /// Links an attribute to a specific task type.
        /// </summary>
        #endregion
        public Type TaskType
        {
            get { return taskType; }
            set { taskType = value; }
        }

        #region Documentation
        /// <summary>
        /// Links an attribute to a specific view within a task.
        /// </summary>
        #endregion
        public string ViewName
        {
            get { return viewName; }
            set { viewName = value; }
        }
    }
}

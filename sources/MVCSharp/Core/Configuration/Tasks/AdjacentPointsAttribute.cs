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
    /// With this attribute applied to a task type adjacent interaction
    /// points can be defined. By the term adjacent we mean interaction
    /// points with transitions possible between each two of them.
    /// </summary>
    /// <remarks>
    /// This attribute is valid only if specific <see cref="ITaskInfoProvider"/>
    /// implementations are used: <see cref="TaskInfoByAttributesProvider"/> or
    /// a composite <see cref="DefaultTaskInfoProvider"/>.
    /// </remarks>
    /// <example>
    /// The code below declares a task with three interaction points,
    /// and transitions possible between each two of them.
    /// <code>
    /// [AdjacentPoints(View1, View2, View3)]
    /// class MyTask : ITask
    /// {
    ///     [IPoint(typeof(MyController))]
    ///     public const string View1 = "View 1";
    ///     [IPoint(typeof(MyController))]
    ///     public const string View2 = "View 2";
    ///     [IPoint(typeof(MyController))]
    ///     public const string View3 = "View 3";
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="IPointAttribute"/>
    /// <seealso cref="NavTargetAttribute"/>
    #endregion
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AdjacentPointsAttribute : Attribute
    {
        private string[] adjacentViewNames;

        #region Documentation
        /// <summary>
        /// Constructor taking an array of adjacent view names.
        /// </summary>
        #endregion
        public AdjacentPointsAttribute(params string[] adjacentViewNames)
        {
            this.adjacentViewNames = adjacentViewNames;
        }

        #region Documentation
        /// <summary>
        /// Gets or sets the array of adjacent view names.
        /// </summary>
        #endregion
        public string[] AdjacentViewNames
        {
            get { return adjacentViewNames; }
            set { adjacentViewNames = value; }
        }
    }
}

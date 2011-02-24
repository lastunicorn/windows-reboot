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
    /// Attribute for describing interaction points within a task.
    /// </summary>
    /// <remarks>Should be used with <see cref="TaskInfoByAttributesProvider"/> or
    /// <see cref="DefaultTaskInfoProvider"/> task info providers. They
    /// convert this attribute occurrences into <see cref="TaskInfo"/>
    /// objects.</remarks>
    /// <example>
    /// Here is an example of how this attribute may be used:
    /// <code>
    /// class MyTask
    /// {
    ///     [InteractionPoint(typeof(MyController1))]
    ///     public const string iPoint1 = "View1";
    /// 
    ///     [IPoint(typeof(MyController2), true, iPoint1)]
    ///     public const string iPoint2 = "View2";
    /// 
    ///     [InteractionPoint(typeof(MyController1), iPoint1, iPoint2)]
    ///     public const string iPoint3 = "View3";
    /// 
    ///     [InteractionPoint(typeof(MyController1), true)]
    ///     [NavTarget("Next", iPoint3)]
    ///     [NavTarget("Previous", iPoint2)]
    ///     public const string iPoint4 = "View4";
    /// }
    /// </code>
    /// </example>
    #endregion
    [AttributeUsage(AttributeTargets.Field)]
    public class InteractionPointAttribute : Attribute
    {
        private bool isCommonTarget;
        private Type controllerType;
        private string[] navTargets;

        #region Documentation
        /// <summary>
        /// Constructor taking the controller type and an array of
        /// navigation targets for this interation point.
        /// </summary>
        #endregion
        public InteractionPointAttribute(Type controllerType,
                                         params string[] navTargets)
                : this (controllerType, false, navTargets){ }

        #region Documentation
        /// <summary>
        /// Constructor taking the controller type and a parameter specifying
        /// whether the interaction point is a common target for other points.
        /// </summary>
        #endregion
        public InteractionPointAttribute(Type controllerType,
                                         bool isCommonTarget)
                : this(controllerType, isCommonTarget, new string[0]) { }

        #region Documentation
        /// <summary>
        /// Constructor taking the controller type, a parameter specifying
        /// whether the interaction point is a common target for other points
        /// and an array of navigation targets for this interation point.
        /// </summary>
        #endregion
        public InteractionPointAttribute(Type controllerType, bool isCommonTarget,
                                                        params string[] navTargets)
        {
            this.controllerType = controllerType;
            this.isCommonTarget = isCommonTarget;
            this.navTargets = navTargets;
        }

        #region Documentation
        /// <summary>
        /// Specifies the type of the controller for the interaction point.
        /// </summary>
        #endregion
        public Type ControllerType
        {
            get { return controllerType; }
            set { controllerType = value; }
        }

        #region Documentation
        /// <summary>
        /// Specifies whether the interaction point should be a common target
        /// (i.e. a navigation can be done from any other interaction point to this one).
        /// </summary>
        #endregion
        public bool IsCommonTarget
        {
            get { return isCommonTarget; }
            set { isCommonTarget = value; }
        }

        #region Documentation
        /// <summary>
        /// Gets or sets an array of target navigation points represented by
        /// their view names. Any of these interaction points can be navigated
        /// to with the trigger name equal to the target view name.
        /// </summary>
        #endregion
        public string[] NavTargets
        {
            get { return navTargets; }
            set { navTargets = value; }
        }
    }

    #region Documentation
    /// <summary>
    /// Equivalent to the <see cref="InteractionPointAttribute"/> attribute.
    /// </summary>
    #endregion
    [AttributeUsage(AttributeTargets.Field)]
    public class IPointAttribute : InteractionPointAttribute
    {
        #region Documentation
        /// <summary>
        /// See <see cref="InteractionPointAttribute"/> equivalent constructor.
        /// </summary>
        #endregion
        public IPointAttribute(Type controllerType, params string[] navTargets)
            : base(controllerType, navTargets)
        { }

        #region Documentation
        /// <summary>
        /// See <see cref="InteractionPointAttribute"/> equivalent constructor.
        /// </summary>
        #endregion
        public IPointAttribute(Type controllerType,
                               bool isCommonTarget)
            : base(controllerType, isCommonTarget) { }

        #region Documentation
        /// <summary>
        /// See <see cref="InteractionPointAttribute"/> equivalent constructor.
        /// </summary>
        #endregion
        public IPointAttribute(Type controllerType, bool isCommonTarget,
            params string[] navTargets) : base(controllerType, isCommonTarget, navTargets)
        { }
    }
}

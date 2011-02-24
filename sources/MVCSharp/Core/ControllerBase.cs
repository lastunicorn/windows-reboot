//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Text;
using MVCSharp.Core.Tasks;
using MVCSharp.Core.Views;

namespace MVCSharp.Core
{
    #region Documentation
    /// <summary>
    /// Simple <see cref="IController"/> interface implementation
    /// with backing fields. Members are marked as virtual    
    /// so it is possible to override them in subclasses.
    /// </summary>
    /// <remarks>It is recommended to use the generic version of
    /// this class - <see cref="ControllerBase{TTask, TView}"/> -
    /// with strongly typed associations and, thus, higher type
    /// safety.</remarks>
    /// <example>ControllerBase class frees users from implementing
    /// <see cref="IController"/> manually, yet allowing to override
    /// IController members:
    /// <code>
    /// class MyController : ControllerBase
    /// {
    ///     public void MyOperation()
    ///     {
    ///         // Do something
    ///     }
    /// 
    ///     public override IView View
    ///     {
    ///         get { return base.View; }
    ///         set
    ///         {
    ///             base.View = value;
    ///             // Do view initialization
    ///         }
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="ControllerBase{TTask, TView}"/>
    /// <seealso cref="IController"/>
    #endregion
    public class ControllerBase : IController
    {
        private ITask task;
        private IView view;

        #region Documentation
        /// <summary>
        /// Simple <see cref="IController.Task">IController.Task</see> implementation
        /// with backing field. Can be overriden in subclasses.
        /// </summary>
        /// <remarks>
        /// The setter method of the Task property is often used
        /// to do the necessary controller initialization:
        /// <code>
        /// class MyController : ControllerBase
        /// {
        ///     public override ITask Task
        ///     {
        ///         get { return base.Task; }
        ///         set
        ///         {
        ///             base.Task = value;
        ///             // Do controller initialization
        ///         }
        ///     }
        /// }
        /// </code>
        /// </remarks>
        /// <example>
        /// Here we access the task state from the controller:
        /// <code>
        /// class MyController : ControllerBase
        /// {
        ///     public void DoSomething()
        ///     {
        ///         if ((Task as MyTask).Counter >= 5)
        ///             MessageBox.Show(&quot;You cannot do something more than five times.&quot;);
        ///         else
        ///             (Task as MyTask).Counter++;
        ///     }
        /// }
        /// </code>
        /// </example>
        #endregion
        public virtual ITask Task
        {
            get { return task; }
            set { task = value; }
        }

        #region Documentation
        /// <summary>
        /// Simple <see cref="IController.View">IController.View</see> implementation
        /// with backing field. Can be overriden in subclasses.
        /// </summary>
        /// <remarks>
        /// The setter method of the View property is often used
        /// to do the necessary view initialization:
        /// <code>
        /// class MyController : ControllerBase
        /// {
        ///     public override IView View
        ///     {
        ///         get { return base.View; }
        ///         set
        ///         {
        ///             base.View = value;
        ///             // Do view initialization
        ///         }
        ///     }
        /// }
        /// </code>
        /// </remarks>
        /// <example>
        /// Here we access the view from the controller:
        /// <code>
        /// class MyController : ControllerBase
        /// {
        ///     public void DoSomething()
        ///     {
        ///         if ((View as IMyView).InputValue &lt; 0)
        ///             MessageBox.Show(&quot;The input value should be not negative.&quot;);
        ///         else
        ///             (View as IMyView).OutputValue = Math.Sqrt((View as IMyView).InputValue);
        ///     }
        /// }
        /// </code>
        /// </example>
        #endregion
        public virtual IView View
        {
            get { return view; }
            set { view = value; }
        }
    }
}

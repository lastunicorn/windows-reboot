//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Collections.Generic;
using System.Text;
using MVCSharp.Core.Tasks;
using MVCSharp.Core.Views;

namespace MVCSharp.Core
{
    #region Documentation
    /// <summary>
    /// Base generic implementation of the <see cref="IController{TTask, TView}"/>
    /// interface. Has strongly typed associations to the linked task
    /// and view (of generic parameter types TTask and TView respectively)
    /// therefore eliminates the need of typecasting when accessing the
    /// task and the view.
    /// <para>Members are marked as virtual so it is possible to override
    /// them in subclasses.</para>
    /// </summary>
    /// <example>ControllerBase class frees users from implementing
    /// <see cref="IController{TTask, TView}"/> manually, yet allowing
    /// to override IController&lt;TTask, TView&gt; members:
    /// <code>
    /// class MyController : ControllerBase&lt;MyTask, IMyView&gt;
    /// {
    ///     public void MyOperation()
    ///     {
    ///         View.MyViewOperation(); // Typecasting NOT required
    ///     }
    /// 
    ///     public override MyTask Task
    ///     {
    ///         get { return base.Task; }
    ///         set
    ///         {
    ///             base.Task = value;
    ///             // Do controller initialization here
    ///         }
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <typeparam name="TTask">Specifies the expected type of the
    /// associated task. Must be a subtype of <see cref="ITask"/>.
    /// </typeparam>
    /// <typeparam name="TView">Specifies the expected type of the
    /// associated view. 
    /// </typeparam>
    #endregion
    public class ControllerBase<TTask, TView> : IController<TTask, TView>
        where TTask : class, ITask
        where TView : class
    {
        private TTask task;
        private TView view;

        #region Documentation
        /// <summary>
        /// A simple implementation (with backing fields) of the
        /// <see cref="IController{TTask, TView}.Task">
        /// IController&lt;TTask, TView&gt;.Task</see> generic interface property.
        /// Represents a strongly typed association to the linked task.
        /// </summary>
        /// <remarks>
        /// The setter method of the Task property is often used
        /// to do the necessary controller initialization:
        /// <code>
        /// class MyController : ControllerBase&lt;MyTask, IMyView&gt;
        /// {
        ///     public override MyTask Task
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
        /// Here we access the task state from the controller (note that no
        /// typecasting required to access the task):
        /// <code>
        /// class MyController : ControllerBase&lt;MyTask, IMyView&gt;
        /// {
        ///     public void DoSomething()
        ///     {
        ///         if (Task.MyCounter >= 5)
        ///             MessageBox.Show(&quot;You cannot do something more than five times.&quot;);
        ///         else
        ///             Task.MyCounter++;
        ///     }
        /// }
        /// </code>
        /// </example>
        #endregion
        public virtual TTask Task
        {
            get { return task; }
            set { task = value; }
        }

        #region Documentation
        /// <summary>
        /// A simple implementation (with backing fields) of the
        /// <see cref="IController{TTask, TView}.View">
        /// IController&lt;TTask, TView&gt;.View</see> generic interface property.
        /// Represents a strongly typed association to the linked view.
        /// </summary>
        /// <remarks>
        /// The setter method of the View property is often used
        /// to do the necessary view initialization:
        /// <code>
        /// class MyController : ControllerBase&lt;MyTask, IMyView&gt;
        /// {
        ///     public override IMyView View
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
        /// Here we access the view from the controller (note that no
        /// typecasting required to access the view):
        /// <code>
        /// class MyController : ControllerBase&lt;MyTask, IMyView&gt;
        /// {
        ///     public void DoSomething()
        ///     {
        ///         if (View.MyInputValue &lt; 0)
        ///             MessageBox.Show(&quot;The input value should be not negative.&quot;);
        ///         else
        ///             View.MyOutputValue = Math.Sqrt(View.MyInputValue);
        ///     }
        /// }
        /// </code>
        /// </example>
        #endregion
        public virtual TView View
        {
            get { return view; }
            set { view = value; }
        }

        #region Documentation
        /// <summary>
        /// <see cref="IController.Task">IController.Task</see> implementation
        /// done as a gateway to the strongly typed
        /// <see cref="ControllerBase{TTask, TView}.Task"/> property.
        /// </summary>
        /// 
        #endregion
        ITask IController.Task
        {
            get { return Task; }
            set { Task = value as TTask; }
        }

        #region Documentation
        /// <summary>
        /// <see cref="IController.View">IController.View</see> implementation
        /// done as a gateway to the strongly typed
        /// <see cref="ControllerBase{TTask, TView}.View"/> property.
        /// </summary>
        #endregion
        IView IController.View
        {
            get { return View as IView; }
            set { View = value as TView; }
        }
    }
}

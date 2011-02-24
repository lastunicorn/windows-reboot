//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Text;
using MVCSharp.Core.Views;
using MVCSharp.Core.Tasks;

namespace MVCSharp.Core
{
    #region Documentation
    /// <summary>
    /// All controller classes should implement this interface.
    /// In practice it is more handy to inherit from <see cref="ControllerBase"/>
    /// or from <see cref="ControllerBase{TTask, TView}"/> class than to manually
    /// implement IController members.
    /// </summary>
    /// <seealso cref="ControllerBase"/>
    /// <seealso cref="ControllerBase{TTask, TView}"/>
    #endregion
    public interface IController
    {
        #region Documentation
        /// <summary>
        /// Links controller to its context <see cref="ITask"/> object. The
        /// framework takes care of setting this property, so that every controller
        /// can access its task (see the example at the bottom).
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
        ITask Task
        {
            get;
            set;
        }

        #region Documentation
        /// <summary>
        /// Links controller to its view. The framework takes care of setting
        /// this property for every controller instance. Thus, in full
        /// accordance to the Model-View-Presenter pattern, any controller
        /// may access its view (see the example in the bottom).
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
        IView View
        {
            get;
            set;
        }
    }
}

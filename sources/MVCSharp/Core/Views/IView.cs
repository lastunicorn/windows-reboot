//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Text;

namespace MVCSharp.Core.Views
{
    #region Documentation
    /// <summary>
    /// All views should implement this interface.
    /// <para>For user convenience a generic version of this
    /// interface exists with a strongly typed association to
    /// the controller, see <see cref="IView{T}"/></para>
    /// </summary>
    /// <seealso cref="IView{T}"/>
    #endregion
    public interface IView
    {
        #region Documentation
        /// <summary>
        /// Used to connect a view to its controller.
        /// </summary>
        /// <remarks>
        /// This is one of the most important and widely used properties
        /// in the framework. Through it views pass control to controllers
        /// (see the example below).
        /// </remarks>
        /// <example>
        /// In this example the view handles a user gesture (button click)
        /// by passing control to the associated controller:
        /// <code>
        /// class MyView : IView
        /// {
        ///     // here the IView implementation goes
        ///     ...
        /// 
        ///     private void button_Click(object sender, EventArgs e)
        ///     {
        ///         (Controller as MyController).DoSomething();
        ///     }
        /// }
        /// </code>
        /// </example>
        #endregion
        IController Controller
        {
            get;
            set;
        }

        #region Documentation
        /// <summary>
        /// Used to assign a view its name.
        /// </summary>
        #endregion
        string ViewName
        {
            get;
            set;
        }
    }
}
//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Collections.Generic;
using System.Text;
using MVCSharp.Core.Views;
using MVCSharp.Core.Tasks;

namespace MVCSharp.Core
{
    #region Documentation
    /// <summary>
    /// Generic extension of the <see cref="IController"/> interface.
    /// Has strongly typed generic associations to the task and the
    /// view. Therefore no typecasting is required when accessing the
    /// associated task or view.
    /// <para>Istead of implementing this interface manually you
    /// might better use the generic base controller class -
    /// <see cref="ControllerBase{TTask, TView}"/></para>
    /// </summary>
    /// <remarks>The framework knows nothing about the generic types
    /// and deals only their non-generic versions. Generic types serve
    /// only for user convenience: to provide type-safety and reduce
    /// the amount of typecasts.</remarks>
    /// <typeparam name="TTask">Specifies the expected type of the
    /// associated task. Must be a subtype of <see cref="ITask"/>.
    /// </typeparam>
    /// <typeparam name="TView">Specifies the expected type of the
    /// associated view.
    /// </typeparam>
    /// <seealso cref="ControllerBase{TTask, TView}"/>
    #endregion
    public interface IController<TTask, TView> : IController where TTask : ITask
    {
        #region Documentation
        /// <summary>
        /// Gets or sets the associated task of the generic parameter
        /// type TTask.
        /// </summary>
        #endregion
        new TTask Task
        {
            get;
            set;
        }

        #region Documentation
        /// <summary>
        /// Gets or sets the associated view of the generic parameter
        /// type TView.
        /// </summary>
        #endregion
        new TView View
        {
            get;
            set;
        }
    }
}

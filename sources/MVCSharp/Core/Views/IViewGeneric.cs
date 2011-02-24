//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Collections.Generic;
using System.Text;

namespace MVCSharp.Core.Views
{
    #region Documentation
    /// <summary>
    /// Generic extension of the <see cref="IView"/> interface.
    /// Has a strongly typed generic association to the controller.
    /// Therefore no typecasting is required when accessing the
    /// associated controller.
    /// <para>Istead of implementing this interface manually you
    /// might better use generic base view classes such as
    /// <see cref="MVCSharp.Webforms.WebFormView{T}"/> or
    /// <see cref="MVCSharp.Winforms.WinFormView{T}"/>.</para>
    /// </summary>
    /// <remarks>The framework knows nothing about the generic types
    /// and deals only their non-generic versions. Generic types serve
    /// only for user convenience: to provide type-safety and reduce
    /// the amount of typecasts.</remarks>
    /// <typeparam name="T">Specifies the expected type of the
    /// associated controller. Must be a subtype of <see cref="IController"/>
    /// </typeparam>
    /// <seealso cref="MVCSharp.Webforms.WebFormView{T}"/>
    /// <seealso cref="MVCSharp.Winforms.WinFormView{T}"/>
    #endregion
    public interface IView<T> : IView where T : IController
    {
        #region Documentation
        /// <summary>
        /// Gets or sets the associated controller of the generic parameter
        /// type T.
        /// </summary>
        #endregion
        new T Controller
        {
            get;
            set;
        }
    }
}

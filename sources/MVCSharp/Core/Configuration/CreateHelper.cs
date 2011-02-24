//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Text;

namespace MVCSharp.Core.Configuration
{
    #region Documentation
    /// <summary>
    /// Helper class for creating objects.
    /// </summary>
    #endregion
    public class CreateHelper
    {
        #region Documentation
        /// <summary>
        /// Creates an object of specified type.
        /// </summary>
        #endregion
        public static object Create(Type t)
        {
            return t.GetConstructor(new Type[] { }).Invoke(new object[] { });
        }

        #region Documentation
        /// <summary>
        /// Creates an object of specified type with parameters passed to the constructor.
        /// </summary>
        #endregion
        public static object Create(Type t, params object[] parameters)
        {
            Type[] paramTypes = new Type[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
                paramTypes[i] = parameters[i].GetType();
            return t.GetConstructor(paramTypes).Invoke(parameters);
        }
    }
}

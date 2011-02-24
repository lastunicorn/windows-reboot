//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Text;
using System.Reflection;

namespace MVCSharp.Core.Configuration.Views
{
    #region Documentation
    /// <summary>
    /// This IViewInfosProvider implementation generates <see cref="ViewInfo"/> objects
    /// from <see cref="ViewAttribute"/> attributes.
    /// </summary>
    #endregion
    public class DefaultViewInfosProvider : IViewInfosProvider
    {
        #region Documentation
        /// <summary>
        /// Generates <see cref="ViewInfo"/> objects from <see cref="ViewAttribute"/> attributes
        /// found in the given assembly.
        /// </summary>
        #endregion
        public ViewInfosByTaskCollection GetFromAssembly(Assembly assembly)
        {
            ViewInfosByTaskCollection result = new ViewInfosByTaskCollection();
            foreach (Type t in assembly.GetTypes())
                foreach (ViewAttribute viewAttr in t.GetCustomAttributes(typeof(ViewAttribute), false))
                {
                    if (result[viewAttr.TaskType] == null)
                        result[viewAttr.TaskType] = new ViewInfoCollection();
                    result[viewAttr.TaskType].Add(newViewInfo(t, viewAttr));
                }
            return result;
        }

        #region Documentation
        /// <summary>
        /// Creates a ViewInfo object by a given ViewAttribute instance.
        /// </summary>
        #endregion
        protected virtual ViewInfo newViewInfo(Type viewType, ViewAttribute viewAttr)
        {
            return new ViewInfo(viewAttr.ViewName, viewType);
        }
    }
}

//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Text;
using System.Reflection;
using MVCSharp.Core.Configuration.Views;

namespace MVCSharp.Webforms.Configuration
{
    #region Documentation
    /// <summary>
    /// <see cref="IViewInfosProvider"/> implementation which generates WebformsViewInfo
    /// objects from <see cref="WebformsViewAttribute"/> attributes.
    /// </summary>
    #endregion
    public class WebformsViewInfosProvider : IViewInfosProvider
    {
        #region Documentation
        /// <summary>
        /// <see cref="IViewInfosProvider.GetFromAssembly"/> implementation.
        /// </summary>
        #endregion
        public ViewInfosByTaskCollection GetFromAssembly(Assembly assembly)
        {
            ViewInfosByTaskCollection result = new ViewInfosByTaskCollection();
            foreach (Type t in assembly.GetTypes())
                foreach (WebformsViewAttribute viewAttr in t.GetCustomAttributes(typeof(WebformsViewAttribute), false))
                {
                    if (result[viewAttr.TaskType] == null)
                        result[viewAttr.TaskType] = new WebformsViewInfoCollection();
                    (result[viewAttr.TaskType] as WebformsViewInfoCollection).Add(
                            new WebformsViewInfo(viewAttr.ViewName, viewAttr.ViewUrl));
                }
            return result;
        }
    }
}

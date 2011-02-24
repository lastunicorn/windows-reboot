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

namespace MVCSharp.Winforms.Configuration
{
    #region Documentation
    /// <summary>
    /// <see cref="IViewInfosProvider"/> implementation which creates
    /// <see cref="WinformsViewInfo"/> objects based on <see cref="WinformsViewAttribute"/>
    /// and <see cref="ViewAttribute"/> attributes.
    /// </summary>
    #endregion
    public class WinformsViewInfosProvider : DefaultViewInfosProvider
    {
        #region Documentation
        /// <summary>
        /// Creates a WinformsViewInfo object by a given ViewAttribute instance.
        /// </summary>
        #endregion
        protected override ViewInfo newViewInfo(Type viewType, ViewAttribute viewAttr)
        {
            WinformsViewInfo viewInfo = new WinformsViewInfo(viewAttr.ViewName, viewType);
            if (!(viewAttr is WinformsViewAttribute)) return viewInfo;

            viewInfo.IsMdiParent = (viewAttr as WinformsViewAttribute).IsMdiParent;
            viewInfo.MdiParent = (viewAttr as WinformsViewAttribute).MdiParent;
            viewInfo.ShowModal = (viewAttr as WinformsViewAttribute).ShowModal;

            return viewInfo;
        }
    }
}


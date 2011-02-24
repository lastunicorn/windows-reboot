//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using MVCSharp.Core.Configuration.Views;

namespace MVCSharp.Mobile.Configuration
{
    public class MobileformsViewInfosProvider : DefaultViewInfosProvider
    {
        #region Documentation
        /// <summary>
        /// Creates a WinformsViewInfo object by a given ViewAttribute instance.
        /// </summary>
        #endregion
        protected override ViewInfo newViewInfo(Type viewType, ViewAttribute viewAttr)
        {
            MobileformsViewInfo viewInfo = new MobileformsViewInfo(viewAttr.ViewName, viewType);
            if (!(viewAttr is MobileformsViewAttribute)) return viewInfo;
            
            viewInfo.ShowModal = (viewAttr as MobileformsViewAttribute).ShowModal;

            return viewInfo;
        }
    }
}

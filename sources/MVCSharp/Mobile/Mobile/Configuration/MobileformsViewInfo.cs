//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using MVCSharp.Core.Configuration.Views;

namespace MVCSharp.Mobile.Configuration
{
    public class MobileformsViewInfo:ViewInfo
    {
        private bool m_ShowModal;

        public MobileformsViewInfo(string viewName, Type viewType) : base(viewName, viewType)
        {
        }

        public bool ShowModal
        {
            get { return m_ShowModal; }
            set { m_ShowModal = value; }
        }
    }
}

//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MVCSharp.Core;
using MVCSharp.Core.Views;

namespace MVCSharp.Mobile
{
    public class MobileUserControlView : UserControl, IView, INotifiedView
    {
        public IController Controller
        { get; set; }

        public string ViewName
        { get; set; }

        public void Activate(bool activate)
        {
            
        }

        public void Initialize()
        {
            
        }
    }
}

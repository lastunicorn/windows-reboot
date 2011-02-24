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
    public class MobileUserControlView<T> : UserControl, INotifiedView, IView<T> where T : class, IController
    {
        private T controller;
        private string viewName;

        public void Activate(bool activate)
        {
            
        }

        public void Initialize()
        {
            
        }

        IController IView.Controller
        {
            get { return controller; }
            set { controller = value as T; }
        }

        public T Controller
        { get { return controller; } set { controller = value; } }

        public string ViewName
        {
            get{ return viewName;}
            set{ viewName = value;}
        }
    }
}

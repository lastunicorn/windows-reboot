//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System.Windows.Forms;
using MVCSharp.Core;
using MVCSharp.Core.Views;

namespace MVCSharp.Mobile
{
    public class MobileFormView:Form, IView, INotifiedView
    {
        private IController m_Controller;
        private string m_ViewName;

        public IController Controller
        {
            get { return m_Controller; }
            set { m_Controller = value; }
        }

        public string ViewName
        {
            get { return m_ViewName; }
            set { m_ViewName = value; }
        }


        public virtual void Activate(bool activate)
        {
            
        }

        public virtual void Initialize()
        {
            
        }
    }
}

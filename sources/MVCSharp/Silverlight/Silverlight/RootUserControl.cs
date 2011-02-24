//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Reflection;
using MVCSharp.Core.Views;
using MVCSharp.Core;

namespace MVCSharp.Silverlight
{
    public class RootUserControl : UserControl
    {
        private int topZIndex = int.MinValue;

        public RootUserControl()
        {
            Application.Current.RootVisual = this;
            Content = new Grid();
        }

        public Grid ContentPanel
        {
            get { return Content as Grid; }
        }

        public void SetView(UserControlView view)
        {
            if (view.Parent == null)
                ContentPanel.Children.Add(view);
            if (view.Parent == ContentPanel)
                Canvas.SetZIndex(view, TopZIndex);
            view.Visibility = Visibility.Visible;
            view.Focus();
        }

        private int TopZIndex
        {
            get { return topZIndex++; }
        }
    }
}

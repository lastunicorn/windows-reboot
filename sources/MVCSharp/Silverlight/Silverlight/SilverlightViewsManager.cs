//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MVCSharp.Core.Views;
using MVCSharp.Core.Configuration;
using MVCSharp.Core.Configuration.Views;
using System.Reflection;

namespace MVCSharp.Silverlight
{
    #region Documentation
    /// <summary>
    /// Works with views represented as Silverlight 2.0 user controls.
    /// </summary>
    #endregion
    public class SilverlightViewsManager : ViewsManagerBase
    {
        private Dictionary<string, UserControlView> views = new Dictionary<string, UserControlView>();
        private IView prevActiveView;
        private bool viewActivatedInCode = false;
        private List<UserControlView> parentFocusedViews = new List<UserControlView>();
        
        public static readonly RootUserControl rootControl = new RootUserControl();

        #region Documentation
        /// <summary>
        /// <see cref="IViewsManager.ActivateView">IViewsManager.ActivateView</see> implementation.
        /// </summary>
        #endregion
        public override void ActivateView(string viewName)
        {
            UserControlView view = FindOrCreateView(viewName);
            NotifyViewsOnActivation(view);
            ActivateView(view);
        }

        private void ActivateView(UserControlView view)
        {
            viewActivatedInCode = true;
            rootControl.SetView(view);
            viewActivatedInCode = false;
        }

        private void NotifyViewsOnActivation(IView activatedView)
        {
            INotifiedView prActiveNotifiedView = prevActiveView as INotifiedView;
            if (prActiveNotifiedView != null) prActiveNotifiedView.Activate(false);
            INotifiedView activatedNotifiedView = activatedView as INotifiedView;
            if (activatedNotifiedView != null) activatedNotifiedView.Activate(true);
            prevActiveView = activatedView;
        }

        private UserControlView FindOrCreateView(string viewName)
        {
            UserControlView result;
            if (views.TryGetValue(viewName, out result))
                return result;

            ViewInfo viewInf = ViewInfos[viewName];
            if (viewInf == null) throw new ViewInfoNotFoundException(viewName);
            result = CreateHelper.Create(ViewInfos[viewName].ViewType) as UserControlView;
            if (result == null) throw new ViewCreationException(viewName,
                                               ViewInfos[viewName].ViewType);
            result.ViewName = viewName;
            InitializeView(result);

            return result;
        }

        #region Documentation
        /// <summary>
        /// Method to perform view initialization.
        /// </summary>
        #endregion
        protected virtual void InitializeView(UserControlView view)
        {
            views[view.ViewName] = view;
            view.Controller = Navigator.GetController(view.ViewName);
            if (view.Controller != null)
                view.Controller.View = view;
            view.GotFocus += new RoutedEventHandler(view_GotFocus);
            NotifyOnInitialization(view);
            InitializeChildViews(view.ContentElement);
        }

        private void view_GotFocus(object sender, RoutedEventArgs e)
        {
            if (viewActivatedInCode) return;
            UserControlView focusedView = sender as UserControlView;
            if (parentFocusedViews.Contains(focusedView))
            {
                parentFocusedViews.Remove(focusedView);
                return;
            }
            PopulateParentFocusedViews(focusedView);
            Navigator.TryNavigateToView((sender as IView).ViewName);
        }

        private void PopulateParentFocusedViews(UserControlView focusedView)
        {
            parentFocusedViews.Clear();
            UserControlView nextParent = GetParentView(focusedView);
            while (nextParent != null)
            {
                parentFocusedViews.Add(nextParent);
                nextParent = GetParentView(nextParent);
            }
        }

        private UserControlView GetParentView(UserControlView view)
        {
            FrameworkElement nextParent = view.Parent as FrameworkElement;
            while (nextParent != null)
            {
                if (nextParent is UserControlView)
                    return nextParent as UserControlView;
                nextParent = nextParent.Parent as FrameworkElement;
            }
            return null;
        }

        #region Documentation
        /// <summary>
        /// Initializes user control views located inside the container given.
        /// </summary>
        #endregion
        protected void InitializeChildViews(UIElement content)
        {
            if (content == null) return;
            if (content is Panel)
                foreach (UIElement child in (content as Panel).Children)
                    InitializeChildViews(child);
            else if (content is UserControlView)
                InitializeView(content as UserControlView);
            else if (content is ContentControl)
                InitializeChildViews((content as ContentControl).Content as UIElement);
            else if (content is Border)
                InitializeChildViews((content as Border).Child);
        }

        private bool IsInitialized(IView view)
        {
            return views.ContainsKey(view.ViewName);
        }

        #region Documentation
        /// <summary>
        /// Notifies the view about its initialization if the view
        /// class implements the <see cref="INotifiedView"/> interface.
        /// </summary>
        /// <param name="view">The view to be notified.</param>
        #endregion
        protected void NotifyOnInitialization(IView view)
        {
            INotifiedView notifiedView = view as INotifiedView;
            if (notifiedView != null)
                notifiedView.Initialize();
        }

        #region Documentation
        /// <summary>
        /// Returns the default MVCConfiguration instance (obtained via
        /// <see cref="MVCConfiguration.GetDefault">MVCConfiguration.GetDefault</see>) with
        /// <see cref="MVCConfiguration.ViewsManagerType"/> set to <see cref="SilverlightViewsManager"/>.
        /// </summary>
        #endregion
        public static MVCConfiguration GetDefaultConfig()
        {
            MVCConfiguration defaultConf = MVCConfiguration.GetDefault();
            defaultConf.ViewsAssembly = Assembly.GetCallingAssembly();
            defaultConf.ViewsManagerType = typeof(SilverlightViewsManager);
            return defaultConf;
        }
    }
}

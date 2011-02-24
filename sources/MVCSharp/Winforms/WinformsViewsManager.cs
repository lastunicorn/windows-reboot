//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;
using MVCSharp.Core.Views;
using MVCSharp.Core.Configuration;
using MVCSharp.Core.Configuration.Views;
using MVCSharp.Winforms.Configuration;

namespace MVCSharp.Winforms
{
    #region Documentation
    /// <summary>
    /// Works with views represented as windows forms and user controls.
    /// </summary>
    /// <remarks>
    /// <see cref="ViewInfo"/> objects for this views manager should be of
    /// <see cref="WinformsViewInfo"/> type. That is why the WinformsViewsManager
    /// is often used in combination with <see cref="MVCConfiguration.ViewInfosProviderType"/>
    /// set to <see cref="WinformsViewInfosProvider"/>.
    /// </remarks>
    #endregion
    public class WinformsViewsManager : ViewsManagerBase
    {
        #region Documentation
        /// <summary>
        /// Used to hold all view instances.
        /// </summary>
        #endregion
        protected Hashtable views = new Hashtable();
        #region Documentation
        /// <summary>
        /// Used to notify the previously active view on its deactivation.
        /// </summary>
        #endregion
        protected IView prevActiveView;
        #region Documentation
        /// <summary>
        /// A flag indicating that the last view activation has been done via code.
        /// </summary>
        #endregion
        protected bool viewActivatedInCode = false;
        #region Documentation
        /// <summary>
        /// Holds currently active view instance.
        /// </summary>
        #endregion
        protected Control activeView;

        #region Documentation
        /// <summary>
        /// <see cref="IViewsManager.ActivateView">IViewsManager.ActivateView</see> implementation.
        /// </summary>
        #endregion
        public override void ActivateView(string viewName)
        {
            IView view = FindOrCreateView(viewName);
            NotifyViewsOnActivation(view);
            if (view is Form)
                ActivateFormView(view);
            else if (view is UserControl)
                ActivateUserControlView(view);
        }

        #region Documentation
        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        #endregion
        public WinformsViewsManager()
        {
            Application.Idle += Application_Idle;
        }

        #region Documentation
        /// <summary>
        /// <see cref="IViewsManager.GetView">IViewsManager.GetView</see> implementation.
        /// </summary>
        #endregion
        public override IView GetView(string viewName)
        {
            return FindOrCreateView(viewName);
        }

        #region Documentation
        /// <summary>
        /// A method used to notify views about their (de)activation.
        /// </summary>
        #endregion
        protected virtual void NotifyViewsOnActivation(IView activatedView)
        {
            INotifiedView prActiveNotifiedView = prevActiveView as INotifiedView;
            if (prActiveNotifiedView != null) prActiveNotifiedView.Activate(false);
            INotifiedView activatedNotifiedView = activatedView as INotifiedView;
            if (activatedNotifiedView != null) activatedNotifiedView.Activate(true);
            prevActiveView = activatedView;
        }

        #region Documentation
        /// <summary>
        /// Method to activate a view if it happens to be a form.
        /// Can be overriden in subclasses.
        /// </summary>
        #endregion
        protected virtual void ActivateFormView(IView view)
        {
            Form form = view as Form;
            WinformsViewInfo viewInf = GetViewInfo(view.ViewName);
            if (viewInf.ShowModal)
            {
                if (!form.Visible) form.ShowDialog();
            }
            else
            {
                viewActivatedInCode = true;
                form.Show();
                form.Activate();
                viewActivatedInCode = false;
            }
        }

        #region Documentation
        /// <summary>
        /// Method to activate a view if it happens to be a user control.
        /// Can be overriden in subclasses.
        /// </summary>
        #endregion        
        protected virtual void ActivateUserControlView(IView view)
        {
            UserControl uc = view as UserControl;
            viewActivatedInCode = true;
            uc.FindForm().Show();
            uc.FindForm().Activate();
            uc.Select();
            uc.Focus();
            viewActivatedInCode = false;
        }

        #region Documentation
        /// <summary>
        /// Method to find a view by its name or create a new one
        /// if nothing was found.
        /// </summary>
        #endregion
        protected IView FindOrCreateView(string viewName)
        {
            IView result = views[viewName] as IView;
            if (result == null)
            {
                WinformsViewInfo viewInf = GetViewInfo(viewName);
                if (viewInf == null) throw new ViewInfoNotFoundException(viewName);
                result = CreateHelper.Create(ViewInfos[viewName].ViewType) as IView;
                if (result == null) throw new ViewCreationException(viewName,
                                                   ViewInfos[viewName].ViewType);
                result.ViewName = viewName;
                if (result is UserControl)
                    InitializeUserControlView(result as UserControl);
                else if (result is Form)
                    InitializeFormView(result as Form, viewInf);
            }
            return result;
        }

        private WinformsViewInfo GetViewInfo(string viewName)
        {
            WinformsViewInfo viewInf = ViewInfos[viewName] as WinformsViewInfo;
            if (viewInf == null)
                viewInf = new WinformsViewInfo(ViewInfos[viewName].ViewName, ViewInfos[viewName].ViewType);
            return viewInf;
        }

        #region Documentation
        /// <summary>
        /// Method to initialize a view if it happens to be a user control.
        /// Can be overriden in subclasses.
        /// </summary>
        #endregion 
        protected virtual void InitializeUserControlView(UserControl userControlView)
        {
            InitializeView(userControlView as IView);
            userControlView.Enter += new EventHandler(View_ActivatedManually);
            userControlView.Leave += new EventHandler(View_Deactivate);
            userControlView.Disposed += new EventHandler(WinformsView_Disposed);
            NotifyInitialize(userControlView as IView);
            InitializeChildViews(userControlView);
        }

        #region Documentation
        /// <summary>
        /// Method to initialize a view if it happens to be a form.
        /// Can be overriden in subclasses.
        /// </summary>
        #endregion
        protected virtual void InitializeFormView(Form form, WinformsViewInfo viewInf)
        {
            InitializeView(form as IView);
            form.Activated += new EventHandler(View_ActivatedManually);
            form.Deactivate += new EventHandler(View_Deactivate);
            form.Disposed += new EventHandler(WinformsView_Disposed);
            form.IsMdiContainer = viewInf.IsMdiParent;
            string mdiParent = viewInf.MdiParent;
            if (mdiParent != null)
                form.MdiParent = views[mdiParent] as Form;
            NotifyInitialize(form as IView);
            InitializeChildViews(form);
        }

        #region Documentation
        /// <summary>
        /// Method to perform a general view initialization
        /// (regardless of its specific type)/
        /// </summary>
        #endregion
        protected virtual void InitializeView(IView view)
        {
            if (view.ViewName == null)
            {
                WinformsViewInfo viewInf = ViewInfos[view.GetType()] as WinformsViewInfo;
                if ((viewInf == null) || (viewInf.ViewName == null)) return;
                view.ViewName = viewInf.ViewName;
            }
            views[view.ViewName] = view;
            view.Controller = Navigator.GetController(view.ViewName);
            if (view.Controller != null)
                view.Controller.View = view;
        }

        #region Documentation
        /// <summary>
        /// Event hanlder for the manual view activation (by the end-user).
        /// </summary>
        #endregion
        protected void View_ActivatedManually(object sender, EventArgs e)
        {
            activeView = sender as Control;
            if (viewActivatedInCode) return;
            Navigator.TryNavigateToView((sender as IView).ViewName);
        }

        #region Documentation
        /// <summary>
        /// Event hanlder view de-activation.
        /// </summary>
        #endregion
        protected virtual void View_Deactivate(object sender, EventArgs e)
        {
            if (sender is Form)
                activeView = FindContainerView(sender as Control);
            else if (!(activeView is Form))
                activeView = FindContainerView(sender as Control);
            if (activeView == null) return;
            if (views[(activeView as IView).ViewName] == null)
                activeView = null;
        }

        #region Documentation
        /// <summary>
        /// A method called whenever the application gets idle and used to eliminate
        /// possible mismatches between the current view and actually active view.
        /// </summary>
        #endregion
        protected void Application_Idle(object sender, EventArgs e)
        {
            if ((activeView == null) ||
                (activeView == views[Navigator.Task.CurrViewName]))
                return;
            Navigator.TryNavigateToView((activeView as IView).ViewName);
        }

        #region Documentation
        /// <summary>
        /// Used to find a container view for a specific control (view).
        /// </summary>
        #endregion
        protected Control FindContainerView(Control currView)
        {
            Control result = GetContainerControl(currView);
            while ((result != null) && !(result is IView))
                result = GetContainerControl(result);
            return result;
        }

        private Control GetContainerControl(Control view)
        {
            if (view is Form)
                return (view as Form).MdiParent;
            return view.Parent;
        }

        void WinformsView_Disposed(object sender, EventArgs e)
        {
            if (!(sender is IView)) return;
            views.Remove((sender as IView).ViewName);
        }

        #region Documentation
        /// <summary>
        /// Notifies the view about its initialization if the view
        /// class implements the <see cref="INotifiedView"/> interface.
        /// </summary>
        /// <param name="view">The view to be notified.</param>
        #endregion
        protected void NotifyInitialize(IView view)
        {
            INotifiedView winformsView = view as INotifiedView;
            if (winformsView != null)
                winformsView.Initialize();
        }

        #region Documentation
        /// <summary>
        /// Initializes user control views located inside a container control.
        /// </summary>
        #endregion
        protected void InitializeChildViews(Control container)
        {
            foreach (Control c in container.Controls)
            {
                IView childView = c as IView;
                if ((childView != null) && (!IsInitialized(childView)))
                    InitializeUserControlView(childView as UserControl);
                else
                    InitializeChildViews(c);
            }
        }

        private bool IsInitialized(IView view)
        {
            if (view.ViewName == null)
                return false;
            return views[view.ViewName] != null;
        }

        #region Documentation
        /// <summary>
        /// Returns the default MVCConfiguration instance (obtained via
        /// <see cref="MVCConfiguration.GetDefault">MVCConfiguration.GetDefault</see>) with
        /// <see cref="MVCConfiguration.ViewInfosProviderType"/> set to
        /// <see cref="WinformsViewInfosProvider"/> and
        /// <see cref="MVCConfiguration.ViewsManagerType"/> set to <see cref="WinformsViewsManager"/>.
        /// </summary>
        #endregion
        public static MVCConfiguration GetDefaultConfig()
        {
            MVCConfiguration defaultConf = MVCConfiguration.GetDefault();
            defaultConf.ViewsAssembly = Assembly.GetCallingAssembly();
            defaultConf.ViewInfosProviderType = typeof(WinformsViewInfosProvider);
            defaultConf.ViewsManagerType = typeof(WinformsViewsManager);
            return defaultConf;
        }
    }
}

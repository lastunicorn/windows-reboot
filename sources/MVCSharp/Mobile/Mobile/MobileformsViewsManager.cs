//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using MVCSharp.Core.Tasks;
using MVCSharp.Mobile.Configuration;
using MVCSharp.Core.Configuration;
using MVCSharp.Core.Configuration.Views;
using MVCSharp.Core.Views;

namespace MVCSharp.Mobile
{
    public class MobileformsViewsManager:ViewsManagerBase
    {
        private Dictionary<string, IView> m_Views = new Dictionary<string, IView>();
        private IView prevActiveView;
        #region Documentation
        /// <summary>
        /// A flag indicating that the last view activation has been done via code.
        /// </summary>
        #endregion
        protected bool viewActivatedInCode = false;

        private Control activeView;

        public static Form LastActivatedForm { get; private set; }

        public Dictionary<string, IView> Views
        {
            get { return m_Views; }
            set { m_Views = value; }
        }

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
        
        /// <summary>
        /// Creates an instance of the class.
        /// </summary>
        public MobileformsViewsManager()
        {
            
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

        private void NotifyViewsOnActivation(IView activatedView)
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
            if (form != null)
            {
                MobileformsViewsManager.LastActivatedForm = form;
                MobileformsViewInfo viewInf = ViewInfos[view.ViewName] as MobileformsViewInfo;
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
            uc.Focus();
            Form form = FindForm(uc);
            if(form != null)
            {
                form.Show();
                form.Activate();
            }
            viewActivatedInCode = false;
        }

        protected Form FindForm(Control control)
        {
            if(control == null)
            {
                return null;
            }
            
            if(control is Form)
            {
                return control as Form;
            }

            return FindForm(control.Parent);
        }

        #region Documentation
        /// <summary>
        /// Method to find a view by its name or create a new one
        /// if nothing was found.
        /// </summary>
        #endregion
        protected IView FindOrCreateView(string viewName)
        {
            IView result = null;
            if(Views.ContainsKey(viewName))
            {
                result = Views[viewName];
            }
            else
            {
                MobileformsViewInfo viewInf = ViewInfos[viewName] as MobileformsViewInfo;
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

        #region Documentation
        /// <summary>
        /// Method to initialize a view if it happens to be a user control.
        /// Can be overriden in subclasses.
        /// </summary>
        #endregion 
        protected virtual void InitializeUserControlView(UserControl userControlView)
        {
            InitializeView(userControlView as IView);
            userControlView.GotFocus += new EventHandler(View_ActivatedManually);
            userControlView.LostFocus += new EventHandler(View_Deactivate);
            userControlView.Disposed += new EventHandler(MobileformsView_Disposed);
            NotifyInitialize(userControlView as IView);
            InitializeChildViews(userControlView);
        }

        #region Documentation
        /// <summary>
        /// Method to initialize a view if it happens to be a form.
        /// Can be overriden in subclasses.
        /// </summary>
        #endregion
        protected virtual void InitializeFormView(Form form, MobileformsViewInfo viewInf)
        {
            InitializeView(form as IView);
            form.Activated += new EventHandler(View_ActivatedManually);
            form.Deactivate += new EventHandler(View_Deactivate);
            form.Disposed += new EventHandler(MobileformsView_Disposed);
            
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
            Views[view.ViewName] = view;
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
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            if ((activeView == null) ||
                (activeView == Views[Navigator.Task.CurrViewName]))
                return;
            Navigator.TryNavigateToView((activeView as IView).ViewName);
        }

        private Control FindContainerView(Control currView)
        {
            Control result = GetContainerControl(currView);
            while ((result != null) && !(result is IView))
                result = GetContainerControl(result);
            return result;
        }

        private Control GetContainerControl(Control view)
        {
            return view.Parent;
        }

        void MobileformsView_Disposed(object sender, EventArgs e)
        {
            if (!(sender is IView)) return;
            Views.Remove((sender as IView).ViewName);
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
            return Views.ContainsKey(view.ViewName) && Views[view.ViewName] != null;
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
            defaultConf.ViewInfosProviderType = typeof(MobileformsViewInfosProvider);
            defaultConf.ViewsManagerType = typeof(MobileformsViewsManager);
            return defaultConf;
        }
    }
}

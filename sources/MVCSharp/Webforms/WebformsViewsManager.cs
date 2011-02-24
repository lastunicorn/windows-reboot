//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Text;
using System.Web;
using System.Reflection;
using System.Configuration;
using System.Web.Configuration;
using System.Web.UI;
using MVCSharp.Core.Views;
using MVCSharp.Webforms.Configuration;
using MVCSharp.Core;
using MVCSharp.Core.Configuration;
using MVCSharp.Core.Tasks;

namespace MVCSharp.Webforms
{
    #region Documentation
    /// <summary>
    /// Works with views represented as web pages.
    /// </summary>
    /// <remarks>
    /// <see cref="MVCSharp.Core.Configuration.Views.ViewInfo"/> objects for this views manager should be of
    /// <see cref="WebformsViewInfo"/> type. That is why the WinformsViewsManager
    /// is often used in combination with <see cref="MVCConfiguration.ViewInfosProviderType"/>
    /// set to <see cref="WebformsViewInfosProvider"/>.
    /// </remarks>
    #endregion
    public class WebformsViewsManager : ViewsManagerBase
    {
        private const string currTaskKey = "currTask";

        static WebformsViewsManager()
        {
            RegisterViewRequestHandler();
        }

        private static void RegisterViewRequestHandler()
        {
            System.Configuration.Configuration configuration = WebConfigurationManager.OpenWebConfiguration(
                                                                HttpContext.Current.Request.ApplicationPath);
            SystemWebSectionGroup systemWeb = (SystemWebSectionGroup)configuration.GetSectionGroup("system.web");
            if (viewRequestHandlerRegisteredAlready(systemWeb)) return;

            systemWeb.HttpModules.Modules.Add(new HttpModuleAction("WebformsViewRequestHandler",
                                                    typeof(WebformsViewRequestHandler).FullName));
            configuration.Save();
        }

        private static bool viewRequestHandlerRegisteredAlready(SystemWebSectionGroup systemWeb)
        {
            foreach (HttpModuleAction httpMod in systemWeb.HttpModules.Modules)
                if (httpMod.Name == "WebformsViewRequestHandler")
                    return true;
            return false;
        }

        #region Documentation
        /// <summary>
        /// This http module handles page requests and links pages (which implement
        /// IView interface) to controllers. It also fulfils navigation to the requested
        /// view if neccessary.
        /// </summary>
        #endregion
        public class WebformsViewRequestHandler : IHttpModule
        {
            #region Documentation
            /// <summary>
            /// IHttpModule.Init implementation.
            /// </summary>
            #endregion
            public void Init(HttpApplication context)
            {
                context.PostAcquireRequestState += new EventHandler(App_PostAcquireRequestState);
            }

            void App_PostAcquireRequestState(object sender, EventArgs e)
            {
                HttpContext httpContext = HttpContext.Current;
                Page handlerPage = httpContext.Handler as Page;
                if (handlerPage == null) return;

                IView view = handlerPage as IView;
                if (view == null) return;
                handlerPage.Init += new EventHandler(WebformsView_Init);
            }

            void WebformsView_Init(object sender, EventArgs e)
            {
                IView view = sender as IView;
                (view as Page).Init -= new EventHandler(WebformsView_Init);
                HttpContext httpContext = HttpContext.Current;
                ITask currTask = httpContext.Session[currTaskKey] as ITask;
                if (currTask == null) return;
                WebformsViewInfo requestedViewInf = (currTask.Navigator.ViewsManager.ViewInfos
                    as WebformsViewInfoCollection).GetByUrl(httpContext.Request.AppRelativeCurrentExecutionFilePath);
                currTask.Navigator.TryNavigateToView(requestedViewInf.ViewName);
                IController requestedController = currTask.Navigator.GetController(requestedViewInf.ViewName);
                view.ViewName = requestedViewInf.ViewName;
                view.Controller = requestedController;
                if (view.Controller != null)
                    view.Controller.View = view;
            }

            #region Documentation
            /// <summary>
            /// IHttpModule.Dispose implementation.
            /// </summary>
            #endregion
            public void Dispose() { }
        }

        #region Documentation
        /// <summary>
        /// IViewsManager.ActivateView implementation.
        /// </summary>
        #endregion
        public override void ActivateView(string viewName)
        {
            HttpContext httpContext = HttpContext.Current;
            WebformsViewInfo currViewInf = ViewInfos.GetByUrl(httpContext.Request.
                                              AppRelativeCurrentExecutionFilePath);
            if ((currViewInf != null) && (currViewInf.ViewName == viewName)) return;
            httpContext.Session[currTaskKey] = Navigator.Task;
            httpContext.Response.Redirect(ViewInfos[viewName].ViewUrl, true);
        }

        new private WebformsViewInfoCollection ViewInfos
        {
            get { return base.ViewInfos as WebformsViewInfoCollection; }
        }

        #region Documentation
        /// <summary>
        /// IViewsManager.Navigator implementation.
        /// </summary>
        #endregion
        public override Navigator Navigator
        {
            get { return base.Navigator; }
            set
            {
                base.Navigator = value;
                HttpContext.Current.Session[currTaskKey] = Navigator.Task;
            }
        }

        #region Documentation
        /// <summary>
        /// IViewsManager.GetView implementation.
        /// </summary>
        #endregion
        public override IView GetView(string viewName)
        {
            return Navigator.GetController(viewName).View;
        }

        #region Documentation
        /// <summary>
        /// Returns the default MVCConfiguration instance (obtained via
        /// <see cref="MVCConfiguration.GetDefault">MVCConfiguration.GetDefault</see>) with
        /// <see cref="MVCConfiguration.ViewInfosProviderType"/> set to
        /// <see cref="WebformsViewInfosProvider"/> and
        /// <see cref="MVCConfiguration.ViewsManagerType"/> set to <see cref="WebformsViewsManager"/>.
        /// </summary>
        #endregion
        public static MVCConfiguration GetDefaultConfig()
        {
            MVCConfiguration defaultConf = MVCConfiguration.GetDefault();
            defaultConf.ViewsAssembly = Assembly.GetCallingAssembly();
            defaultConf.ViewInfosProviderType = typeof(WebformsViewInfosProvider);
            defaultConf.ViewsManagerType = typeof(WebformsViewsManager);
            return defaultConf;
        }
    }
}

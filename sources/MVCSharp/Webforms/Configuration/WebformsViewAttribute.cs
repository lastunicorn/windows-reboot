//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Text;
using MVCSharp.Core.Configuration.Views;

namespace MVCSharp.Webforms.Configuration
{
    #region Documentation
    /// <summary>
    /// Attribute for declaring views for the web forms presentation
    /// mechanism.
    /// </summary>
    /// <remarks>It does not matter what type this attribute is applied
    /// to. All neccessary information is contained inside the attribute
    /// declarations.</remarks>
    /// <example>
    /// Below we declare two web views:
    /// <code>
    /// [WebformsView(typeof(MyTask), "View 1", "Default.aspx")]
    /// [WebformsView(typeof(MyTask), "View 2", "Views/View2.aspx")]
    /// class MyViews {}
    /// </code>
    /// </example>
    #endregion
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class WebformsViewAttribute : ViewAttribute
    {
        private string viewUrl;

        #region Documentation
        /// <summary>
        /// Constructor taking the task type, view name and page Url as parameters.
        /// </summary>
        #endregion
        public WebformsViewAttribute(Type taskType, string viewName, string viewUrl)
            : base(taskType, viewName)
        {
            ViewUrl = viewUrl;
        }

        #region Documentation
        /// <summary>
        /// Parameterless constructor.
        /// </summary>
        #endregion
        public WebformsViewAttribute()
        { }

        #region Documentation
        /// <summary>
        /// Specifies the url of the page representing the view.
        /// </summary>
        #endregion
        public string ViewUrl
        {
            get { return viewUrl; }
            set { viewUrl = value; }
        }
    }
}

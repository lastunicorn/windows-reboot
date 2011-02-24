//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace MVCSharp.Core.Configuration.Views
{
    #region Documentation
    /// <summary>
    /// Represents an enumerable collection of <see cref="ViewInfo"/> objects
    /// with an indexer to retrieve an element by the view name.
    /// </summary>
    /// <remarks>
    /// Before a task is started the ViewInfoCollection object is stored
    /// within the <see cref="MVCSharp.Core.Configuration.Tasks.TaskInfo.ViewInfos">TaskInfo.ViewInfos</see> property.
    /// After a task is started the corresponding <see cref="MVCSharp.Core.Views.IViewsManager"/> is linked
    /// to this ViewInfoCollection object via its <see cref="MVCSharp.Core.Views.IViewsManager.ViewInfos">
    /// IViewsManager.ViewInfos</see> property.</remarks>
    /// <seealso cref="MVCSharp.Core.Views.IViewsManager.ViewInfos"/>
    /// <seealso cref="MVCConfiguration.ViewInfosByTask"/>
    #endregion
    public class ViewInfoCollection : IEnumerable
    {
        private Dictionary<string, ViewInfo> viewInfoCollection = new Dictionary<string, ViewInfo>();

        #region Documentation
        /// <summary>
        /// Indexer to retrieve ViewInfo object by the view name.
        /// </summary>
        #endregion
        public ViewInfo this[string viewName]
        {
            get
            {
                ViewInfo result;
                viewInfoCollection.TryGetValue(viewName, out result);
                return result;
            }
            set { viewInfoCollection[viewName] = value; }
        }

        #region Documentation
        /// <summary>
        /// Indexer to retrieve ViewInfo object by the view type.
        /// </summary>
        #endregion
        public ViewInfo this[Type viewType]
        {
            get
            {
                foreach (ViewInfo vi in viewInfoCollection.Values)
                    if (vi.ViewType == viewType)
                        return vi;
                return null;
            }
        }

        #region Documentation
        /// <summary>
        /// Adds ViewInfo object to the collection.
        /// </summary>
        #endregion
        public virtual void Add(ViewInfo viewInf)
        {
            this[viewInf.ViewName] = viewInf;
        }

        #region Documentation
        /// <summary>
        /// Removes the view description with given view name from the collection.
        /// </summary>
        #endregion
        public void Remove(string viewName)
        {
            viewInfoCollection.Remove(viewName);
        }

        #region Documentation
        /// <summary>
        /// Adds all items from another collection to this one.
        /// </summary>
        #endregion
        public void Add(ViewInfoCollection viewInfColl)
        {
            foreach (ViewInfo vi in viewInfColl)
                Add(vi);
        }

        #region Documentation
        /// <summary>
        /// Returns the number of <c>ViewInfo</c> objects
        /// in the collection.
        /// </summary>
        #endregion
        public int Count
        {
            get { return viewInfoCollection.Count; }
        }

        #region Documentation
        /// <summary>
        /// Implementation of <see cref="IEnumerable.GetEnumerator"/>
        /// method. Returns an enumerator through the collection of
        /// <c>ViewInfo</c> objects.
        /// </summary>
        #endregion
        public IEnumerator GetEnumerator()
        {
            return viewInfoCollection.Values.GetEnumerator();
        }
    }
}

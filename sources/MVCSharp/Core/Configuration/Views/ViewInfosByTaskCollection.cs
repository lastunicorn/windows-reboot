//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System.Collections;
using System.Collections.Generic;
using System;

namespace MVCSharp.Core.Configuration.Views
{
    #region Documentation
    /// <summary>
    /// Represents an enumerable set of <see cref="ViewInfoCollection"/> objects for each
    /// task type. Thus includes descriptions for all views in the
    /// application.
    /// </summary>
    /// <remarks>This collection is a part of the <see cref="MVCConfiguration"/>
    /// object: when a task is started the necessary <c>ViewInfoCollection</c> object is
    /// obtained from the <see cref="MVCConfiguration.ViewInfosByTask"/> property.
    /// </remarks>
    #endregion
    public class ViewInfosByTaskCollection : IEnumerable
    {
        #region Documentation
        /// <summary>
        /// Dictionary storing ViewInfoCollection objects with task types as keys.
        /// </summary>
        #endregion
        protected Dictionary<Type, ViewInfoCollection> viewInfosByTask = new Dictionary<Type, ViewInfoCollection>();

        #region Documentation
        /// <summary>
        /// Indexer to get or set ViewInfoCollection instance for a given task type.
        /// </summary>
        #endregion
        public ViewInfoCollection this[Type taskType]
        {
            get
            {
                ViewInfoCollection result;
                viewInfosByTask.TryGetValue(taskType, out result);
                return result;
            }
            set
            {
                viewInfosByTask[taskType] = value;
            }
        }

        #region Documentation
        /// <summary>
        /// Gets the number of <c>ViewInfoCollection</c> objects
        /// present in this collection.
        /// </summary>
        #endregion
        public int Count
        {
            get { return viewInfosByTask.Count; }
        }

        #region Documentation
        /// <summary>
        /// Adds all items from another <c>ViewInfosByTaskCollection</c>
        /// collection to this one.
        /// </summary>
        #endregion
        public void Add(ViewInfosByTaskCollection viewInfBTColl)
        {
            foreach (Type t in viewInfBTColl)
                if (this[t] == null)
                    this[t] = viewInfBTColl[t];
                else
                    this[t].Add(viewInfBTColl[t]);
        }

        #region Documentation
        /// <summary>
        /// Implementation of <see cref="IEnumerable.GetEnumerator"/> method.
        /// Returns an enumerator to walk through the task types included
        /// in this collection.
        /// </summary>
        #endregion
        public IEnumerator GetEnumerator()
        {
            return viewInfosByTask.Keys.GetEnumerator();
        }
    }
}

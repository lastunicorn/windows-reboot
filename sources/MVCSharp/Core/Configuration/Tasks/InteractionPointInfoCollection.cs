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

namespace MVCSharp.Core.Configuration.Tasks
{
    #region Documentation
    /// <summary>
    /// Represents a collection of <see cref="InteractionPointInfo"/>
    /// objects accessible by the view name.
    /// </summary>
    /// <remarks>
    /// Such collection is a part of the <see cref="TaskInfo"/> object
    /// descibing a task (see <see cref="TaskInfo.InteractionPoints">
    /// TaskInfo.InteractionPoints</see>).</remarks>
    #endregion
    public class InteractionPointInfoCollection : IEnumerable
    {
        private Dictionary<string, InteractionPointInfo> interactionPointInfos = new Dictionary<string, InteractionPointInfo>();

        #region Documentation
        /// <summary>
        /// Gets the number of interation points within the collection.
        /// </summary>
        #endregion
        public int Count
        {
            get { return interactionPointInfos.Count; }
        }

        #region Documentation
        /// <summary>
        /// Removes the interaction with given view name from the collection.
        /// </summary>
        #endregion
        public void Remove(string viewName)
        {
            interactionPointInfos.Remove(viewName);
        }

        #region Documentation
        /// <summary>
        /// Accesses the description of an interaction point by the view name.
        /// </summary>
        #endregion
        public InteractionPointInfo this[string viewName]
        {
            get
            {
                InteractionPointInfo result;
                interactionPointInfos.TryGetValue(viewName, out result);
                return result;
            }
            set { interactionPointInfos[viewName] = value; }
        }

        #region Documentation
        /// <summary>
        /// Allows to enumerate through all interaction points within the collection.
        /// </summary>
        #endregion
        public IEnumerator GetEnumerator()
        {
            return interactionPointInfos.Values.GetEnumerator();
        }
    }
}

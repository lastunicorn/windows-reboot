using System;
using System.Collections.Generic;
using System.Text;
using MVCSharp.Core.Tasks;

namespace MVCSharp.Core
{
    #region Documentation
    /// <summary>
    /// Navigator subclass which has no real functionality except for
    /// modifying the task state. The views manager is not used at all, and no
    /// actual view switching is done. This class is intended primarily for 
    /// tests. </summary>
    #endregion
    public class StubNavigator : Navigator
    {
        #region Documentation
        /// <summary>
        /// Only alters the task state by making its current view
        /// (<see cref="ITask.CurrViewName"/>) equal to the passed
        /// <c>viewName</c>.
        /// </summary>
        #endregion
        public override void Navigate(string triggerName)
        {
            ActivateView(triggerName);
        }

        #region Documentation
        /// <summary>
        /// Only alters the task state by making its current view
        /// (<see cref="ITask.CurrViewName"/>) equal to the passed
        /// <c>viewName</c>.
        /// </summary>
        #endregion
        public override void NavigateDirectly(string viewName)
        {
            ActivateView(viewName);
        }

        #region Documentation
        /// <summary>
        /// Only alters the task state by making its current view
        /// (<see cref="ITask.CurrViewName"/>) equal to the passed
        /// <c>viewName</c>.
        /// </summary>
        #endregion
        public override void TryNavigateToView(string viewName)
        {
            ActivateView(viewName);
        }

        #region Documentation
        /// <summary>
        /// Only alters the task state by making its current view
        /// (<see cref="ITask.CurrViewName"/>) equal to the passed
        /// <c>viewName</c>.
        /// </summary>
        #endregion
        public override void ActivateView(string viewName)
        {
            Task.CurrViewName = viewName;
        }
    }
}

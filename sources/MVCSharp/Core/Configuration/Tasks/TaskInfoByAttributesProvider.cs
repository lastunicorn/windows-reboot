//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace MVCSharp.Core.Configuration.Tasks
{
    #region Documentation
    /// <summary>
    /// Implementation of <see cref="ITaskInfoProvider"/> interface.
    /// Extracts task information from <see cref="InteractionPointAttribute"/>,
    /// <see cref="NavTargetAttribute"/> and <see cref="AdjacentPointsAttribute"/>
    /// attributes applied to the task type and its members.
    /// </summary>
    #endregion
    public class TaskInfoByAttributesProvider : TaskInfoProviderBase
    {
        #region Documentation
        /// <summary>
        /// Extracts task information from <see cref="InteractionPointAttribute"/>,
        /// <see cref="NavTargetAttribute"/>, <see cref="AdjacentPointsAttribute"/>
        /// attributes applied to the task type and its members.
        /// </summary>
        #endregion
        public override TaskInfo GetTaskInfo(Type taskType)
        {
            TaskInfo result = new TaskInfo();

            TaskAttribute ta = GetTaskAttribute(taskType);
            if (ta != null)
                result.NavigatorType = ta.NavigatorType;

            IList iPointInfWrappers = CreateIPointInfos(taskType, result.InteractionPoints);
            FillNavTargets(taskType, iPointInfWrappers, result.InteractionPoints);

            return result;
        }

        private IList CreateIPointInfos(Type taskType, InteractionPointInfoCollection iPointInfos)
        {
            FieldInfo[] fields = taskType.GetFields(BindingFlags.Public | BindingFlags.Static);
            List<InteractionPointInfoWrapper> iPointInfWrappers = new List<InteractionPointInfoWrapper>();
            foreach (FieldInfo field in fields)
                if (field.IsLiteral && field.FieldType == typeof(string))
                {
                    InteractionPointAttribute[] ipAttributes = field.GetCustomAttributes(
                        typeof(InteractionPointAttribute), false) as InteractionPointAttribute[];
                    if (ipAttributes.Length > 0)
                    {
                        string viewName = field.GetValue(null) as string;
                        NavTargetAttribute[] navTargAttributes = field.GetCustomAttributes(
                                typeof(NavTargetAttribute), false) as NavTargetAttribute[];
                        InteractionPointInfo iPointInf = CreateInteractionPointInfo(
                                                            viewName, ipAttributes[0]);
                        iPointInfos[viewName] = iPointInf;
                        iPointInfWrappers.Add(new InteractionPointInfoWrapper(iPointInf,
                                                  ipAttributes[0], navTargAttributes));
                    }
                }
            return iPointInfWrappers;
        }

        #region Documentation
        /// <summary>
        /// Extracts task information from the specific <see cref="InteractionPointAttribute"/>
        /// attribute instance. Can be overriden in subclasses to support custom
        /// interaction point attributes for example.
        /// </summary>
        #endregion
        protected virtual InteractionPointInfo CreateInteractionPointInfo(string viewName,
                                            InteractionPointAttribute ipAttribute)
        {
            InteractionPointInfo result = new InteractionPointInfo();
            result.ViewName = viewName;
            result.ControllerType = ipAttribute.ControllerType;
            result.IsCommonTarget = ipAttribute.IsCommonTarget;
            return result;
        }

        private void FillNavTargets(Type taskType, IList iPointInfWrappers,
                                    InteractionPointInfoCollection ipInfos)
        {
            AdjacentPointsAttribute[] adjPointAttributes = taskType.GetCustomAttributes(
                      typeof(AdjacentPointsAttribute), false) as AdjacentPointsAttribute[];
            foreach (InteractionPointInfoWrapper ipWrapper in iPointInfWrappers)
            {
                InteractionPointInfo iPointInf = ipWrapper.iPointInf;

                foreach (string targetName in ipWrapper.ipInfAttribute.NavTargets)
                    iPointInf.NavTargets[targetName] = ipInfos[targetName];
                foreach (NavTargetAttribute trgAttr in ipWrapper.navTargAttributes)
                    iPointInf.NavTargets[trgAttr.TriggerName] = ipInfos[trgAttr.ViewName];
                foreach (AdjacentPointsAttribute containingAdjPointAttr in
                         GetContainingAdjPointAttrs(iPointInf.ViewName, adjPointAttributes))
                    foreach (string adjPointName in containingAdjPointAttr.AdjacentViewNames)
                        if (adjPointName != iPointInf.ViewName)
                            iPointInf.NavTargets[adjPointName] = ipInfos[adjPointName];
            }
        }

        private IList GetContainingAdjPointAttrs(string iPointName,
                        AdjacentPointsAttribute[] adjPointAttributes)
        {
            List<AdjacentPointsAttribute> result = new List<AdjacentPointsAttribute>();
            foreach (AdjacentPointsAttribute adjPointAttr in adjPointAttributes)
                if ((adjPointAttr.AdjacentViewNames as IList).Contains(iPointName))
                    result.Add(adjPointAttr);
            return result;
        }

        private class InteractionPointInfoWrapper
        {
            public readonly InteractionPointInfo iPointInf;
            public readonly InteractionPointAttribute ipInfAttribute;
            public readonly NavTargetAttribute[] navTargAttributes;

            public InteractionPointInfoWrapper(InteractionPointInfo iPointInf,
                   InteractionPointAttribute ipInfAttribute, NavTargetAttribute[] navTargAttributes)
            {
                this.iPointInf = iPointInf;
                this.ipInfAttribute = ipInfAttribute;
                this.navTargAttributes = navTargAttributes;
            }
        }
    }
}

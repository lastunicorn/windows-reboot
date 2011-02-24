//===========================================
// MVC# Framework | www.MVCSharp.org        |
// ------------------------------------------
// Copyright (C) 2008 www.MVCSharp.org      |
// All rights reserved.                     |
//===========================================

using System;
using System.Text;
using System.Xml;
using System.Reflection;

namespace MVCSharp.Core.Configuration.Tasks
{
    #region Documentation
    /// <summary>
    /// Implementation of the <see cref="ITaskInfoProvider"/>
    /// interface. Generates task information from the <see cref="TaskAttribute"/>
    /// attribute applied to a task type.
    /// </summary>
    /// <seealso cref="ITaskInfoProvider"/>
    /// <seealso cref="TaskAttribute"/>
    #endregion
    public class TaskInfoByXmlAttributeProvider : TaskInfoProviderBase
    {
        #region Documentation
        /// <summary>
        /// Generates task information from the <see cref="TaskAttribute"/>
        /// attribute applied to a task type.
        /// </summary>
        /// <seealso cref="ITaskInfoProvider"/>
        /// <seealso cref="TaskAttribute"/>
        #endregion
        public override TaskInfo GetTaskInfo(Type taskType)
        {
            TaskInfo result = new TaskInfo();
            
            TaskAttribute ta = GetTaskAttribute(taskType);
            if (ta != null)
                result.NavigatorType = ta.NavigatorType;

            InteractionPointInfoWrapper[] iPointInfoWrappers = CreateInteractionPointInfos(
                                                        taskType, result.InteractionPoints);

            foreach (InteractionPointInfoWrapper iPointWrapper in iPointInfoWrappers)
                FillNavTargets(iPointWrapper, result.InteractionPoints);   

            return result;
        }

        private InteractionPointInfoWrapper[] CreateInteractionPointInfos(Type taskType,
                                                InteractionPointInfoCollection iPointInfos)
        {
            Assembly controllersDefaultAssembly = taskType.Assembly;
            XmlNodeList iPntNodes = GetTaskRootElement(taskType).SelectNodes(
                        "interactionPoints/*[self::iPoint or self::interactionPoint]");
            InteractionPointInfoWrapper[] iPointWrappers = new InteractionPointInfoWrapper[iPntNodes.Count];
            for (int i = 0; i < iPntNodes.Count; i++)
            {
                InteractionPointInfo ipInfo = CreateInteractionPointInfo(iPntNodes[i] as XmlElement, controllersDefaultAssembly);
                iPointInfos[ipInfo.ViewName] = ipInfo;
                iPointWrappers[i] = new InteractionPointInfoWrapper(ipInfo, iPntNodes[i] as XmlElement);
            }
            return iPointWrappers;
        }

        private XmlElement GetTaskRootElement(Type taskType)
        {
            TaskAttribute taskAttr = GetTaskAttribute(taskType);
            if (taskAttr == null) return null;

            XmlDocument taskXml = new XmlDocument();
            taskXml.LoadXml("<taskConfiguration></taskConfiguration>");
            taskXml.DocumentElement.InnerXml = taskAttr.Xml;
            return taskXml.DocumentElement;
        }

        private InteractionPointInfo CreateInteractionPointInfo(XmlElement interactionPointElement,
                                                                Assembly controllersDefaultAssembly)
        {
            InteractionPointInfo result = new InteractionPointInfo();
            result.ViewName = interactionPointElement.GetAttribute("view");
            result.IsCommonTarget = interactionPointElement.GetAttribute("isCommonTarget") == "true";
            string controllerTypeName = interactionPointElement.GetAttribute("controllerType");
            if (controllerTypeName.IndexOf(',') < 0)
                controllerTypeName = controllerTypeName + ", " + controllersDefaultAssembly.FullName;
            result.ControllerType = Type.GetType(controllerTypeName);

            return result;
        }

        private void FillNavTargets(InteractionPointInfoWrapper iPointInfWrapper,
                                    InteractionPointInfoCollection iPointInfos)
        {
            foreach (XmlElement navTargetElmt in iPointInfWrapper.iPointElmt.SelectNodes("navTarget"))
            {
                string triggerName = navTargetElmt.GetAttribute("trigger");
                InteractionPointInfo target = iPointInfos[navTargetElmt.GetAttribute("view")];
                if (triggerName == string.Empty)
                    triggerName = target.ViewName;
                iPointInfWrapper.iPointInf.NavTargets[triggerName] = target;
            }
            string quotedViewName = "\"" + iPointInfWrapper.iPointInf.ViewName + "\"";
            XmlNodeList adjacentPointElmts = iPointInfWrapper.iPointElmt.SelectNodes(
                           "/*/adjacentPoints[iPointRef/@view | interactionPointRef/@view = "
                           + quotedViewName + "]/*[self::iPointRef or self::interactionPointRef][@view!="
                           + quotedViewName + "]");
            foreach (XmlElement adjacentPointElmt in adjacentPointElmts)
            {
                string targetViewName = adjacentPointElmt.GetAttribute("view");
                if (   (iPointInfWrapper.iPointInf.NavTargets[targetViewName] != null)
                    && (iPointInfWrapper.iPointInf.NavTargets[targetViewName].ViewName == targetViewName)) continue;
                InteractionPointInfo target = iPointInfos[targetViewName];
                string triggerName = target.ViewName;
                iPointInfWrapper.iPointInf.NavTargets[targetViewName] = target;
            }
        }

        private struct InteractionPointInfoWrapper
        {
            public InteractionPointInfo iPointInf;
            public XmlElement iPointElmt;
            
            public InteractionPointInfoWrapper(InteractionPointInfo iPointInf, XmlElement iPointElmt)
            {
                this.iPointElmt = iPointElmt;
                this.iPointInf = iPointInf;
            }
        }
    }
}

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using MVCSharp.Core.Configuration.Tasks;

namespace MVCSharp.Core.Configuration.Tasks
{
    #region Documentation
    /// <summary>
    /// Stub implementation of the <see cref="ITaskInfoProvider"/>
    /// interface. Should generate task information from the <see cref="TaskAttribute"/>
    /// attribute applied to a task type, but actually does nothing due to
    /// the limited XML parsing capabilities in Silverlight CLR.
    /// </summary>
    /// <seealso cref="ITaskInfoProvider"/>
    /// <seealso cref="TaskInfoByAttributesProvider"/>
    #endregion
    public class TaskInfoByXmlAttributeProvider : ITaskInfoProvider
    {
        #region Documentation
        /// <summary>
        /// Should generate task information from the <see cref="TaskAttribute"/>
        /// attribute applied to a task type, but actually does nothing due to
        /// the limited XML parsing capabilities in Silverlight CLR.
        /// </summary>
        /// <seealso cref="ITaskInfoProvider"/>
        #endregion
        public TaskInfo GetTaskInfo(Type taskType)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DustInTheWind.WinFormsAdditions
{
    public class FormWithBehaviors : Form
    {
        private readonly List<IFormBehaviour> behaviors = new List<IFormBehaviour>();

        public void AddBehavior(IFormBehaviour behaviour)
        {
            if (behaviour == null) throw new ArgumentNullException(nameof(behaviour));

            behaviour.Form = this;
            behaviors.Add(behaviour);
        }
    }
}
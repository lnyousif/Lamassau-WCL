using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lamassau.Web.UI.WebControls
{
    /// <summary>
    /// Summary description for WebControlIDConverter.
    /// </summary>
    internal class TextBoxLabelIDConverter : StringConverter
    {
        public TextBoxLabelIDConverter()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        protected virtual object[] GetControls(IContainer container)
        {
            ArrayList list1 = new ArrayList();
            foreach (IComponent component1 in container.Components)
            {
                Control control1 = component1 as Control;
                if (control1 is TextBox  || control1 is Label)
                {
                    list1.Add(string.Copy(control1.ID));
                }
            }
            list1.Sort();
            return list1.ToArray();
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            if ((context != null) && (context.Container != null))
            {
                object[] objArray1 = this.GetControls(context.Container);
                if (objArray1 != null)
                {
                    return new TypeConverter.StandardValuesCollection(objArray1);
                }
            }
            return null;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

    }
}

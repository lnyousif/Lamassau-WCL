using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Text; 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Security.Permissions;
using System.Collections.Specialized; 
using System.Drawing;
using System.Web.UI.Design;
using System.Web.UI.Design.WebControls; 
using Lamassau.Web.UI.WebControls.Design;
using System.ComponentModel.Design; 
using System.Text.RegularExpressions;
using System.Data;
using Lamassau.Web.UI;


[assembly:TagPrefix("Lamassau.Web.UI.WebControls","lms")]
namespace Lamassau.Web.UI.WebControls
{
	
	/// <summary>
	/// Label: Inherting the Label ASP.NET control;
	/// Designed by Laith N Yousif.
	/// Developed by Laith N Yousif. 
	/// Date: Jan 2006.
	/// </summary>
	[ToolboxData("<{0}:Label runat=server></{0}:Label>")]
    [DesignerAttribute(typeof(LabelDesigner))]
	public class Label: System.Web.UI.WebControls.Label,ILamassauControl 
	{
	
        #region Initialization
		
        private Helper Ctr;
		// Methods
        public Label()
		{
            this.Ctr = new Helper();
		}
		
		#endregion

		#region Properties

        #region IControl Implementation
        [Bindable(true), Category("Behavior"), Description("TextBox View Mode"), DefaultValue(ViewModes.Invisible)]
        public ViewModes ViewMode
        {
            get
            {
                return Ctr.ViewMode;
            }
            set
            {
                Ctr.ViewMode = value;
            }
        }

        [Bindable(true), Category("Behavior"), Description("TextBox Inhertance ViewMode Status"), DefaultValue(ViewModes.Invisible)]
        public InhertedViewModes InhertedViewMode
        {
            get
            {
                return Ctr.InhertedViewMode;
            }
            set
            {
                Ctr.InhertedViewMode = value;
            }
        }


        #endregion		
		

        [Bindable(true), Category("Appearance"), Description("TextBox Inhertance ViewMode Status"), DefaultValue(ViewModes.Invisible)]
        public string StringFormat
        {
            get
            {
                object o = ViewState["StringFormat"];
                return (o != null) ? o.ToString() : "";
            }
            set
            {
                ViewState["StringFormat"] = value;
            }
        }

        [Bindable(true), Category("Appearance"), Description("TextBox Inhertance ViewMode Status"), DefaultValue(ViewModes.Invisible)]
        public ValueTypes ValueType
        {
            get
            {
                object o = ViewState["ValueType"];
                return (o != null) ? (ValueTypes) o : ValueTypes.Custom ;
            }
            set
            {
                ViewState["ValueType"] = value;
            }
        }

        //[Browsable(false)]
        //public object Value
        //{
        //    get
        //    {
        //        return TypeHelper.HackType(this.Text,typeof(Object));
        //    }
        //    set
        //    {
        //        base.Text = value.ToString();
        //    }
        //}

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

		#endregion

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.StringFormat.Trim().Length > 0)
            {
                try
                {

                    if (this.ValueType != ValueTypes.Custom)
                    {
                        Type t = Type.GetType("System." + this.ValueType.ToString());
                        this.Text = String.Format(this.StringFormat, Lamassau.Helper.TypeHelper.HackType(this.Text, t));
                    }
                    else
                    {
                        this.Text = String.Format(this.StringFormat, this.Text);
                    }
                }
                catch
                {
                    // keep the text as it is.
                }
            }
            base.Render(writer);

        }





	}

}

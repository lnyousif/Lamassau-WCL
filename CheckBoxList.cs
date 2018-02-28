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

[assembly:TagPrefix("Lamassau.Web.UI.WebControls","lms")]
namespace Lamassau.Web.UI.WebControls
{
	
	/// <summary>
	/// CheckBoxList: Inherting the CheckBoxList ASP.NET control;
	/// Designed by Laith N Yousif.
	/// Developed by Laith N Yousif. 
	/// Date: Jan 2006.
	/// </summary>
	[ToolboxData("<{0}:CheckBoxList runat=server></{0}:CheckBoxList>"), PermissionSet(SecurityAction.Demand, Unrestricted=true)]
	//[DesignerAttribute(typeof(CheckBoxListControlDesigner))]
	public class CheckBoxList: System.Web.UI.WebControls.CheckBoxList,ILamassauControl 
	{

        #region Initialization

        private Helper Ctr;
		// Methods
        public CheckBoxList()
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


        public string SelectedValues
        {
            get 
            {
                return GetSelectedUtemsValues();
            }
        }

        private string GetSelectedUtemsValues()
        {
            string strSelectedList = string.Empty;
            foreach (ListItem tmpItem in this.Items)
            {
                if (tmpItem.Selected)
                {
                    if (strSelectedList == "")
                    {
                        strSelectedList = tmpItem.Value; 
                    }
                    else
                    {
                        strSelectedList += ("," + tmpItem.Value);
                    }
                }

            }
            return strSelectedList; 
        }
		#endregion
		
		#region Rendering
	
		protected override void Render(HtmlTextWriter writer)
		{

			if (!this.Visible)
				return;

			EnsureChildControls();


            switch (this.ViewMode)
            {
                case ViewModes.ReadOnly:
                    base.Enabled = false;
                    base.Render(writer);
                    break;

                case ViewModes.Editable:
                    base.Render(writer);
                    break;

                default:
                    break;
            }            
		}
	
		#endregion

	}
	

}

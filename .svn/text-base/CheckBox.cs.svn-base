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
	/// CheckBox: Inherting the CheckBox ASP.NET control;
	/// Designed by Laith N Yousif.
	/// Developed by Laith N Yousif. 
	/// Date: Jan 2006.
	/// </summary>
	[ToolboxData("<{0}:CheckBox runat=server></{0}:CheckBox>"), PermissionSet(SecurityAction.Demand, Unrestricted=true)]
//	[DesignerAttribute(typeof(CheckBoxControlDesigner))]
	public class CheckBox: System.Web.UI.WebControls.CheckBox,ILamassauControl 
	{
	
		#region Initialization
		
		private Label lblView ;
        private Helper Ctr;
		// Methods
		public CheckBox()
		{
			this.lblView = new Label();
            this.Ctr = new Helper();
		}
		
		#endregion


        //protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        //{
           
        //    bool CheckedPresentValue = this.Checked;
        //    bool CheckedPostedValue = CheckedPresentValue;
        //    try
        //    {
        //        string strPostedValue = this.Page.Request.Form[postDataKey];
        //        CheckedPostedValue = bool.Parse(strPostedValue);
        //    }
        //    catch
        //    { }

        //    if (!CheckedPresentValue.Equals(CheckedPostedValue))
        //    {
        //        this.Checked = CheckedPostedValue;
        //        return true;
        //    }

        //    return false;

        //}






 


        #region IControl Implementation
        [Bindable(true), Category("Behavior"), Description("CheckBox View Mode"), DefaultValue(ViewModes.Invisible)]
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

        [Bindable(true), Category("Behavior"), Description("CheckBox Inhertance ViewMode Status"), DefaultValue(ViewModes.Invisible)]
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

		#region PreRendering

		protected override void CreateChildControls()
		{
			base.CreateChildControls ();
			
			//this.lblView.CopyBaseAttributes(this); 
			this.lblView.CopyBaseAttributes(this);
			this.lblView.ForeColor = this.ForeColor;
			this.lblView.BackColor = this.BackColor ;
            //this.lblView.Font.Size = FontUnit.XSmall;
            //this.lblView.Font.Name = "Webdings";
            //this.lblView.Font.Bold = true;  
		}
		#endregion
		
		#region Rendering
	
		protected override void Render(HtmlTextWriter writer)
		{

			if (!this.Visible)
				return;

			EnsureChildControls();


            // yes = "a"  No="r"
			if (this.Checked)
			{
                this.lblView.Text = this.Text +  " Yes";
                      	
			}
			else
			{
                this.lblView.Text =this.Text +  " No";
			} 
			this.lblView.Width = this.Width;
			this.lblView.Height = this.Height;
			  
			this.lblView.Enabled = this.Enabled;

            switch (this.ViewMode)
            {
                case ViewModes.ReadOnly:
                    //base.Enabled = false;
                    base.Style["display"] = "none";
                    base.Render(writer);
                    lblView.RenderControl(writer);
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
	
//	#region Design Time Support
//	namespace Design 
//	{
//		public class CheckBoxControlDesigner  : CheckBoxDesigner 
//		{
//	      
//			public override string GetDesignTimeHtml() 
//			{
//				// Component is the instance of the component or control that
//				// this designer object is associated with. This property is 
//				// inherited from System.ComponentModel.ComponentDesigner.
//				string tmpS=string.Empty ;
//				try
//				{
//				
//					CheckBox DesignCheckBox = (CheckBox) Component;
//
//					// Capture the default output of the CheckBox control
//					StringWriter writer = new StringWriter();
//					HtmlTextWriter buffer = new HtmlTextWriter(writer);
//					DesignCheckBox.RenderControl(buffer);			
//					tmpS = writer.ToString();
//
//				}		
//				catch( Exception ex ) 
//				{
//					tmpS = this.GetErrorDesignTimeHtml( ex );
//				}			
//				return tmpS;
//				
//			} 
//
//			protected override string GetErrorDesignTimeHtml(Exception e)
//			{
//				return this.CreatePlaceHolderDesignTimeHtml( e.Message  );
//			}
//			
//		}
//	}
//	#endregion
}

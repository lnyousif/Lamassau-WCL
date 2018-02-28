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
	/// LinkButton: Inherting the LinkButton ASP.NET control;
	/// Designed by Laith N Yousif.
	/// Developed by Laith N Yousif. 
	/// Date: Jan 2006.
	/// </summary>
	[ToolboxData("<{0}:LinkButton runat=server></{0}:LinkButton>"), PermissionSet(SecurityAction.Demand, Unrestricted=true)]
	[DesignerAttribute(typeof(LinkButtonControlDesigner))]
	public class LinkButton: System.Web.UI.WebControls.LinkButton
	{
	
	    #region Initialization
		
		private Label lblView ;
        private Helper Ctr;
		// Methods
        public LinkButton()
		{
			this.lblView = new Label();
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
		

		#endregion

		#region PreRendering

		protected override void CreateChildControls()
		{
			base.CreateChildControls ();
			
			this.lblView.CopyBaseAttributes(this); 
			this.lblView.ForeColor = this.ForeColor;
			this.lblView.BackColor = this.BackColor ;
			  
		}
		#endregion
		
		#region Rendering
	
		protected override void Render(HtmlTextWriter writer)
		{

			if (!this.Visible)
				return;

			EnsureChildControls();
			
			this.lblView.Text = this.Text;
			this.lblView.Width = this.Width;
			this.lblView.Height = this.Height;
			  
			this.lblView.Enabled = this.Enabled;

            switch (this.ViewMode)
            {
                case ViewModes.ReadOnly:
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
	
	#region Design Time Support
	namespace Design 
	{
		public class LinkButtonControlDesigner  :LinkButtonDesigner  
		{
	      
//			public override string GetDesignTimeHtml() 
//			{
//				// Component is the instance of the component or control that
//				// this designer object is associated with. This property is 
//				// inherited from System.ComponentModel.ComponentDesigner.
//				string tmpS=string.Empty ;
//				try
//				{
//				
//					LinkButton DesignLinkButton = (LinkButton) Component;
//
//					// Capture the default output of the LinkButton control
//					StringWriter writer = new StringWriter();
//					HtmlTextWriter buffer = new HtmlTextWriter(writer);
//					DesignLinkButton.RenderControl(buffer);			
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
			
		}
	}
	#endregion
}

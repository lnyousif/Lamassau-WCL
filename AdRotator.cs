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
	/// AdRotator: Inherting the AdRotator ASP.NET control;
	/// Designed by Laith N Yousif.
	/// Developed by Laith N Yousif. 
	/// Date: Jan 2006.
	/// </summary>
	[ToolboxData("<{0}:AdRotator runat=server></{0}:AdRotator>"), PermissionSet(SecurityAction.Demand, Unrestricted=true)]
	[DesignerAttribute(typeof(AdRotatorControlDesigner))]
	public class AdRotator: System.Web.UI.WebControls.AdRotator,ILamassauControl 
	{
	
       #region Initialization

        private Helper Ctr;
		// Methods
        public AdRotator()
		{

            this.Ctr = new Helper();
		}
		
		#endregion


		#region Properties

		//private bool DesignMode = (HttpContext.Current == null);

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

		

	}
	
	#region Design Time Support
	namespace Design 
	{
		public class AdRotatorControlDesigner  : AdRotatorDesigner  
		{
	      
			public override string GetDesignTimeHtml() 
			{
				// Component is the instance of the component or control that
				// this designer object is associated with. This property is 
				// inherited from System.ComponentModel.ComponentDesigner.
				string tmpS=string.Empty ;
				try
				{
				
					AdRotator DesignAdRotator = (AdRotator) Component;

					// Capture the default output of the AdRotator control
					StringWriter writer = new StringWriter();
					HtmlTextWriter buffer = new HtmlTextWriter(writer);
					DesignAdRotator.RenderControl(buffer);			
					tmpS = writer.ToString();

				}		
				catch( Exception ex ) 
				{
					tmpS = this.GetErrorDesignTimeHtml( ex );
				}			
				return tmpS;
				
			} 

			protected override string GetErrorDesignTimeHtml(Exception e)
			{
				return this.CreatePlaceHolderDesignTimeHtml( e.Message  );
			}
			
		}
	}
	#endregion
}

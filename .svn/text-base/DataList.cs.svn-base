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
	/// DataList: Inherting the DataList ASP.NET control;
	/// Designed by Laith N Yousif.
	/// Developed by Laith N Yousif. 
	/// Date: Jan 2006.
	/// </summary>
	[ToolboxData("<{0}:DataList runat=server></{0}:DataList>"), PermissionSet(SecurityAction.Demand, Unrestricted=true)]
	[DesignerAttribute(typeof(DataListControlDesigner))]
	public class DataList: System.Web.UI.WebControls.DataList,ILamassauControl 
	{
	

        #region Initialization

        private Helper Ctr;
		// Methods
        public DataList()
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
		

		#endregion
	

	}
	
	#region Design Time Support
	namespace Design 
	{
		public class DataListControlDesigner  : DataListDesigner 
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
//					DataList DesignDataList = (DataList) Component;
//
//					// Capture the default output of the DataList control
//					StringWriter writer = new StringWriter();
//					HtmlTextWriter buffer = new HtmlTextWriter(writer);
//					DesignDataList.RenderControl(buffer);			
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

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
    /// ClearBox: Inherting the Image  control;
	/// Designed by Laith N Yousif.
	/// Developed by Laith N Yousif. 
	/// Date: Jan 2006.
	/// </summary>
    [ToolboxData("<{0}:ClearBox runat=server></{0}:ClearBox>"), PermissionSet(SecurityAction.Demand, Unrestricted = true)]
    public class ClearBox : Image 
	{
        #region Initialization
		

		#endregion

		#region Properties

        [Description("ControlToClear:  Point to the WebControl to be cleared"), DefaultValue(""), Category("Behavior"), Bindable(true), TypeConverter(typeof(Lamassau.Web.UI.WebControls.WebControlIDConverter))]
        public string ControlToClear
        {
            get
            {
                string s = (string)ViewState["ControlToClear"];
                return (s == null) ? String.Empty : s;
            }
            set
            {
                ViewState["ControlToClear"] = value;
            }
        }
        #endregion


        #region PreRendering

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            base.Style.Add("border", "1px solid Transparent");
            if (!this.Page.ClientScript.IsClientScriptIncludeRegistered("clearJavascript"))
            {
                this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "clearJavascript", this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Lamassau.Web.UI.WebControls.Resources.Scripts.clearbox.js"));
            }

            if (!this.Page.ClientScript.IsClientScriptBlockRegistered("clearMouseOverJavascript"))
            {
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "clearMouseOverJavascript", GetMouseOverScript(),true  );
            }



        }

        private string GetMouseOverScript()
        {
            string strScript = @"

        function clearmouseover(ClearControlID)
        {
            document.getElementById(ClearControlID).src='[clearovergif]';
        }
        function clearmouseout(ClearControlID)
        {
            document.getElementById(ClearControlID).src='[cleargif]';
            
        }";
            strScript = strScript.Replace("[cleargif]", this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Lamassau.Web.UI.WebControls.Resources.Images.clear.gif"));
            strScript = strScript.Replace("[clearovergif]", this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Lamassau.Web.UI.WebControls.Resources.Images.clearover.gif"));
            return strScript;
        }


        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            try
            {
                Control tmpControlToClear = this.Parent.FindControl(this.ControlToClear);
                string controlType = tmpControlToClear.GetType().ToString();
                controlType = controlType.Substring(controlType.LastIndexOf(".") + 1).ToLower();
                base.Attributes.Add("onclick", "javascript:clear" + controlType + "('" + tmpControlToClear.ClientID + "');");
                base.Attributes.Add("onmouseover", "javascript:clearmouseover('" + this.ClientID + "');");
                base.Attributes.Add("onmouseout", "javascript:clearmouseout('" + this.ClientID + "');");
                
            }
            catch { }

        }

        #endregion 


        #region Rendering

        protected override void Render(HtmlTextWriter writer)
        {
            if (!this.Visible)
                return;

            if (this.ViewMode != ViewModes.Editable)
                return;
            if (this.ImageUrl == "")
            {
                this.ImageUrl = this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Lamassau.Web.UI.WebControls.Resources.Images.clear.gif");
            }

            base.Render(writer);
        }



        #endregion

    }
  

}


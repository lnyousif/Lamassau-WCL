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
using System.Drawing.Design;
using Lamassau.Web.UI;
using System.Globalization;

[assembly:TagPrefix("Lamassau.Web.UI.WebControls","lms")]
namespace Lamassau.Web.UI.WebControls
{
	
	/// <summary>
	/// TextBox: Inherting the TextBox ASP.NET control;
	/// Designed by Laith N Yousif.
	/// Developed by Laith N Yousif. 
	/// Date: Jan 2006.
	/// </summary>
	[ToolboxData("<{0}:TextBox runat=server></{0}:TextBox>"), PermissionSet(SecurityAction.Demand, Unrestricted=true)]
	public class TextBox: System.Web.UI.WebControls.TextBox, ILamassauControl  
	{
	
		#region Initialization
		
		private Label lblView ;
        private Helper Ctr;
        private RegularExpressionValidator regVal;
		
        // Methods
		public TextBox()
		{
			this.lblView = new Label();
            this.Ctr = new Helper();
            this.regVal = new RegularExpressionValidator();
            this.regVal.Visible = false;
            this.Controls.Add(this.regVal);

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

        [Bindable(true), Category("Behavior"), Description("TextBox Inhertance ViewMode Status"), DefaultValue(InhertedViewModes.Auto)]
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


        [Category("Validation"), Description("Regular Expression to be applied to the TextBox"),Editor("System.Web.UI.Design.WebControls.RegexTypeEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), DefaultValue(""), Themeable(false)]
        public string ValidationExpression
        {
            get
            {
                return regVal.ValidationExpression;
            }
            set
            {
                this.regVal.ValidationExpression = value;  
            }
        }
        [Category("Validation")]
        public override string ValidationGroup
        {
            get
            {
                return base.ValidationGroup;
            }
            set
            {
                this.regVal.ValidationGroup = value;
                base.ValidationGroup = value;
            }
        }
        [Category("Validation")]
        public ValidatorDisplay ValidatorDisplayMode 
        {
            get
            {
                return this.regVal.Display; 
            }
            set
            {
                this.regVal.Display = value;
            }
        }

        [Category("Validation"), DefaultValue(true), Description("BaseValidator_EnableClientScript"), Themeable(false)]
        public bool ValidatorEnableClientScript
        {
            get
            {
                return this.regVal.EnableClientScript; 
            }
            set
            {
                this.regVal.EnableClientScript = value;
            }
        }

        [Category("Validation")]
        public string ValidatorErrorMessage 
        {
            get
            {
                return this.regVal.ErrorMessage;
            }
            set
            {
                this.regVal.ErrorMessage = value;
            }
        }
        
        [Category("Validation"),DefaultValue(typeof(Color), "Red")]
        public Color ValidatorForeColor
        {
            get
            {
                return this.regVal.ForeColor; 
            }
            set
            {
                this.regVal.ForeColor = value;
            }
        }
        
        [Browsable(false)]
        public bool IsValid 
        {
            get
            {
                return this.regVal.IsValid;
            }
            set
            {
                this.regVal.IsValid = value; 
            }
        }

        [Category("Validation"), DefaultValue(false), Description("BaseValidator_SetFocusOnError"), Themeable(false)]
        public bool SetFocusOnError 
        {
            get
            {
                return this.regVal.SetFocusOnError; 
            }
            set
            {
                this.regVal.SetFocusOnError = value;
            }
        }

        [Category("Validation"),Description("BaseValidator_Text"), DefaultValue("")]
        public string ValidatorText 
        {
            get
            {
                return this.regVal.Text; 
            }

            set
            {
                this.regVal.Text = value; 
            }
        }

        [Category("Validation"), Description("User Control Validator"), Themeable(false)]
        public bool UseValidator
        {
            get
            {
                return this.regVal.Visible ;
            }
            set
            {
                this.regVal.Visible = value;
            }
        }


        [Category("Appearance"), Description("Should the string formating be applied when the control in Editable state"), Themeable(false)]
        public bool EditStateFormatted
        {
            get
            {
                object o = ViewState["EditStateFormatted"];
                return (o != null) ? bool.Parse(o.ToString()) : false;
            }
            set
            {
                ViewState["EditStateFormatted"] = value;
            }
        }

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
                return (o != null) ? (ValueTypes)o : ValueTypes.Custom;
            }
            set
            {
                ViewState["ValueType"] = value;
            }
        }

        public string Value
        {
            get
            {
                if (this.StringFormat.Length > 0)
                {
                    if (this.ValueType == ValueTypes.Double)
                    {
                        try
                        {
                            return double.Parse(this.Text,NumberStyles.Currency).ToString();
                        }
                        catch
                        {
                            return base.Text;
                        }
                    }
                    else if (this.ValueType == ValueTypes.Decimal )
                    {
                        try
                        {
                            return decimal.Parse(this.Text, NumberStyles.Currency).ToString();
                        }
                        catch
                        {
                            return this.Text;
                        }
                    }
                    else if (this.ValueType == ValueTypes.Single)
                    {
                        try
                        {
                            return Single.Parse(this.Text, NumberStyles.Currency).ToString();
                        }
                        catch
                        {
                            return this.Text;
                        }
                    }
                    else if (this.ValueType == ValueTypes.Int32)
                    {
                        try
                        {
                            return int.Parse(this.Text, NumberStyles.Currency).ToString();
                        }
                        catch
                        {
                            return this.Text;
                        }
                    }
                    else
                    {
                        return this.Text;
                    }

                }
                else
                {
                    return this.Text;
                }
            }
            set
            {
                base.Text = value;
            }
        }






        #endregion

        #region PreRendering

        protected override void CreateChildControls()
		{
            base.CreateChildControls();
            this.lblView.CopyBaseAttributes(this);
            

            this.regVal.ID = this.ClientID + "regval";
            if (this.UseValidator)
            {
                base.Attributes.Add("onchange", "javascript:Page_ClientValidate();");
                this.regVal.ControlToValidate = this.ID;
                this.regVal.Visible = true; 
                
            }

		}




		#endregion
		
		#region Rendering

		protected override void Render(HtmlTextWriter writer)
		{
            if (!this.Visible)
                return;

			EnsureChildControls();


            this.lblView.StringFormat = this.StringFormat;
            this.lblView.ValueType = this.ValueType;

			this.lblView.Text =this.Text.Replace("\n","<br>") ;

			this.lblView.Width = this.Width;
			this.lblView.Height = this.Height;
            
			this.lblView.Enabled = this.Enabled;

            switch (this.ViewMode)
            {

                case ViewModes.ReadOnly:
                    if (!this.DesignMode)
                    {
                        lblView.RenderControl(writer);
                    }
                    break;

                case ViewModes.Editable:
                    try
                    {
                        if (this.EditStateFormatted)
                        {
                            if (this.StringFormat.Trim().Length > 0 && !this.DesignMode)
                            {
                                if (this.ValueType == ValueTypes.Custom)
                                {
                                    this.Text = String.Format(this.StringFormat, this.Text);
                                }
                                else
                                {
                                    Type t = Type.GetType("System." + this.ValueType.ToString());
                                    this.Text = String.Format(this.StringFormat, Lamassau.Helper.TypeHelper.HackType(this.Value, t));
                                }
                            }
                        }
                    }
                    catch
                    {
                        // leave the text as is
                    }

                    base.Render(writer);
                    
                    if (this.UseValidator)
                    {
                        this.regVal.RenderControl(writer);
                    }
                    break;

                default:
                    break;

            }

		}
	
		#endregion

	}
}

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

[assembly: TagPrefix("Lamassau.Web.UI.WebControls", "lms")]

namespace Lamassau.Web.UI.WebControls
{

    /// <summary>
    /// Summary description for DatePicker.
    /// </summary>
    [ToolboxData("<{0}:Calendar runat=server></{0}:Calendar>")]
    [DesignerAttribute(typeof(CalendarControlDesigner))]
    public class DatePicker : System.Web.UI.WebControls.Calendar, ILamassauControl, IPostBackDataHandler, ICallbackEventHandler  
    {
        #region Initialization

        private Label lblDrop;
        private Label lblValue;
        private Helper Ctr;
        private TextBox EditableValue;
        private Label lblView;


        
        // Methods
        public DatePicker()
        {
            this.lblDrop = new Label();
            this.lblValue = new Label();
            this.Ctr = new Helper();
            this.EditableValue = new TextBox();
            
            this.lblView = new Label();
            this.SelectionMode = CalendarSelectionMode.None;
            
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

        [Bindable(true), Category("Behavior"), Description("Inhertance ViewMode Status"), DefaultValue(InhertedViewModes.Auto)]
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

         


        [Browsable(false)]
        public new CalendarSelectionMode SelectionMode
        {
            get
            {
                return base.SelectionMode;
            }

            set
            {
                base.SelectionMode = value;
            }
        }

        public DateTime? SelectedValue
        {
            get
            {
                if (this.SelectedDate.Ticks > 0)
                {
                    return this.SelectedDate;
                }
                else
                {
                    return null; 
                }
            }
            set
            {
                if (value != null)
                {
                    this.SelectedDate = value.Value ;
                }
                else
                {
                    this.SelectedDate = DateTime.MinValue;
                }

            }
        }


        [Bindable(true), Category("Behavior"), Description("Calendar Box DateFormat String"), DefaultValue("MM/dd/yyyy")]
        public String DateFormat
        {
            get
            {
                return (ViewState["DateFormat"] != null) ? ViewState["DateFormat"].ToString() : "MM/dd/yyyy";
            }
            set
            {
                ViewState["DateFormat"] = value;
            }
        }



        [Bindable(false), Category("Layout"), Description("Get or Set the Width of the Calendar Under the Dropdown Box"), DefaultValue("122px")]
        public Unit CalendarWidth
        {
            get
            {
                return (ViewState["CalendarWidth"] != null) ? Unit.Parse(ViewState["CalendarWidth"].ToString()) : Unit.Parse("100px");
            }
            set
            {
                ViewState["CalendarWidth"] = value;
            }
        }

        /// <summary>
        /// Get or Set the Grid Visiblity
        /// </summary>
        [Description("Get or Set the DateSelection Visibility Under the Dropdown Box"), DefaultValue(false)]
        private bool CalendarVisible
        {
            get
            {
                return (ViewState["CalendarVisible"] != null) ? bool.Parse(ViewState["CalendarVisible"].ToString()) : false;
            }
            set
            {
                ViewState["CalendarVisible"] = value;
            }
        }


        /// <summary>
        /// Get or Set if the Calendar Control allow editing the the value field
        /// </summary>
        [Category("Behavior"), Description("Get or Set if the Calendar Control allow editing the the value field"), DefaultValue(true)]
        public bool AllowEdit
        {
            get
            {
                return (ViewState["AllowEdit"] != null) ? bool.Parse(ViewState["AllowEdit"].ToString()) : true;
            }
            set
            {
                ViewState["AllowEdit"] = value;
            }
        }

        [Description("AssociatedControl:  Point to the WebControl to carry the Date Value"), DefaultValue(""), Category("Behavior"), Bindable(true), TypeConverter(typeof(Lamassau.Web.UI.WebControls.TextBoxLabelIDConverter))]
        public string AssociatedControl
        {
            get
            {
                string s = (string)ViewState["AssociatedControl"];
                return (s == null) ? String.Empty : s;
            }
            set
            {
                ViewState["AssociatedControl"] = value;
            }
        }

        #endregion
        #region PreRender

        protected override void CreateChildControls()
        {

            base.CreateChildControls();


            if (this.VisibleDate.ToShortDateString() == "1/1/0001" && !this.DesignMode )
            {
                if (this.SelectedDate.ToShortDateString() == "1/1/0001")
                {
                    this.VisibleDate = DateTime.Now;
                }
                else
                {
                    this.VisibleDate = this.SelectedDate;
                }
            }


            base.Style.Add("Position", "absolute");

            base.Style.Add("ZOrder", "200");
            if (!this.AllowEdit)
            {
                this.lblValue.BackColor = Color.White;
            }
            this.lblValue.Style.Remove("align");
            //this.lblDrop.BackColor = Color.White;


            this.lblView.CopyBaseAttributes(this);
            this.lblValue.CopyBaseAttributes(this);
            this.EditableValue.CopyBaseAttributes(this);
            this.lblDrop.CopyBaseAttributes(this);


            this.lblValue.Style.Add("border-top", "silver 1px solid");
            this.lblValue.Style.Add("border-left", "silver 1px solid");
            this.lblValue.Style.Add("border-bottom", "silver 1px solid");
            this.lblValue.Style.Add("border-right", "silver 1px solid");


            this.lblView.Style.Remove("Position");
            this.lblValue.Style.Remove("Position");
            this.lblDrop.Style.Remove("Position");
            this.EditableValue.Style.Remove("Position"); 

            //this.lblValue.Style.Add("DISPLAY", "inline");
            this.lblValue.Style.Add("overflow", "hidden");
            //this.lblValue.Style.Add("align", "left");

            this.lblDrop.ID = this.ClientID + "_btn";
            this.lblValue.ID = this.ClientID + "_SelectedDate_txt";
            //this.lblValue.Style.Add("vertical-align", "center");
            //this.lblValue.Style.Add("border-top", "windowframe 1px solid");
            //this.lblValue.Style.Add("border-left", "windowframe 1px solid");
            //this.lblValue.Style.Add("border-bottom", "windowframe 1px solid");
            //this.lblValue.Style.Remove("border-right");


            this.EditableValue.ID = this.ClientID + "_SelectedDate_txt";

            this.EditableValue.Style.Add("vertical-align", "center");
  
            this.EditableValue.ValueType = ValueTypes.DateTime;

            this.EditableValue.ValidationExpression = this.ValidationExpression;

            this.EditableValue.ValidatorDisplayMode = this.ValidatorDisplayMode;
            this.EditableValue.ValidationGroup = this.ValidationGroup ;
            this.EditableValue.ValidatorEnableClientScript= this.ValidatorEnableClientScript;
            this.EditableValue.ValidatorErrorMessage = this.ValidatorErrorMessage;
            this.EditableValue.ValidatorForeColor = this.ValidatorForeColor;
            this.EditableValue.ValidatorText = this.ValidatorText;



            if (!(this.AssociatedControl.Trim().Length > 0))
            {
                if (!this.AllowEdit)
                {
                    //this.lblDrop.Style.Add("border-top", "windowframe 1px solid");
                    //this.lblDrop.Style.Add("border-right", "windowframe 1px solid");
                    //this.lblDrop.Style.Add("border-bottom", "windowframe 1px solid");
                    //this.lblDrop.Style.Remove("border-left"); 
                }
            }

            this.lblDrop.Style.Add("background-image", "url(" + this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Lamassau.Web.UI.WebControls.Resources.Images.calendar.gif") + ")");
            this.lblDrop.Style.Add("background-repeat", "no-repeat");
            this.lblDrop.Style.Add("background-position", "center center");

            this.lblDrop.Width = new Unit(16);
            this.lblDrop.Style.Add("CURSOR", "Default");

            this.lblDrop.Attributes.Add("OnClick", "ShowHideCalendar('" + this.ClientID + "');");
            this.lblValue.Attributes.Add("OnClick", "ShowHideCalendar('" + this.ClientID + "');");

            SetPageClickWayAttribute();

            if (this.Height.IsEmpty)
            {
                this.Height = new Unit(16);
            }

            this.lblDrop.Height = this.Height;
            this.lblValue.Height = this.Height;

            this.EditableValue.MaxLength = 10;
            
        }

        private void SetPageClickWayAttribute()
        {
            if (!this.DesignMode)
            {
                if (this.Page.Form.Attributes["OnClick"] != null)
                {
                    string tmpOnclick = this.Page.Form.Attributes["OnClick"].Trim();
                    if (tmpOnclick.Length > 0)
                    {
                        if (tmpOnclick.EndsWith(";"))
                        {
                            this.Page.Form.Attributes["OnClick"] = tmpOnclick + " OnCalendarClickAway();";
                        }
                        else
                        {
                            this.Page.Form.Attributes["OnClick"] = tmpOnclick + "; OnCalendarClickAway();";
                        }
                        return;

                    }
                }

                this.Page.Form.Attributes.Add("OnClick", "OnCalendarClickAway();");
            }
        }


        protected override void OnPreRender(System.EventArgs e)
        {
            if (!this.Page.ClientScript.IsClientScriptBlockRegistered("CalendarScript"))
            {
                this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "CalendarScript", this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Lamassau.Web.UI.WebControls.Resources.Scripts.calendar.js"));
            }

            if (Page != null)
            {
                Page.RegisterRequiresPostBack(this);
                string strStartupScript = @"
<SCRIPT language=""javascript"" type=""text/javascript""> 
<!--
// Position " + this.ClientID + @" Correctly 
SetCalendarTablePosition('" + this.ClientID + @"'); 
// -->
</script>";

                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), this.ClientID + "_SetPosition", strStartupScript);

                base.Attributes.Add("OnClick", "CalendarClick();");
            }

            GetCallbackScript();
            SetDayRenderStrings();
            base.OnPreRender(e);

        }


        #region HELPERS

           // *****************************************************************************
        // METHOD GetCalendarMarkup
        // Return the default markup for the control
        private string GetCalendarMarkup()
        {
            base.Style.Add("DISPLAY", CalendarVisible ? "inline" : "NONE");
            base.Style.Add("Position", "absolute");



            StringWriter writer = new StringWriter();
            HtmlTextWriter buffer = new HtmlTextWriter(writer);
            Unit tmpWidthUnit = base.Width;

            base.Width = this.CalendarWidth ;
            base.Render(buffer);
            string markup = writer.ToString();

            base.Width = tmpWidthUnit;
            

            //int previousMonthJSPos = markup.IndexOf("__doPostBack(");
            //string JSPrevFunction = markup.Substring(previousMonthJSPos, markup.IndexOf(")", previousMonthJSPos) - previousMonthJSPos + 1);

            //int nextMonthJSPos = markup.IndexOf("__doPostBack(", previousMonthJSPos + JSPrevFunction.Length);
            //string JSNextFunction = markup.Substring(nextMonthJSPos, markup.IndexOf(")", nextMonthJSPos) - nextMonthJSPos + 1);

            int previousMonthJSPos = markup.IndexOf(@"href=""javascript:__doPostBack(");
            string JSPrevFunction = markup.Substring(previousMonthJSPos, markup.IndexOf(")", previousMonthJSPos) - previousMonthJSPos + 1);

            int nextMonthJSPos = markup.IndexOf(@"href=""javascript:__doPostBack(", previousMonthJSPos + JSPrevFunction.Length);
            string JSNextFunction = markup.Substring(nextMonthJSPos, markup.IndexOf(")", nextMonthJSPos) - nextMonthJSPos + 1);


            string sCallBackNextFunctionInvocation = this.Page.ClientScript.GetCallbackEventReference(this, "'" + this.VisibleDate.AddMonths(1).ToShortDateString() + "'", "ShowVisibleMonth", "'" + this.ClientID + "'") ;

            string sCallBackPreviousFunctionInvocation = this.Page.ClientScript.GetCallbackEventReference(this, "'" + this.VisibleDate.AddMonths(-1).ToShortDateString() + "'", "ShowVisibleMonth", "'" + this.ClientID + "'") ;

            //markup = markup.Replace(JSNextFunction, sCallBackNextFunctionInvocation);
            //markup = markup.Replace(JSPrevFunction, sCallBackPreviousFunctionInvocation);

            markup = markup.Replace(JSNextFunction, @"onclick=""" + sCallBackNextFunctionInvocation +@"""");
            markup = markup.Replace(JSPrevFunction, @"onclick=""" + sCallBackPreviousFunctionInvocation + @"""");


            return markup;

        }


        #endregion

        #region Interfact: IPostBackDataHandler
        public virtual void RaisePostDataChangedEvent()
        {
            this.OnSelectionChanged();
        }

        public virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            string key = postDataKey.Replace("$", "_");
            string CalendarVisiblePostedValue = postCollection[key + "_CalendarVisible_hdn"];
            this.CalendarVisible = CalendarVisiblePostedValue == null ? false : bool.Parse(CalendarVisiblePostedValue);
            
            string SelectedDatePresentValue = this.SelectedDate.ToString();


            string SelectedDateDisplayedPostedValue = null;
            if (!(this.AssociatedControl.Trim().Length > 0))
            {
                if (this.AllowEdit)
                {
                    SelectedDateDisplayedPostedValue = postCollection[key + "_SelectedDate_txt"];
                }
            }
            else
            { 
                SelectedDateDisplayedPostedValue = postCollection[this.AssociateControlObject.UniqueID];
            }

            string SelectedDatePostedValue = SelectedDateDisplayedPostedValue;

            if (SelectedDatePostedValue == null)
            {
                string SelectedDateCalendarHiddenPostedValue = postCollection[key + "_SelectedDate_hdn"];
                SelectedDatePostedValue = SelectedDateCalendarHiddenPostedValue;
            }

            if (SelectedDatePresentValue == null || !SelectedDatePresentValue.Equals(SelectedDatePostedValue))
            {
                if (SelectedDatePostedValue != null)
                {
                    if (SelectedDatePostedValue.Trim().Length == 0)
                    {
                        this.SelectedDate = DateTime.MinValue;
                    }
                    else
                    {
                        if (this.VisibleDate == this.SelectedDate)
                        {
                            this.VisibleDate = DateTime.Parse(SelectedDatePostedValue);
                        }
                        this.SelectedDate = DateTime.Parse(SelectedDatePostedValue);
                    }
                    return true;
                }
            }
            return false;

        }

        #endregion

        #endregion

        #region Render

        private string javascriptFunctionName = string.Empty  ;
        private string AssociatedControlClientID = "";

        private void SetDayRenderStrings()
        {
            if (this.javascriptFunctionName == string.Empty || this.AssociatedControlClientID == string.Empty)
            {
                if (this.AssociatedControl.Trim().Length > 0)
                {
                    //Control tmpAssociatedControl = this.Parent.FindControl(this.AssociatedControl);
                    //string controlType = tmpAssociatedControl.GetType().ToString();
                    //controlType = controlType.Substring(controlType.LastIndexOf(".") + 1).ToLower();
                    this.javascriptFunctionName = "calendarselectdate" + this.AssociatedControlType.ToLower();
                    this.AssociatedControlClientID = this.AssociateControlObject.ClientID;
                }
                else
                {
                    if (this.AllowEdit)
                    {
                        this.javascriptFunctionName = "calendarselectdatetextbox";
                    }
                    else
                    {
                        this.javascriptFunctionName = "calendarselectdatelabel";
                    }
                    this.AssociatedControlClientID = this.ClientID + "_SelectedDate_txt" ;
                }
            }
        }

        private Control AssociateControlObject
        {
            get
            {
                return this.Parent.FindControl(this.AssociatedControl);
            }
        }

        private string AssociatedControlType
        {
            get
            {
                if (this.AssociatedControl.Trim().Length > 0)
                {
                    Control tmpAssociatedControl = this.Parent.FindControl(this.AssociatedControl);
                    string controlType = tmpAssociatedControl.GetType().ToString();
                    controlType = controlType.Substring(controlType.LastIndexOf(".") + 1);
                    return controlType;
                }
                else
                {
                    if (this.AllowEdit)
                    {
                        return "TextBox";
                    }
                    else
                    {
                        return "Label";
                    }
                }
            }
        }


        protected override void OnDayRender(TableCell cell, CalendarDay day)
        {
            cell.Attributes.Add("OnClick", this.javascriptFunctionName + "('" + this.ClientID + "','" + this.AssociatedControlClientID + "','" + day.Date.ToString() + "','" + day.Date.ToString(this.DateFormat) + "');");
            base.OnDayRender(cell, day);

        }

        /// <summary> 
        /// Render this control to the output parameter specified.
        /// </summary>
        /// <param name="output"> The HTML writer to write out to </param>
        protected override void Render(HtmlTextWriter writer)
        {

            if (!this.Visible)

                return;

            EnsureChildControls();

            //this.lblValue.Width = this.TextBoxWidth;
            this.lblValue.Width = this.Width;
            this.lblValue.Height = this.lblDrop.Height;
            this.lblValue.Enabled = this.Enabled;

            //this.EditableValue.Width = this.TextBoxWidth;
            this.EditableValue.Width = this.Width;
            this.EditableValue.Enabled = this.Enabled;


            this.lblDrop.Height = this.Height;
            this.lblDrop.Enabled = this.Enabled;


            this.lblView.Enabled = this.Enabled;

            if (this.lblDrop.Style["POSITION"] != null)
            {
                if (this.lblDrop.Style["POSITION"].Trim().ToLower() == "absolute")
                {
                    lblDrop.Style["LEFT"] = (Unit.Parse(this.lblValue.Style["LEFT"]).Value + this.lblValue.Width.Value).ToString();
                }
            }

            if (this.SelectedDate.Ticks > 0)
            {
                this.lblValue.Text = this.SelectedDate.ToString(this.DateFormat);
                this.EditableValue.Text = this.SelectedDate.ToString(this.DateFormat);
                this.lblView.Text = this.SelectedDate.ToString(this.DateFormat);
            }

            switch (this.ViewMode)
            {
                case ViewModes.ReadOnly:
                    lblView.RenderControl(writer);
                    break;

                case ViewModes.Editable:
                    writer.Write("<NOBR>");
                    
                    if (!(this.AssociatedControl.Trim().Length > 0))
                    {
                        if (this.AllowEdit)
                        {
                            this.EditableValue.RenderControl(writer);
                        }
                        else
                        {
                            this.lblValue.RenderControl(writer);
                        }
                    }

                    this.lblDrop.RenderControl(writer);
                    writer.Write("</NOBR>");
                    writer.Write("<INPUT id=" + this.ClientID + "_CalendarVisible_hdn name=" + this.ClientID + "_CalendarVisible_hdn TYPE=\"HIDDEN\" value=\"" + this.CalendarVisible.ToString() + "\"/>\n");
                    writer.Write("<INPUT id=" + this.ClientID + "_SelectedDate_hdn name=" + this.ClientID + "_SelectedDate_hdn TYPE=\"HIDDEN\" value=\"" + this.SelectedDate.ToString() + "\"/>\n");

                    // Output
                    if (!DesignMode)
                    {
                        // Get the default markup
                        string markup = GetCalendarMarkup();
                        writer.Write(markup);
                    }
                    break;

                default:
                    break;
            }

        }

        private void GetCallbackScript()
        {
            string strScript = @" <SCRIPT language=""javascript"" type=""text/javascript""> 

<!--

//// CallBack Implementation
function ShowVisibleMonth(Message, context) {
document.getElementById(context).outerHTML = Message;
}

function OnError(message, context) {
alert('An unhandled exception has occurred:\n' + message);
}

// -->

</script>";

            if (!this.Page.ClientScript.IsClientScriptBlockRegistered(this.ClientID + "_CalendarCallbackVisibleMonth"))
            {
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), this.ClientID + "_CalendarCallbackVisibleMonth", strScript);
            }
        }


        #endregion

        #region ICallbackEventHandler Members

        public string GetCallbackResult()
        {
            base.Style.Add("DISPLAY", CalendarVisible ? "inline" : "NONE");
            base.Style.Add("Position", "absolute");
            return GetCalendarMarkup();
        }

        public void RaiseCallbackEvent(string eventArgument)
        {
            this.VisibleDate = DateTime.Parse(eventArgument);
            this.CalendarVisible = true;
            SetDayRenderStrings();
        }

        #endregion


        [Category("Appearance")]
        public string EditableTextBoxCssClass
        {
            get
            {
                return this.EditableValue.CssClass ;
            }
            set
            {
                this.EditableValue.CssClass = value;
            }
        }


        [Category("Validation"), Description("Regular Expression to be applied to the TextBox"), Editor("System.Web.UI.Design.WebControls.RegexTypeEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), DefaultValue(""), Themeable(false)]
        public string ValidationExpression
        {
            get
            {
                if (this.EditableValue.ValidationExpression != "")
                {
                    return this.EditableValue.ValidationExpression;
                }
                else
                {
                    return @"(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d ";
                }
            }
            set
            {
                this.EditableValue.ValidationExpression = value;
            }
        }

        [Category("Validation")]
        public string ValidationGroup
        {
            get
            {
                return this.EditableValue.ValidationGroup;
            }
            set
            {
                this.EditableValue.ValidationGroup = value;
            }
        }
        [Category("Validation")]
        public ValidatorDisplay ValidatorDisplayMode
        {
            get
            {
                return this.EditableValue.ValidatorDisplayMode ;
            }
            set
            {
                this.EditableValue.ValidatorDisplayMode  = value;
            }
        }

        [Category("Validation"), DefaultValue(true), Description("BaseValidator_EnableClientScript"), Themeable(false)]
        public bool ValidatorEnableClientScript
        {
            get
            {
                return this.EditableValue.ValidatorEnableClientScript ;
            }
            set
            {
                this.EditableValue.ValidatorEnableClientScript = value;
            }
        }

        [Category("Validation")]
        public string ValidatorErrorMessage
        {
            get
            {
                return this.EditableValue.ValidatorErrorMessage;
            }
            set
            {
                this.EditableValue.ValidatorErrorMessage = value;
            }
        }

        [Category("Validation"), DefaultValue(typeof(Color), "Red")]
        public Color ValidatorForeColor
        {
            get
            {
                return this.EditableValue.ValidatorForeColor;
            }
            set
            {
                this.EditableValue.ValidatorForeColor = value;
            }
        }

        [Browsable(false)]
        public bool IsValid
        {
            get
            {
                return this.EditableValue.IsValid;
            }
            set
            {
                this.EditableValue.IsValid = value;
            }
        }

        [Category("Validation"), DefaultValue(false), Description("BaseValidator_SetFocusOnError"), Themeable(false)]
        public bool SetFocusOnError
        {
            get
            {
                return this.EditableValue.SetFocusOnError;
            }
            set
            {
                this.EditableValue.SetFocusOnError = value;
            }
        }

        [Category("Validation"), Description("BaseValidator_Text"), PersistenceMode(PersistenceMode.InnerDefaultProperty), DefaultValue("")]
        public string ValidatorText
        {
            get
            {
                return this.EditableValue.ValidatorText ;
            }

            set
            {
                this.EditableValue.ValidatorText = value;
            }
        }

        [Category("Validation"), Description("User Control Validator"), Themeable(false)]
        public bool UseValidator
        {
            get
            {
                return this.EditableValue.UseValidator ;
            }
            set
            {
                this.EditableValue.UseValidator = value;
            }
        }



    }

    #region Design Time Support
    namespace Design
    {
        public class CalendarControlDesigner : CalendarDesigner
        {
            public override string GetDesignTimeHtml()
            {
                // Component is the instance of the component or control that
                // this designer object is associated with. This property is 
                // inherited from System.ComponentModel.ComponentDesigner.
                string tmpS = string.Empty;

                try
                {
                    Calendar Calendar = (Calendar)Component;
                    // Capture the default output of the Calendar control
                    StringWriter writer = new StringWriter();
                    HtmlTextWriter buffer = new HtmlTextWriter(writer);
                    Calendar.RenderControl(buffer);
                    tmpS = writer.ToString();
                }
                catch (Exception ex)
                {
                    tmpS = this.GetErrorDesignTimeHtml(ex);
                }
                return tmpS;
            }

            protected override string GetErrorDesignTimeHtml(Exception e)
            {
                return this.CreatePlaceHolderDesignTimeHtml(e.Message);
            }

        }

    }

    #endregion

}




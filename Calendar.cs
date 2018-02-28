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
    [ToolboxData("<{0}:TextBox runat=server></{0}:TextBox>"), PermissionSet(SecurityAction.Demand, Unrestricted = true)]
    public class Calendar : TextBox, ILamassauControl
    {
        public Calendar()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            this.MaxLength = 10;
            this.ValueType = ValueTypes.DateTime; 
            this.UseValidator = true;
            this.ValidationExpression = @"([1-9]|0[1-9]|1[012])[-./]([1-9]|0[1-9]|[12][0-9]|3[01])[-./](19|20)\d\d";
            //this.ValidationExpression = @"(((0[13578]|10|12)([-./])(0[1-9]|[12][0-9]|3[01])([-./])(\d{4}))|((0[469]|11)([-./])([0][1-9]|[12][0-9]|30)([-./])(\d{4}))|((2)([-./])(0[1-9]|1[0-9]|2[0-8])([-./])(\d{4}))|((2)(\.|-|\/)(29)([-./])([02468][048]00))|((2)([-./])(29)([-./])([13579][26]00))|((2)([-./])(29)([-./])([0-9][0-9][0][48]))|((2)([-./])(29)([-./])([0-9][0-9][2468][048]))|((2)([-./])(29)([-./])([0-9][0-9][13579][26]))) ";
            this.ValidatorText = "*";
            //this.ValidatorErrorMessage = "<br>" + this.ToolTip + " the format should be [" + this.DateFormat + "]";
            this.ValidatorErrorMessage = this.ToolTip + " Not valid.  Enter date as mm/dd/yyyy" + "<br>";
        }


        public DateTime? SelectedValue
        {
            get

            {
                try
                {
                    DateTime dtvl = (DateTime.Parse(this.Value));
                    if (dtvl.Ticks > 0)
                    {
                        return dtvl;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                if (value != null)
                {
                    this.Value  = value.Value.ToString(this.DateFormat) ;
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




    }

}
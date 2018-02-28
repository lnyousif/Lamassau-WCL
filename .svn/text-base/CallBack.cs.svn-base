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
    /// TextBox: Inherting the WebControl ASP.NET control;
    /// Designed by Laith N Yousif.
    /// Developed by Laith N Yousif. 
    /// Date: My 2006.
    /// </summary>
    [ToolboxData("<{0}:CallBack runat=server></{0}:CallBack>"),PermissionSet(SecurityAction.Demand, Unrestricted=true)]
    public class CallBack : System.Web.UI.WebControls.WebControl, ICallbackEventHandler 
    {

        public delegate void CallBackRaiseEventHandler(object sender, CallbackRaiseEventArgs e);
        public delegate void CallBackGetResultEventHandler(object sender, CallBackGetResultEventArgs e);

        public event CallBackRaiseEventHandler CallBackRaiseEvent;
        public event CallBackGetResultEventHandler CallBackGetResultEvent;


        public CallBack()
		{

		}

        protected override void Render(HtmlTextWriter writer)
        {
            if (this.DesignMode)
            {
                writer.Write("["+this.ID+"]");
            }
        }


        protected override void OnLoad(EventArgs e)
        {
            if (this.Enabled)
            {
                if (this.Script.Trim() != "")
                {
                    string strScript = this.Script.Replace("[CustomCallBackFunction]", this.CustomCallBackFunction);
                    if (!this.Page.ClientScript.IsClientScriptBlockRegistered(this.ClientID + "CallBackScript"))
                    {
                        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), this.ClientID + "CallBackScript", strScript, true);
                    }
                }

                if (this.CallBackInitiationControlID.Trim() != "")
                {
                    try
                    {
                        ((WebControl)this.Parent.FindControl(this.CallBackInitiationControlID)).Attributes.Add(this.CallBackInitiationClientEvent, this.Page.ClientScript.GetCallbackEventReference(this, this.CallBackArguments, this.CallBackClientFunction, this.CallBackContext, this.CallBackClientErrorFunction, this.CallBackAsynchronous));
                    }
                    catch
                    {
                        try
                        {
                            ((HtmlControl)this.Parent.FindControl(this.CallBackInitiationControlID)).Attributes.Add(this.CallBackInitiationClientEvent, this.Page.ClientScript.GetCallbackEventReference(this, this.CallBackArguments, this.CallBackClientFunction, this.CallBackContext, this.CallBackClientErrorFunction, this.CallBackAsynchronous));
                        }
                        catch
                        { }

                    }
                }
                base.OnLoad(e);
            }
        }

        [Description("CallBackInitiationControlID:  Point to the Control to be Associated by a client event in order to initialize the call back"), DefaultValue(""), Category("Behavior"), Bindable(true), TypeConverter(typeof(Lamassau.Web.UI.WebControls.WebControlIDConverter))]
        public virtual string CallBackInitiationControlID
        {
            get
            {
                object o = ViewState["CallBackInitiationControlID"];
                return (o != null) ? o.ToString() : "";
            }
            set
            {
                ViewState["CallBackInitiationControlID"] = value;
            }
        }

        [Bindable(false), Category("Behavior"), Description("CallBackInitiationClientEvent"), DefaultValue("")]
        public virtual string CallBackInitiationClientEvent
        {
            get
            {
                object o = ViewState["CallBackInitiationClientEvent"];
                return (o != null) ? o.ToString() : "";
            }
            set
            {
                ViewState["CallBackInitiationClientEvent"] = value;
            }
        }


        [Browsable(false),
        Bindable(true),
        PersistenceMode(PersistenceMode.InnerProperty),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public virtual string Script
        {
            get
            {
                object o = ViewState["Script"];
                return (o != null) ? o.ToString() : "";
            }
            set
            {
                ViewState["Script"] = value;
            }
        }

        [Browsable(false),Bindable(false)]
        public string CustomCallBackFunction
        {
            get
            {
                return GetCallbackEventReference(this, this.CallBackArguments, this.CallBackClientFunction, this.CallBackContext, this.CallBackClientErrorFunction, this.CallBackAsynchronous);
            }
        }

        public string GetCallbackEventReference(
    Control control, string argument, string clientCallback, string context, string clientErrorCallback, bool useAsynch)
        {

            string js = string.Empty;
            try
            {
                if (this.Enabled)
                {
                    if (this.InitiBeforeEveryCallback)
                    {
                        js = String.Format("javascript:{0};{1};{2};",
                            "__theFormPostData = ''",
                            "WebForm_InitCallback()",
                            this.Page.ClientScript.GetCallbackEventReference(control, argument, clientCallback, context, clientErrorCallback, useAsynch));
                    }
                    else
                    {
                    js = String.Format("javascript:{0};", this.Page.ClientScript.GetCallbackEventReference(control, argument, clientCallback, context, clientErrorCallback, useAsynch));
                    }
                }
            }
            catch
            { }
            return js;
        }


        [Bindable(false), Category("Behavior"), Description("CallBackClientFunction"), DefaultValue("")]
        public virtual string CallBackClientFunction
        {
            get
            {
                object o = ViewState["CallBackClientFunction"];
                return (o != null) ? o.ToString() : "";
            }
            set
            {
                ViewState["CallBackClientFunction"] = value;
            }
        }

        [Bindable(false), Category("Behavior"), Description("CallBackClientErrorFunction"), DefaultValue("")]
        public virtual string CallBackClientErrorFunction
        {
            get
            {
                object o = ViewState["CallBackClientErrorFunction"];
                return (o != null) ? o.ToString() : "";
            }
            set
            {
                ViewState["CallBackClientErrorFunction"] = value;
            }
        }

        [Bindable(false), Category("Behavior"), Description("CallBackArguments"), DefaultValue("")]
        public virtual string CallBackArguments
        {
            get
            {
                object o = ViewState["CallBackArguments"];
                return (o != null) ? o.ToString() : "";
            }
            set
            {
                ViewState["CallBackArguments"] = value;
            }
        }

        [Bindable(true), Category("Behavior"), Description("CallBackContext"), DefaultValue("")]
        public virtual string CallBackContext
        {
            get
            {
                object o = ViewState["CallBackContext"];
                return (o != null) ? o.ToString() : "";
            }
            set
            {
                ViewState["CallBackContext"] = value;
            }
        }

        [Bindable(true), Category("Behavior"), Description("CallBackAsynchronous"), DefaultValue("")]
        public virtual bool CallBackAsynchronous
        {
            get
            {
                object o = ViewState["CallBackAsynchronous"];
                return (o != null) ? bool.Parse(o.ToString()) : true;
            }
            set
            {
                ViewState["CallBackAsynchronous"] = value;
            }
        }

        [Bindable(true), Category("Behavior"), Description("InitiBeforeEveryCallback"), DefaultValue("false")]
        public virtual bool InitiBeforeEveryCallback
        {
            get
            {
                object o = ViewState["InitiBeforeEveryCallback"];
                return (o != null) ? bool.Parse(o.ToString()) : true;
            }
            set
            {
                ViewState["InitiBeforeEveryCallback"] = value;
            }
        }

        #region ICallbackEventHandler Members

        public string GetCallbackResult()
        {
                this.SaveViewState();
                try
                {
                    CallBackGetResultEventArgs e = new CallBackGetResultEventArgs();
                    CallBackGetResultEvent(this, e);
                    return e.StringToClient;
                }
                catch
                {
                    return "GetResult Event is not implemented";
                }

        }

        public void RaiseCallbackEvent(string eventArgument)
        {
            if (this.Enabled)
            {
                try
                {
                    CallbackRaiseEventArgs e = new CallbackRaiseEventArgs();
                    e.CallBackCommand = eventArgument;
                    CallBackRaiseEvent(this, e);
                }
                catch
                {
                    // nothing to do
                }
            }
        }

        #endregion

    }

    public class CallbackRaiseEventArgs : EventArgs 
    {
       private string _CallBackCommand = string.Empty ;
       
       //Properties.
        public string CallBackCommand
        {
            get
            {
                return _CallBackCommand;
            }
            set
            {
                _CallBackCommand = value;
            }
        }
    }


    public class CallBackGetResultEventArgs : EventArgs
    {
        private string _StringToClient = string.Empty;
        //Properties.
        public string StringToClient
        {
            get
            {
                return _StringToClient;
            }
            set
            {
                _StringToClient = value;
            }
        }
    }
}

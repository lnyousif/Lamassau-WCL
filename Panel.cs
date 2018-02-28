using System;
using System.Globalization;
using System.Resources;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.Design.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Lamassau.Web.UI;
using Lamassau.Web.UI.WebControls;
using System.Collections.Specialized;

[assembly: TagPrefix("Lamassau.Web.UI.WebControls", "lms")]
namespace Lamassau.Web.UI.WebControls
{
    [Designer(typeof(PanelContainerDesigner)), DefaultProperty("TitleText"), ToolboxData("<{0}:Panel runat=server></{0}:Panel>")]
    public class Panel : System.Web.UI.WebControls.Panel, ILamassauControl
    {

        private Helper Ctr;

        public Panel()
        {
            this.Ctr = new Helper();
        }


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
                 if (this.HasControls())
                {
                    UpdateControlsState(this.Controls,value);
                }
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

        private void UpdateControlsState(System.Web.UI.ControlCollection ctrCollection, ViewModes value)
        {
            foreach (Control myControl in ctrCollection)
            {
                if (myControl is ILamassauControl)
                {
                    if (((ILamassauControl)myControl).InhertedViewMode == InhertedViewModes.Auto)
                    {
                        ((ILamassauControl)myControl).ViewMode = value;
                    }
                }



                if (myControl.HasControls())
                {
                    try
                    {
                        UpdateControlsState(myControl.Controls, ((ILamassauControl)myControl).ViewMode);
                    }
                    catch
                    {
                        UpdateControlsState(myControl.Controls, value);
                    }
                }

            }

        }


        #endregion


        private void BuildJavascript()
        {
            //Register the toggle function

            bool flag1 = false;
            bool flag2 = false;
            bool flag3 = false;
            string text1 = "";
            string text2 = this.TableStyle(this.CollapsedTitleStyle, ref flag2);
            string text3 = this.TableStyle(this.TitleStyle, ref flag3);
            string strUserAgent = HttpContext.Current.Request.UserAgent;
            string strBrowserType = "";
            string strContainers = "";
            string strLeftContainer = this.UniqueID + "_ContainerLeft";
            string strRightContainer = this.UniqueID + "_ContainerRight";
            if (this.TitleStyleContainerMode == TitleStyleContainer.Normal)
            {
                strContainers = strLeftContainer + ";" + strRightContainer;
                if ((this.ExpandImageUrl.Length != 0) && (this.CollapseImageUrl.Length != 0))
                {
                    strContainers = strContainers + ";" + this.m_actionLink.ClientID;
                }
            }
            else if (this.TitleStyleContainerMode == TitleStyleContainer.TitleOnly)
            {
                strContainers = strLeftContainer;
            }
            else if (this.TitleStyleContainerMode == TitleStyleContainer.EntirePanel)
            {
                strContainers = strLeftContainer;
            }
            if (this.Collapsed && this.Collapsable)
            {
                text1 = text2;
                flag1 = flag2;
            }
            else
            {
                text1 = text3;
                flag1 = flag3;
            }
            if (strUserAgent.IndexOf("MSIE") >= 0)
            {
                strBrowserType = "_IE";
            }
            else if (strUserAgent.IndexOf("Gecko") >= 0)
            {
                strBrowserType = "_NS";
            }
            else
            {
                strBrowserType = "";
            }

            string strClickScript = "javascript:CollapsablePanel" + strBrowserType + "_DoNothing();";

            if (this.Collapsable)
            {
                string strScript = "<script language=\"javascript\">\n";
                //string text2 = strScript;
                strScript += "var " + this.UniqueID.Replace(":", "_") + "_Height = document.getElementById('" + this.UniqueID + "_contentRow').clientHeight;\n";
                if (this.Collapsable && this.Collapsed)
                {
                    strScript += "document.getElementById('" + this.UniqueID + "_contentRow').style.display='none';";
                }
                strScript = strScript + "</script>";
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), this.UniqueID, strScript);


                


                strClickScript = "CollapsablePanel" + strBrowserType + "_ExpandCollapse('" + this.UniqueID + "_contentRow','" + this.m_actionLink.ClientID + "','" + this.m_currentState.ClientID + "','" + this.ExpandText + "','" + this.CollapseText + "'," + this.AllowSliding.ToString().ToLower() + ", " + this.UniqueID.Replace(":", "_") + "_Height, " + this.SlideSpeed.ToString() + ", " + this.SlideLines.ToString() + ", '" + strContainers + "', " + flag2.ToString().ToLower() + ", " + flag3.ToString().ToLower() + ", '" + text2 + "', '" + text3 + "', '" + this.ClientID + "','[statusGuide]','" + this.JavascriptOnToggleFunction + "');";
                

                strClickScript = @"
function " + this.ClientID + @"_CollapsablePanel_Toggle()
{
    " + strClickScript.Replace("[statusGuide]","") + @"
}

function " + this.ClientID + @"_CollapsablePanel_Expand()
{
    " + strClickScript.Replace("[statusGuide]","expand") + @"
}

function " + this.ClientID + @"_CollapsablePanel_Collapse()
{
    " + strClickScript.Replace("[statusGuide]", "collapse") + @"
}

";

                if (!Page.ClientScript.IsClientScriptBlockRegistered(this.GetType(), this.ClientID + "_Toggle"))
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), this.ClientID + "_Toggle", strClickScript, true);
                }

            }
        }

        protected override void CreateChildControls()
        {
            this.m_currentState = new HtmlInputHidden();
            this.m_actionLink = new HyperLink();
            this.m_titleLink = new HyperLink();
            this.m_actionImage = new Image();
            this.m_currentState.ID = this.UniqueID + "_currentState";
            this.m_actionLink.ID = this.UniqueID + "_actionLink";
            this.m_actionImage.ID = this.UniqueID + "_actionImg";
            this.m_titleLink.ID = this.UniqueID + "_titleLink";
        }

        protected override void LoadViewState(object savedState)
        {
            if (savedState != null)
            {
                object[] objArray1 = (object[])savedState;
                bool flag1 = this.Collapsed;
                if (objArray1[0] != null)
                {
                    base.LoadViewState(objArray1[0]);
                }
                if (objArray1[1] != null)
                {
                    ((IStateManager)this.TitleLinkStyle).LoadViewState(objArray1[1]);
                }
                if (objArray1[2] != null)
                {
                    ((IStateManager)this.TitleStyle).LoadViewState(objArray1[2]);
                }
                if (objArray1[3] != null)
                {
                    ((IStateManager)this.CollapsedTitleStyle).LoadViewState(objArray1[3]);
                }
                this.Collapsed = flag1;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (this.Page.IsPostBack)
            {
                this.EnsureChildControls();
                if (HttpContext.Current.Request.Form[this.m_currentState.Name] != null)
                {
                    this.Collapsed = bool.Parse(HttpContext.Current.Request.Form[this.m_currentState.Name]);
                }
            }
        }
        




        protected override void OnPreRender(EventArgs e)
        {
            string text1 = HttpContext.Current.Request.UserAgent;
            if (text1.IndexOf("Opera") >= 0)
            {
                this.AllowSliding = false;
            }
            if (text1.IndexOf("MSIE") >= 0)
            {
                if ((this.Page != null) && !this.Page.ClientScript.IsClientScriptIncludeRegistered(this.GetType(), "CollapsablePanelScript"))
                {

                    this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "CollapsablePanelScript", this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Lamassau.Web.UI.WebControls.Resources.Scripts.collapsablepanel.js"));
                }
                this.m_currentState.Value = this.Collapsed.ToString();
            }
            else
            {
                this.Collapsable = false;
            }
            this.BuildJavascript();
            base.OnPreRender(e);
        }

        protected override void Render(HtmlTextWriter output)
        {


                if (this.Collapsable && !this.DesignMode )
                {
                    this.RenderTheContent(output);
                }
                else
                {
                    base.Render(output);
                }
            

        }

        private void RenderTheContent(HtmlTextWriter output)
        {
            bool flag1 = false;
            bool flag2 = false;
            bool flag3 = false;
            string text1 = "";
            string text2 = this.TableStyle(this.CollapsedTitleStyle, ref flag2);
            string text3 = this.TableStyle(this.TitleStyle, ref flag3);
            string strUserAgent = HttpContext.Current.Request.UserAgent;
            string strBrowserType = "";
            string strContainers = "";
            string strLeftContainer = this.UniqueID + "_ContainerLeft";
            string strRightContainer = this.UniqueID + "_ContainerRight";
            if (this.TitleStyleContainerMode == TitleStyleContainer.Normal)
            {
                strContainers = strLeftContainer + ";" + strRightContainer;
                if ((this.ExpandImageUrl.Length != 0) && (this.CollapseImageUrl.Length != 0))
                {
                    strContainers = strContainers + ";" + this.m_actionLink.ClientID;
                }
            }
            else if (this.TitleStyleContainerMode == TitleStyleContainer.TitleOnly)
            {
                strContainers = strLeftContainer;
            }
            else if (this.TitleStyleContainerMode == TitleStyleContainer.EntirePanel)
            {
                strContainers = strLeftContainer;
            }
            if (this.Collapsed && this.Collapsable)
            {
                text1 = text2;
                flag1 = flag2;
            }
            else
            {
                text1 = text3;
                flag1 = flag3;
            }
            if (strUserAgent.IndexOf("MSIE") >= 0)
            {
                strBrowserType = "_IE";
            }
            else if (strUserAgent.IndexOf("Gecko") >= 0)
            {
                strBrowserType = "_NS";
            }
            else
            {
                strBrowserType = "";
            }
            if (base.Style.Count > 0)
            {
                IEnumerator enumerator1 = base.Style.Keys.GetEnumerator();
                while (enumerator1.MoveNext())
                {
                    output.AddStyleAttribute(enumerator1.Current.ToString(), base.Style[enumerator1.Current.ToString()]);
                }
                base.Style.Clear();
            }
            if (this.TitleStyleContainerMode == TitleStyleContainer.EntirePanel)
            {
                if (flag1)
                {
                    output.AddAttribute("class", text1);
                }
                else if (text1.Length != 0)
                {
                    output.AddAttribute("style", text1);
                }
                output.AddAttribute("id", strContainers);
            }
            output.AddAttribute("width", this.Width.ToString());
            output.AddAttribute("cellspacing", "0");
            output.AddAttribute("cellpadding", "0");
            output.AddStyleAttribute("table-layout", "fixed");
            output.RenderBeginTag("table");
            output.RenderBeginTag("tr");
            if (this.TitleStyleContainerMode == TitleStyleContainer.TitleOnly)
            {
                output.RenderBeginTag("td");
                if (flag1)
                {
                    output.AddAttribute("class", text1);
                }
                else if (text1.Length != 0)
                {
                    output.AddAttribute("style", text1);
                }
                output.AddAttribute("id", strContainers);
                output.AddAttribute("width", this.Width.ToString());
                output.AddAttribute("cellspacing", "0");
                output.AddAttribute("cellpadding", "0");
                output.RenderBeginTag("table");
                output.RenderBeginTag("tr");
            }
            if (this.CollapserAlign == HorizontalAlignment.Right)
            {
                if (this.TitleStyleContainerMode == TitleStyleContainer.Normal)
                {
                    if (flag1)
                    {
                        output.AddAttribute("class", text1);
                    }
                    else if (text1.Length != 0)
                    {
                        output.AddAttribute("style", text1);
                    }
                    output.AddAttribute("id", strLeftContainer);
                }
                output.AddAttribute("align", "left");
                if (this.AllowTitleRowExpandCollapse && this.Collapsable)
                {
                    if ((this.ExpandImageUrl.Length == 0) || (this.CollapseImageUrl.Length == 0))
                    {
                        output.AddAttribute("onclick", "javascript:CollapsablePanel" + strBrowserType + "_ExpandCollapse('" + this.UniqueID + "_contentRow','" + this.m_actionLink.ClientID + "','" + this.m_currentState.ClientID + "','" + this.ExpandText + "','" + this.CollapseText + "'," + this.AllowSliding.ToString().ToLower() + ", " + this.UniqueID.Replace(":", "_") + "_Height, " + this.SlideSpeed.ToString() + ", " + this.SlideLines.ToString() + ", '" + strContainers + "', " + flag2.ToString().ToLower() + ", " + flag3.ToString().ToLower() + ", '" + text2 + "', '" + text3 + "', '" + this.ClientID + "','','" + this.JavascriptOnToggleFunction + "'); " + this.JavascriptOnClickEndFunction + "; ");
                    }
                    else
                    {
                        output.AddAttribute("onclick", "javascript:CollapsablePanel" + strBrowserType + "_ExpandCollapseImage('" + this.UniqueID + "_contentRow','" + this.m_actionImage.ClientID + "','" + this.m_currentState.ClientID + "','" + base.ResolveUrl(this.ExpandImageUrl) + "','" + base.ResolveUrl(this.CollapseImageUrl) + "','" + this.ExpandText + "','" + this.CollapseText + "'," + this.AllowSliding.ToString().ToLower() + ", " + this.UniqueID.Replace(":", "_") + "_Height, " + this.SlideSpeed.ToString() + ", " + this.SlideLines.ToString() + ", '" + strContainers + "', " + flag2.ToString().ToLower() + ", " + flag3.ToString().ToLower() + ", '" + text2 + "', '" + text3 + "', '" + this.ClientID + "','', '" + this.JavascriptOnToggleFunction + "'); " + this.JavascriptOnClickEndFunction + "; ");
                    }
                }
                output.AddAttribute(HtmlTextWriterAttribute.Valign, this.TitleVerticalAlignment.ToString());
                output.RenderBeginTag("td");
                if (this.AllowTitleExpandCollapse && this.Collapsable)
                {
                    if (!this.AllowTitleRowExpandCollapse)
                    {
                        if ((this.ExpandImageUrl.Length == 0) || (this.CollapseImageUrl.Length == 0))
                        {
                            this.m_titleLink.Attributes.Add("onclick", "javascript:CollapsablePanel" + strBrowserType + "_ExpandCollapse('" + this.UniqueID + "_contentRow','" + this.m_actionLink.ClientID + "','" + this.m_currentState.ClientID + "','" + this.ExpandText + "','" + this.CollapseText + "'," + this.AllowSliding.ToString().ToLower() + ", " + this.UniqueID.Replace(":", "_") + "_Height, " + this.SlideSpeed.ToString() + ", " + this.SlideLines.ToString() + ", '" + strContainers + "', " + flag2.ToString().ToLower() + ", " + flag3.ToString().ToLower() + ", '" + text2 + "', '" + text3 + "', '" + this.ClientID + "','', '" + this.JavascriptOnToggleFunction + "'); " + this.JavascriptOnClickEndFunction + "; ");
                        }
                        else
                        {
                            this.m_titleLink.Attributes.Add("onclick", "javascript:CollapsablePanel" + strBrowserType + "_ExpandCollapseImage('" + this.UniqueID + "_contentRow','" + this.m_actionImage.ClientID + "','" + this.m_currentState.ClientID + "','" + base.ResolveUrl(this.ExpandImageUrl) + "','" + base.ResolveUrl(this.CollapseImageUrl) + "','" + this.ExpandText + "','" + this.CollapseText + "'," + this.AllowSliding.ToString().ToLower() + ", " + this.UniqueID.Replace(":", "_") + "_Height, " + this.SlideSpeed.ToString() + ", " + this.SlideLines.ToString() + ", '" + strContainers + "', " + flag2.ToString().ToLower() + ", " + flag3.ToString().ToLower() + ", '" + text2 + "', '" + text3 + "', '" + this.ClientID + "','', '" + this.JavascriptOnToggleFunction + "'); " + this.JavascriptOnClickEndFunction + "; ");
                        }
                    }
                    else
                    {
                        this.m_titleLink.Attributes.Add("onclick", "javascript:CollapsablePanel" + strBrowserType + "_DoNothing(); " + this.JavascriptOnClickEndFunction + "; ");
                    }
                    if (this.TitleStyleContainerMode == TitleStyleContainer.Normal)
                    {
                        this.m_titleLink.MergeStyle(this.TitleLinkStyle);
                    }
                    this.m_titleLink.Text = this.TitleText;
                    this.m_titleLink.RenderControl(output);
                }
                else
                {
                    output.Write(this.TitleText);
                }
                output.RenderEndTag();

                if (this.Collapsable)
                {
                    if (this.TitleStyleContainerMode == TitleStyleContainer.Normal)
                    {
                        if (flag1)
                        {
                            output.AddAttribute("class", text1);
                        }
                        else if (text1.Length != 0)
                        {
                            output.AddAttribute("style", text1);
                        }
                        output.AddAttribute("id", strRightContainer);
                    }
                    output.AddAttribute("align", "right");
                    output.AddAttribute("width", "0%");
                    output.AddAttribute("nowrap", "nowrap");
                    if (this.AllowTitleRowExpandCollapse && this.Collapsable)
                    {
                        if ((this.ExpandImageUrl.Length == 0) || (this.CollapseImageUrl.Length == 0))
                        {
                            output.AddAttribute("onclick", "javascript:CollapsablePanel" + strBrowserType + "_ExpandCollapse('" + this.UniqueID + "_contentRow','" + this.m_actionLink.ClientID + "','" + this.m_currentState.ClientID + "','" + this.ExpandText + "','" + this.CollapseText + "'," + this.AllowSliding.ToString().ToLower() + ", " + this.UniqueID.Replace(":", "_") + "_Height, " + this.SlideSpeed.ToString() + ", " + this.SlideLines.ToString() + ", '" + strContainers + "', " + flag2.ToString().ToLower() + ", " + flag3.ToString().ToLower() + ", '" + text2 + "', '" + text3 + "', '" + this.ClientID + "','', '" + this.JavascriptOnToggleFunction + "'); " + this.JavascriptOnClickEndFunction + "; ");
                        }
                        else
                        {
                            output.AddAttribute("onclick", "javascript:CollapsablePanel" + strBrowserType + "_ExpandCollapseImage('" + this.UniqueID + "_contentRow','" + this.m_actionImage.ClientID + "','" + this.m_currentState.ClientID + "','" + base.ResolveUrl(this.ExpandImageUrl) + "','" + base.ResolveUrl(this.CollapseImageUrl) + "','" + this.ExpandText + "','" + this.CollapseText + "'," + this.AllowSliding.ToString().ToLower() + ", " + this.UniqueID.Replace(":", "_") + "_Height, " + this.SlideSpeed.ToString() + ", " + this.SlideLines.ToString() + ", '" + strContainers + "', " + flag2.ToString().ToLower() + ", " + flag3.ToString().ToLower() + ", '" + text2 + "', '" + text3 + "', '" + this.ClientID + "','', '" + this.JavascriptOnToggleFunction + "'); " + this.JavascriptOnClickEndFunction + "; ");
                        }
                    }
                    output.AddAttribute(HtmlTextWriterAttribute.Valign, this.TitleVerticalAlignment.ToString());
                    output.RenderBeginTag("td");
                    if (this.ShowLinkOrImage)
                    {
                        if ((this.ExpandImageUrl.Length == 0) || (this.CollapseImageUrl.Length == 0))
                        {
                            if (this.TitleStyleContainerMode == TitleStyleContainer.Normal)
                            {
                                if (this.Collapsed && this.Collapsable)
                                {
                                    this.m_actionLink.ApplyStyle(this.CollapsedTitleStyle);
                                }
                                else
                                {
                                    this.m_actionLink.ApplyStyle(this.TitleStyle);
                                }
                            }
                            if (!this.AllowTitleRowExpandCollapse)
                            {
                                this.m_actionLink.Attributes.Add("onclick", "javascript:CollapsablePanel" + strBrowserType + "_ExpandCollapse('" + this.UniqueID + "_contentRow','" + this.m_actionLink.ClientID + "','" + this.m_currentState.ClientID + "','" + this.ExpandText + "','" + this.CollapseText + "'," + this.AllowSliding.ToString().ToLower() + ", " + this.UniqueID.Replace(":", "_") + "_Height, " + this.SlideSpeed.ToString() + ", " + this.SlideLines.ToString() + ", '" + strContainers + "', " + flag2.ToString().ToLower() + ", " + flag3.ToString().ToLower() + ", '" + text2 + "', '" + text3 + "', '" + this.ClientID + "','', '" + this.JavascriptOnToggleFunction + "'); " + this.JavascriptOnClickEndFunction + "; ");
                            }
                            else
                            {
                                this.m_actionLink.Attributes.Add("onclick", "javascript:CollapsablePanel" + strBrowserType + "_DoNothing(); " + this.JavascriptOnClickEndFunction + "; ");
                            }
                            if (this.Collapsed)
                            {
                                this.m_actionLink.Text = this.ExpandText;
                            }
                            else
                            {
                                this.m_actionLink.Text = this.CollapseText;
                            }
                            this.m_actionLink.RenderControl(output);
                        }
                        else
                        {
                            if (this.TitleStyleContainerMode == TitleStyleContainer.Normal)
                            {
                                if (this.Collapsed && this.Collapsable)
                                {
                                    this.m_actionLink.ApplyStyle(this.CollapsedTitleStyle);
                                }
                                else
                                {
                                    this.m_actionLink.ApplyStyle(this.TitleStyle);
                                }
                            }
                            this.m_actionLink.Controls.Add(this.m_actionImage);
                            if (!this.AllowTitleRowExpandCollapse)
                            {
                                this.m_actionLink.Attributes.Add("onclick",  "javascript:CollapsablePanel" + strBrowserType + "_ExpandCollapseImage('" + this.UniqueID + "_contentRow','" + this.m_actionImage.ClientID + "','" + this.m_currentState.ClientID + "','" + base.ResolveUrl(this.ExpandImageUrl) + "','" + base.ResolveUrl(this.CollapseImageUrl) + "','" + this.ExpandText + "','" + this.CollapseText + "'," + this.AllowSliding.ToString().ToLower() + ", " + this.UniqueID.Replace(":", "_") + "_Height, " + this.SlideSpeed.ToString() + ", " + this.SlideLines.ToString() + ", '" + strContainers + "', " + flag2.ToString().ToLower() + ", " + flag3.ToString().ToLower() + ", '" + text2 + "', '" + text3 + "', '" + this.ClientID + "','', '" + this.JavascriptOnToggleFunction + "'); " + this.JavascriptOnClickEndFunction + "; ");
                            }
                            else
                            {
                                this.m_actionLink.Attributes.Add("onclick", "javascript:CollapsablePanel" + strBrowserType + "_DoNothing(); " + this.JavascriptOnClickEndFunction + "; ");
                            }
                            if (this.Collapsed)
                            {
                                this.m_actionLink.ToolTip = this.ExpandText;
                                this.m_actionImage.ImageUrl = base.ResolveUrl(this.ExpandImageUrl);
                            }
                            else
                            {
                                this.m_actionLink.ToolTip = this.CollapseText;
                                this.m_actionImage.ImageUrl = base.ResolveUrl(this.CollapseImageUrl);
                            }
                            this.m_actionLink.RenderControl(output);
                        }
                    }
                    output.RenderEndTag();
                }
                output.RenderEndTag();
            }
            else
            {
                if (this.Collapsable)
                {
                    if (this.TitleStyleContainerMode == TitleStyleContainer.Normal)
                    {
                        if (flag1)
                        {
                            output.AddAttribute("class", text1);
                        }
                        else if (text1.Length != 0)
                        {
                            output.AddAttribute("style", text1);
                        }
                        output.AddAttribute("id", strLeftContainer);
                    }
                    output.AddAttribute("align", "left");
                    output.AddAttribute("width", "100%");
                    output.AddAttribute("nowrap", "nowrap");
                    if (this.AllowTitleRowExpandCollapse && this.Collapsable)
                    {
                        if ((this.ExpandImageUrl.Length == 0) || (this.CollapseImageUrl.Length == 0))
                        {
                            output.AddAttribute("onclick", "javascript:CollapsablePanel" + strBrowserType + "_ExpandCollapse('" + this.UniqueID + "_contentRow','" + this.m_actionLink.ClientID + "','" + this.m_currentState.ClientID + "','" + this.ExpandText + "','" + this.CollapseText + "'," + this.AllowSliding.ToString().ToLower() + ", " + this.UniqueID.Replace(":", "_") + "_Height, " + this.SlideSpeed.ToString() + ", " + this.SlideLines.ToString() + ", '" + strContainers + "', " + flag2.ToString().ToLower() + ", " + flag3.ToString().ToLower() + ", '" + text2 + "', '" + text3 + "', '" + this.ClientID + "','', '" + this.JavascriptOnToggleFunction + "'); " + this.JavascriptOnClickEndFunction + "; ");
                        }
                        else
                        {
                            output.AddAttribute("onclick", "javascript:CollapsablePanel" + strBrowserType + "_ExpandCollapseImage('" + this.UniqueID + "_contentRow','" + this.m_actionImage.ClientID + "','" + this.m_currentState.ClientID + "','" + base.ResolveUrl(this.ExpandImageUrl) + "','" + base.ResolveUrl(this.CollapseImageUrl) + "','" + this.ExpandText + "','" + this.CollapseText + "'," + this.AllowSliding.ToString().ToLower() + ", " + this.UniqueID.Replace(":", "_") + "_Height, " + this.SlideSpeed.ToString() + ", " + this.SlideLines.ToString() + ", '" + strContainers + "', " + flag2.ToString().ToLower() + ", " + flag3.ToString().ToLower() + ", '" + text2 + "', '" + text3 + "', '" + this.ClientID + "','', '" + this.JavascriptOnToggleFunction + "'); " + this.JavascriptOnClickEndFunction + "; ");
                        }
                    }
                    output.AddAttribute(HtmlTextWriterAttribute.Valign, this.TitleVerticalAlignment.ToString());
                    output.RenderBeginTag("td");
                    if (this.ShowLinkOrImage)
                    {
                        if ((this.ExpandImageUrl.Length == 0) || (this.CollapseImageUrl.Length == 0))
                        {
                            if (this.TitleStyleContainerMode == TitleStyleContainer.Normal)
                            {
                                if (this.Collapsed && this.Collapsable)
                                {
                                    this.m_actionLink.ApplyStyle(this.CollapsedTitleStyle);
                                }
                                else
                                {
                                    this.m_actionLink.ApplyStyle(this.TitleStyle);
                                }
                            }
                            if (!this.AllowTitleRowExpandCollapse)
                            {
                                this.m_actionLink.Attributes.Add("onclick", "javascript:CollapsablePanel" + strBrowserType + "_ExpandCollapse('" + this.UniqueID + "_contentRow','" + this.m_actionLink.ClientID + "','" + this.m_currentState.ClientID + "','" + this.ExpandText + "','" + this.CollapseText + "'," + this.AllowSliding.ToString().ToLower() + ", " + this.UniqueID.Replace(":", "_") + "_Height, " + this.SlideSpeed.ToString() + ", " + this.SlideLines.ToString() + ", '" + strContainers + "', " + flag2.ToString().ToLower() + ", " + flag3.ToString().ToLower() + ", '" + text2 + "', '" + text3 + "', '" + this.ClientID + "','', '" + this.JavascriptOnToggleFunction + "'); " + this.JavascriptOnClickEndFunction + "; ");
                            }
                            else
                            {
                                this.m_actionLink.Attributes.Add("onclick", "javascript:CollapsablePanel" + strBrowserType + "_DoNothing(); " + this.JavascriptOnClickEndFunction + "; ");
                            }
                            if (this.Collapsed)
                            {
                                this.m_actionLink.Text = this.ExpandText;
                            }
                            else
                            {
                                this.m_actionLink.Text = this.CollapseText;
                            }
                            this.m_actionLink.RenderControl(output);
                            output.Write(this.TitleSpacer);
                        }
                        else
                        {
                            if (this.TitleStyleContainerMode == TitleStyleContainer.Normal)
                            {
                                if (this.Collapsed && this.Collapsable)
                                {
                                    this.m_actionLink.ApplyStyle(this.CollapsedTitleStyle);
                                }
                                else
                                {
                                    this.m_actionLink.ApplyStyle(this.TitleStyle);
                                }
                            }
                            this.m_actionLink.Controls.Add(this.m_actionImage);
                            if (!this.AllowTitleRowExpandCollapse)
                            {
                                this.m_actionLink.Attributes.Add("onclick", "javascript:CollapsablePanel" + strBrowserType + "_ExpandCollapseImage('" + this.UniqueID + "_contentRow','" + this.m_actionImage.ClientID + "','" + this.m_currentState.ClientID + "','" + base.ResolveUrl(this.ExpandImageUrl) + "','" + base.ResolveUrl(this.CollapseImageUrl) + "','" + this.ExpandText + "','" + this.CollapseText + "'," + this.AllowSliding.ToString().ToLower() + ", " + this.UniqueID.Replace(":", "_") + "_Height, " + this.SlideSpeed.ToString() + ", " + this.SlideLines.ToString() + ", '" + strContainers + "', " + flag2.ToString().ToLower() + ", " + flag3.ToString().ToLower() + ", '" + text2 + "', '" + text3 + "', '" + this.ClientID + "','', '" + this.JavascriptOnToggleFunction + "'); " + this.JavascriptOnClickEndFunction + "; ");
                            }
                            else
                            {
                                this.m_actionLink.Attributes.Add("onclick", "javascript:CollapsablePanel" + strBrowserType + "_DoNothing(); " + this.JavascriptOnClickEndFunction + "; ");
                            }
                            if (this.Collapsed)
                            {
                                this.m_actionLink.ToolTip = this.ExpandText;
                                this.m_actionImage.ImageUrl = base.ResolveUrl(this.ExpandImageUrl);
                            }
                            else
                            {
                                this.m_actionLink.ToolTip = this.CollapseText;
                                this.m_actionImage.ImageUrl = base.ResolveUrl(this.CollapseImageUrl);
                            }
                            this.m_actionLink.RenderControl(output);
                            output.Write(this.TitleSpacer);
                        }
                    }
                }
                if (this.AllowTitleExpandCollapse && this.Collapsable)
                {
                    if (!this.AllowTitleRowExpandCollapse)
                    {
                        if ((this.ExpandImageUrl.Length == 0) || (this.CollapseImageUrl.Length == 0))
                        {
                            this.m_titleLink.Attributes.Add("onclick", "javascript:CollapsablePanel" + strBrowserType + "_ExpandCollapse('" + this.UniqueID + "_contentRow','" + this.m_actionLink.ClientID + "','" + this.m_currentState.ClientID + "','" + this.ExpandText + "','" + this.CollapseText + "'," + this.AllowSliding.ToString().ToLower() + ", " + this.UniqueID.Replace(":", "_") + "_Height, " + this.SlideSpeed.ToString() + ", " + this.SlideLines.ToString() + ", '" + strContainers + "', " + flag2.ToString().ToLower() + ", " + flag3.ToString().ToLower() + ", '" + text2 + "', '" + text3 + "', '" + this.ClientID + "','', '" + this.JavascriptOnToggleFunction + "'); " + this.JavascriptOnClickEndFunction + "; ");
                        }
                        else
                        {
                            this.m_titleLink.Attributes.Add("onclick", "javascript:CollapsablePanel" + strBrowserType + "_ExpandCollapseImage('" + this.UniqueID + "_contentRow','" + this.m_actionImage.ClientID + "','" + this.m_currentState.ClientID + "','" + base.ResolveUrl(this.ExpandImageUrl) + "','" + base.ResolveUrl(this.CollapseImageUrl) + "','" + this.ExpandText + "','" + this.CollapseText + "'," + this.AllowSliding.ToString().ToLower() + ", " + this.UniqueID.Replace(":", "_") + "_Height, " + this.SlideSpeed.ToString() + ", " + this.SlideLines.ToString() + ", '" + strContainers + "', " + flag2.ToString().ToLower() + ", " + flag3.ToString().ToLower() + ", '" + text2 + "', '" + text3 + "', '" + this.ClientID + "','', '" + this.JavascriptOnToggleFunction + "'); " + this.JavascriptOnClickEndFunction + "; ");
                        }
                    }
                    else
                    {
                        this.m_titleLink.Attributes.Add("onclick", "javascript:CollapsablePanel" + strBrowserType + "_DoNothing(); " + this.JavascriptOnClickEndFunction + "; ");
                    }
                    if (this.TitleStyleContainerMode == TitleStyleContainer.Normal)
                    {
                        this.m_titleLink.MergeStyle(this.TitleLinkStyle);
                    }
                    this.m_titleLink.Text = this.TitleText;
                    this.m_titleLink.RenderControl(output);
                }
                else
                {
                    output.Write(this.TitleText);
                }
                output.RenderEndTag();
                output.RenderEndTag();
            }
            if (this.TitleStyleContainerMode == TitleStyleContainer.TitleOnly)
            {
                output.RenderEndTag();
                output.RenderEndTag();
                output.RenderEndTag();
            }
            output.AddAttribute("id", this.UniqueID + "_contentRow");
            output.RenderBeginTag("tr");
            output.AddAttribute("align", "left");
            if (this.TitleStyleContainerMode != TitleStyleContainer.TitleOnly)
            {
                output.AddAttribute("colspan", "2");
            }
            output.RenderBeginTag("td");
            base.Render(output);
            output.RenderEndTag();
            output.RenderEndTag();
            output.RenderEndTag();
            if (this.Collapsable)
            {
                this.m_currentState.RenderControl(output);
            }

        }

        protected override object SaveViewState()
        {
            return new object[] { base.SaveViewState(), ((IStateManager)this.TitleLinkStyle).SaveViewState(), ((IStateManager)this.TitleStyle).SaveViewState(), ((IStateManager)this.CollapsedTitleStyle).SaveViewState() };
        }

        protected string TableStyle(Style inStyle, ref bool isCssClass)
        {
            TableCell cell1 = new TableCell();
            StringWriter writer1 = new StringWriter();
            HtmlTextWriter writer2 = new HtmlTextWriter(writer1);
            if (this.TitleStyleContainerMode == TitleStyleContainer.Normal)
            {
                cell1.MergeStyle(inStyle);
            }
            cell1.RenderControl(writer2);
            string text1 = writer1.ToString();
            int num1 = -1;
            int num2 = -1;
            if (inStyle.CssClass.Length != 0)
            {
                isCssClass = true;
                return inStyle.CssClass;
            }
            num1 = text1.IndexOf("style=\"", 0);
            if (num1 > 0)
            {
                num2 = text1.IndexOf("\"", num1 + 7);
                return text1.Substring(num1 + 7, num2 - (num1 + 7)).Replace("\"", "'");
            }
            return "";
        }

        protected override void TrackViewState()
        {
            base.TrackViewState();
            ((IStateManager)this.TitleLinkStyle).TrackViewState();
            ((IStateManager)this.TitleStyle).TrackViewState();
            ((IStateManager)this.CollapsedTitleStyle).TrackViewState();
        }


        [Category("Behavior"), Bindable(false), Description("Indicates if the panels will slide upon expand or collapse in Internet Explorer."), DefaultValue(false)]
        public virtual bool AllowSliding
        {
            get
            {
                object obj1 = this.ViewState["AllowSliding"];
                if (obj1 != null)
                {
                    return (bool)obj1;
                }
                return false;
            }
            set
            {
                this.ViewState["AllowSliding"] = value;
            }
        }

        [Bindable(true), DefaultValue(false), Category("Appearance")]
        public bool AllowTitleExpandCollapse
        {
            get
            {
                if (this.ViewState["AllowTitleExpandCollapse"] != null)
                {
                    return (bool)this.ViewState["AllowTitleExpandCollapse"];
                }
                return false;
            }
            set
            {
                this.ViewState["AllowTitleExpandCollapse"] = value;
            }
        }

        [DefaultValue(false), Category("Appearance"), Bindable(true)]
        public bool AllowTitleRowExpandCollapse
        {
            get
            {
                if (this.ViewState["AllowTitleRowExpandCollapse"] != null)
                {
                    return (bool)this.ViewState["AllowTitleRowExpandCollapse"];
                }
                return false;
            }
            set
            {
                this.ViewState["AllowTitleRowExpandCollapse"] = value;
            }
        }

        [Bindable(true), Category("Behavior"), DefaultValue(false)]
        public bool Collapsable
        {
            get
            {
                if (this.ViewState["Collapsable"] != null)
                {
                    return (bool)this.ViewState["Collapsable"];
                }
                this.ViewState["Collapsable"] = false;
                return false;
            }
            set
            {
                if (!value)
                {
                    this.Collapsed = false;
                    this.AllowTitleExpandCollapse = false;
                }
                this.ViewState["Collapsable"] = value;
            }
        }

        [Bindable(true), Category("Behavior"), DefaultValue(false)]
        public bool Collapsed
        {
            get
            {
                if (this.ViewState["Collapsed"] != null)
                {
                    return (bool)this.ViewState["Collapsed"];
                }
                return false;
            }
            set
            {
                this.ViewState["Collapsed"] = value;
            }
        }

        [Bindable(false), PersistenceMode(PersistenceMode.Attribute), Category("Style"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true)]
        public Style CollapsedTitleStyle
        {
            get
            {
                if (this.m_collapsedTitleStyle == null)
                {
                    this.m_collapsedTitleStyle = new Style();
                    this.m_collapsedTitleStyle.CopyFrom(this.TitleStyle);
                }
                return this.m_collapsedTitleStyle;
            }
            set
            {
                this.m_collapsedTitleStyle = value;
            }
        }

        [DefaultValue(""), Bindable(true), Category("Appearance"), Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
        public string CollapseImageUrl
        {
            get
            {
                if (this.ViewState["CollapseImageUrl"] != null)
                {
                    return (string)this.ViewState["CollapseImageUrl"];
                }
                return "";
            }
            set
            {
                this.ViewState["CollapseImageUrl"] = value;
            }
        }


        [Category("Appearance"), Bindable(true), DefaultValue(1)]
        public HorizontalAlignment CollapserAlign
        {
            get
            {
                if (this.ViewState["CollapserAlign"] != null)
                {
                    return (HorizontalAlignment)this.ViewState["CollapserAlign"];
                }
                return HorizontalAlignment.Right;
            }
            set
            {
                this.ViewState["CollapserAlign"] = value;
            }
        }

        [Category("Appearance"), DefaultValue("Collapse"), Bindable(true)]
        public string CollapseText
        {
            get
            {
                if (this.ViewState["CollapseText"] != null)
                {
                    return (string)this.ViewState["CollapseText"];
                }
                return "Collapse";
            }
            set
            {
                this.ViewState["CollapseText"] = value;
            }
        }

        [DefaultValue(""), Editor(typeof(ImageUrlEditor), typeof(UITypeEditor)), Category("Appearance"), Bindable(true)]
        public string ExpandImageUrl
        {
            get
            {
                if (this.ViewState["ExpandImageUrl"] != null)
                {
                    return (string)this.ViewState["ExpandImageUrl"];
                }
                return "";
            }
            set
            {
                this.ViewState["ExpandImageUrl"] = value;
            }
        }

        [Bindable(true), DefaultValue("Expand"), Category("Appearance")]
        public string ExpandText
        {
            get
            {
                if (this.ViewState["ExpandText"] != null)
                {
                    return (string)this.ViewState["ExpandText"];
                }
                return "Expand";
            }
            set
            {
                this.ViewState["ExpandText"] = value;
            }
        }

        [Category("Appearance"), Description("The path to the external resource javascript file."), Bindable(false), DefaultValue("")]
        public virtual string ExternalResourcePath
        {
            get
            {
                if (this.ViewState["ExternalResourcePath"] != null)
                {
                    return (string)this.ViewState["ExternalResourcePath"];
                }
                return "";
            }
            set
            {
                this.ViewState["ExternalResourcePath"] = value;
            }
        }

        [DefaultValue(""), Category("Behavior"), Description("Javascript function to call whenever it is toggled. See documentation for more information."), Bindable(false)]
        public virtual string JavascriptOnToggleFunction
        {
            get
            {
                object obj1 = this.ViewState["JavascriptOnToggleFunction"];
                if (obj1 != null)
                {
                    return (string)obj1;
                }
                return "";
            }
            set
            {
                this.ViewState["JavascriptOnToggleFunction"] = value;
            }
        }



        [DefaultValue(""), Category("Behavior"), Description("Javascript function to call whenever a Link inside the Panel get clicked (After the toggle) ."), Bindable(false)]
        public virtual string JavascriptOnClickEndFunction
        {
            get
            {
                object obj1 = this.ViewState["JavascriptOnClickEndFunction"];
                if (obj1 != null)
                {
                    return (string)obj1;
                }
                return "";
            }
            set
            {
                this.ViewState["JavascriptOnClickEndFunction"] = value;
            }
        }


        [Bindable(true), Category("Appearance")]
        public bool ShowLinkOrImage
        {
            get
            {
                if (this.ViewState["ShowLinkOrImage"] != null)
                {
                    return (bool)this.ViewState["ShowLinkOrImage"];
                }
                if (this.AllowTitleExpandCollapse)
                {
                    return false;
                }
                return true;
            }
            set
            {
                if (!value && this.Collapsable)
                {
                    this.AllowTitleExpandCollapse = true;
                }
                this.ViewState["ShowLinkOrImage"] = value;
            }
        }

        [Category("Behavior"), DefaultValue(10), Description("Indicates the number of pixel lines that are increased/decreased when a panel is sliding."), Bindable(false)]
        public virtual int SlideLines
        {
            get
            {
                object obj1 = this.ViewState["SlideLines"];
                if (obj1 != null)
                {
                    return (int)obj1;
                }
                return 10;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Value for SlideLines must be >= 0");
                }
                this.ViewState["SlideLines"] = value;
            }
        }

        [Bindable(false), Description("Indicates the speed of the panel sliding."), Category("Behavior"), DefaultValue(20)]
        public virtual int SlideSpeed
        {
            get
            {
                object obj1 = this.ViewState["SlideSpeed"];
                if (obj1 != null)
                {
                    return (int)obj1;
                }
                return 20;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Value for SlideSpeed must be >= 0");
                }
                this.ViewState["SlideSpeed"] = value;
            }
        }

        [PersistenceMode(PersistenceMode.Attribute), NotifyParentProperty(true), Bindable(false), Category("Style"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Style TitleLinkStyle
        {
            get
            {
                if (this.m_titleLinkStyle == null)
                {
                    this.m_titleLinkStyle = new Style();
                }
                return this.m_titleLinkStyle;
            }
            set
            {
                this.m_titleLinkStyle = value;
            }
        }

        [Category("Appearance"), Bindable(true), DefaultValue("&nbsp;"), Description("The spacer that is applied when CollapserAlign=Left.")]
        public virtual string TitleSpacer
        {
            get
            {
                object obj1 = this.ViewState["TitleSpacer"];
                if (obj1 != null)
                {
                    return (string)obj1;
                }
                return "&nbsp;";
            }
            set
            {
                this.ViewState["TitleSpacer"] = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Bindable(false), NotifyParentProperty(true), PersistenceMode(PersistenceMode.Attribute), Category("Style")]
        public Style TitleStyle
        {
            get
            {
                if (this.m_titleStyle == null)
                {
                    this.m_titleStyle = new Style();
                }
                return this.m_titleStyle;
            }
            set
            {
                this.m_titleStyle = value;
            }
        }

        [Category("Appearance"), DefaultValue(0), Bindable(false)]
        public TitleStyleContainer TitleStyleContainerMode
        {
            get
            {
                object obj1 = this.ViewState["TitleStyleContainerMode"];
                if (obj1 != null)
                {
                    return (TitleStyleContainer)obj1;
                }
                return TitleStyleContainer.Normal;
            }
            set
            {
                this.ViewState["TitleStyleContainerMode"] = value;
            }
        }

        [DefaultValue(""), Bindable(true), Category("Appearance")]
        public string TitleText
        {
            get
            {
                if (this.ViewState["TitleText"] != null)
                {
                    return (string)this.ViewState["TitleText"];
                }
                return "";
            }
            set
            {
                this.ViewState["TitleText"] = value;
            }
        }

        [Bindable(false)]
        public string ToggleJavascriptFunction
        {
            get
            {
                return this.ClientID + "_CollapsablePanel_Toggle()";
            }
           
        }


        [Bindable(false)]
        public string ExpandJavascriptFunction
        {
            get
            {
                return this.ClientID + "_CollapsablePanel_Expand()";
            }

        }

        [Bindable(false)]
        public string CollapseJavascriptFunction
        {
            get
            {
                return this.ClientID + "_CollapsablePanel_Collapse()";
            }

        }


        [Description("The vertical alignment for the title text cell."), Category("Appearance"), DefaultValue(2), Bindable(false)]
        public virtual VerticalAlign TitleVerticalAlignment
        {
            get
            {
                object obj1 = this.ViewState["TitleVerticalAlignment"];
                if (obj1 != null)
                {
                    return (VerticalAlign)obj1;
                }
                return VerticalAlign.Middle;
            }
            set
            {
                this.ViewState["TitleVerticalAlignment"] = value;
            }
        }

        [Bindable(false), Description("Indicates if the external resource javascript file is used instead of inline Javascript."), Category("Appearance"), DefaultValue(false)]
        public virtual bool UseExternalResource
        {
            get
            {
                object obj1 = this.ViewState["UseExternalResource"];
                if (obj1 != null)
                {
                    return (bool)obj1;
                }
                return false;
            }
            set
            {
                this.ViewState["UseExternalResource"] = value;
            }
        }


        //public override Unit Width
        //{
        //    get
        //    {
        //        return this.m_width;
        //    }
        //    set
        //    {
        //        this.m_width = value;
        //    }
        //}


        private Image m_actionImage;
        private HyperLink m_actionLink;
        private Style m_collapsedTitleStyle;
        private HtmlInputHidden m_currentState;
        //private StringBuilder m_javascript;
        private HyperLink m_titleLink;
        private Style m_titleLinkStyle;
        private Style m_titleStyle;
        //private Unit m_width;

        #region IPostBackEventHandler Members



        #endregion
    }

    public enum TitleStyleContainer
    {
        Normal,
        TitleOnly,
        EntirePanel
    }


    public enum HorizontalAlignment
    {
        Left,
        Right
    }

}


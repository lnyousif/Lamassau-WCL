using System;
using System.Collections;
using System.Reflection;
using System.Configuration;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Lamassau.Web.UI.WebControls
{
    /// <summary>
    /// Summary description for MultiselectionNode.
    /// </summary>
    [DefaultProperty("Enabled"),
    ToolboxData("<{0}:MultiselectionNode runat=server></{0}:MultiselectionNode>")]
    public class MultiSelectionNode : System.Web.UI.WebControls.WebControl,ILamassauControl, IPostBackDataHandler
    {

        public MultiSelectionNode()
        {
            this.Ctr = new Helper();
        }

        private Helper Ctr;

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


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }


        #region IPostBackDataHandler Members

        public void RaisePostDataChangedEvent()
        {
            string[] values = this.SelectedValues.Split(',');

            if (this.ListControlSourceControl == null || this.ListControlDestinationControl  == null) throw new ArgumentNullException("Could not find source or destination control in the control tree.");


            int sourceIndex;
            int destinationIndex;
            ListItem item = null;
            string removedValue;
            foreach (string value in values)
            {
                if (value.Length == 0) continue;

                //remove from destination
                if (value.StartsWith("--"))
                {
                    removedValue = value.Substring(2);
                    item = this.ListControlDestinationControl.Items.FindByValue(removedValue);
                    if (item != null)
                    {
                        destinationIndex = this.ListControlDestinationControl.Items.IndexOf(item);
                        this.ListControlDestinationControl.Items.RemoveAt(destinationIndex);
                        if (this.RemoveFromSource)
                        {
                            //throw new NotImplementedException();
                            //should add it back to source list.
                            this.AddListItem(this.ListControlSourceControl, item.Text, item.Value);
                            //this.ListControlSourceControl.Items.Add(new ListItem(item.Text, item.Value));
                        }
                    }
                }
                else
                {
                    item = this.ListControlSourceControl.Items.FindByValue(value);
                    if (item != null)
                    {
                        sourceIndex = this.ListControlSourceControl.Items.IndexOf(item);

                        this.AddListItem(this.ListControlDestinationControl, item.Text, item.Value);
                        //this.ListControlDestinationControl.Items.Add(new ListItem(item.Text, item.Value));

                        if (this.RemoveFromSource)
                            this.ListControlSourceControl.Items.RemoveAt(sourceIndex);
                    }
                }
            }

            ////this.SelectedValues = "";

        }

        private string TrimSelectedValues(string SelectedList)
        {
            string[] values = SelectedList.Split(',');
            string fixedValues = string.Empty;
            
            string strAdd = string.Empty;
            string strRemove = string.Empty;


            for (int i = 0; i < values.Length; i++)
            {
                if (!(values[i].StartsWith("--")))
                {
                    if (strAdd == "")
                    {
                        strAdd = values[i];
                    }
                    else
                    {
                        strAdd += "," + values[i];
                    }
                }
                else
                {
                    if (strRemove == "")
                    {
                        strRemove = values[i].Substring(2);
                    }
                    else
                    {
                        strRemove += "," + values[i].Substring(2);
                    }
                }
            }

            string[] strAddValues = strAdd.Split(',');
            string[] strRemoveValues = strRemove.Split(',');

            for (int i = 0; i < strAddValues.Length; i++)
            {
                bool add = true ;
                for (int j = 0; j < strRemoveValues.Length; j++)
                {
                    if (i != j)
                    {
                        if (strAddValues[i] ==strRemoveValues[j])
                        {
                            add = false;
                            strAddValues[i] = "";
                            strRemoveValues[j]="";
                        }
                    }
                }

                if (add && (strAddValues[i]!=""))
                {
                    if (fixedValues == "")
                    {
                        fixedValues = strAddValues[i];
                    }
                    else
                    {
                        fixedValues += "," + strAddValues[i];
                    }
                }
            }

            return fixedValues;

        }


        private void  AddListItem(ListControl ctrList,string itemText, string itemValue)
        {
            ListItem item = ctrList.Items.FindByValue(itemValue);
            if (item == null)
            {
                ctrList.Items.Add(new ListItem(itemText, itemValue));
            }
        }


        public bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
        {
            this.SelectedValues = postCollection[postDataKey];

            if (SelectedValues.Length == 0)
                return false;

            return true;
        }

        #endregion

        #region ValuesSeperator
        [Category("Behavior")]
        public string ValuesSeperator
        {
            get
            {
                string s = (string)ViewState["ValuesSeperator"];
                return (s == null) ? "," : s;
            }

            set
            {
                ViewState["ValuesSeperator"] = value;
            }
        }
        #endregion ValuesSeperator

        #region ValueLeftBracket
        [Category("Behavior")]
        public string ValueLeftBracket
        {
            get
            {
                string s = (string)ViewState["ValueLeftBracket"];
                return (s == null) ? String.Empty : s;
            }

            set
            {
                ViewState["ValueLeftBracket"] = value;
            }
        }
        #endregion ValueLeftBracket

        #region ValueRightBracket
        [Category("Behavior")]
        public string ValueRightBracket
        {
            get
            {
                string s = (string)ViewState["ValueRightBracket"];
                return (s == null) ? String.Empty : s;
            }

            set
            {
                ViewState["ValueRightBracket"] = value;
            }
        }
        #endregion ValueRightBracket

        #region RemoveFromSource
        [Category("Behavior")]
        public bool RemoveFromSource
        {
            get
            {
                object o = ViewState["RemoveFromSource"];
                return (o == null) ? false : (bool)o;
            }

            set
            {
                ViewState["RemoveFromSource"] = value;
            }
        }
        #endregion RemoveFromSource

        #region AddToDestination
        [Category("Behavior")]
        public bool AddToDestination
        {
            get
            {
                object o = ViewState["AddToDestination"];
                return (o == null) ? true : (bool)o;
            }

            set
            {
                ViewState["AddToDestination"] = value;
            }
        }
        #endregion AddToDestination

        #region ImageRightUrl
        [Category("Appearance")]
        public string ImageRightUrl
        {
            get
            {
                string s = (string)ViewState["ImageRightUrl"];
                return (s == null) ? "" : s;
            }
            set
            {
                ViewState["ImageRightUrl"] = value;
            }
        }
        #endregion ImageRightUrl

        #region ImageLeftUrl
        [Category("Appearance")]
        public string ImageLeftUrl
        {
            get
            {
                string s = (string)ViewState["ImageLeftUrl"];
                return (s == null) ? "" : s;
            }
            set
            {
                ViewState["ImageLeftUrl"] = value;
            }
        }
        #endregion ImageLeftUrl

        #region SelectedValues
        /// <summary>
        /// Gets or sets the values that have been moved from source control to  destination control.
        /// </summary>
        public string SelectedValues
        {
            get
            {
                string s = (string)ViewState["SelectedValues"];
                return (s == null) ? String.Empty : s;
            }
            set 
            {
                ViewState["SelectedValues"] = value;
                //RaisePostDataChangedEvent();
            }
        }
        #endregion SelectedValues

        #region ListControlSource
        [Description("ListControlSource:  Point to the ListBox that contain All the data Optional."), DefaultValue(""), Category("Behavior"), Bindable(true), TypeConverter(typeof(Lamassau.Web.UI.WebControls.ListControlIDConverter))]
        public string ListControlSource
        {
            get
            {
                string s = (string)ViewState["ListControlSource"];
                return (s == null) ? String.Empty : s;
            }
            set
            {
                ViewState["ListControlSource"] = value;
            }
        }

        private ListControl ListControlSourceControl
        {
            get
            {
                return (ListControl)this.Parent.FindControl(this.ListControlSource);
            }
        }

        #endregion ListControlSource

        #region ListControlDestination
        [Description("ListControlDestination:  Point to the ListBox that will contain the data selected. Optional."), DefaultValue(""), Category("Behavior"), Bindable(true), TypeConverter(typeof(Lamassau.Web.UI.WebControls.ListControlIDConverter))]
        public string ListControlDestination
        {
            get
            {
                string s = (string)ViewState["ListControlDestination"];
                return (s == null) ? String.Empty : s;
            }
            set
            {
                ViewState["ListControlDestination"] = value;
            }
        }


        private ListControl ListControlDestinationControl
        {
            get
            {
                return (ListControl)this.Parent.FindControl(this.ListControlDestination);
            }
        }

        #endregion ListControlDestination

        #region DestinationValues
        public string DestinationValues
        {
            get
            {
                
                string s = String.Empty;

                string[] sa = new string[this.ListControlDestinationControl.Items.Count];
                for (int i = 0; i < ListControlDestinationControl.Items.Count; i++)
                {
                    sa[i] = ValueLeftBracket + ListControlDestinationControl.Items[i].Value + ValueRightBracket;
                }

                return String.Join(ValuesSeperator, sa);


            }
        }
        #endregion DestinationValues


        /// <summary> 
        /// Render this control to the output parameter specified.
        /// </summary>
        /// <param name="output"> The HTML writer to write out to </param>
        protected override void Render(HtmlTextWriter output)
        {
            if (this.SelectedValues != "")
            {
                RaisePostDataChangedEvent();
            }


            if (!this.DesignMode)
            {

                output.RenderBeginTag("SPAN");

                output.AddAttribute("id", this.ClientID + "_imgRightall");
                output.AddAttribute("name", this.ClientID + "_imgRightall");
                output.AddAttribute("src", (1==1) ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Lamassau.Web.UI.WebControls.Resources.Images.rightall.gif") : "");
                output.AddAttribute("align", "absMiddle");

                output.AddAttribute("onclick", "javascript:MSNTransfer('" + this.ListControlSourceControl.ClientID + "','" + this.ListControlDestinationControl.ClientID + "', false, " + this.AddToDestination.ToString().ToLower() + ", " + this.RemoveFromSource.ToString().ToLower() + ", false, '" + this.UniqueID + "');");

                output.RenderBeginTag("IMG");
                output.RenderEndTag();

                output.Write("<BR>");








                output.AddAttribute("id", this.ClientID + "_imgRight");
                output.AddAttribute("name", this.ClientID + "_imgRight");
                output.AddAttribute("src", (this.ImageRightUrl == "") ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Lamassau.Web.UI.WebControls.Resources.Images.right.gif") : this.ImageRightUrl);
                output.AddAttribute("align", "absMiddle");

                output.AddAttribute("onclick", "javascript:MSNTransfer('" + this.ListControlSourceControl.ClientID + "','" + this.ListControlDestinationControl.ClientID + "', true, " + this.AddToDestination.ToString().ToLower() + ", " + this.RemoveFromSource.ToString().ToLower() + ", false, '" + this.UniqueID + "');");

                output.RenderBeginTag("IMG");
                output.RenderEndTag();

                output.Write("<BR>");

                output.AddAttribute("id", this.ClientID + "_imgLeft");
                output.AddAttribute("name", this.ClientID + "_imgLeft");
                output.AddAttribute("src", (this.ImageLeftUrl == "") ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Lamassau.Web.UI.WebControls.Resources.Images.left.gif") : this.ImageLeftUrl);
                output.AddAttribute("align", "absMiddle");


                output.AddAttribute("onclick", String.Format("javascript: MSNTransfer( '{0}', '{1}', {2}, {3}, {4}, {5}, '{6}' )",
                                this.ListControlDestinationControl.ClientID,
                                this.ListControlSourceControl.ClientID,
                                "true",
                                ((this.AddToDestination) ? "false" : "true"),
                                ((this.RemoveFromSource) ? "false" : "true"),
                                "false",
                                this.UniqueID));

                output.RenderBeginTag("IMG");
                output.RenderEndTag();






                output.Write("<BR>");

                output.AddAttribute("id", this.ClientID + "_imgLeftall");
                output.AddAttribute("name", this.ClientID + "_imgLeftall");
                output.AddAttribute("src", (1==1) ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Lamassau.Web.UI.WebControls.Resources.Images.leftall.gif") : "");
                output.AddAttribute("align", "absMiddle");


                output.AddAttribute("onclick", String.Format("javascript: MSNTransfer( '{0}', '{1}', {2}, {3}, {4}, {5}, '{6}' )",
                                this.ListControlDestinationControl.ClientID,
                                this.ListControlSourceControl.ClientID,
                                "false",
                                ((this.AddToDestination) ? "false" : "true"),
                                ((this.RemoveFromSource) ? "false" : "true"),
                                "false",
                                this.UniqueID));

                output.RenderBeginTag("IMG");
                output.RenderEndTag();




                output.RenderEndTag();
            }
            else
            {
                output.RenderBeginTag("SPAN");


                output.AddAttribute("id", this.ClientID + "_imgRightall");
                output.AddAttribute("name", this.ClientID + "_imgRightall");
                output.AddAttribute("src", (1==1) ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Lamassau.Web.UI.WebControls.Resources.Images.rightall.gif") : "");
                output.AddAttribute("align", "absMiddle");

                output.RenderBeginTag("IMG");
                output.RenderEndTag();

                output.Write("<BR>");



                output.AddAttribute("id", this.ClientID + "_imgRight");
                output.AddAttribute("name", this.ClientID + "_imgRight");
                output.AddAttribute("src", (this.ImageRightUrl == "") ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Lamassau.Web.UI.WebControls.Resources.Images.right.gif") : this.ImageRightUrl);
                output.AddAttribute("align", "absMiddle");

                output.RenderBeginTag("IMG");
                output.RenderEndTag();

                output.Write("<BR>");

                output.AddAttribute("id", this.ClientID + "_imgLeft");
                output.AddAttribute("name", this.ClientID + "_imgLeft");
                output.AddAttribute("src", (this.ImageLeftUrl == "") ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Lamassau.Web.UI.WebControls.Resources.Images.left.gif") : this.ImageLeftUrl);
                output.AddAttribute("align", "absMiddle");




                output.RenderBeginTag("IMG");
                output.RenderEndTag();


                output.Write("<BR>");

                output.AddAttribute("id", this.ClientID + "_imgLeftall");
                output.AddAttribute("name", this.ClientID + "_imgLeftall");
                output.AddAttribute("src", (1==1) ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Lamassau.Web.UI.WebControls.Resources.Images.leftall.gif") : "");
                output.AddAttribute("align", "absMiddle");




                output.RenderBeginTag("IMG");
                output.RenderEndTag();


                output.RenderEndTag();
            }


            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), this.GetType().FullName + "_Loadvalues", String.Format("javascript: MSNLoadItems( '{0}', '{1}', {2}, {3}, {4}, '{5}')",
            //                       this.ListControlDestinationControl.ClientID,
            //                       this.ListControlSourceControl.ClientID,
            //                       ((this.AddToDestination) ? "false" : "true"),
            //                       ((this.RemoveFromSource) ? "false" : "true"),
            //                       "false",
            //                       this.UniqueID), true);




        }



        protected override void OnPreRender(EventArgs e)
        {
            if (Page != null)
            {
                if (!this.Page.ClientScript.IsClientScriptBlockRegistered(this.GetType() , this.GetType().FullName + "_transfer"))
            {
                this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), this.GetType().FullName + "_transfer", this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Lamassau.Web.UI.WebControls.Resources.Scripts.multiselectionnode.js"));
            }

            this.Page.ClientScript.RegisterHiddenField(this.UniqueID , this.SelectedValues);

                Page.RegisterRequiresPostBack(this);
            }


            


            base.OnPreRender(e);

        }

    }

    /// <summary>
    /// Summary description for ListControlIDConverter.
    /// </summary>
    public class ListControlIDConverter : StringConverter
    {
        public ListControlIDConverter()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        protected virtual object[] GetControls(IContainer container)
        {
            ArrayList list1 = new ArrayList();
            foreach (IComponent component1 in container.Components)
            {
                Control control1 = component1 as Control;
                if (control1 is ListControl)
                {
                    list1.Add(string.Copy(control1.ID));
                }
            }
            list1.Sort();
            return list1.ToArray();
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            if ((context != null) && (context.Container != null))
            {
                object[] objArray1 = this.GetControls(context.Container);
                if (objArray1 != null)
                {
                    return new TypeConverter.StandardValuesCollection(objArray1);
                }
            }
            return null;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

    }

}

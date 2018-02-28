
using System;
using System.Reflection;
using System.Configuration;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web.UI.Design;
using System.Text.RegularExpressions;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Security.Permissions;
using System.IO;

namespace Lamassau.Web.UI.WebControls
{
    
    [Designer(typeof(ToolTipDesigner))]
    [ToolboxData("<{0}:ToolTip runat=server></{0}:ToolTip>")]
    [ParseChildren(true)]
    [PersistChildren(true)]
    public class ToolTip : System.Web.UI.WebControls.WebControl, IPostBackDataHandler
    {



        public ToolTip()
        {
            //
            // TODO: Add constructor logic here
            //
            _tbl = new Table();

        }


        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            TableRow row = new TableRow();

            TableCell cell = new TableCell();
            cell.ApplyStyle(this.NoteStyle);
            NoteTemplate.InstantiateIn(cell);
            row.Cells.Add(cell); 
            _tbl.Rows.Add(row);

            _tbl.ID = this.ClientID + "_NoteContainerRow";

        }



        #region IPostBackDataHandler Members

        public void RaisePostDataChangedEvent()
        {
            //			string [] values = this.SelectedValues.Split( ',' );
            //			ListControl source = ( ListControl ) Page.FindControl( this.ListControlSource );
            //			ListControl destination = ( ListControl ) Page.FindControl( this.ListControlDestination );

            //TODO: Bill here we need to make sure that the control also work with the static dropdown

            //			this.SelectedValues = "";

        }

        public bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
        {
            //			this.SelectedValues = postCollection[postDataKey];
            //			
            //			if ( SelectedValues.Length == 0 )
            //				return false;
            //
            return true;
        }

        #endregion




        #region ImageTipUrl
        [Category("Appearance"), Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Description("Panel_BackImageUrl"), Bindable(true), DefaultValue("")]
        public string ImageTipUrl
        {
            get
            {
                string s = (string)ViewState["ImageTipUrl"];
                return (s == null) ? this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Lamassau.Web.UI.WebControls.Resources.Images.details.gif") : s;
            }
            set
            {
                ViewState["ImageTipUrl"] = value;
            }
        }
        #endregion ImageTipUrl


        #region ParentControl
        [Category("Note")]
        [Editor(typeof(AllControlsEditor), typeof(UITypeEditor))]
        public string ParentControl
        {
            get
            {
                string s = (string)ViewState["ParentControl"];
                return (s == null) ? String.Empty : s;
            }
            set
            {
                ViewState["ParentControl"] = value;
            }
        }



        #endregion ParentControl

        //		#region SynchWidth 
        //		[Category("Note")]	
        //		public bool SynchWidth 
        //		{
        //			get
        //			{
        //				object o = ViewState["SynchWidth"];
        //				return ( o == null) ? false : ( bool ) o;
        //			}
        //
        //			set
        //			{
        //				ViewState["SynchWidth"] = value;
        //			}
        //		}
        //		#endregion SynchWidth 
        //
        //		#region SynchHight 
        //		[Category("Note")]	
        //		public bool SynchHight 
        //		{
        //			get
        //			{
        //				object o = ViewState["SynchHight"];
        //				return ( o == null) ? false : ( bool ) o;
        //			}
        //
        //			set
        //			{
        //				ViewState["SynchHight"] = value;
        //			}
        //		}
        //		#endregion SynchHight



        #region Note
        //[Category("Note")]
        //public string Note
        //{
        //    get
        //    {
        //        string s = (string)ViewState["Note"];
        //        return (s == null) ? String.Empty : s;
        //    }
        //    set
        //    {
        //        ViewState["Note"] = value;
        //    }
        //}


        private ITemplate _NoteTemplate;
        private TableItemStyle _NoteStyle;
        private Table _tbl;

        [Browsable(false)]
        [Description("Note Template")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [TemplateInstance(TemplateInstance.Single)]
        [EditorBrowsable(EditorBrowsableState.Always)]
       
        public ITemplate NoteTemplate
        {
            get
            {
                return _NoteTemplate;
            }
            set
            {
                _NoteTemplate = value;
            }
        }

        [Category("Styles"), Description("Note Style"), TemplateContainerAttribute(typeof(TableItemStyle)), PersistenceMode(PersistenceMode.InnerProperty), Browsable(false), DefaultValue((string)null)]
        public virtual TableItemStyle NoteStyle
        {
            get
            {
                return _NoteStyle;
            }
            set
            {
                _NoteStyle = value;
            }
        }

        #endregion Note


        [Category("Appearance"), Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=1.0.5000.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Description("Panel_BackImageUrl"), Bindable(true), DefaultValue("")]
        public virtual string BackImageUrl
        {
            get
            {
                string text1 = (string)this.ViewState["BackImageUrl"];
                if (text1 != null)
                {
                    return text1;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["BackImageUrl"] = value;
            }
        }

        // Properties
        [Category("Note")]
        public System.Web.UI.WebControls.HorizontalAlign AlignNote
        {
            get
            {
                if (this.ViewState["AlignNote"] != null)
                {
                    return (System.Web.UI.WebControls.HorizontalAlign)this.ViewState["AlignNote"];
                }
                return System.Web.UI.WebControls.HorizontalAlign.NotSet;
            }
            set
            {
                this.ViewState["AlignNote"] = value;
            }
        }

        [Category("Note")]
        public System.Web.UI.WebControls.HorizontalAlign TextAlign
        {
            get
            {
                if (this.ViewState["TextAlign"] != null)
                {
                    return (System.Web.UI.WebControls.HorizontalAlign)this.ViewState["TextAlign"];
                }
                return System.Web.UI.WebControls.HorizontalAlign.NotSet;
            }
            set
            {
                this.ViewState["TextAlign"] = value;
            }
        }

        // Properties
        [Category("Note")]
        public System.Web.UI.WebControls.VerticalAlign vAlignNote
        {
            get
            {
                if (this.ViewState["vAlignNote"] != null)
                {
                    return (System.Web.UI.WebControls.VerticalAlign)this.ViewState["vAlignNote"];
                }
                return System.Web.UI.WebControls.VerticalAlign.NotSet;
            }
            set
            {
                this.ViewState["vAlignNote"] = value;
            }
        }




        /// <summary> 
        /// Render this control to the output parameter specified.
        /// </summary>
        /// <param name="output"> The HTML writer to write out to </param>
        protected override void Render(HtmlTextWriter output)
        {
            Controls.Clear();

            if (this.ParentControl == String.Empty)
            {
                System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                img.ID = "img" + this.ClientID;
                img.ImageUrl = this.ImageTipUrl;

                img.Attributes.Add("AlignNote", this.AlignNote.ToString());
                img.Attributes.Add("vAlignNote", this.vAlignNote.ToString());
                img.Attributes.Add("NoteID", this.ClientID + "_note");
                img.Attributes.Add("onclick", "javascript:clip_note(this);");
                img.Attributes.Add("onmouseover", "javascript:show_note(this);");
                img.Attributes.Add("onmouseout", "javascript:hide_note(this);");

                img.RenderControl(output);

            }


            WebControls.Panel pnNote = new WebControls.Panel();
            pnNote.ID = this.ClientID + "_note";
            pnNote.Attributes.Add("NoteStatus", "");
            pnNote.Style.Add("DISPLAY", "none");
            pnNote.Style.Add("POSITION", "absolute");

            pnNote.ApplyStyle(this.ControlStyle);
            pnNote.CopyBaseAttributes(this);



            WebControls.Panel pnNoteClose = new WebControls.Panel();
            pnNoteClose.ID = this.ClientID + "_note_close_div";


            pnNoteClose.Style.Add("VISIBILITY", "hidden");
            pnNoteClose.Attributes.Add("align", "right");
            pnNoteClose.Controls.Add(new LiteralControl(@"<B dir=""ltr"" style=""FONT-SIZE: 10px; CURSOR: hand; COLOR: red; FONT-FAMILY: Arial, Helvetica, sans-serif"" onclick=""close_note(this)"">Close&nbsp;&nbsp;</B>"));


            WebControls.Panel pnNoteContent = new WebControls.Panel();
            pnNoteContent.ID = this.ClientID + "_note_itself";

            pnNoteContent.Style.Add("PADDING-RIGHT", "5px");
            pnNoteContent.Style.Add("PADDING-LEFT", "5px");
            pnNoteContent.Style.Add("PADDING-BOTTOM", "5px");
            pnNoteContent.Style.Add("PADDING-TOP", "5px");
            pnNoteContent.Attributes.Add("align", this.TextAlign.ToString());
            //pnNoteContent.Controls.Add(new LiteralControl(Note));

            pnNoteContent.Controls.Add(_tbl);
            
            




            //			TODO: get the Synch width & Height working
            //			if (this.ParentControl  != String.Empty)
            //			{
            //				System.Web.UI.WebControls.WebControl control = ( System.Web.UI.WebControls.WebControl ) Page.FindControl( this.ParentControl );
            //				if (this.SynchHight) 
            //				{
            //					pnNote.Height= control.Height;
            //				}
            //				if (this.SynchWidth) 
            //				{
            //					pnNote.Width= control.Width;
            //				}
            //			}

            pnNote.Controls.Add(pnNoteClose);
            pnNote.Controls.Add(pnNoteContent);

            pnNote.RenderControl(output);


        }


        /// <summary>
        /// Returns html code for designer
        /// </summary>
        internal string GetDesignCode()
        {

            StringWriter sw = new StringWriter();
            HtmlTextWriter output = new HtmlTextWriter(sw);



                System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();
                img.ID = "img" + this.ClientID;
                img.ImageUrl = this.ImageTipUrl;


                output.Write("<DIV align=left>");
                img.RenderControl(output);
                output.Write("</DIV>");
                return sw.ToString();

            //if (this.ParentControl == String.Empty)
            //{



                //img.Attributes.Add("AlignNote", this.AlignNote.ToString());
                //img.Attributes.Add("vAlignNote", this.vAlignNote.ToString());
                //img.Attributes.Add("NoteID", this.ClientID + "_note");
                //img.Attributes.Add("onclick", "javascript:clip_note(this);");
                //img.Attributes.Add("onmouseover", "javascript:show_note(this);");
                //img.Attributes.Add("onmouseout", "javascript:hide_note(this);");

            //}
            //else
            //{
            //    output.WriteLine("[Note For " + this.ParentControl + "]");
            //}




            //WebControls.Panel pnNote = new WebControls.Panel();
            //pnNote.ID = this.ClientID + "_note";

            //pnNote.ApplyStyle(this.ControlStyle);
            //pnNote.CopyBaseAttributes(this);



            //WebControls.Panel pnNoteClose = new WebControls.Panel();
            //pnNoteClose.ID = this.ClientID + "_note_close_div";
            ////			pnNoteClose.Style.Add("VISIBILITY","hidden");
            //pnNoteClose.Attributes.Add("align", "right");
            //pnNoteClose.Controls.Add(new LiteralControl(@"<B dir=""ltr"" style=""FONT-SIZE: 10px; CURSOR: hand; COLOR: red; FONT-FAMILY: Arial, Helvetica, sans-serif"" onclick=""close_note(this)"">Close&nbsp;&nbsp;</B>"));


            //WebControls.Panel pnNoteContent = new WebControls.Panel();
            //pnNoteContent.ID = this.ClientID + "_note_itself";

            //pnNoteContent.Style.Add("PADDING-RIGHT", "5px");
            //pnNoteContent.Style.Add("PADDING-LEFT", "5px");
            //pnNoteContent.Style.Add("PADDING-BOTTOM", "5px");
            //pnNoteContent.Style.Add("PADDING-TOP", "5px");
            //pnNoteContent.Attributes.Add("align", this.TextAlign.ToString());
            //pnNoteContent.Controls.Add(new LiteralControl(Note));

            //pnNote.Controls.Add(pnNoteClose);
            //pnNote.Controls.Add(pnNoteContent);

            //pnNote.RenderControl(output);
            
        }




        protected override void OnPreRender(EventArgs e)
        {
            //if (!this.Page.IsClientScriptBlockRegistered(this.GetType().FullName + "_transfer"))
            //{
            //    this.Page.RegisterClientScriptBlock(this.GetType().FullName + "_transfer", this.placeJavascript());
            //}

            if (!this.Page.ClientScript.IsClientScriptBlockRegistered("ToolTipScript"))
            {
                this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "ToolTipScript", this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Lamassau.Web.UI.WebControls.Resources.Scripts.tooltip.js"));
            }


            if (this.ParentControl != String.Empty)
            {
                //ondblclick
                System.Web.UI.WebControls.WebControl control = (System.Web.UI.WebControls.WebControl)this.Parent.FindControl(this.ParentControl);
                control.Attributes.Add("AlignNote", this.AlignNote.ToString());
                control.Attributes.Add("vAlignNote", this.vAlignNote.ToString());
                control.Attributes.Add("NoteID", this.ClientID + "_note");

                //onmouseover="show_note(this);" onclick="clip_note(this)" onmouseout="hide_note(this);"
                control.Attributes.Add("ondblclick", "javascript:clip_note(this);");
                control.Attributes.Add("onmouseover", "javascript:show_note(this);");
                control.Attributes.Add("onmouseout", "javascript:hide_note(this);");

            }


            base.OnPreRender(e);
        }



    }
    /// <summary>
    /// Editor for selecting controls from Asp.Net page
    /// </summary>
    public abstract class ControlsEditor : UITypeEditor
    {
        #region Variables

        private System.Windows.Forms.Design.IWindowsFormsEditorService edSvc = null;
        private System.Windows.Forms.ListBox lb;
        private Type typeShow;

        #endregion
        #region Constructor


        /// <summary>
        /// onstructor - show specified types
        /// </summary>
        /// <param name="show">Type descriptor</param>
        public ControlsEditor(Type show)
        {
            typeShow = show;
        }

        #endregion
        #region Methods

        /// <summary>
        /// Overrides the method used to provide basic behaviour for selecting editor.
        /// Shows our custom control for editing the value.
        /// </summary>
        /// <param name="context">The context of the editing control</param>
        /// <param name="provider">A valid service provider</param>
        /// <param name="value">The current value of the object to edit</param>
        /// <returns>The new value of the object</returns>
        public override object EditValue(ITypeDescriptorContext context,
            IServiceProvider provider, object value)
        {
            if (context != null && context.Instance != null && provider != null)
            {
                edSvc = (System.Windows.Forms.Design.IWindowsFormsEditorService)
                    provider.GetService(typeof(System.Windows.Forms.Design.IWindowsFormsEditorService));
                if (edSvc != null)
                {
                    lb = new System.Windows.Forms.ListBox();
                    lb.BorderStyle = System.Windows.Forms.BorderStyle.None;
                    lb.SelectedIndexChanged += new EventHandler(lb_SelectedIndexChanged);
                    foreach (Control ctrl in ((Control)context.Instance).Page.Controls)
                    {
                        if (ctrl.GetType().IsSubclassOf(typeShow) ||
                            ctrl.GetType().FullName == typeShow.FullName) lb.Items.Add(ctrl.ID);
                    }
                    edSvc.DropDownControl(lb);
                    if (lb.SelectedIndex == -1) return value;
                    return lb.SelectedItem;
                }
            }

            return value;
        }


        /// <summary>
        /// Choose editor type
        /// </summary>
        /// <param name="context">The context of the editing control</param>
        /// <returns>Returns <c>UITypeEditorEditStyle.DropDown</c></returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }


        /// <summary>
        /// Close the dropdowncontrol when the user has selected a value
        /// </summary>
        private void lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (edSvc != null)
            {
                edSvc.CloseDropDown();
            }
        }

        #endregion
    }


    /// <summary>
    /// Editor for selecting all Asp.Net controls
    /// </summary>
    public class AllControlsEditor : ControlsEditor
    {
        #region Members

        /// <summary>
        /// Invoke base constructor
        /// </summary>
        public AllControlsEditor() : base(typeof(Control)) { }

        #endregion
    }

    /// <summary>
    /// Class for displaying ToolTipDesigner in designer
    /// </summary>
    public class ToolTipDesigner : ControlDesigner
    {
        #region Overriden methods

        /// <summary>
        /// Returns HTML code to show in designer
        /// </summary>
        public override string GetDesignTimeHtml()
        {
            try
            {
                return ((ToolTip)Component).GetDesignCode();
                //if (((ToolTip)Component).ParentControl != "")
                //{
                //    return "[Note For " + ((ToolTip)Component).ParentControl + "]";
                //}
                //else
                //{
                //    return "[Note  " + ((ToolTip)Component).ID + "]";
                //}
            }
            catch (Exception er)
            {
                return GetErrorDesignTimeHtml(er);
            }
        }

        #endregion
    }

}

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
	/// DropDownGrid: Control that combine the look and feel of dropdown control with the many features of ASP.NET GridView;
	/// Designed by John Carter.
	/// Developed by Laith N Yousif.
	/// Date: Jan 2006.
	/// </summary>
    [ToolboxData("<{0}:DropDownGridView runat=server></{0}:DropDownGridView>"), PermissionSet(SecurityAction.Demand, Unrestricted = true)]
    [DesignerAttribute(typeof(DropDownGridViewControlDesigner))]
    [ValidationPropertyAttribute("SelectedID")]
	public class DropDownGridView: GridView, IPostBackDataHandler,ILamassauControl, INamingContainer
	{

        public delegate void GetSeletedItemTextEventHandler(object sender, GetSeletedItemTextEventArgs e);
        public event GetSeletedItemTextEventHandler GetSeletedItemText;


		#region Initialization
		
		private Label lblView ;
        private HyperLink hlnkDetails; 
		private Label lblValue;
		private Label lblDrop;
        private CallBack filterCallBack;
   

		// Methods
		public DropDownGridView()
		{
			this.lblView = new Label();
            this.hlnkDetails = new HyperLink();
            this.lblDrop = new Label();
			this.lblValue  = new Label();
            this.filterCallBack = new CallBack();
            this.filterCallBack.Enabled = false; 

		}

		#endregion

		#region Properties

        [Bindable(false), Category("Behavior"), Description("Select the behavior when you select item in the drop down grid"), DefaultValue("DefaultClientScript")]
        public SelectionBehavior SelectionBehavior
        {
            get
            {
                if (ViewState["SelectionBehavior"] == null)
                {
                    return SelectionBehavior.DefaultClientScript;
                }
                else
                {
                    switch (ViewState["SelectionBehavior"].ToString())
                    {
                        case "CustomClientScript":
                            return SelectionBehavior.CustomClientScript;

                        case "PostBack":
                            return SelectionBehavior.PostBack ;

                        case "CallBack":
                            return SelectionBehavior.CallBack ;

                        default:
                            return SelectionBehavior.DefaultClientScript ;

                    }
                }

            }
            set
            {
                ViewState["SelectionBehavior"] = value;
            }
        }



        [Bindable(false), Category("Behavior"), Description("the Client Script {Fuction Name] that will replace the default selection script, it should have the following parameter defined [(rowObj, colValue,colCode, colText,ControlID)]  "), DefaultValue("")]
        public string CustomScriptFunctionName
        {
            get
            {
                object o = ViewState["CustomScriptFunctionName"];
                return (o != null) ? o.ToString() : "";
            }
            set
            {
                ViewState["CustomScriptFunctionName"] = value;
            }
        }


        [Bindable(false), Category("Behavior"), Description("the Client Script Fuction that will run before the newly selected item get selected return true or false; true to execute the selection "), DefaultValue("")]
        public string ClientScriptBeforeSelection
        {
            get
            {
                object o = ViewState["ClientScriptBeforeSelection"];
                return (o != null) ? o.ToString() : "";
            }
            set
            {
                ViewState["ClientScriptBeforeSelection"] = value;
            }
        }

        [Bindable(false), Category("Behavior"), Description("The Client Script Fuction that will run after the newly selected item get selected "), DefaultValue("")]
        public string ClientScriptAfterSelection
        {
            get
            {
                object o = ViewState["ClientScriptAfterSelection"];
                return (o != null) ? o.ToString() : "";
            }
            set
            {
                ViewState["ClientScriptAfterSelection"] = value;
            }
        }

        [Bindable(false), Category("Behavior"), Description("the ClientID for the control that will handle the CallBack Event for the ItemSelectewd"), DefaultValue("")]
        public string SelectionCallBackHandlerControlID
        {
            get
            {
                object o = ViewState["SelectionCallBackHandlerControlID"];
                return (o != null) ? o.ToString() : "";
            }
            set
            {
                ViewState["SelectionCallBackHandlerControlID"] = value;
            }
        }

        private Control SelectionCallBackHandlerControl
        {
            get
            {
                if (this.SelectionCallBackHandlerControlID.Trim() != "")
                {
                    try
                    {
                        return this.Parent.FindControl(this.SelectionCallBackHandlerControlID);
                    }
                    catch
                    {
                        return  this.Page;
                    }
                }
                else
                {
                    return this.Page;
                }
            }
        }

        [Bindable(false), Category("Behavior"), Description("SelectionCallBackClientFunction"), DefaultValue("")]
        public string SelectionCallBackClientFunction
        {
            get
            {
                object o = ViewState["SelectionCallBackClientFunction"];
                return (o != null) ? o.ToString() : "";
            }
            set
            {
                ViewState["SelectionCallBackClientFunction"] = value;
            }
        }

        [Bindable(false), Category("Behavior"), Description("SelectionCallBackClientErrorFunction"), DefaultValue("")]
        public string SelectionCallBackClientErrorFunction
        {
            get
            {
                object o = ViewState["SelectionCallBackClientErrorFunction"];
                return (o != null) ? o.ToString() : "";
            }
            set
            {
                ViewState["SelectionCallBackClientErrorFunction"] = value;
            }
        }


        

		[Bindable(false),Category("Data"),Description("Get or Set the DataSource Field that contain the Selected ID"),DefaultValue("")]
		public string DataValueField
		{
			get 
			{
                object o = ViewState["DataValueField"];
				return (o !=null) ?  o.ToString() : "" ;
			}
			set 
			{ 
				ViewState["DataValueField"]=value;
			}
		}

        [Bindable(false), Category("Data"), Description("Get or Set the DataSource Field that contain the Selected ID"), DefaultValue("")]
        public string DataCodeField
        {
            get
            {
                object o = ViewState["DataCodeField"];
                return (o != null) ? o.ToString() : this.DataValueField;
            }
            set
            {
                ViewState["DataCodeField"] = value;
            }
        }


		/// <summary>
		/// Get or Set the Grid Column that contain the Text
		/// </summary>
		[Bindable(false),Category("Data"),Description("Get or Set the DataSource Field that contain the Selected Text"),DefaultValue("")]
		public string DataTextField
		{
			get 
			{
                object o = ViewState["DataTextField"];
				return (o!=null) ?  o.ToString() : "" ;
			}
			set 
			{ 
				ViewState["DataTextField"]= value;
			}
		}

        [Category("Layout"), Description("Drop Down Selected Text Format ")]
        public SelectedTextFormatEnum SelectedTextFormat
		{
			get 
			{
                object o = ViewState["SelectedTextFormat"];
                return (o != null) ? (o.ToString().ToLower() == "textonly") ? SelectedTextFormatEnum.TextOnly : SelectedTextFormatEnum.CodeandText : SelectedTextFormatEnum.TextOnly ;
			}
			set 
			{
                ViewState["SelectedTextFormat"] = value;
			}
		}


        [Category("Layout"), Description("Drop Down Selected Item Detailed View Link ")]
        public virtual string DetailedViewUrl
        {
            get
            {
                object o = ViewState["DetailedViewUrl"];
                return (o != null) ? o.ToString() : "";
            }
            set
            {
                ViewState["DetailedViewUrl"] = value;
            }
        }

        [Category("Layout"), Description("Drop Down Selected Item Detailed View Link Visibility")]
        public virtual bool DetailedViewUrlVisible
        {
            get
            {
                object o = ViewState["DetailedViewUrlVisible"];
                return (o != null) ? bool.Parse(o.ToString()) : true ;
            }
            set
            {
                ViewState["DetailedViewUrlVisible"] = value;
            }
        }


        [Category("Behavior"), Description("Enable CallBack Filtering")]
        public virtual bool EnableFiltering
        {
            get
            {
                object o = ViewState["EnableFiltering"];
                return (o != null) ? bool.Parse(o.ToString()) : false;
            }
            set
            {
                ViewState["EnableFiltering"] = value;
                //if (value)
                //{
                //    this.EnableSortingAndPagingCallbacks = true;
                //}
            }
        }


        public override Unit Width
        {
            get
            {
                return base.Width;
            }
            set
            {

                base.Width = value;
                this.GridWidth = Unit.Parse((this.Width.Value + Unit.Parse("6px").Value).ToString());
                this.TextBoxWidth = Unit.Parse((this.Width.Value - Unit.Parse("16px").Value).ToString());

            }
        }



		[Bindable(false),Category("Layout"),Browsable(false),Description("Get or Set the Width of the SelectionBox (16px for the dropdown click box is not included)"),DefaultValue("100px")]
		private Unit TextBoxWidth
		{
			get
			{
                object o =  ViewState["TextBoxWidth"];
				return (o!=null) ?  Unit.Parse(o.ToString()) : Unit.Parse("100px") ;
			}
			set
			{
				ViewState["TextBoxWidth"]=value;
			}
		}
		

		[Bindable(false),Category("Layout"),Description("Get or Set the Width of the DataGrid Under the Dropdown Box"),DefaultValue("116")]
		public Unit GridWidth
		{
			get
			{
                object o = ViewState["GridWidth"];
				return (o!=null) ?  Unit.Parse(o.ToString()) : Unit.Parse("100px") ;
			}
			set
			{
				ViewState["GridWidth"]=value;
			}
		}

		/// <summary>
		/// Get or Set the Grid Column that contain the ID
		/// </summary>
		[Bindable(true),Category("Behavior"),DefaultValue("")]
		public string SelectedID

		{

            get
            {
                object o = ViewState["SelectedID"];
                return (o != null) ? o.ToString() : "";
            }
            set
            {
                ViewState["SelectedID"] = value;
            }
		}


        private void SetTheSelectedItem()
        {
            int tmpSelectedRowIndex = -1;

            //Determine if the selected row is visible and re-select it
            foreach (GridViewRow row in this.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    if (row.Cells[this.Columns.Count - 3].Text == this.SelectedID)
                    {
                        tmpSelectedRowIndex = row.RowIndex;
                    }

                }
            }
            this.SelectedIndex = tmpSelectedRowIndex;

        }


		[Bindable(false),Category("Data"),Description("Get or Set the DataSource Field that contain the Selected ID"),DefaultValue("")]
		private string SelectedText

		{
            get 
            {
                object o = ViewState["SelectedText"];
                return (o!=null) ?  o.ToString() : "" ;
            }
            set 
            { 
                ViewState["SelectedText"]=value;
            }

		}
		
		
		[Bindable(false),Category("Behavior"),Description("Is DropDownList in View only mode"),DefaultValue(false)]
		private bool GridVisible
		{
			get
			{
                object o = ViewState["GridVisible"];
				return (o!=null) ?  bool.Parse(o.ToString()) : false ;
			}
			set
			{
				ViewState["GridVisible"] = value;
			}
		}


        [Browsable(false),Bindable(false), Category("Behavior"), Description("Is the GridDropDownList rendered but hidden through style visibility attribute"), DefaultValue(false)]
        public bool Hidden
        {
            get
            {
                object o = ViewState["Hidden"];
                return (o != null) ? bool.Parse(o.ToString()) : false;
            }
            set
            {
                ViewState["Hidden"] = value;
            }
        }

        public Color HoverColor
        {
            get
            {
                object o = ViewState["HoverColor"];
                return (o == null) ? Color.Transparent  : (Color) o ;
            }
            set
            {
                ViewState["HoverColor"] = value;
            }
        }


	#endregion

		#region PreRendering
		
		protected override void OnPreRender(EventArgs e)
		{
				base.OnPreRender(e);
                if (!this.DesignMode)
                {
                    if (!this.Page.ClientScript.IsClientScriptIncludeRegistered("DropDownGridScript"))
                    {
                        this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "DropDownGridScript", this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Lamassau.Web.UI.WebControls.Resources.Scripts.dropdowngrid.js"));

                    }

                    if (Page != null)
                    {
                        Page.RegisterRequiresPostBack(this);

                        SetPositionScripts();
                        SetSelectionScripts();

                        if (this.EnableFiltering)
                        {
                            SetFilteringScripts();
                        }

                    }
                }
			
		}

//        private string GetMouseOverScript()
//        {
//            string strScript = @"
//
//        function ddgvmouseover(ControlID)
//        {
//            document.getElementById(ControlID).style.background-image='url([dropgif])';
//        }
//        function ddgvmouseout(ControlID)
//        {
//            document.getElementById(ControlID).style.background-image='url([dropovergif])';
//            
//        }";
//            strScript = strScript.Replace("[dropgif]", this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Lamassau.Web.UI.WebControls.Resources.Images.drop.gif"));
//            strScript = strScript.Replace("[dropovergif]", this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Lamassau.Web.UI.WebControls.Resources.Images.dropover.gif"));
//            return strScript;
//        }

        private void SetPositionScripts()
        {
            string strStartupPositionScript = @"
                // Position " + this.ClientID + @" Correctly 
                SetGridTablePosition('" + this.ClientID + @"');
";
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), this.ClientID + "_PositionScript", strStartupPositionScript, true);

        }

        private void SetSelectionScripts()
        {
  
                string strRowSelectionScript = @"
                function [DropDownGridViewClientID]SelectRow(rowObj,rowIndex,colValue,colCode, colText,ControlID)
                {
                    var SelectedTextFormat = '[DropDownGridSelectedTextFormat]';
                    var BeforeReturnValue;
                    [BeforeDropDownGridRowSelectFunction]  
                    if (BeforeReturnValue!= false)
                    {
                        [DropDownGridRowSelectFunction]
	                }
	                [AfterDropDownGridRowSelectFunction]
                }

                ";

                
                string strSelectFunction = "NumberedSelectRow(rowObj,colValue,colCode,colText,ControlID,SelectedTextFormat);";
                
                switch (this.SelectionBehavior)
                {
                    case SelectionBehavior.PostBack:

                        strSelectFunction = @"var rowToSelect = 'Select$' + rowIndex;
                        __doPostBack('" + this.UniqueID + "',rowToSelect);";

                        break;

                    case SelectionBehavior.CallBack:

                        strSelectFunction = @"NumberedSelectRow(rowObj, colValue,colCode,colText,ControlID,SelectedTextFormat);
                                                " + this.Page.ClientScript.GetCallbackEventReference(this.SelectionCallBackHandlerControl, "rowObj.cells(colValue).innerText", SelectionCallBackClientFunction, this.SelectionCallBackClientErrorFunction, "'" + this.ClientID + "'", false) + ";";
                        break;

                    case SelectionBehavior.CustomClientScript:

                        strSelectFunction = this.CustomScriptFunctionName + "(this," + (this.Columns.Count - 3) + "," + (this.Columns.Count - 2) + "," + (this.Columns.Count - 1) + ",'" + this.ClientID + "');";

                        break;

                }

                strRowSelectionScript = strRowSelectionScript.Replace("[DropDownGridRowSelectFunction]", strSelectFunction);

                strRowSelectionScript = strRowSelectionScript.Replace("[DropDownGridViewClientID]", this.ClientID);

                if (this.ClientScriptBeforeSelection.Trim() == "")
                {
                    strRowSelectionScript = strRowSelectionScript.Replace("[BeforeDropDownGridRowSelectFunction]", "BeforeReturnValue= true;");
                }
                else
                {
                    strRowSelectionScript = strRowSelectionScript.Replace("[BeforeDropDownGridRowSelectFunction]", this.ClientScriptBeforeSelection + ";");
                }

                if (this.ClientScriptAfterSelection.Trim() != "")
                {
                    strRowSelectionScript = strRowSelectionScript.Replace("[AfterDropDownGridRowSelectFunction]", this.ClientScriptAfterSelection + ";");
                }
                else
                {
                    strRowSelectionScript = strRowSelectionScript.Replace("[AfterDropDownGridRowSelectFunction]", "");
                }

                strRowSelectionScript = strRowSelectionScript.Replace("[DropDownGridSelectedTextFormat]", this.SelectedTextFormat.ToString().ToLower() );
                

                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), this.ClientID + "_SelectionScript",  strRowSelectionScript , true);

                

                base.Attributes.Add("OnClick", "GridDropDownClick();");
        }

//        private void SetFilteringScripts()
//        {
//            string strStartupFilterScript = @"
//        function [ControlClientID]_FilterCallBack() 
//        {
//            ShowGridDropDown('[ControlClientID]');
//            
//            var tmpTxt =document.getElementById('[ControlClientID]_SelectedText_txt');
//            var tmpValue = GridViewTrim(tmpTxt.innerText);
//            
//            [FilterCallBackFunction];    
//        }
//
//        function [ControlClientID]_SetFilterResult(Message, context) 
//        {
//            
//            if (Message!='')
//            {
//                document.getElementById('__gv[ControlClientID]__div').outerHTML = Message;
//                __gv[ControlClientID].panelElement = document.getElementById('__gv[ControlClientID]__div');
//            }
//            else
//            {
//                alert ('No results were returned');
//            }
//        }
//
//";

//            strStartupFilterScript = strStartupFilterScript.Replace("[ControlClientID]", this.ClientID);

//            //string strFilterCallBackFunction = this.Page.ClientScript.GetCallbackEventReference(this, "GridViewFilterValue('"+this.ClientID +"')", this.ClientID  + "_SetFilterResult" , "", "GridViewOnCallBackError", true );

//            string strFilterCallBackFunction = this.filterCallBack.CustomCallBackFunction; 

//            strStartupFilterScript = strStartupFilterScript.Replace("[FilterCallBackFunction]", strFilterCallBackFunction);

//            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), this.ClientID + "_FilteringScript", strStartupFilterScript, true);

//        }


        private void SetFilteringScripts()
        {
            string strStartupFilterScript = @"
        function [ControlClientID]_FilterCallBack(e) 
        {
            ShowGridDropDown('[ControlClientID]');
            
            var tmpTxt =document.getElementById('[ControlClientID]_SelectedText_txt');
            var tmpValue = GridViewTrim(tmpTxt.innerText);
            var unicode=e.charCode? e.charCode : e.keyCode;
            if (unicode==13)
            { 
                var tmpFilter =document.getElementById('[ControlClientID]_FilterExpression_hdn');
                tmpFilter.value = tmpValue;
                [FilterCallBackFunction];
                document.getElementById('__gv[ControlClientID]__div').innerHTML = '';
                tmpTxt.innerText = 'Applying [' + tmpValue + '] filter';
                tmpTxt.contentEditable = false;
                return false;
            }
        }

        function [ControlClientID]_SetFilterResult(Message, context) 
        {
            var tmpTxt =document.getElementById('[ControlClientID]_SelectedText_txt');
            tmpTxt.innerText = tmpTxt.innerText.replace('Applying [','').replace('] filter','');
            tmpTxt.contentEditable = true;
            
            if (Message!='')
            {
                
                document.getElementById('__gv[ControlClientID]__div').outerHTML = Message;
                ShowGridDropDown('[ControlClientID]');
                try
                { 
                    __gv[ControlClientID].panelElement = document.getElementById('__gv[ControlClientID]__div');
                }
                catch(e)
                {}
            }
            else
            {
                alert ('No results were returned');
            }
        }

";

            strStartupFilterScript = strStartupFilterScript.Replace("[ControlClientID]", this.ClientID);

            //string strFilterCallBackFunction = this.Page.ClientScript.GetCallbackEventReference(this, "GridViewFilterValue('"+this.ClientID +"')", this.ClientID  + "_SetFilterResult" , "", "GridViewOnCallBackError", true );

            string strFilterCallBackFunction = this.filterCallBack.CustomCallBackFunction;

            strStartupFilterScript = strStartupFilterScript.Replace("[FilterCallBackFunction]", strFilterCallBackFunction);

            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), this.ClientID + "_FilteringScript", strStartupFilterScript, true);

        }


        protected override AutoGeneratedField CreateAutoGeneratedColumn(AutoGeneratedFieldProperties fieldProperties)
        {
            if (this.DesignMode)
            {
                return base.CreateAutoGeneratedColumn(fieldProperties); 
            }

            AutoGeneratedField tmpAutoGeneratedField = new AutoGeneratedField(fieldProperties.DataField);
            string strFieldName = fieldProperties.Name;
            if (this.AllowSorting)
            {
                tmpAutoGeneratedField.SortExpression = strFieldName;
            }
            tmpAutoGeneratedField.ReadOnly = fieldProperties.IsReadOnly;
            tmpAutoGeneratedField.DataType = fieldProperties.Type;

            if (fieldProperties.DataField == this.DataTextField)
            {
                tmpAutoGeneratedField.HeaderText = "Name";
                return tmpAutoGeneratedField;
            }
            else if (fieldProperties.DataField == this.DataValueField)
            {
                tmpAutoGeneratedField.HeaderText = "ID";
                return tmpAutoGeneratedField;
            }
            else if (fieldProperties.DataField == this.DataCodeField)
            {
                tmpAutoGeneratedField.HeaderText = "Code";
                return tmpAutoGeneratedField;
            }
            else
            {
                return null;
            }        
        }
        

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (!this.DesignMode)
            {
                CreateHiddenColumns();
            }
            
            
        }

        //protected override ICollection CreateColumns(PagedDataSource dataSource, bool useDataSource)
        //{
        //    return base.CreateColumns(dataSource, useDataSource);
        //}

        private void CreateHiddenColumns()
        {
 
            BoundField tmpValueCol = new BoundField();
            tmpValueCol.HeaderText = "ID";
            tmpValueCol.SortExpression = this.DataValueField;
            tmpValueCol.DataField = this.DataValueField;
            base.Columns.Add(tmpValueCol);

            BoundField tmpCodeCol = new BoundField();
            tmpCodeCol.HeaderText = "Code";
            tmpCodeCol.SortExpression = this.DataCodeField;
            tmpCodeCol.DataField = this.DataCodeField;
            base.Columns.Add(tmpCodeCol);


            BoundField tmpTextCol = new BoundField();
            tmpTextCol.HeaderText = "Name";
            tmpTextCol.SortExpression = this.DataTextField;
            tmpTextCol.DataField = this.DataTextField;
            base.Columns.Add(tmpTextCol);

        }

 
        protected override void CreateChildControls()
		{
             
            base.CreateChildControls();
            
            this.lblValue.BackColor = Color.White;
            this.lblDrop.BackColor = Color.White;
            
            
            if (this.Height.IsEmpty)
            {
                this.Height = new Unit(16);
            }

            base.Style.Add("Position", "absolute");

			this.lblView.CopyBaseAttributes(this);
            this.hlnkDetails.CopyBaseAttributes(this);
			this.lblValue.CopyBaseAttributes(this);
            this.lblDrop.CopyBaseAttributes(this);

            

            this.lblView.Style.Remove("Position");
            this.lblValue.Style.Remove("Position");
            this.lblDrop.Style.Remove("Position");
            this.hlnkDetails.Style.Remove("Position");



            this.lblValue.EnableTheming = false ;

            this.lblDrop.ID = this.ClientID + "_btn";
			this.lblValue.ID = this.ClientID+"_SelectedText_txt";

            this.lblValue.Style.Add("overflow", "hidden");


            this.lblValue.Style.Add("vertical-align","top");


            this.lblDrop.Style.Add("background-image", "url(" + this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Lamassau.Web.UI.WebControls.Resources.Images.drop.gif") + ")"); 
            
            this.lblDrop.Style.Add("background-repeat", "no-repeat");
            this.lblDrop.Style.Add("background-position", "center center"); 

            
            this.lblDrop.Style.Add("CURSOR", "Default");

            this.lblDrop.Attributes.Add("OnClick", "ShowHideGridDropDown('" + this.ClientID + "');");

            this.lblValue.Attributes.Add("OnClick", "ShowHideGridDropDown('" + this.ClientID + "');");


            if (!this.DesignMode)
            {
                if (this.EnableFiltering)
                {
                    this.lblValue.Attributes.Add("contentEditable", "true");
                    this.lblValue.Attributes.Add("onkeypress", "return " + this.ClientID + "_FilterCallBack(event);");
                    SetFilteringCallBackProperties();
                }

                SetPageClickWayAttribute();
            }
		}

        private void SetFilteringCallBackProperties()
        {
            
                filterCallBack.Enabled = true;
                filterCallBack.ID = "CallBackFilter";
                filterCallBack.CallBackClientFunction = this.ClientID + "_SetFilterResult";
                filterCallBack.CallBackArguments = "GridViewFilterValue('" + this.ClientID + "')";
                filterCallBack.CallBackClientErrorFunction = "GridViewOnCallBackError";

                filterCallBack.CallBackGetResultEvent += new CallBack.CallBackGetResultEventHandler(filterCallBack_CallBackGetResultEvent);
                filterCallBack.CallBackRaiseEvent += new CallBack.CallBackRaiseEventHandler(filterCallBack_CallBackRaiseEvent);
            if (!this.DesignMode)
            {
                this.Controls.Add(filterCallBack);
            }

        }

        void filterCallBack_CallBackRaiseEvent(object sender, CallbackRaiseEventArgs e)
        {

            if (e.CallBackCommand.Trim() != "")
            {

                this.PageIndex = 0;
                this.FilterExpression = e.CallBackCommand.Trim();
            }
            else
            {
                this.PageIndex = 0;
                this.FilterExpression = string.Empty;
            }
        }

        void filterCallBack_CallBackGetResultEvent(object sender, CallBackGetResultEventArgs e)
        {
            StringWriter writer = new StringWriter();
            HtmlTextWriter buffer = new HtmlTextWriter(writer);            

            this.RenderControl(buffer);
            
            string markup = writer.ToString();
            markup = markup.Trim();

            markup = markup.Substring(markup.IndexOf(@"<div id=""__gv" + this.ClientID + @"__div"""));
            markup = markup.Substring(0, markup.Length - 6);
            
            e.StringToClient = markup; 
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
                            this.Page.Form.Attributes["OnClick"] = tmpOnclick + " OnGridDropDownClickAway();";
                        }
                        else
                        {
                            this.Page.Form.Attributes["OnClick"] = tmpOnclick + "; OnGridDropDownClickAway();";
                        }
                        return;
                    }
                }

                this.Page.Form.Attributes.Add("OnClick", "OnGridDropDownClickAway();");
            }
        } 

        
        		
#endregion 

		#region Rendering

		protected override void Render(HtmlTextWriter writer)
		{
            
                if (!this.DesignMode)
                {
                    if (!this.Visible)
                    {
                        return;
                    }
                }

                this.lblValue.Width = this.TextBoxWidth;
                this.lblDrop.Height = this.Height;
                this.lblDrop.Width = this.Height;
                this.lblValue.Height = this.lblDrop.Height;


                if (this.lblDrop.Style["POSITION"] != null)
                {
                    if (this.lblDrop.Style["POSITION"].Trim().ToLower() == "absolute")
                    {
                        lblDrop.Style["LEFT"] = (Unit.Parse(this.lblValue.Style["LEFT"]).Value + this.lblValue.Width.Value).ToString();
                    }
                }






                GetSelectedText();
                SetTheSelectedItem();

                this.lblValue.Text = this.SelectedText;
                this.lblView.Text = this.SelectedText;
                this.hlnkDetails.ImageUrl = this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Lamassau.Web.UI.WebControls.Resources.Images.view.gif");
                this.hlnkDetails.NavigateUrl = this.DetailedViewUrl + this.SelectedID;


                this.lblValue.Enabled = this.Enabled;
                this.lblDrop.Enabled = this.Enabled;
                this.lblView.Enabled = this.Enabled;
                this.hlnkDetails.Enabled = this.Enabled;




                switch (this.ViewMode)
                {
                    case ViewModes.ReadOnly:
                        if (this.Hidden)
                        {
                            this.lblView.Style["display"] = "none";
                            this.hlnkDetails.Style["display"] = "none";
                        }


                        if (this.DetailedViewUrl.Trim() != "" && DetailedViewUrlVisible == true && this.SelectedID != "")
                        {
                            this.hlnkDetails.RenderControl(writer);
                        }
                        this.lblView.Text = this.lblView.Text + "  ";
                        this.lblView.RenderControl(writer);



                        writer.Write("<INPUT id=" + this.ClientID + "_SelectedID_hdn name=" + this.ClientID + "_SelectedID_hdn TYPE=\"HIDDEN\" value=\"" + this.SelectedID + "\"/>\n");
                        writer.Write("<INPUT id=" + this.ClientID + "_SelectedText_hdn name=" + this.ClientID + "_SelectedText_hdn TYPE=\"HIDDEN\" value=\"" + this.SelectedText + "\"/>\n");
                        writer.Write("<INPUT id=" + this.ClientID + "_GridVisible_hdn name=" + this.ClientID + "_GridVisible_hdn TYPE=\"HIDDEN\" value=\"" + this.GridVisible.ToString() + "\"/>\n");


                        break;

                    case ViewModes.Editable:
                        //EnsureChildControls();

                        string markup = "";


                        if (this.Hidden)
                        {
                            writer.Write("<DIV id=" + this.ClientID + "_Container Style=\"display:none;overflow: hidden;border:solid 1px SteelBlue;align:left;width:" + this.Width + ";height:" + this.lblValue.Height + "\">");
                        }
                        else
                        {
                            writer.Write("<DIV id=" + this.ClientID + "_Container Style=\"overflow:hidden;border:solid 1px SteelBlue;align:left;width:" + this.Width + ";height:" + this.lblValue.Height + "\">");
                        }


                        writer.Write("<INPUT id=" + this.ClientID + "_SelectedID_hdn name=" + this.ClientID + "_SelectedID_hdn TYPE=\"HIDDEN\" value=\"" + this.SelectedID + "\"/>\n");
                        writer.Write("<INPUT id=" + this.ClientID + "_SelectedText_hdn name=" + this.ClientID + "_SelectedText_hdn TYPE=\"HIDDEN\" value=\"" + this.SelectedText + "\"/>\n");

                        writer.Write("<INPUT id=" + this.ClientID + "_GridVisible_hdn name=" + this.ClientID + "_GridVisible_hdn TYPE=\"HIDDEN\" value=\"" + this.GridVisible.ToString() + "\"/>\n");

                        writer.Write("<INPUT id=" + this.ClientID + "_FilterExpression_hdn name=" + this.ClientID + "_FilterExpression_hdn TYPE=\"HIDDEN\" value=\"" + this.FilterExpression + "\"/>\n");

                        writer.Write("<NOBR>");
                        this.lblValue.Text = this.SelectedText;


                        this.lblValue.RenderControl(writer);
                        this.lblDrop.RenderControl(writer);
                        writer.Write("</NOBR>");




                        if (!this.Page.IsCallback)
                        {
                            base.Style.Add("visibility", GridVisible ? "visible" : "hidden");
                            base.Style.Add("display", GridVisible ? "inline-block" : "none");
                        }

                        if (base.Style["border-right"] == null)
                        {
                            base.Style.Add("border-right", "SteelBlue 1px solid");
                        }
                        if (base.Style["border-left"] == null)
                        {
                            base.Style.Add("border-left", "SteelBlue 1px solid");
                        }
                        if (base.Style["border-bottom"] == null)
                        {
                            base.Style.Add("border-bottom", "SteelBlue 1px solid");
                        }

                        // Output
                        if (!DesignMode)
                        {
                            markup = GetGridViewMarkup();
                            writer.Write(markup);
                            this.filterCallBack.RenderControl(writer);
                        }

                        writer.Write("</DIV>");



                        break;

                    default:
                        break;
                }


		}

        protected override void OnRowCreated(GridViewRowEventArgs e)
        {
            if (!this.DesignMode)
            {
                if (e.Row.RowType != DataControlRowType.Pager)
                {
                    e.Row.Cells[this.Columns.Count - 1].Style.Add("Display", "none");
                    e.Row.Cells[this.Columns.Count - 2].Style.Add("Display", "none");
                    e.Row.Cells[this.Columns.Count - 3].Style.Add("Display", "none");

                }

                if (!this.DesignMode && this.DataValueField != "" && this.DataTextField != "")
                {
                    if ((e.Row.RowType == DataControlRowType.DataRow))
                    {
                        e.Row.Attributes.Add("ID", this.ClientID + "Row" + e.Row.RowIndex.ToString());
                        e.Row.Attributes.Add("onclick", this.ClientID + "SelectRow(this," + e.Row.RowIndex.ToString() + "," + (this.Columns.Count - 3) + "," + (this.Columns.Count - 2) + "," + (this.Columns.Count - 1) + ",'" + this.ClientID + "');");

                        if (HoverColor != Color.Transparent)
                        {
                            e.Row.Attributes.Add("onmouseover", "TRmouseevent('" + this.ClientID + "Row" + e.Row.RowIndex.ToString() + "','" + ColorTranslator.ToHtml(HoverColor) + "');");
                            if (e.Row.RowIndex % 2 == 0)
                            {
                                e.Row.Attributes.Add("onmouseout", "TRmouseevent('" + this.ClientID + "Row" + e.Row.RowIndex.ToString() + "','" + ColorTranslator.ToHtml(this.RowStyle.BackColor) + "');");
                            }
                            else
                            {
                                e.Row.Attributes.Add("onmouseout", "TRmouseevent('" + this.ClientID + "Row" + e.Row.RowIndex.ToString() + "','" + ColorTranslator.ToHtml(this.AlternatingRowStyle.BackColor) + "');");
                            }
                        }
                    }

                }
            }
            base.OnRowCreated(e);
        }



		#region HELPERS
		// *****************************************************************************
		// METHOD GetGridViewMarkup
		// Return the default markup for the control
		private string GetGridViewMarkup()
		{
			// Capture the default output of the DataGrid control
			StringWriter writer = new StringWriter();
			HtmlTextWriter buffer = new HtmlTextWriter(writer);

			Unit tmpWidthUnit =  base.Width; 
			
			base.Width = this.GridWidth;

			base.Render(buffer);
			
			base.Width= tmpWidthUnit;

            return writer.ToString();
		}

		#endregion
		
		
		
#endregion

		#region Interfact: IPostBackDataHandler

		public virtual void RaisePostDataChangedEvent() 
		{
           
		}

		public virtual bool LoadPostData(string postDataKey,NameValueCollection postCollection) 
		{

            string key = postDataKey.Replace("$", "_");
			string GridVisiblePostedValue = postCollection[key+"_GridVisible_hdn"];
			this.GridVisible = GridVisiblePostedValue==null ? false : bool.Parse(GridVisiblePostedValue);

            string SelectedIDPresentValue = this.SelectedID;
            string SelectedIDPostedValue = postCollection[key + "_SelectedID_hdn"];

            string SelectedTextPostedValue = postCollection[key + "_SelectedText_hdn"];
            this.SelectedText = SelectedTextPostedValue == null ? "" : SelectedTextPostedValue;

            string FilterExpressionPostedValue = postCollection[key + "_FilterExpression_hdn"];
            ViewState["FilterExpression"] = FilterExpressionPostedValue == null ? "" : FilterExpressionPostedValue;


            if (SelectedIDPresentValue == null || !SelectedIDPresentValue.Equals(SelectedIDPostedValue))
            {
                this.SelectedID = SelectedIDPostedValue;
                return true;
            }

			return false;
		}

		#endregion
		
		#region Paging Customization





        private void GetSelectedText()
        {
            if (this.DesignMode)
            {
                return;
            }

            if (this.SelectedID == "")
            {
                this.SelectedText = "";
                return ;
            }
            if (this.SelectedText != "" && !this.Page.IsCallback )
            {
                return ;
            }

            try
            {

                try
                {
                    // call the event
                    GetSeletedItemTextEventArgs e = new GetSeletedItemTextEventArgs();
                    e.SelectedID = this.SelectedID; 
                    GetSeletedItemText(this, e);
                    
                    this.SelectedText = e.TextValue;



                }
                catch
                {

                    if (this.DataMember != "")
                    {
                        IDataSource IDS = this.GetDataSource();
                        DataSourceView dsv = IDS.GetView(this.DataMember);
                        dsv.Select(DataSourceSelectArguments.Empty, this.GetTextFromView);
                    }
                    else
                    {
                        string typeName = this.DataSource.GetType().Name;
                        switch (typeName)
                        {
                            case "DataSet":
                                SetSelectedTextValue(((DataSet)this.DataSource).Tables[0].DefaultView);
                                break;
                            case "DataTable":
                                SetSelectedTextValue(((DataTable)this.DataSource).DefaultView);
                                break;
                            default:
                                SetSelectedTextValue((DataView)this.DataSource);
                                break;
                        }
                    }
                }

            }
            catch
            {}
        }

        private void GetTextFromView(IEnumerable data)
        {
            DataView dv = (DataView)data;
            SetSelectedTextValue(dv);

        }

        private void SetSelectedTextValue(DataView dv)
        {
            string tmpRowFilter = dv.RowFilter;
            string strFilter = this.DataValueField + "="; 
            try
            {
                int.Parse(this.SelectedID);
                strFilter += this.SelectedID;
            }
            catch
            {
                strFilter += "'" + this.SelectedID + "'";
            }


            
            dv.RowFilter = strFilter;
            if (this.SelectedTextFormat == SelectedTextFormatEnum.TextOnly)
            {
                this.SelectedText = dv[0][this.DataTextField].ToString();
            }
            else
            {
                this.SelectedText  = dv[0][this.DataCodeField ].ToString() + " : " + dv[0][this.DataTextField].ToString();
            }
            
            dv.RowFilter = tmpRowFilter;
        }

		#endregion


        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            if (!this.DesignMode)
            {
                // Save selected index
                if (this.SelectedIndex != -1)
                {
                    ViewState["SelectedID"] = this.SelectedRow.Cells[this.Columns.Count - 2].Text;
                    ViewState["SelectedText"] = "";
                }
                else
                {
                    ViewState["SelectedID"] = null;
                    ViewState["SelectedText"] = null;
                }
            }
            base.OnSelectedIndexChanged(e);
        }


        protected override void OnSelectedIndexChanging(GridViewSelectEventArgs e)
        {
            if (!this.DesignMode)
            {
                this.GridVisible = false;
                base.OnSelectedIndexChanging(e);
            }
        }

    }



    public enum SelectedTextFormatEnum
    {
        TextOnly,
        CodeandText
    }



    #region Design Time Support
    namespace Design
    {
        public class DropDownGridViewControlDesigner : GridViewDesigner
        {

            public override string GetDesignTimeHtml()
            {
                // Component is the instance of the component or control that
                // this designer object is associated with. This property is 
                // inherited from System.ComponentModel.ComponentDesigner.
                string tmpS = string.Empty;
                try
                {

                    DropDownGridView DesignGridView = (DropDownGridView)Component;

                    if (DesignGridView.DataValueField == "" || DesignGridView.DataTextField == "")
                    {
                        throw new Exception("Define DataTextField and DataValueField properties for the dropdown to work properly");
                    }

                    // Capture the default output of the DropDownGrid control
                    StringWriter writer = new StringWriter();
                    HtmlTextWriter buffer = new HtmlTextWriter(writer);
                    DesignGridView.RenderControl(buffer);
                    tmpS = writer.ToString();

                }
                catch (Exception ex)
                {
                    tmpS = this.GetErrorDesignTimeHtml(ex);
                }
                base.GetDesignTimeHtml();
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


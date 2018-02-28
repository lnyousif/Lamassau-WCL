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
using System.Globalization;

[assembly: TagPrefix("Lamassau.Web.UI.WebControls", "lms")]
namespace Lamassau.Web.UI.WebControls
{

    /// <summary>
    /// DataGrid: Inherting the DataGrid ASP.NET control;
    /// Designed by Laith N Yousif.
    /// Developed by Laith N Yousif. 
    /// Date: Jan 2006.
    /// </summary>
    [ToolboxData("<{0}:GridView runat=server></{0}:GridView>"), PermissionSet(SecurityAction.Demand, Unrestricted = true)]
    public class GridView : System.Web.UI.WebControls.GridView, ILamassauControl  
    {
        public delegate void GridViewFilteringEventHandler(object sender, GridViewFilteringEventArgs e);
        public event GridViewFilteringEventHandler GridViewFiltering;
 

        #region Initialization

        //private System.Web.UI.WebControls.GridView gvXSL;
        private Helper Ctr;
		// Methods
        public GridView()
		{
            this.Ctr = new Helper();
            //this.gvXSL = new System.Web.UI.WebControls.GridView();
 		}
		
		#endregion

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

        #region Properties
        /// <summary>
        /// Enable/Disable MultiColumn Sorting.
        /// </summary>
        [
        Description("Whether Sorting On more than one column is enabled"),
        Category("Behavior"),
        DefaultValue("true"),
        ]
        public bool AllowMultiColumnSorting
        {
            get
            {
                object o = ViewState["AllowMultiColumnSorting"];
                return (o != null ? (bool)o : false);
            }
            set
            {
                if (value)
                {
                    this.AllowSorting = true;
                }

                ViewState["AllowMultiColumnSorting"] = value;
            }
        }



        public bool ShowTotalRowCount
        {
            get
            {
                object o = ViewState["ShowTotalRowCount"];
                return (o != null ? (bool)o : false);
            }
            set
            {
                ViewState["ShowTotalRowCount"] = value;
            }
        }


        [DefaultValue("true")]
        public override bool EnableSortingAndPagingCallbacks
        {
            get
            {
                return base.EnableSortingAndPagingCallbacks;
            }
            set
            {
                base.EnableSortingAndPagingCallbacks = value;
            }
        }

        /// <summary>
        /// Get or Set Image location to be used to display Ascending Sort order.
        /// </summary>
        [
        Description("Image to display for Ascending Sort"),
        Category("Misc"),
        Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
        DefaultValue(""),
        ]
        public string SortAscImageUrl
        {
            get
            {
                object o = ViewState["SortImageAsc"];
                return (o != null ? o.ToString() : "");
            }
            set
            {
                ViewState["SortImageAsc"] = value;
            }
        }
        /// <summary>
        /// Get or Set Image location to be used to display Ascending Sort order.
        /// </summary>
        [Description("Image to display for Descending Sort"),Category("Misc"),Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),DefaultValue("")]
        public string SortDescImageUrl
        {
            get
            {
                object o = ViewState["SortImageDesc"];
                return (o != null ? o.ToString() : "");
            }
            set
            {
                ViewState["SortImageDesc"] = value;
            }
        }
        #endregion



        public int TotalRowCount
        {
            get
            {
                
                object o = ViewState["TotalRowCount"];
                return (o != null ? int.Parse(o.ToString()) : 0);
            }

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.DesignMode)
            {
                if (this.FilterExpression != "")
                {
                    GridViewFilteringEventArgs ee = new GridViewFilteringEventArgs();
                    ee.FilterExpression = this.FilterExpression;
                    GridViewFiltering(this, ee);
                }
            }
            //if (this.SortExpression == "")
            //{
            //    this.Sort(this.DefaultSortExpression, SortDirection.Ascending);
            //}
        }
        protected override string GetCallbackResult()
        {
            if (this.FilterExpression != "")
            {
                GridViewFilteringEventArgs ee = new GridViewFilteringEventArgs();
                ee.FilterExpression = this.FilterExpression;
                GridViewFiltering(this, ee);
            }
            return base.GetCallbackResult();
        }


        [Bindable(false),
        Description("Filter for the Datasource"),
        Category("Data"),
        DefaultValue("")]
        public string FilterExpression
        {
            get
            {
                object o = ViewState["FilterExpression"];
                return (o != null ? o.ToString() : "");
            }
            set
            {
                ViewState["FilterExpression"] = value;

                try
                {
                    GridViewFilteringEventArgs e = new GridViewFilteringEventArgs();
                    e.FilterExpression = value;
                    GridViewFiltering(this, e);
                }
                catch
                { }
            
                try
                {
                    this.DataBind();
                }
                catch
                { }
                
            }
        }

        public string DefaultSortExpression
        {
            get
            {
                object o = ViewState["DefaultSortExpression"];
                return (o != null ? o.ToString() : "");
            }
            set
            {
                ViewState["DefaultSortExpression"] = value;
            }
        }


        //private void GetFilterView(IEnumerable data)
        //{
        //    DataView dv = (DataView)data;
        //    SetFilter(dv);
        //}

        //private void SetFilter(DataView dv)
        //{
        //    dv.RowFilter = this.DataFilter;
        //}

        #region Life Cycle


        protected override void OnSorting(GridViewSortEventArgs e)
        {
            if (!this.DesignMode)
            {
                if (AllowMultiColumnSorting)


                    e.SortExpression = GetSortExpression(e);

                //e.SortExpression = (this.DefaultSortExpression != "") ? e.SortExpression.Replace(this.DefaultSortExpression , "") : e.SortExpression;


                //if (e.SortExpression.Trim().StartsWith(","))
                //{
                //    e.SortExpression = e.SortExpression.Remove(0, 1);
                //}

                if (e.SortExpression == "")
                {
                    e.SortExpression = this.DefaultSortExpression;
                }

            }
            base.OnSorting(e);
        }

        

        protected override void OnRowCreated(GridViewRowEventArgs e)
        {
            if (!this.DesignMode)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    if (SortExpression != String.Empty)
                    {
                        string mySortExpression = SortExpression;
                        mySortExpression = (this.DefaultSortExpression != "") ? mySortExpression.Replace(this.DefaultSortExpression, "") : mySortExpression;


                        if (mySortExpression.Trim().StartsWith(","))
                        {
                            mySortExpression = mySortExpression.Remove(0, 1);
                        }

                        DisplaySortOrderImages(mySortExpression, e.Row);
                    }
                }
            }

            base.OnRowCreated(e);
                        
        }
        protected override void OnRowDataBound(GridViewRowEventArgs e)
        {
            if (!this.DesignMode)
            {
                // for excel export
                if (_ExportMode)
                {
                    foreach (TableCell ctrCell in e.Row.Cells)
                    {
                        foreach (Control ctrItem in ctrCell.Controls)
                        {
                            if (ctrItem.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                            {
                                Literal l = new Literal();
                                l.Text = (ctrItem as System.Web.UI.WebControls.CheckBox).Checked ? "True" : "False";

                                int itemIndex = ctrCell.Controls.IndexOf(ctrItem);
                                ctrCell.Controls.Remove(ctrItem);
                                ctrCell.Controls.AddAt(itemIndex, l);

                            }

                        }
                    }
                }
            }
            base.OnRowDataBound(e);
        }


        protected override void PrepareControlHierarchy()
        {
            base.PrepareControlHierarchy();
            if (!this.DesignMode)
            {
                if (this.TotalRowCount > 0 && this.ShowTotalRowCount)
                {

                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.EmptyDataRow, DataControlRowState.Normal);
                    this.InnerTable.Rows.Add(row);
                    TableCell cell = new TableCell();
                    cell.ColumnSpan = this.Columns.Count;
                    cell.ApplyStyle(PagerStyle);
                    row.Cells.Add(cell);
                    cell.Text = this.TotalRowCount.ToString() + " record(s)";
                }
            }

        }


        protected System.Web.UI.WebControls.Table InnerTable
        {
            get
            {
                if (this.HasControls())
                {
                    return (System.Web.UI.WebControls.Table)this.Controls[0];
                }

                return null;
            }
        }



        #endregion

        #region Protected Methods
        /// <summary>
        ///  Get Sort Expression by Looking up the existing Grid View Sort Expression 
        /// </summary>
        protected string GetSortExpression(GridViewSortEventArgs e)
        {
            if (!this.DesignMode)
            {
                string[] sortColumns = null;
                string sortAttribute = string.Empty;

                //if (this.SortExpression.Trim() != "")
                //{
                sortAttribute = SortExpression;
                //}
                //else
                //{
                //    sortAttribute = this.DefaultSortExpression;
                //}

                if (e.SortExpression != "" && this.DefaultSortExpression != "")
                {
                    sortAttribute = SortExpression.Replace(this.DefaultSortExpression, "");
                }

                //Check to See if we have an existing Sort Order already in the Grid View.	
                //If so get the Sort Columns into an array
                if (sortAttribute != String.Empty)
                {
                    sortColumns = sortAttribute.Split(",".ToCharArray());
                }

                //if User clicked on the columns in the existing sort sequence.
                //Toggle the sort order or remove the column from sort appropriately


                if (sortAttribute.IndexOf(e.SortExpression) > 0 || sortAttribute.StartsWith(e.SortExpression))
                    sortAttribute = ModifySortExpression(sortColumns, e.SortExpression);
                else
                    sortAttribute += String.Concat(",", e.SortExpression, " ASC ");
                return sortAttribute.TrimStart(",".ToCharArray()).TrimEnd(",".ToCharArray());
            }
            else
            {
                return "";
            }
        }

        
        protected string ModifySortExpression(string[] sortColumns, string sortExpression)
        {

            string ascSortExpression = String.Concat(sortExpression, " ASC ");
            string descSortExpression = String.Concat(sortExpression, " DESC ");

            for (int i = 0; i < sortColumns.Length; i++)
            {

                if (ascSortExpression.Equals(sortColumns[i]))
                {
                    sortColumns[i] = descSortExpression;
                }

                else if (descSortExpression.Equals(sortColumns[i]))
                {
                    Array.Clear(sortColumns, i, 1);
                }
            }

            return String.Join(",", sortColumns).Replace(",,", ",").TrimStart(",".ToCharArray());

        }
        /// <summary>
        ///  Lookup the Current Sort Expression to determine the Order of a specific item.
        /// </summary>
        protected void SearchSortExpression(string[] sortColumns, string sortColumn, out string sortOrder, out int sortOrderNo)
        {
            sortOrder = "";
            sortOrderNo = -1;
            for (int i = 0; i < sortColumns.Length; i++)
            {
                if (sortColumns[i].StartsWith(sortColumn))
                {
                    sortOrderNo = i + 1;
                    if (AllowMultiColumnSorting)
                        sortOrder = sortColumns[i].Substring(sortColumn.Length).Trim();
                    else
                        sortOrder = ((SortDirection == SortDirection.Ascending) ? "ASC" : "DESC");
                }
            }
        }

        /// <summary>
        ///  Display a graphic image for the Sort Order along with the sort sequence no.
        /// </summary>
        protected void DisplaySortOrderImages(string sortExpression, GridViewRow dgItem)
        {
            if (!this.DesignMode)
            {
                string[] sortColumns = sortExpression.Split(",".ToCharArray());

                for (int i = 0; i < dgItem.Cells.Count; i++)
                {
                    if (dgItem.Cells[i].Controls.Count > 0 && dgItem.Cells[i].Controls[0] is System.Web.UI.WebControls.LinkButton)
                    {
                        string sortOrder;
                        int sortOrderNo;
                        string column = ((System.Web.UI.WebControls.LinkButton)dgItem.Cells[i].Controls[0]).CommandArgument;
                        SearchSortExpression(sortColumns, column, out sortOrder, out sortOrderNo);
                        if (sortOrderNo > 0)
                        {
                            string sortImgLoc = (sortOrder.Equals("ASC") ? SortAscImageUrl : SortDescImageUrl);

                            if (sortImgLoc != String.Empty)
                            {
                                Image imgSortDirection = new Image();
                                imgSortDirection.ImageUrl = sortImgLoc;
                                dgItem.Cells[i].Controls.Add(imgSortDirection);
                                Label lblSortOrder = new Label();
                                lblSortOrder.Font.Size = FontUnit.Small;
                                lblSortOrder.Text = sortOrderNo.ToString();
                                dgItem.Cells[i].Controls.Add(lblSortOrder);

                            }
                            else
                            {
                                Label lblSortDirection = new Label();
                                lblSortDirection.Font.Size = FontUnit.XSmall;
                                lblSortDirection.Font.Name = "webdings";
                                lblSortDirection.EnableTheming = false;
                                lblSortDirection.Text = (sortOrder.Equals("ASC") ? "5" : "6");
                                dgItem.Cells[i].Controls.Add(lblSortDirection);

                                if (AllowMultiColumnSorting)
                                {
                                    Literal litSortSeq = new Literal();
                                    litSortSeq.Text = sortOrderNo.ToString();
                                    dgItem.Cells[i].Controls.Add(litSortSeq);

                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion

        
        

        protected override void OnDataBound(EventArgs e)
        {
            base.OnDataBound(e);
            if (!this.DesignMode)
            {
                ViewState["TotalRowCount"] = 0;

                if (this.PageCount > 1)
                {
                    if (this.SelectArguments.TotalRowCount > -1)
                    {
                        ViewState["TotalRowCount"] = this.SelectArguments.TotalRowCount;
                    }
                }
                else
                {
                    ViewState["TotalRowCount"] = this.Rows.Count;
                }

                if (this.TotalRowCount < 1)
                {
                    IDataSource IDS = this.GetDataSource();
                    DataSourceView dsv = IDS.GetView(this.DataMember);
                    dsv.Select(DataSourceSelectArguments.Empty, this.SetRowCount);
                }
            }

        }



        private void SetRowCount(IEnumerable data)
        {
            try
            {
                DataView dv = (DataView)data;
                ViewState["TotalRowCount"] = dv.Count;
            }
            catch
            {
            
            }
        }
            



        protected override void Render(HtmlTextWriter writer)
        {
            if (!this.DesignMode)
            {

                StringWriter tmpWriter = new StringWriter();
                HtmlTextWriter buffer = new HtmlTextWriter(tmpWriter);


                base.Render(buffer);

                string strGRid = tmpWriter.ToString();
                string strDivTag = strGRid.Substring(0, strGRid.IndexOf(">"));
                if (strDivTag.IndexOf(" id=") == -1)
                {
                    strGRid = strGRid.Remove(0, 4);

                    strGRid = @"<div id=""__gv" + this.ClientID + @"__div""" + strGRid;
                }
                writer.Write(strGRid);
            }
            else
            {
                base.Render(writer); 
            }
        }

        #region Excel Export
        private const string C_HTTP_HEADER_CONTENT = "Content-Disposition";
        private const string C_HTTP_ATTACHMENT = "attachment;filename=";
        //private const string C_HTTP_CONTENT_TYPE_EXCEL =  "application/vnd.xls";
        private const string C_HTTP_CONTENT_TYPE_EXCEL = "application/ms-excel";
        private const string C_HTTP_CONTENT_LENGTH = "Content-Length";



        public void ExportToExcel()
        {

            this.Page.Response.Clear();

            this.Page.Response.AddHeader(C_HTTP_HEADER_CONTENT, C_HTTP_ATTACHMENT + ((this.DataMember!="")? this.DataMember : "list")+ ".xls")  ;


            this.Page.Response.Charset = "";

            // If you want the option to open the Excel file without saving than
            // comment out the line below
            // Response.Cache.SetCacheability(HttpCacheability.NoCache);

            this.Page.Response.ContentType = C_HTTP_CONTENT_TYPE_EXCEL;

            System.IO.StringWriter stringWrite = new System.IO.StringWriter();

            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);


            this.AllowMultiColumnSorting = false;
            this.EnableSortingAndPagingCallbacks = false;
            this.AllowPaging = false;
            this.AllowSorting = false;

            this.Style.Clear();

            this.RowStyle.Reset();
            this.HeaderStyle.Reset();
            this.AlternatingRowStyle.Reset();
            this.BackColor = Color.Transparent;
            this.BorderWidth = new Unit(0);
            this.ShowTotalRowCount = false;
            this._ExportMode= true;

            this.Columns.Clear();
            this.AutoGenerateColumns = true;

            this.DataBind();

            this.RenderControl(htmlWrite);


            this.Page.Response.Write(stringWrite.ToString());

            this.Page.Response.End();

        }

        private bool _ExportMode=false;

        #endregion


        protected override DataSourceSelectArguments CreateDataSourceSelectArguments()
        {
            DataSourceSelectArguments dataSourceSelectArguments = base.CreateDataSourceSelectArguments();
            if (!this.DesignMode)
            {
                if (string.IsNullOrEmpty(dataSourceSelectArguments.SortExpression) && !string.IsNullOrEmpty(this.DefaultSortExpression))
                {
                    dataSourceSelectArguments.SortExpression = this.DefaultSortExpression;
                }
            }
            return dataSourceSelectArguments;
        }


        

    }



}

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


namespace Lamassau.Web.UI.WebControls
{

    /// <summary>
    /// Summary description for DataSourceLoadingEventArgs
    /// </summary>GetSeletedItemText
    public class GridViewFilteringEventArgs : EventArgs
    {
        private string _FilterExpression = string.Empty;


        public GridViewFilteringEventArgs()
        {
        }

        //Properties.
        public string FilterExpression
        {
            get
            {
                return _FilterExpression;
            }
            set
            {
                _FilterExpression = value;
            }
        }

    }
}
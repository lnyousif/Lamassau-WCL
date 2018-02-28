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
    public class GetSeletedItemTextEventArgs : EventArgs
    {
        private string _TextValue = string.Empty;
        private string _SelectedID = string.Empty;
        private SelectedTextFormatEnum _SelectedTextFormat; 


        public GetSeletedItemTextEventArgs()
        {
        }

        //Properties.
        public string TextValue
        {
            get
            {
                return _TextValue;
            }
            set
            {
                _TextValue = value;
            }
        }

        //Properties.
        public string SelectedID
        {
            get
            {
                return _SelectedID;
            }
            set
            {
                _SelectedID = value;
            }
        }

        public SelectedTextFormatEnum SelectedTextFormat
        {
            get
            {
                return _SelectedTextFormat;
            }
            set
            {
                _SelectedTextFormat = value;
            }
        }

       
    }
}
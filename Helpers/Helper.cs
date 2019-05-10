using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.ComponentModel;

namespace Lamassau.Web.UI.WebControls
{
    internal class Helper : Control 
    {
        #region IControl Implementation
        [Bindable(true), Category("Behavior"), Description("TextBox View Mode"), DefaultValue(ViewModes.Invisible)]
        public ViewModes ViewMode
        {
            get
            {
                if (ViewState["ViewMode"] == null)
                {
                    return ViewModes.Editable;
                }
                else
                {
                    switch (ViewState["ViewMode"].ToString())
                    {
                        case "ReadOnly":
                            return ViewModes.ReadOnly;

                        case "Editable":
                            return ViewModes.Editable;

                        default:
                            return ViewModes.Invisible;

                    }
                }
            }
            set
            {
                ViewState["ViewMode"] = value;

            }
        }

        [Bindable(true), Category("Behavior"), Description("InhertedViewMode"), DefaultValue(InhertedViewModes.Auto)]
        public InhertedViewModes InhertedViewMode
        {
            get
            {
                if (ViewState["InhertedViewMode"] == null)
                {
                    return InhertedViewModes.Auto;
                }
                else
                {
                    switch (ViewState["InhertedViewMode"].ToString())
                    {
                        case "Manual":
                            return InhertedViewModes.Manual;

                        default:
                            return InhertedViewModes.Auto;

                    }
                }
            }
            set
            {
                ViewState["InhertedViewMode"] = value;
            }
        }
        #endregion
    }
}

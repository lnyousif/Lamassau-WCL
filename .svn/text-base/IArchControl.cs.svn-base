using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace Lamassau.Web.UI
{
    public interface ILamassauControl
    {
        ViewModes ViewMode
        {
            get;
            set;
        }

        InhertedViewModes InhertedViewMode
        {
            get;
            set;
        }


    }

    public enum ViewModes
    {
        Invisible,
        ReadOnly,
        Editable
    }

    public enum InhertedViewModes
    {
        Auto,
        Manual

    }


    public enum SelectionBehavior
    {
        DefaultClientScript,
        PostBack,
        CallBack,
        CustomClientScript
    }

}

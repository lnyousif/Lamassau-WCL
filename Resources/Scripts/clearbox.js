
function cleartextbox(ControlToClearID)
{
    document.getElementById(ControlToClearID).value='';
}

function clearcheckbox(ControlToClearID)
{
    document.getElementById(ControlToClearID).checked=false;
}

function clearcalendar(ControlToClearID)
{
    document.getElementById(ControlToClearID +'_SelectedDate_txt').innerText = '';
    document.getElementById(ControlToClearID +'_SelectedDate_hdn').value = '';
}

function cleardropdowngridview(ControlToClearID)
{
    document.getElementById(ControlToClearID +'_SelectedID_hdn').value = '';
    document.getElementById(ControlToClearID +'_SelectedText_txt').innerText = '';
    document.getElementById(ControlToClearID +'_SelectedText_hdn').value = '';
}

//function clearmouseover(ClearControlID)
//{
//    //document.getElementById(ClearControlID).style.border='1px solid navy';
//    document.getElementById(ClearControlID).src='test.gif';
//}
//function clearmouseout(ClearControlID)
//{
//    //document.getElementById(ClearControlID).style.border='1px solid #ccc';
//    document.getElementById(ClearControlID).style.border='1px solid transparent';
//    
//}

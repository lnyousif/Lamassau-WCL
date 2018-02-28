
function SelectDate(ControlID, DateValue,DateValueFormatedString)
{
    window.event.cancelBubble = true;
	HideCalendar(ControlID);
	document.getElementById(ControlID +'_SelectedDate_txt').innerText = DateValueFormatedString;
    document.getElementById(ControlID +'_SelectedDate_hdn').value = DateValue;
}


function DoMouseOver (obj, useHeader, cursor,colr,txtClr){
	cursor = 'hand';
	var topIndex = useHeader ? 0 : 1;
	if (obj.rowIndex >= topIndex) 	{
	obj.style.color = txtClr;
	obj.style.backgroundColor = colr;
}

}
function DoMouseOut (obj, CssClass, useHeader){
	if (CssClass=='')
		{
		CssClass='';
		}
	var topIndex = useHeader ? 0 : 1;
	if (obj.rowIndex >= topIndex) 
		{
		obj.style.cursor='auto';
		obj.style.cssText = '';
		obj.className = CssClass;
		}
}

function ShowHideCalendar(getDIVID)
{
	//window.event.cancelBubble = true;
	var tableID = getDIVID ;
	var CalendarVisibleHiddenField = getDIVID + '_CalendarVisible_hdn';
	var browserName = navigator.appName;
	
	if (document.getElementById(tableID)!=null)
	{
		var myTable = document.getElementById(tableID);
		if (document.getElementById(tableID).style.display == 'none') 
			{
			myTable.style.display='inline'
			document.getElementById(CalendarVisibleHiddenField).value ='true';
			
			HideCalendar(document.forms[0].getAttribute('VisibleCalendar'));
			document.forms[0].setAttribute('VisibleCalendar',tableID);
			}
		else
			{
			myTable.style.display = 'none';
			document.getElementById(CalendarVisibleHiddenField).value ='false';
			document.forms[0].removeAttribute('VisibleCalendar');
			}
	}
}

function OnCalendarClickAway()
{
    HideCalendar(document.forms[0].getAttribute('VisibleCalendar'));
}

function CalendarClick()
{
    window.event.cancelBubble = true;
}

function HideCalendar(getDIVID)
{
	window.event.cancelBubble = true;
	//var tableID = getDIVID + '_tbl' ;
	var tableID = getDIVID ;
	var CalendarVisibleHiddenField = getDIVID + '_CalendarVisible_hdn';
	//var browserName = navigator.appName;
	
	if (document.getElementById(tableID)!=null)
	{
	var myTable = document.getElementById(tableID);
        if (document.getElementById(tableID).style.display == 'inline')
        {
            myTable.style.display = 'none';
            document.getElementById(CalendarVisibleHiddenField).value ='false';
            document.forms[0].removeAttribute('VisibleCalendar');
        }
	}
}


function SetCalendarTablePosition(getDIVID)
{
	//var tableID = getDIVID + '_tbl' ;
	var tableID = getDIVID ;
	var myTable = document.getElementById(tableID); 
    if (myTable!=null)
    {
		var DropTextBox = document.getElementById(getDIVID +'_btn');
		myTable.style.left = GetCalendarElementLeft(myTable,DropTextBox) - myTable.Width;
		myTable.style.top = GetCalendarElementTop(myTable,DropTextBox) + DropTextBox.Height;
	}
}

function headerHeight(tabName,onOff,csss)
	{
	table = document.getElementById(tabName);
	cells = table.getElementsByTagName('th');
	for (var i = 0; i < cells.length; i++) 
		{
		if (!onOff)
			{
			cells[i].style.fontSize = '8pt';
			//cells[i].style.cssText = csss;
			cells[i].style.backgroundColor = 'buttonface';
			}
		else
			{
			cells[i].Height = '0pt';
			}
		}
	}
	
	
function GetCalendarElementLeft(myDIV ,eElement)
{
var nLeftPos = eElement.offsetLeft;      
var eParElement = eElement.offsetParent;
while (eParElement != null)
   {                                            
   nLeftPos += eParElement.offsetLeft;     
   eParElement = eParElement.offsetParent; 
   }

return nLeftPos;


}

function GetCalendarElementTop(myDIV, eElement)
{

var nTopPos = eElement.offsetTop;
var eParElement = eElement.offsetParent;

while (eParElement != null)
  {
  nTopPos += eParElement.offsetTop;
  eParElement = eParElement.offsetParent;
  }

nTopPos = nTopPos + eElement.offsetHeight;
return nTopPos ;
}	




function calendarselectdatelabel(ControlID, AssociatedControlID, DateValue,DateValueFormatedString)
{
    window.event.cancelBubble = true;
	HideCalendar(ControlID);
	document.getElementById(ControlID +'_SelectedDate_hdn').value = DateValue;
	document.getElementById(AssociatedControlID).innerText = DateValueFormatedString;
}

function calendarselectdatetextbox(ControlID,AssociatedControlID, DateValue,DateValueFormatedString)
{
    window.event.cancelBubble = true;
	HideCalendar(ControlID);
	document.getElementById(ControlID +'_SelectedDate_hdn').value = DateValue;
	document.getElementById(AssociatedControlID).value = DateValueFormatedString;
}





//function isDate(dateStr)
//{
//    if(dateStr=="")
//    {
//        return;
//    }

//    var datePat = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
//    var matchArray = dateStr.match(datePat);
//    if (matchArray == null) 
//    {
//        alert("Please enter date as either mm/dd/yyyy or mm-dd-yyyy.");
//        return false;
//        }
//        
//        month = matchArray[1];
//        day = matchArray[3];
//        year = matchArray[5];
//        
//        if (month < 1 || month > 12) 
//        {
//            alert("Month must be between 1 and 12.");
//            return false;
//        }
//        if (day < 1 || day > 31) 
//        {
//            alert("Day must be between 1 and 31.");
//            return false;
//        }
//        if ((month==4 || month==6 || month==9 || month==11) && day==31) 
//        {
//            alert("Month " + month + " doesn't have 31 days!");
//            return false;
//        }
//        if (month == 2) 
//        {
//            var isleap = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0));if (day > 29 || (day==29 && !isleap)) 
//            {
//                alert("February " + year + " doesn't have " + day + " days!");
//                return false;
//            }
//        }
//    }          
//    return true;
//}

		
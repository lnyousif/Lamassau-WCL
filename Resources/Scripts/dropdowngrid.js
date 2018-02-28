
//function SelectRow(obj, colValue, colText,ControlID)
//{
//    HideGridDropDown(ControlID);
//    document.getElementById(ControlID +'_SelectedID_hdn').value = colValue;
//    document.getElementById(ControlID +'_SelectedText_txt').innerText = colText;
//    document.getElementById(ControlID +'_SelectedText_hdn').value = colText;
//}


// the function as of july 27 2006
//function NumberedSelectRow(rowObj, colValue, colText,ControlID)
//{
//	ShowHideGridDropDown(ControlID);
//	document.getElementById(ControlID +'_SelectedID_hdn').value = rowObj.cells(colValue).innerText;
//	document.getElementById(ControlID +'_SelectedText_txt').innerText = rowObj.cells(colText).innerText;	
//	document.getElementById(ControlID +'_SelectedText_hdn').value  = rowObj.cells(colText).innerText;
//}

//function NumberedSelectRow(rowObj, colValue, colText,ControlID,SelectedTextFormat)
//{
//	ShowHideGridDropDown(ControlID);
//	document.getElementById(ControlID +'_SelectedID_hdn').value = rowObj.cells(colValue).innerText;
//	if (SelectedTextFormat=='textonly')
//	{
//	    document.getElementById(ControlID +'_SelectedText_txt').innerText = rowObj.cells(colText).innerText;	
//	}
//	else
//	{
//	    document.getElementById(ControlID +'_SelectedText_txt').innerText = rowObj.cells(colValue).innerText + ':' + rowObj.cells(colText).innerText;	
//	}
//	document.getElementById(ControlID +'_SelectedText_hdn').value  = rowObj.cells(colText).innerText;
//}

function NumberedSelectRow(rowObj, colValue,colCode,colText,ControlID,SelectedTextFormat)
{
	ShowHideGridDropDown(ControlID);
	document.getElementById(ControlID +'_SelectedID_hdn').value = rowObj.cells(colValue).innerText;
	if (SelectedTextFormat=='textonly')
	{
	    document.getElementById(ControlID +'_SelectedText_txt').innerText = rowObj.cells(colText).innerText;
	}
	else
	{
	    document.getElementById(ControlID +'_SelectedText_txt').innerText = rowObj.cells(colCode).innerText + ' : ' + rowObj.cells(colText).innerText;
	}
	document.getElementById(ControlID +'_SelectedText_hdn').value  = document.getElementById(ControlID +'_SelectedText_txt').innerText;
	//alert ('me' +  rowObj.cells(colText).innerText + 'me');
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

function ShowHideGridDropDown(getDIVID)
{
    //window.event.cancelBubble = true;
    var mainDivName =  getDIVID + '_div';
//    var tableID = getDIVID + '_tbl' ;
    var tableID = getDIVID ;  
    var GridVisibleHiddenField = getDIVID + '_GridVisible_hdn';
    var browserName = navigator.appName;
	
    if (document.getElementById(tableID)!=null)
    {
	    var myTable = document.getElementById(tableID);
	    
	    //if (document.getElementById(tableID).style.display == 'none') 
		if (myTable.style.visibility == 'hidden') 
		    {
		    myTable.style.display='inline-block'
		    myTable.style.visibility= 'visible'
		    document.getElementById(GridVisibleHiddenField).value ='true';
		    HideGridDropDown(document.forms[0].getAttribute('VisibleGridDropDown'));
			document.forms[0].setAttribute('VisibleGridDropDown',tableID);
		    }
	    else
		    {
		    myTable.style.display = 'none';
		    myTable.style.visibility= 'hidden'
		    document.getElementById(GridVisibleHiddenField).value ='false';
		    document.forms[0].removeAttribute('VisibleGridDropDown');
		    }
    }
}


function OnGridDropDownClickAway()
{
    HideGridDropDown(document.forms[0].getAttribute('VisibleGridDropDown'));
}

function GridDropDownClick()
{
    window.event.cancelBubble = true;
}



function HideGridDropDown(getDIVID)
{

	window.event.cancelBubble = true;
	//var tableID = getDIVID + '_tbl' ;
	var tableID = getDIVID ;
	var GridVisibleHiddenField = getDIVID + '_GridVisible_hdn';
	//var browserName = navigator.appName;
	
	if (document.getElementById(tableID)!=null)
	{
	    var myTable = document.getElementById(tableID);
        //if (document.getElementById(tableID).style.display == 'inline')
        //if (myTable.style.visibility == 'visible') 
        //{
            myTable.style.display = 'none';
            myTable.style.visibility= 'hidden';
            document.getElementById(GridVisibleHiddenField).value ='false';
            document.forms[0].removeAttribute('VisibleGridDropDown');
        //}
        document.getElementById(getDIVID +'_SelectedText_txt').innerText = document.getElementById(getDIVID +'_SelectedText_hdn').value;
	}
	
	
}

function ShowGridDropDown(getDIVID)
{

	window.event.cancelBubble = true;
	var tableID = getDIVID ;
	var GridVisibleHiddenField = getDIVID + '_GridVisible_hdn';
	
	if (document.getElementById(tableID)!=null)
	{
	var myTable = document.getElementById(tableID);
        //if (myTable.style.visibility == 'hidden') 
        //{
            myTable.style.display = 'inline-block';
            myTable.style.visibility= 'visible';
            document.getElementById(GridVisibleHiddenField).value ='true';
            document.forms[0].setAttribute('VisibleGridDropDown',tableID);
        //}
	}
}




function SetGridTablePosition(getDIVID)
{
var tableID = getDIVID;
if (document.getElementById(tableID)!=null)
{
    alert('in');
    var DropTextBox = document.getElementById(getDIVID +'_SelectedText_txt');
    var myTable = document.getElementById(tableID); 
    myTable.style.left = GetElementLeft(myTable,DropTextBox);
    myTable.style.top = GetElementTop(myTable,DropTextBox);
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
	        cells[i].style.backgroundColor = 'buttonface';
	        }
        else
	        {
	        cells[i].Height = '0pt';
	        }
        }
}
	
	
function GetElementLeft(myDIV ,eElement)
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

function GetElementTop(myDIV, eElement)
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



function GridViewTrim(sString) 
{
    while (sString.substring(0,1) == ' ')
    {
    sString = sString.substring(1, sString.length);
    }
    while (sString.substring(sString.length-1, sString.length) == ' ')
    {
    sString = sString.substring(0,sString.length-1);
    }
return sString;

}


function GridViewFilterValue(ControlID)
{
    var tmp =document.getElementById(ControlID + '_SelectedText_txt');
    return tmp.innerText; 
}


function GridViewOnCallBackError(message, context) 
{
alert('An unhandled exception has occurred:\n' + message);
}

//function TRmouseevent(ControlID,borderstyle)
//{
//    //document.getElementById(ClearControlID).style.border='1px solid #ccc';
//    //document.getElementById(ClearControlID).style.border='1px solid Transparent';
//    document.getElementById(ControlID).style.border=borderstyle;
//    //ControlID.style.border=borderstyle;
//}

function TRmouseevent(ControlID,bkcolor)
{
    //document.getElementById(ClearControlID).style.border='1px solid #ccc';
    //document.getElementById(ClearControlID).style.border='1px solid Transparent';
    document.getElementById(ControlID).style.background=bkcolor;
    //ControlID.style.border=borderstyle;
}

function dropdowngridviewSetValue(ControlToClearID,Value,Text)
{
    document.getElementById(ControlToClearID +'_SelectedID_hdn').value = Value;
    document.getElementById(ControlToClearID +'_SelectedText_txt').innerText = Text;
    document.getElementById(ControlToClearID +'_SelectedText_hdn').value = Text;
}
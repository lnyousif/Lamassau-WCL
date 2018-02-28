
//function MSNLoadItems( sourceID, destinationID, add, remove, allowDuplicates, MultiSelectionsNodeId)
//{
//	var sourceList = document.getElementById(sourceID);
//	var destinationList = document.getElementById(destinationID);
//	var itemsForTransfer = new Array();
//    var selectedItemsList = document.getElementById(MultiSelectionsNodeId).value;
//	var value_array = selectedItemsList.split(',');
//	var i;
//	
//	alert(sourceList.options.length);
//	for(i = 0; i < sourceList.options.length; i++)
//	{
//		var option = sourceList.options[i];
//		if(MSNContains(value_array,option))
//		{
//			alert('MSNContains');
//			itemsForTransfer[itemsForTransfer.length] = new Option(option.text, option.value);
//			if(!remove)
//			{		
//				LT_UpdateState(destinationList,MultiSelectionsNodeId,option.value);
//			}
//		}
//	}
//	
//	if(remove)
//	{
//		for(i = 0; i < sourceList.options.length; i++){
//			var option = sourceList.options[i];
//			if(MSNContains(value_array,option))
//			{
//				LT_UpdateState(destinationList,MultiSelectionsNodeId,'--'+option.value);
//				sourceList.options[i] = null;
//				i--;
//			}
//		}
//	} 
//	
//	

//	for(i = 0; i < itemsForTransfer.length; i++)
//	{
//	
//		var option = itemsForTransfer[i];
//		if(add && (allowDuplicates || !LT_Contains(destinationList, option)))
//		{
//			destinationList.options[destinationList.options.length] = option;
//		}
//	}

//}

function MSNContains(ValueArray, option)
{
	var i;
	debugger;
	for(i = 0; i < ValueArray.length; i++)
	{
		if(ValueArray[i].value == option.value)
		{
			return true;
		}
	}
}




function MSNTransfer( sourceID, destinationID, selectedOnly, add, remove, allowDuplicates, MultiSelectionsNodeId)
{
	var sourceList = document.getElementById(sourceID);
	var destinationList = document.getElementById(destinationID);
	var itemsForTransfer = new Array();

	var i;
	
	for(i = 0; i < sourceList.options.length; i++)
	{
		var option = sourceList.options[i];
		if(option.selected || !selectedOnly)
		{
			itemsForTransfer[itemsForTransfer.length] = new Option(option.text, option.value);
			if(!remove)
			{		
				LT_UpdateState(destinationList,MultiSelectionsNodeId,option.value);
			}
		}
	}
	
	if(remove)
	{
		for(i = 0; i < sourceList.options.length; i++){
			var option = sourceList.options[i];
			if(option.selected || !selectedOnly)
			{
				LT_UpdateState(destinationList,MultiSelectionsNodeId,'--'+option.value);
				sourceList.options[i] = null;
				i--;
			}
		}
	} 

	for(i = 0; i < itemsForTransfer.length; i++)
	{
		var option = itemsForTransfer[i];
		if(add && (allowDuplicates || !LT_Contains(destinationList, option)))
		{
			destinationList.options[destinationList.options.length] = option;
		}
	}

}



function LT_Contains(list, option)
{
	var i;
	for(i = 0; i < list.options.length; i++)
	{
		if(list.options[i].value == option.value)
		{
			return true;
		}
	}
}


function LT_UpdateState(list,MultiSelectionsNodeId,value)
{
	
	var addIt='true'; 
	var i;
	var state = '';
	for(i = 0; i < list.options.length; i++)
	{
		if (value == list.options[i].value)
		{
			addIt='false';
		}
	}
		
	if (addIt == 'true')
	{
	    if ( document.getElementById(MultiSelectionsNodeId).value == null || document.getElementById(MultiSelectionsNodeId).value == '' )
	    {
	        document.all[MultiSelectionsNodeId].value += (value);
	    }
	    else
	    {
	        document.all[MultiSelectionsNodeId].value += (',' + value);
	    }
		
	}

}

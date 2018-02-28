
	function clip_note(parent_reference)
	{
		var note = document.getElementById(parent_reference.attributes['NoteID'].nodeValue);
		//var note = document.getElementById(parent_reference.id + '_note');				
		//alert(note.attributes['NoteStatus'].nodeValue);
		
	 
	 if (note.attributes['NoteStatus'].nodeValue !== 'clip')
	 {	
		note.attributes['NoteStatus'].nodeValue = 'clip'
		
		
		var note_close_btn = document.getElementById(note.id + '_close_div');
		
		note_close_btn.style.visibility = 'visible';
		note.style.display = 'block';

		note.style.top = DL_GetElementTop(note ,parent_reference);
		note.style.left = DL_GetElementLeft(note ,parent_reference);
		}
	}

	function hide_note(parent_reference)
	{ 
		var note = document.getElementById(parent_reference.attributes['NoteID'].nodeValue);
		if(note.attributes['NoteStatus'].nodeValue == 'view')
			{
				note.style.display = 'none';
			}
	}
	function close_note(close_btn)
	{
		var note = close_btn.parentNode.parentNode;
		note.attributes['NoteStatus'].nodeValue = 'view';
		
		//close_btn.style.visibility = 'hidden';
		note.style.display = 'none';
		
	}
	function show_note(parent_reference)
	{	
		var note = document.getElementById(parent_reference.attributes['NoteID'].nodeValue);
		if (note.attributes['NoteStatus'].nodeValue != 'clip')
			{
				note.attributes['NoteStatus'].nodeValue = 'view';		
				var note_content = document.getElementById(note.id + '_itself');
				var note_close_btn = document.getElementById(note.id + '_close_div');
				
				note.style.display = 'block';
				note.style.top = DL_GetElementTop(note,parent_reference);
				note.style.left = DL_GetElementLeft(note,parent_reference);
				note_close_btn.style.visibility = 'hidden';
				
				
			}
		
				
			
		
	}
	
	function DL_GetElementLeft(note ,eElement)
    {
    var nLeftPos = eElement.offsetLeft;      
    var eParElement = eElement.offsetParent;
    while (eParElement != null)
       {                                            
       nLeftPos += eParElement.offsetLeft;     
       eParElement = eParElement.offsetParent; 
       }
                  
	switch (eElement.attributes['AlignNote'].nodeValue)
		{
			case 'Left': 
				{
					nLeftPos = nLeftPos - note.offsetWidth;
					break 
				}
			case 'Right': 
				{ 
					nLeftPos = nLeftPos + eElement.offsetWidth;;
					break 
				}
			default: 
				{ 
					break 
				}
		}     
       
       
    return nLeftPos                             
    }

   function DL_GetElementTop(note, eElement)
   {
  
   var nTopPos = eElement.offsetTop;
   var eParElement = eElement.offsetParent;
   while (eParElement != null)
      {
      nTopPos += eParElement.offsetTop;
      eParElement = eParElement.offsetParent;
      }
   
   switch (eElement.attributes['vAlignNote'].nodeValue)
	{
		case 'Top': 
			{
				nTopPos = nTopPos - note.offsetHeight;
				break 
			}
		case 'Bottom': 
			{ 
				nTopPos = nTopPos + eElement.offsetHeight;
				break 
			}
		default: 
			{ 
				break 
			}
	}     
   return nTopPos ;
  }
function pdata(){
	var objs_two = util.getExreas();
	var op = {
    	Token:objs_two.Token,
    	Username:objs_two.Username,
    	id:vm.$data.id,
    	role:1
	};						
 	$.ajax({
 	url:"http://www.shiguanja.com/ERP_CRMLoad/DeletePicturesSS",
        type:"POST",
        data:op,
        dataType:"json",
        success: function (data) {
	        if(data.state == 1){					            
	            if(objs_two.Sunber==2){
	            	alert(data.msg);
	            	vm.$data.img_ = '';
	            	setTimeout(function(){
							$('#O_fileURL').hide();
							$('.imgfe').hide();
							$('.pop').text('');
						})
	            }else{
	            	alert(data.msg);
	            	vm.$data.img_ = '';		            	
	            	setTimeout(function(){
							$('#O_fileURL').hide();
							$('.imgfe').hide();
							$('.pop').text('');
						})
	            }
	           
            }else{							            	
            	alert(data.msg);
            }
        },
        error: function () {
            alert("网络错误！");
        	}
   })		
}
;(function($){
	if(!$){
		return console.error("No JQueyJs!");
	}
	var data = util.getExreas();
	var op = {
    	Token:data.Token,
    	Username:data.Username
  	};
	$("#upload").on("change",function(){
		var options = {
        url:"http://www.shiguanja.com/ERP_CRMLoad/PicPunlicSS",
        type: "POST",
        data:op,
        dataType: "json",
        success: function (data) {
            vm.$data.img_ = data.data;
            if(data.state == 1){								            	
	        	setTimeout(function(){
					$('#O_fileURL').css('display','inline-block');
					$('.imgfe').show();
				})        
            }else{
            	alert(data.msg)
            }
        },
        error: function () {
            alert("网络错误！");
        }
	    }
	    $("#updata").ajaxSubmit(options);
	})
})(jQuery);

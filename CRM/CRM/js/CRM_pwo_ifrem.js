function pwo1(){
		$.ajax({
			url:util.Config.url+'/Documentary/SearchIsPwdPRO',
			type:'post',//HTTP请求方式
			dataType:'json',//规定预期的服务器响应的数据类型
			async: false,//true表示发送异步请求，false表示发送同步请求
			cache:true,//true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
			contentType:"application/x-www-form-urlencoded",//发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
			data:{
				'Username':oves_.Username,
				'Token':oves_.Token,
				'pwd':$('#pwo').val()
			},
			success: function(data){//服务器响应成功并返回数据调用该	
				if(data.state==-1){
					window.parent.location.href = '../CRM/CRM_log.html';							
				}else if(data.state==1){					
						setTimeout(function(){
						$('.ok').css("background","#3a6c92");
						$('.ok').removeAttr('disabled');
						$('.pwo1').show();
						$('#img1').remove();
						$('#img2').remove();
						$('#pwo').after('<span id="img1" style="color:green;">√</span>');				
						})									
				}else if(data.state==2){
					setTimeout(function(){	
					$('.ok').css("background","#333333");
					$('.ok').attr('disabled','disabled');
					$('#img1').remove();
					$('#img2').remove();
					$('#pwo').after('<span id="img2" style="color:red;">X</span>');
					$('.pwo1').hide();
					})					
				}else{
					setTimeout(function(){	
					$('.ok').css("background","#333333");
					$('.ok').attr('disabled','disabled');
					$('#img1').remove();
					$('#img2').remove();
					$('#pwo').after('<span id="img2" style="color:red;">X</span>');
					$('.pwo1').hide();
					})		
				}
			},
			error: function(){
				console.log('error');
			}
		})
}
function pwo2(){
		$.ajax({
			url:util.Config.url+'/Documentary/UpdataPWD',
			type:'post',//HTTP请求方式
			dataType:'json',//规定预期的服务器响应的数据类型
			async: false,//true表示发送异步请求，false表示发送同步请求
			cache:true,//true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
			contentType:"application/x-www-form-urlencoded",//发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
			data:{
				'Username':oves_.Username,
				'Token':oves_.Token,
				'pwd':$('#pwo1').val(),
				'oldPwd':$('#pwo').val()
			},
			success: function(data){//服务器响应成功并返回数据调用该	
				if(data.state==-1){
					window.parent.location.href = '../CRM/CRM_log.html';							
				}else if(data.state==1){
					alert(data.msg);										
					window.parent.location.href = '../CRM/CRM_log.html';
				}else if(data.state==2){
					alert(data.msg)		
				}else{
					alert(data.msg);
				}
			},
			error: function(){
				console.log('error');
			}
		})
}
function jixi(){		
		var Username = objs_ove.Username;
		var Token = objs_ove.Token;
		$.ajax({
			url:util.Config.url+'/Documentary/Searchdocumentaryinfor',
			type:'post',//HTTP请求方式
			dataType:'json',//规定预期的服务器响应的数据类型
			async: false,//true表示发送异步请求，false表示发送同步请求
			cache:true,//true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
			contentType:"application/x-www-form-urlencoded",//发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
			data:{
				'Username':Username,
				'Token':Token
			},
			success: function(data){//服务器响应成功并返回数据调用该	
				if(data.state==1){																
					if(data.data.T_photoUrl.data == null){
						vm.$data.items = data.data;					
						vm.$data.ids =	objs_ove;
					}else{
						vm.$data.items = data.data;					
						vm.$data.ids =	objs_ove;
						var inm_ = data.data.T_photoUrl.data[0];
						$('.img_')[0].src = inm_.Url;
					}
				}else if(data.state==-1){
					alert(data.msg);
					window.parent.location.href = '../CRM/CRM_log.html';
				}else{
					alert(data.msg);
				}
			},
			error: function(){
				console.log('error');
			}
		})
	}
function xiugai(){
	var url = '../CRM/CRM_name.html';
	var ids = vm.$data.ids;
	util.openWindow(url,ids);
}

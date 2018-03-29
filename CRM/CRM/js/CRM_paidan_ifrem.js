	function flitrate(){
		var objs_ove = util.getExreas(null,true);
		vm.$data.itemst = {
			"Username":objs_ove.T_accountnumber,
			"Token":objs_ove.Token
		}	
	$.ajax({
				url:util.Config.url + '/Business/BussinessScreen',
				type:'post',//HTTP请求方式
				dataType:'json',//规定预期的服务器响应的数据类型
				async: false,//true表示发送异步请求，false表示发送同步请求
				cache:true,//true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
				data:{
					"Username":objs_ove.T_accountnumber,
					"Token":objs_ove.Token,
					"UserID":objs_ove.T_UserID,
					"rows":vm.$data.rows,
					"page":vm.$data.page,
					"status":$('#province').val(),
					"BussName":$('#B_name').val(),
					"Dates1":$('#Mindate').val(),
					"Dates2":$('#Maxdate').val()
				},
				contentType:"application/x-www-form-urlencoded",//发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
				success: function(data){//服务器响应成功并返回数据调用该
					if(data.state == -1){
					    window.parent.location.href = '../CRM/CRM_log.html';
					}else if(data.state == 1){
						extend(vm.$data.itemo,data.data);
						vm.$data.items = data.data;
						vm.$data.count = Math.ceil(data.count/vm.$data.rows);
						vm.$data.page = data.page;	
						pege();
						isH = false;
					}else{
						extend(vm.$data.itemo,data.data);
						vm.$data.items = data.data;
						vm.$data.count = Math.ceil(data.count/vm.$data.rows);
						vm.$data.page = data.page;
						alert(data.msg)
					}					
				},
				error: function(){
					console.log('error');
				}
			})
	}
	var isH = true;
			function pege(){
				if(isH){
						$(".tcdPageCode").createPage({		
							pageCount:vm.$data.count,
							current:vm.$data.page,
							backFn:function(p){
					        	vm.$data.page = p;
					        	flitrate();
					        }
					    });
				}
				}
	function filtrate_ove(){
			var objs_two = util.getExreas();
				$.ajax({
				url:util.Config.url + '/Business/SearchBussinessInfor',
				type:'post',//HTTP请求方式
				dataType:'json',//规定预期的服务器响应的数据类型
				async: false,//true表示发送异步请求，false表示发送同步请求
				cache:true,//true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
				data:{
					"Username":objs_two.Username,
					"Token":objs_two.Token,
					"ID":objs_two.ID
				},
				contentType:"application/x-www-form-urlencoded",//发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
				success: function(data){//服务器响应成功并返回数据调用该					
					if(data.state == -1){
					    window.parent.location.href = '../CRM/CRM_log.html';
					}else if(data.state == 1){
					vm.$data.items = data.data;
					
					}else{
						alert(data.msg)
					}
					var index = vm.$data.items.B_status;
					switch(index){
						case 0:{
							vm.$data.sallshow = false;
						};break;
						case 1:{
							$('#ove').attr('checked','checked');							
						};break;
						case 2:{
							$('#two').attr('checked','checked');
						};break;
						case 3:{
							$('#san').attr('checked','checked');
							vm.$data.sallshow = true;
						};break;
						default:break;
					}
				},
				error: function(){
					console.log('error');
				}
			})	
	}
	
function filtrate_oves(){
			var objs_san = util.getExreas();
				$.ajax({
				url:util.Config.url + '/Business/UpdataBusinessStatus',
				type:'post',//HTTP请求方式
				dataType:'json',//规定预期的服务器响应的数据类型
				async: false,//true表示发送异步请求，false表示发送同步请求
				cache:true,//true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
				data:{
					"Username":objs_san.Username,
					"Token":objs_san.Token,
					"ID":objs_san.ID,
					"B_status":vm.$data.index,
					"B_giveupcause":vm.$data.items.B_giveupcause
				},
				contentType:"application/x-www-form-urlencoded",//发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
				success: function(data){//服务器响应成功并返回数据调用该
					if(data.state == -1){
					    window.parent.location.href = '../CRM/CRM_log.html';
					}else if(data.state == 1||data.state == 2||data.state == 3){					
						alert(data.msg);	
						if(objs_san.Sunber == 1||objs_san.Sunber == 2){
							util.bcak();
						}else{
							window.location.href = '../CRM/CRM_filtrate.html'
						}
					}else{
						alert(data.msg)
					}					
				},
				error: function(){
					console.log('error');
				}
			})	
	}
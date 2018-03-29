function so_flitrate(){
		var objs_ove = util.getExreas(null,true);
		vm.$data.itemst = {
			"Username":objs_ove.T_accountnumber,
			"Token":objs_ove.Token,
			"UserID":objs_ove.T_UserID,
			"Addbusinessman":objs_ove.T_name
		}
	$.ajax({
				url:util.Config.url + '/Business/BussinessSendorders',
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
					"documentaryname":$('#documentaryname').val(),
					"BussName":$('#B_name').val(),
					"Dates1":$('#Mindate').val(),
					"Dates2":$('#Maxdate').val()
				},
				contentType:"application/x-www-form-urlencoded",//发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
				success: function(data){//服务器响应成功并返回数据调用该
					if(data.state == -1){
					    window.parent.location.href = '../CRM/CRM_log.html';
					}else if(data.state == 1){
						vm.$data.items = data.data;						
						vm.$data.count = Math.ceil(data.count/vm.$data.rows);
						vm.$data.page = data.page;
						pege();
						isH = false;
					}else{
						vm.$data.items = data.data;						
						vm.$data.count = Math.ceil(data.count/vm.$data.rows);
						vm.$data.page = data.page;
						pege();
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
					        	so_flitrate();
					        }
					   });
					}		
				}
//分页
				
function so_filtrate_oves(){
			var objs_san = util.getExreas();
			vm.$data.name = {
				"Username":objs_san.Username,
				"Token":objs_san.Token,
				"C_name":objs_san.C_name,
				"Addbusinessman":objs_san.Addbusinessman,
				"B_documentaryName":objs_san.B_documentaryName,
				"UserID":objs_san.UserID
			}
			if(objs_san.Sunber == 2){
				$('.zt').text('新增');
			}else{
				$('.zt').text('修改');
			}
				$.ajax({
				url:util.Config.url + '/Business/SearchBussinessInfor',
				type:'post',//HTTP请求方式
				dataType:'json',//规定预期的服务器响应的数据类型
				async: false,//true表示发送异步请求，false表示发送同步请求
				cache:true,//true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
				data:{
					"Username":objs_san.Username,
					"Token":objs_san.Token,
					"ID":objs_san.ID
				},
				contentType:"application/x-www-form-urlencoded",//发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
				success: function(data){//服务器响应成功并返回数据调用该
					if(data.state == -1){
					    window.parent.location.href = '../CRM/CRM_log.html';
					}else if(data.state == 1){
					vm.$data.items = data.data;	
					}else if(data.state == 2){
						alert(data.msg)
					}else{
						alert(data.msg)
					}
				},
				error: function(){
					console.log('error');
				}
			})	
	}
	function so_filtrate_ove(){
		var objs_two = util.getExreas();
		$.ajax({
				url:util.Config.url + '/Business/UpdataBusinessDocumentary',
				type:'post',//HTTP请求方式
				dataType:'json',//规定预期的服务器响应的数据类型
				async: false,//true表示发送异步请求，false表示发送同步请求
				cache:true,//true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
				data:{
					"Username":objs_two.Username,
					"Token":objs_two.Token,
					"ID":objs_two.ID,
					"Addbusinessman":objs_two.Addbusinessman,
					"B_documentaryID": $("#province").val(),
					"B_documentaryneed":vm.$data.items.B_documentaryneed
				},
				contentType:"application/x-www-form-urlencoded",//发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
				success: function(data){//服务器响应成功并返回数据调用该
					if(data.state == -1){
					    window.parent.location.href = '../CRM/CRM_log.html';
					}else if(data.state == 1){
						alert(data.msg)
					if(objs_two.Sunber == 1 ||objs_two.Sunber == 2){
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
	function so_out(){
		var objs_five = util.getExreas();
//		console.log(objs_five)
			window.onload=function(){			
				getPro(0,1000);
			}
				//三级联查
			function getPro() {
				var _data;
				var type = "name";
				var type_id = "ID";
				_data = {
					"Username":objs_five.Username,
					"Token":objs_five.Token,
					"UserID":objs_five.UserID,
				};
			 $.ajax({
                   	url:util.Config.url + '/Documentary/SearchUserBystatusStatus',
                    dataType: "json",
                    timeout: 10000,
                    data:_data,
                    success: function (data) {
                        $("#province").children().remove();
                        for (var i = 0; i < data.data.length; i++) {
                            var option_ = $("<option></option>");
                            option_.val(data.data[i][type_id]);
                            option_.html(data.data[i][type]);
                            $("#province").append(option_);
                        }
                    },
                    error: function (data) {

                        //三级联查失败
                        getPro();//
                    }
               })
			}
		}
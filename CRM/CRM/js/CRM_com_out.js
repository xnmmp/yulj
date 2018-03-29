
	function Shiwimg(){//放大镜客户查询
										
						$.ajax({
							url:util.Config.url+'/Customer/SearchByname',
							type:'post',//HTTP请求方式
							dataType:'json',//规定预期的服务器响应的数据类型
							async: false,//true表示发送异步请求，false表示发送同步请求
							cache:true,//true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
							data:{
								"Username":objs_ove.Username,
								"Token":objs_ove.Token,
								"C_name":$('#C_name').val(),
								"C_UserID":objs_ove.UserID
							},
							contentType:"application/x-www-form-urlencoded",//发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
							success: function(data){//服务器响应成功并返回数据调用该
								if(data.state == -1){
								    window.parent.location.href = '../CRM/CRM_log.html';
								}else if(data.state == 1){
										vm.$data.items = data.data;
										if(vm.$data.index == 0){
											vm.$data.willShow = true;
										}else{
											vm.$data.willShow = false;
										}
								}else{
										vm.$data.liShow = true;
								}						
							},
							error: function(){
								console.log('error');
							}
						})
					}
		//添加商机
				function inte(){	
					var C_name = $('#C_name').val();
					var C_address = $('#C_address').val();
					var B_source = $('#B_source').val();
					var B_kind = $('#B_kind').val();
					var B_servicekind1 = $("#B_servicekind1").val();
					var B_servicekind2 = $("#B_servicekind2").val();
					var B_name = $('#B_name').val();
					var B_neednumber = $('#B_neednumber').val();
					var B_needcause = $('#B_needcause').val();
					if(B_name == "" ||C_name == "" ||C_address == "" ||B_neednumber == ''|| B_needcause == ''
					||B_source == 0||B_kind == 0||B_servicekind1 == 0||B_servicekind2 == 0){
						alert('您还有重要的信息没有填写!');
						return;
					}else{
					 $.ajax({
						url:util.Config.url+'/Business/Addbusiness',
						type:'post',//HTTP请求方式
						dataType:'json',//规定预期的服务器响应的数据类型
						async: false,//true表示发送异步请求，false表示发送同步请求,
						data:{
							"Username":objs_ove.Username,
							"Token":objs_ove.Token,
							"B_name":B_name,
							"B_source":B_source,
							"B_kind":B_kind,
							"B_servicekind1":B_servicekind1,
							"B_servicekind2":B_servicekind2,
							"B_needcause":B_needcause,
							"B_neednumber":B_neednumber,
							"Addbusinessman":objs_ove.Addbusinessman,
							"CustomerID":vm.$data.itemst.ID,
							"B_fistuseerID":objs_ove.UserID
						},
						cache:true,//true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
						contentType:"application/x-www-form-urlencoded",//发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
						success: function(data){//服务器响应成功并返回数据调用该
//							vm.$data.items = data;	
							if(data.state==-1){
							    window.parent.location.href = '../CRM/CRM_log.html';
							}else if(data.state == 1){															
								alert(data.msg);
								util.bcak();	
							}else{
								alert(data.msg);
								vm.$data.willShow = false;
							}							 
						},
						error: function(){
							console.log('error');
						}
					})
				}
			}
				//修改
				
				function comShow(){
					
					$.ajax({ //商机查询
						url: util.Config.url + '/Business/SearchBussinessInfor',
						type: 'post', //HTTP请求方式vs
						dataType: 'json', //规定预期的服务器响应的数据类型
						async: false, //true表示发送异步请求，false表示发送同步请求
						data: {
							"Username": objs_ove.Username,
							"Token": objs_ove.Token,
							"ID": objs_ove.ID
						},
						contentType: "application/x-www-form-urlencoded", //发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
						success: function(data) { //服务器响应成功并返回数据调用该
							extend(vm.$data.itemo,data.data);
							vm.$data.itemse = data.data;
//							console.log(vm.$data.itemse.CustomerID)
							vm.$data.item = data.data.Kinds;
							window.citys = data.data.Kinds;
							getProX(0,1000);
						},
						error: function() {
							console.log('error');
						}
					})
				}
				function extend(p,c){
						for(var itemo in c){
							p[itemo] = c[itemo];
						}
					}
				//时间搓处理
//					function changeDate(){
//						var date = vm.$data.itemse.B_datetime;
//						var d = date;
//						var b = d.replace(/[^0-9]+/ig,"");
//						return new Date(parseInt(b));
//					}
				
				//修改
				function intes(){	
					var C_name = $('#C_name').val();
					var C_address = $('#C_address').val();
					var B_source = $('#B_source').val();
					var B_kind = $('#B_kind').val();
					var B_servicekind1 = $("#B_servicekind1").val();
					var B_servicekind2 = $("#B_servicekind2").val();
					var B_name = $('#B_name').val();
					var B_neednumber = $('#B_neednumber').val();
					var B_needcause = $('#B_needcause').val();
					if(B_name == "" ||C_name == "" ||C_address == "" ||B_neednumber == ''|| B_needcause == ''
					||B_source == 0||B_kind == 0||B_servicekind1 == 0||B_servicekind2 == 0){
						alert('您还有没输入必要的信息!');
						return;
					}else{
					 $.ajax({
						url:util.Config.url+'/Business/UpdataBusiness',
						type:'post',//HTTP请求方式
						dataType:'json',//规定预期的服务器响应的数据类型
						async: false,//true表示发送异步请求，false表示发送同步请求,
						data:{
							"Username":objs_ove.Username,
							"Token":objs_ove.Token,
							"ID":objs_ove.ID,
							"B_name":B_name,
							"B_source":B_source,
							"B_kind":B_kind,
							"B_servicekind1":B_servicekind1,
							"B_servicekind2":B_servicekind2,
							"B_needcause":B_needcause,
							"B_neednumber":B_neednumber,
							"CustomerID":vm.$data.itemse.CustomerID
						},
						cache:true,//true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
						contentType:"application/x-www-form-urlencoded",//发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
						success: function(data){//服务器响应成功并返回数据调用该
							console.log(data.data)
							if(data.state==-1){
							    window.parent.location.href = '../CRM/CRM_log.html';
							}else if(data.state == 1){
								alert(data.msg);
								util.bcak();
//								window.location.href='CRM_co.html'
							}else{
								alert(data.msg)
							}						 
						},
						error: function(){
							console.log('error');
						}
					})
				}
			}	
//二级联查
function Hits(){
		window.onload=function(){			
				getPro(0,1000);
			}
			$("#three>select").on("change", function() {				
					var childId = $(this).val();
					var index = $(this).index()+1;
					if(index<2){
						getPro(index,childId);
					}
				})
				//三级联查
			function getPro(index, childId) {
				var _data;
				var type = "Text";
				var type_id = "ID";
				_data = {
					"Username":objs_ove.Username,
					"Token":objs_ove.Token,
					"index": index,
					"childId": childId
				};
				var obj;
				switch(index) {
					case 0:
						{
							obj = $("#B_servicekind1");
						};
						break;
					case 1:
						{
							type = "Text";
							type_id = "ID";
							obj = $("#B_servicekind2");
						};
						break;
						default:{
							return ;
						}
						break;
				}
				$.ajax({
					url: util.Config.url + '/Servicekind/ServiceOneSerchTwo',
					type:"post",
					dataType: "json",
					timeout: 10000,
					data: _data,
					success: function(data) {	
						if(data.state == 1){
							$(obj).children().remove();
							if(index == 0){
								$(obj).append('<option id="op1" value="">请选择</option>');
							}else{
								$('#op1').remove();	
							}
							for(var i = 0; i < data.data.length; i++) {	
								var option_ = $("<option></option>");
								option_.val(data.data[i][type_id]);
								option_.html(data.data[i][type]);
								$(obj).append(option_);
							}
						}
						
					},
					error: function(data) {
						//三级联查失败
						getPro(); //
					}
				})
			}
		}
			function Hit(){
			var province = $("#province").val();
			var city = $("#city").val();
			var district = $("#district").val();
			$("#three>select").on("change", function() {				
					var childId = $(this).val();
					var index = $(this).index()+1;
					citys = null;
					if(index<2){
						getProX(index,childId);
					}
				})
			}
		function getProX(index, childId) {
				var _data;
				var type = "Text";
				var type_id = "ID";
				var show = "so";
				var val = "ID";
				_data = {
					"Username":objs_ove.Username,
					"Token":objs_ove.Token,
					"index": index,
					"childId": childId
				};
				var obj;
				switch(index) {
					case 0:
						{
							obj = $("#B_servicekind1");							
						};
						break;
					case 1:
						{
							type = "Text";
							type_id = "ID";
							obj = $("#B_servicekind2");
							show = "st";
							val = "ID";
						};
						break;
						default:{
							return ;
						}
						break;
				}
		$.ajax({
			url: util.Config.url + '/Servicekind/ServiceOneSerchTwo',
			type:"post",
			dataType: "json",
			timeout: 10000,
			data: _data,
			success: function(data) {	
				if(data && data.state==1){
					$(obj).html("");//
					for(var i = 0; i < data.data.length; i++) {	
						var option_ = $("<option></option>");
						if(citys && data.data[i][type_id] == citys[show][val]){
							
							option_.attr("selected","selected");
						}
						option_.val(data.data[i][type_id]);
						option_.html(data.data[i][type]);
						$(obj).append(option_);						
					}
					if(index<2){
						getProX((index+1),((citys && citys[show][val])||data.data[0][type_id]));
					}
				}
				
			},
			error: function(data) {
				//三级联查失败
				getProX(); //
			}
		})
}
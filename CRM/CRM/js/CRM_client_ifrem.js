		function intes(){
					$.ajax({
					url:util.Config.url+'/Customer/loadCustomer',
					type:'post',//HTTP请求方式
					dataType:'json',//规定预期的服务器响应的数据类型
					async: false,//true表示发送异步请求，false表示发送同步请求
					cache:true,//true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
					data:{
						"Username":objs_oves.T_accountnumber,
						"Token":objs_oves.Token,
						"rows":vm.$data.rows,
						"page":vm.$data.page,
						"UserID":objs_oves.T_UserID,
						"customerName":$('#C_name').val(),
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
							        	intes();
							        }
							    });
							}
						}
		//二级跳转
		
		function clients(){
				var objs_ovet = util.getExreas();
					$.ajax({
					url:util.Config.url+"/Customer/SearchCustomerAllByID",
					type:'post',//HTTP请求方式
					dataType:'json',//规定预期的服务器响应的数据类型
					async: false,//true表示发送异步请求，false表示发送同步请求
					cache:true,//true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
					data:{
						"Username":objs_ovet.Username,
						"Token":objs_ovet.Token,
						"ID":objs_ovet.ID
					},
					contentType:"application/x-www-form-urlencoded",//发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
					success: function(data){//服务器响应成功并返回数据调用该
						extend(vm.$data.itemo, data.data);
						vm.$data.items = data.data;	
						vm.$data.item = data.data.city;
						window.citys = data.data.city;
						getProX(0,1000);
					},
					error: function(){
						console.log('error');
					}
				})
			}	
		function client(){
			$.ajax({
					type: "post", //html.datas.params
					url: util.Config.url + '/Customer/AddCustomer',
					dataType: "json",
					data: {
						"Username":objs_ove.Username,
						"Token":objs_ove.Token,
						"C_UserID":objs_ove.UserID,
						"C_name":vm.$data.items.C_name,
						"C_address":vm.$data.items.C_address,
						"C_linkname":vm.$data.items.C_linkname,
						"C_linkphone":vm.$data.items.C_linkphone,
						"C_province":$("#province").val(),
						"C_city":$("#city").val(),	
						"C_district":$("#district").val(),
						"youxian":$('#youxian').val(),
						"C_kind":$('#C_kind').val(),
						"C_cooperation":$('#C_cooperation').val(),
						"C_scale":$('#C_scale').val(),
						"C_industry":vm.$data.items.C_industry						
						},
						async: true,
						success: function(data) {
							if(data.state == -1){
								
								window.parent.location.href = '../CRM/CRM_log.html';
							}else if(data.state == 1){
//								vm.$data.items.youxian = 
								alert(data.msg);
								util.bcak();							
							}else{
								alert(data.msg+"全部必填!");
							}
						},
						error: function(data) {
							console.log(data)
						}
					});
			}
		function clientp(){
			$.ajax({
					type: "post", //html.datas.params
					url: util.Config.url + '/Customer/UpdataCustomer',
					dataType: "json",
					data: {
						"Username":objs_ove.Username,
						"Token":objs_ove.Token,
						"ID":objs_ove.ID,
						"C_name":vm.$data.items.C_name,
						"C_address":vm.$data.items.C_address,
						"C_linkname":vm.$data.items.C_linkname,
						"C_linkphone":vm.$data.items.C_linkphone,
						"C_province":$("#province").val(),
						"C_city":$("#city").val(),	
						"C_district":$("#district").val(),
						"C_kind":$('#C_kind').val(),
						"C_cooperation":$('#C_cooperation').val(),
						"C_scale":$('#C_scale').val(),
						"C_industry":vm.$data.items.C_industry						
						},
						async: true,
						success: function(data) {
							if(data.state == -1){
								
								window.parent.location.href = '../CRM/CRM_log.html';
							}else if(data.state == 1){
								alert(data.msg);
								location.href = 'CRM_client.html';							
							}else{
								alert(data.msg+"全部必填!");
							}
						},
						error: function(data) {
							console.log(data)
						}
					});
			}
		function widom(){
			if(objs_ove.Sunber == 2){
				vm.$data.zt = '新增';
				Hits();
			}else{
				vm.$data.zt = '修改';
				clients();
				Hit();				
			}
		}
		
		function Hits(){
			var province = $("#province").val();
			var city = $("#city").val();
			var district = $("#district").val();
			window.onload=function(){			
				getPro(0,1000);
			}
			$(".ul_two>#three>select").on("change", function() {			
					var childId = $(this).val();
					var index = $(this).index()+1;
					if(index<3){
						getPro(index,childId);
					}
				})
				//三级联查
			function getPro(index, childId) {
				var _data;
				var type = "province";
				var type_id = "provinceid";
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
							obj = $("#province");
						};
						break;
					case 1:
						{
							type = "city";
							type_id = "cityid";
							obj = $("#city");
							
						};
						break;
					case 2:
						{
							obj = $("#district");
							type = "area";
							type_id = "areaid";
						};break;
						default:{
							return ;
						}
						break;
				}
				$.ajax({
					url: util.Config.url + '/Servicekind/AdressSearch',
					type:"post",
					dataType: "json",
					timeout: 10000,
					data: _data,
					success: function(data) {
							$(obj).children().remove();							
							$(obj).append("<option value=''>请选择</option>");
							for(var i = 0; i < data.data.length; i++) {	
							var option_ = $("<option></option>");
							option_.val(data.data[i][type_id]);
							option_.html(data.data[i][type]);
							$(obj).append(option_);						
							}														
					},
					error: function(data) {
						//三级联查失败
						getPro(); //
					}
				})
			}
		}
		//selected="selected"
		function Hit(){
			var province = $("#province").val();
			var city = $("#city").val();
			var district = $("#district").val();		
			$(".ul_two>#three>select").on("change", function() {			
					var childId = $(this).val();
					var index = $(this).index()+1;
					citys = null;
					if(index<3){
						getProX(index,childId);
					}
			})
		}
function getProX(index, childId) {
		var _data;
		var type = "province";
		var type_id = "provinceid";
		var show = "provinces";
		var val = "provinceid";
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
					obj = $("#province");
				};
				break;
			case 1:
				{
					type = "city";
					type_id = "cityid";
					obj = $("#city");
					show = "cities";
					val = "cityid";
				};
				break;
			case 2:
				{
					obj = $("#district");
					type = "area";
					type_id = "areaid";
					show = "areass";
					val = "areaid";
				};break;
				default:{
					return ;
				}
				break;
		}
		$.ajax({
			url: util.Config.url + '/Servicekind/AdressSearch',
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
					if(index<3){
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
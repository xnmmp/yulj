
function as_flitrate(){
		$.ajax({
				url:util.Config.url + '/CRM_projectassess/projectassess',
				type:'post',//HTTP请求方式
				dataType:'json',//规定预期的服务器响应的数据类型
				async: false,//true表示发送异步请求，false表示发送同步请求
				cache:true,//true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
				data:{
					"Username":objs_ove.T_accountnumber,
					"Token":objs_ove.Token,
					"UserID":objs_ove.T_UserID,
					"A_documentaryID":objs_ove.ID,
					"rows":vm.$data.rows,
					"page":vm.$data.page,
					"customerName":$('#C_name').val(),
					"BussName":$('#B_name').val(),
					"Dates1":$('#Mindate').val(),
					"Dates2":$('#Maxdate').val(),
					"bussmass1":$('#Minmass').val(),
					"bussmass2":$('#Maxmass').val(),
					"Over1":$('#MinOve').val(),
					"Over2":$('#MaxOve').val()
				},
				contentType:"application/x-www-form-urlencoded",//发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
				success: function(data){//服务器响应成功并返回数据调用该
//					console.log(data.data)
					if(data.state == -1){
						window.parent.location.href = '../CRM/CRM_log.html';
					}else if(data.state == 1){
						vm.$data.items = data.data;
						vm.$data.count = Math.ceil(data.count/vm.$data.rows);
						vm.$data.page = data.page;
						pege();
						isH = false;
							for(var i = 0;i<vm.$data.items.length;i++){
								var CV_datetime = vm.$data.items[i].CV_datetime;
								var O_date = CV_datetime.replace(/[^0-9]+/ig,"");
								var O_dates = new Date(parseInt(O_date));
								var dates = O_dates.getFullYear() + '-' + (O_dates.getMonth() + 1) + '-' + 
								O_dates.getDate() + ' ' + O_dates.getHours() + ':' + 
								O_dates.getMinutes() + ':' + O_dates.getSeconds();
								vm.$data.items[i].CV_datetime = dates;							
							}	
					}else{
						vm.$data.items = data.data;
						vm.$data.count = Math.ceil(data.count/vm.$data.rows);
						vm.$data.page = data.page;
						pege();
						alert(data.msg)
					}
//					
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
							        	as_flitrate();
							        }
							   });
							}
					}
//时间搓处理
					function changeDate(){
						var d = vm.$data.date;
						var b = d.replace(/[^0-9]+/ig,"");
						return new Date(parseInt(b));
					}
function as_flitrate_ove(){
	var objs_oves = util.getExreas();
				$.ajax({
				url:util.Config.url + '/CRM_projectassess/projectassessInfor',
				type:'post',//HTTP请求方式
				dataType:'json',//规定预期的服务器响应的数据类型
				async: false,//true表示发送异步请求，false表示发送同步请求
				cache:true,//true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
				data:{
					"Username":objs_oves.Username,
					"Token":objs_oves.Token,
					"ID":objs_oves.ID
				},
				contentType:"application/x-www-form-urlencoded",//发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
				success: function(data){//服务器响应成功并返回数据调用该
//					console.log(data.data)
					if(data.state == -1){
						window.parent.location.href = '../CRM/CRM_log.html';
					}else if(data.state == 2){
						alert(data.msg);
					}else if(data.state == 1){
						if(objs_oves.Sunber == 1){							
							extend(vm.$data.items,data.data);
							vm.$data.item = data.data;
							numbers();
						}else{
							extend(vm.$data.items,data.data);
							vm.$data.item = data.data;
						}												
					}else{
						alert(data.msg)
					}
//					
				},
				error: function(){
					console.log('error');
				}
			})	
		}
					
function as_flitrate_two(){
					var objs_oves = util.getExreas();
					var objst = JSON.parse(vm.$data.itemnt);
					var A_businessmasst	= vm.$data.Sunber.A_businessmass;
					var A_overordert = vm.$data.Sunber.A_overorder;
					var A_durationneed = $("#A_durationneed").val();
					var A_constructneed = $("#A_constructneed").val();
					var A_acceptanceneed = $("#A_acceptanceneed").val();
					var A_defaultmakeup = $("#A_defaultmakeup").val();
					var A_aptitudes = $("#A_aptitudes").val();
					var A_fundsbackfront = $("#A_fundsbackfront").val();
					var A_projectscale = $("#A_projectscale").val();
					var A_budget = $("#A_budget").val();
					var A_compete = $("#A_compete").val();
					var A_consumerkey = $("#A_consumerkey").val();
					var A_kickbackneed = $("#A_kickbackneed").val();
					var A_consumerFocus = $("#A_consumerFocus").val();
					var A_competitiveness = $("#A_competitiveness").val();
					var A_projectdrag = $("#A_projectdrag").val();
					var A_conclusion = $("#A_conclusion").val();
					if (A_conclusion == 0) {
					    alert("请选择评估结论!");
		            return;
	            }else{	
	            $.ajax({
				url:util.Config.url + '/CRM_projectassess/Updataprojectassess',
				type:'post',//HTTP请求方式
				dataType:'json',//规定预期的服务器响应的数据类型
				async: false,//true表示发送异步请求，false表示发送同步请求
				cache:true,//true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
				data:{
					"Username":objs_oves.Username,
					"Token":objs_oves.Token,
					"ID":objs_oves.ID,
					"A_durationneed":$("#A_durationneed").val(),
					"A_constructneed":$("#A_constructneed").val(),
					"A_acceptanceneed":$("#A_acceptanceneed").val(),
					"A_defaultmakeup":$("#A_defaultmakeup").val(),
					"A_aptitudes":$("#A_aptitudes").val(),
					"A_fundsbackfront":$("#A_fundsbackfront").val(),
					"A_projectscale":$("#A_projectscale").val(),
					"A_businessmass":vm.$data.item.A_businessmass,
					"A_budget":$("#A_budget").val(),
					"A_compete":$("#A_compete").val(),
					"A_consumerkey":$("#A_consumerkey").val(),
					"A_kickbackneed":$("#A_kickbackneed").val(),
					"A_consumerFocus":$("#A_consumerFocus").val(),
					"A_competitiveness":$("#A_competitiveness").val(),
					"A_projectdrag":$("#A_projectdrag").val(),
					"A_overorder":vm.$data.item.A_overorder,
					"A_conclusion":$("#A_conclusion").val(),
				},
				contentType:"application/x-www-form-urlencoded",//发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
				success: function(data){//服务器响应成功并返回数据调用该
					
					if(data.state == -1){
						window.parent.location.href = '../CRM/CRM_log.html';
					}else if(data.state == 1){
						alert(data.msg);
						if(objs_oves.Sunber==2||objs_oves.Sunber==1){
							util.bcak();
						}else{
						window.location.href = '../CRM/CRM_as.html';
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
}
//添加

function as_flitrate_twos(){
					var A_durationneed = $("#A_durationneed").val();
					var A_constructneed = $("#A_constructneed").val();
					var A_acceptanceneed = $("#A_acceptanceneed").val();
					var A_defaultmakeup = $("#A_defaultmakeup").val();
					var A_aptitudes = $("#A_aptitudes").val();
					var A_fundsbackfront = $("#A_fundsbackfront").val();
					var A_projectscale = $("#A_projectscale").val();
					var A_budget = $("#A_budget").val();
					var A_compete = $("#A_compete").val();
					var A_consumerkey = $("#A_consumerkey").val();
					var A_kickbackneed = $("#A_kickbackneed").val();
					var A_consumerFocus = $("#A_consumerFocus").val();
					var A_competitiveness = $("#A_competitiveness").val();
					var A_projectdrag = $("#A_projectdrag").val();
					var A_conclusion = $("#A_conclusion").val();
					if (A_conclusion == 0) {
		        alert("请选择评估结论!");
		        return;
	            }else{	
	            $.ajax({
				url:util.Config.url + '/CRM_projectassess/Addprojectassess',
				type:'post',//HTTP请求方式
				dataType:'json',//规定预期的服务器响应的数据类型
				async: false,//true表示发送异步请求，false表示发送同步请求
				cache:true,//true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
				data:{
					"Username":objs_oves.Username,
					"Token":objs_oves.Token,
					"ID":objs_oves.ID,
					"A_durationneed":$("#A_durationneed").val(),
					"A_constructneed":$("#A_constructneed").val(),
					"A_acceptanceneed":$("#A_acceptanceneed").val(),
					"A_defaultmakeup":$("#A_defaultmakeup").val(),
					"A_aptitudes":$("#A_aptitudes").val(),
					"A_fundsbackfront":$("#A_fundsbackfront").val(),
					"A_projectscale":$("#A_projectscale").val(),
					"A_businessmass":vm.$data.item.A_businessmass,
					"A_budget":$("#A_budget").val(),
					"A_compete":$("#A_compete").val(),
					"A_consumerkey":$("#A_consumerkey").val(),
					"A_kickbackneed":$("#A_kickbackneed").val(),
					"A_consumerFocus":$("#A_consumerFocus").val(),
					"A_competitiveness":$("#A_competitiveness").val(),
					"A_projectdrag":$("#A_projectdrag").val(),
					"A_overorder":vm.$data.item.A_overorder,
					"A_conclusion":$("#A_conclusion").val(),
					"A_businessID":objs_oves.A_businessID,
					"A_customerID":objs_oves.A_customerID,
					"A_documentaryID":objs_oves.A_documentaryID,
					"UserID":objs_oves.UserID
				},
				contentType:"application/x-www-form-urlencoded",//发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
				success: function(data){//服务器响应成功并返回数据调用该
					console.log(data.data)
					if(data.state == 0){
						
						window.parent.location.href = '../CRM/CRM_log.html';
					}else if(data.state == 1){
						alert(data.msg);
						if(objs_oves.Sunber==2||objs_oves.Sunber==1){
							util.bcak();
						}else{
							window.location.href = '../CRM/CRM_as.html';
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
	}
function numbers(){
					
					vm.$data.item.A_businessmass = parseInt(vm.$data.item.A_durationneed)+parseInt(vm.$data.item.A_constructneed)+
					parseInt(vm.$data.item.A_acceptanceneed)+parseInt(vm.$data.item.A_defaultmakeup)+parseInt(vm.$data.item.A_aptitudes)+
					parseInt(vm.$data.item.A_fundsbackfront)+parseInt(vm.$data.item.A_projectscale)+parseInt(vm.$data.item.A_budget);
					
					
					vm.$data.item.A_overorder = parseInt(vm.$data.item.A_compete)+parseInt(vm.$data.item.A_consumerkey)+
					parseInt(vm.$data.item.A_kickbackneed)+parseInt(vm.$data.item.A_consumerFocus)+parseInt(vm.$data.item.A_competitiveness)+
					parseInt(vm.$data.item.A_projectdrag);
					vm.$data.itemnt = JSON.stringify(vm.$data.item);
					
			}
function numberst(){
					vm.$data.Sunber.A_businessmass = parseInt(vm.$data.itemnt.A_durationneed)+parseInt(vm.$data.itemnt.A_constructneed)+
					parseInt(vm.$data.itemnt.A_acceptanceneed)+parseInt(vm.$data.itemnt.A_defaultmakeup)+parseInt(vm.$data.itemnt.A_aptitudes)+
					parseInt(vm.$data.itemnt.A_fundsbackfront)+parseInt(vm.$data.itemnt.A_projectscale)+parseInt(vm.$data.itemnt.A_budget);
					
					
					vm.$data.Sunber.A_overorder = parseInt(vm.$data.itemnt.A_compete)+parseInt(vm.$data.itemnt.A_consumerkey)+
					parseInt(vm.$data.itemnt.A_kickbackneed)+parseInt(vm.$data.itemnt.A_consumerFocus)+parseInt(vm.$data.itemnt.A_competitiveness)+
					parseInt(vm.$data.itemnt.A_projectdrag);
					vm.$data.itemnt = JSON.stringify(vm.$data.items);
			}
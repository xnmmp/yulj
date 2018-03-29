//商机详情

function com(){
					vm.$data.itemst = objs_ove;					
					$.ajax({ //商机查询
						url: util.Config.url + '/Business/SearchBussinessInfor',
						type: 'post', //HTTP请求方式vs
						dataType: 'json', //规定预期的服务器响应的数据类型
						async: false, //true表示发送异步请求，false表示发送同步请求
						cache: true, //true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
						data: {
							"Username": objs_ove.Username,
							"Token": objs_ove.Token,
							"ID": objs_ove.IDs
						},
						contentType: "application/x-www-form-urlencoded", //发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
						success: function(data) { //服务器响应成功并返回数据调用该
							if(data.state == -1){
							    window.parent.location.href = '../CRM/CRM_log.html';
							}else if(data.state == 1){
								extend(vm.$data.itemo,data.data);
								vm.$data.items = data.data;
							//时间搓处理
							var p = changeDate();
							var dates = p.getFullYear() + '-' + (p.getMonth() + 1) + '-' + 
							p.getDate() + ' ' + p.getHours() + ':' + 
							p.getMinutes() + ':' + p.getSeconds();
							vm.$data.date = dates;
							}else{
								alert(data.msg)
							}
							var index = vm.$data.items.B_status;
					switch(index){
						case 0:{
							vm.$data.sallshow = false;
						};break;
						case 1:{
							$('#texta').css("opacity","0");
							$('#ove').attr('checked','checked');							
						};break;
						case 2:{
							$('#texta').css("opacity","0");
							$('#two').attr('checked','checked');
						};break;
						case 3:{
							$('#san').attr('checked','checked');
							vm.$data.sallshow = true;
							vm.$data.h1show2 = false;
							vm.$data.h1show3 = false;
							vm.$data.tallshow1 = false;
						};break;
						default:break;
					}
						},
						error: function() {
							console.log('error');
						}
					})
					//时间搓处理
					function changeDate(){
						var date = vm.$data.items.B_datetime;
						var d = date;
						var b = d.replace(/[^0-9]+/ig,"");
						return new Date(parseInt(b));
						}
					}
					function extend(p,c){
						for(var itemo in c){
							p[itemo] = c[itemo];
						}
					}
//更改派单
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
					"ID":objs_san.IDs,
					"B_status":vm.$data.index,
					"B_giveupcause":vm.$data.items.B_giveupcause
				},
				contentType:"application/x-www-form-urlencoded",//发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
				success: function(data){//服务器响应成功并返回数据调用该
					if(data.state == -1){
					    window.parent.location.href = '../CRM/CRM_log.html';
					}else if(data.state == 1||data.state == 2||data.state == 3){
						alert(data.msg);
						
					}else{
						alert(data.msg)
					}					
				},
				error: function(){
					console.log('error');
				}
			})	
	}
	//列表
	function PV_ifrems(){
			$.ajax({
				url:util.Config.url + '/CRM_visit/VisitByBusinessID',
				type:'post',//HTTP请求方式
				dataType:'json',//规定预期的服务器响应的数据类型
				async: false,//true表示发送异步请求，false表示发送同步请求
				cache:true,//true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
				data:{
					"Username":objs_ove.Username,
					"Token":objs_ove.Token,
					"CV_businessID":objs_ove.IDs														
				},
				contentType:"application/x-www-form-urlencoded",//发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
				success: function(data){//服务器响应成功并返回数据调用该
					if(data.state == -1 ){
					    window.parent.location.href = '../CRM/CRM_log.html';
					}else if(data.state == 1){
					    for (var i = 0; i < data.data.length; i++) {
					        var CV_datetime = data.data[i].CV_datetime;
					        var O_date = CV_datetime.replace(/[^0-9]+/ig, "");
					        var O_dates = new Date(parseInt(O_date));
					        var dates = O_dates.getFullYear() + '-' + (O_dates.getMonth() + 1) + '-' +
                            O_dates.getDate() + ' ' + O_dates.getHours() + ':' +
                            O_dates.getMinutes() + ':' + O_dates.getSeconds();
					        data.data[i].CV_datetime = dates;

					    }
					    vm.$data.pv = data.data;
					}else{
						alert(data.msg)
					}					
				},
				error: function(){
					console.log('error');
				}
			})
		}
					//时间搓处理
					function changeDate(){
						var date = vm.$data.items.B_datetime;		
						var b = date.replace(/[^0-9]+/ig,"");
						return new Date(parseInt(b));
					}
					//评估
	function as_flitrate_ove(){
				$.ajax({
				url:util.Config.url + '/CRM_projectassess/projectassessInforBybuss',
				type:'post',//HTTP请求方式
				dataType:'json',//规定预期的服务器响应的数据类型
				async: false,//true表示发送异步请求，false表示发送同步请求
				cache:true,//true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
				data:{
					"Username":objs_ove.Username,
					"Token":objs_ove.Token,
					"A_businessID":objs_ove.IDs
				},
				contentType:"application/x-www-form-urlencoded",//发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
				success: function(data){//服务器响应成功并返回数据调用该
//					console.log(data.data)
					if(data.state == -1){
					    window.parent.location.href = '../CRM/CRM_log.html';
					}else if(data.state == 2){
						alert("没有找到该详情，请添加！")
					}else if(data.state == 1){
							extend(vm.$data.itemo,data.data);
							vm.$data.as = data.data;
							numbers();	
//							switch_oves();
					}else{
						alert(data.msg)
					}
				},
				error: function(){
					console.log('error');
				}
			})	
		}
	function numbers(){
					
					vm.$data.as.A_businessmass = parseInt(vm.$data.as.A_durationneed)+parseInt(vm.$data.as.A_constructneed)+
					parseInt(vm.$data.as.A_acceptanceneed)+parseInt(vm.$data.as.A_defaultmakeup)+parseInt(vm.$data.as.A_aptitudes)+
					parseInt(vm.$data.as.A_fundsbackfront)+parseInt(vm.$data.as.A_projectscale)+parseInt(vm.$data.as.A_budget);
					
					
					vm.$data.as.A_overorder = parseInt(vm.$data.as.A_compete)+parseInt(vm.$data.as.A_consumerkey)+
					parseInt(vm.$data.as.A_kickbackneed)+parseInt(vm.$data.as.A_consumerFocus)+parseInt(vm.$data.as.A_competitiveness)+
					parseInt(vm.$data.as.A_projectdrag);
					
			}
	//报价
	function baoj_flitrate_ove(){
				$.ajax({
				url:util.Config.url + '/CRM_QProgram/QprogramByBuss',
				type:'post',//HTTP请求方式
				dataType:'json',//规定预期的服务器响应的数据类型
				async: false,//true表示发送异步请求，false表示发送同步请求
				cache:true,//true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
				data:{
					"Username":objs_ove.Username,
					"Token":objs_ove.Token,
					"O_businessID":objs_ove.IDs
				},
				contentType:"application/x-www-form-urlencoded",//发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
				success: function(data){//服务器响应成功并返回数据调用该
					if(data.state == -1){
					    window.parent.location.href = '../CRM/CRM_log.html';
					}else if(data.state == 2){
						alert(data.msg)
					}else if(data.state == 1){
							vm.$data.baoj = data.data;
							for(var i = 0;i<vm.$data.baoj.length;i++){
								var O_datetime = vm.$data.baoj[i].O_datetime;
								var O_date = O_datetime.replace(/[^0-9]+/ig,"");
								var O_dates = new Date(parseInt(O_date));
								var dates = O_dates.getFullYear() + '-' + (O_dates.getMonth() + 1) + '-' + 
								O_dates.getDate() + ' ' + O_dates.getHours() + ':' + 
								O_dates.getMinutes() + ':' + O_dates.getSeconds();
								vm.$data.baoj[i].O_datetime = dates;								
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
	//合同详情
	function bg_flitrate_ove(){
				$.ajax({
				url:util.Config.url + '/CRM_contract/ContractInforByBuss',
				type:'post',//HTTP请求方式
				dataType:'json',//规定预期的服务器响应的数据类型
				async: false,//true表示发送异步请求，false表示发送同步请求
				cache:true,//true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
				data:{
					"Username":objs_ove.Username,
					"Token":objs_ove.Token,
					"P_businessID":objs_ove.IDs
				},
				contentType:"application/x-www-form-urlencoded",//发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
				success: function(data){//服务器响应成功并返回数据调用该
					if(data.state == -1){
					    window.parent.location.href = '../CRM/CRM_log.html';
					}else if(data.state == 2){
						alert(data.msg);
					}else if(data.state == 1){
							extend(vm.$data.itemo,data.data);
							vm.$data.bj = data.data;
							vm.$data.img_ = data.data.P_URL1.data;
								var P_datetime = vm.$data.bj.P_datetime;
								var O_date = P_datetime.replace(/[^0-9]+/ig,"");
								var O_dates = new Date(parseInt(O_date));
								var dates = O_dates.getFullYear() + '-' + (O_dates.getMonth() + 1) + '-' + 
								O_dates.getDate() + ' ' + O_dates.getHours() + ':' + 
								O_dates.getMinutes() + ':' + O_dates.getSeconds();
								vm.$data.bj.P_datetime = dates;	
					}else{
						
						alert(data.msg);
						
					}			
				},
				error: function(){
					console.log('error');
				}
			})	
		}


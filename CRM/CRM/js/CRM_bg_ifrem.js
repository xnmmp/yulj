	function bg_ifrem_ove(){
				$.ajax({
				url:util.Config.url + '/CRM_contract/Contract',
				type:'post',//HTTP请求方式
				dataType:'json',//规定预期的服务器响应的数据类型
				async: false,//true表示发送异步请求，false表示发送同步请求
				cache:true,//true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
				data:{
					"Username":objs_ove.T_accountnumber,
					"Token":objs_ove.Token,
					"rows":vm.$data.rows,
					"page":vm.$data.page,
					"P_documentaryID":objs_ove.ID,
					"UserID":objs_ove.T_UserID,
					"customerName":$('#C_name').val(),
					"BussName":$('#B_name').val(),
					"Dates1":$('#Mindate').val(),
					"Dates2":$('#Maxdate').val(),
					"P_name":$('#P_name').val(),
					"P_cookiename":$('#P_cookiename').val()
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
						//时间搓处理
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
							        	bg_ifrem_ove();
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
			function bg_ifrem_two(){
				var objs_two = util.getExreas();
				$.ajax({
				url:util.Config.url + '/CRM_contract/ContractInforByID',
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
//					console.log(data.data)
					if(data.state == -1){
					    window.parent.location.href = '../CRM/CRM_log.html';
					}else if(data.state == 1){									
							if(data.data.P_URL1.data == null){
								extend(vm.$data.item,data.data);
								vm.$data.items = data.data;	
								$('#O_fileURL').hide();
								date();
							}else{
								extend(vm.$data.item,data.data);
								vm.$data.items = data.data;	
								vm.$data.img_ = data.data.P_URL1.data[vm.$data.state];
								$('#O_fileURL').show();
								date();					
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
				function extend(p,c){
						for(var item in c){
							p[item] = c[item];
						}
					}	
			function date(){	
					if(vm.$data.items.P_datetime == ''){
						return;
					}else{
							vm.$data.date = vm.$data.items.P_datetime;
							var p = changeDate();
							var dates = p.getFullYear() + '-' + (p.getMonth() + 1) + '-' + 
							p.getDate() + ' ' + p.getHours() + ':' + 
							p.getMinutes() + ':' + p.getSeconds();
							vm.$data.items.P_datetime = dates;	
						}
				}
				function disan(){
					switch(vm.$data.items.P_kindtwo){
						case 1:{			
							$('#tallshow').show();
						};break;
						default:break;
					}
					$('#P_kindtwo').on("change",function(){
						var P_kindthree = $(this).val();
						if(P_kindthree == 1){
							$('#tallshow').show();
						}else{
							$('#tallshow').show();
						}
					})
				}
				//时间搓处理
				function changeDate(){					
					var d = vm.$data.date;					
					var b = d.replace(/[^0-9]+/ig,"");
					return new Date(parseInt(b));
				}
				function bg_ifrem_san(){
					var P_URL1 = vm.$data.img_.ID;
					var P_cookiename  = $('#P_cookiename').val();
					var P_startdatetime = $('#P_startdatetime').val();
					var P_enddatetime = $('#P_enddatetime').val();
					var P_kindone = $('#P_kindone').val();
					var P_kindtwo = $('#P_kindtwo').val();
					var P_name = $('#P_name').val();
					var P_money = $('#P_money').val();
					if(P_kindone == 0|| P_kindtwo == 0|| P_cookiename == ''|| P_startdatetime == ''
					|| P_enddatetime == ''|| P_name == ''|| P_money == ''|| P_URL1 == ''){
						alert('信息填写不完整，且格式不正确，请查看！');
						return;
					}else{
					$.ajax({
					url:util.Config.url + '/CRM_contract/UpdataContract',
					type:'post',//HTTP请求方式
					dataType:'json',//规定预期的服务器响应的数据类型
					async: false,//true表示发送异步请求，false表示发送同步请求
					cache:true,//true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
					data:{
						"Username":objs_two.Username,
						"Token":objs_two.Token,
						"ID":objs_two.ID,
						"P_cookiename":P_cookiename,
						"P_startdatetime":P_startdatetime,
						"P_enddatetime":P_enddatetime,
						"P_kindone":P_kindone,
						"P_kindtwo":P_kindtwo,
						"P_name":P_name,
						"P_money":P_money,
						"P_URL1":P_URL1
					},
					contentType:"application/x-www-form-urlencoded",//发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
					success: function(data){//服务器响应成功并返回数据调用该
						if(data.state == -1){
						    window.parent.location.href = '../CRM/CRM_log.html';
						}else if(data.state == 1){
							alert(data.msg);
							if(objs_two.Sunber == 2|| objs_two.Sunber ==1){								
								util.bcak();
							}else{
								window.location.href = "../CRM/CRM_bg.html";
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
			function bg_ifrem_san1(){					
					var P_URL1 = vm.$data.img_.ID;
					var P_cookiename  = $('#P_cookiename').val();
					var P_startdatetime = $('#P_startdatetime').val();
					var P_enddatetime = $('#P_enddatetime').val();
					var P_kindone = $('#P_kindone').val();
					var P_kindtwo = $('#P_kindtwo').val();
					var P_name = $('#P_name').val();
					var P_money = $('#P_money').val();
				if(P_kindone == 0|| P_kindtwo == 0|| P_cookiename == ''|| P_startdatetime == ''
				|| P_enddatetime == ''|| P_name == ''|| P_money == ''){
					alert('信息填写不完整，且格式不正确，请查看！');
					return;
				}else{
				$.ajax({
				url:util.Config.url + '/CRM_contract/Addcontract',
				type:'post',//HTTP请求方式
				dataType:'json',//规定预期的服务器响应的数据类型
				async: false,//true表示发送异步请求，false表示发送同步请求
				cache:true,//true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
				data:{
					"Username":objs_two.Username,
					"Token":objs_two.Token,
					"P_businessID":objs_two.P_businessID,
					"P_customerID":objs_two.P_customerID,
					"P_documentaryID": objs_two.P_documentaryID,
					"UserID":objs_two.UserID,
					"P_money":P_money,
					"P_cookiename":P_cookiename,
					"P_startdatetime":P_startdatetime,
					"P_enddatetime":P_enddatetime,
					"P_kindone":P_kindone,
					"P_kindtwo":P_kindtwo,
					"P_name":P_name,
					"P_money":P_money,
					"P_URL1":P_URL1
				},
				contentType:"application/x-www-form-urlencoded",//发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
				success: function(data){//服务器响应成功并返回数据调用该
					if(data.state == -1 ){
					    window.parent.location.href = '../CRM/CRM_log.html';
					}else if(data.state == 1){
						alert(data.msg);
						if(objs_two.Sunber == 2 || objs_two.Sunber == 1){
							util.bcak();
						}else{
							window.location.href = "../CRM/CRM_bg.html";
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

						
							
						
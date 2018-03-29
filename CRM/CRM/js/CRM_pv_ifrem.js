
function PV_ifrem(){
	$.ajax({
				url:util.Config.url + '/CRM_visit/CustomerVisit',
				type:'post',//HTTP请求方式
				dataType:'json',//规定预期的服务器响应的数据类型
				async: false,//true表示发送异步请求，false表示发送同步请求
				cache:true,//true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
				data:{
					"Username":objs_ove.T_accountnumber,
					"Token":objs_ove.Token,
					"rows":vm.$data.rows,
					"page":vm.$data.page,
					"CV_documentaryID":objs_ove.ID,
					"UserID":objs_ove.T_UserID,
					"customerName":$('#C_name').val(),
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
					        	PV_ifrem();
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
function PV_ifrem_two(){
	$.ajax({
				url:util.Config.url + '/CRM_visit/SearchVisitByID',
				type:'post',//HTTP请求方式
				dataType:'json',//规定预期的服务器响应的数据类型
				async: false,//true表示发送异步请求，false表示发送同步请求
				cache:true,//true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
				data:{
					"Username":objs_five.Username,
					"Token":objs_five.Token,
					"ID":objs_five.ID															
				},
				contentType:"application/x-www-form-urlencoded",//发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
				success: function(data){//服务器响应成功并返回数据调用该
					if(data.state == -1){
					    window.parent.location.href = '../CRM/CRM_log.html';
					}else if(data.state == 1){
						if(objs_five.Sunber == 1){
							extend(vm.$data.items,data.data);
							vm.$data.item = data.data;
							if(data.data.CV_fileurl.data == null){
								return;
							}else{
								vm.$data.img_ = data.data.CV_fileurl.data[0];
							}
							
						}else{
						extend(vm.$data.items,data.data);
						vm.$data.item = data.data;
						if(data.data.CV_fileurl.data == null){
								return;
							}else{
								vm.$data.img_ = data.data.CV_fileurl.data[0];
							}
						//时间搓处理						
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
			function date(){
				vm.$data.date = vm.$data.item.CV_datetime;
				var p = changeDate();
				var dates = p.getFullYear() + '-' + (p.getMonth() + 1) + '-' + 
				p.getDate() + ' ' + p.getHours() + ':' + 
				p.getMinutes() + ':' + p.getSeconds();
				vm.$data.item.CV_datetime = dates;
			}
function PV_ifrem_san(){
	var objs_san = util.getExreas();
		var CV_consumerName = $('#CV_consumerName').val();
		var CV_ways = $('#CV_ways').val();
		var CV_oppositepeople = $('#CV_oppositepeople').val();
		var CV_purpose = $('#CV_purpose').val();
		var CV_feedback = $('#CV_feedback').val();
		var CV_remark1 = $('#CV_remark1').val();
		var CV_talkdetails = $('#CV_talkdetails').val();
		var CV_lowerplan = $('#CV_lowerplan').val();
		if(CV_oppositepeople == "" || CV_talkdetails == "" || CV_lowerplan == ""||
		CV_consumerName == ""||CV_ways == 0 || CV_purpose == 0 ||
		CV_feedback == 0){
			alert("数据填写不完整，请检查！");
			return;
		}else if(vm.$data.img_.ID == undefined){
			alert("请上传文件！");
			return;
		}else{
		$.ajax({
				url:util.Config.url + '/CRM_visit/UpdataVisit',
				type:'post',//HTTP请求方式
				dataType:'json',//规定预期的服务器响应的数据类型
				async: false,//true表示发送异步请求，false表示发送同步请求
				cache:true,//true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
				data:{
					"Username":objs_san.Username,
					"Token":objs_san.Token,	
					"ID":objs_san.ID,
					"CV_ways":CV_ways,
					"CV_oppositepeople":CV_oppositepeople,
					"CV_purpose":CV_purpose,
					"CV_talkdetails":CV_talkdetails,
					"CV_feedback":CV_feedback,
					"CV_fileurl":vm.$data.img_.ID,
					"CV_lowerplan":CV_lowerplan,
					"CV_consumerName":CV_consumerName,
					"CV_remark1":CV_remark1
				},
				contentType:"application/x-www-form-urlencoded",//发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
				success: function(data){//服务器响应成功并返回数据调用该
					if(data.state == -1 ){
					    window.parent.location.href = '../CRM/CRM_log.html';
					}else if(data.state == 1){
						alert(data.msg)
						util.bcak();
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
//客户拜访添加
	function pv_(){ 
		var CV_consumerName = $('#CV_consumerName').val();
		var CV_ways = $('#CV_ways').val();
		var CV_oppositepeople = $('#CV_oppositepeople').val();
		var CV_purpose = $('#CV_purpose').val();
		var CV_feedback = $('#CV_feedback').val();
		var CV_remark1 = $('#CV_remark1').val();
		var CV_talkdetails = $('#CV_talkdetails').val();
		var CV_lowerplan = $('#CV_lowerplan').val();
		if(CV_oppositepeople == "" || CV_talkdetails == "" || CV_lowerplan == ""||
		CV_consumerName == ""||CV_ways == 0 || CV_purpose == 0 ||
		CV_feedback == 0){
			alert("数据填写不完整，请检查！");
			return;
		}else if(vm.$data.img_.ID == undefined){
			alert("请上传文件！");
			return;
		}else{
			$.ajax({
				url:util.Config.url + '/CRM_visit/AddVisit',
				type:'post',//HTTP请求方式
				dataType:'json',//规定预期的服务器响应的数据类型
				async: false,//true表示发送异步请求，false表示发送同步请求
				cache:true,//true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
				data:{
					"Username":objs_five.Username,
					"Token":objs_five.Token,
					"UserID":objs_five.UserID,
					"CV_businessID":objs_five.CV_businessID,
					"CV_consumerID":objs_five.CV_consumerID,
					"CV_documentaryID":objs_five.CV_documentaryID,
					"CV_ways":CV_ways,
					"CV_oppositepeople":CV_oppositepeople,
					"CV_purpose":CV_purpose,
					"CV_talkdetails":CV_talkdetails,
					"CV_feedback":CV_feedback,
					"CV_fileurl":vm.$data.img_.ID,
					"CV_lowerplan":CV_lowerplan,
					"CV_consumerName":CV_consumerName,
					"CV_remark1":CV_remark1
				},
				contentType:"application/x-www-form-urlencoded",//发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
				success: function(data){//服务器响应成功并返回数据调用该
//					console.log(data.data)
					if(data.state == -1){
					    window.parent.location.href = '../CRM/CRM_log.html';
					}else if(data.state == 1){
						alert(data.msg);
						if(objs_five.Sunber == 2||objs_five.Sunber == 1){
							util.bcak();
						}else{
							window.location.href="CRM/CRM_com.html"
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
function baoj_ifrem() {
	$.ajax({
		url: util.Config.url + '/CRM_QProgram/Qprogram',
		type: 'post', //HTTP请求方式
		dataType: 'json', //规定预期的服务器响应的数据类型
		async: false, //true表示发送异步请求，false表示发送同步请求
		cache: true, //true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
		data: {
			"Username": objs_ove.T_accountnumber,
			"Token": objs_ove.Token,
			"page": vm.$data.page,
			"rows": vm.$data.rows,
			"O_documentaryID": objs_ove.ID,
			"UserID": objs_ove.T_UserID,
			"customerName": $('#C_name').val(),
			"BussName": $('#B_name').val(),
			"Dates1": $('#Mindate').val(),
			"Dates2": $('#Maxdate').val()
		},
		contentType: "application/x-www-form-urlencoded", //发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
		success: function(data) { //服务器响应成功并返回数据调用该
			//					console.log(data.data)
			if(data.state == -1) {
			    window.parent.location.href = '../CRM/CRM_log.html';
			} else if(data.state == 1) {
				vm.$data.items = data.data;
				vm.$data.count = Math.ceil(data.count / vm.$data.rows);
				vm.$data.page = data.page;
				pege();
				isH = false;
				//						console.log(data.count)
				//时间搓处理
				for(var i = 0; i < vm.$data.items.length; i++) {
					var CV_datetime = vm.$data.items[i].CV_datetime;
					var O_date = CV_datetime.replace(/[^0-9]+/ig, "");
					var O_dates = new Date(parseInt(O_date));
					var dates = O_dates.getFullYear() + '-' + (O_dates.getMonth() + 1) + '-' +
						O_dates.getDate() + ' ' + O_dates.getHours() + ':' +
						O_dates.getMinutes() + ':' + O_dates.getSeconds();
					vm.$data.items[i].CV_datetime = dates;
				}
			} else {
				vm.$data.items = data.data;
				vm.$data.count = Math.ceil(data.count / vm.$data.rows);
				vm.$data.page = data.page;
				pege();
				alert(data.msg)
			}
		},
		error: function() {
			console.log('error');
		}
	})
}
var isH = true;

function pege() {
	if(isH) {
		$(".tcdPageCode").createPage({
			pageCount: vm.$data.count,
			current: vm.$data.page,
			backFn: function(p) {
				vm.$data.page = p;
				baoj_ifrem();
			}
		});
	}
}
//时间搓处理
function changeDate() {
	var d = vm.$data.date;
	var b = d.replace(/[^0-9]+/ig, "");
	return new Date(parseInt(b));
}

function baoj_ifrem_ove() {
	var objs_two = util.getExreas();
	//	var Suns = objs_two.Sunber;
	if(objs_two.Sunber == 1) {
		$('.zt').text("修改");
	} else if(objs_two.Sunber == 2) {
		$('.zt').text("添加");
	}
	$.ajax({
		url: util.Config.url + '/CRM_QProgram/LoadinforQprogram',
		type: 'post', //HTTP请求方式
		dataType: 'json', //规定预期的服务器响应的数据类型
		async: false, //true表示发送异步请求，false表示发送同步请求
		cache: true, //true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
		data: {
			"Username": objs_two.Username,
			"Token": objs_two.Token,
			"ID": objs_two.ID
		},
		contentType: "application/x-www-form-urlencoded", //发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
		success: function(data) { //服务器响应成功并返回数据调用该
			if(data.state == -1) {
			    window.parent.location.href = '../CRM/CRM_log.html';
			} else if(data.state == 1) {
				if(objs_two.Sunber == 1) {
					extend(vm.$data.items, data.data);
					vm.$data.item = data.data;
					vm.$data.img_ = data.data.O_fileURL.data[0];
				} else {
					extend(vm.$data.items, data.data);
					vm.$data.item = data.data;
					vm.$data.img_ = data.data.O_fileURL.data[0];
					var O_datetime = vm.$data.item.O_datetime;				
					var O_date = O_datetime.replace(/[^0-9]+/ig,"");
					var O_dates = new Date(parseInt(O_date));
					var dates = O_dates.getFullYear() + '-' + (O_dates.getMonth() + 1) + '-' + 
					O_dates.getDate() + ' ' + O_dates.getHours() + ':' + 
					O_dates.getMinutes() + ':' + O_dates.getSeconds();
					vm.$data.item.O_datetime = dates;
//					console.log(vm.$data.item.O_datetime)
				}
			} else {
				alert(data.msg)
			}
		},
		error: function() {
			console.log('error');
		}
	})
}
//修改报价
function baoj_ifrem_san() {
	var objs_san = util.getExreas();
	var O_customerfeedback = $('#O_customerfeedback').val();
	var O_consumerpeople = $('#O_consumerpeople').val();
	var O_conferaddress = $('#O_conferaddress').val();
	var O_tokleinfor = $('#O_tokleinfor').val();
	var O_nextplan = $('#O_nextplan').val();
	var remark1 = $('#remark1').val();
	if(O_customerfeedback == 0 || O_consumerpeople == '' || O_conferaddress == '' || O_tokleinfor == '' ||
		O_nextplan == '' || remark1 == '') {
		alert('还未填写完整，请查看！');
		return;
	}if(vm.$data.img_.ID == undefined){
		alert("请上传文件！");
		return;
	}else{
		$.ajax({
			url: util.Config.url + '/CRM_QProgram/UpdataQprogram',
			type: 'post', //HTTP请求方式
			dataType: 'json', //规定预期的服务器响应的数据类型
			async: false, //true表示发送异步请求，false表示发送同步请求
			cache: true, //true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
			data: {
				"Username": objs_san.Username,
				"Token": objs_san.Token,
				"ID": objs_san.ID,
				"O_consumerpeople": O_consumerpeople,
				"O_conferaddress": O_conferaddress,
				"O_tokleinfor": O_tokleinfor,
				"O_nextplan": O_nextplan,
				"O_customerfeedback": O_customerfeedback,
				"O_fileURL": vm.$data.img_.ID,
				"remark1": remark1
			},
			contentType: "application/x-www-form-urlencoded", //发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
			success: function(data) { //服务器响应成功并返回数据调用该
				//					console.log(data.data)
				if(data.state == -1) {
				    window.parent.location.href = '../CRM/CRM_log.html';
				} else if(data.state == 1) {
					alert(data.msg);
					if(objs_san.Sunber == 1) {
						util.bcak();
					} else {
						window.location.href = '../CRM/CRM_baoj.html';
					}
				} else {
					alert(data.msg)
				}
			},
			error: function() {
				console.log('error');
			}
		})
	}
}
//报价添加
function baoj_ifrem_san1() {
	var objs_san = util.getExreas();
	var O_customerfeedback = $('#O_customerfeedback').val();
	var O_consumerpeople = $('#O_consumerpeople').val();
	var O_conferaddress = $('#O_conferaddress').val();
	var O_tokleinfor = $('#O_tokleinfor').val();
	var O_nextplan = $('#O_nextplan').val();
	var remark1 = $('#remark1').val();
	if(O_customerfeedback == 0 || O_consumerpeople == '' || O_conferaddress == '' || O_tokleinfor == '' ||
		O_nextplan == '' || remark1 == '') {
		alert('还未填写完整，请查看！');
		return;
	}if(vm.$data.img_.ID == undefined){
		alert("请上传文件！");
		return;
	}else {
		$.ajax({
			url: util.Config.url + '/CRM_QProgram/AddQprogram',
			type: 'post', //HTTP请求方式
			dataType: 'json', //规定预期的服务器响应的数据类型
			async: false, //true表示发送异步请求，false表示发送同步请求
			cache: true, //true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
			data: {
				"Username": objs_san.Username,
				"Token": objs_san.Token,
				"UserID": objs_san.UserID,
				"O_businessID": objs_san.O_businessID,
				"O_consumerID": objs_san.CustomerID,
				"O_documentaryID": objs_san.O_documentaryID,
				"O_fileURL": vm.$data.img_.ID,
				"O_consumerpeople": O_consumerpeople,
				"O_conferaddress": O_conferaddress,
				"O_tokleinfor": O_tokleinfor,
				"O_nextplan": O_nextplan,
				"O_customerfeedback": O_customerfeedback,
				"remark1": remark1
			},
			contentType: "application/x-www-form-urlencoded", //发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
			success: function(data) { //服务器响应成功并返回数据调用该
				if(data.state == -1) {
				    window.parent.location.href = '../CRM/CRM_log.html';
				} else if(data.state == 1) {
					alert(data.msg);
					if(objs_ove.Sunber == 2) {
						util.bcak();
					} else {
						window.location.href = '../CRM/CRM_baoj.html';
					}

				} else {
					alert(data.msg)
				}
			},
			error: function() {
				console.log('error');
			}
		})
	}
}
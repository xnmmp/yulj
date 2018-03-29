var html = {
	data:{
		
	},
	methods:{
		login:function(){
				var names = $('.name').val();
				var pwds = $('.pwd').val();	
				
				if(names == "" || names.length == 0 || pwds == "" || pwds.length == 0){					
					alert('请输入用户名和密码！');
				}else{
						$.ajax({
							url:util.Config.url+'/Login/loginfrom',
							type:'post',
							dataType:'json',
							async: false,
							cache:true,
							contentType:"application/x-www-form-urlencoded",
							data:{								
								"name":names,
								"pwd":pwds
							},
							success: function(data){	
									if(data.state == 1 || data.state == 2){
										var url = '../CRM/CRM_cm.html';
										data.data.Token = data.guid;
//										alert(data.msg);
										util._setCookie("cookie",+new Date());
										util.openWindow(url, data.data);

									}else{
										alert(data.msg);
									}
							},
							error: function(err){
								console.log('err');
							}
						})
				}
		},
		name: function () {
		    var str = $('#name').val();
		    if (str.length != 0) {
		        reg = /^[^\u4e00-\u9fa5]{0,}$/;
		        if (!reg.test(str)) {
		            alert("对不起，不能输入中文字符!");//请将“英文字母类型”改成你需要验证的属性名称!
		            document.getElementById("name").value = "";
		        }
		    }
		},
		pwd: function () {
		    var strs = $('#pwd').val();
		    if (strs.length != 0) {
		        regs = /^[^\u4e00-\u9fa5]{0,}$/;
		        if (!regs.test(strs)) {
		            alert("对不起，不能输入中文字符!");//请将“英文字母类型”改成你需要验证的属性名称!
		            document.getElementById("pwd").value = "";
		        }
		    }
		}
	}
}
			
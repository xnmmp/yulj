$(function(){
			 $.ajax({
				url:'josn/new2.json',
				type:'get',//HTTP请求方式
				dataType:'json',//规定预期的服务器响应的数据类型
				async: false,//true表示发送异步请求，false表示发送同步请求
				cache:true,//true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
				contentType:"application/x-www-form-urlencoded",//发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
				success: function(data){//服务器响应成功并返回数据调用该
					vm.$data.items = data;					
//					var Dome1 = function(){
//						var obj =location.hash;
//						var cot = obj.substr(3);
//						vm.$data.items = data[cot-1]
//					}
					 
				},
				error: function(){
					console.log('error');
				}
			})
		})	
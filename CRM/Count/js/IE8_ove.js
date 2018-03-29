	var DEFAULT_VERSION = "8.0";
			var ua = navigator.userAgent.toLowerCase();
			var isIE = ua.indexOf("msie")>-1;
			var safariVersion;
			if(isIE){
			    safariVersion =  ua.match(/msie ([\d.]+)/)[1];
			}
			if(safariVersion <= DEFAULT_VERSION ){
			    // 进行你所要的操作
			    $('.tu1').remove();
			    $('.li_ove').append('<span>用户名：</span>')
			     $('.tu2').remove();
			    $('.li_two').append('<span>密&nbsp;&nbsp;&nbsp;码：</span>')
			}
			var DEFAULT_VERSION = "8.0";
			var ua = navigator.userAgent.toLowerCase();
			var isIE = ua.indexOf("msie")>-1;
			var safariVersion;
			if(isIE){
			    safariVersion =  ua.match(/msie ([\d.]+)/)[1];
			}
			if(safariVersion <= DEFAULT_VERSION ){
			$(document).ready(function(){
					var txt=  "由于检测您的浏览器版本过低，建议您点击确定下载更新，或者使用别的浏览器打开该网站！";
					var option = {
						title: "提示",
						btn: parseInt("0011",2),
						icon:"0 -96px",
						onOk: function(){
							window.location.href = 'http://www.baidu.com/link?url=PP1Font40aBOmE6H4mCK57KISEhhixyxX5aTVQLEfxeytLWFDbSepqhPC4wwZ5q2laKcHWvGxTfSajrE0x8T-7CaiAxedeVeWX8Hwp7Q0FW&wd=&eqid=fc4e236f00078d400000000458391dbb'
						}
					}
					window.wxc.xcConfirm(txt,"custom", option);
				})
			}
			

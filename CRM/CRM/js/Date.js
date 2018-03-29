var d = "\/Date(1480414407000)\/";
			console.log(changeDate())
			function changeDate(){
				var b = d.replace(/[^0-9]+/ig,"");
				return new Date(parseInt(b));
			}//后台返回的时间

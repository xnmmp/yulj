var util = {
	_keyStr: "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=",
	_childId:"",
	openWindow: function(url, options, id) {
		var val = this.setExreas(options);
		if(id) {
			this._childId = id;
			var ifram_ = document.getElementById(id)
			if(!ifram_){
				ifram_ = window.parent.document.getElementById(id);
			}
			ifram_.src = url + "#" + val;
		} else {
			document.location.href = url + "#" + val;
		}
	},
	getExreas: function(code,parent) {
		var hash = "";
		var path = "";
		if(typeof(code) == 'string') {
			hash = code;
		} else {
			if(parent){
				hash = window.parent.location.hash;
			}else{
				hash = location.hash;
			}
		}
		var maps = {};
		if(hash.indexOf("#")==0){
			hash = hash.substring(1, hash.length);
		}
		hash = this.decode(hash);
		if(this.Config.IsLocal){
			try{
				maps = JSON.parse(localStorage.getItem(hash));
			}catch(e){
				maps = {};
			}
		}else{
			maps = JSON.parse(hash);
		}
		if(maps.cookie) {
			try{
				var nav = this._getNavigator();
				if(maps.cookie == this._getCookie("cookie") && maps.navigator==nav[0]) {
					return maps;
				} else {
					return {};
				}
			}catch(e){
				return {};
			}
		}
	},
	setExreas: function(options,change,key_) { //change表示返回还是设置
		var val = "";
		if(typeof(options) == "string") { //typeof判断(options)是不是字符串				
			val += options;
		} else if(typeof(options) == "object") { //typeof判断(options)如果是跟对象
			var nav = this._getNavigator()[0];
			var cookie = this._getCookie("cookie");
			var bcakUrl = location.href;
			if(this.Config.IsLocal){
				if(typeof key_=='string'){
					localStorage.removeItem(key_);
					localStorage.setItem(key_,JSON.stringify(options));
					return key_;
				}else{
					val = location.pathname.replace(/\/|\./g,"_");
					options.cookie = cookie;
					options.navigator = nav;
					options.backUrl = bcakUrl;
				}
				localStorage.removeItem(val);
				localStorage.setItem(val,JSON.stringify(options));
			}else{
				if(typeof key_=="string"){
					return this.encode(JSON.stringify(options));
				}else{
					options.cookie = cookie;
					options.navigator = nav;
					options.backUrl = bcakUrl;
					val = JSON.stringify(options);
				}
			}
		} else {
			val = "";
		}
		if(change) {
			location.hash = this.encode(val);
		} else {
			return this.encode(val);
		}
	},
	encode: function(input) {
		var output = "";
		var chr1, chr2, chr3, enc1, enc2, enc3, enc4;
		var i = 0;
		input = this._utf8_encode(input);
		while(i < input.length) {
			chr1 = input.charCodeAt(i++);
			chr2 = input.charCodeAt(i++);
			chr3 = input.charCodeAt(i++);
			enc1 = chr1 >> 2;
			enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
			enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
			enc4 = chr3 & 63;
			if(isNaN(chr2)) {
				enc3 = enc4 = 64;
			} else if(isNaN(chr3)) {
				enc4 = 64;
			}
			output = output +
				this._keyStr.charAt(enc1) + this._keyStr.charAt(enc2) +
				this._keyStr.charAt(enc3) + this._keyStr.charAt(enc4);
		}
		return output;
	},
	decode: function(input) {
		var output = "";
		var chr1, chr2, chr3;
		var enc1, enc2, enc3, enc4;
		var i = 0;
		input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");
		while(i < input.length) {
			enc1 = this._keyStr.indexOf(input.charAt(i++));
			enc2 = this._keyStr.indexOf(input.charAt(i++));
			enc3 = this._keyStr.indexOf(input.charAt(i++));
			enc4 = this._keyStr.indexOf(input.charAt(i++));
			chr1 = (enc1 << 2) | (enc2 >> 4);
			chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
			chr3 = ((enc3 & 3) << 6) | enc4;
			output = output + String.fromCharCode(chr1);
			if(enc3 != 64) {
				output = output + String.fromCharCode(chr2);
			}
			if(enc4 != 64) {
				output = output + String.fromCharCode(chr3);
			}
		}
		output = this._utf8_decode(output);
		return output;
	},
	_utf8_encode: function(string) {
		string = string.replace(/\r\n/g, "\n");
		var utftext = "";
		for(var n = 0; n < string.length; n++) {
			var c = string.charCodeAt(n);
			if(c < 128) {
				utftext += String.fromCharCode(c);
			} else if((c > 127) && (c < 2048)) {
				utftext += String.fromCharCode((c >> 6) | 192);
				utftext += String.fromCharCode((c & 63) | 128);
			} else {
				utftext += String.fromCharCode((c >> 12) | 224);
				utftext += String.fromCharCode(((c >> 6) & 63) | 128);
				utftext += String.fromCharCode((c & 63) | 128);
			}
		}
		return utftext;
	},
	_utf8_decode: function(utftext) {
		var string = "";
		var i = 0;
		var c = c1 = c2 = 0;
		while(i < utftext.length) {
			c = utftext.charCodeAt(i);
			if(c < 128) {
				string += String.fromCharCode(c);
				i++;
			} else if((c > 191) && (c < 224)) {
				c2 = utftext.charCodeAt(i + 1);
				string += String.fromCharCode(((c & 31) << 6) | (c2 & 63));
				i += 2;
			} else {
				c2 = utftext.charCodeAt(i + 1);
				c3 = utftext.charCodeAt(i + 2);
				string += String.fromCharCode(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
				i += 3;
			}
		}
		return string;
	},
	_setCookie: function(name, value) {
		var Days = 1;
		var exp = new Date();
		exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
		document.cookie = name + "=" + escape(value) + ";expires=" + exp.toGMTString();
		return value;
	},
	_getCookie: function(name) {
		var arr, reg = new RegExp("(^| )" + name + "=([^;]*)(;|$)");
		if(arr = document.cookie.match(reg))
			return unescape(arr[2]);
		else
			return null;
	},
	isLogin: function(fn) {
		var that = this;
		var userInfo = that.getExreas();
		if(userInfo && userInfo.ID) {
			try {
				var params = {
					Username: userInfo.T_accountnumber,
					Token: userInfo.Token
				};
//				console.log(JSON.stringify(params))
				$.ajax({
					url: this.Config.url + "/Login/Changetoken",
					type: "POST",
					data: params,
					dataType: "json",
					success: function(data) {
						if(data.state == 1) {
							userInfo.Token = data.guid;
							that.setExreas(userInfo, true);
							fn(true, data)
						} else {
							fn(false, data);
						}
					},
					error: function(err) {
						fn(false, err)
					}
				});

			} catch(e) {
				fn(false, e)
			}
		}
	},
	xchange: function(options) {//更换主页面的数据！options是需要更好的值，主页没有的无法更换
		if(window.parent) {
			var extras = this.getExreas(window.parent.location.hash);
			for(var item in options) {
				if(extras[item]) {
					extras[item] = options[item];
				}
			}
			var val = this.setExreas(extras);
			window.parent.location.hash = "#" + val;
			window.parent.location.reload();
		}
	},
	_getNavigator: function() { //获取浏览器标识
		var agent = navigator.userAgent.toLowerCase();
		var regStr_ie = /msie [\d.]+;/gi;
		var regStr_ff = /firefox\/[\d.]+/gi
		var regStr_chrome = /chrome\/[\d.]+/gi;
		var regStr_saf = /safari\/[\d.]+/gi;
		if(agent.indexOf("msie") > 0) {
			return ["ie/wqnmlgb"];
		}
		if(agent.indexOf("firefox") > 0) {
			return agent.match(regStr_ff);
		}
		if(agent.indexOf("chrome") > 0) {
			return agent.match(regStr_chrome);
		}
		if(agent.indexOf("safari") > 0 && agent.indexOf("chrome") < 0) {
			return agent.match(regStr_saf);
		}
		return ["undefied"];
	},
	bcak:function(options){
		var backUrl = this.getExreas().backUrl;
		var _url = backUrl.split("#");
		var hash = _url[1];
		var data = this.getExreas(hash);
		if(typeof options=='object'){
			for(var item in options){
				if(item!="backUrl" && item!="navigator" && item !="cookie"){
					data[item] = options[item];
				}
			}
		}
		var url_ = _url[0] +"#"+ this.setExreas(data,false,hash);
		location.href = url_;
	},
	Config: {
        url:"",
		IsLocal:window.localStorage,
//		IsLocal:false,
	},
	Curl: {
	    url: "http://shiguanja.com"
	}
}


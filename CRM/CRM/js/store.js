var STORAGE_KEY = 'todos-vuejs'
var store={
	fetch:function(){
		return JSON.parse(window.localStorage.getItem(STORAGE_KEY)||'[]')
	},
	save:function(items){
		window.localStorage.setItem(STORAGE_KEY,JSON.stringify(items))
	}
}

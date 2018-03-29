
function CRM_co(){
	img_put();
	util.openWindow("CRM_co.html",objs_two,"inframe");
}
function CRM_filtrate(){
	img_put();
	util.openWindow("CRM_filtrate.html",objs_two,"inframe");
}
function CRM_so(){
	img_put();
	util.openWindow("CRM_so.html",objs_two,"inframe");
}
function CRM_pv(){
	img_put();
	util.openWindow("CRM_pv.html",objs_two,"inframe");
}
function CRM_baoj(){
	img_put();
	util.openWindow("CRM_baoj.html",objs_two,"inframe");
}
function CRM_bg(){
	img_put();
	util.openWindow("CRM_bg.html",objs_two,"inframe");
}
function CRM_as(){
	img_put();
	util.openWindow("CRM_as.html",objs_two,"inframe");
}
function beijtu(){
	img_ove()
}
function CRM_Ar(){
	img_put();
	util.openWindow("CRM_Ar.html",objs_two,"inframe");
}
function CRM_client(){
	img_put();
	util.openWindow("CRM_client.html",objs_two,"inframe");
}
function CRM_names(){
if(objs_ove.T_name == undefined){
    window.parent.location.href = '../CRM/CRM_log.html';
	return;
}else{	
	img_put();	
	util.openWindow("CRM_names.html",objs_two,"inframe");
	}
}

function CRM_out(){
	$.ajax({
			url:util.Config.url+"/Login/Clean",
			type:"POST",
			data:{
				Username:objs_two.Username,
				Token:objs_two.Token
			},
			dataType:"json",
			success:function(data){
			if(data.state == -1){
				window.parent.location.href = '../CRM/CRM_log.html';
			}else if(data.state == 1){
			    window.parent.location.href = '../CRM/CRM_log.html';
			}else{
				alert(data.msg);
			}
		},
		error:function(data){
			alert(data.msg)
		}
	})
}
function img_put(){
	var _parentElement = document.getElementById("img_ove");
	if(_parentElement){
		_parentElement.parentNode.removeChild(_parentElement);	
		img_oputs();
	}
}
function img_oput(){
	var inframe =  $('#inframe');
	inframe.css('display','none');	
}
function img_oputs(){
	var inframe =  $('#inframe');
	if(inframe.css('display')=='none'){
		inframe.css('display','');
	}
}
function img_ove(){
	var inframe =  $('#inframe');
	if(inframe.css('display')=='none'){
		return;
	}else{
		inframe.css('display','none');
		var img = document.createElement("img");
		img.setAttribute("id", "img_ove");
		img.src = "img/beijtu.png";
		document.getElementById("li_li3").appendChild(img);//把元素放进body标签里面
		return;
	}
}
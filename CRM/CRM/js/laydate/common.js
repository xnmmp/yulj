var common={};
common.init=function(){
    common.menu();
    common.mainHeight();
    common.minHeight();
    common.fnResize();
};
$(document).ready(common.init);
common.fnResize=function(){
    window.onresize=function(){
        common.mainHeight();
        common.minHeight();
    };
};
common.menu=function(){
    var oMenu=$('.menuCon ul li');
    oMenu.hover(function(){
        $(this).addClass('active').siblings().removeClass('active');
    },function(){})
};
common.mainHeight=function(){
    var sHeight=$(window).height()-74+'px';
    $('.mainWrap').css('height',sHeight);
};
common.minHeight=function(){
    var sHeight=$(window).height()-200+'px';
    $('.main').css('height',sHeight);
};
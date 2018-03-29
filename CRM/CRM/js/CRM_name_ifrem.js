!(function () {
    Vue.filter("year", function (val) {
        var vals = val.split(".");
        return vals[0];
    })
    Vue.filter("month", function (val) {
        var vals = val.split(".");
        return vals[1];
    })
    var vm = new Vue({
        el: '#ul_ove',
        data: {
            items: null,
            img_: '',
            ID: '',
            Sunbe: '',
            Sunbe1: '',
            Sunbe2: '',
            Sunbe3: ''
        },
        methods: {
            out: function () {
                util.openWindow("CRM_names.html", objs_ove, "inframe");
            },
            put: function () {
                put();
            }
        }
    })
    var objs_ove = util.getExreas();
    init();
    img_();
    Pduan();
    function init() {
        $.ajax({
            url: util.Config.url + "/Documentary/Searchdocumentaryinfor",
            type: "POST",
            data: {
                Token: objs_ove.Token,
                Username: objs_ove.Username
            },
            dataType: "json",
            success: function (data) {
                if (data.state == -1) {
                    window.parent.location.href = '../CRM/CRM_log.html';
                } else if (data.state == 1) {
                    if (data.data.T_photoUrl.data == null) {
                        vm.$data.items = data.data;
                        $('.img_')[0].src = '../img_log/timg.jpg';
                    } else {
                        $('#O_fileURL').show();
                        vm.$data.items = data.data;
                        var inm_ = data.data.T_photoUrl.data[0];
                        vm.$data.ID = inm_.ID;
                        $('#O_fileURL').val(inm_.Name);
                        $('.img_')[0].src = inm_.Url;
                    }
                } else {
                    alert(data.msg)
                }

            },
            error: function (data) {
                alert(data.msg)
            }
        });
    }
    function put() {
        var data_ = {
            Username: objs_ove.Username,
            Token: objs_ove.Token,
            T_name: $('#Namesen').val(),
            T_demartment: $('#T_demartment').val(),
            T_duct: $('#T_duct').val(),
            T_phone: $('#Ptime').val(),
            T_jobnumber: vm.$data.items.T_jobnumber,
            T_Birth1: $("#year").val(),
            T_Birth2: $("#month").val(),
            T_Sex: $('input:radio:checked').val(),
            T_nativeplace: $('#T_nativeplace').val(),
            T_Entrytime: $('#time').val(),
            T_Nowhous: $('#T_Nowhous').val(),
            T_photoUrl: vm.$data.ID,
            T_IDcard: $('#T_IDcard').val()
        }
        if (vm.$data.Sunbe != 0 || vm.$data.Sunbe1 != 0 || vm.$data.Sunbe3 != 0 || $('.Six').val() == 0) {
            alert('必要信息未填写完整且填写正确格式！');
            return;
        } else {
            $.ajax({
                url: util.Config.url + '/Documentary/UpdatauserInfor',
                type: 'post', //HTTP请求方式
                dataType: 'json', //规定预期的服务器响应的数据类型
                async: false, //true表示发送异步请求，false表示发送同步请求
                cache: true, //true表示从浏览器缓存中加载请求信息，false表示不从浏览器缓存中加载请求信息
                contentType: "application/x-www-form-urlencoded", //发送信息至服务器时内容编码类型。默认值适合大多数应用场合。
                data: data_,
                success: function (data) { //服务器响应成功并返回数据调用该
                    if (data.state == -1) {
                        window.parent.location.href = '../CRM/CRM_log.html';
                    } else if (data.state == 1) {
                        alert(data.msg);
                        util.bcak();
                    } else {
                        alert(data.msg);
                    }
                },
                error: function () {
                    console.log('error');
                }
            })
        }
    }
    function img_() {
        $('#O_fileURL').hide();
        var op = {
            Token: objs_ove.Token,
            Username: objs_ove.Username
        };
        $("#upload").on("change", function () {
            var options = {
                url: "http://www.shiguanja.com/ERP_CRMLoad/PicPunlicSS",
                type: "POST",
                data: op,
                dataType: "json",
                success: function (data) {
                    console.log(data.data)
                    if (data.state == 1) {
                        vm.$data.img_ = data.data;
                        vm.$data.ID = data.data.ID;
                        alert('上传成功');
                        $('#O_fileURL').show();
                        $('#O_fileURL').val(vm.$data.img_.Name);
                    } else {
                        //	            	$('.img_').removeAttr('src');
                        //	            	$('.img_').attr('src',"img/dengdai.gif");
                        //	            	$(".qd").addClass("qds")
                        //	            	$('.qd').removeClass('qd');
                        //	            	$('.qds').attr('disabled','disabled');
                    }
                },
                error: function () {
                    alert("网络错误！");
                }
            }
            $("#updata").ajaxSubmit(options);
        })
    }

    function Pduan() {
        var a = "日期格式不正确，如：1990";
        $(".inputMax").keyup(function () {
            $(this).val($(this).val().replace(/[^0-9.]/g, ''));
        }).bind("paste", function () {  //CTR+V事件处理    
            $(this).val($(this).val().replace(/[^0-9.]/g, ''));
        }).css("ime-mode", "disabled"); //CSS设置输入法不可用
        $('.U_sex').on('click', function () {
            html.data.U_sex = $(this).val();
        })
        $('#year').on('blur', function () {
            var Mi = $(this).val();
            var Max = /^(\d{4})$/;
            if (!Max.test(Mi)) {
                $(this).siblings('.span_').text(a).css('color', '#FF0000');
                vm.$data.Sunbe = 1;
                $('#Min').attr('disabled', 'disabled')
                return;
            } else {
                $(this).siblings('.span_').text('请输入月份');
                vm.$data.Sunbe = 0;
                $('#Min').removeAttr('disabled', 'disabled')
                return;
            }
        })


        $('#Ptime').on('blur', function () {
            var Mi = $(this).val();
            var Min = /^1\d{10}$/;
            if (!Min.test(Mi)) {
                $(this).siblings('.span_').text("请输入11位手机号码!").css('color', '#FF0000');
                vm.$data.Sunbe3 = 1;
                return;
            } else {
                $(this).siblings('.span_').text('√').css('color', '#008000');
                vm.$data.Sunbe3 = 0;
                return;
            }
        })
        $('#Sex>input').on('click', function () {
            vm.$data.items.T_Sex = $(this).val();

        })
    }

})();
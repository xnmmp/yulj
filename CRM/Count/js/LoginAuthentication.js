$(function () {
    var pp = 0;
    $.ajax({
        url: "/Login/Loginstate",
        datatype: "Json",
        type: "post",
        success: function (data) {
            if (data.msg!= "0" && data.msg!= null) {
                $("#IsLogo").text(data.msg);
                $("#perRegisterAndlogout").text("注销");
                pp = 1;
            } else {
                $("#IsLogo").text("登录");
                $("#perRegisterAndlogout").text("注册");
                pp = 2;
            }
            $("#IsLogo").on("click", function () {
                if (pp == 1) {
                    //window.location.href = "/Management/index";
                }
                else if (pp == 2) {
                    window.location.href = "/UserInfor/login";
                }
                else {
                    alert("异常！");
                }
            })
            $("#perRegisterAndlogout").on("click", function () {
                if (pp == 1) {
                    window.location.href = "/UserInfor/Clean";
                }
                else if (pp == 2) {
                    window.location.href = "/UserInfor/perRegister";
                }

            })
        },
        error: function () {
            alert("服务异常！");
        }
    })
})
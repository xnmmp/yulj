
function submit() {
    var name = $("#name").val();
    var pwd = $("#pwd").val();
    if (name == "" || name.trim().length == 0 || pwd == "" || pwd.trim().length == 0) {
        alert('请输入用户名和密码！');
    }
    else {
        $.ajax({
            url: "/Login/loginfrom",
            dataType: "JSON",
            Type: "POST",
            data: { "name": name, "pwd": pwd },
            success: function (data) {
                if (data.state == 1 || data.state == 2) {
                    alert(data.msg);
                    //window.location.href = "";
                }
                else {
                    alert(data.msg);
                }
            },
            error: function () {
                alert("服务异常！");
            }
        })
    }
}

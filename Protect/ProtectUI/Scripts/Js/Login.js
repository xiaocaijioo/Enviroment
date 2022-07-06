$('#btn-login').on('click', function () {
    if ($('#userid').val().length == 0) {
        $('.tips').html('请输入账号！');
    } else if ($('#upassword').val().length == 0) {
        $('.tips').html('请输入密码！');
    } else {
        $.ajax({
            url: "/UserManage/Login",
            type: "post",
            data: {
                userid: $('#userid').val(),
                upassword: $('#upassword').val()
            },
            success: function (data) {
                if (data == "success") {
                    window.location.href = '/Home/Index';
                }
                else if(data=="fail")
                    $('.tips').html('对不起，该账号已被冻结，无法登录！');
                else
                    $('.tips').html('账号或密码错误，请正确输入！');
            }
        });
    }
});
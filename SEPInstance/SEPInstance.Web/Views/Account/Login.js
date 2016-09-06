
function ClickRemoveChangeCode() {
    var code = $("#imgCode").attr("src");
    $("#imgCode").attr("src", code + "1");
}

$(document).ready(function () {
    $('#LoginForm').validate({
        rules: {
            EmailAddressInput: {
                required: true,
                maxlength: 32
            },
            PasswordInput: {
                required: true,
                maxlength: 64
            },
            ValidateCode: {
                required: true,
                maxlength: 4,
                remote: {
                    url: "/Account/CheckCode",
                    type: "post",
                    dataType: "json",
                    data: { code: function () { return $("#ValidateCode").val(); } }
                }
            }
        },
        messages: {
            EmailAddressInput: {
                required: "用户名不能为空",
                maxlength: "最大长度32个字符"
            },
            PasswordInput: {
                required: "密码不能为空",
                maxlength: "最大长度64个字符"
            },
            ValidateCode: {
                required: "验证码不能为空",
                maxlength: "最大长度4个字符",
                remote: "验证码错误"
            }
        },

    });
    $('#LoginButton').click(function (e) {
        if (!$("#LoginForm").valid())
            return false;
        e.preventDefault();
        abp.ui.setBusy(
            $('#LoginArea'),
            $.ajax({
                url: abp.appPath + 'Account/Login',
                type: 'POST',
                dataType:"json",
                data:{
                    TenancyName: $('#TenancyName').val(),
                    UsernameOrEmailAddress: $('#EmailAddressInput').val(),
                    password: $('#PasswordInput').val(),
                    RememberMe: $('#RememberMeInput').is(':checked'),
                    returnUrlHash: $('#ReturnUrlHash').val()
                },
                success: function (data) {
                    window.location.href = data.targetUrl;
                },
                error: function (err) {
                    abp.message.error("登陆失败");
                    ClickRemoveChangeCode();
                    return;
                }
            })
        );
    });

    $('#ReturnUrlHash').val(location.hash);

    $('#LoginForm input:first-child').focus();
})
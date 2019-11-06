<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="green_manufacturing.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
    <meta name="format-detection" content="telephone=no,email=no,date=no,address=no" />
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=no" />
    <title></title>
    <link href="css/aui.css" rel="stylesheet" />
    <link href="css/loading.css" rel="stylesheet" />
    <style type="text/css">
        @-webkit-keyframes fadeinR {
            0% {
                opacity: 0;
                -webkit-transform: translateX(-100px);
            }

            100% {
                opacity: 1;
                -webkit-transform: translateX(0);
            }
        }

        @-moz-keyframes fadeinR {
            0% {
                opacity: 0;
                -moz-transform: translateX(-100px);
            }

            100% {
                opacity: 1;
                -moz-transform: translateX(0);
            }
        }

        @-ms-keyframes fadeinR {
            0% {
                opacity: 0;
                -ms-transform: translateX(-100px);
            }

            100% {
                opacity: 1;
                -ms-transform: translateX(0);
            }
        }

        @keyframes fadeinR {
            0% {
                opacity: 0;
                transform: translateX(-100px);
            }

            100% {
                opacity: 1;
                transform: translateX(0);
            }
        }
    </style>
</head>
<body>
    <div style="padding: 0.5em">
        <img src="img/logo.png" class="logo" />
    </div>
    <div class="i_box">
        <div class="load_box">
            <div class="load_icon">
                <img src="img/green.png" />
            </div>
            <div class="load_text">绿色制造</div>
        </div>
        <div class="i_text">
            <整改管理>
        </div>
    </div>
    <div class="not_found">
        <div>
            <span id="name"></span>
            <span id="phone"></span>
        </div>
        <div>未能查找到您的信息，请于HR部门联系</div>
    </div>
    <footer>© 2017 国家智能铸造产业创新中心 </footer>
    <script src="js/jquery-1.11.0.min.js"></script>
    <script type="text/javascript">
        
        var _config = {
            userid: '<%=userid%>',
            userinfo: '<%=status%>',
            n:'<%=news%>'
        }

        var flag = false;
        var time;
        $(function () {
            var arr = window.location.host;
            document.domain = arr.split(':')[0];
            $(".i_box").css({
                "display": "flex",
                "animation":"fadeinR 2s",
                "-moz-animation":"fadeinR 2s", /* Firefox */
                "-webkit-animation":"fadeinR 2s", /* Safari and Chrome */
                "-o-animation":"fadeinR 2s" /* Opera */
            });
            $(".i_box").on("webkitAnimationEnd", function () {
                flag = true;
            });
            time = setInterval(function () {
                load()
            }, 1000);
        });
        function load() {
            if (flag) {
                clearInterval(time);
                if (_config.userinfo != "none") {
                    var js = JSON.parse(_config.userinfo);
                    var ar = js.status.split(',');
                    if (_config.n == "none") {
                        request(ar[0]);
                    }
                    else {
                        var js = JSON.parse(_config.n).data;
                        var f = "责任人";
                        var html = "\n";
                        for (var j = 0; j < js.length; j++) {
                            if (js[j].txt == "EHS审核人")
                                f = "EHS审核人";
                        }
                        for (var i = 0; i < js.length; i++) {
                            if (js[i].txt == "检查人")
                                f = "检查人";
                            html += js[i].txt + "事项：" + js[i].num + "项\n";
                        }
                        parent.ddalert( html, "代办事项", "好的")
                        request(f);
                    }
                }
                else {
                    parent.getUserInfo(function (re) {
                        $("#name").html(re.nickName);
                        $("#phone").html(re.phone);
                        $(".not_found").css("display", "flex");
                    });
                }
            }
        }

        function request(re) {
            switch (re) {
                case "检查人": {
                    location.href = "inspect/index.aspx?userid=" + _config.userid + "&status=" + JSON.parse(_config.userinfo).status.replace("EHS","");
                    break;
                }
                case "EHS审核人": {
                    location.href = "audit/index.aspx?userid=" + _config.userid + "&status=" + JSON.parse(_config.userinfo).status.replace("EHS", "");
                    break;
                }
                case "责任人": {
                    location.href = "responsible/index.aspx?userid=" + _config.userid + "&status=" + JSON.parse(_config.userinfo).status.replace("EHS", "");
                    break;
                }
                case "管理员": {
                    location.href = "admin/index.aspx?userid=" + _config.userid + "&status=" + JSON.parse(_config.userinfo).status.replace("EHS", "");
                    break;
                }
            }
        }

    </script>
</body>
</html>

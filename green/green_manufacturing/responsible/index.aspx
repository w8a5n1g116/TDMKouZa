<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="green_manufacturing.responsible.index" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
    <meta name="format-detection" content="telephone=no,email=no,date=no,address=no" />
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=no" />
    <title></title>
    <link href="../css/Style.css" rel="stylesheet" />
    <link href="../css/aui.css" rel="stylesheet" />
    <link href="../css/font/iconfont.css" rel="stylesheet" />
    <style type="text/css">
        .iconfont {
            font-size:1.5em;
        }
        .aui-bar-tab-label {
            margin-bottom:0.5em;
        }
        
    </style>
</head>
<body >
    <div class="head_box">
        <div class="head_btn">
            <div><i class="icon iconfont icon-people_fill"></i></div>
            <div id="status">责任人</div>
        </div>
    </div>
  <div class="page_box" style="height:92vh">
      <div class="sx_box" id="zg1">
                <div class="active">全部</div>
                <div>待整改</div>
                <div>已汇报</div>
            </div>
        <div class="pages" style="display:flex;">
         
            <div class="box">
                <div class="n_box">
                    <div class="b_box"></div>
                    <div class="w_box"></div>
                </div>
                <div class="n_box">
                    <div class="b_box"></div>
                    <div class="w_box"></div>
                </div>
                <div class="n_box">
                    <div class="b_box"></div>
                    <div class="w_box"></div>
                </div>
                <div class="n_box">
                    <div class="b_box"></div>
                    <div class="w_box"></div>
                </div>
                <div class="n_box">
                    <div class="b_box"></div>
                    <div class="w_box"></div>
                </div>
                <div class="n_box">
                    <div class="b_box"></div>
                    <div class="w_box"></div>
                </div>
            </div>
        </div>

    </div>
    <script src="../js/jquery-1.11.0.min.js"></script>
    <script src="../js/status_menu.js"></script>
    <script type="text/javascript">
        var _config = {
            userid: '<%=userid%>',
            status: '<%=status%>'
        }
        $(function () {
            var arr = window.location.host;
            document.domain = arr.split(':')[0];
            $(".head_btn").click(function () {
                menus();
            });
        })
        function status(e) {
            $("#status").html(e);
        }
        function menus() {
            var arr = _config.status.split(',');
            var m = '{"data":[';
            for (var i = 0; i < arr.length; i++) {
                m += '{ "icon": "icon-people_fill", "text": "' + arr[i] + '" },'
            }
            m = m.substring(0, m.length - 1);
            m += "]}";
            var data = JSON.parse(m).data;
            if ($('.menu_box').length > 0)
                close_menu();
            else
                menu(data);
        }
        function menu_click(obj) {
            console.log($(obj).attr("num"))
            switch ($(obj).attr("num")) {
                case "检查人": {
                    request_jump("../inspect/index.aspx?userid=" + _config.userid + "&status=" + _config.status);
                    break;
                }
                case "审核人": {
                    request_jump("../audit/index.aspx?userid=" + _config.userid + "&status=" + _config.status);
                    break;
                }
                case "责任人": {
                    close_menu(); 

                    break;
                }
                case "管理员": {
                    request_jump("../admin/index.aspx?userid=" + _config.userid + "&status=" + _config.status);
                    break;
                }
            }
        }
        function request_jump(url) {
            location.href = url;
        }
    </script>
</body>
</html>

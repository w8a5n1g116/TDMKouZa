<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="green_manufacturing.inspect.index" %>

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
            <div id="status">检查人</div>
        </div>
    </div>
    <div class="page_box">

        <div class="pages" style="display:flex;">
            <div class="sx_box" id="zg1">
                <div class="active">全部</div>
                <div>待检查</div>
                <div>已合格</div>
                <div>不合格</div>
            </div>
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

        <div class="pages">
            <div class="sx_box" id="zg2">
                <div class="active">全部</div>
                <div>待审核</div>
                <div>延期申请</div>
            </div>
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

        <div class="pages">
            <div class="date">
                <i class="icon iconfont icon-activity_fill"></i>
                <div id="date"></div>
            </div>
            <div class="sx_box" id="zg3">
                <div class="active">全部</div>
                <div>合格</div>
                <div>整改中</div>
                <div>已整改</div>
            </div>
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
    <footer class="aui-bar aui-bar-tab" id="footer">
        <div class="aui-bar-tab-item aui-active" tapmode>
            <i class="icon iconfont icon-createtask"></i>
            <div class="aui-bar-tab-label">今日事项</div>
        </div>
        <div class="aui-bar-tab-item" tapmode>
            <i class="icon iconfont icon-smallscreen"></i>
            <div class="aui-bar-tab-label">整改反馈</div>
        </div>
        <div class="aui-bar-tab-item" tapmode>
            <i class="icon iconfont icon-dynamic"></i>
            <div class="aui-bar-tab-label">历史查询</div>
        </div>
    </footer>
    <script src="../js/jquery-1.11.0.min.js"></script>
    <script src="../js/status_menu.js"></script>
    <script type="text/javascript">
        var mydate = new Date();
        var _config = {
            userid: '<%=userid%>',
            status:'<%=status%>'
        }
        $(function () {
            var arr = window.location.host;
            document.domain = arr.split(':')[0];
            $("#date").html(mydate.getFullYear() + "-" + (mydate.getMonth() + 1) + "-" + mydate.getDate());

            $(".head_btn").click(function () {
                menus();
            });
            $(".aui-bar-tab-item").click(function () {
                if (!$(this).hasClass("aui-active")) {
                    $("#footer").each(function () {
                        $(this).children().removeClass("aui-active");
                    })
                    $(this).addClass("aui-active");
                    tab_change($(this).find('div').html())
                }
            })
            $("#zg1 div").click(function () {
                $("#zg1").children().removeClass("active");
                $(this).addClass("active");
            })
            $("#zg2 div").click(function () {
                $("#zg2").children().removeClass("active");
                $(this).addClass("active");
            })
            $("#zg3 div").click(function () {
                $("#zg3").children().removeClass("active");
                $(this).addClass("active");
            })
            $(".date i").bind("click", date);
            $("#date").bind("click", date);
        })
        function date() {
            parent.dddatepicker($("#date").html(), function (re) {
                $("#date").html(re.value);
            })
        }
        function tab_change(re) {
            switch (re) {
                case "今日事项": {
                    $(".page_box").children().css("display", "none");
                    $(".page_box").find('.pages').eq(0).css("display", "flex");
                    break;
                }
                case "整改反馈": {
                    $(".page_box").children().css("display", "none");
                    $(".page_box").find('.pages').eq(1).css("display", "flex");
                    break;
                }
                case "历史查询": {
                    $(".page_box").children().css("display", "none");
                    $(".page_box").find('.pages').eq(2).css("display", "flex");
                    break;
                }
            }
        }
        function status(e) {
            $("#status").html(e);
        }
        function menus() {
            var arr = _config.status.split(',');
            var m = '{"data":[';
            for (var i = 0; i < arr.length; i++) {
                m+='{ "icon": "icon-people_fill", "text": "'+arr[i]+'" },'
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
                    close_menu();
                    break;
                }
                case "审核人": {
                    request_jump("../audit/index.aspx?userid=" + _config.userid+"&status="+_config.status);
                    break;
                }
                case "责任人": {
                    request_jump("../responsible/index.aspx?userid=" + _config.userid + "&status=" + _config.status);
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

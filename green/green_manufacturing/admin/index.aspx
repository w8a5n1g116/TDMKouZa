<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="green_manufacturing.admin.index" %>

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
            <div id="status">管理员</div>
        </div>
    </div>
   <div class="page_box">

        <div class="pages" style="display:flex;">
            <div class="sx_box" id="zg1">
                <div class="active">全部</div>
                <div>检查人</div>
                <div>EHS审核人</div>
                <div>责任人</div>
                <div>员工</div>
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
            <i class="icon iconfont icon-createtask_fill"></i>
            <div class="aui-bar-tab-label">权限管理</div>
        </div>
        <div class="aui-bar-tab-item" tapmode>
            <i class="icon iconfont icon-smallscreen"></i>
            <div class="aui-bar-tab-label">事项分配</div>
        </div>
        <div class="aui-bar-tab-item" tapmode>
            <i class="icon iconfont icon-dynamic"></i>
            <div class="aui-bar-tab-label">人员事例</div>
        </div>
    </footer>
    <script src="../js/jquery-1.11.0.min.js"></script>
    <script src="../js/status_menu.js"></script>
    <script type="text/javascript">
        var _config = {
            list: '<%=jyt_list%>',
            status: '<%=status%>',
            userid:'<%=userid%>'
        }
        $(function () {
            load()
            console.log(_config.list);
            var arr = window.location.host;
            document.domain = arr.split(':')[0];
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
            });
            $("#zg1 div").click(function () {
                $("#zg1").children().removeClass("active");
                $(this).addClass("active");
                var js = JSON.parse(_config.list).data;
                var html = "";
                if ($(this).html() == "全部") {
                    for (var i = 0; i < js.length; i++) {
                            html += '<div class="n_box" onclick="box_click(this)" id="' + js[i].phone + '"><div class="b_box"><div>' + js[i].name + '</div><div>' + js[i].jyt + '</div></div><div class="w_box">' + js[i].status + '</div></div>';
                    }
                }
                else
                {
                    for (var i = 0; i < js.length; i++) {
                        if (js[i].status.indexOf($(this).html()) >= 0)
                            html += '<div class="n_box" onclick="box_click(this)" id="' + js[i].phone + '"><div class="b_box"><div>' + js[i].name + '</div><div>' + js[i].jyt + '</div></div><div class="w_box">' + js[i].status + '</div></div>';
                    }
                }
                $(".box").html(html);
            })
            
        })
        function load() {
            var js = JSON.parse(_config.list).data;
            var html = "";
            for (var i = 0; i < js.length; i++) {
                html += '<div class="n_box" onclick="box_click(this)" id="' + js[i].phone + '"><div class="b_box"><div>' + js[i].name + '</div><div>' + js[i].jyt + '</div></div><div class="w_box">' + js[i].status + '</div></div>';
            }
            $(".box").html(html);
        }
        function box_click(obj) {
            if ($(obj).find('.w_box').is(':hidden')) {
                $(obj).find('.w_box').css("display", "flex");
                $(obj).next().remove();
            }
            else
            {
                $(obj).find('.w_box').css("display", "none");
                var html = "<div class='c_box'><div onclick='c_box(this)'>检查人</div><div onclick='c_box(this)'>EHS审核人</div><div onclick='c_box(this)'>责任人</div></div>";
                $(obj).after(html);
                var arr = $(obj).find('.w_box').html().split(',');
                for (var i = 0; i < arr.length; i++) {
                    $(obj).next().find('div').each(function () {
                        if ($(this).html() == arr[i]) {
                            $(this).css("background", "#EF9A85");
                            $(this).css("color", "#fff");
                        }
                    })
                }
            }
        }
        function c_box(obj) {
            if ($(obj).css("background-color") == "rgb(239, 154, 133)") {
                $(obj).css("background", "#DFDFDF");
                $(obj).css("color", "#545454");
            }
            else {
                $(obj).css("background", "#EF9A85");
                $(obj).css("color", "#fff");
            }
            var st = "";
            $(obj).parent().children().each(function () {
                if ($(this).css("background-color") == "rgb(239, 154, 133)")
                    st += $(this).html()+",";
            })
            if (st.length > 0)
                st = st.substring(0, st.length - 1);
            else
                st = "员工";
            
            if ($(obj).parent().prev('.n_box').attr("id") == _config.userid)
                st += ",管理员";
            $(obj).parent().prev('.n_box').find('.w_box').html(st);
            status_change($(obj).parent().prev('.n_box').attr("id"),st);
        }
        function status_change(id,text) {
            $.post("../api/admin/status.aspx", { id: id, text: text }, function () { })
        }
        function tab_change(re) {
            switch (re) {
                case "权限管理": {
                    $(".page_box").children().css("display", "none");
                    $(".page_box").find('.pages').eq(0).css("display", "flex");
                    break;
                }
                case "事项分配": {
                    $(".page_box").children().css("display", "none");
                    $(".page_box").find('.pages').eq(1).css("display", "flex");
                    break;
                }
                case "人员事例": {
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
            console.log($(obj).attr("num"));
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
                    request_jump("../responsible/index.aspx?userid=" + _config.userid + "&status=" + _config.status);
                    break;
                }
                case "管理员": {
                    close_menu();
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

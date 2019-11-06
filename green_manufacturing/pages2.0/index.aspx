<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TDM.pages2._0.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
    <meta name="format-detection" content="telephone=no,email=no,date=no,address=no" />
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=no"/>
    <title>TDM</title>
    <link href="js/pop/pop.css" rel="stylesheet" />
    <link href="../css/aui.css" rel="stylesheet" />
    <link href="css/style_min_bodys.css" rel="stylesheet" />
    <link href="css/font/iconfont.css" rel="stylesheet" />
    <style type="text/css">
        .aui-bar-nav .aui-btn.aui-btn-outlined {
            margin-top: 1em;
        }
        .prevetwinow{
             overflow: hidden;
}
    </style>
</head>
<body class="prevetwinow">
    <header class="aui-bar aui-bar-nav" style="background: #fff">
        <div class="aui-pull-left aui-btn aui-btn-outlined" tapmode onclick="showPopup('top-left')">
            <i class="icon iconfont icon-order_fill" ></i>
        </div>
    </header>
    <div class="page_body scroll" id="page_body">
        
    </div>
    <footer class="aui-bar aui-bar-tab" id="footer">
        <div class="aui-bar-tab-item aui-active" tapmode>
            <i class="icon iconfont icon-createtask_fill"></i>
            <div class="aui-bar-tab-label">计划任务</div>
        </div>
        <div class="aui-bar-tab-item" tapmode>
            <i class="icon iconfont icon-smallscreen"></i>
            <div class="aui-bar-tab-label">指标职责</div>
        </div>
        <div class="aui-bar-tab-item" tapmode>
            <i class="icon iconfont icon-dynamic"></i>
            <div class="aui-bar-tab-label">我的绩效</div>
        </div>
         
    </footer>
            <footer class="aui-bar aui-bar-tab" id="footer1" style="display:none;">
        <div class="aui-bar-tab-item aui-active" tapmode>
            <i class="icon iconfont icon-createtask_fill"></i>
            <div class="aui-bar-tab-label">量化卡</div>
        </div>
        <div class="aui-bar-tab-item" tapmode >
            <i class="icon iconfont icon-smallscreen"></i>
            <div class="aui-bar-tab-label">典型事例</div>
        </div>
        <div class="aui-bar-tab-item" tapmode>
            <i class="icon iconfont icon-dynamic"></i>
            <div class="aui-bar-tab-label">团队绩效</div>
        </div>
        </footer>
</body>
<script src="../js/jquery-1.11.0.min.js"></script>
<script src="../js/dingtalk.js"></script>
<script src="../js/api.js"></script>
<script src="../js/aui-tab.js"></script>
<script src="../js/aui-popup-new.js"></script>
<script src="js/pop/pop.js"></script>
<script src="../js/aui-toast.js"></script>
<script type="text/javascript">
    var _config = {
        appId: '<%=appId%>',
        corpId: '<%=corpId%>',
        timeStamp: '<%=timestamp%>',
        nonce: '<%=nonceStr%>',
        signature: '<%=signature%>'
    };
    var userid = "13995377539";
    $("#page_body").html('<iframe src="TDM/plan.aspx?userid='+userid+'"></iframe>')
    dd.config({
        appId: _config.appId,
        corpId: _config.corpId,
        timeStamp: _config.timeStamp,
        nonceStr: _config.nonce,
        signature: _config.signature,
        jsApiList: ['biz.user.get']
    });
    dd.ready(function () {
        dd.biz.user.get({
            onSuccess: function (info) {
                onload(info.emplId);
            },
            onFail: function (err) {
               
            }
        });
        function onload(id) {
            $.post("../api/public/user_info.aspx", { userid: id }, function (fd) {
                userid = fd;
                $("#page_body").html('<iframe src="TDM/plan.aspx?userid='+fd+'"></iframe>')
            });
        }
        dd.biz.navigation.setLeft({
            control: true,//是否控制点击事件，true 控制，false 不控制， 默认false
            text: '关闭',//控制显示文本，空字符串表示显示默认文本
            onSuccess: function (result) {
                dd.biz.navigation.close({
                    onSuccess: function (result) {
                       
                    },
                    onFail: function (err) { }
                })
            },
            onFail: function (err) { }
        });
        document.addEventListener('backbutton', function (e) {
            e.preventDefault();
            dd.biz.navigation.close({
                onSuccess: function (result) {

                },
                onFail: function (err) { }
            })
        }, false);
    })

    $(function () {
        $(".aui-bar-tab-item").bind("click", bar_click);
    })
    apiready = function () {
        api.parseTapmode();
    }
    var popup = new auiPopup();
    var toast = new auiToast();
    function showPopup(location) {
        popup.init({
            frameBounces: true,//当前页面是否弹动，（主要针对安卓端）
            location: location,//位置，top(默认：顶部中间),top-left top-right,bottom,bottom-left,bottom-right
            buttons: [{
                icon: 'icon iconfont icon-createtask',
                text: '我的计划',
                value: 'plan'//可选
            }, {
                icon: 'icon iconfont icon-workbench',
                text: '我的量化',
                value: 'lhk'
            }, {
                icon: 'icon iconfont icon-redpacket',
                text: '消费明细',
                value: 'xf'
            }, {
                icon: 'icon iconfont icon-businesscard',
                text: '个人信息',
                value: 'info'
            }],
        }, function (ret) {
            if (ret) {
                switch (ret.buttonIndex) {
                    case 1:
                        {

                            //alert(ret.buttonIndex)
                            menu_change("TDM/plan.aspx", "plan");
                            break;
                        }
                    case 2:
                        {
                            //go(ret.buttonIndex, "../lhk/index.aspx");
                            menu_change("lhk/text.aspx", "lhk");
                            break;
                        }
                    case 3:
                        {
                            //go(ret.buttonIndex, "../pay_info/index.aspx");
                            menu_change("payinfo.aspx","pay");
                            break;
                        }
                    case 4:
                        {
                            //go(ret.buttonIndex, "../userid/index.aspx");
                            menu_change("user_info.aspx","userinfo");
                            location.href = "user_info.aspx";
                            break;
                        }
                }
            }
        })
    }
    function menu_change(url,type) {
        switch (type) {
            case "userinfo": {
                $("#footer").css("display", "none");
                $("#footer1").css("display", "none");
                $("iframe").css("height", "92vh");
                $(".page_body").css("height", "92vh");
                $("iframe").attr('src', url + "?userid=" + userid);
                break;
            }
            case "plan": {
                $("#footer").css("display", "table");
                $("#footer1").css("display", "none");
                $("iframe").css("height", "84vh");
                $(".page_body").css("height", "84vh");
                $("iframe").attr('src', url + "?userid=" + userid);
                break;
            }
            case "pay": {
                $("#footer").css("display", "none");
                $("#footer1").css("display", "none");
                $("iframe").css("height", "92vh");
                $(".page_body").css("height", "92vh");
                $("iframe").attr('src', url + "?userid=" + userid);
                break;
            }
            case "lhk": {
                $("#footer").css("display", "none");
                $("#footer1").css("display", "table");
                $("iframe").css("height", "84vh");
                $(".page_body").css("height", "84vh");
                $("iframe").attr('src', url + "?userid=" + userid);
                break;
            }
        }
    }
    var tab = new auiTab({
        element: document.getElementById("footer")
    }, function (ret) {
        if (ret) {
            console.log(ret);
            switch (ret.index) {
                case 1: {
                    $("iframe").attr('src', "TDM/plan.aspx?userid=" + userid);
                    break;
                }
                case 2: {
                    $("iframe").attr('src', "TDM/lhk.aspx?userid=" + userid);
                    break;
                }
                case 3: {
                    $("iframe").attr('src', "TDM/p_jx_info.aspx?userid=" + userid);
                    break;
                }
            }
            // document.getElementById("demo").textContent = ret.index;
        }
    });

    var tab1 = new auiTab({
        element: document.getElementById("footer1")
    }, function (ret) {
        if (ret) {
            // document.getElementById("demo").textContent = ret.index;
            switch (ret.index) {
                case 1: {
                    $("iframe").attr('src', "lhk/text.aspx?userid=" + userid);
                    break;
                }
                case 2: {
                    $("iframe").attr('src', "lhk/deedindex.aspx?userid=" + userid);
                    break;
                }
                case 3: {
                    $("iframe").attr('src', "lhk/team.aspx?userid=" + userid);
                    break;
                }
            }
        }
    });
    
    function bar_click() {

        switch ($(this).parent().attr("id")) {
            case "footer": {
                bar_clean();
                var icon = $(this).find('i').attr("class");
                $(this).find('i').attr("class", icon + "_fill");
                break;
            }
            case "footer1": {
                bar_clean1();
                var icon = $(this).find('i').attr("class");
                $(this).find('i').attr("class", icon + "_fill");
                break;
            }
        }

    }
    function bar_clean() {
        var i = 1;
        $("#footer div i").each(function () {
            switch (i) {
                case 1: {
                    $(this).attr("class", "icon iconfont icon-createtask");
                    i += 1;
                    break;
                }
                case 2: {
                    $(this).attr("class", "con iconfont icon-smallscreen");

                    i += 1;
                    break;
                }
                case 3: {
                    $(this).attr("class", "icon iconfont icon-dynamic");
                    break;
                }
            }
        });
    }
    function bar_clean1() {
        var i = 1;
        $("#footer1 div i").each(function () {
            switch (i) {
                case 1: {
                    $(this).attr("class", "icon iconfont icon-createtask");
                    i += 1
                    break;
                }
                case 2: {
                    $(this).attr("class", "con iconfont icon-smallscreen");
                    i += 1
                    break;
                }
                case 3: {
                    $(this).attr("class", "icon iconfont icon-dynamic");
                    break;
                }
            }
        });
    }


    function pops_show(data) {
        $("#footer").css("display", "none");
        $("iframe").css("height", "92vh");
        $(".page_body").css("height", "92vh");
        pop_main(data);
    }

    function pops_hide() {
        $("#footer").css("display", "table");
        $("iframe").css("height", "84vh");
        $(".page_body").css("height", "84vh");
        $(".pop_box").remove();
    }

    function ok() {
        //alert($("#type").html());
        if (errors()) {
            var data = '{"data":{';
            $(".input_text").each(function () {
                data += '"' + $(this).children().eq(1).attr("name") + '":"' + $(this).children().eq(1).val() + '",';
            })
            data = data.substring(0, data.length - 1);
            data += '}}';
            //alert(data);
            //alert($("#type").html());
            //alert($("#id").html());
            children_function(data, $("#type").html(), $("#id").html());
        }
        else if ($("#type").html() == "plan_jl_hb_pj") {
            var data = '{"data":{';
            $(".input_text").each(function () {
                if ($(this).is(':visible'))
                data += '"' + $(this).children().eq(1).attr("name") + '":"' + $(this).children().eq(1).val() + '",';
            })
            data = data.substring(0, data.length - 1);
            data += '}}';
            //alert(data);
            //alert($("#type").html());
            //alert($("#id").html());
            children_function(data, $("#type").html(), $("#id").html());
        }
        else if ($("#type").html() == "plan_jl_sh") {
            
            children_function("", $("#type").html(), $("#id").html())
        }
        else if ($("#type").html() == "lhk_jl_hb") {
            var data = '{"data":{';
            $(".input_text").each(function () {
                if ($(this).is(':visible'))
                    data += '"' + $(this).children().eq(1).attr("name") + '":"' + $(this).children().eq(1).val() + '",';
            })
            data = data.substring(0, data.length - 1);
            data += '}}';
            children_function(data, $("#type").html(), $("#id").html())
        }
        else {
            toast.fail({
                title: "有信息未填写",
                duration: 2000
            });
        }
    }
    function children_function(fd,type,id) {
        switch (type) {
            case "plan_add": {
                $("iframe")[0].contentWindow.add(fd);
                pops_hide();
                break;
            }
            case "plan_yg_hb": {
                var dat = new Date();
                var str = "" + dat.getFullYear() + "-";
                str += (dat.getMonth() + 1) + "-";
                str += dat.getDate();
                var js = JSON.parse(fd)
                var html = '<div class="boxs_info"><div class="info_line1"><div>' + js.data.stage + '</div><div>' + str + '</div></div><div class="info_line2">' + js.data.text + '</div></div>';
                $(".plan_hb").prepend(html);
                $("iframe")[0].contentWindow.hb(fd, id);
                break;
            }
            case "plan_jl_pj": {
                $("iframe")[0].contentWindow.jl_pj(fd,id);
                pops_hide();
                break;
            }
            case "plan_zg_pj": {
                $("iframe")[0].contentWindow.zg_pj(fd, id);
                pops_hide();
                break;
            }
            case "plan_jl_hb_pj": {
                if ($(".pop_title .active").html() == "汇报") {
                    var dat = new Date();
                    var str = "" + dat.getFullYear() + "-";
                    str += (dat.getMonth() + 1) + "-";
                    str += dat.getDate();
                    var js=JSON.parse(fd)
                    var html = '<div class="boxs_info"><div class="info_line1"><div>' + js.data.stage + '</div><div>' + str + '</div></div><div class="info_line2">' + js.data.text + '</div></div>';
                    $(".plan_hb").prepend(html);
                    $("iframe")[0].contentWindow.hb(fd, id);
                }
                else {
                    toast.custom({
                        title: "评价完成",
                        html: '<i class="aui-iconfont aui-icon-correct"></i>',
                        duration: 2000
                    });
                    $("iframe")[0].contentWindow.jl_pj(fd, id);
                    pops_hide();
                }
                break;
            }
            case "plan_jl_sh": {
                $("iframe")[0].contentWindow.jl_sh(id);
                toast.custom({
                    title: "审核完成",
                    html: '<i class="aui-iconfont aui-icon-correct"></i>',
                    duration: 2000
                });
                pops_hide();
                break;
            }
            case "lhk_jl_hb": {
                if ($(".pop_title .active").html() == "汇报") {
                    toast.custom({
                        title: "汇报完成",
                        html: '<i class="aui-iconfont aui-icon-correct"></i>',
                        duration: 2000
                    });
                    $("iframe")[0].contentWindow.hb(fd, id);
                    pops_hide();
                }
                else {
                    toast.custom({
                        title: "审核完成",
                        html: '<i class="aui-iconfont aui-icon-correct"></i>',
                        duration: 2000
                    });
                    $("iframe")[0].contentWindow.jl_pj(fd, id);
                    pops_hide();
                }
                break;
            }
            case "lhk_hb": {
                toast.custom({
                    title: "汇报完成",
                    html: '<i class="aui-iconfont aui-icon-correct"></i>',
                    duration: 2000
                });
                $("iframe")[0].contentWindow.hb(fd, id);
                pops_hide();
                break;
            }
            case "lhk_jl_sp": {
                toast.custom({
                    title: "审核完成",
                    html: '<i class="aui-iconfont aui-icon-correct"></i>',
                    duration: 2000
                });
                $("iframe")[0].contentWindow.jl_pj(fd, id);
                pops_hide();
                break;
            }
        }
    }
    function errors() {
        var flag=true
        $(".input_text").each(function () {
            if ($(this).children().eq(1).val() == "" || $(this).children().eq(1).val()==null)
                flag = false;
        })
        return flag;
    }

    function page_load() {
        toast.loading({
            title: "加载中",
            duration: 2000
        });
    }

    function load_hide() {
        toast.hide();
    }
    
</script>
</html>

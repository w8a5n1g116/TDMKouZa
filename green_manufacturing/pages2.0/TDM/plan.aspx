<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="plan.aspx.cs" Inherits="TDM.pages2._0.TDM.plan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
    <meta name="format-detection" content="telephone=no,email=no,date=no,address=no" />
    <title>TDM</title>
    <link href="../../css/aui.css" rel="stylesheet" />
    <link href="../../css/iconfont.css" rel="stylesheet" />
    <link href="../css/font/iconfont.css" rel="stylesheet" />
    <link href="../css/page_plan.css" rel="stylesheet" />
    <style type="text/css">
        body {
            background: #f9f9f9;
        }
    </style>
</head>
<body>
    <header class="menu_box">
        <div class="add_box" id="add">
            <i class="icon iconfont icon-addition_fill"></i>
            新增
        </div>
        <div class="add_box" style="display:none" id="back">
            <i class="icon iconfont icon-undo"></i>
            返回
        </div>
        <div class="menu_line">|</div>
        <div class="type_box">
            <div class="active">全部</div>
            <div>制定</div>
            <div>进行</div>
            <div>完成</div>
            <div>逾期</div>
        </div>
    </header>
    <div class="plans">
        <div class="none">
            <i class="icon iconfont icon-feedback_fill"></i>
            <p>未能查找到任何计划</p>
        </div>
    </div>
    <div class="leader_box" style="display:none;">
        

    </div>
</body>
<script src="../../js/jquery-1.11.0.min.js"></script>
<script type="text/javascript">
    var _config = {
        userid: '<%=userid%>'
    }

    var data_list;
    $(function () {
        plan_load();
        $("#add").bind("click", plan_add);
        $(".type_box div").bind("click", type_change);
        $("#back").bind("click", bm_back);
    })

    function plan_load() {
         parent.page_load();
        $.post("../../api/plan/all_list.aspx", { userid: _config.userid }, function (fd) {
            parent.load_hide();
            list(fd);
        })
    }
    function list(fd) {
        if (fd == "none") {
            $(".none").css("display", "flex");
        }
        
        else {
            data_list = JSON.parse(fd);
            if (data_list.status == "集团领导") {
                var html = "";
                for (var i = 0; i < data_list.data.length;i++){
                    html += '<div class="plan_box" onclick="bm_click(this)" name="' + data_list.data[i].bm + '"><div class="info" style="padding:0.5em;"><div class="leader_text">' + data_list.data[i].bm + '</div><div class="ts">' + data_list.data[i].count + '条计划</div></div><div class="stage"><div>查看详情</div></div></div>';
                }
                $("header").css("display", "none");
                $(".plans").css("display", "none");
                $(".leader_box").css("display", "flex");
                $(".leader_box").html(html);
                return;
            }
            $(".none").css("display", "none");
            var html = "";
            for (var i = 0; i < data_list.data.length; i++)
                html += '<div class="plan_box" onclick="list_click(this)" id="' + data_list.data[i].id + '" name="' + data_list.data[i].userid + '"><div class="info"><div class="title"><div>' + data_list.data[i].plan_type + '</div><div>' + data_list.data[i].name + '</div></div><div class="text">' + data_list.data[i].text + '</div></div><div class="stage"><div>' + data_list.data[i].type + '</div><div>' + data_list.data[i].date + '截止</div></div></div>';
            $(".plans").html(html);
        }
    }
    function type_change() {
        $(".type_box div").each(function () {
            $(this).removeAttr("class");
        })
        $(this).attr("class", "active");
        var html = "";
        if ($(this).html() == "全部") {
            for (var i = 0; i < data_list.data.length; i++) {
                html += '<div class="plan_box" onclick="list_click(this)" id="' + data_list.data[i].id + '" name="' + data_list.data[i].userid + '"><div class="info"><div class="title"><div>' + data_list.data[i].plan_type + '</div><div>' + data_list.data[i].name + '</div></div><div class="text">' + data_list.data[i].text + '</div></div><div class="stage"><div>' + data_list.data[i].type + '</div><div>' + data_list.data[i].date + '截止</div></div></div>';
            }
        }
        else {
            for (var i = 0; i < data_list.data.length; i++) {
                if (data_list.data[i].type == $(this).html())
                    html += '<div class="plan_box" onclick="list_click(this)" id="' + data_list.data[i].id + '" name="' + data_list.data[i].userid + '"><div class="info"><div class="title"><div>' + data_list.data[i].plan_type + '</div><div>' + data_list.data[i].name + '</div></div><div class="text">' + data_list.data[i].text + '</div></div><div class="stage"><div>' + data_list.data[i].type + '</div><div>' + data_list.data[i].date + '截止</div></div></div>';
            }
        }
        $(".plans").html(html);
    }
    function plan_add() {
        parent.pops_show({ title: ["新增"], text: "plan_new", type: "plan_add" });
    }
    function list_click(obj) {
        var data;
      
        switch (data_list.status) {
           
            case "主管": {
                if ($(obj).attr("name") == "N") {
                    switch ($(obj).find(".stage").children().eq(0).html()) {
                        case "制定": {
                            parent.pops_show({ title: ["审核"], text: "plan_sh", type: "plan_jl_sh", id: $(obj).attr("id") });
                            break;
                        }
                        default: {
                            parent.pops_show({ title: ["评价"], text: "plan_pj", type: "plan_zg_pj", id: $(obj).attr("id") });
                            break;
                        }
                    }
                }
                else {
                    switch ($(obj).find(".stage").children().eq(0).html()) {
                        case "制定": {
                            parent.pops_show({ title: ["审核"], text: "plan_sh", type: "plan_jl_sh", id: $(obj).attr("id") });
                            break;
                        }
                        default: {
                            parent.pops_show({ title: ["汇报", "评价"], text: ["plan_yg_hb", "plan_pj"], type: "plan_jl_hb_pj", id: $(obj).attr("id") });
                            break;
                        }
                    }
                }
                break;
            }
            case "经理": {
                //alert($(obj).find(".stage").children().eq(0).html());
                //alert($(obj).attr("name"))

                if ($(obj).attr("name") == "N") {
                    switch ($(obj).find(".stage").children().eq(0).html()) {
                        case "制定": {
                            parent.pops_show({ title: ["审核"], text: "plan_sh", type: "plan_jl_sh", id: $(obj).attr("id") });
                            break;
                        }
                        default: {
                            parent.pops_show({ title: ["评价"], text: "plan_pj", type: "plan_jl_pj", id: $(obj).attr("id") });
                            break;
                        }
                    }
                }
                else {
                    switch ($(obj).find(".stage").children().eq(0).html()) {
                        case "制定": {
                            parent.pops_show({ title: ["审核"], text: "plan_sh", type: "plan_jl_sh", id: $(obj).attr("id") });
                            break;
                        }
                        default: {
                            parent.pops_show({ title: ["汇报", "评价"], text: ["plan_yg_hb", "plan_pj"], type: "plan_jl_hb_pj", id: $(obj).attr("id") });
                            break;
                        }
                    }
                }
                break;
            }
            case "集团领导": {
                bm_list_click(obj);
                break;
            }
            default: {
                if ($(obj).find(".stage").children().eq(0).html() != "制定")
                    parent.pops_show({ title: ["汇报"], text: "plan_yg_hb", type: "plan_yg_hb", id: $(obj).attr("id") });
                break;
            }
        }

    }
    function add(fd) {
        var js = JSON.parse(fd);
        $.post("../../api/plan/make_plan.aspx", { userid: _config.userid, type: js.data.plan_type, text: js.data.text, date: js.data.date, complete: js.data.complete, money: js.data.money }, function (fd) {
            plan_load();
        })
    }
    function jl_pj(fd, id) {
        var js = JSON.parse(fd).data;
        $.post("../../api/plan/pj.aspx", { code: id, user: "经理", money: js.money, text: js.text, jg: js.jg, wc: js.stage }, function () {

        })
    }
    function zg_pj(fd, id) {
        var js = JSON.parse(fd).data;
        $.post("../../api/plan/pj.aspx", { code: id, user: "主管", money: js.money, text: js.text, jg: js.jg, wc: js.stage }, function () {

        })
    }
    function hb(fd, id) {
        var js = JSON.parse(fd).data;
        if (js.stage == "Yes") {
            for (var i = 0; i < data_list.data.length; i++) {
                if (data_list.data[i].id == id)
                    data_list.data[i].type = "完成";
            }
            sh_data_change($(".type_box .active").html());
        }
        $.post("../../api/plan/hb.aspx", { code: id, text: js.text, type: js.stage }, function () {

        })
    }
    function jl_sh(id) {
        for (var i = 0; i < data_list.data.length; i++) {
            if (data_list.data[i].id == id)
                data_list.data[i].type = "进行";
        }
        sh_data_change($(".type_box .active").html());
        $.post("../../api/plan/sh.aspx", { code: id }, function () { });
    }
    function sh_data_change(type) {
        var html = "";
        if (type == "全部") {
            for (var i = 0; i < data_list.data.length; i++) {
                html += '<div class="plan_box" onclick="list_click(this)" id="' + data_list.data[i].id + '" name="' + data_list.data[i].userid + '"><div class="info"><div class="title"><div>' + data_list.data[i].plan_type + '</div><div>' + data_list.data[i].name + '</div></div><div class="text">' + data_list.data[i].text + '</div></div><div class="stage"><div>' + data_list.data[i].type + '</div><div>' + data_list.data[i].date + '截止</div></div></div>';
            }
        }
        else {
            for (var i = 0; i < data_list.data.length; i++) {
                if (data_list.data[i].type == type)
                    html += '<div class="plan_box" onclick="list_click(this)" id="' + data_list.data[i].id + '" name="' + data_list.data[i].userid + '"><div class="info"><div class="title"><div>' + data_list.data[i].plan_type + '</div><div>' + data_list.data[i].name + '</div></div><div class="text">' + data_list.data[i].text + '</div></div><div class="stage"><div>' + data_list.data[i].type + '</div><div>' + data_list.data[i].date + '截止</div></div></div>';
            }
        }
        $(".plans").html(html);
    }

    function bm_click(obj){
         parent.page_load();
        $.post("../../api/plan/bm_plan_list.aspx", { userid: _config.userid,bm:$(obj).attr("name") }, function (fd) {
            parent.load_hide();
            console.log(fd);
            bm_list(fd);
        })
    }
    function bm_list(fd) {
        if (fd == "none") {
            $(".none").css("display", "flex");
        }
        else {
            data_list = JSON.parse(fd);
            console.log(data_list);
            $("header").css("display", "flex");
            $(".plans").css("display", "flex");
            $(".leader_box").css("display", "none");
            $(".none").css("display", "none");
            $("#add").css("display", "none");
            $("#back").css("display", "block");
            var html = "";
            for (var i = 0; i < data_list.data.length; i++)
                html += '<div class="plan_box" onclick="bm_list_click(this)" id="' + data_list.data[i].id + '" name="' + data_list.data[i].userid + '"><div class="info"><div class="title"><div>' + data_list.data[i].plan_type + '</div><div>' + data_list.data[i].name + '</div></div><div class="text">' + data_list.data[i].text + '</div></div><div class="stage"><div>' + data_list.data[i].type + '</div><div>' + data_list.data[i].date + '截止</div></div></div>';
            $(".plans").html(html);
        }
    }
    function bm_back() {
        $("header").css("display", "none");
        $(".plans").css("display", "none");
        $(".leader_box").css("display", "flex");
    }
    function bm_list_click(obj) {
        parent.pops_show({ title: ["查看"], text: "plan_yg_hb", type: "plan_zg_ck", id: $(obj).attr("id") });
    }
</script>
</html>

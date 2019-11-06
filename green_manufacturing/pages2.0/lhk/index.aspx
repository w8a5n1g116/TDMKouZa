<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TDM.pages2._0.lhk.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
    <meta name="format-detection" content="telephone=no,email=no,date=no,address=no" />
    <title>TDM</title>
    <link href="../../css/aui.css" rel="stylesheet" />
    
    <link href="../../css/complanation.css" rel="stylesheet" /> 
    <link href="../css/mobileSelect.css" rel="stylesheet" />
    <link href="../js/pop/pop.css" rel="stylesheet" />

</head>
<body style="font-family:微软雅黑;background:none;">
    <div class="page_body scroll" id="page_body">
       
            <div class="list_box" id="lhk" style="margin-top:0;margin-bottom:8vh;">             

        </div>        
    </div>    
</body>
  <script src="../../js/jquery-1.11.0.min.js"></script>
<script type="text/javascript">
    var _config = {
        userid: '<%=userid%>',
        nowdate: '<%=nowdate%>'
    }
    var lhk_data = "";
    var date = _config.nowdate.split(",");
    lhk_list(date);
    function lhk_list(date) {
        $.post("../../api/lhk/list.aspx", { userid: _config.userid, yue: date[1], year: date[0] }, function (fd) {
            if (fd.length > 0) {
                var data = JSON.parse(fd);
                lhk_data = data;
                var text = "";
                var color = "#000";
                for (var i = 0; i < data.list.length; i++) {
                    if (data.list[i].sh_text.length > 0)
                        color = "#0088F5";
                    text += '<div class="list" style="padding-bottom: 0;background: #DDDDDD;" data-id="' + data.list[i].id + '" data-user="' + data.list[i].name + '" onclick="lhk_click(this)"><div class="list_line1" style="justify-content: space-around; padding: 0 0.5em"><div><div>责任人：' + data.list[i].zr_name + '</div></div><div class="project">' + data.list[i].type + '</div><div class="date">' + data.list[i].date + '</div></div><hr />';

                    text += '<div class="list_line2" style="background: #f9f9f9;font-size: 0.9em; flex-direction: column; align-items: flex-start; padding: 0.5em;color:' + data.list[i].color + '"><div class="line_text1"><div>指标/目标：</div><div>' + data.list[i].text + '</div></div><div class="line_text3">评价标准：<div class="line_text1">' + data.list[i].pj_text + '</div></div><div class="line_text2"><div>权重：<div>' + data.list[i].qz + '</div></div><div>金额：<div>' + data.list[i].money + '</div></div></div><div class="line_text2"><div>责任班：<div>' + data.list[i].zr_f + '</div></div></div><div class="line_text3">备注：<div class="line_text1">' + data.list[i].item + '</div></div>';

                    if (data.list[i].wc.length > 0) {
                        text += '<div class="line_text1"><div>自评金额：</div><div>' + data.list[i].zp + '</div></div><div class="line_text3">完成情况说明：<div class="line_text1">' + data.list[i].wc + '</div></div>';
                    }
                    if (data.list[i].sh_text.length > 0) {
                        text += '<div class="line_text1"><div>审核金额：</div><div>' + data.list[i].sh + '</div></div><div class="line_text3">审核评语：<div class="line_text1">' + data.list[i].sh_text + '</div></div>';
                    }
                    text += '</div></div>';
                }
                $("#lhk").html(text);

                $.getScript('../../api/public/list.js', function () {
                });

            }
            else {
                    $("#lhk").html("没有查找到量化卡数据");
            }
           
        })
    }
    function type_change(data) {
        var text = "";
        var color = "#000";
        if (data == "全部") {
            for (var i = 0; i < lhk_data.list.length; i++) {
                if (lhk_data.list[i].sh_text.length > 0)
                    color = "#0088F5";
                text += '<div class="list" style="padding-bottom: 0;background: #DDDDDD;" data-id="' + lhk_data.list[i].id + '" data-user="' + lhk_data.list[i].name + '" onclick="lhk_click(this)"><div class="list_line1" style="justify-content: space-around; padding: 0 0.5em"><div><div>责任人：' + lhk_data.list[i].zr_name + '</div></div><div class="project">' + lhk_data.list[i].type + '</div><div class="date">' + lhk_data.list[i].date + '</div></div><hr />';

                text += '<div class="list_line2" style="background: #f9f9f9;font-size: 0.9em; flex-direction: column; align-items: flex-start; padding: 0.5em;color:' + lhk_data.list[i].color + '"><div class="line_text1"><div>指标/目标：</div><div>' + lhk_data.list[i].text + '</div></div><div class="line_text3">评价标准：<div class="line_text1">' + lhk_data.list[i].pj_text + '</div></div><div class="line_text2"><div>权重：<div>' + lhk_data.list[i].qz + '</div></div><div>金额：<div>' + lhk_data.list[i].money + '</div></div></div><div class="line_text2"><div>责任班：<div>' + lhk_data.list[i].zr_f + '</div></div></div><div class="line_text3">备注：<div class="line_text1">' + lhk_data.list[i].item + '</div></div>';

                if (lhk_data.list[i].wc.length > 0) {
                    text += '<div class="line_text1"><div>自评金额：</div><div>' + lhk_data.list[i].zp + '</div></div><div class="line_text3">完成情况说明：<div class="line_text1">' + lhk_data.list[i].wc + '</div></div>';
                }
                if (lhk_data.list[i].sh_text.length > 0) {
                    text += '<div class="line_text1"><div>审核金额：</div><div>' + lhk_data.list[i].sh + '</div></div><div class="line_text3">审核评语：<div class="line_text1">' + lhk_data.list[i].sh_text + '</div></div>';
                }
                text += '</div></div>';
            }
        }
        else {
            switch (data) {
                case "已下达": {
                    for (var i = 0; i < lhk_data.list.length; i++) {
                        if (lhk_data.list[i].wc.length < 1) {
                            if (lhk_data.list[i].sh_text.length > 0)
                                color = "#0088F5";
                            text += '<div class="list" style="padding-bottom: 0;background: #DDDDDD;" data-id="' + lhk_data.list[i].id + '" data-user="' + lhk_data.list[i].name + '" onclick="lhk_click(this)"><div class="list_line1" style="justify-content: space-around; padding: 0 0.5em"><div><div>责任人：' + lhk_data.list[i].zr_name + '</div></div><div class="project">' + lhk_data.list[i].type + '</div><div class="date">' + lhk_data.list[i].date + '</div></div><hr />';

                            text += '<div class="list_line2" style="background: #f9f9f9;font-size: 0.9em; flex-direction: column; align-items: flex-start; padding: 0.5em;color:' + lhk_data.list[i].color + '"><div class="line_text1"><div>指标/目标：</div><div>' + lhk_data.list[i].text + '</div></div><div class="line_text3">评价标准：<div class="line_text1">' + lhk_data.list[i].pj_text + '</div></div><div class="line_text2"><div>权重：<div>' + lhk_data.list[i].qz + '</div></div><div>金额：<div>' + lhk_data.list[i].money + '</div></div></div><div class="line_text2"><div>责任班：<div>' + lhk_data.list[i].zr_f + '</div></div></div><div class="line_text3">备注：<div class="line_text1">' + lhk_data.list[i].item + '</div></div>';

                            if (lhk_data.list[i].wc.length > 0) {
                                text += '<div class="line_text1"><div>自评金额：</div><div>' + lhk_data.list[i].zp + '</div></div><div class="line_text3">完成情况说明：<div class="line_text1">' + lhk_data.list[i].wc + '</div></div>';
                            }
                            if (lhk_data.list[i].sh_text.length > 0) {
                                text += '<div class="line_text1"><div>审核金额：</div><div>' + lhk_data.list[i].sh + '</div></div><div class="line_text3">审核评语：<div class="line_text1">' + lhk_data.list[i].sh_text + '</div></div>';
                            }
                            text += '</div></div>';
                        } 
                    }
                    break;
                }
                case "已汇报": {
                    for (var i = 0; i < lhk_data.list.length; i++) {
                        if (lhk_data.list[i].wc.length > 0 && lhk_data.list[i].sh_text.length < 1) {
                            if (lhk_data.list[i].sh_text.length > 0)
                                color = "#0088F5";
                            text += '<div class="list" style="padding-bottom: 0;background: #DDDDDD;" data-id="' + lhk_data.list[i].id + '" data-user="' + lhk_data.list[i].name + '" onclick="lhk_click(this)"><div class="list_line1" style="justify-content: space-around; padding: 0 0.5em"><div><div>责任人：' + lhk_data.list[i].zr_name + '</div></div><div class="project">' + lhk_data.list[i].type + '</div><div class="date">' + lhk_data.list[i].date + '</div></div><hr />';

                            text += '<div class="list_line2" style="background: #f9f9f9;font-size: 0.9em; flex-direction: column; align-items: flex-start; padding: 0.5em;color:' + lhk_data.list[i].color + '"><div class="line_text1"><div>指标/目标：</div><div>' + lhk_data.list[i].text + '</div></div><div class="line_text3">评价标准：<div class="line_text1">' + lhk_data.list[i].pj_text + '</div></div><div class="line_text2"><div>权重：<div>' + lhk_data.list[i].qz + '</div></div><div>金额：<div>' + lhk_data.list[i].money + '</div></div></div><div class="line_text2"><div>责任班：<div>' + lhk_data.list[i].zr_f + '</div></div></div><div class="line_text3">备注：<div class="line_text1">' + lhk_data.list[i].item + '</div></div>';

                            if (lhk_data.list[i].wc.length > 0) {
                                text += '<div class="line_text1"><div>自评金额：</div><div>' + lhk_data.list[i].zp + '</div></div><div class="line_text3">完成情况说明：<div class="line_text1">' + lhk_data.list[i].wc + '</div></div>';
                            }
                            if (lhk_data.list[i].sh_text.length > 0) {
                                text += '<div class="line_text1"><div>审核金额：</div><div>' + lhk_data.list[i].sh + '</div></div><div class="line_text3">审核评语：<div class="line_text1">' + lhk_data.list[i].sh_text + '</div></div>';
                            }
                            text += '</div></div>';
                        }
                    }
                    break;
                }
                case "已审批": {
                    for (var i = 0; i < lhk_data.list.length; i++) {
                        if (lhk_data.list[i].sh_text.length > 0) {
                            if (lhk_data.list[i].sh_text.length > 0)
                                color = "#0088F5";
                            text += '<div class="list" style="padding-bottom: 0;background: #DDDDDD;" data-id="' + lhk_data.list[i].id + '" data-user="' + lhk_data.list[i].name + '" onclick="lhk_click(this)"><div class="list_line1" style="justify-content: space-around; padding: 0 0.5em"><div><div>责任人：' + lhk_data.list[i].zr_name + '</div></div><div class="project">' + lhk_data.list[i].type + '</div><div class="date">' + lhk_data.list[i].date + '</div></div><hr />';

                            text += '<div class="list_line2" style="background: #f9f9f9;font-size: 0.9em; flex-direction: column; align-items: flex-start; padding: 0.5em;color:' + lhk_data.list[i].color + '"><div class="line_text1"><div>指标/目标：</div><div>' + lhk_data.list[i].text + '</div></div><div class="line_text3">评价标准：<div class="line_text1">' + lhk_data.list[i].pj_text + '</div></div><div class="line_text2"><div>权重：<div>' + lhk_data.list[i].qz + '</div></div><div>金额：<div>' + lhk_data.list[i].money + '</div></div></div><div class="line_text2"><div>责任班：<div>' + lhk_data.list[i].zr_f + '</div></div></div><div class="line_text3">备注：<div class="line_text1">' + lhk_data.list[i].item + '</div></div>';

                            if (lhk_data.list[i].wc.length > 0) {
                                text += '<div class="line_text1"><div>自评金额：</div><div>' + lhk_data.list[i].zp + '</div></div><div class="line_text3">完成情况说明：<div class="line_text1">' + lhk_data.list[i].wc + '</div></div>';
                            }
                            if (lhk_data.list[i].sh_text.length > 0) {
                                text += '<div class="line_text1"><div>审核金额：</div><div>' + lhk_data.list[i].sh + '</div></div><div class="line_text3">审核评语：<div class="line_text1">' + lhk_data.list[i].sh_text + '</div></div>';
                            }
                            text += '</div></div>';
                        }
                    }
                    break;
                }
            }
        }
        $("#lhk").html(text);
    }
    function lhk_click(obj) {
        switch (lhk_data.status) {
            case "主管": {
                if ($(obj).attr("data-user")=="Y")
                    parent.pops_shows({ title: ["汇报", "审批"], text: ["lhk_hb", "lhk_sp"], type: "lhk_jl_hb", id: $(obj).attr("data-id") });
                else
                    parent.pops_shows({ title: ["审批"], text: ["lhk_hb", "lhk_sp"], type: "lhk_jl_sp", id: $(obj).attr("data-id") });
                break;
            }
            case "经理": {
                if ($(obj).attr("data-user") == "Y")
                    parent.pops_shows({ title: ["汇报", "审批"], text: ["lhk_hb", "lhk_sp"], type: "lhk_jl_hb", id: $(obj).attr("data-id") });
                else
                    parent.pops_shows({ title: ["审批"], text: ["lhk_hb", "lhk_sp"], type: "lhk_jl_sp", id: $(obj).attr("data-id") });
                break;
            }
            default: {
                parent.pops_shows({ title: ["汇报"], text: "lhk_hb", type: "lhk_hb", id: $(obj).attr("data-id") });
                break;
            }
        }
    }
    function jl_pj(fd, id) {
        var js = JSON.parse(fd).data;
        var color = "#0088F5";
        if (lhk_data.status == "主管")
            color = "#FD2515";
        $.post("../../api/lhk/sh.aspx", { id: _config.userid, code: id, money: js.sh_money, text: js.sh }, function () { })
        for (var i = 0; i < lhk_data.list.length; i++) {
            if (lhk_data.list[i].id == id) {
                lhk_data.list[i].sh_text = js.sh;
                lhk_data.list[i].sh = js.sh_money;
                lhk_data.list[i].color = color;
            }
        }
        type_change($('.type_box .active', window.parent.document).html());
    }
    function hb(fd, id) {
        var js = JSON.parse(fd).data;
        $.post("../../api/lhk/bc.aspx", { code: id, money: js.zp_money, text: js.wc }, function () { })
        for (var i = 0; i < lhk_data.list.length; i++) {
            if (lhk_data.list[i].id == id) {
                lhk_data.list[i].wc = js.wc;
                lhk_data.list[i].zp = js.zp_money;
            }
        }
        type_change($('.type_box .active', window.parent.document).html());
    }
</script>
</html>

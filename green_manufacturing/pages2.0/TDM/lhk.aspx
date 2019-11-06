<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="lhk.aspx.cs" Inherits="TDM.pages2._0.TDM.lhk" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
    <meta name="format-detection" content="telephone=no,email=no,date=no,address=no" />
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=no"/>
    <title>TDM</title>
    <link href="../../css/aui.css" rel="stylesheet" />
    <link href="../../css/complanation.css" rel="stylesheet" />
</head>
<body>
    <div class="page_body scroll" id="page_body">
        
            <div class="list_box" id="lhk">
            </div>
 
       
    </div>
</body>

<script src="../../js/jquery-1.11.0.min.js"></script>
<script type="text/javascript">
    var _config = {
        userid: '<%=userid%>'
    }
    var lhk_data = "";
    lhk_list();
    function lhk_list() {
        userid = _config.userid;
        parent.page_load();
        var mydate = new Date();
   
        $.post("../../api/plan/hb_list.aspx", { userid: userid, yue: (mydate.getMonth() + 1), year: mydate.getFullYear() }, function (fd) {
            parent.load_hide();
            if (fd != "none") {
                var data = JSON.parse(fd);
                lhk_data = data;
                console.log(data);
                var text = "";
                var color = "#000";
                for (var i = 0; i < data.list.length; i++) {
                    if (data.list[i].sh_text.length > 0)
                        color = "#0088F5";
                    text += '<div class="list" style="padding-bottom: 0;margin-bottom:0.5em;background:#EFEFEF" data-id="' + data.list[i].id + '" data-user="' + data.list[i].user + '" onclick="lhk_click(this)"><div class="list_line1" style="justify-content: space-between; padding: 0 0.5em;"><div><div>责任人：' + data.list[i].zr_name + '</div></div><div >' + data.list[i].type + '</div><div class="date">' + data.list[i].date + '</div></div><hr />';

                    text += '<div class="list_line2" style="background: #fff; font-size: 0.9em; flex-direction: column; align-items: flex-start; padding: 0.5em;color:' + data.list[i].color + '"><div class="line_text1"><div>指标/目标：</div><div>' + data.list[i].text + '</div></div><div class="line_text3">评价标准：<div class="line_text1">' + data.list[i].pj_text + '</div></div><div class="line_text2"><div>权重：<div>' + data.list[i].qz + '</div></div><div>金额：<div>' + data.list[i].money + '</div></div></div><div class="line_text2"><div>责任方：<div>' + data.list[i].zr_f + '</div></div></div><div class="line_text3">备注：<div class="line_text1">' + data.list[i].item + '</div></div>';

                    if (data.list[i].wc.length > 0) {
                        text += '<div class="line_text1"><div>自评金额：</div><div>' + data.list[i].zp + '</div></div><div class="line_text3">完成情况说明：<div class="line_text1">' + data.list[i].wc + '</div></div>';
                    }
                    if (data.list[i].sh_text.length > 0) {
                        text += '<div class="line_text1"><div>审核金额：</div><div>' + data.list[i].sh + '</div></div><div class="line_text3">审核评语：<div class="line_text1">' + data.list[i].sh_text + '</div></div>';
                    }
                    text += '</div></div>';
                }
                $("#lhk").html(text);
            }
            else {
                parent.load_hide();
                $("#lhk").html("没有查找到量化卡数据");
            }

        })
    }
    function lhk_click(obj) {
        switch (lhk_data.status) {
            case "主管": {
                parent.pops_show({ title: ["汇报", "审批"], text: ["lhk_hb","lhk_sp"], type: "lhk_jl_hb", id: $(obj).attr("data-id") });
                break;
            }
            case "经理": {
                parent.pops_show({ title: ["汇报", "审批"], text: ["lhk_hb", "lhk_sp"], type: "lhk_jl_hb", id: $(obj).attr("data-id") });
                break;
            }
            default: {
                parent.pops_show({ title: ["汇报"], text: "lhk_hb", type: "lhk_hb", id: $(obj).attr("data-id") });
                break;
            }
        }
    }
    function jl_pj(fd, id) {
 
        var js = JSON.parse(fd).data;
        $.post("../../api/lhk/sh.aspx", { id:_config.userid,code: id,  money: js.sh_money,text:js.sh }, function () {})
        lhk_list();
    }
    function hb(fd, id) {
        var js = JSON.parse(fd).data;
        $.post("../../api/lhk/bc.aspx", { code: id, money: js.zp_money, text: js.wc }, function () {})
        lhk_list();
    }
</script>
</html>

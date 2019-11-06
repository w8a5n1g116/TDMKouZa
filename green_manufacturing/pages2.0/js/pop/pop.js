
var datas = {
    plan_new: [{
        type: "select",
        text: "计划类别",
        value: ["基础管理", "TDM建设", "IT事物", "项目管理", "其他"],
        re:"plan_type"
    },
    {
        type: "text",
        text: "工作内容",
        placeholder: "请填写工作内容",
        re:"text"
    },
    {
        type: "date",
        text:"完成期限",
        re:"date"
    },
    {
        type: "select",
        text: "完成方式",
        value: ["岗位指标", "岗位职责", "计划任务", "常规典型", "自主管理"],
        re: "complete"
    },
    {
        type: "input",
        text: "金额",
        value: 0,
        re:"money"
    }],
    plan_yg_hb: [{
        type: "select",
        text: "完成情况",
        value: ["Yes", "Doing", "No"],
        re:"stage"
    },
    {
        type: "textarea",
        text: "汇报内容",
        value:"",
        re:"text"
    }],
    plan_pj: [{
        type: "select",
        text: "评价结果",
        value: ["优", "良", "中", "差"],
        re:"jg"
    },
    {
        type: "select",
        text: "完成判断",
        value: ["Yes", "Doing", "No"],
        re:"stage"
    },
    {
        type: "input",
        text: "审核金额",
        value: "0",
        re:"money"
    },
    {
        type: "textarea",
        text: "完成情况",
        value:"ok",
        re:"text"
    }
    ]
    
}

function pop_main(data) {
    //alert(JSON.stringify(data));
    
    var html = '<div class="pop_box"><div class="back" onclick="pops_hide()"></div><div class="box"><div class="pop_title">';
    var click = "pop_menu(this)";
    if (data.title.length < 2)
        click = "num()"
    for (var i = 0; i < data.title.length; i++) {
        if (i == 0)
            html += '<div class="active" onclick="' + click + '">' + data.title[i] + '</div>';
        else
            html += '<div onclick="' + click + '">' + data.title[i] + '</div>';
    }
    html += '</div><div class="pop_page">';
    html += '<div id="type" style="display:none">' + data.type + '</div>';
    switch (data.type) {
        case "plan_add": {
            var js = datas[data.text];
            html += pop_box(js);
            break;
        }
        case "plan_jl_pj": {
            html += '<div id="id" style="display:none">' + data.id + '</div>';
            var json = pop_ajax("../api/plan/plan_info.aspx", data.id);
            if (json != "none") {
                html += "<div style='margin-bottom:1em;'>"
                var da = JSON.parse(json).data;
                html += '<div class="big_title">' + da[0].works + '</div>';
                for (var i = 0; i < da.length; i++) {
                    html += '<div class="boxs_info"><div class="info_line1"><div>' + da[i].text + '</div><div>' + da[i].date + '</div></div><div class="info_line2">' + da[i].complete_stage + '</div></div>';
                }
                html += "</div>";
                html += "<div style='margin-bottom:1em'></div>";
            }
            var js = datas[data.text];
            html += pop_box(js);
            break;
        }
        case "plan_zg_pj": {
            html += '<div id="id" style="display:none">' + data.id + '</div>';
            var json = pop_ajax("../api/plan/plan_info.aspx", data.id);
            if (json != "none") {
                html += "<div style='margin-bottom:1em;'>"
                var da = JSON.parse(json).data;
                html += '<div class="big_title">' + da[0].works + '</div>';
                for (var i = 0; i < da.length; i++) {
                    html += '<div class="boxs_info"><div class="info_line1"><div>' + da[i].text + '</div><div>' + da[i].date + '</div></div><div class="info_line2">' + da[i].complete_stage + '</div></div>';
                }
                html += "</div>";
                html += "<div style='margin-bottom:1em'></div>";
            }
            var js = datas[data.text];
            html += pop_box(js);
            break;
        }
        case "plan_zg_ck": {
            html += '<div id="id" style="display:none">' + data.id + '</div>';
            var json = pop_ajax("../api/plan/plan_info.aspx", data.id);
            if (json != "none") {
                html += "<div style='margin-bottom:1em;'>"
                var da = JSON.parse(json).data;
                html += '<div class="big_title">' + da[0].works + '</div>';
                html += '<div  class="plan_hb">';
                for (var i = 0; i < da.length; i++) {
                    html += '<div class="boxs_info"><div class="info_line1"><div>' + da[i].text + '</div><div>' + da[i].date + '</div></div><div class="info_line2">' + da[i].complete_stage + '</div></div>';
                }
                html += "</div></div>";
                html += "<div style='margin-bottom:1em'></div>";
            }
            else {
                html += '<div  class="plan_hb">该计划还未汇报</div>';
            }
           
            break;
        }
        case "plan_yg_hb": {
            html += '<div id="id" style="display:none">' + data.id + '</div>';
            var json = pop_ajax("../api/plan/plan_info.aspx", data.id);
            if (json != "none") {
                html += "<div style='margin-bottom:1em;'>"
                var da = JSON.parse(json).data;
                html += '<div class="big_title">' + da[0].works + '</div>';
                html += '<div  class="plan_hb">';
                for (var i = 0; i < da.length; i++) {
                    html += '<div class="boxs_info"><div class="info_line1"><div>' + da[i].text + '</div><div>' + da[i].date + '</div></div><div class="info_line2">' + da[i].complete_stage + '</div></div>';
                }
                html += "</div></div>";
                html += "<div style='margin-bottom:1em'></div>";
            }
            else {
                html += '<div  class="plan_hb"></div>';
            }
            var js = datas[data.text];
            html += pop_box(js);
            break;
        }
        case "plan_jl_hb_pj": {
            html += '<div id="id" style="display:none">' + data.id + '</div>';
            var json = pop_ajax("../api/plan/plan_info.aspx", data.id);
            if (json != "none") {
                html += "<div style='margin-bottom:1em;'>"
                var da = JSON.parse(json).data;
                html += '<div class="big_title">' + da[0].works + '</div>';
                html += '<div  class="plan_hb">';
                for (var i = 0; i < da.length; i++) {
                    html += '<div class="boxs_info"><div class="info_line1"><div>' + da[i].text + '</div><div>' + da[i].date + '</div></div><div class="info_line2">' + da[i].complete_stage + '</div></div>';
                }
                html += "</div></div>";
                html += "<div style='margin-bottom:1em'></div>";
            }
            else {
                html += '<div  class="plan_hb"></div>';
            }
            html += "<div name='hb' >";
            var js = datas[data.text[0]];
            html += pop_box(js);
            html += "</div>";
            html += "<div name='pj' style='display:none'>";
            js = datas[data.text[1]];
            html += pop_box(js);
            html += "</div>";
            break;
        }
        case "lhk_jl_hb": {
            html += '<div id="id" style="display:none">' + data.id + '</div>';
            var json = pop_ajax("../api/lhk/lhk_info.aspx", data.id);
            var data = JSON.parse(json);
            var js = '[{"text": "指标|目标","type": "wbtxt","value": "' + data.text + '"},{"text": "评价标准","type": "wbtxt","value": ""}, {"text": "完成情况说明","type": "textarea","value": "' + data.wc + '","re": "wc"}, {"text": "自评金额","type": "num","value": "' + data.zp + '","re": "zp_money"}]';
            html += "<div name='hb' >";
            var ts = JSON.parse(js);
            ts[1].value = data.pj_text;
            html += pop_box(ts);
            html += "</div>";
            html += "<div name='pj' style='display:none'>";
            js = '[{"text": "指标|目标","type": "wbtxt","value": "' + data.text + '"},{"text": "评价标准","type": "wbtxt","value": ""}, {"text": "审核评语","type": "textarea","value": "' + data.sh_text + '","re": "sh"}, {"text": "审核金额","type": "num","value": "' + data.sh + '","re": "sh_money"}]';
            ts = JSON.parse(js);
            ts[1].value = data.pj_text;
            html += pop_box(ts);
            html += "</div>";
            break;
        }
        case "lhk_jl_sp": {
            html += '<div id="id" style="display:none">' + data.id + '</div>';
            var json = pop_ajax("../api/lhk/lhk_info.aspx", data.id);
            var data = JSON.parse(json);
            var js = '[{"text": "指标|目标","type": "wbtxt","value": "' + data.text + '"},{"text": "评价标准","type": "wbtxt","value": ""}, {"text": "审核评语","type": "textarea","value": "' + data.sh_text + '","re": "sh"}, {"text": "审核金额","type": "num","value": "' + data.sh + '","re": "sh_money"}]';
            var ts = JSON.parse(js);
            ts[1].value = data.pj_text;
            html += pop_box(ts);
            break;
        }
        case "lhk_hb": {
            html += '<div id="id" style="display:none">' + data.id + '</div>';
            var json = pop_ajax("../api/lhk/lhk_info.aspx", data.id);
            var data = JSON.parse(json);
            var js = '[{"text": "指标|目标","type": "wbtxt","value": "' + data.text + '"},{"text": "评价标准","type": "wbtxt","value": "' + data.pj_text + '"}, {"text": "完成情况说明","type": "textarea","value": "' + data.wc + '","re": "wc"}, {"text": "自评金额","type": "num","value": "' + data.zp + '","re": "zp_money"}]';
            html += pop_box(JSON.parse(js));
            break;
        }
        case "plan_jl_sh": {
            html += '<div id="id" style="display:none">' + data.id + '</div>';
            var json = pop_ajax("../api/plan/plan_zd_info.aspx", data.id);
            if (json != "none") {
                html+=pop_box(JSON.parse(json).data);
            }
            break;
        }
    }
    html += '</div><div class="foot_btn" onclick="ok()">确定</div></div></div>';
    $("body").prepend(html);
    $("input").removeClass('.aui-input')
}

function pop_ajax(url, id) {
    var re = "";
    $.ajax({
        type: "POST",
        url: url,
        data: { id: id},
        async:false,
        success: function (data) {
            re = data;
        }
    });
    return re;
}

function pop_box(name) {
    var html = '';
    console.log(name)
    for (var i = 0; i < name.length; i++) {
        switch (name[i].type) {
            case "select": {
                html += '<div class="input_text"><div class="input_title">' + name[i].text + '</div>';
                html += '<select class="pop_select" name="'+name[i].re+'"><option selected="true" disabled="true">请选择</option>';
                for (var j = 0; j < name[i].value.length; j++) {
                    html += '<option value=' + name[i].value[j] + '>' + name[i].value[j] + '</option>';
                }
                html += '</select></div>';
                break;
            }
            case "date": {
                html += '<div class="input_text"><div class="input_title">' + name[i].text + '</div>';
                var date = new Date();
                var month = date.getMonth() + 1;
                if (month < 10)
                    month = "0" + month;
                var day = date.getDate();;
                if (day < 10)
                    day = "0" + day
                var str = date.getFullYear() + "-" + month + "-" + day;
                html += '<input type="date" class="pop_input" value="' + str + '" name="' + name[i].re + '"/>'
                html += '</div>';
                break;
            }
            case "text": {
                html += '<div class="input_text"><div class="input_title">' + name[i].text + '</div>';
                html += '<textarea rows="5" placeholder="' + name[i].placeholder + '" class="pop_textarea" name="' + name[i].re + '"></textarea>';
                html += '</div>';
                break;
            }
            case "input": {
                html += '<div class="input_text"><div class="input_title">' + name[i].text + '</div>';
                html += '<input class="pop_input" value="' + name[i].value + '" name="' + name[i].re + '"/>'
                html += '</div>';
                break;
            }
            case "num": {
                html += '<div class="input_text"><div class="input_title">' + name[i].text + '</div>';
                html += '<input type="number" class="pop_input" style="border:1px solid #E7775F;border-radius:4px;padding:0.3em 0.5em;" value="' + name[i].value + '" name="' + name[i].re + '"/>'
                html += '</div>';
               
                break;
            }
            case "textarea": {
                html += '<div class="input_text"><div class="input_title">' + name[i].text + '</div>';
                html += '<textarea class="pop_input" rows="3" name="' + name[i].re + '" >' + name[i].value + '</textarea>'
                html += '</div>';
                break;
            }
            case "wbtxt": {
                
                html += '<div class="wb_text"><div class="input_title">' + name[i].text + '</div>';
                html += '<div class="wb_txt">' + name[i].value + '</div>';
                html += '</div>';
                console.log(html)
                break;
            }
        }
    }
    return html;
}

function num() { }

function pop_menu(obj){
    $(".pop_title div").each(function () {
        $(this).removeClass("active");
    })
    $(obj).addClass("active");
    if ($(obj).html() == "汇报") {
        $("div[name='hb']").css("display", "block");
        $("div[name='pj']").css("display", "none");
    }
    else {
        $("div[name='hb']").css("display", "none");
        $("div[name='pj']").css("display", "block");
    }
        
}

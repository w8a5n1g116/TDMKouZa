<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterailStockList.aspx.cs" Inherits="DDpage.KmrStorage.MaterailStockList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>掌上仓储-现场领料</title>
    <meta name="viewport" content="width=device-width, initial-scale=1,maximum-scale=1,user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <!--标准mui.css-->
    <link rel="stylesheet" href="../css/mui.min.css">
    <link rel="stylesheet" type="text/css" href="../css/app.css" />
    <link href="../css/mui.picker.css" rel="stylesheet" />
    <link href="../css/mui.poppicker.css" rel="stylesheet" />
    <link href="../css/iconfont.css" rel="stylesheet" />
    <!--App自定义的css-->
    <!--<link rel="stylesheet" type="text/css" href="../css/app.css" />-->
</head>
<body>
    <style>
        .mui-control-content {
            background-color: white;
            min-height: 215px;
        }

            .mui-control-content .mui-loading {
                margin-top: 50px;
            }

        .font_fix {
            font-size: 14px;
        }

        .mui-input-row {
            display: flex;
            flex-direction: row;
            align-items: center;
            justify-content: flex-start;
        }

            .mui-input-row label {
                padding: 0;
                padding-left: 0.5em;
            }

        .ui-alert {
            text-align: center;
            padding: 20px 10px;
            font-size: 14px;
        }

        .mui-btn {
            font-size: 14px;
        }

        .font_fix2 {
            /* placeholder颜色  */
            10 color: #aab2bd;
            11 /* placeholder字体大小  */
            12 font-size: 14px;
            13 /* placeholder位置  */
            14 text-align: left;
        }

        .mui-slider-indicator.mui-segmented-control {
            background-color: #fff;
        }

        .mui-slider .mui-segmented-control.mui-segmented-control-inverted ~ .mui-slider-group .mui-slider-item {
            border: none;
        }

        .mui-table-view-cell {
            padding: 0;
            margin-bottom: 0.5em;
            -moz-box-shadow: 2px 3px 12px #ADADAD;
            -webkit-box-shadow: 2px 3px 12px #ADADAD;
            box-shadow: 2px 3px 12px #ADADAD;
        }

        .list_info {
            width: 100%;
            display: flex;
            flex-direction: row;
            justify-content: space-between;
            padding: 0.5em 0;
            font-size: 1em;
            align-items: center;
            border-bottom: 1px solid #EFEFEF;
        }

        .list_text {
            margin-top: 1em;
            padding: 0 0.5em;
            display: flex;
            flex-direction: column;
            font-size: 1em;
            color: #808080;
        }

        .mui-popover {
            height: 90%;
            width: 100%;
        }
    </style>
    <!--<header class="mui-bar mui-bar-nav">
        <a class="mui-action-back mui-icon mui-icon-left-nav mui-pull-left"></a>
        <h1 class="mui-title">顶部选项卡-可左右拖动(div)</h1>
    </header>-->
    <div class="mui-content">
        <div class="mui-content-padded" style="margin: 5px;">
            <form class="mui-input-group font_fix">
                <div class="mui-input-row">
                    <label>物料名称</label>
                    <div id="PMaterialName" class="div_fix"></div>
                </div>
                <div class="mui-input-row">
                    <label>物料代码</label>
                    <div id="PMaterialCode" class="div_fix"></div>
                </div>
                <div class="mui-input-row">
                    <label>物料型号</label>
                    <div id="PMaterialModel" class="div_fix"></div>
                </div>
                <div class="mui-input-row">
                    <label>物料组</label>
                    <div id="PMaterialGroup" class="div_fix"></div>
                </div>
            </form>
        </div>



        <div id="slider" class="mui-slider">
            <div id="sliderSegmentedControl" class="mui-slider-indicator mui-segmented-control mui-segmented-control-inverted">
                <a class="mui-control-item" href="#item1mobile">物料移库
                </a>
                <a class="mui-control-item" href="#item2mobile">移库记录
                </a>
            </div>
            <div id="sliderProgressBar" class="mui-slider-progress-bar mui-col-xs-4"></div>

            <div class="mui-slider-group">

                <div id="item1mobile" class="mui-slider-item mui-control-content mui-active" style="height: 25em">

                    <div id="scroll1" class="mui-scroll-wrapper">
                        <div class="mui-scroll">
                            <ul class="mui-table-view mui-table-view-striped mui-table-view-condensed" id="list1">
                            </ul>
                        </div>
                    </div>
                </div>
                <div id="item2mobile" class="mui-slider-item mui-control-content" style="height: 25em">
                    <div id="scroll2" class="mui-scroll-wrapper">
                        <div class="mui-scroll">
                            <ul class="mui-table-view mui-table-view-striped mui-table-view-condensed" id="list2">
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="middlePopover" class="mui-popover font_fix" style="margin-left: 5px;">

            <div class="mui-popover-arrow list_text"></div>
            <div class="mui-scroll-wrapper" style="margin: 0; height: 90%;">
                <div class="mui-scroll">
                    <ul class="mui-table-view" id="blist">
                    </ul>
                </div>
            </div>
        </div>
    </div>
     <div style="margin: 0.5em; padding: 0.5em; width: 90%; background: red; color: #fff; text-align: center;" id="save_change">保存</div>
    <script src="../js/jquery-1.11.0.min.js"></script>
    <script src="../js/dingtalk.js"></script>
    <script src="../js/mui.min.js"></script>
    <script src="../js/mui.picker.js"></script>
    <script src="../js/mui.poppicker.js"></script>
    <script>

        mui.init({
            swipeBack: false
        });

        var _phone = '<%=strPhone%>';
            var _company = "";
            var _dept = "";
            var _name = "";
            var _venture = "";
            var _unit = "";
            var _cardId = "";
            var strJson = "";
            var ajaxUrl = "";
            var dataStr = Array();
            var _kcAmount = 0;
            var strMatreialCode = GetQueryString("code");

            var JsonUserInfo = '<%=user_info%>';

                var strArray = new Array();
                strArray = JsonUserInfo.split(",")
                _cardId = strArray[0];
                _company = strArray[1];
                _dept = strArray[2];
                _name = strArray[3];
                _venture = strArray[4];
                _unit = strArray[5];

                var listhtml = "";

                (function ($) {
                    $('.mui-scroll-wrapper').scroll({
                        indicators: true //是否显示滚动条
                    });
                })(mui);

                //发料仓库下拉列表
                (function ($, doc) {
                    $.init();
                    $.ready(function () {
                        var _getParam = function (obj, param) {
                            return obj[param] || '';
                        };

                        ajaxUrl = "../api/KmrStorage/MaterialSqlDetailWeb.aspx";
                        mui.ajax(ajaxUrl, {
                            data: { MatreialCode: strMatreialCode },
                            async: false,
                            crossDomain: true,
                            //dataType: 'json',//服务器返回json格式数据
                            type: 'post',//HTTP请求类型
                            //timeout: 10000,//超时时间设置为10秒；
                            // headers: { 'Content-Type': 'text/json' },
                            success: function (data) {
                                //服务器返回响应，根据响应结果，分析是否登录成功；PMaterialNamePMaterialCodePMaterialModelPMaterialGroup
                                var js = JSON.parse(data).data;

                                mui("#PMaterialName")[0].innerHTML = js[0].wlmc;
                                mui("#PMaterialCode")[0].innerHTML = js[0].wldm;
                                mui("#PMaterialModel")[0].innerHTML = js[0].wlxh;
                                mui("#PMaterialGroup")[0].innerHTML = js[0].wlz;
                                _kcAmount = js[0].kc;

                            },
                            error: function (xhr, type, errorThrown) {
                                //异常处理；
                                alert(type);
                                console.log(type);
                            }
                        });
                        getFList();
                        getSList();
                    });

                })(mui, document);

                $(function () {
                    $("#save_change").click(function () {

                        var kcdd = $("#factory  option:selected").html();
                        var kcamount = $("#Fkc").val();
                        if (kcamount > _kcAmount) {
                            mui.alert("移库数量已大于库存数量！");
                        }
                        else {
                            $.post("../api/KmrStorage/MaterialStockMoveSql.aspx", { Code: strMatreialCode, kcdd: kcdd, phone: _phone, Amount: kcamount }, function (fd) {
                                if (fd == "True") {
                                    mui.alert("操作成功！");
                                    getFList();
                                    getSList();
                                }
                                else {
                                    mui.alert("操作失败！");
                                }
                            })
                        }
                    })
                })
                //发料仓库下拉列表
                //查询按钮loading处理,查询数据
                var strMatName = "";
                var strStock = "";
                var strdataNum = 0;
                //获取连接地址参数
                function GetQueryString(name) {
                    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
                    var r = window.location.search.substr(1).match(reg);
                    if (r != null)
                        return unescape(r[2]);
                    return null;
                }
                //查询按钮loading处理,查询数据

                function getFList(ob) {
                    var fragment = document.createDocumentFragment();
                    ajaxUrl = "../api/KmrStorage/MaterialSqlDetailWeb.aspx";
                    mui.ajax(ajaxUrl, {
                        data: { MatreialCode: strMatreialCode },
                        async: false,
                        crossDomain: true,
                        type: 'post',//HTTP请求类型
                        success: function (data) {
                            //服务器返回响应，根据响应结果，分析是否登录成功；
                            var js = JSON.parse(data).data;
                            var li;

                            var strHtml;
                            for (var i = 0; i < js.length; i++) {

                                strHtml = "";
                                strHtml += "<div class=\"list_text\">";
                                strHtml += "<div class=\"list_info\"><div>基本计量单位：<span class=\"wlmc" + js[i].wldm + "\">" + js[i].jbjldw + "</span></div></div>";
                                strHtml += "<div class=\"list_info\"><div>批次：<span class=\"wlmc" + js[i].wldm + "\">" + js[i].pc + "</span></div></div>";
                                strHtml += "<div class=\"list_info\"><div>净重：<span class=\"wlmc" + js[i].wldm + "\">" + js[i].jz + "</span></div></div>";
                                strHtml += "<div class=\"list_info\"><div>重量单位：<span class=\"wlmc" + js[i].wldm + "\">" + js[i].zldw + "</span></div></div>";
                                strHtml += "<div class=\"list_info\"><div>工厂：<span class=\"wlmc" + js[i].wldm + "\">" + js[i].gc + "</span></div></div>";
                                strHtml += "<div class=\"list_info\"> <div style='display:flex;flex-driction:row;align-items:center'>库存地点： <span id='factory' class=\"wlmc" + js[i].wldm + "\" >" + select_add(js[i].kcdd) + "</span></div></div>";
                                strHtml += "<div class=\"list_info\"><div>库存：<span class=\"wlmc" + js[i].wldm + "\"><input id='Fkc' type='text' class='mui-input-clear font_fix' style='width:80%'  value='" + js[i].kc + "';/></span></div></div>";
                                strHtml += "</div>";
                                li = document.createElement('li');
                                li.id = js[i].matID;
                                li.title = "";
                                li.className = 'mui-table-view-cell';
                                li.innerHTML = strHtml;
                                fragment.appendChild(li);
                            };
                            $("#list1 li").remove();
                            $("#list1").append(fragment);
                        },
                        error: function (xhr, type, errorThrown) {
                            //异常处理；
                            alert(type);
                            console.log(type);
                        }
                    });

                }
                function getSList(ob) {
                    var fragment = document.createDocumentFragment();
                    ajaxUrl = "../api/KmrStorage/MaterialStockMoveInfoSql.aspx";
                    mui.ajax(ajaxUrl, {
                        data: { MatreialCode: strMatreialCode },
                        async: false,
                        crossDomain: true,
                        type: 'post',//HTTP请求类型
                        success: function (data) {
                            //服务器返回响应，根据响应结果，分析是否登录成功；
                            var js = JSON.parse(data).data;
                            var li;

                            var strHtml;

                            for (var i = 0; i < js.length; i++) {
                                strHtml = "";
                                strHtml += "<div class=\"list_text\">";
                                strHtml += "<div class=\"list_info\"><div >起始库存地点：<span class=\"FLocationStart" + js[i].FmaterialCode + "\">" + js[i].FLocationStart + "</span></div><div>最终库存地点：<span class=\"Factory" + js[i].FLocationEnd + "\">" + js[i].FLocationEnd + "</div></div>";
                                strHtml += "<div class=\"list_info\"><div >库管员：<span class=\"FKeeper" + js[i].FmaterialCode + "\">" + js[i].FKeeper + "</span></div><div>移库时间：<span class=\"EditDate" + js[i].FmaterialCode + "\">" + js[i].EditDate + "</div></div>";
                                strHtml += "<div class=\"list_info\"><div>移库数量：<span class=\"MAmount" + js[i].FmaterialCode + "\">" + js[i].MAmount + "</span></div></div>";
                                strHtml += "</div>";
                                li = document.createElement('li');
                                li.id = "sec" + js[i].matID;
                                li.title = "sec";
                                li.className = 'mui-table-view-cell';
                                li.innerHTML = strHtml;
                                fragment.appendChild(li);
                            };


                            $("#list2 li").remove();
                            $("#list2").append(fragment);

                        },
                        error: function (xhr, type, errorThrown) {
                            //异常处理；
                            alert(type);
                            console.log(type);
                        }
                    });
                }
                function select_add(p) {
                    var html = '<select style="margin:0;">';
                    ajaxUrl = "../api/KmrStorage/GetInventoryStock.aspx";
                    mui.ajax(ajaxUrl, {
                        data: {},
                        async: false,
                        crossDomain: true,
                        //dataType: 'json',//服务器返回json格式数据
                        type: 'post',//HTTP请求类型
                        //timeout: 10000,//超时时间设置为10秒；
                        // headers: { 'Content-Type': 'text/json' },
                        success: function (data) {
                            //服务器返回响应，根据响应结果，分析是否登录成功；
                            var js = JSON.parse(data).data;

                            console.log(js)
                            for (var i = 0; i < js.length; i++) {
                                if (js[i] == p)
                                    html += '<option value="' + js[i] + '" selected>' + js[i] + '</option>';
                                else
                                    html += '<option value="' + js[i] + '">' + js[i] + '</option>';
                            }

                        },
                        error: function (xhr, type, errorThrown) {
                            //异常处理；
                            alert(errorThrown);
                            console.log(type);
                        }
                    });


                    html += '</select>';
                    return html;
                }
                function getTHList() {

                }

                function btn_switch(obj) {
                    var nameid = $(obj).attr("name");
                    if ($(obj).hasClass('mui-active')) {
                        $(obj).removeClass("mui-active");
                        var num = $("span.mui-badge").text()
                        num--;
                        $("span.mui-badge").text(num);
                        $("li." + nameid + "").remove();

                    }
                    else {
                        $(obj).addClass("mui-active");
                        $(obj).addClass("onselected");

                        var num = $("span.mui-badge").text();
                        num++;
                        $("span.mui-badge").text(num);

                        var fragment = document.createDocumentFragment();
                        var li;
                        li = document.createElement('li');
                        li.className = 'mui-table-view-cell picked ' + nameid + '';
                        li.id = nameid;
                        li.innerHTML = $("li#" + nameid + "").html();

                        fragment.appendChild(li);
                        $("#blist").append(fragment);
                        $(obj).removeClass("onselected");
                        $(".onselected").attr("onclick", "btn_pickswitch(this)");

                    }
                }


                function btn_secswitch(obj) {
                    var nameid = $(obj).attr("name");
                    if ($(obj).hasClass('mui-active')) {
                        $(obj).removeClass("mui-active");
                        var num = $("span.mui-badge").text()
                        num--;
                        $("span.mui-badge").text(num);
                        $("li." + nameid + "").remove();

                    }
                    else {
                        $(obj).addClass("mui-active");
                        $(obj).addClass("onsecselected");

                        var num = $("span.mui-badge").text();
                        num++;
                        $("span.mui-badge").text(num);

                        var fragment = document.createDocumentFragment();
                        var li;
                        li = document.createElement('li');
                        li.className = 'mui-table-view-cell picked ' + nameid + '';
                        li.id = nameid;
                        li.innerHTML = $("li#" + nameid + "").html();

                        fragment.appendChild(li);
                        $("#blist").append(fragment);
                        $(obj).removeClass("onsecselected");
                        $(".onsecselected").attr("onclick", "btn_Secpickswitch(this)");

                    }

                }

                function btn_pickswitch(obj) {
                    var nameclass = $(obj).attr("name");
                    if ($(obj).hasClass('mui-active')) {
                        $(obj).removeClass("mui-active");
                        $("li." + nameclass + "").removeClass("picked");
                    }
                    else {
                        $(obj).addClass("mui-active");
                        $("li." + nameclass + "").addClass("picked");
                    }
                }

                function btn_Secpickswitch(obj) {
                    var nameclass = $(obj).attr("name");
                    if ($(obj).hasClass('mui-active')) {
                        $(obj).removeClass("mui-active");
                        $("li." + nameclass + "").removeClass("picked");
                    }
                    else {
                        $(obj).addClass("mui-active");
                        $("li." + nameclass + "").addClass("picked");
                    }
                }


                function getNowFormatDate() {
                    var date = new Date();
                    var seperator1 = "-";
                    var seperator2 = ":";
                    var year = date.getFullYear();
                    var month = date.getMonth() + 1;
                    var strDate = date.getDate();
                    if (month >= 1 && month <= 9) {
                        month = "0" + month;
                    }
                    if (strDate >= 0 && strDate <= 9) {
                        strDate = "0" + strDate;
                    }
                    var currentdate = year + seperator1 + month + seperator1 + strDate
                        + " " + date.getHours() + seperator2 + date.getMinutes()
                        + seperator2 + date.getSeconds();
                    return currentdate;
                }

                $("#clear").click(function () {
                    if (confirm("删除全部物料？")) {
                        $("#blist li").remove();
                        $("span.mui-badge").text("0");
                        mui('#middlePopover').popover('hide');
                        var btn = document.getElementById("queryBtn");
                        mui.trigger(btn, 'tap');
                        //location.reload();
                        // $(".pick-submit-clear").hide();
                        // $("#middlePopover").removeClass("mui-active");
                        // $("#middlePopoverhref").addClass("mui-active");
                        // $(".mui-backdrop").remove();
                    }
                });

                $("#submit").click(function () {

                    if ($("li.picked").length > 0) {
                        if (confirm("确认提交？")) {

                            var orderNum = $("#OddNo").val();
                            var stockName = $("#PStock").text();
                            if (stockName == "请选择发料仓库") {
                                stockName = "";
                            }
                            var matName = $("#PMName").val();

                            var strJson = "";
                            //strJson += "{\"PCompany\":\"" + _company + "\",\"PDept\":\"" + _dept + "\",\"PVenture\":\"" + _venture + "\",\"PName\":\"" + _name + "\",\"PCode\":\"" + _cardId + "\",";
                            //strJson += "\"PTime\":\"" + getNowFormatDate() + "\",\"OddNo\":\"" + orderNum + "\",\"PickStat\":\"待备餐\",\"data\":[";
                            strJson += "[";
                            var tmp = "";
                            $("li.picked").each(function () {
                                //alert($(this).html());
                                //alert($(this).attr("id"));

                                tmp = $(this).attr("id");

                                $(this).find("span").addClass("on");
                                $(this).find("input").addClass("on");

                                strJson += "{\"MStock\":\"" + $("span.stock" + tmp + ".on").text() + "\",";
                                strJson += "\"MStockCode\":\"" + $("span.stockcode" + tmp + ".on").text() + "\",";
                                strJson += "\"MFactory\":\"" + $("span.fac" + tmp + ".on").text() + "\",";
                                strJson += "\"MFactoryCode\":\"" + $("span.faccode" + tmp + ".on").text() + "\",";
                                strJson += "\"MGroup\":\"" + $("span.group" + tmp + ".on").text() + "\",";
                                strJson += "\"MGroupCode\":\"" + $("span.groupcode" + tmp + ".on").text() + "\",";
                                strJson += "\"Material\":\"" + strtojs($("span.wlmc" + tmp + ".on").text()) + "\",";
                                strJson += "\"MCode\":\"" + $("span.code" + tmp + ".on").text() + "\",";
                                strJson += "\"MBatch\":\"" + $("span.no" + tmp + ".on").text() + "\",";
                                strJson += "\"MUnit\":\"" + $("span.unit" + tmp + ".on").text() + "\",";
                                strJson += "\"MInventory\":\"" + $("span.num" + tmp + ".on").text() + "\",";
                                strJson += "\"PickInventory\":\"" + $("input.pnum" + tmp + ".on").val() + "\",";
                                strJson += "\"PType\":\"" + $("span.type" + tmp + ".on").text() + "\",";
                                strJson += "\"PDStat\":\"1\"},";
                            })
                            strJson = strJson

                            strJson = strJson.substr(0, strJson.length - 1);
                            strJson += "]";
                            //strJson += "]}";
                            //ajax后台数据处理

                            $.post("../api/KmrStorage/materielList_sumbit.aspx",
                                { PCompany: _company, PDept: _dept, PVenture: _venture, PName: _name, PCode: _cardId, PTime: getNowFormatDate(), OddNo: orderNum, PickStat: "1", dataList: strJson },
                                function (data, status) {

                                    mui.toast(data, { duration: 'long', type: 'div' })
                                    //数据提交后页面处理
                                    $("#blist li").remove();
                                    $("span.mui-badge").text("0");
                                    mui('#middlePopover').popover('hide');
                                    var btn = document.getElementById("queryBtn");
                                    mui.trigger(btn, 'tap');

                                    //location.reload();
                                    //数据提交后页面处理
                                });



                            //ajax后台数据处理
                            //console.log(strJson);

                        }


                    }
                    else {

                        alert("无领料信息，请返回继续添加！")
                    }
                })


                function strtojs(str) {
                    console.log(str)
                    var jst = "";
                    for (var i = 0; i < str.length; i++) {
                        var c = str[i];
                        switch (c) {
                            case '\"':
                                jst += "\\\"";
                                break;
                            case '\\':
                                jst += "\\\\";
                                break;
                            case '/':
                                jst += "\\/";
                                break;
                            case '\b':
                                jst += "\\b";
                                break;
                            case '\f':
                                jst += "\\f";
                                break;
                            case '\n':
                                jst += "\\n";
                                break;
                            case '\r':
                                jst += "\\r";
                                break;
                            case '\t':
                                jst += "\\t";
                                break;
                            default: {
                                jst += c;
                                break;
                            }

                        }
                    }
                    return jst;
                }
    </script>
</body>
</html>

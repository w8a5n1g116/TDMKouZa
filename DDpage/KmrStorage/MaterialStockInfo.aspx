<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterialStockInfo.aspx.cs" Inherits="DDpage.KmrStorage.MaterialStockInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>掌上仓储-库存明细</title>
    <meta name="viewport" content="width=device-width, initial-scale=1,maximum-scale=1,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <!--标准mui.css-->
    <link rel="stylesheet" href="../css/mui.min.css" />
    <link rel="stylesheet" type="text/css" href="../css/app.css" />
    <link href="../css/mui.picker.css" rel="stylesheet" />
    <link href="../css/mui.poppicker.css" rel="stylesheet" />
    <link href="../css/iconfont.css" rel="stylesheet" />
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

        a:link {
            color: #aab2bd;
        }

        a:visited {
            color: #aab2bd;
        }

        a:hover {
            color: #aab2bd;
        }

        a:active {
            color: #aab2bd;
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

        .icon_fix {
            color: red;
        }

        .header {
            font-family: "微软雅黑";
            font-size: large;
            color: white;
        }
    </style>
</head>
<body>
    <header class="mui-bar mui-bar-nav header">
        <a class="mui-action-back mui-icon mui-icon-left-nav mui-pull-left"></a>
        <h1 class="mui-title">物料明细</h1>
    </header>
    <div class="mui-content">
        <div class="mui-slider-group">
            <div id="item1mobile" class="mui-slider-item mui-control-content mui-active" style="height: 35em">
                <div id="scroll1" class="mui-scroll-wrapper">
                    <div class="mui-scroll">
                        <ul class="mui-table-view mui-table-view-striped mui-table-view-condensed" id="list1">
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>

</body>
<script src="../js/jquery-1.11.0.min.js"></script>
<script src="../js/dingtalk.js"></script>
<%--<script src="../js/ddjs.js"></script>--%>
<script src="../js/mui.min.js"></script>
<script src="../js/mui.picker.js"></script>
<script src="../js/mui.poppicker.js"></script>
<script type="text/javascript">
    var data_list;

    var strMatreialCode = GetQueryString("code");

    (function ($) {
        getFList();

    })(mui);

    function GetQueryString(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null)
            return unescape(r[2]);
        return null;
    }

    //待配送查询函数<a href="MaterailSqlWeb.aspx">MaterailSqlWeb.aspx</a>
    function getFList(ob) {
        var strMaterialName = "";
        var strFactory = "";
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
                    strHtml += "<div class=\"list_info\"><div>物料名称：<span class=\"wlmc" + js[i].wldm + "\">" + js[i].wlmc + "</span></div></div>";
                    strHtml += "<div class=\"list_info\"><div>物料型号：<span class=\"wlmc" + js[i].wldm + "\">" + js[i].wlxh + "</span></div></div>";
                    strHtml += "<div class=\"list_info\"><div>物料代码：<span class=\"wlmc" + js[i].wldm + "\">" + js[i].wldm + "</span></div></div>";
                    strHtml += "<div class=\"list_info\"><div>大小量纲：<span class=\"wlmc" + js[i].wldm + "\">" + js[i].dxlg + "</span></div></div>";
                    strHtml += "<div class=\"list_info\"><div>物料组：<span class=\"wlmc" + js[i].wldm + "\">" + js[i].wlz + "</span></div></div>";
                    strHtml += "<div class=\"list_info\"><div>基本计量单位：<span class=\"wlmc" + js[i].wldm + "\">" + js[i].jbjldw + "</span></div></div>";
                    strHtml += "<div class=\"list_info\"><div>批次：<span class=\"wlmc" + js[i].wldm + "\">" + js[i].pc + "</span></div></div>";
                    strHtml += "<div class=\"list_info\"><div>净重：<span class=\"wlmc" + js[i].wldm + "\">" + js[i].jz + "</span></div></div>";
                    strHtml += "<div class=\"list_info\"><div>重量单位：<span class=\"wlmc" + js[i].wldm + "\">" + js[i].zldw + "</span></div></div>";
                    strHtml += "<div class=\"list_info\"><div>工厂：<span class=\"wlmc" + js[i].wldm + "\">" + js[i].gc + "</span></div></div>";
                    strHtml += "<div class=\"list_info\"><div>库存地点：<span class=\"wlmc" + js[i].wldm + "\">" + js[i].kcdd + "</span></div></div>";
                    strHtml += "<div class=\"list_info\"><div>库存：<span class=\"wlmc" + js[i].wldm + "\">" + js[i].kc + "</span></div></div>";
                    strHtml += "</div>";
                    li = document.createElement('li');
                    li.id = js[i].wldm;
                    li.title = "";
                    li.className = 'mui-table-view-cell listOne';
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
</script>
</html>

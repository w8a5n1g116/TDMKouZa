<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterialStockMoveList.aspx.cs" Inherits="DDpage.KmrStorage.MaterialStockMoveList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>掌上仓储-移库</title>
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
    </style>
</head>
<body>
    <header class="mui-bar mui-bar-nav header">
        <a class="mui-action-back mui-icon mui-icon-left-nav mui-pull-left"></a>
        <h1 class="mui-title">物料移库</h1>
    </header>
    <div class="mui-content">
        <div class="mui-content-padded" style="margin: 5px;">
            <form class="mui-input-group font_fix">
                <div class="mui-input-row">
                    <label>调入出库</label>
                    <div id="PStock">
                        <span id="factory" class="font_fix2">请选择...</span>
                    </div>
                </div>
                <div class="mui-input-row">
                    <label>调出仓库</label>
                    <div id="PPStock">
                        <span id="PPfactory" class="font_fix2">请选择...</span>
                    </div>
                </div>
                <div class="mui-input-row">
                    <label>物料名称</label>
                    <input id="Material" type="text" class="mui-input-clear font_fix" placeholder="请输入物料名称" />
                </div>
                <div class="mui-button-row" id="query">
                    <button type="button" class="mui-btn mui-btn-primary" id="queryBtn" onclick="getFList()">查询</button>
                </div>
            </form>
        </div>



        <div id="slider" class="mui-slider">
            <div id="sliderSegmentedControl" class="mui-slider-indicator mui-segmented-control mui-segmented-control-inverted">
                <a class="mui-control-item" href="#item1mobile">物料列表
                </a>
            </div>
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
    </div>
    <script src="../js/jquery-1.11.0.min.js"></script>
    <script src="../js/dingtalk.js"></script>
    <%--<script src="../js/ddjs.js"></script>--%>
    <script src="../js/mui.min.js"></script>
    <script src="../js/mui.picker.js"></script>
    <script src="../js/mui.poppicker.js"></script>
    <script>

        var _config = {
            appId: '<%=appId%>',
            corpId: '<%=corpId%>',
            timeStamp: '<%=timestamp%>',
            nonce: '<%=nonceStr%>',
            signature: '<%=signature%>',
            title: '<%=title%>'
        };
        var userinfo = "";
        var OpenLink;
        var preImages;
        var upImgFromCameras;

        dd.config({
            appId: _config.appId,
            corpId: _config.corpId,
            timeStamp: _config.timeStamp,
            nonceStr: _config.nonce,
            signature: _config.signature,
            jsApiList: ['biz.user.get', 'device.geolocation.get', 'biz.map.locate', 'biz.util.uploadImageFromCamera', 'biz.util.uploadImage']
        });


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
        var arrayStock = Array();
        var dataStr = Array();
        var factory = "";
        var materialcode = "";

        var JsonUserInfo = '<%=user_info%>';

        var strArray = new Array();
        strArray = JsonUserInfo.split(",")
        _cardId = strArray[0];
        _company = strArray[1];
        _dept = strArray[2];
        _name = strArray[3];
        _venture = strArray[4];
        _unit = strArray[5];


        (function ($) {
            $('.mui-scroll-wrapper').scroll({
                indicators: true //是否显示滚动条
            });


        })(mui);


        (function ($) {
            $('.mui-scroll-wrapper').scroll({
                indicators: true //是否显示滚动条
            });

            getFList();

        })(mui);

        //查询按钮loading处理,查询数据
        var strCompany = "";
        var strDept = "";
        var strVenture = "";
        var strOrder = "";
        var strStock = "";
        var strMatName = "";

        //发料仓库下拉列表
        (function ($, doc) {
            $.init();
            $.ready(function () {
                var _getParam = function (obj, param) {
                    return obj[param] || '';
                };

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

                        for (var i = 0; i < js.length; i++) {
                            arrayStock.push({ value: js[i], text: js[i] });
                        }

                    },
                    error: function (xhr, type, errorThrown) {
                        //异常处理；
                        alert(errorThrown);
                        console.log(type);
                    }
                });
                var dataPicekerStock = new $.PopPicker();
                dataPicekerStock.setData(arrayStock);

                var showStockButton = doc.getElementById('factory');
                var stockResult = doc.getElementById('PStock');

                showStockButton.addEventListener('tap', function (event) {
                    dataPicekerStock.show(function (items) {
                        showStockButton.innerText = items[0].text;
                    });
                }, false);
            });

        })(mui, document);
        //发料仓库下拉列表

        //查询按钮loading处理,查询数据<a href="MaterialStockInfo.aspx">MaterialStockInfo.aspx</a>
        function checkdetail_click(obj) {
            var key = $(obj).attr("id");
            location.href = "MaterailStockList.aspx?code=" + key;
        }

        //待配送查询函数<a href="MaterailSqlWeb.aspx">MaterailSqlWeb.aspx</a>
        function getFList(ob) {
            var strFactory = $("#factory").html();

            var strMaterialName = $("#Material").val();
            if (strFactory == "请选择...") {
                strFactory = "";
            }
            var fragment = document.createDocumentFragment();
            ajaxUrl = "../api/KmrStorage/MaterailSqlWeb.aspx";
            mui.ajax(ajaxUrl, {
                data: { Factory: strFactory, MaterialName: strMaterialName },
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
                        strHtml += "<div class=\"list_info\"><div  onclick='checkdetail_click(this)' id='" + js[i].wldm + "'>物料名称：<span class=\"wlmc" + js[i].wldm + "\">" + js[i].wlmc + "</span></div></div>";
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
</body>
</html>
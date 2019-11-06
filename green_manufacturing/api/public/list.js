function go(e) {
    var code = e.getAttribute("data-id");
    dd.ui.nav.preload({
        pages: [ //需要预加载的页面数组
          {
              id: 'page_id4', //预加载的页面id
              url: 'report.aspx?code=' + code //页面url
          }
        ],
        onSuccess: function (data) { // 回调通知预加载结果
            dd.ui.nav.go({
                id: 'page_id4', //要跳转的页面id，一般是先调用```dd.ui.nav.preload```预加载成功后，再调用```dd.ui.nav.go```跳转
                onSuccess: function (data) {

                },
                onFail: function (err) {

                }
            })
        },
        onFail: function (err) {
        } //回调通知预加载调用失败
    });
}

function sh(e)
{
    var code = e.getAttribute("data-id");
    dd.device.notification.confirm({
        message: "计划审核通过？",
        title: "请确认",
        buttonLabels: ['确定', '取消'],
        onSuccess: function (result) {
            if (result.buttonIndex == 0)
            {
                $.post("../../api/plan/sh.aspx", { code: code }, function (fd) {
                    var mydate = new Date();
                    var date = (mydate.getMonth() + 1);
                    list(date);
                })
            }
            //onSuccess将在点击button之后回调
            /*
            {
                buttonIndex: 0 //被点击按钮的索引值，Number类型，从0开始
            }
            */
        },
        onFail: function (err) { }
    });
}

function pj(e)
{
    var code = e.getAttribute("data-id");
    apiready = function () {
        api.parseTapmode();
    }
    var dialog = new auiDialog();
    dialog.prompt({
        title: "填写评价",
        text1: '完成情况',
        text2: '审核金额',
        type: 'number',
        buttons: ['取消', '确定']
    }, function (ret) {
        if (ret.buttonIndex == 2) {
            $.post("../../api/plan/pj.aspx", { code:code,text: ret.text1, money: ret.text2, wc: ret.text3, jg: ret.text4 }, function (fd)
            {
                dd.device.notification.toast({
                    icon: 'success', //icon样式，有success和error，默认为空 0.0.2
                    text: "评价完成", //提示信息
                    duration: 2, //显示持续时间，单位秒，默认按系统规范[android只有两种(<=2s >2s)]
                    delay: 0, //延迟显示，单位秒，默认0
                    onSuccess: function (result) {
                        /*{}*/
                    },
                    onFail: function (err) { }
                })
            })
        }
    })
}

function lhk_bc(e)
{
    var code = e.getAttribute("data-id");
    apiready = function () {
        api.parseTapmode();
    }
    var dialog = new auiDialog();
    dialog.prompts({
        title: "填写量化卡",
        text1: '完成情况',
        text2: '自评金额',
        type: 'number',
        buttons: ['取消', '确定']
    }, function (ret) {
        if (ret.buttonIndex == 2) {
            if (ret.text1.length > 0 && ret.text2.length > 0)
                $.post("../../api/lhk/bc.aspx", { code: code, text: ret.text1, money: ret.text2 }, function (fd) {
                    var mydate = new Date();
                    var date = (mydate.getMonth() + 1)
                    lhk_list1(date);
                    dd.device.notification.toast({
                        icon: 'success', //icon样式，有success和error，默认为空 0.0.2
                        text: "自评完成", //提示信息
                        duration: 2, //显示持续时间，单位秒，默认按系统规范[android只有两种(<=2s >2s)]
                        delay: 0, //延迟显示，单位秒，默认0
                        onSuccess: function (result) {
                            /*{}*/
                        },
                        onFail: function (err) { }
                    })
                })
            else
            {
                alert("请填写完成情况与自评金额！");
            }
        }
    })
}

function sh_up(title,text1,text2,url,code)
{

    apiready = function () {
        api.parseTapmode();
    }
    var dialog = new auiDialog();
    dialog.prompts({
        title: title,
        text1: text1,
        text2: text2,
        type: 'number',
        buttons: ['取消', '确定']
    }, function (ret) {
        if (ret.buttonIndex == 2) {
            if (ret.text1.length > 0 && ret.text2.length > 0)
                $.post(url, { code: code, text: ret.text1, money: ret.text2, userid: userid }, function (fd) {
                    var mydate = new Date();
                    var date = (mydate.getMonth() + 1)
                    lhk_list1(date);
                    dd.device.notification.toast({
                        icon: 'success', //icon样式，有success和error，默认为空 0.0.2
                        text: "完成", //提示信息
                        duration: 2, //显示持续时间，单位秒，默认按系统规范[android只有两种(<=2s >2s)]
                        delay: 0, //延迟显示，单位秒，默认0
                        onSuccess: function (result) {
                            /*{}*/
                        },
                        onFail: function (err) { }
                    })
                })
            else {
                alert("请填写完成情况与自评金额！");
            }
        }
    })
}

function lhk_sh(e)
{
    var code = e.getAttribute("data-id");
    var user = e.getAttribute("data-user");
  
    dd.biz.user.get({
        onSuccess: function (info) {
            if (user == info.nickName) {
                dd.device.notification.confirm({
                    message: "您可以选择填写自己的量化卡或审核量化卡",
                    title: "请选择",
                    buttonLabels: ['填写', '审核'],
                    onSuccess: function (result) {
                        switch (result.buttonIndex) {
                            case 0:
                                {
                                    sh_up("填写量化卡", "完成情况", "自评金额", "../../api/lhk/bc.aspx", code);
                                    break;
                                }
                            case 1:
                                {
                                    sh_up("审核量化卡", "审核评语", "审核金额", "../../api/lhk/sh.aspx", code);
                                    break;
                                }
                        }
                        //onSuccess将在点击button之后回调
                        /*
                        {
                            buttonIndex: 0 //被点击按钮的索引值，Number类型，从0开始
                        }
                        */
                    },
                    onFail: function (err) { }
                });
            }
            else
            {
                sh_up("审核量化卡", "审核评语", "审核金额", "../../api/lhk/sh.aspx", code);
            }
        },
        onFail: function (err) {
            logger.e('userGet fail: ' + JSON.stringify(err));
        }
    });
    
}

function lhk_list1(date) {
    $.post("../../api/lhk/list.aspx", { userid: userid, date: date }, function (fd) {

        var data = JSON.parse(fd);
        var text = "";
        for (var i = 0; i < data.list.length; i++) {

            text += '<div class="list" style="padding-bottom: 0;" data-id="' + data.list[i].id + '" data-user="' + data.list[i].user + '" onclick="' + data.list[i].fun + '"><div class="list_line1" style="justify-content: space-around; padding: 0 0.5em"><div class="list_type" style="margin: 0;">计划</div><div class="project">' + data.list[i].type + '</div><div class="name">' + data.list[i].item + '</div><div class="date">' + data.list[i].date + '</div></div><hr />';

            text += '<div class="list_line2" style="background: #DDDDDD; font-size: 0.9em; flex-direction: column; align-items: flex-start; padding: 0.5em"><div class="line_text1"><div>指标/目标：</div><div>' + data.list[i].text + '</div></div><div class="line_text1"><div>评价标准：</div><div>' + data.list[i].pj_text + '</div></div><div class="line_text2"><div><span>权重：</span><span>' + data.list[i].qz + '</span></div><div><span>金额：</span><span>' + data.list[i].money + '</span></div></div><div class="line_text2"><div><div>责任人：</div><div>' + data.list[i].zr_name + '</div></div><div><div>责任班：</div><div>' + data.list[i].zr_f + '</div></div></div>';

            if (data.list[i].wc.length > 0) {
                text += '<div class="line_text1"><div>自评金额：</div><div>' + data.list[i].zp + '</div></div><div class="line_text3"><span>完成情况说明：</span><div>' + data.list[i].wc + '</div></div>';
            }
            if (data.list[i].sh_text.length > 0) {
                text += '<div class="line_text1"><div>审核金额：</div><div>' + data.list[i].sh + '</div></div><div class="line_text3"><span>审核评语：</span><div>' + data.list[i].sh_text + '</div></div>';
            }
            text += '</div></div>';

        }
        $("#lhk").html(text);
        $.getScript('../../api/public/list.js', function () {
        });
    })
}
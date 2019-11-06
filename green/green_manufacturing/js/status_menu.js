function menu(data) {
    console.log(data);
    var html = "";
    html += '<div class="menu_box"><div class="menu_bk" onclick="close_menu()"></div>';
    html += '<div class="menu_top_arrow"></div>';
    html += '<div class="menu_body">';
    for (var i = 0; i < data.length; i++) {
        if (i > 0) {
            html+="<hr/>"
        }
        html += '<div class="menu_line" onclick="menu_click(this)" num="' + data[i].text + '"><div><i class="icon iconfont ' + data[i].icon + '"></i></div><div>' + data[i].text + '</div></div>';
    }
    html += '</div></div>';
    $("body").append(html);
    $("body").css({
        "overflow-y": "hidden",
        "overflow-x": "hidden"
    });
}

function close_menu() {
    $(".menu_box").remove();
    $("body").css({
        "overflow-y": "auto",
        "overflow-x": "auto"
    });
}
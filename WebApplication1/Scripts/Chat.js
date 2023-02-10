$(document).ready(function () {
    $('#btnLogin').click(function () {
        var nickName = ("#txtUserName").val()
        if (nickName) {
            var href = "/Chat?user=" + encodeURIComponent(nickName)
            href = href + "&logOn=true"
            $('#LoginButton').attr('href', href).click()
            $('#UserName').text(nickName)
        }
    })
})

function LoginOnSucces(result) {
    /*Scroll();*/
    ShowLastRefresh();

    setTimeout("Refresh()", 4000)

    $('#txtMessage').keydown(function (e) {
        if (e.keyDown == 13) {
            e.preventDefault();
            $("#btnMessage").click();
        }
    })
    $("#btnMessage").click(function () {
        var text = $("#txtMessage").val();
        if (text) {
            var href = "/Chat?user=" + encodeURIComponent($"#UserName").val();
            href = href + "&chatMessage=" + encodeURIComponent(text);
            $("#ActionLInk").attr("href", href).click();
        }
    })

    $("#btnLogOFF").click(function () {
        var href = "/Chat?user=" + encodeURIComponent($"#UserName").val();
        href = href + "&logOff=true";
        $("#ActionLink").attr("href", href).click();

        document.location.href = "Chat";
    })
}

function LofinOnFailure(result) {
    $("#UserName").val("");
    $("#Error").text(result.responceText);
    setTimeout("$('#Error').empty()", 2000);
}

function Refresh() {
    var href = "/Chat?user=" + encodeURIComponent($("#UserName").text();

    $("#ActionLink").attr("href", href).click();
    setTimeout("Refresh();", 5000)
}

function ChatOnFailure() {
    $("#Error").text(result.responseText);
    setTimeout("$('#Error').empty()", 2000);
}

function ChatOnSuccess() {
/*    Scroll();*/
    ShowLastRefresh();

    setTimeout("Refresh()", 4000)

    $('#txtMessage').keydown(function (e) {
        if (e.keyDown == 13) {
            e.preventDefault();
            $("#btnMessage").click();
        }
    })
    $("#btnMessage").click(function () {
        var text = $("#txtMessage").val();
        if (text) {
            var href = "/Chat?user=" + encodeURIComponent($"#UserName").text();
            href = href + "&chatMessage=" + encodeURIComponent(text);
            $("#ActionLInk").attr("href", href).click();
        }
    })

    $("#btnLogOFF").click(function () {
        var href = "/Chat?user=" + encodeURIComponent($"#UserName").text();
        href = href + "&logOff=true";
        $("#ActionLink").attr("href", href).click();

        document.location.href = "Chat";
    })
}

function Scroll() {
    var win = $('#Message')
    var height = win[0].scrollHeight;
    win.scrollTop(height)
}

function showLastRefresh() {
    var dt = new Date();
    var time = dt.getHours() + ":" + dt.getMinutes + ":" + dt.getSeconds;
    $('#LastRefresh').text("Last update: " + time);
}

$("#MauChu").addClass(checkCookie());
$(".nav-link").addClass(checkCookie());
$(".dropdown-item").addClass(checkCookie());
$("span").addClass(checkCookie());
//Màu nền
$(".bg-faded").css("background-color", checkCookie2());
function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}
function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}
function checkCookie() {
    var mauChu1 = getCookie("MauChu");
    if (mauChu1 != "") {
        return mauChu1;
    } else {
        setCookie('MauChu', 'text-dark', 30);
    }
};
function checkCookie2() {
    var mauNen1 = getCookie("MauNen");
    if (mauNen1 != "") {
        return mauNen1;
    } else {
        setCookie('MauNen', 'alert alert-light', 30);
    }
};
//Nhiều theme
$('.btnMauNen1').on('click', function () {
    var mauChu = $('.btnMauNen1').data('color');
    setCookie('MauNen', mauChu, 30);
    window.location.href = $(location).attr('href');
});
$('.btnMauNen2').on('click', function () {
    var mauChu = $('.btnMauNen2').data('color');
    setCookie('MauNen', mauChu, 30);
    window.location.href = $(location).attr('href');
});
$('.btnMauNen3').on('click', function () {
    var mauChu = $('.btnMauNen3').data('color');
    setCookie('MauNen', mauChu, 30);
    window.location.href = $(location).attr('href');
});
$('.btnMauNen4').on('click', function () {
    var mauChu = $('.btnMauNen4').data('color');
    setCookie('MauNen', mauChu, 30);
    window.location.href = $(location).attr('href');
});
//Màu chữ
$('.btnMauChu1').on('click', function () {
    var mauChu = $('.btnMauChu1').data('color');
    setCookie('MauChu', mauChu, 30);
    window.location.href = $(location).attr('href');
});
$('.btnMauChu2').on('click', function () {
    var mauChu = $('.btnMauChu2').data('color');
    setCookie('MauChu', mauChu, 30);
    window.location.href = $(location).attr('href');
});
$('.btnMauChu3').on('click', function () {
    var mauChu = $('.btnMauChu3').data('color');
    setCookie('MauChu', mauChu, 30);
    window.location.href = $(location).attr('href');
});
$('.btnMauChu4').on('click', function () {
    var mauChu = $('.btnMauChu4').data('color');
    setCookie('MauChu', mauChu, 30);
    window.location.href = $(location).attr('href');
});
$('.btnMauChu5').on('click', function () {
    var mauChu = $('.btnMauChu5').data('color');
    setCookie('MauChu', mauChu, 30);
    window.location.href = $(location).attr('href');
});
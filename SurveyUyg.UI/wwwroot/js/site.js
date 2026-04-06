function FormatDate(d) {
    var date = new Date(d);
    if (isNaN(date)) return "";

    var day = date.getDate() > 9 ? date.getDate() : '0' + date.getDate();
    var mount = (date.getMonth() + 1) > 9 ? (date.getMonth() + 1) : '0' + (date.getMonth() + 1);
    var year = date.getFullYear();
    var hour = date.getHours() > 9 ? date.getHours() : '0' + date.getHours();
    var minute = date.getMinutes() > 9 ? date.getMinutes() : '0' + date.getMinutes();
    var second = date.getSeconds() > 9 ? date.getSeconds() : '0' + date.getSeconds();

    return day + "." + mount + "." + year + " " + hour + ":" + minute + ":" + second;
}

function parseJwt(token) {
    try {
        var base64Url = token.split('.')[1];
        var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        var jsonPayload = decodeURIComponent(window.atob(base64).split('').map(function (c) {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        }).join(''));

        return JSON.parse(jsonPayload);
    } catch (e) {
        return null;
    }
}

$.ajaxSetup({
    beforeSend: function (xhr) {
        var token = localStorage.getItem("token");
        if (token) {
            xhr.setRequestHeader('Authorization', 'Bearer ' + token);
        }
    },
    error: function (xhr) {
        if (xhr.status === 401) {
            localStorage.removeItem("token");
            location.href = "/Login";
        }
        if (xhr.status === 403) {
            alert("Bu işlem için yetkiniz bulunmamaktadır!");
        }
    }
});

var token = localStorage.getItem("token");
var userRoles = [];

$(document).ready(function () {
    if (token == null) {
        $(".NotLogined").show();
        $(".Logined").hide();
        $(".admin-only").hide();
    } else {
        $(".NotLogined").hide();
        $(".Logined").show();

        var payload = parseJwt(token);
        if (payload) {
            var username = payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
            userRoles = payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
            var userPhoto = payload["UserPhoto"];

            $("#UserName").html(username);
            $("#ImgUserPhoto").attr("src", "https://localhost:7218/Files/UserPhotos/" + (userPhoto || "default.png"));

            var isAdmin = false;
            if (Array.isArray(userRoles)) {
                isAdmin = userRoles.includes("Admin");
            } else {
                isAdmin = (userRoles === "Admin");
            }

            if (isAdmin) {
                $("#divRole").html("Sistem Yönetici");
                $(".admin-only").show();
            } else {
                $("#divRole").html("Normal Üye");
                $(".admin-only").hide();
            }
        }
    }

    $("#Logout").click(function () {
        localStorage.removeItem("token");
        location.href = "/Login";
    });
});
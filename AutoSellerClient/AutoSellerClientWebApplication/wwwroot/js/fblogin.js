window.fbAsyncInit = function () {
    FB.init({
        appId: 921088648551296,
        status: true,
        version: 'v13.0',
        xfbml: true,
        cookie: true,
        locallocalStorage: false
    });
    (function (d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) { return; }
        js = d.createElement(s); js.id = id;
        js.src = "https://connect.facebook.net/en_US/sdk.js";
        fjs.parentNode.insertBefore(js, fjs);
    }(document, 'script', 'facebook-jssdk'));

    FB.AppEvents.logPageView();
};

function checkLoginStateRegister() {
    FB.getLoginStatus(function (response) {
        var accessToken = response["authResponse"]["accessToken"];
        var loginProvider = "Facebook";
        $("#accessToken").val(accessToken);
        $("#externalLogin").val(loginProvider);
        $("#userRegistrationForm").submit();
    });
}


function checkLoginStateLogin() {
    FB.getLoginStatus(function (response) {
        var accessToken = response["authResponse"]["accessToken"];
        var loginProvider = "Facebook";
        $("#loginAccessToken").val(accessToken);
        $("#loginExternalLogin").val(loginProvider);
        $("#localLoginForm").submit();
    });
}

function setOAuthUserPasswordLoginState() {
    FB.getLoginStatus(function (response) {
        var accessToken = response["authResponse"]["accessToken"];
        var loginProvider = "Facebook";
        $("#setOAuthUserAccessToken").val(accessToken);
        $("#setOAuthProvider").val(loginProvider);
    });
}

//TODO: Make sense of this!
$(document).ready(function() {
    //Login-Local
    $(".login-button").click(function() {
        console.log("login");
        $("#localLoginForm").submit();
    });
    //Register-FBLogin
    $(".fb-login-button").click(function() {
        console.log("register");
    });
});

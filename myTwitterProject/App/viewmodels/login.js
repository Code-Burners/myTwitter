define(['durandal/app', 'knockout', 'plugins/router', 'durandal/system'],

    function (app, ko, router, system) {
    var name = ko.observable();
        

    $(function () {


        $.ajax({
            type: "POST",
            url: "/token",
            data: {
                grant_type: "password",
                UserName: "2",
                Password: "1"
            },
            dataType: "json"
        });
 


    });
      
    return {
        desString: "myTwitter is fun having lots of stuff regarding people and their daily activities. It keeps you updated on the trending stuff on the internet. People come here to releive their stress and it happens to be one of the best platform to take your life online with lots of people reading your tweets. Each and everyone can deliver their views regarding trending topics. Come! Join the fun.",
        name: name,
        password: ko.observable(),

        login: function () {
          
            $.ajax({
                type: "POST",
                url: "/token",
                data: {
                    grant_type: "password",
                    UserName: this.name(),
                    Password: this.password()
                },
                dataType: "json",
                success: function (data) {
                    
                    sessionStorage.setItem("username", ko.utils.unwrapObservable(name));
                    sessionStorage.setItem("token", data.access_token);
                    location.reload();
                    router.navigate('#welcome');

                },
                error: function (data) {
                    $("#error").show();
                    $("#error").text(data.responseJSON["error_description"]);
                    $("#error").fadeOut(6000);
                }
            });
        }
    }
});
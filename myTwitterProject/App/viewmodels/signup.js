﻿define(['durandal/app','knockout','plugins/router','durandal/system'],function (app,ko,router,system) {


    return {
        desString: "myTwitter is fun having lots of stuff regarding people and their daily activities. It keeps you updated on the trending stuff on the internet. People come here to releive their stress and it happens to be one of the best platform to take your life online with lots of people reading your tweets. Each and everyone can deliver their views regarding trending topics. Come! Join the fun.",
        password: ko.observable(),
        cPassword: ko.observable(),
        name: ko.observable(),
       
        signup: function () {
            
            $.ajax({
                type: "POST",
                url: "/api/Account/Register",
                data: {
                    UserName: this.name(),
                    Password: this.password(),
                    ConfirmPassword: this.cPassword()
                },
                dataType: "html",

                success: function (data) {
                    alert("Thankyou! You are successfully Registered. Please login!")
                router.navigate('');

                },
                error: function (data) {
                    alert("Oops..! Something went wrong. Please try again");
                    
                    
                }
            });
        }
    }




})
define(['durandal/app','knockout','plugins/router','durandal/system'],function (app,ko,router,system) {
   

    return {
        sName:sessionStorage.getItem("username"),
        newPass: ko.observable(),
        cNewPass: ko.observable(),
        oldPass: ko.observable(),

        change: function () {
            $("#delete, #text").hide();
            $("#changePassword").show();
            
        },

        changePass:function(){
            $.ajax({
                type: "POST",
                url: "/api/Account/ChangePassword",
                beforeSend: function (xhr) { xhr.setRequestHeader('Authorization', 'Bearer ' + sessionStorage.getItem("token")); },
                data: {
                    OldPassword: this.oldPass(),
                    NewPassword: this.newPass(),
                    ConfirmPassword: this.cNewPass()
                },
                dataType: "html",
                success: function (data) {
                    alert("Password changed successfully");

                },
                error: function (data) {
                    alert("Oops.. Something is not right. Please try again!");
                }
            });
        },

        deleteView: function () {
            $("#changePassword, #text").hide();
            $("#delete").show();
        },

        deleteAccount: function () {
            $.ajax({
                type: "POST",
                url: "/api/status/DeleteAcc",
                data: {
                    "Name":sessionStorage.getItem("username")
                },

                dataType: "html",
                success: function (data) {
                  
                    $.ajax({
                        type: "POST",
                        url: "/api/Account/RemoveLogin",
                        beforeSend: function (xhr) { xhr.setRequestHeader('Authorization', 'Bearer ' + sessionStorage.getItem("token")); },
                        data: {
                            LoginProvider: "Local",
                            ProviderKey: sessionStorage.getItem("username")
                        },
                        dataType: "html",
                        success: function (data) {
                            location.reload();
                            sessionStorage.clear();
                            router.navigate("");

                        },
                        error: function (data) {
                            alert("Oops...! Something went wrong");
                        }
                    });

                },
                error: function (data) {
                    alert("Oops...! Something went wrong");
                }
            });
           
        }
    }
});
define(['durandal/app', 'plugins/router', 'durandal/system', 'knockout'], function (app, router, system, ko) {

    var self = this;
    var time = new Date();
    var noOfStatus = 10;
    var fullStatus = [];
    var newVal;
    var likeStatus = false;
    var searchTerm = ko.observable();
    var status = ko.observable("");
    var comment = ko.observable("");
    var sName = sessionStorage.getItem("username");
    self.statuses = ko.observableArray([]);
    self.comments = ko.observableArray([]);
    self.likedBy = ko.observableArray([]);


    $(function () {


        $.get("api/status/Getstatus?NoOfStatus=10", function (data) {
            self.statuses(data);

        });



    });

    return {

        statuses: statuses,

        searchTerm: searchTerm,

        sName: sName,

        newVal: newVal,

        status: status,

        likedBy: likedBy,

        comment: comment,

        comments: comments,

        count: function () {

            var currentLength = $("#txtbox").val().length;
            $("#lblcount").text(50 - currentLength);


        },

        updateLike: function (id) {
            $.ajax(
                {
                    type: "GET",
                    url: "api/status/GetLikes?StatusId=" + id,
                    async: false,
                    success: function (data) {


                        for (var i = 0; i < data.length; i++) {
                            if (~data[i]["name"].indexOf(sName)) {

                                likeStatus = true;
                                break;
                            }
                        }
                    }
                });


            if (likeStatus === false) {
                $.ajax({
                    type: "POST",
                    url: "/api/status/Postlike/" + id,
                    async: false,
                    data: {
                        statusId: id,
                        Name: sessionStorage.getItem("username"),
                        like: (parseInt($("#" + id + "l").html()) + 1),
                    },
                    dataType: "html",

                    success: function (data) {
                        $("#" + id + "l").text(parseInt($("#" + id + "l").html()) + 1);
                        likeStatus = true;




                    },
                    error: function (data) {
                        alert("Oops..! Something went wrong. Please try again")
                    }
                })

            }

            else {
                $.ajax({
                    type: "POST",
                    url: "/api/status/Postlike/" + id,
                    data: {
                        statusId: id,
                        Name: sessionStorage.getItem("username"),
                        like: (parseInt($("#" + id + "l").html()) - 1),
                        date: "del"
                    },
                    dataType: "html",

                    success: function (data) {
                        $("#" + id + "l").text(parseInt($("#" + id + "l").html()) - 1);
                        likeStatus = false;



                    },
                    error: function (data) {
                        alert("Oops..! Something went wrong. Please try again")
                    }
                })


            }
            likeStatus = false;


        },

        displayLikes: function (id) {
            $.get("api/status/GetLikes?StatusId=" + id, function (data) {
                likedBy(data);
            });


        },

        deleteComment: function (id) {
            $.ajax({
                type: "DELETE",
                url: "/api/status/DeleteComment/" + id,

                success: function (data) {

                    $("#" + id + "main").hide();


                },
                error: function (data) {

                    alert("Oops..! Something went wrong");
                }
            })
        },

        deleteStatus: function (id) {
            $.ajax({
                type: "DELETE",
                url: "/api/status/Deletestatus/" + id,

                success: function (data) {

                    $("#" + id + "main").hide();


                },
                error: function (data) {

                    alert("Oops..! Something went wrong");
                }
            })
        },

        edit: function (id) {
            $("#" + id + "e").show();
            $("#" + id + "eb").show();
            $("#" + id).hide();
            $("#" + id + "e").val($("#" + id).text());

        },

        commentbox: function (id) {

            $("#commentbx").modal('show');


            commentId = id;


            $.get("api/status/GetComment/?StatusId=" + commentId, function (data) {
                self.comments(data);


            });


        },

        commentmsg: function () {
            query = {
                commentId: Date.now().toString().slice(2),
                StatusId: commentId,
                Name: sessionStorage.getItem("username"),
                comment: this.comment(),
                date: time.toString().slice(4, 15),
                time: time.toString().slice(16, 21)

            };
            $.ajax({
                type: "POST",
                url: "/api/status/PostComment",

                data: query,
                dataType: "json",
                success: function () {
                    //system.log(StatusId);
                    //system.log(commentId);
                    $.get("api/status/GetComment/?StatusId=" + commentId, function (data) {
                        self.comments(data);


                    });


                },
                error: function (data) {
                    alert("Oops..! Something went wrong. Just Chill and try again");
                }

            });
            $("#txtbox1").val("");
            alert("Commented successfully!");
        },

        update: function (id, like, date, time) {
            var val = this.newVal;
            $.ajax({
                type: "PUT",
                url: "/api/status/Putstatus/" + id,
                data: {
                    statusId: id,
                    Name: sessionStorage.getItem("username"),
                    statuses: this.newVal,
                    like: like,
                    date: date,
                    time: time

                },
                dataType: "json",
                success: function (data) {

                    $("#" + id).show();
                    $("#" + id).text(val);
                    $("#" + id + "e").hide();
                    $("#" + id + "eb").hide();




                },
                error: function (data) {
                    alert("Oops..! Something went wrong. Please try again");
                }
            })
        },

        share: function () {
            Idnew = Date.now().toString().slice(4);
            query = {
                StatusId: Idnew,
                Name: sessionStorage.getItem("username"),
                statuses: this.status(),
                like: "0",
                date: time.toString().slice(4, 15),
                time: time.toString().slice(16, 21)
            };
            $.ajax({
                type: "POST",
                url: "/api/status/Poststatus",

                data: query,
                dataType: "json",
                success: function () {

                    $.get("api/status/Getstatus/?NoOfStatus=" + (noOfStatus += 10), function (data) {
                        self.statuses(data)
                    })

                },
                error: function (data) {
                    alert("Oops..! Something went wrong. Just Chill and try again");
                }
            });
            $("#txtbox").val("");
            $("#lblcount").text(50);

        },

        search: function () {
            $("#searchbox").val("");
            $.get("api/status/SearchStatus?searchTerm=" + searchTerm(), function (data) {
                statuses(data);
            });

            $("#search").show();
            $("#welcomeTop, #statusShare, #showMore").hide();



        },

        refresh: function () {
            $("#search").hide();
            $("#welcomeTop, #statusShare, #showMore").show();

            $.get("api/status/Getstatus?NoOfStatus=" + (noOfStatus), function (data) {
                self.statuses(data)
            });



        },

        showMore: function () {
            $.get("api/status/Getstatus/?NoOfStatus=" + (noOfStatus += 10), function (data) {
                self.statuses(data)
            })

        },

        logout: function () {

            location.reload();
            sessionStorage.clear();
            router.navigate("");
        }
    };
});
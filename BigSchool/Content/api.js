﻿$(document).ready(function () {
    $(".js-tongle-attendence").click(function (e) {
        var button = $(e.target);
        $.post("/api/attendances", { Id: button.attr("data-course-id") })
            .done(function (result) {
                if (result == "cancel") {
                    alert("Cancel register course sucessfully!")
                    button
                        .removeClass("btn-info")
                        .addClass("btn-default")
                        .text("Going?");
                } else {
                    alert("Register course sucessfully!")
                    button
                        .removeClass("btn-default")
                        .addClass("btn-info")
                        .text("Going");
                }
            }).fail(function () {
                alert("Something fail!");
            });
    });

    $(".js-tongle-follow").click(function (e) {
        var button = $(e.target);
        var followee = button.attr("data-user-id");
        $.post("/api/followings", { FolloweeId: button.attr("data-user-id") })
            .done(function (result) {
                if (result == "cancel") {
                    $('.js-tongle-follow').each(function (i, obj) {
                        if ($(this).attr('data-user-id') == followee) {
                            $(this).removeClass("btn-success")
                                .addClass("btn-default")
                                .text("Follow");
                        }
                    });
                    alert("unFollow sucessfully")
                } else {
                    $('.js-tongle-follow').each(function (i, obj) {
                        if ($(this).attr('data-user-id') == followee) {
                            $(this).removeClass("btn-default")
                                .addClass("btn-success")
                                .text("Following");
                        }
                    });
                    alert("Follow sucessfully")
                }
            }).fail(function () {
                alert("Something fail!");
            });
    });
});
﻿function getStaffLeaveApproveModal(id) {
    var modelContent = $("#approveDiv")
    $("#approveDiv").empty();

    $.ajax({
        method: "GET",
        url: "/Home/GetApproveStaffLeaveModal/" + id,
        dataType: "html",
        cache: false,
    }).done(function (content) {
        if (content != null) {
            modelContent.html(content);
        }
        $("#staffLeaveApprove").modal("show");


    }).fail(function (error) {
        alert(error);
    });

}
function getCurrentlyOnStaffLeaveApproveModal(id) {
    var modelContent = $("#approveDiv")
    $("#approveDiv").empty();

    $.ajax({
        method: "GET",
        url: "/Home/GetCurrentlyOnStaffLeaveModal/" + id,
        dataType: "html",
        cache: false,
    }).done(function (content) {
        if (content != null) {
            modelContent.html(content);
        }
        $("#staffCurrentlyOnLeaveApprove").modal("show");


    }).fail(function (error) {
        alert(error);
    });

}
function getUpcomingStaffLeaveApproveModal() {
    var modelContent = $("#approveDiv")
    $("#approveDiv").empty();

    $.ajax({
        method: "GET",
        url: "/Home/GetUpcomingStaffShiftModal",
        dataType: "html",
        cache: false,
    }).done(function (content) {
        if (content != null) {
            modelContent.html(content);
        }
        $("#staffUpcomingLeaveApprove").modal("show");


    }).fail(function (error) {
        alert(error);
    });

}

function getStaffShiftApproveModal(id) {
    var modelContent = $("#approveDiv")
    $("#approveDiv").empty();

    $.ajax({
        method: "GET",
        url: "/Home/GetApproveStaffShiftModal/" + id,
        dataType: "html",
        cache: false,
    }).done(function (content) {
        if (content != null) {
            modelContent.html(content);
        }
        $("#staffShiftApprove").modal("show");


    }).fail(function (error) {
        alert(error);
    });

}

function getStaffPaymentApproveModal(id) {
    var modelContent = $("#approveDiv")
    $("#approveDiv").empty();

    $.ajax({
        method: "GET",
        url: "/Home/GetApproveStaffPaymentModal/" + id,
        dataType: "html",
        cache: false,
    }).done(function (content) {
        if (content != null) {
            modelContent.html(content);
        }
        $("#staffPaymentApprove").modal("show");


    }).fail(function (error) {
        alert(error);
    });

}

function generalTypeChange() {
    var modelContent = $("#systemVariableTable")
    $("#systemVariableTable").empty();

    $("#btnGetLeaveTypes").removeClass("active");
    $("#btnGetLeaveRules").removeClass("active");
    $("#btnGetRoles").removeClass("active");
    $("#btnGetCompanies").removeClass("active");
    $("#btnGetColorCodes").removeClass("active");
    $("#btnGetCompanyBranchs").removeClass("active");

    var id = document.getElementById("kt_select2_1").value;

    if (id > 0) {
        $.ajax({
            method: "GET",
            url: "/SystemVariable/GetGeneralTypeById/" + id,
            dataType: "html",
            cache: false,
        }).done(function (content) {
            localStorage.SystemVariableId = id;
            if (content != null) {
                modelContent.html(content);
            }
        }).fail(function (error) {
            alert(error);
        });
    }
}

function GetLeaveTypes() {
    $("#btnGetRoles").removeClass("active");
    $("#btnGetCompanyBranchs").removeClass("active");
    $("#btnGetCompanies").removeClass("active");
    $("#btnGetLeaveRules").removeClass("active");
    $("#btnGetColorCodes").removeClass("active");
    $("#btnGetLeaveTypes").addClass("active");

    var modelContent = $("#systemVariableTable")
    $("#systemVariableTable").empty();
    localStorage.SystemVariableId = 1000;

    $.ajax({
        method: "GET",
        url: "/SystemVariable/GetLeaveTypeList/",
        dataType: "html",
        cache: false,
    }).done(function (content) {
        if (content != null) {
            modelContent.html(content);
        }
    }).fail(function (error) {
        alert(error);
    });

}
function GetLeaveRules() {
    $("#btnGetLeaveTypes").removeClass("active");
    $("#btnGetRoles").removeClass("active");
    $("#btnGetCompanyBranchs").removeClass("active");
    $("#btnGetCompanies").removeClass("active");
    $("#btnGetColorCodes").removeClass("active");
    $("#btnGetLeaveRules").addClass("active");

    var modelContent = $("#systemVariableTable")
    $("#systemVariableTable").empty();
    localStorage.SystemVariableId = 1001;

    $.ajax({
        method: "GET",
        url: "/SystemVariable/GetLeaveRuleList/",
        dataType: "html",
        cache: false,
    }).done(function (content) {
        if (content != null) {
            modelContent.html(content);
        }
    }).fail(function (error) {
        alert(error);
    });

}

function GetCompanies() {
    $("#btnGetRoles").removeClass("active");
    $("#btnGetLeaveTypes").removeClass("active");
    $("#btnGetLeaveRules").removeClass("active");
    $("#btnGetCompanyBranchs").removeClass("active");
    $("#btnGetColorCodes").removeClass("active");
    $("#btnGetCompanies").addClass("active");
    var modelContent = $("#systemVariableTable")
    $("#systemVariableTable").empty();
    localStorage.SystemVariableId = 1002;

    $.ajax({
        method: "GET",
        url: "/SystemVariable/GetCompanyList/",
        dataType: "html",
        cache: false,
    }).done(function (content) {
        if (content != null) {
            modelContent.html(content);
        }
    }).fail(function (error) {
        alert(error);
    });
}
function GetRoles() {
    $("#btnGetLeaveTypes").removeClass("active");
    $("#btnGetLeaveRules").removeClass("active");
    $("#btnGetCompanyBranchs").removeClass("active");
    $("#btnGetCompanies").removeClass("active");
    $("#btnGetColorCodes").removeClass("active");
    $("#btnGetRoles").addClass("active");
    var modelContent = $("#systemVariableTable")
    $("#systemVariableTable").empty();
    localStorage.SystemVariableId = 1003;
    $.ajax({
        method: "GET",
        url: "/SystemVariable/GetRoleList/",
        dataType: "html",
        cache: false,
    }).done(function (content) {
        if (content != null) {
            modelContent.html(content);
        }
    }).fail(function (error) {
        alert(error);
    });
}
function GetCompanyBranches() {
    $("#btnGetLeaveTypes").removeClass("active");
    $("#btnGetLeaveRules").removeClass("active");
    $("#btnGetRoles").removeClass("active");
    $("#btnGetCompanies").removeClass("active");
    $("#btnGetColorCodes").removeClass("active");
    $("#btnGetCompanyBranchs").addClass("active");
    var modelContent = $("#systemVariableTable")
    $("#systemVariableTable").empty();
    localStorage.SystemVariableId = 1004;
    $.ajax({
        method: "GET",
        url: "/SystemVariable/GetCompanyBranchList/",
        dataType: "html",
        cache: false,
    }).done(function (content) {
        if (content != null) {
            modelContent.html(content);
        }
    }).fail(function (error) {
        alert(error);
    });
}
function GetColorCodes() {
    debugger;
    $("#btnGetLeaveTypes").removeClass("active");
    $("#btnGetLeaveRules").removeClass("active");
    $("#btnGetRoles").removeClass("active");
    $("#btnGetCompanies").removeClass("active");
    $("#btnGetCompanyBranchs").removeClass("active");
    $("#btnGetColorCodes").addClass("active");
    var modelContent = $("#systemVariableTable")
    $("#systemVariableTable").empty();
    localStorage.SystemVariableId = 1005;
    $.ajax({
        method: "GET",
        url: "/SystemVariable/GetColorCodeList/",
        dataType: "html",
        cache: false,
    }).done(function (content) {
        if (content != null) {
            modelContent.html(content);
        }
    }).fail(function (error) {
        alert(error);
    });
}
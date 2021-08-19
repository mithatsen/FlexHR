// Modal Add Staff 

// Create a FormValidation instance
var fv = FormValidation.formValidation(document.getElementById('AddAuthorizeForm'),
    {
        fields: {

            UserId: {
                validators: {
                    notEmpty: {
                        message: 'Kullanıcı seçiniz'
                    },

                }
            },

        },
        plugins: {
            trigger: new FormValidation.plugins.Trigger(),
            // Bootstrap Framework Integration
            bootstrap: new FormValidation.plugins.Bootstrap(),

        }
    });

function staffAddAuthorizeTypeClickFunction() {
    fv.validate().then(function (status) {
        // Update the login button content based on the validation status
        if (status == 'Valid') {
            console.log($("#ContractTypeSelect").val());
            var formData = {

                Roles: $("#roles").val(),
                Users: $("#users").val(),
                AuthorizeTypes: $("#authorizeRoles").val(),

            };
            $.ajax({
                method: 'post',
                url: '/RoleAssign/AddAuthorizeWithAjax',
                data: formData,
                success: function (data) {
                    if (data == "true") {
                        Swal.fire({
                            title: "Eklendi!",
                            text: "Kaydınız eklendi.",
                            icon: "success",
                        }).then(function () {
                            window.location.reload()
                        })
                    } else {

                        Swal.fire({
                            title: "Başarısız!",
                            text: "Kullanıcı adı zaten var.",
                            icon: "error",
                        }).then(function () {
                            window.location.reload()
                        })
                    }

                }

            })
        }
    });
}

function userChange() {

    var id = document.getElementById("kt_select2_1").value;
    localStorage.SingleRoleAssignUserId = $("#kt_select2_1").val();

    var modelContent = $("#staffRoleTable")
    $("#staffRoleTable").empty();
    if (id > 0) {
        $.ajax({
            method: "GET",
            url: "/RoleAssign/GetRoleListByUserId/" + id,
            dataType: "html",
            cache: false,
        }).done(function (content) {

            if (content != null) {
                modelContent.html(content);
                localStorage.SingleRoleAssignUserId = id;
            }
        }).fail(function (error) {
            alert(error);
        });
    }
}


//Modal Add Staff 

// Create a FormValidation instance
var fvAddStaff = FormValidation.formValidation(document.getElementById('addStaffFormModal29'),
    {
        fields: {
            NameSurname: {
                validators: {
                    notEmpty: {
                        message: 'Ad soyad alanı boş geçilemez'
                    }
                }
            },
            EmailPersonal: {
                validators: {
                    emailAddress: {
                        message: 'Geçerli bir E-posta adresi girin'
                    },
                    notEmpty: {
                        message: 'E-posta alanı boş geçilemez'
                    }
                    
                }
            },
            EmailJob: {
                validators: {

                    emailAddress: {
                        message: 'Geçerli bir E-posta adresi girin'
                    }
                }
            },
            PhonePersonal: {
                validators: {
                    notEmpty: {
                        message: 'Telefon alanı boş geçilemez'
                    },
                    phone: {
                        country: 'US',
                        message: 'Geçerli bir numara girin'
                    }
                }
            },
            PhoneJob: {
                validators: {
                    phone: {
                        country: 'US',
                        message: 'Geçerli bir numara girin'
                    }
                }
            },
            JobJoinDate: {
                validators: {
                    notEmpty: {
                        message: 'Tarih alanı boş geçilemez'
                    },

                }
            },
            ContractTypeId: {
                validators: {
                    notEmpty: {
                        message: 'Sözleşme türü seçiniz'
                    },

                }
            }


        },
        plugins: {
            trigger: new FormValidation.plugins.Trigger(),
            // Bootstrap Framework Integration
            bootstrap: new FormValidation.plugins.Bootstrap(),

        }
    });

function staffAddClickFunction() {
    debugger;
    fvAddStaff.validate().then(function (status) {
        // Update the login button content based on the validation status
        if (status == 'Valid') {
            console.log($("#ContractTypeSelect").val());
            var formData = {
                NameSurname: $("#NameSurname").val(),
                EmailJob: $("#EmailJob").val(),
                PhoneJob: $("#PhoneJob").val(),
                EmailPersonal: $("#EmailPersonal").val(),
                PhonePersonal: $("#PhonePersonal").val(),
                ContractTypeGeneralSubTypeId: $("#ContractTypeSelect").val(),
                JobFinishDate: $("#JobFinishDate").val(),
                JobJoinDate: $("#JobJoinDate").val(),
                UserName: $("#UserName").val(),
                WillUseSystem: $("#WillUseSystem")[0].checked,
                RoleId: $("#RoleId").val(),
                Password: $("#Password").val(),
                PageRoles: $("#pageRoles").val(),

            };
            $.ajax({
                method: 'post',
                url: '/Staff/AddStaffWithAjax',
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

/*sözleşme türü süreli ise tarih seçme alanı disabled olmamalı, süresizse disable olmalı */
$('#ContractTypeSelect').on('change', function () {
    var id = document.getElementById("ContractTypeSelect").value;
    if (id == 1) {
        $("#JobFinishDate").attr("disabled", false);
        $("#JobFinishDate").removeClass("text-muted");
        $("#jobFinishDateLbl").removeClass("text-muted");
    }
    else {
        $("#JobFinishDate").attr("disabled", true);
        $("#JobFinishDate").addClass("text-muted");
        $("#jobFinishDateLbl").addClass("text-muted");
    }
});

$('#WillUseSystem').on('change', function () {

    if ($('#WillUseSystem').is(":checked")) {
        $("#dvPassword").attr("hidden", false);
        $("#dvUserName").attr("hidden", false);
        $("#dvRoles").attr("hidden", false);
        $("#dvAccessType").attr("hidden", false);
    }
    else {
        $("#dvPassword").attr("hidden", true);
        $("#dvUserName").attr("hidden", true);
        $("#dvRoles").attr("hidden", true);
        $("#dvAccessType").attr("hidden", true);
    }
})
$('#pageRoles').select2({
    closeOnSelect: false,
    width: '100%'

});
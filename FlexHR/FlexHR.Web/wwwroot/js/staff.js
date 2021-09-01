
//Modal Add Staff 

// Create a FormValidation instance
var fvAddStaffWithoutUser = FormValidation.formValidation(document.getElementById('addStaffFormModal29'),
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
            ContractTypeGeneralSubTypeId: {
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

var fvAddStaffWithUser;

function ValidateStaff(status) {

    if (status == "true" && fvAddStaffWithoutUser != null) {
        fvAddStaffWithoutUser.destroy();
        fvAddStaffWithUser = FormValidation.formValidation(document.getElementById('addStaffFormModal29'),
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
                    ContractTypeGeneralSubTypeId: {
                        validators: {
                            notEmpty: {
                                message: 'Sözleşme türü seçiniz'
                            },

                        }
                    },
                    UserNameAddStaff: {
                        validators: {
                            notEmpty: {
                                message: 'Kullanıcı adı boş geçilemez'
                            },

                        }
                    },
                    PasswordAddStaff: {
                        validators: {
                            notEmpty: {
                                message: 'Şifre boş geçilemez'
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
    }
    else {
        fvAddStaffWithUser.destroy();
        fvAddStaffWithoutUser = FormValidation.formValidation(document.getElementById('addStaffFormModal29'),
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
                    ContractTypeGeneralSubTypeId: {
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
    }

}


function staffAddClickFunction() {

    if ($("#WillUseSystem")[0].checked == true) {
        fvAddStaffWithUser.validate().then(function (status) {
            if (status == 'Valid') {

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
                        } else if (data == "not_valid") {
                            Swal.fire({
                                title: "Başarısız!",
                                text: "Kullanıcı adı en az üç karakterden oluşmalı.",
                                icon: "error",
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
    else {
        fvAddStaffWithoutUser.validate().then(function (status) {
            if (status == 'Valid') {

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
}
/* kt_apps_contacts_view_tab_2*/

/*sözleşme türü süreli ise tarih seçme alanı disabled olmamalı, süresizse disable olmalı */
$('#ContractTypeSelect').on('change', function () {
    var id = document.getElementById("ContractTypeSelect").value;
    if (id == 1) {
        $("#JobFinishDate").attr("disabled", false);
        $("#JobFinishDate").removeClass("text-muted");
        $("#jobFinishDateLbl").removeClass("text-muted");
        $("#JobFinishDate").val(moment().format('DD.MM.YYYY'));

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

        ValidateStaff("true");


    }
    else {
        $("#dvPassword").attr("hidden", true);
        $("#dvUserName").attr("hidden", true);
        $("#dvRoles").attr("hidden", true);
        $("#dvAccessType").attr("hidden", true);
        ValidateStaff("false");
    }
});
$('#pageRoles').select2({
    closeOnSelect: false,
    width: '100%'

});
$('#ContractTypeSelect').select2({
    placeholder: 'Seçiniz',
    width: '100%'
});

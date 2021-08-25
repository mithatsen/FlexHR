
//Validation
var fv = FormValidation.formValidation(
    document.getElementById('modalAddCareerForm'),
    {
        fields: {
            JobStartDate: {
                validators: {
                    notEmpty: {
                        message: 'Tarih alanı boş geçilemez'
                    },
                    date: {
                        format: 'DD.MM.YYYY',
                        message: 'Geçerli bir tarih girin'
                    }
                }
            },
            JobFinishDate: {
                validators: {

                    date: {
                        format: 'DD.MM.YYYY',
                        message: 'Geçerli bir tarih girin'
                    }
                }
            },
            DepartmantGeneralSubTypeId: {
                validators: {
                    notEmpty: {
                        message: 'Departman seçiniz'
                    },

                }
            },
            CompanyId: {
                validators: {
                    notEmpty: {
                        message: 'Şirket seçiniz'
                    }
                }
            },
            ModeOfOperationGeneralSubTypeId: {
                validators: {
                    notEmpty: {
                        message: 'Çalışma şekli seçiniz'
                    },

                }
            },
            TitleGeneralSubTypeId: {
                validators: {
                    notEmpty: {
                        message: 'Unvan seçiniz'
                    }
                }
            },
        },
        plugins: {
            trigger: new FormValidation.plugins.Trigger(),
            //// Bootstrap Framework Integration
            bootstrap: new FormValidation.plugins.Bootstrap(),
            //// Validate fields when clicking the Submit button

        },
    }
);
function careerClickFunction() {
    fv.validate().then(function (status) {
        if (status == 'Valid') {
            var formData = {
                CompanyId: $("#c_companySelect").val(),
                DepartmantGeneralSubTypeId: $("#DepartmantGeneralSubTypeId").val(),
                ModeOfOperationGeneralSubTypeId: $("#ModeOfOperationGeneralSubTypeId").val(),
                CompanyBranchId: $("#c_companyBranchSelect").val(),
                JobStartDate: $("#JobStartDate").val(),
                TitleGeneralSubTypeId: $("#TitleGeneralSubTypeId").val(),
                JobFinishDate: $("#JobFinishDate").val(),
                StaffId: $("#StaffId").val(),
            };

            $.ajax({
                method: 'post',
                url: '/StaffCareer/AddStaffCareerWithAjax',
                data: formData,
                success: function (data) {
                    if (data == "true") {
                        Swal.fire({
                            title: "Eklendi!",
                            text: "Kaydınız eklendi.",
                            icon: "success",
                            showCancelButton: false,

                        }).then(function () {
                            window.location.reload();
                        })
                    }

                }
            })
        }

    });
}

function CareerUpdate(id) {
    debugger;
    var modelContent = $("#staffCareerUpdateDiv")
    $("#staffCareerUpdateDiv").empty();

    $.ajax({
        method: "GET",
        url: "/StaffCareer/GetUpdateStaffCareerModal/" + id,
        dataType: "html",
        cache: false,
    }).done(function (content) {

        if (content != null) {
            modelContent.html(content);
        }
        $("#staffCareerUpdateModal").modal("show");



    }).fail(function (error) {
        alert(error);
    });
}


// Şirket değişikliği, şube  drop downına yansır 
// kariyer ekleme sayfasındaki şirket şube listeleme
function CompanyChange() {
    var id = document.getElementById("c_companySelect").value;
    console.log(id);
    if (id != "" || id > 0) {

        $.ajax({
            method: "GET",
            url: "/StaffCareer/BranchList/" + id,
        }).done(function (content) {
            $("#c_companyBranchSelect").attr("disabled", false);
            $("#c_companyBranchSelect").removeClass("text-muted");
            $("#c_companyBranchSelect").empty();
            $("#c_companyBranchSelect").append('<option  value="-1">Şube Seçiniz</option>');

            let companyBranch = JSON.parse(content);
            for (var i = 0; i < companyBranch.length; i++) {
                var opt = '<option value="' + companyBranch[i].CompanyBranchId + '">' + companyBranch[i].BranchName + '</option>';
                $("#c_companyBranchSelect").append(opt);
            }
        }).fail(function (error) {

        });
    } else {
        $("#c_companyBranchSelect").empty();
        $("#c_companyBranchSelect").append('<option value="-1">Şube Seçiniz</option>');
        $("#c_companyBranchSelect").attr("disabled", true);
        $("#c_companyBranchSelect").addClass("text-muted");
    }
}
// kariyer güncelleme sayfasındaki şirket şube listeleme
function CompanyChangeUpdate() {

    var id = document.getElementById("c_companySelectUpdate").value;
    if (id != "" || id > 0) {
        $.ajax({
            method: "GET",
            url: "/StaffCareer/BranchList/" + id,
        }).done(function (content) {
            $("#c_companyBranchSelectUpdate").attr("disabled", false);
            $("#c_companyBranchSelectUpdate").removeClass("text-muted");
            $("#c_companyBranchSelectUpdate").empty();
            $("#c_companyBranchSelectUpdate").append('<option  value="-1">Şube Seçiniz</option>');

            let companyBranch = JSON.parse(content);
            for (var i = 0; i < companyBranch.length; i++) {
                var opt = '<option value="' + companyBranch[i].CompanyBranchId + '">' + companyBranch[i].BranchName + '</option>';
                $("#c_companyBranchSelectUpdate").append(opt);
            }
        }).fail(function (error) {

        });
    } else {
        $("#c_companyBranchSelectUpdate").empty();
        $("#c_companyBranchSelectUpdate").append('<option value="-1">Şube Seçiniz</option>');
        $("#c_companyBranchSelectUpdate").attr("disabled", true);
        $("#c_companyBranchSelectUpdate").addClass("text-muted");
    }
}
//Delete Career with sweet alert
function DeleteCareer(id) {
    Swal.fire({
        title: "Silmek istediğinize emin misiniz ?",
        text: "Bunu geri alamazsınız!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Evet!",
        cancelButtonText: "Hayır!",
        reverseButtons: true
    }).then(function (result) {
        if (result.value) {

            $.ajax({
                method: "POST",
                url: "/StaffCareer/DeleteCareer/" + id,
            }).done(function (result) {
                if (result == true) {
                    Swal.fire({
                        title: "Silindi!",
                        text: "Kaydınız silindi.",
                        icon: "success",
                        showCancelButton: false
                    }).then(function () {
                        window.location.reload();
                    })

                } else {
                    Swal.fire(
                        "Hata",
                        "Kaydınız silinemedi",
                        "hata"
                    )
                }

            })
        } else if (result.dismiss === "cancel") {
            Swal.fire({
                title: "İptal Edildi",
                text: "Kaydınız silinmedi",
                icon: "error",
                showCancelButton: false
            })


        }
    });


}
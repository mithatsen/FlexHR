function CompanyBranchUpdate(id) {
    var modelContent = $("#roleUpdateDiv")
    $("#roleUpdateDiv").empty();

    $.ajax({
        method: "GET",
        url: "/SystemVariable/GetUpdateCompanyBranchModal/" + id,
        dataType: "html",
        cache: false,
    }).done(function (content) {

        if (content != null) {
            modelContent.html(content);
        }
        $("#CompanyBranchUpdateModal").modal("show");



    }).fail(function (error) {
        alert(error);
    });
}

var fv = FormValidation.formValidation(
    document.getElementById('CompanyBranchAddForm'),
    {
        fields: {
            BranchName: {
                validators: {
                    notEmpty: {
                        message: 'Şube adı alanı boş geçilemez'
                    }
                }
            },
            CompanyId: {
                validators: {
                    notEmpty: {
                        message: 'Şirket seçiniz'
                    }
                }
            },
        },
        plugins: {
            trigger: new FormValidation.plugins.Trigger(),
            // Bootstrap Framework Integration
            bootstrap: new FormValidation.plugins.Bootstrap(),
            // Validate fields when clicking the Submit button

        }
    }
);

function CompanyBranchAddClickFunction() {
    fv.validate().then(function (status) {
        if (status == 'Valid') {
            var formData = {
                BranchName: $("#BranchName").val(),
                CompanyId: $("#CompanyId").val()
            };
            $.ajax({
                method: 'post',
                url: '/SystemVariable/AddCompanyBranch',
                data: formData,
                success: function (data) {
                    if (data == true) {
                        Swal.fire({
                            title: "Eklendi!",
                            text: "Kaydınız eklendi.",
                            icon: "success",
                        }).then(function () {
                            window.location.reload()
                        })
                    }

                }

            })
        }
    });
}


// Delete generalSubRule with sweet alert *@

function DeleteCompanyBranch(id) {
    Swal.fire({
        title: "Silmek istediğinize emin misiniz ?",
        text: "Bunu geri alamazsınız!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Evet",
        cancelButtonText: "Hayır!",
        reverseButtons: true
    }).then(function (result) {
        if (result.value) {

            $.ajax({
                method: "POST",
                url: "/SystemVariable/DeleteCompanyBranch/" + id,
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

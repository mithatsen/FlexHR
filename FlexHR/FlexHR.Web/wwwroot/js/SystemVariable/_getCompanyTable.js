
function CompanyUpdate(id) {
    var modelContent = $("#companyUpdateDiv")
    $("#companyUpdateDiv").empty();

    $.ajax({
        method: "GET",
        url: "/SystemVariable/GetUpdateCompanyModal/" + id,
        dataType: "html",
        cache: false,
    }).done(function (content) {

        if (content != null) {
            modelContent.html(content);
        }
        $("#companyUpdateModal").modal("show");



    }).fail(function (error) {
        alert(error);
    });
}




var fv = FormValidation.formValidation(
    document.getElementById('companyAddForm'),
    {
        fields: {
            CompanyName: {
                validators: {
                    notEmpty: {
                        message: 'Şirket adı alanı boş geçilemez'
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
function companyAddClickFunction() {
    fv.validate().then(function (status) {
        if (status == 'Valid') {
            var formData = {
                CompanyName: $("#CompanyName").val()
            };
            $.ajax({
                method: 'post',
                url: '/SystemVariable/AddCompany',
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


// Delete company with sweet alert 

function DeleteCompany(id) {
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
                url: "/SystemVariable/DeleteCompany/" + id,
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


function RoleUpdate(id) {
    var modelContent = $("#roleUpdateDiv")
    $("#roleUpdateDiv").empty();

    $.ajax({
        method: "GET",
        url: "/SystemVariable/GetUpdateRoleModal/" + id,
        dataType: "html",
        cache: false,
    }).done(function (content) {

        if (content != null) {
            modelContent.html(content);
        }
        $("#roleUpdateModal").modal("show");



    }).fail(function (error) {
        alert(error);
    });
}


var fv = FormValidation.formValidation(
    document.getElementById('RoleAddForm'),
    {
        fields: {
            Name: {
                validators: {
                    notEmpty: {
                        message: 'İzin türü adı alanı boş geçilemez'
                    }
                }
            },
            Description: {
                validators: {
                    notEmpty: {
                        message: 'Açıklama alanı boş geçilemez'
                    },
                    stringLength: {
                        max: 999,
                        message: '1000 karakterden az girin'
                    }
                }
            },
            AuthorizeTypeGeneralSubTypeId: {
                validators: {
                    notEmpty: {
                        message: 'Yetki Tipi boş geçilemez '
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

function roleAddClickFunction() {
    fv.validate().then(function (status) {
        if (status == 'Valid') {
            var formData = {
                Name: $("#Name").val(),
                Description: $("#Description").val(),
                AuthorizeTypeGeneralSubTypeId: $("#authorizeType").val()
            };
            $.ajax({
                method: 'post',
                url: '/SystemVariable/AddRole',
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
                    } else {
                        Swal.fire({
                            title: "Başarısız!",
                            text: "Kaydınız eklenmedi.",
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


// Delete generalSubRule with sweet alert 

function DeleteRole(id) {
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
                url: "/SystemVariable/DeleteRole/" + id,
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

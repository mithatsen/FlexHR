
function ColorCodeUpdate(id) {
    var modelContent = $("#ColorCodeUpdateDiv")
    $("#ColorCodeUpdateDiv").empty();
    $.ajax({
        method: "GET",
        url: "/SystemVariable/GetUpdateColorCodeModal/" + id,
        dataType: "html",
        cache: false,
    }).done(function (content) {

        if (content != null) {
            modelContent.html(content);
        }
        $("#ColorCodeUpdateModal").modal("show");



    }).fail(function (error) {
        alert(error);
    });
}




var fv = FormValidation.formValidation(
    document.getElementById('colorCodeAddForm'),
    {
        fields: {
            Description: {
                validators: {
                    notEmpty: {
                        message: 'Açıklama alanı boş geçilemez'
                    }
                }
            },
            Color: {
                validators: {
                    notEmpty: {
                        message: 'Renk kodu alanı boş geçilemez'
                    }
                }
            },
            Code: {
                validators: {
                    notEmpty: {
                        message: 'Tanımlama kodu alanı boş geçilemez'
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
function colorCodeAddClickFunction() {
    fv.validate().then(function (status) {
        if (status == 'Valid') {
            var formData = {
                Color: $("#Color").val(),
                Code: $("#Code").val(),
                Description: $("#Description").val(),
                Name: $("#Name").val()
            };
            debugger;
            $.ajax({
                method: 'post',
                url: '/SystemVariable/AddColorCode',
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

function DeleteColorCode(id) {
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
                url: "/SystemVariable/DeleteColorCode/" + id,
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
                        "error"
                    )
                }

            })
        }
    });
}


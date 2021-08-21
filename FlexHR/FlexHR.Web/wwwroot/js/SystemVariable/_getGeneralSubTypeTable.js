
function GeneralSubTypeUpdate(id) {
    console.log(id);
    var modelContent = $("#generalSubTypeUpdateDiv")
    $("#generalSubTypeUpdateDiv").empty();

    $.ajax({
        method: "GET",
        url: "/SystemVariable/GetUpdateGeneralSubTypeModal/" + id,
        dataType: "html",
        cache: false,
    }).done(function (content) {

        if (content != null) {
            modelContent.html(content);
        }
        $("#generalSubTypeUpdateModal").modal("show");



    }).fail(function (error) {
        alert(error);
    });
}



var fv = FormValidation.formValidation(
    document.getElementById('generalSubTypeAddForm'),
    {
        fields: {
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
            }
        },
        plugins: {
            trigger: new FormValidation.plugins.Trigger(),
            // Bootstrap Framework Integration
            bootstrap: new FormValidation.plugins.Bootstrap(),
            // Validate fields when clicking the Submit button

        }
    }
);
function generalSubTypeAddClickFunction() {
    fv.validate().then(function (status) {
        if (status == 'Valid') {
            var formData = {
                Description: $("#Description").val(),
                GeneralTypeId: Number(localStorage.SystemVariableId)
            };
            $.ajax({
                method: 'post',
                url: '/SystemVariable/AddGeneralSubType',
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


//Delete generalSubType with sweet alert 

function DeleteGeneralSubType(id) {
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
                url: "/SystemVariable/DeleteGeneralSubtype/" + id,
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
var textAs = $('#kt_select2_1 option:selected').text();
$('#VariableTitle').text(textAs);



function LeaveTypeUpdate(id) {
    var modelContent = $("#leaveTypeUpdateDiv")
    $("#leaveTypeUpdateDiv").empty();

    $.ajax({
        method: "GET",
        url: "/SystemVariable/GetUpdateLeaveTypeModal/" + id,
        dataType: "html",
        cache: false,
    }).done(function (content) {

        if (content != null) {
            modelContent.html(content);
        }
        $("#leaveTypeUpdateModal").modal("show");



    }).fail(function (error) {
        alert(error);
    });
}


$('#kt_touchspin_2, #kt_touchspin_2_2').TouchSpin({
    buttondown_class: 'btn btn-secondary',
    buttonup_class: 'btn btn-secondary',
    min: 0,
    max: 100,
    step: 1,
    int: 1,
    boostat: 5,
    maxboostedstep: 10,

});





var fv = FormValidation.formValidation(
    document.getElementById('LeaveTypeAddForm'),
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
            TotalDay: {
                validators: {
                    notEmpty: {
                        message: 'İzin hakkı alanı boş geçilemez',
                    },
                    between: {
                        min: 1,
                        max: 365,
                        message: 'Toplam gün 0 olamaz',
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
function leaveTypeAddClickFunction() {
    fv.validate().then(function (status) {
        if (status == 'Valid') {
            var formData = {
                Name: $("#Name").val(),
                Description: $("#Description").val(),
                IsFree: $("#IsFree").val(),
                TotalDay: $("#kt_touchspin_2").val()
            };
            $.ajax({
                method: 'post',
                url: '/SystemVariable/AddLeaveType',
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

function DeleteLeaveType(id) {
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
                url: "/SystemVariable/DeleteLeaveType/" + id,
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

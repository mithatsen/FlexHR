
var fv = FormValidation.formValidation(
    document.getElementById('LeaveRuleAddForm'),
    {
        fields: {
            SeniorityYear: {
                validators: {
                    notEmpty: {
                        message: 'Deneyim alanı boş geçilemez',
                    },
                    between: {
                        min: 1,
                        max: 365,
                        message: 'Toplam yıl 0 olamaz',
                    }

                }
            },
            AditionalLeaveAmount: {
                validators: {
                    notEmpty: {
                        message: 'Ek izin hakkı alanı boş geçilemez',
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
function leaveRuleAddClickFunction() {
    fv.validate().then(function (status) {
        if (status == 'Valid') {
            var formData = {
                SeniorityYear: $("#kt_touchspin_3").val(),
                AditionalLeaveAmount: $("#kt_touchspin_2").val()
            };
            $.ajax({
                method: 'post',
                url: '/SystemVariable/AddLeaveRule',
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
$('#kt_touchspin_3, #kt_touchspin_2_2').TouchSpin({
    buttondown_class: 'btn btn-secondary',
    buttonup_class: 'btn btn-secondary',
    min: 0,
    max: 100,
    step: 1,
    int: 1,
    boostat: 5,
    maxboostedstep: 10,

});

function LeaveRuleUpdate(id) {
    var modelContent = $("#leaveRuleUpdateDiv")
    $("#leaveRuleUpdateDiv").empty();

    $.ajax({
        method: "GET",
        url: "/SystemVariable/GetUpdateLeaveRuleModal/" + id,
        dataType: "html",
        cache: false,
    }).done(function (content) {

        if (content != null) {
            modelContent.html(content);
        }
        $("#leaveRuleUpdateModal").modal("show");



    }).fail(function (error) {
        alert(error);
    });
}

//Delete generalSubRule with sweet alert 

function DeleteLeaveRule(id) {
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
                url: "/SystemVariable/DeleteLeaveRule/" + id,
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

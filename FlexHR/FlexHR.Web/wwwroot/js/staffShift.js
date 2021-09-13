//add staff shift
//  document.addEventListener('DOMContentLoaded', function (e) {

var fv = FormValidation.formValidation(document.getElementById('modalAddShift'), {
    fields: {
        StartDate: {
            validators: {
                notEmpty: {
                    message: 'Tarih alanı boş geçilemez'
                },
                date: {
                    format: 'DD.MM.YYYY HH:mm',
                    message: 'Geçerli bir tarih girin'
                }

            }
        },
        ShiftHour: {
            validators: {
                notEmpty: {
                    message: 'Saat alanı boş geçilemez',
                },
                greaterThan: {
                    min: 0,
                    message: 'Saat alanı sıfırdan küçük olamaz',
                }

            }
        },
        ShiftMinute: {
            validators: {
                notEmpty: {
                    message: 'Süre alanı boş geçilemez',
                },
                between: {
                    min: 0,
                    max: 59,
                    message: 'Dakika alanı 0 - 60 arasında olmalı'
                }

            }
        },
        Description: {
            validators: {
                notEmpty: {
                    message: 'Açıklama alanı boş geçilemez'
                },
                stringLength: {
                    max: 499,
                    message: '500 karakterden az girin'
                }
            }
        },
    },
    plugins: {
        trigger: new FormValidation.plugins.Trigger(),
        // Bootstrap Framework Integration
        bootstrap: new FormValidation.plugins.Bootstrap(),
        // Validate fields when clicking the Submit button
        // submitButton: new FormValidation.plugins.SubmitButton(),
        // Submit the form when all fields are valid
        // defaultSubmit: new FormValidation.plugins.DefaultSubmit(),
    },
});
function shiftClickFunction() {

    fv.validate().then(function (status) {
        // Update the login button content based on the validation status
        if (status == 'Valid') {

            var formData = {
                StaffId: $("#StaffId").val(),
                StartDate: $("#StartDate").val(),
                ShiftHour: $("#kt_touchspin_1").val(),
                ShiftMinute: $("#kt_touchspin_123").val(),
                Description: $("#Description").val(),
                IsCheckedApprove: $("#IsCheckedApprove").is(":checked")
            };
            console.log(formData);
            $.ajax({
                method: 'post',
                url: '/StaffShift/AddStaffShiftWithAjax',
                data: formData,
                success: function (data) {
                    if (data == 'true') {
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

//update staff shift
    function ShiftUpdate(id) {

        var modelContent = $("#UpdateStaffShiftModalDiv")
        $("#UpdateStaffShiftModalDiv").empty();

        $.ajax({
            method: "GET",
            url: "/StaffShift/GetUpdateStaffShiftModal/" + id,
            dataType: "html",
            cache: false,
        }).done(function (content) {
            if (content != null) {
                modelContent.html(content);
            }
            $("#staffShiftUpdateModal").modal("show");

            //CompanyChangeUpdate();

        }).fail(function (error) {

        });
    }
// Delete Staff Shift 
function DeleteStaffShift(id) {
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
                url: "/StaffShift/DeleteStaffShift/" + id,
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
$('#kt_touchspin_123, #kt_touchspin_2_1').TouchSpin({
    buttondown_class: 'btn btn-secondary',
    buttonup_class: 'btn btn-secondary',

    min: 0,
    max: 60,
    step: 1,
    int: 1,
    boostat: 5,
    maxboostedstep: 10,
});
$('#kt_datetimepicker_18').datetimepicker({
    locale: 'tr',
    format: 'DD.MM.YYYY HH:mm'
});
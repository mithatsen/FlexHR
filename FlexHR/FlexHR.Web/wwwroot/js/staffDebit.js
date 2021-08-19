// Modal Add Career

var fv = FormValidation.formValidation(document.getElementById('modalAddDebitForm'), {
    fields: {
        IssueDate: {
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
        ReturnDate: {
            validators: {

                date: {
                    format: 'DD.MM.YYYY',
                    message: 'Geçerli bir tarih girin'
                }

            }
        },
        DebitGeneralSubTypeId: {
            validators: {
                notEmpty: {
                    message: 'Kategori alanı boş geçilemez'
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
        SerialNumber: {
            validators: {
                stringLength: {
                    max: 499,
                    message: 'Seri numarası 50 karakterden az olmalı'
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
    }
});
function debitClickFunction() {
    debugger;
    fv.validate().then(function (status) {
        // Update the login button content based on the validation status
        if (status == 'Valid') {
            debugger;
            var formData = {
                DebitGeneralSubTypeId: $("#DebitGeneralSubTypeId").val(),
                IssueDate: $("#IssueDate").val(),
                SerialNumber: $("#SerialNumber").val(),
                ReturnDate: $("#ReturnDate").val(),
                Description: $("#Description").val(),
                StaffId: $("#StaffId").val(),
            };

            $.ajax({
                method: 'post',
                url: '/StaffDebit/AddStaffDebitWithAjax',
                data: formData,
                success: function (data) {
                    debugger;

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
//update staff debit

function DebitUpdate(id) {
    console.log(id);
    var modelContent = $("#staffDebitUpdateDiv")
    $("#staffDebitUpdateDiv").empty();

    $.ajax({
        method: "GET",
        url: "/StaffDebit/GetUpdateStaffDebitModal/" + id,
        dataType: "html",
        cache: false,
    }).done(function (content) {

        if (content != null) {
            modelContent.html(content);
        }
        $("#staffDebitUpdateModal").modal("show");



    }).fail(function (error) {
        alert(error);
    });
}


//Delete Debit with sweet alert 

function DeleteStaffDebit(id) {
    Swal.fire({
        title: "Silmek istediğinize emin misiniz ?",
        text: "Bunu geri alamazsınız!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Evet, sil!",
        cancelButtonText: "Hayır, iptal et!",
        reverseButtons: true
    }).then(function (result) {
        if (result.value) {

            $.ajax({
                method: "POST",
                url: "/StaffDebit/DeleteDebit/" + id,
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

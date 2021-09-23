
//Validation
var fv = FormValidation.formValidation(
    document.getElementById('modalAddJobRotationForm'),
    {
        fields: {
            StartDate: {
                validators: {
                    notEmpty: {
                        message: 'Saat alanı boş geçilemez'
                    },
                   
                }
            },
            EndDate: {
                validators: {
                    notEmpty: {
                        message: 'Saat alanı boş geçilemez'
                    },
                    
                }
            },
            Name: {
                validators: {
                    notEmpty: {
                        message: 'Vardiya adı alanı boş geçilemez'
                    },
                }
            },
            ShiftTime: {
                validators: {
                    notEmpty: {
                        message: 'Vardiya süresi alanı boş geçilemez',
                    },
                    greaterThan: {
                        min: 1,
                        message: 'Vardiya süresi alanı birden küçük olamaz',
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
function jobRotationClickFunction() {
    fv.validate().then(function (status) {
        if (status == 'Valid') {
            var formData = {
                Name: $("#RotationName").val(),
                StartDate: $("#StartDate").val(),
                ShiftTime: $("#ShiftTime").val(),
                EndDate: $("#EndDate").val(),
                StaffId: $("#StaffId").val(),
            };
            $.ajax({
                method: 'post',
                url: '/JobRotation/AddJobRotationWithAjax',
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

function JobRotationUpdate(id) {
    debugger;
    var modelContent = $("#jobRotationUpdateDiv")
    $("#jobRotationUpdateDiv").empty();

    $.ajax({
        method: "GET",
        url: "/JobRotation/GetUpdateJobRotationModal/" + id,
        dataType: "html",
        cache: false,
    }).done(function (content) {

        if (content != null) {
            modelContent.html(content);
        }
        $("#jobRotationUpdateModal").modal("show");



    }).fail(function (error) {
        alert(error);
    });
}

//Delete Career with sweet alert
function DeleteJobRotation(id) {
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
                url: "/JobRotation/DeleteJobRotation/" + id,
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
        } 
    });


}




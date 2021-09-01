function approvePayment(id) {
    Swal.fire({
        title: "Onaylamak ister misiniz ?",
        text: "Bunu geri alamazsınız!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Evet",
        cancelButtonText: "Hayır",
        reverseButtons: true
    }).then(function (result) {
        if (result.value) {
            $.ajax({
                method: "POST",
                url: "/Payment/ApprovePayment/" + id,
            }).done(function (result) {
                debugger;
                if (result == true) {
                    Swal.fire({
                        title: "Onaylandı!",
                        text: "Talep Onaylandı.",
                        icon: "success",
                        showCancelButton: false
                    }).then(function () {
                        window.location.reload();
                    })

                } else {
                    Swal.fire(
                        "Hata",
                        "Kaydınız onaylanmadı",
                        "hata"
                    )
                }

            })
        } else if (result.dismiss === "cancel") {
            Swal.fire({
                title: "İptal Edildi",
                text: "Kaydınız onaylanmadı",
                icon: "error",
                showCancelButton: false
            })
        }
    });

}
function rejectPayment(id) {
    Swal.fire({
        title: "Talebi Reddetmek İster Misiniz ?",
        text: "Bunu geri alamazsınız!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Evet",
        cancelButtonText: "Hayır",
        reverseButtons: true
    }).then(function (result) {
        if (result.value) {
            $.ajax({
                method: "POST",
                url: "/Payment/RejectPayment/" + id,
            }).done(function (result) {
                debugger;
                if (result == true) {
                    Swal.fire({
                        title: "Başarılı!",
                        text: "Talep Reddedildi.",
                        icon: "success",
                        showCancelButton: false
                    }).then(function () {
                        window.location.reload();
                    })

                } else {
                    Swal.fire(
                        "Hata",
                        "İşlem Başarısız",
                        "hata"
                    )
                }

            })
        } else if (result.dismiss === "cancel") {
            Swal.fire({
                title: "İptal Edildi",
                text: "Kaydınız onaylanmadı",
                icon: "error",
                showCancelButton: false
            })
        }
    });

}

function isPaidPayment(id) {
    Swal.fire({
        title: "Onaylamak ister misiniz ?",
        text: "Bunu geri alamazsınız!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Evet",
        cancelButtonText: "Hayır",
        reverseButtons: true
    }).then(function (result) {
        if (result.value) {
            $.ajax({
                method: "POST",
                url: "/Payment/IsPaidPayment/" + id,
            }).done(function (result) {
                debugger;
                if (result == true) {
                    Swal.fire({
                        title: "Onaylandı!",
                        text: "Talep Onaylandı.",
                        icon: "success",
                        showCancelButton: false
                    }).then(function () {
                        window.location.reload();
                    })

                } else {
                    Swal.fire(
                        "Hata",
                        "Kaydınız onaylanmadı",
                        "hata"
                    )
                }

            })
        } else if (result.dismiss === "cancel") {
            Swal.fire({
                title: "İptal Edildi",
                text: "Kaydınız onaylanmadı",
                icon: "error",
                showCancelButton: false
            })
        }
    });

}

function ShowReceiptInModall(id) {
   
    var modelContent = $("#ShowStaffPaymentInfoDiv");

    $.ajax({
        url: "/StaffPayment/GetStaffPaymentWithReceiptInfoModal/" + id,
        type: "GET"
    }).done(function (content) {

        if (content != null) {
            modelContent.html(content);
        }
        $("#ShowPaymentReceiptModal").modal("show");

    }).fail(function (error) {
        alert(error);
    });



}

function approveStaffPaymentDash(id) {

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

function rejectStaffPaymentDash(id) {
    Swal.fire({
        title: "Reddetmek ister misiniz ?",
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
                        title: "Reddedildi!",
                        text: "Talep Reddedildi.",
                        icon: "success",
                        showCancelButton: false
                    }).then(function () {
                        window.location.reload();
                    })

                } else {
                    Swal.fire(
                        "Hata",
                        "İşlem başarısız",
                        "hata"
                    )
                }

            })
        } else if (result.dismiss === "cancel") {
            Swal.fire({
                title: "İptal Edildi",
                text: "İşlem başarısız",
                icon: "error",
                showCancelButton: false
            })
        }
    });

}
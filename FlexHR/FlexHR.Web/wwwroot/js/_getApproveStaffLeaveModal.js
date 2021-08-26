function approveLeaveDash(id) {
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
                url: "/Leave/ApproveLeave/" + id,
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
function rejectLeaveDash(id) {
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
                url: "/Leave/RejectLeave/" + id,
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

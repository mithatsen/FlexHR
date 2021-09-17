$(document).ready(function () {
    if (localStorage.PaymentTabId == "tab_3") {
        $("#tab_3").addClass("active");
        $("#tab3_btn").addClass("active");
        $("#tab_2").removeClass("active");
        $("#tab2_btn").removeClass("active");
    }
    else {
        $("#tab_2").addClass("active");
        $("#tab2_btn").addClass("active");
        $("#tab_3").removeClass("active");
        $("#tab3_btn").removeClass("active");
    } 
});
$("#tab2_btn").click(function () {
    localStorage.PaymentTabId = "0";
});
$("#tab3_btn").click(function () {
    localStorage.PaymentTabId = "tab_3";
});
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
                        "error"
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
                        "error"
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
                        localStorage.SingleRoleAssignUserId = id;
                        localStorage.PaymentTabId = "tab_3";
                        window.location.reload();
                    })

                } else {
                    Swal.fire(
                        "Hata",
                        "Kaydınız onaylanmadı",
                        "error"
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

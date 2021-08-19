function ChangeDate() {
    var dateTime = $("#staffLeaveMonthlyDate").val();
    $.ajax({
        method: 'get',
        url: '/Leave/StaffLeaveMonthlyInfo',
        data: dateTime,
        success: function (data) {
            if (data == "true") {
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
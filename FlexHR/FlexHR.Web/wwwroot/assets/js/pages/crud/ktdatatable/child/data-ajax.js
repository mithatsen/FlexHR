'use strict';
// Class definition

var KTDatatableChildRemoteDataDemo = function () {
    // Private functions

    // demo initializer
    var demo = function () {
        var datatable = $('#kt_datatable').KTDatatable({
            // datasource definition
            data: {
                type: 'remote',
                source: {
                    read: {
                        url: "/TakePayment/GetStaffPayments",
                    },
                },
                pageSize: 10, // display 20 records per page
                serverPaging: true,
                serverFiltering: false,
                serverSorting: true,
            },

            // layout definition
            layout: {
                scroll: false,
                footer: false,
            },

            // column sorting
            sortable: true,

            pagination: true,

            detail: {
                title: 'Load sub table',
                content: subTableInit,
            },

            search: {
                input: $('#kt_datatable_search_query'),
                key: 'generalSearch'
            },

            // columns definition
            columns: [

                {
                    field: 'staffPaymentId',
                    title: '#',
                    sortable: 'asc',
                    width: 40,
                }, {
                    field: 'nameSurname',
                    title: 'Ad Soyad',
                    sortable: 'asc',
                    template: function (row) {
                        return '\
	                                 <a href="/StaffGeneral/Index/'+ row.staffId + '">' + row.nameSurname + ' </a>\
	                            ';
                    },
                }, {
                    field: 'paymentType',
                    title: 'Ödeme Türü',
                    sortable: 'asc',
                }, {
                    field: 'creationDate',
                    title: 'Tarih',
                    template: function (row) {
                        var formattedDate = new Date(row.creationDate);
                        var d = formattedDate.getDate();
                        var m = formattedDate.getMonth();
                        m += 1;  // JavaScript months are 0-11
                        var y = formattedDate.getFullYear();
                        if (d < 10) {
                            d = "0" + d;
                        }
                        if (m < 10) {
                            m = "0" + m;
                        }
                        return d + "." + m + "." + y;

                    }
                }, {
                    field: 'amount',
                    title: 'Toplam Tutar',
                    sortable: 'asc',
                    template: function (row) {

                        return row.amount + " " + row.currencyType;
                    }
                }, {
                    field: 'installment',
                    title: 'Taksit Miktarý',
                    sortable: 'asc',
                }, {
                    field: 'Actions',
                    width: 125,
                    title: 'Ýþlem',
                    class: 'text-center',
                    sortable: false,
                    overflow: 'visible',
                    autoHide: false,
                    template: function (row) {
                        return '\
                                 <div class="text-center">\
	                                 <a onclick="InstallmentAddModal('+ row.staffPaymentId + ')" class="btn btn-sm btn-primary btn-icon mr-2" title="Taksit Ekle">\
	                                     <span class="svg-icon svg-icon-md">\
                                              <i class="fa fa-plus text-white"></i>\
	                                      </span>\
	                                  </a>\
	                             </div>\
	                            ';
                    },
                }],
        });
        var abc = datatable
        console.log(abc)
        $('#kt_datatable_search_status').on('change', function () {
            datatable.search($(this).val().toLowerCase(), 'Status');
        });

        $('#kt_datatable_search_type').on('change', function () {
            datatable.search($(this).val().toLowerCase(), 'Type');
        });

        $('#kt_datatable_search_status, #kt_datatable_search_type').selectpicker();


        function subTableInit(e) {
            $('<div/>').attr('id', 'child_data_ajax_' + e.data.staffPaymentId).appendTo(e.detailCell).KTDatatable({
                data: {
                    type: 'remote',
                    source: {
                        read: {
                            url: "/TakePayment/GetStaffPaymentInfo/" + e.data.staffPaymentId,
                            params: {
                                // custom query params
                                query: {
                                    generalSearch: '',
                                    CustomerID: e.data.RecordID,
                                },
                            },
                        },
                    },
                    pageSize: 5,
                    serverPaging: true,
                    serverFiltering: false,
                    serverSorting: true,
                },

                // layout definition
                layout: {
                    scroll: false,
                    footer: false,

                    // enable/disable datatable spinner.
                    spinner: {
                        type: 1,
                        theme: 'default',
                    },
                },

                sortable: true,

                // columns definition
                columns: [
                    {
                        field: 'id',
                        title: '#',
                        width: 40                        
                    }, {
                        field: 'paymentDate',
                        title: 'Ödeme Tarihi',
                        sortable: 'asc',
                        template: function (row) {
                            var formattedDate = new Date(row.paymentDate);
                            var d = formattedDate.getDate();
                            var m = formattedDate.getMonth();
                            m += 1;  // JavaScript months are 0-11
                            var y = formattedDate.getFullYear();
                            if (d < 10) {
                                d = "0" + d;
                            }
                            if (m < 10) {
                                m = "0" + m;
                            }
                            return d + "." + m + "." + y;

                        },
                    }, {
                        field: 'installmentAmount',
                        title: 'Taksit Tutarý',
                        sortable: true, 
                        template: function (row) {

                            return row.installmentAmount + " " + row.currencyType;
                        }
                    }, {
                        field: 'isPaid',
                        title: 'Durumu',
                        // callback function support for column rendering
                        template: function (row) {
                            if (row.isPaid == true) {
                                return '<span class="label label-light-success label-inline font-weight-bold label-lg">Ödeme Alýndý</span>';
                            } else {
                                return '<span class="label label-light-danger label-inline font-weight-bold label-lg">Ödeme Alýnmadý</span>';
                            }
                        },
                    }, {
                        field: 'Actions',
                        width: 125,
                        title: 'Ýþlem',
                        class: 'text-center',
                        sortable: false,
                        overflow: 'visible',
                        autoHide: false,
                        template: function (row) {
                            if (row.isPaid == true) {
                                return '\
                                     <div class="text-center">\
	                                    <a onclick="getInstallmentUpdateModal('+row.id+')" class="btn btn-sm btn-warning btn-icon mr-2" title="Düzenle">\
	                                        <span class="svg-icon svg-icon-md">\
                                                <i class="fa fa-edit text-white"></i>\
	                                        </span>\
	                                    </a>\
	                                    <a onclick="DeleteInstallment('+ row.id + ')" class="btn btn-sm btn-danger btn-icon" title="Sil">\
	                                        <span class="svg-icon svg-icon-md">\
	                                             <i class="fa fa-trash text-white"></i>\
	                                        </span>\
	                                    </a>\
                                    </div>\
	                            ';
                            } else {
                                return '\
                                     <div class="text-center">\
	                                     <a onclick="ApproveInstallment('+ row.id + ')"  class="btn btn-sm btn-success btn-icon mr-2" title="Onayla">\
	                                        <span class="svg-icon svg-icon-md">\
                                                <i class="fa fa-check text-white"></i>\
	                                        </span>\
	                                    </a>\
                                        <a onclick="getInstallmentUpdateModal('+ row.id +')" class="btn btn-sm btn-warning btn-icon mr-2" title="Düzenle">\
	                                        <span class="svg-icon svg-icon-md">\
                                                <i class="fa fa-edit text-white"></i>\
	                                        </span>\
	                                    </a>\
	                                    <a onclick="DeleteInstallment('+ row.id + ')" class="btn btn-sm btn-danger btn-icon" title="Sil">\
	                                        <span class="svg-icon svg-icon-md">\
	                                             <i class="fa fa-trash text-white"></i>\
	                                        </span>\
	                                    </a>\
                                    </div>\
	                            ';
                            }

                        },
                    }],
            });
            console.log(e)
        }
        var abcd = datatable

    };

    return {
        // Public functions
        init: function () {
            // init dmeo
            demo();
        },
    };
}();

jQuery(document).ready(function () {
    KTDatatableChildRemoteDataDemo.init();
});

$("#InstallmentAmount").mask('000000000000,00', { reverse: true });
$('input[name="PaymentDate"]').mask('00.00.0000');

var fvInstallment = FormValidation.formValidation(document.getElementById('modalAddInstallmentForm'), {
    fields: {
        PaymentDate: {
            validators: {
                notEmpty: {
                    message: 'Tarih alaný boþ geçilemez'
                },
                date: {
                    format: 'DD.MM.YYYY',
                    message: 'Geçerli bir tarih girin'
                }

            }
        },
        InstallmentAmount: {
            validators: {
                notEmpty: {
                    message: 'Tutar alaný boþ geçilemez'
                },
                numeric: {
                    message: ' Tutar rakamlardan oluþmalýdýr',
                    decimalSeparator: ','
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
function AddInstallmentButton() {

    fvInstallment.validate().then(function (status) {
        // Update the login button content based on the validation status
        if (status == 'Valid') {
            var formData = {
                StaffPaymentId: $("#StaffPaymentId").val(),
                PaymentDate: $("#PaymentDate").val(),
                IsPaid: $("#IsPaid").is(":checked"),
                InstallmentAmount: $("#InstallmentAmount").val(),
            };
            $.ajax({
                method: 'post',
                url: '/TakePayment/AddInstallment',
                data: formData,
                success: function (data) {
                    if (data == true) {
                        Swal.fire({
                            title: "Eklendi!",
                            text: "Kaydýnýz eklendi.",
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

//update ýnstallment

function getInstallmentUpdateModal(id) {

    var modelContent = $("#InstallmentUpdateModalDiv")
    $("#InstallmentUpdateModalDiv").empty();

    $.ajax({
        method: "GET",
        url: "/TakePayment/GetInstallmentUpdateModal/" + id,
        dataType: "html",
        cache: false,
    }).done(function (content) {
        if (content != null) {
            modelContent.html(content);
        }
        $("#staffInstallmentUpdateModal").modal("show");
    }).fail(function (error) {
        alert(error);
    });
}

function InstallmentAddModal(id) {
    $("#StaffPaymentId").val(id);
    $("#staffInstallmentAddModal").modal("show");

}



function ApproveInstallment(id) {
    Swal.fire({
        title: "Onaylamak ister misiniz ?",
        text: "Bunu geri alamazsýnýz!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Evet",
        cancelButtonText: "Hayýr",
        reverseButtons: true
    }).then(function (result) {
        if (result.value) {
            $.ajax({
                method: "POST",
                url: "/TakePayment/ApproveInstallment/" + id,
            }).done(function (result) {
                debugger;
                if (result == true) {
                    Swal.fire({
                        title: "Onaylandý!",
                        text: "Ödeme Alýndý.",
                        icon: "success",
                        showCancelButton: false
                    }).then(function () {
                        window.location.reload();
                    })

                } else {
                    Swal.fire(
                        "Hata",
                        "Ödeme onaylanmadý",
                        "error"
                    )
                }

            })
        }
    });
}
function DeleteInstallment(id) {
    Swal.fire({
        title: "Silmek istediðinize emin misiniz ?",
        text: "Bunu geri alamazsýnýz!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Evet!",
        cancelButtonText: "Hayýr!",
        reverseButtons: true
    }).then(function (result) {
        if (result.value) {

            $.ajax({
                method: "POST",
                url: "/TakePayment/DeleteInstallment/" + id,
            }).done(function (result) {
                if (result == true) {
                    Swal.fire({
                        title: "Silindi!",
                        text: "Kaydýnýz silindi.",
                        icon: "success",
                        showCancelButton: false
                    }).then(function () {
                        window.location.reload();
                    })

                } else {
                    Swal.fire(
                        "Hata",
                        "Kaydýnýz silinemedi",
                        "error"
                    )
                }

            })
        }
    });
}



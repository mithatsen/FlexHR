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
                    width: 10
                }, {
                    field: 'nameSurname',
                    title: 'Ad Soyad',
                    sortable: 'asc',
                }, {
                    field: 'paymentType',
                    title: 'Ödeme Türü',
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
                    title: 'Tutar',
                }, {
                    field: 'installment',
                    title: 'Taksit Miktarý',
                },


            ],
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
                        width: 10,
                        template: function (row) {

                            return row.id;
                        }
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
                        title: 'Tutar',
                        sortable: 'asc',
                    }, {
                        field: 'isPaid',
                        title: 'Durumu',
                        // callback function support for column rendering
                        template: function (row) {
                            if (row.isPaid == true) {
                                return '<span class="label label-light-success label-inline font-weight-bold label-lg">Ödendi</span>';
                            } else {
                                return '<span class="label label-light-danger label-inline font-weight-bold label-lg">Ödenmedi</span>';
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
	                                    <a href="javascript:;" class="btn btn-sm btn-warning btn-icon mr-2" title="Düzenle">\
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
                                        <a href="javascript:;" class="btn btn-sm btn-warning btn-icon mr-2" title="Düzenle">\
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

jQuery(document).ready(function () {
    KTDatatableChildRemoteDataDemo.init();
});

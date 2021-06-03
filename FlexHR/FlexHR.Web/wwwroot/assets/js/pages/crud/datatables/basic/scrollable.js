"use strict";
var KTDatatablesBasicScrollable = function () {

    var initTable1 = function () {
        var table = $('#kt_datatable1');

        // begin first table
        table.DataTable({
            scrollY: '50vh',
            scrollX: true,
            scrollCollapse: true,

            columnDefs: [{
                targets: -2,
                width: '75px',
                render: function (data, type, full, meta) {

                    var status = {
                        'True': {
                            'title': 'Aktif',
                            'state': 'success'
                        },
                        'False': {
                            'title': 'Pasif',
                            'state': 'danger'
                        },
                    };
                    if (typeof status[data] === 'undefined') {
                        return data;
                    }
                    return '<span class="label label-' + status[data].state + ' label-dot mr-2"></span>' +
                        '<span class="font-weight-bold text-' + status[data].state + '">' + status[data].title + '</span>';
                },
            },

            ],
        });
    };

    var initTable2 = function () {
        var table = $('#kt_datatable2');
        // begin first table
        table.DataTable({
            scrollY: '50vh',
            scrollX: true,
            scrollCollapse: true,

            columnDefs: [{
                targets: -2,
                width: '75px',
                render: function (data, type, full, meta) {

                    var status = {
                        'True': {
                            'title': 'Aktif',
                            'state': 'success'
                        },
                        'False': {
                            'title': 'Pasif',
                            'state': 'danger'
                        },
                    };
                    if (typeof status[data] === 'undefined') {
                        return data;
                    }
                    return '<span class="label label-' + status[data].state + ' label-dot mr-2"></span>' +
                        '<span class="font-weight-bold text-' + status[data].state + '">' + status[data].title + '</span>';
                },
            },

            ],
        });
    };

    return {

        //main function to initiate the module
        init: function () {
            initTable1();
            initTable2();
        },

    };

}();

jQuery(document).ready(function () {
    KTDatatablesBasicScrollable.init();
});

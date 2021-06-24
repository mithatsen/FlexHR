"use strict";
var KTDatatablesAdvancedColumnRendering = function () {

    var init = function () {
        var table = $('#kt_datatable');

        // begin first table
        table.DataTable({
            responsive: true,
            paging: true,
            columnDefs: [

                {
                    targets: 0,
                    title: '#',
                    width: '75px',
                    render: function (data, type, full, meta) {
                        var status = {
                            'True': {
                                'title': '',
                                'state': 'success'
                            },
                            'False': {
                                'title': '',
                                'state': 'danger'
                            },
                        };
                        if (typeof status[data] === 'undefined') {
                            return data;
                        }
                        return '<span class="label label-' + status[data].state + ' label-dot label-xl  mr-2"></span>'
                        // + '<span class="font-weight-bold text-' + status[data].state + '">' + status[data].title + '</span>';
                    },
                },

            ],
        });
        var table = $('#kt_datatable_2');

        // begin first table
        table.DataTable({
            responsive: true,
            paging: true,
            columnDefs: [

                {
                    targets: -2,
                    title: 'Durum',
                    width: '75px',
                    language: "tr",
                    render: function (data, type, full, meta) {
                        var status = {
                            '96': {
                                'title': 'Onay Bekliyor',
                                'state': 'warning'
                               
                            },
                            '98': {
                                'title': 'Reddedildi',
                                'state': 'danger'
                            },
                            '97': {
                                'title': 'Onaylandý',
                                'state': 'success'
                            }
                        };
                        if (typeof status[data] === 'undefined') {
                            return data;
                        }
                        return '<span class="text-light font-weight-bold badge badge-' + status[data].state+'">' + status[data].title + '</span>';
                    },
                },

            ],
        });
<<<<<<< HEAD
        var table = $('#kt_datatable_4');
=======
        var table = $('#kt_datatable_3');
>>>>>>> 06cd49799e1b2442e414f4466d6949c23b4090be

        // begin first table
        table.DataTable({
            responsive: true,
            paging: true,
            columnDefs: [

<<<<<<< HEAD
                //{
                //    targets: -2,
                //    title: 'Durum',
                //    width: '75px',
                //    language: "tr",
                //    render: function (data, type, full, meta) {
                //        var status = {
                //            '96': {
                //                'title': 'Onay Bekliyor',
                //                'state': 'warning'

                //            },
                //            '98': {
                //                'title': 'Reddedildi',
                //                'state': 'danger'
                //            },
                //            '97': {
                //                'title': 'Onaylandý',
                //                'state': 'success'
                //            }
                //        };
                //        if (typeof status[data] === 'undefined') {
                //            return data;
                //        }
                //        return '<span class="text-light font-weight-bold badge badge-' + status[data].state + '">' + status[data].title + '</span>';
                //    },
                //},
=======
                {
                    targets: -4,
                    title: 'Durum',
                    width: '75px',
                    language: "tr",
                    render: function (data, type, full, meta) {
                        var status = {
                            '96': {
                                'title': 'Onay Bekliyor',
                                'state': 'warning'

                            },
                            '98': {
                                'title': 'Reddedildi',
                                'state': 'danger'
                            },
                            '97': {
                                'title': 'Onaylandý',
                                'state': 'success'
                            }
                        };
                        if (typeof status[data] === 'undefined') {
                            return data;
                        }
                        return '<span class="text-light font-weight-bold badge badge-' + status[data].state + '">' + status[data].title + '</span>';
                    },
                },
>>>>>>> 06cd49799e1b2442e414f4466d6949c23b4090be

            ],
        });

        $('#kt_datatable_search_status').on('change', function () {
            datatable.search($(this).val().toLowerCase(), 'Status');
        });

        $('#kt_datatable_search_type').on('change', function () {
            datatable.search($(this).val().toLowerCase(), 'Type');
        });

        $('#kt_datatable_search_status, #kt_datatable_search_type').selectpicker();
    };

    return {

        //main function to initiate the module
        init: function () {
            init();
        }
    };
}();

jQuery(document).ready(function () {
    KTDatatablesAdvancedColumnRendering.init();
});

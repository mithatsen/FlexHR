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
                    width: '20px',

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
        var table2 = $('#kt_datatable_2');

        // begin first table
        table2.DataTable({
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
                                'title': 'Onaylandı',
                                'state': 'success'
                            }
                        };
                        if (typeof status[data] === 'undefined') {
                            return data;
                        }
                        return '<span class="text-light font-weight-bold badge badge-' + status[data].state + '">' + status[data].title + '</span>';
                    },
                },

            ],
        });

        var table = $('#kt_datatable_3');

        // begin first table
        table.DataTable({
            responsive: true,
            paging: true,
            columnDefs: [

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
                                'title': 'Onaylandı',
                                'state': 'success'
                            }
                        };
                        if (typeof status[data] === 'undefined') {
                            return data;
                        }
                        return '<span class="text-light font-weight-bold badge badge-' + status[data].state + '">' + status[data].title + '</span>';
                    },

                },
                {
                    targets: -2,
                    title: 'ÖDENDİ Mİ',
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
                        else if (data == 'True') {
                            return '<span class="mr-2"><i class="fas fa-check text-' + status[data].state + '"></i></span>'
                        }
                        else {
                            return '<span class="mr-2"><i class="fas fa-times text-' + status[data].state + '"></i></span>'
                        }

                        // + '<span class="font-weight-bold text-' + status[data].state + '">' + status[data].title + '</span>';
                    },
                },

            ],
        });
        var table = $('#kt_datatable_4');

        // begin first table

        table.DataTable({
            responsive: true,
            paging: true,
            columnDefs: [

                {
                    targets: -1,
                    title: 'Çalışma Durumu',
                    width: '20px',

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


        var table = $('#kt_datatable_5');

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
                                'title': 'Onaylandı',
                                'state': 'success'
                            }
                        };
                        if (typeof status[data] === 'undefined') {
                            return data;
                        }
                        return '<span class="text-light font-weight-bold badge badge-' + status[data].state + '">' + status[data].title + '</span>';
                    },
                },

            ],
        });
        var table = $('#kt_datatable_6');

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
                                'title': 'Onaylandı',
                                'state': 'success'
                            }
                        };
                        if (typeof status[data] === 'undefined') {
                            return data;
                        }
                        return '<span class="text-light font-weight-bold badge badge-' + status[data].state + '">' + status[data].title + '</span>';
                    },
                },

            ],
        });
        var table = $('#kt_datatable_7');

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
                                'title': 'Onaylandı',
                                'state': 'success'
                            }
                        };
                        if (typeof status[data] === 'undefined') {
                            return data;
                        }
                        return '<span class="text-light font-weight-bold badge badge-' + status[data].state + '">' + status[data].title + '</span>';
                    },
                },

            ],
        });
        var table = $('#kt_datatable_8');

        // begin first table

        table.DataTable({
            responsive: true,
            paging: true,
            columnDefs: [

                {
                    targets: -1,
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
                                'title': 'Onaylandı',
                                'state': 'success'
                            }
                        };
                        if (typeof status[data] === 'undefined') {
                            return data;
                        }
                        return '<span class="text-light font-weight-bold badge badge-' + status[data].state + '">' + status[data].title + '</span>';
                    },
                },

            ],
        });

        var table = $('#kt_datatable_9');

        // begin first table
        table.DataTable({
            responsive: true,
            paging: true,

        });
        var table = $('#kt_datatable_10');
        // begin first table
        table.DataTable({
            responsive: true,
            paging: true,
            columnDefs: [

                {
                    targets: -1,
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
                                'title': 'Onaylandı',
                                'state': 'success'
                            }
                        };
                        if (typeof status[data] === 'undefined') {
                            return data;
                        }
                        return '<span class="text-light font-weight-bold badge badge-' + status[data].state + '">' + status[data].title + '</span>';
                    },
                },

            ],
        });

        var table = $('#kt_datatable_11');
        // begin first table
        table.DataTable({
            responsive: true,
            paging: true,
            columnDefs: [

                {
                    targets: -1,
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
                                'title': 'Onaylandı',
                                'state': 'success'
                            }
                        };
                        if (typeof status[data] === 'undefined') {
                            return data;
                        }
                        return '<span class="text-light font-weight-bold badge badge-' + status[data].state + '">' + status[data].title + '</span>';
                    },
                },

            ],
        });

        var table = $('#kt_datatable_12');
        // begin first table
        table.DataTable({
            responsive: true,
            paging: true,
            columnDefs: [

                {
                    targets: -1,
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
                                'title': 'Onaylandı',
                                'state': 'success'
                            }
                        };
                        if (typeof status[data] === 'undefined') {
                            return data;
                        }
                        return '<span class="text-light font-weight-bold badge badge-' + status[data].state + '">' + status[data].title + '</span>';
                    },
                },

            ],
        });
        var table = $('#kt_datatable_13');
        // begin first table
        table.DataTable({

            responsive: true,
            paging: true,
            columnDefs: [

                {
                    targets: -1,
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
                                'title': 'Onaylandı',
                                'state': 'success'
                            }
                        };
                        if (typeof status[data] === 'undefined') {
                            return data;
                        }
                        return '<span class="text-light font-weight-bold badge badge-' + status[data].state + '">' + status[data].title + '</span>';
                    },
                },

            ],
        });

        var table = $('#kt_datatable_14');

        // begin first table
        table.DataTable({
            responsive: true,
            paging: true,

        });
        var table = $('#kt_datatable_15');

        // begin first table
        table.DataTable({
            responsive: true,
            paging: true,

        });
        var table = $('#kt_datatable_16');

        // begin first table
        table.DataTable({
            responsive: true,
            paging: true,

        });

        var table = $('#kt_datatable_17');

        // begin first table
        table.DataTable({
            responsive: true,
            paging: true,
            scrollY: "500px",
            scrollCollapse: true,
            columnDefs: [
                {
                    targets: 0,
                    title: 'Ad Soyad',
                    render: function (data, type, full, meta) {
                        var user_img = full[0];
                        var stringSplitted = user_img.split("~");


                        var output;
                        if (stringSplitted[0] != "-") {
                            output = `
                                <div class="d-flex align-items-center">
                                    <div class="symbol symbol-50 flex-shrink-0">
                                        <img src="/img/` + stringSplitted[0] + `" alt="photo">
                                    </div>
                                    <div class="ml-3">
                                         <a href="StaffGeneral/Index/` + stringSplitted[3] + `" class=" text-hover-primary text-dark-75 font-weight-bold line-height-sm d-block pb-2">` + stringSplitted[2] + `</a>
                                         <span class="text-muted font-weight-bold line-height-sm d-block pb-2">`+ stringSplitted[1] + `</span>
                                    </div>
                                </div>`;
                        }

                        else {
                            var stateNo = KTUtil.getRandomInt(0, 6);
                            var states = [
                                'success',
                                'danger',
                                'success',
                                'warning',
                                'dark',
                                'primary',
                                'info'];

                            var state = states[stateNo];

                            output = `
                                <div class="d-flex align-items-center">
                                    <div class="symbol symbol-50 symbol-light-` + state + `" flex-shrink-0">
                                        <div class="symbol-label font-size-h5">` + stringSplitted[2].substring(0, 1) + `</div>
                                    </div>
                                    <div class="ml-3">
                                       <a href="StaffGeneral/Index/` + stringSplitted[3] + `" class=" text-hover-primary text-dark-75 font-weight-bold line-height-sm d-block pb-2">` + stringSplitted[2] + `</a>
                                         <span class="text-muted font-weight-bold line-height-sm d-block pb-2">`+ stringSplitted[1] + `</span>
                                    </div>
                                </div>`;
                        }

                        return output;
                    },
                },
                {
                    targets: 1,
                    render: function (data, type, full, meta) {
                        return '<a class="text-dark-50 text-hover-primary" href="mailto:' + data + '">' + data + '</a>';
                    },
                },

            ],
        });

        var table = $('#kt_datatable_18');
        // begin first table
        table.DataTable({
            scrollY: "500px",
            scrollCollapse: true,
            responsive: true,
            paging: true,

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

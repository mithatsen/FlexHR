// Class definition

var KTBootstrapDatetimepicker = function () {
    // Private functions
    var baseDemos = function () {
        // Demo 1
        $('#kt_datetimepicker_1').datetimepicker();

        // Demo 2
        $('#kt_datetimepicker_2').datetimepicker({
            locale: 'tr',
            format: 'DD.MM.YYYY'
        });

        // Demo 3
        $('#kt_datetimepicker_3').datetimepicker({
            locale: 'tr',
            format: 'DD.MM.YYYY'
        });

        // Demo 4
        $('#kt_datetimepicker_4').datetimepicker({
            locale: 'tr',
            format: 'DD.MM.YYYY'
        });
        $('#kt_datetimepicker_5').datetimepicker({
            locale: 'tr',
            format: 'DD.MM.YYYY'
        });
        $('#kt_datetimepicker_6').datetimepicker({
            locale: 'tr',
            format: 'DD.MM.YYYY'
        });
        $('#kt_datetimepicker_29').datetimepicker({
            locale: 'tr',
            format: 'DD.MM.YYYY'
        });
        $('#kt_datetimepicker_45').datetimepicker({
            locale: 'tr',
            format: 'DD.MM.YYYY'
        });
        $('#kt_datetimepicker_14').datetimepicker({
            locale: 'tr',
            format: 'DD.MM.YYYY'
        });
        $('#kt_datetimepicker_15').datetimepicker({
            locale: 'tr',
            format: 'DD.MM.YYYY'
        });
        $('#kt_datetimepicker_16').datetimepicker({
            locale: 'tr',
            format: 'DD.MM.YYYY'
        });
        $('#kt_datetimepicker_17').datetimepicker({
            locale: 'tr',
            format: 'DD.MM.YYYY'
        });
        $('#kt_datetimepicker_18').datetimepicker({
            locale: 'tr',
            format: 'DD.MM.YYYY HH:mm'
        });
        $('#kt_datetimepicker_20').datetimepicker({
            locale: 'tr',
            format: 'DD.MM.YYYY HH:mm'
        });


    }

    var modalDemos = function () {
        // Demo 9
        $('#kt_datetimepicker_9').datetimepicker();

        // Demo 10
        $('#kt_datetimepicker_10').datetimepicker({
            locale: 'de'
        });

        // Demo 11
        $('#kt_datetimepicker_11').datetimepicker({
            format: 'L'
        });
    }

    var validationDemos = function () {
        // Demo 12
        $('#kt_datetimepicker_12').datetimepicker();

        // Demo 13
        $('#kt_datetimepicker_13').datetimepicker();
    }

    return {
        // Public functions
        init: function() {
            baseDemos();
            modalDemos();
            validationDemos();
        }
    };
}();

jQuery(document).ready(function() {
    KTBootstrapDatetimepicker.init();
});

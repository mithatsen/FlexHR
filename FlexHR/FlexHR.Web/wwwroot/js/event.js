function deletePublicHoliday() {
    var id = $("#publicHolidayId").val();
    Swal.fire({
        title: "Silmek istediğinize emin misiniz ?",
        text: "Bunu geri alamazsınız!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Evet!",
        cancelButtonText: "Hayır!",
        reverseButtons: true
    }).then(function (result) {
        if (result.value) {

            $.ajax({
                method: "POST",
                url: "/Event/DeletePublicHoliday/" + id,
            }).done(function (result) {
                if (result == true) {
                    Swal.fire({
                        title: "Silindi!",
                        text: "Kaydınız silindi.",
                        icon: "success",
                        showCancelButton: false
                    }).then(function () {
                        window.location.reload();
                    })

                } else {
                    Swal.fire(
                        "Hata",
                        "Kaydınız silinemedi",
                        "error"
                    )
                }

            })
        } else if (result.dismiss === "cancel") {
            Swal.fire({
                title: "İptal Edildi",
                text: "Kaydınız silinmedi",
                icon: "error",
                showCancelButton: false
            })

        }
    });

}

function deleteEvent() {
    var id = $("#eventId").val();
    Swal.fire({
        title: "Silmek istediğinize emin misiniz ?",
        text: "Bunu geri alamazsınız!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Evet!",
        cancelButtonText: "Hayır!",
        reverseButtons: true
    }).then(function (result) {
        if (result.value) {

            $.ajax({
                method: "POST",
                url: "/Event/DeleteEvent/" + id,
            }).done(function (result) {
                if (result == true) {
                    Swal.fire({
                        title: "Silindi!",
                        text: "Kaydınız silindi.",
                        icon: "success",
                        showCancelButton: false
                    }).then(function () {
                        window.location.reload();
                    })

                } else {
                    Swal.fire(
                        "Hata",
                        "Kaydınız silinemedi",
                        "error"
                    )
                }

            })
        } else if (result.dismiss === "cancel") {
            Swal.fire({
                title: "İptal Edildi",
                text: "Kaydınız silinmedi",
                icon: "error",
                showCancelButton: false
            })

        }
    });

}

function FullCalenderProcessPublicHoliday(calEvent) {
    $("#publicHolidayId").val(calEvent.event.id)
    $("#publicHolidayDescription").val(calEvent.event.extendedProps.description)
    var ds = calEvent.event.start.toString();
    var date = moment(new Date(ds.substr(0, 25)));
    console.log(calEvent.event);
    if (calEvent.event.end != null) {
        var ds2 = calEvent.event.end.toString();
        var date2 = moment(new Date(ds2.substr(0, 25)));
        $("#publicHolidayEndDate").val(date2.format("DD.MM.YYYY"))
    }
    $("#publicHolidayStartDate").val(date.format("DD.MM.YYYY"))

    $("#publicHolidayTitle").val(calEvent.event.title);
    var temp = !calEvent.event.allDay;
    $("#publicHolidayCheck").prop('checked', temp);

}

function FullCalenderProcess(calEvent) {

    $("#eventId").val(calEvent.event.id)
    $("#eventDescription").val(calEvent.event.extendedProps.description)
    var ds = calEvent.event.start.toString();
    var date = moment(new Date(ds.substr(0, 25)));
    console.log(calEvent.event);
    var ds2 = calEvent.event.end.toString();
    var date2 = moment(new Date(ds2.substr(0, 25)));
    $("#eventStartDate").val(date.format("DD.MM.YYYY HH:mm"))
    $("#eventEndDate").val(date2.format("DD.MM.YYYY HH:mm"))
    $("#eventTitle").val(calEvent.event.title)

}
var KTCalendarListView = function () {

    return {
        //main function to initiate the module
        init: function () {
            var todayDate = moment().startOf('day');
            var YM = todayDate.format('YYYY-MM');
            var YESTERDAY = todayDate.clone().subtract(1, 'day').format('YYYY-MM-DD');
            var TODAY = todayDate.format('YYYY-MM-DD');
            var TOMORROW = todayDate.clone().add(1, 'day').format('YYYY-MM-DD');

            var calendarEl = document.getElementById('kt_calendar');
            var calendar = new FullCalendar.Calendar(calendarEl, {
                plugins: ['interaction', 'dayGrid', 'timeGrid', 'list'],
                locale: 'TR',
                isRTL: KTUtil.isRTL(),
                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'dayGridMonth,timeGridWeek,timeGridDay,listWeek'
                },

                height: 800,
                contentHeight: 750,
                aspectRatio: 3,  // see: https://fullcalendar.io/docs/aspectRatio

                views: {
                    dayGridMonth: { buttonText: 'Ay' },
                    timeGridWeek: { buttonText: 'Hafta' },
                    timeGridDay: { buttonText: 'Gün' },
                    listDay: { buttonText: 'Liste' },
                    listWeek: { buttonText: 'Liste' }
                },

                defaultView: 'dayGridMonth',
                defaultDate: TODAY,
                firstDay:1,
                editable: true,

                eventLimit: true, // allow "more" link when too many events
                navLinks: true,
                events: "Event/GetAllEvents",
                eventDrop: function (calEvent) {
                    if ($(calEvent.el).hasClass("fc-event-primary")) {
                        FullCalenderProcessPublicHoliday(calEvent);
                        $("#btnUpdatePublicHoliday").trigger("click");
                    }
                    else if ($(calEvent.el).hasClass("fc-event-danger")) {
                        FullCalenderProcess(calEvent);
                        $("#btnUpdateEvent").trigger("click");
                    }
                },
                eventResize: function (calEvent) {
                    if ($(calEvent.el).hasClass("fc-event-primary")) {
                        console.log("dkdkdk");
                        console.log(calEvent);
                        $("#publicHolidayEndDate").val(calEvent.event.end);
                        FullCalenderProcessPublicHoliday(calEvent);
                        $("#btnUpdatePublicHoliday").trigger("click");
                    }
                    else if ($(calEvent.el).hasClass("fc-event-danger")) {
                        console.log(calEvent);
                        FullCalenderProcess(calEvent);
                        $("#btnUpdateEvent").trigger("click");
                    }

                },
                eventClick: function (calEvent) {

                    if ($(calEvent.el).hasClass("fc-event-primary")) {
                        FullCalenderProcessPublicHoliday(calEvent);
                        $("#publicHolidayUpdateModal").modal("show");
                    }
                    else if ($(calEvent.el).hasClass("fc-event-danger")) {
                        FullCalenderProcess(calEvent);
                        $("#eventUpdateModal").modal("show");
                    }


                },
                dateClick: function (info) {
                    console.log(info.date.toString());
                    var ds2 = info.date.toString();
                    var date2 = moment(new Date(ds2.substr(0, 25)));
                    $("#eventAddStartDate").val(date2.format("DD.MM.YYYY HH:mm"))
                    $("#eventAddModal").modal("show");

                },
                eventRender: function (info) {
                    var element = $(info.el);

                    if (info.event.extendedProps && info.event.extendedProps.description) {
                        if (element.hasClass('fc-day-grid-event')) {
                            element.data('content', info.event.extendedProps.description);
                            element.data('placement', 'top');
                            KTApp.initPopover(element);
                        } else if (element.hasClass('fc-time-grid-event')) {
                            element.find('.fc-title').append('<div class="fc-description">' + info.event.extendedProps.description + '</div>');
                        } else if (element.find('.fc-list-item-title').lenght !== 0) {
                            element.find('.fc-list-item-title').append('<div class="fc-description">' + info.event.extendedProps.description + '</div>');
                        }
                    }
                },
            });

            calendar.render();

        }
    };
}();

jQuery(document).ready(function () {
    KTCalendarListView.init();
});

FormValidation.formValidation(
    document.getElementById('modalAddEventForm'),
    {
        fields: {

            Description: {
                validators: {
                    stringLength: {
                        max: 1999,
                        message: '2000 karakterden az girin'
                    }
                }
            },

            Title: {
                validators: {
                    notEmpty: {
                        message: 'Başlık alanı boş geçilemez'
                    },
                    stringLength: {
                        max: 100,
                        message: '100 karakterden az girin'
                    }
                }
            },
            Start: {
                validators: {
                    notEmpty: {
                        message: 'Tarih alanı boş geçilemez'
                    },
                    date: {
                        format: 'DD.MM.YYYY HH:mm',
                        message: 'Geçerli bir tarih girin'
                    }
                }
            },
            End: {
                validators: {
                    notEmpty: {
                        message: 'Tarih alanı boş geçilemez'
                    },
                    date: {
                        format: 'DD.MM.YYYY HH:mm',
                        message: 'Geçerli bir tarih girin'
                    }
                }
            },
        },

        plugins: {
            trigger: new FormValidation.plugins.Trigger(),
            // Bootstrap Framework Integration
            bootstrap: new FormValidation.plugins.Bootstrap(),
            // Validate fields when clicking the Submit button
            submitButton: new FormValidation.plugins.SubmitButton(),
            // Submit the form when all fields are valid
            defaultSubmit: new FormValidation.plugins.DefaultSubmit(),
        }
    }
);

FormValidation.formValidation(
    document.getElementById('modalAddLeaveForm'),
    {
        fields: {

            Description: {
                validators: {
                    stringLength: {
                        max: 1999,
                        message: '2000 karakterden az girin'
                    }
                }
            },

            Title: {
                validators: {
                    notEmpty: {
                        message: 'Başlık alanı boş geçilemez'
                    },
                    stringLength: {
                        max: 100,
                        message: '100 karakterden az girin'
                    }
                }
            },
            Start: {
                validators: {
                    notEmpty: {
                        message: 'Tarih alanı boş geçilemez'
                    },
                    date: {
                        format: 'DD.MM.YYYY HH:mm',
                        message: 'Geçerli bir tarih girin'
                    }
                }
            },
            End: {
                validators: {
                    notEmpty: {
                        message: 'Tarih alanı boş geçilemez'
                    },
                    date: {
                        format: 'DD.MM.YYYY HH:mm',
                        message: 'Geçerli bir tarih girin'
                    }
                }
            },
        },

        plugins: {
            trigger: new FormValidation.plugins.Trigger(),
            // Bootstrap Framework Integration
            bootstrap: new FormValidation.plugins.Bootstrap(),
            // Validate fields when clicking the Submit button
            submitButton: new FormValidation.plugins.SubmitButton(),
            // Submit the form when all fields are valid
            defaultSubmit: new FormValidation.plugins.DefaultSubmit(),
        }
    }
);

FormValidation.formValidation(
    document.getElementById('modalAddPublicHolidayForm'),
    {
        fields: {

            Description: {
                validators: {
                    stringLength: {
                        max: 1999,
                        message: '2000 karakterden az girin'
                    }
                }
            },

            Title: {
                validators: {
                    notEmpty: {
                        message: 'Başlık alanı boş geçilemez'
                    },
                    stringLength: {
                        max: 100,
                        message: '100 karakterden az girin'
                    }
                }
            },
            Start: {
                validators: {
                    notEmpty: {
                        message: 'Tarih alanı boş geçilemez'
                    },
                    date: {
                        format: 'DD.MM.YYYY',
                        message: 'Geçerli bir tarih girin'
                    }
                }
            }
        },

        plugins: {
            trigger: new FormValidation.plugins.Trigger(),
            // Bootstrap Framework Integration
            bootstrap: new FormValidation.plugins.Bootstrap(),
            // Validate fields when clicking the Submit button
            submitButton: new FormValidation.plugins.SubmitButton(),
            // Submit the form when all fields are valid
            defaultSubmit: new FormValidation.plugins.DefaultSubmit(),
        }
    }
);

FormValidation.formValidation(
    document.getElementById('modalUpdatePublicHolidayForm'),
    {
        fields: {

            Description: {
                validators: {
                    stringLength: {
                        max: 1999,
                        message: '2000 karakterden az girin'
                    }
                }
            },

            Title: {
                validators: {
                    notEmpty: {
                        message: 'Başlık alanı boş geçilemez'
                    },
                    stringLength: {
                        max: 100,
                        message: '100 karakterden az girin'
                    }
                }
            },
            Start: {
                validators: {
                    notEmpty: {
                        message: 'Tarih alanı boş geçilemez'
                    },
                    date: {
                        format: 'DD.MM.YYYY',
                        message: 'Geçerli bir tarih girin'
                    }
                }
            }
        },

        plugins: {
            trigger: new FormValidation.plugins.Trigger(),
            // Bootstrap Framework Integration
            bootstrap: new FormValidation.plugins.Bootstrap(),
            // Validate fields when clicking the Submit button
            submitButton: new FormValidation.plugins.SubmitButton(),
            // Submit the form when all fields are valid
            defaultSubmit: new FormValidation.plugins.DefaultSubmit(),
        }
    }
);


$("#eventAddStartDate").val(moment().format('DD.MM.YYYY h:mm:ss'))
// Demo 7
$('#kt_datetimepicker_18').datetimepicker({
    locale: 'tr',
    format: 'DD.MM.YYYY HH:mm'
});
$('#kt_datetimepicker_20').datetimepicker({
    locale: 'tr',
    format: 'DD.MM.YYYY HH:mm'
});

$('#kt_datetimepicker_18').on('change.datetimepicker', function (e) {
    $('#kt_datetimepicker_20').datetimepicker('minDate', e.date);
});
$('#kt_datetimepicker_20').on('change.datetimepicker', function (e) {
    $('#kt_datetimepicker_18').datetimepicker('maxDate', e.date);
});


$('#kt_datetimepicker_99').datetimepicker({
    locale: 'tr',
    format: 'DD.MM.YYYY HH:mm',
    autoclose: true
});

$('#kt_datetimepicker_100').datetimepicker({
    locale: 'tr',
    format: 'DD.MM.YYYY HH:mm',
    autoclose: true
}); 

$('#kt_datetimepicker_99').on('change.datetimepicker', function (e) {
    $('#kt_datetimepicker_100').datetimepicker('minDate', e.date);
    var x = $('#eventStartDate').val().split(".").reverse().join(".");
    var y = $('#eventEndDate').val().split(".").reverse().join(".");
    if (x > y) {

        $('#eventEndDate').val($('#eventStartDate').val());
    }
});
$('#kt_datetimepicker_100').on('change.datetimepicker', function (e) {
    $('#kt_datetimepicker_99').datetimepicker('maxDate', e.date);
});
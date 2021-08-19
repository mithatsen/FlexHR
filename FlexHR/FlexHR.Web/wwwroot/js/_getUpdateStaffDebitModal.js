FormValidation.formValidation(
    document.getElementById('debitUpdateModal'),
    {
        fields: {
            IssueDate: {
                validators: {
                    notEmpty: {
                        message: 'Tarih alanı boş geçilemez'
                    },
                    date: {
                        format: 'DD.MM.YYYY',
                        message: 'Geçerli bir tarih girin'
                    }

                }
            },
            ReturnDate: {
                validators: {

                    date: {
                        format: 'DD.MM.YYYY',
                        message: 'Geçerli bir tarih girin'
                    }

                }
            },
            DebitGeneralSubTypeId: {
                validators: {
                    notEmpty: {
                        message: 'Kategori alanı boş geçilemez'
                    }
                }
            },
            Description: {
                validators: {
                    notEmpty: {
                        message: 'Açıklama alanı boş geçilemez'
                    },
                    stringLength: {
                        max: 499,
                        message: '500 karakterden az girin'
                    }
                }
            },
            SerialNumber: {
                validators: {
                    stringLength: {
                        max: 499,
                        message: 'Seri numarası 50 karakterden az olmalı'
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

$('#kt_datetimepicker_38').datetimepicker({
    locale: 'tr',
    format: 'DD.MM.YYYY'
});
$('#kt_datetimepicker_39').datetimepicker({
    locale: 'tr',
    format: 'DD.MM.YYYY'
});
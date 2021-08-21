FormValidation.formValidation(
    document.getElementById('uploadExcelFileForm'),
    {
        fields: {
            Date: {
                validators: {
                    notEmpty: {
                        message: 'Tarih alanı boş geçilemez'
                    },
                    date: {
                        format: 'DD.MM.YYYY',
                        message: 'Geçerli bir tarih girin'
                    },
                }
            },
            file: {
                validators: {
                    notEmpty: {
                        message: 'Dosya ekleme alanı boş geçilemez'
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
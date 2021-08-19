FormValidation.formValidation(
    document.getElementById('updateCareerForm'),
    {
        fields: {
            JobStartDate: {
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
            JobFinishDate: {
                validators: {

                    date: {
                        format: 'DD.MM.YYYY',
                        message: 'Geçerli bir tarih girin'
                    }
                }
            },
            DepartmantGeneralSubTypeId: {
                validators: {
                    notEmpty: {
                        message: 'Departman seçiniz'
                    },

                }
            },
            CompanyId: {
                validators: {
                    notEmpty: {
                        message: 'Şirket seçiniz'
                    }
                }
            },
            ModeOfOperationGeneralSubTypeId: {
                validators: {
                    notEmpty: {
                        message: 'Çalışma şekli seçiniz'
                    },

                }
            },
            TitleGeneralSubTypeId: {
                validators: {
                    notEmpty: {
                        message: 'Unvan seçiniz'
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
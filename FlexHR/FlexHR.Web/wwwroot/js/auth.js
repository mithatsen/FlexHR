FormValidation.formValidation(
    document.getElementById('signInForm'),
    {
        fields: {
            UserName: {
                validators: {
                    notEmpty: {
                        message: 'Kullanıcı adı alanı boş geçilemez'
                    },
                    stringLength: {
                        max: 100,
                        message: '100 karakterden az girin'
                    }
                }
            },
            Password: {
                validators: {
                    notEmpty: {
                        message: 'Şifre alanı boş geçilemez'
                    },
                    stringLength: {
                        max: 100,
                        message: '100 karakterden az girin'
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
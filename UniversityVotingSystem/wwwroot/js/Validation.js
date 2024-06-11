$(function () {
    jQuery.validator.addMethod("lettersonly", function (value, element) {
        return this.optional(element) || /^[a-z]+$/i.test(value);
    }, "This input accepts only latin letters");
    $("#register-form").validate({
        rules: {
            email: {
                required: true,
                email:true
            },
            password: {
                required: true,
                maxlength: 16,
                minlength:8,
            },
            password2: {
                required: true,
                equalTo: "#password",
                maxlength: 16,
                minlength: 8,
            },
            first_name: {
                required: true,
                lettersonly:true,
            },
            last_name: {
                required: true,
                lettersonly: true,
            },
            phone_number: {
                required: true,
                maxlength: 20,
                digits:true,
            },
        }
    })
})
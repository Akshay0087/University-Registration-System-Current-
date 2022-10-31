
function PasswordMatch(password1, password2) {

    toastr.options.timeOut = 100000;
    PasswordValidation(password1);
    if (password1.value != password2.value && password2.value!="") {
        password2.setCustomValidity('Password does not match');
        toastr.error("Password does not match");
        password2.value = "";
    }
}

function PasswordValidation(element) {
    if (element.value.length < 8 && element.value.length > 25) {
        toastr.error("Incorrect password length");
        element.setCustomValidity("Password character length requirement(8-25)");
        element.value = "";
    }
}
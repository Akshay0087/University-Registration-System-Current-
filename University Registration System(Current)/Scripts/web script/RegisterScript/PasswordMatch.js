function PasswordMatch(password1, password2) {

    toastr.options.timeOut = 100000;
    
    if (password1.value != password2.value && password2.value!="") {
        toastr.error("Password does not match");
        password2.value = "";
    }
    PasswordValidation(password1);
}

function PasswordValidation(element) {
    if (element.value.length < 8 || element.value.length > 25) {
        toastr.error("Incorrect password length. length requirement is (8-25)");
        element.value = "";
    }
}
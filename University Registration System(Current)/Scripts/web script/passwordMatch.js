﻿
function passwordMatch(password1, password2) {
    if (password1.value != password2.value && password2.value!="") {
        password2.setCustomValidity('Password does not match');
        toastr.error("'Password does not match'");
        password2.value = "";
    }
}
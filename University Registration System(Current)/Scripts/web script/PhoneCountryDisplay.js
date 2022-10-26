$("phone").on("click", function () {
    const phoneInputField = document.querySelector("#phone");
    const phoneInput = window.intlTelInput(phoneInputField, {
        initialCountry: "mu",
        utilsScript:
            "https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/17.0.8/js/utils.js",
    });
    //verify if number entered is valid
    if (!phoneInput.isValidNumber()) {
        toastr.error("Invalid phone number!");
    }
});
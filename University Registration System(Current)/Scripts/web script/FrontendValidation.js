toastr.options.timeOut = 100000;


function EmailValidation(element) {
	var emailRegex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
	if (!emailRegex.test(element.value)) {
		toastr.error("Invalid Email Format");
		element.setCustomValidity("Invalid Email Format");
		element.value = "";
	}
	if (element.value.length < 8 || element.value.length > 50) {
		toastr.error("Incorrect Email length");
		element.setCustomValidity("Address character requirement(8-50)");
		element.value = "";
	}
}

function PasswordValidation(element) {
	if (element.value.length < 8 && element.value.length > 25) {
		toastr.error("Incorrect password length");
		element.setCustomValidity("Password character length requirement(8-25)");
		element.value = "";
	}
}
function NameValidation(element) { 
	if (element.value.length < 2 || element.value.length > 50) {
		var attributeName = element.getAttribute("name")
		toastr.error("Incorrect " + attributeName + " length");
		element.setCustomValidity(attributeName + " character length requirement(2-50)");
		element.value = "";
	}
}

function NationalIdentityNumberValidation() {
	var minlength = "9";
	var maxlength = "16";
	var NIDRegexCharacter =/^[a-zA-Z0-9]*$/;
	if (element.value.length < 9 || element.value.length > 20) {
		toastr.error("Incorrect NID length");
		element.setCustomValidity("NID characters length requirement(" + minlength + "-" + maxlength + ")");
		element.value = "";
	}
	if (!NIDRegexCharacter.test(element.value)) {
		toastr.error("Invalid NID Format");
		element.setCustomValidity("Invalid Email Format (Only letters and numbers accepted)");
		element.value = "";
	}
}

function addressValidation(element) {
	if (element.value.length < 2 || element.value.length > 100) {
		toastr.error("Incorrect Adress length");
		element.setCustomValidity("Address character length requirement (2-100)");
		element.value = "";
	}
}
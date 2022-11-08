toastr.options.timeOut = 100000;

function EmailValidation(element) {
	var emailRegex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
	if (!emailRegex.test(element.value)) {
		toastr.error("Invalid Email Format");
		element.value = "";
	}
	if (element.value.length < 8 || element.value.length > 50) {
		toastr.error("Incorrect Email length");
		element.value = "";
	}
}

function NameValidation(element) {
	if (element.value.length < 2 || element.value.length > 50) {
		var attributeName = element.getAttribute("name")
		toastr.error("Incorrect " + attributeName + " length");
		element.value = "";
	}
}

function NationalIdentityNumberValidation(element) {
	var minLength = 9;
	var maxLength = 16;
	var NIDRegexCharacter = /^[a-zA-Z0-9]*$/;
	if (element.value.length < minLength || element.value.length > maxLength) {
		toastr.error("Incorrect NID length");
		element.value = "";
	}
	if (!NIDRegexCharacter.test(element.value)) {
		toastr.error("Invalid NID Format");
		element.value = "";
	}
}

function AddressValidation(element) {
	if (element.value.length < 2 || element.value.length > 100) {
		toastr.error("Incorrect Address length");
		element.value = "";
	}
}

function PhoneNumberValidation(element) {
	var phoneNumberPattern = /^[0-9]*$/;

	if (!(element.value.length == 8 || element.value.length == 7)) {
		toastr.error("Incorrect Phone number length (8 character)");
		element.value = "";
	}
	if (!phoneNumberPattern.test(element.value)) {
		toastr.error("Invalid character");
		element.value = "";
	}
}
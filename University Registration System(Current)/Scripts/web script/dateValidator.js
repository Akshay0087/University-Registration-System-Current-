function RegistrationDateValidation(id) {

	var rawFieldValue = id.value;
	const minimumAgeToAccept = 16;
	const maximumAgeToAccept = 100;
	var rawToDate = new Date(rawFieldValue);
	var todayDate = new Date();
	var maximumDateRequired = new Date(todayDate.getFullYear() - minimumAgeToAccept, todayDate.getMonth(), todayDate.getDate());
	var minimumDateRequired = new Date(todayDate.getFullYear() - maximumAgeToAccept, todayDate.getMonth(), todayDate.getDate());

	toastr.options.timeOut = 100000;

	if (maximumDateRequired.getTime() < rawToDate.getTime()) {
		toastr.error("Age should be greater than " + minimumAgeToAccept);
		id.value = id.defaultValue;
	}
	if (minimumDateRequired.getTime() < 0 && minimumDateRequired.getTime() > rawToDate.getTime()) {
		toastr.error("Age should be less than " + maximumAgeToAccept);
		id.value = id.defaultValue;
	} else if (minimumDateRequired.getTime() < rawToDate.getTime() && minimumDateRequired.getTime>0) {
		toastr.error("Age should be less than " + maximumAgeToAccept);
		id.value = id.defaultValue;
    }
}
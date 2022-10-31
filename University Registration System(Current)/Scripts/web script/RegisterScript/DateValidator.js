function RegistrationDateValidation(id) {

	const minimumAgeToAccept = 16;
	const maximumAgeToAccept = 100;
	var rawToDate = new Date(id.value);
	var todayDate = new Date();
	var maximumDateRequired = new Date(todayDate.getFullYear() - minimumAgeToAccept, todayDate.getMonth(), todayDate.getDate());
	var minimumDateRequired = new Date(todayDate.getFullYear() - maximumAgeToAccept, todayDate.getMonth(), todayDate.getDate());
	var maximumDateMilisecond = maximumDateRequired.getTime();
	var minimumDateMilisecond = minimumDateRequired.getTime();
	var rawDateMilisecond = rawToDate.getTime();

	toastr.options.timeOut = 100000;

	if (maximumDateMilisecond < rawDateMilisecond) {
		toastr.error("Age should be greater than " + minimumAgeToAccept);
		id.value = id.defaultValue;
	}
	if (minimumDateMilisecond < 0 && minimumDateMilisecond > rawDateMilisecond) {
		toastr.error("Age should be less than " + maximumAgeToAccept);
		id.value = id.defaultValue;
	} else if (minimumDateMilisecond < rawDateMilisecond && minimumDateMilisecond >0) {
		toastr.error("Age should be less than " + maximumAgeToAccept);
		id.value = id.defaultValue;
    }
}
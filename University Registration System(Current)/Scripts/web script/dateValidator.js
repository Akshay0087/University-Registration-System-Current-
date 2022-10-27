function dateCheck(id) {

	var input = id.value;
	const ageMin = 16;
	const ageMax = 100;
	var my_dob = new Date(input);
	var today = new Date();
	var min_dob = new Date(today.getFullYear() - ageMin, today.getMonth(), today.getDate());
	var max_dob = new Date(today.getFullYear() - ageMax, today.getMonth(), today.getDate());

	toastr.options.timeOut = 100000;

	if (min_dob.getTime() < my_dob.getTime()) {
		toastr.error("Age should be greater than " + ageMin);
		document.getElementById("dateB").value = document.getElementById("dateB").defaultValue;

	}

	if (max_dob.getTime() < 0) {
		if (max_dob.getTime() > my_dob.getTime()) {
			toastr.error("Age should be less than " + ageMax);
			document.getElementById("dateB").value = document.getElementById("dateB").defaultValue;
		}
	} else {
		if (max_dob.getTime() < my_dob.getTime()) {
			toastr.error("Age should be less than " + ageMax);
			document.getElementById("dateB").value = document.getElementById("dateB").defaultValue;
		}
	}


}
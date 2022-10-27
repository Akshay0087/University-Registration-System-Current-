function validation(id) {

	var emailRegex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
	var nidRegex = /[^ A - Za - z0 - 9]+/;

	toastr.options.timeOut = 100000;

	switch (id) {
		case "email":
			if (!emailRegex.test(document.getElementById(id).value)) {
				toastr.error("Invalid Email Format");
				document.getElementById(id).setCustomValidity("Invalid Email Format");
				document.getElementById(id).value = "";
			}
			break;
		case "password1":

			if (document.getElementById(id).value.length < 8) {
				toastr.error("Incorrect password");
				document.getElementById(id).setCustomValidity("Password character requirement should be greater than 8");
				document.getElementById(id).value = "";
			}
			break;
		case "fname":
			if (document.getElementById(id).value.length < 2 || document.getElementById(id).value.length > 20) {
				toastr.error("Incorrect firstname");
				document.getElementById(id).setCustomValidity("Firstname character requirement(2-20)");
				document.getElementById(id).value = "";
			}
			break;
		case "lname":
			if (document.getElementById(id).value.length < 2 || document.getElementById(id).value.length > 20) {
				toastr.error("Incorrect Lastname");
				document.getElementById(id).setCustomValidity("Lastname character requirement(2-20)");
				document.getElementById(id).value = "";
			}
			break;
		case "NID":
			if (!(document.getElementById(id).value.length == 14) || !nidRegex.test(document.getElementById(id).value) ){
				toastr.error("Incorrect NID");
				document.getElementById(id).setCustomValidity("NID should be of 14 characters(Numbers and letters only)NID should be of 14 characters(Numbers and letters only)");
				document.getElementById(id).value = "";
			}
			break;
		case "address":
			if (!document.getElementById(id).value.length > 2 || !document.getElementById(id).value.length<120) {
				toastr.error("Incorrect Adress");
				document.getElementById(id).setCustomValidity("Address character requirement greater than 2");
				document.getElementById(id).value = "";
			}
			break;

		default:
			break;
	}
}
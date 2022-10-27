$(function () {
	let form = document.querySelector('form');
	/*const phoneinputfield = document.queryselector("#phone");
	const phoneinput = window.intltelinput(phoneinputfield, {
		initialcountry: "mu",
		utilsscript:
			"https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/17.0.8/js/utils.js",
	});*/
	form.addEventListener('submit', (e) => {
		e.preventDefault();
		return false;
	});
});

function checkPhone() {

}

function register_button() {

	var emailAddress = $("#email").val();
	var password = $("#password1").val();
	var firstname = $("#fname").val();
	var lastname = $("#lname").val();
	var NID = $("#NID").val();
	var DOB = $("#dateB").val();
	var phone = $("#phone").val();
	var address = $("#address").val();

	toastr.options.timeOut = 100000;

	if (emailAddress == "" || password == "" || firstname == "" || lastname == "" || NID == "" || address == "") {
		toastr.error("Please fill the required fields");
	} else {

		var authObj = {
			emailAddress: emailAddress,
			passwordHash: password,
			firstname: firstname,
			lastname: lastname,
			address: address,
			phoneNum: phone,
			dob: DOB,
			NID: NID
		};

		sendData(authObj).then((response) => {
			if (!response.msg) {
				if (response.result) {
					toastr.success("Registration Successful");
					setTimeout(redirect, 3000);
				} else {
					toastr.error('Registration Failed');
					return false;
				}
			} else {
				toastr.error("Record already exist. Please Login");
				return false;
			}
		})
			.catch((error) => {
				toastr.error('An error occured. Please try again later');
				console.log(error);
			});

	}
}

function redirect() {
	window.location.href = "/login/Login";
}

function sendData(registeruser) {
	return new Promise((resolve, reject) => {
		$.ajax({
			type: "POST",
			url: "/Register/SaveUserData",
			data: registeruser,
			dataType: "json",
			success: function (data) {
				resolve(data)
			},
			error: function (error) {
				reject(error)
			}
		})
	});
}
$(function () {
	let form = document.querySelector('form');
	form.addEventListener('submit', (e) => {
		e.preventDefault();
		return false;
	});
});

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
		return false;
	} else {
		var userDataObj = {
			EmailAddress: emailAddress,
			PasswordHash: password,
			Firstname: firstname,
			Lastname: lastname,
			ResidentialAddress: address,
			PhoneNumber: phone,
			DateOfBirth: DOB,
			NationalIdentityNumber: NID
		};

		sendRegisterData(userDataObj).then((response) => {
			if (response.data == "insert") {
				if (response.status) {
					toastr.success("Registration Successful");
					setTimeout(redirectToLogin, 3000);
				} else {
					toastr.error("Registration unsuccessful");
					return false;
				}
			}
			if (response.data == "uniquenessError") {
				toastr.error("Record already exist. Please fill using unused data");
				return false;
			}
			if (response.data == "validationError") {
				var dataJson = JSON.parse(response.result);
				for (let key in dataJson) {
					var element = document.getElementById(key);
					toastr.error(dataJson[key]);
					element.value = "";

				}
				return false;
			}


		})
			.catch((error) => {
				toastr.error('An error occured. Please try again later');
				console.log(error);
			});

	}
}

function redirectToLogin() {
	window.location.href = "/login/Login";
}

function sendRegisterData(registeruser) {
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
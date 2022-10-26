$(function () {
	let form = document.querySelector('form');
	form.addEventListener('submit', (e) => {
		e.preventDefault();
		return false;
	});
});

function login_button() {
	//toastr.options.timeOut = 500000;
	var emailAddress = $("#email").val(); // read email address input
	var password = $("#pword").val(); // read password input

	if (emailAddress == "" || password == "") {
		toastr.error("Please fill the required fields");
	} else {
		// create object to map LoginModel
		var authObj = {
			emailAddress: emailAddress,
			passwordHash: password
		};

		sendData(authObj).then((response) => {
			if (response.result) {
				toastr.success("Login Success");
				setTimeout(redirect, 3000);

			} else {
				toastr.error('Login Failed');
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
	window.location.href = "/RequestInterface/Main";
}


function sendData(userlogin) {
	return new Promise((resolve, reject) => {
		$.ajax({
			type: "POST",
			url: "/Login/Authenticate",
			data: userlogin,
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
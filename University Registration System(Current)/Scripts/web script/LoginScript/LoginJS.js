$(function () {
	let form = document.querySelector('form');
	form.addEventListener('submit', (e) => {
		e.preventDefault();
		return false;
	});
});

function login_button() {
	
	var emailAddress = $("#email").val(); 
	var password = $("#pword").val(); 

	toastr.options.timeOut = 100000;

	if (emailAddress == "" || password == "") {
		toastr.error("Please fill the required fields");
	} else {
		
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
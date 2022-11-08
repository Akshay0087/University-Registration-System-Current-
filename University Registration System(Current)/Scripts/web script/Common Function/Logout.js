function Logout() {

	toastr.options.timeOut = 100000;

	LogoutUserOut(Obj = null).then((response) => {
		if (response.result) {
			toastr.success("Logout Successful");
			setTimeout(redirect, 2000);

		} else {
			toastr.error('Logout Failed');
			return false;
		}
	})
		.catch((error) => {
			toastr.error('An error occured. Please try again later');
			console.log(error);
		});

}

function redirect() {
	window.location.href = "/login/Login";
}

function LogoutUserOut(user) {
	return new Promise((resolve, reject) => {
		$.ajax({
			type: "POST",
			url: "/Login/Logout",
			data: user,
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
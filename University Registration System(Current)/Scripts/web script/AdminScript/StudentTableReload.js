$(function () {
	let form = document.querySelector('form');
	form.addEventListener('submit', (e) => {
		e.preventDefault();
		return false;
	});
});

function refresh_button() {

	toastr.options.timeOut = 100000;

	reloadStudentSubjectList(obj=null).then((response) => {
		if (response.result) {
			toastr.success("Reload Successful");
			toastr.success("New List Generated");
			setTimeout(redirect, 2000);

		} else {
			toastr.error('Reload Failed');
			return false;
		}
	})
		.catch((error) => {
			toastr.error('An error occured. Please try again later');
			console.log(error);
		});
	
}

function redirect() {
	window.location.href = "/Admin/AdminPanel";
}



function reloadStudentSubjectList(userData) {
	return new Promise((resolve, reject) => {
		$.ajax({
			type: "POST",
			url: "/Admin/GetStudentStatusList",
			data: userData,
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
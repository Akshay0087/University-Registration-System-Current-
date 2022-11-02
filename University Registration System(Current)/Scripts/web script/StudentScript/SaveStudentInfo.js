$(function () {
	let form = document.querySelector('form');
	form.addEventListener('submit', (e) => {
		e.preventDefault();
		return false;
	});
});

function registerStudentInfoButton() {

	var firstname = $("#fname").val();
	var lastname = $("#lname").val();
	var subject1 = document.getElementById("subject1").value;
	var grade1 = document.getElementById("grade1").value;
	var subject2 = document.getElementById("subject2").value;
	var grade2 = document.getElementById("grade2").value;
	var subject3 = document.getElementById("subject3").value;
	var grade3 = document.getElementById("grade3").value;

	toastr.options.timeOut = 100000;

	var authObjj = {
		subject: [{
			SubjectGrade: grade1,
			SubjectName: subject1

		}, {
			SubjectGrade: grade2,
			SubjectName: subject2

		},
		{
			SubjectGrade: grade3,
			SubjectName: subject3

		}
		],
		guardian: {
			FirstName: firstname,
			LastName: lastname
		}
	};


	var authObj = {
		
	};

	sendData(authObjj).then((response) => {
		if (response.result) {
			toastr.success("Login Successful");

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


function redirect() {
	window.location.href = "/Login/Login";
}


function sendData(userlogin) {
	return new Promise((resolve, reject) => {
		$.ajax({
			type: "POST",
			url: "/StudentInterface/SaveStudentSubjectGuardianInfo",
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
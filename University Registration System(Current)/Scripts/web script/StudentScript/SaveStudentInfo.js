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

	var subjectResult = {
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

	sendSubjectData(subjectResult).then((response) => {
		if (response.result) {
			toastr.success("Information Successfully registered");
			setTimeout(redirect, 2000);
		} else {
			toastr.error('Information saving failed.Please try again');
			setTimeout("/StudentInterface/Main", 2000);
			return false;
		}
	})
		.catch((error) => {
			toastr.error('An error occured. Please try again later');
		});

}

function redirect() {
	window.location.href = "/StudentInterface/DetailsScreen";
}

function sendSubjectData(subjectGrades) {
	return new Promise((resolve, reject) => {
		$.ajax({
			type: "POST",
			url: "/StudentInterface/SaveStudentSubjectGuardianInfo",
			data: subjectGrades,
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
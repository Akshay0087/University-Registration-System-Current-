$(function () {
	$(document).ready(function () {
		registerStudentInfoButton();
	});
});

function registerStudentInfoButton() {
	var firstname = document.getElementById("fname");
	var lastname = document.getElementById("lname");
	var subject1 = document.getElementById("subject1_detail");
	var grade1 = document.getElementById("grade1_detail");
	var subject2 = document.getElementById("subject2_detail");
	var grade2 = document.getElementById("grade2_detail");
	var subject3 = document.getElementById("subject3_detail");
	var grade3 = document.getElementById("grade3_detail");
	var status = document.getElementById("status");

	toastr.options.timeOut = 100000;

	sendData(Obj = null).then((response) => {
			if (response.result) {
				
				var obj = JSON.parse(response.resultList);
				
				firstname.textContent = obj["StudentGuardianInfo"]["FirstName"];
				lastname.textContent = obj["StudentGuardianInfo"]["LastName"];
			
				if (obj["StudentStatus"] == "P" || obj["StudentStatus"] == "'") {
					status.textContent = "Pending"
				}
				if (obj["StudentStatus"] == "A") {
					status.textContent = "Accepted"
				}
				if (obj["StudentStatus"] == "W") {
					status.textContent = "Waiting"
				}
				if (obj["StudentStatus"] == "R") {
					status.textContent = "Rejected"
				}
				for (var i = 0; i < obj["Subjects"].length; ++i) {
					if (i == 0) {
						subject1.textContent = obj["Subjects"][i].SubjectName;
						grade1.textContent = obj["Subjects"][i].SubjectGrade;
					}
					if (i == 1) {
						subject2.textContent = obj["Subjects"][i].SubjectName;
						grade2.textContent = obj["Subjects"][i].SubjectGrade;
					}
					if (i == 2) {
						subject3.textContent = obj["Subjects"][i].SubjectName;
						grade3.textContent = obj["Subjects"][i].SubjectGrade;
					}
				}

			} else {
				toastr.error("DB issue. Please reload the pages");
				return false;
			}
		
	})
		.catch((error) => {
			toastr.error('An error occured. Please try again later');
			console.log(error);
		});


}


function sendData(userlogin) {
	return new Promise((resolve, reject) => {
		$.ajax({
			type: "POST",
			url: "/StudentInterface/GetStudentStudentSubjectList",
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
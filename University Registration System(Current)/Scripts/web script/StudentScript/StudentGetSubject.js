var subjectArray = [];
var gradeArray = [];
$(function () {
	$(document).ready(function () {
		SubjectListArray("subject1");
		GradeListArray("grade1");
		SubjectListArray("subject2");
		GradeListArray("grade2");
		SubjectListArray("subject3");
		GradeListArray("grade3");
	});
});

function SubjectListArray(element) {
	LoadSubjectData(obj = null).then((response) => {
		if (response.subjectList) {
			subjectArray = JSON.parse(response.subjectList);

			var select = document.getElementById(element);
			for (var i = 0; i < subjectArray.length; i++) {
				let option = document.createElement("option");
				option.setAttribute('value', subjectArray[i]);

				let optionText = document.createTextNode(subjectArray[i]);
				option.appendChild(optionText);

				select.appendChild(option);
			}
		} else {
			return false;
		}
	})
		.catch((error) => {
			toastr.error('An error occured. Please try again later');
			console.log(error);
		});
}

function LoadSubjectData(subjects) {
	return new Promise((resolve, reject) => {
		$.ajax({
			type: "POST",
			url: "/StudentInterface/GetSubjectList",
			data: subjects,
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

function GradeListArray(element) {
	LoadGradeData(obj = null).then((response) => {
		if (response.subjectList) {
			gradeArray = JSON.parse(response.subjectList);

			var select = document.getElementById(element);
			for (var i = 0; i < gradeArray.length; i++) {
				let option = document.createElement("option");
				option.setAttribute('value', gradeArray[i]);

				let optionText = document.createTextNode(gradeArray[i]);
				option.appendChild(optionText);

				select.appendChild(option);
			}
		} else {
			return false;
		}
	})
		.catch((error) => {
			toastr.error('An error occured. Please try again later');
			console.log(error);
		});
}

function LoadGradeData(grades) {
	return new Promise((resolve, reject) => {
		$.ajax({
			type: "POST",
			url: "/StudentInterface/GetGradeList",
			data: grades,
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
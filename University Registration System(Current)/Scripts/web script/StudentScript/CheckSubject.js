function subjectRepetitionCheck() {

	toastr.options.timeOut = 100000;

	if (document.getElementById("subject1").value == document.getElementById("subject2").value && document.getElementById("subject1").value != "") {
		toastr.error("Subject field cant be same");
		document.getElementById("subject1").value = "";
		document.getElementById("subject2").value = "";
		document.getElementById("grade1").value = "";
		document.getElementById("grade2").value = "";
	}
	if (document.getElementById("subject1").value == document.getElementById("subject3").value && document.getElementById("subject1").value != "") {
		toastr.error("Subject field cant be same");
		document.getElementById("subject1").value = "";
		document.getElementById("subject3").value = "";
		document.getElementById("grade1").value = "";
		document.getElementById("grade3").value = "";
	}
	if (document.getElementById("subject2").value == document.getElementById("subject3").value && document.getElementById("subject2").value != "") {
		toastr.error("Subject field cant be same");
		document.getElementById("subject2").value = "";
		document.getElementById("subject3").value = "";
		document.getElementById("grade2").value = "";
		document.getElementById("grade3").value = "";
	}
}

function gradeCheck() {

	toastr.options.timeOut = 100000;

	if (document.getElementById("subject1").value == "" && document.getElementById("grade1").value != "") {
		toastr.error("Please set subject before grade");
		document.getElementById("grade1").value = "";
	}
	if (document.getElementById("subject2").value == "" && document.getElementById("grade2").value != "") {
		toastr.error("Please set subject before grade");
		document.getElementById("grade2").value = "";
	}
	if (document.getElementById("subject3").value == "" && document.getElementById("grade3").value != "") {
		toastr.error("Please set subject before grade");
		document.getElementById("grade3").value = "";
	}
}
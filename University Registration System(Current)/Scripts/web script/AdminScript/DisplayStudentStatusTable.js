$(document).ready(function () {
	DisplayTable();
});
function DisplayTable() {

	toastr.options.timeOut = 100000;
	let placeholder = document.querySelector("#data-output");
	let out = "";

	getStudentStatusList().then((response) => {
		if (response.status) {

			out += `<tr id="approvedRow"><th colspan="4">Accepted Students</th></tr>`;
			for (let approved of response.approvedList) {
				out += `
						 <tr id="approvedRow">
							<td>${approved.StudentId}</td>
							<td>${approved.StudentFullName}</td>
							<td>${approved.TotalSubjectPoints}</td>
							<td>Accepted</td>
						 </tr>
						`;
			}
			out += `<tr id="waitingRow"><th colspan="4">Waiting Students</th></tr>`;
			for (let waiting of response.waitingList) {
				out += `
						 <tr id="waitingRow">
							<td>${waiting.StudentId}</td>
							<td>${waiting.StudentFullName}</td>
							<td>${waiting.TotalSubjectPoints}</td>
							<td>Waiting</td>
						 </tr>
						`;
			}
			out += `<tr id="rejectedRow" ><th colspan="4">Rejected Students</th></tr>`;
			for (let rejected of response.rejectedList) {
				out += `
						 <tr id="rejectedRow">
							<td>${rejected.StudentId}</td>
							<td>${rejected.StudentFullName}</td>
							<td>${rejected.TotalSubjectPoints}</td>
							<td>Rejected</td>
						 </tr>
						`;
			}
			placeholder.innerHTML = out;
		} else {
			toastr.error('Could not reload. Please try again later');
			return false;
		}
	}).catch((error) => {
		toastr.error('An error occured. Please try again later');
		console.log(error);
	});
}


function getStudentStatusList(userData) {
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
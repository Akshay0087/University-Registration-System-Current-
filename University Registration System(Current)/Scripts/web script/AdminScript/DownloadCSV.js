
function DownloadCsvFile() {

	toastr.options.timeOut = 100000;
	let placeholder = document.querySelector("#data-output");
	let out = "";


	GenerateCsvFile().then((response) => {
		if (response.status) {
			var approvedList=[];
			var waitingList=[];
			var rejectedList=[];

			for (let approved of response.approvedList) {
				approvedList.push(approved.StudentFullName);
			}
			for (let waiting of response.waitingList) {
				waitingList.push(waiting.StudentFullName);
			}
			for (let rejected of response.rejectedList) {
				rejectedList.push(rejected.StudentFullName);
			}
			var rows = _.zip(approvedList, waitingList, rejectedList);
			var heading = ["Approved Student", "Waiting Student", "Rejected Student"];  

			var content = "data:text/csv;charset=utf-8,";

			var dataRows = rows.map(function (columnValues, index) {
				return columnValues.join(",");
			});

			content += heading + "\n" + dataRows.join("\n");

			var encodedUri = encodeURI(content);
			var link = document.createElement("a");
			link.setAttribute("href", encodedUri);
			link.setAttribute("download", "Student_Status_Summary.csv");
			document.body.appendChild(link); 

			link.click();

		} else {
			toastr.error('Login Failed');
			return false;
		}
	}).catch((error) => {
		toastr.error('An error occured. Please try again later');
		console.log(error);
	});
}


function GenerateCsvFile(userData) {
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
var subjectList = [];
 
function loadData() {
    $.ajax({
        url: "/StudentInterface/GetSubjectList",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
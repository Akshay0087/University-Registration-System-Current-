
function session_role_check() { 

        sendData().then((response) => {
            if (response.flag) {
                document.write('<ul></><li><a class="active" href="#home">Home</a></li>');
                if (response.role == "Student") {
                    document.write('<li><a href="/RequestInterface/StudentForm">Details</a></li>');
                } else if (response.role == "Admin") {
                    document.write('<li><a href="/RequestInterface/AdminPanel">Admin Option</a></li>');
                }
                document.write('<li style="float:right">>Logout</a></li></ul>');
                
                
            }
            else {
                toastr.error('Session Closed. Login again');
                window.location = window.location = Url.Action("Login", "Login");
                return false;
            }
        })
            .catch((error) => {
                toastr.error('An error occured. Please try again later');
                window.location = window.location = Url.Action("Login", "Login");
                console.log(error);
            });
    }


function sendData() {
    return new Promise((resolve, reject) => {
        $.ajax({
            type: "GET",
            url: "/RequestInterface/GetSessionRole",
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
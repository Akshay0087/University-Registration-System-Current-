$(function () {
    let form = document.querySelector('form');
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });
});



function register_button() {
    var emailAddress = $("#email").val(); 
    var password = $("#password1").val();
    var firstname = $("#fname").val();
    var lastname = $("#lname").val();
    var NID = $("#NID").val();
    var DOB = $("#dateB").val();
    var phone = $("#phone").val();
    var address = $("#address").val();

    if (emailAddress == "" || password == "" || firstname == "" || lastname == "" || NID == ""|| address == "") {
        toastr.error("Please fill the required fields");
    } else {
        // create object to map LoginModel
        var authObj = { emailAddress: emailAddress, passwordHash: password, firstname: firstname, lastname: lastname, address: address, phoneNum: phone, dob: DOB,NID:NID };

        sendData(authObj).then((response) => {
            if (response.result) {
                toastr.success("Login Success");
                window.location = response.url;
            }
            else {
                toastr.error('Login Failed');
                return false;
            }
        })
            .catch((error) => {
                toastr.error('An error occured. Please try again later');
                console.log(error);
            });
    }
}

function sendData(registeruser) {
    return new Promise((resolve, reject) => {
        $.ajax({
            type: "POST",
            url: "/Register/SaveUserData",
            data: registeruser,
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
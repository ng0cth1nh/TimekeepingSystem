window.onload = function () {
    loadTableEmployee();
    // submitAttendance();
}

function loadTableEmployee() {
    $.ajax({
        url: '/RecordAttendance/LoadDataEmployee',
        type: 'GET',
        dataType: 'json',
        success: function (response) {
            if (response.status) {
                var data = response.data;
                var html = '';
                var template = $('#model-employee').html();
                $.each(data, function (i, item) {
                    if ((i + 1) % 2 === 0) {
                        html += Mustache.render(template, {
                            ID: item.ID,
                            Name: item.Name,
                            Role: item.Role,
                            Type: item.Type,
                            Class: "even",
                        });
                    } else {
                        html += Mustache.render(template, {
                            ID: item.ID,
                            Name: item.Name,
                            Role: item.Role,
                            Type: item.Type,
                            Class: "odd",
                        });
                    }

                });
                $('#tbEmployees').html(html);
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
}


function submitAttendance (){
    let employees = $(".employee");
    let arr = [];
    for (let i = 0; i < employees.length; i++) {
        let id = $(`.employee:eq(${i}) td:eq(0)`).text();
        let ispresent = $(`input[name=${id}]:checked`).val();
        let note = $(`input[name=absentNode${id}]`).val();
        let record = {
            EmployeeID: id,
            IsPresent: ispresent,
            Note: note
        }
        arr.push(record);
    }

    $.ajax({
        url: '/RecordAttendance/SaveRecordAttendance',
        data: {
            record: JSON.stringify(arr)
        },
        type: 'POST',
        dataType: 'json',
        success: function (response) {
            if (response.status == true) {
                $("#btnSubmit").text("Update");
                alert("Submit Successfully!");   
            } else {
                alert(response.message);
            }
        },
        error: function (err) {
            console.log(err);
        }
    });

}
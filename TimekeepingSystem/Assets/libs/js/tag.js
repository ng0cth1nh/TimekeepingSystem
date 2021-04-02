$(document).ready(function () {
    console.log("Loaded");
    init();
});

$("#btnSearch").on("click", function () {
    $.ajax({
        url: '/AddTag/LoadTag',
        type: 'GET',
        data: {
            id: $("#tag-input").val()
        },
        dataType: 'json',
        success: function (response) {
            if (response.status) {
                var data = response.data;
                var html1 = '';
                var html2 = '';
                var templateModelTag = $('#model-tag').html();
                var templateModelTagHeader = $('#model-tag-header').html();
                $.each(data, function (i, item) {
                    if (i === 0) {
                        html1 += Mustache.render(templateModelTagHeader, {
                            ProductID: item.ProductID,
                            Color: item.Color,
                            Size: item.Size,
                            Quantity: item.Quantity
                        });
                    }
                    html2 += Mustache.render(templateModelTag, {
                        Name: item.Step,
                        ID: item.StepID
                    });
                });
                $('#Tag-Header').html(html1);
                $('#tbTag').html(html2);
                init();
                update();
            }
        }
    });
});

function update() {
    $("#complete").on("change paste keyup", function () {
        if ($("#complete").val() !== "") {
            $("#error").val($("#quantity").text() - $("#complete").val());
        } else {
            $("#error").val("");
        }
    });
}



function init() {
    console.log("init");
    $(".step").on("change paste keyup", function () {
        let item = $(this);
        if (item.val() !== "") {
            $.ajax({
                url: '/AddTag/LoadEmployee',
                type: 'GET',
                data: {
                    id: item.val()
                },
                dataType: 'json',
                success: function (response) {
                    if (response.status) {
                        var data = response.data;
                        console.log(data.Name);
                        item.parents("td").next().text(data.Name);
                    } else {
                        item.parents("td").next().text("");
                    }
                }
            })
        } else {
            item.parents("td").next().text("");
        }
    });
}

$("#btnSubmit").on("click", submitTag);

function submitTag() {
    let employees = $(".employee");
    let arr = [];
    for (let i = 0; i < employees.length; i++) {
        let id = parseInt($(`.employee:eq(${i}) td:eq(0)`).text(), 10);
        let present = $(`input[name=${id}]:checked`).val();
        let ispresent = false;
        if (present == "1") {
            ispresent = true;
        }
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
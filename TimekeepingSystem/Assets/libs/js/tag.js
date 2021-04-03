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
    // console.log("init");
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

$("#btnSubmit").on("click", function () {
    submitTag();
});

function submitTag() {

    let completeTag = {
        TagID: parseInt($("#tag-input").val(), 10),
        Table: $("#table").val(),
        ProductID: parseInt($("#productID").text(), 10),
        Quantity: parseInt($("#quantity").text(), 10),
        CompleteQuantity: parseInt($("#complete").val(), 10)
    }

    let steps = $(".step");
    let arr = [];

    for (let i = 0; i < steps.length; i++) {
        let completeTagDetail = {
            TagID: parseInt($("#tag-input").val(), 10),
            StepID: parseInt($(`.step:eq(${i})`).parent("td").prev().text(), 10),
            EmployeeID: parseInt($(`.step:eq(${i})`).val(), 10)
        }
        arr.push(completeTagDetail);
    }

    $.ajax({
        url: '/AddTag/SaveTag',
        data: {
            completeTagDetails: JSON.stringify(arr),
            completeTags: JSON.stringify(completeTag)
        },
        type: 'POST',
        dataType: 'json',
        success: function (response) {
            if (response.status == true) {
                alert("Submit Successfully!");
                // return true;
            } else {
                alert(response.message);
            }
        },
        error: function (err) {
            console.log(err);
            //return false;
        }
    });

}

//function submitCompleteTag() {
//    let completeTag = {
//        TagID: parseInt($("#tag-input").val(), 10),
//        Table: $("#table").val(),
//        ProductID: parseInt($("#productID").text(), 10),
//        Quantity: parseInt($("#quantity").text(), 10),
//        CompleteQuantity: parseInt($("#complete").val(), 10)
//    }

//    $.ajax({
//        url: '/AddTag/SaveCompleteTag',
//        data: {
//            record: JSON.stringify(completeTag)
//        },
//        type: 'POST',
//        dataType: 'json',
//        success: function (response) {
//            if (response.status == true) {
//                alert("dome");
//                return true;
//            } else {
//                alert(response.message);
//            }
//        },
//        error: function (err) {
//            console.log(err);
//            return false;
//        }
//    });

//}
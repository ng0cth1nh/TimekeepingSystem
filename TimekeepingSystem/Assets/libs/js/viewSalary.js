$("#month-year").text(new Date().getMonth() + 1 + "/" + new Date().getFullYear());

function update() {
    let a = $("#month-year").text().split("/");

    $.ajax({
        url: '/ViewSalary/LoadData',
        type: 'GET',
        data: {
            month: a[0],
            year: a[1]
        },
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


$("#btnNext").on('click', function () {

    let a = $("#month-year").text().split("/");

    $("#month-year").text(parseInt(a[0]) + 1 + "/" + new Date().getFullYear());

    update();


});

$("#btnPrev").on('click', function () {

    let a = $("#month-year").text().split("/");

    $("#month-year").text(parseInt(a[0]) - 1 + "/" + new Date().getFullYear());

    update();


})

update();
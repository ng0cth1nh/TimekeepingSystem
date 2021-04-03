

/* STUDENTS IGNORE THIS FUNCTION
 * All this does is create an initial
 * attendance record if one is not found
 * within localStorage.
 */
(function () {
    if (!localStorage.attendance) {
        console.log('Creating attendance records...');
        function getRandom() {
            return (Math.random() >= 0.5);
        }

        var nameColumns = $('tbody .name-col'),
            attendance = {};

        nameColumns.each(function () {
            var name = this.innerText;
            attendance[name] = [];

            for (var i = 0; i <= 11; i++) {
                attendance[name].push(getRandom());
            }
        });

        localStorage.attendance = JSON.stringify(attendance);
    }
}());


/* STUDENT APPLICATION */
$(function () {
    var attendance = JSON.parse(localStorage.attendance),
        $allMissed = $('tbody .missed-col'),
        $allCheckboxes = $('tbody input');

    // Count a student's missed days
    function countMissing() {
        $allMissed.each(function () {
            var studentRow = $(this).parent('tr'),
                dayChecks = $(studentRow).children('td').children('input'),
                numMissed = 0;

            dayChecks.each(function () {
                if (!$(this).prop('checked')) {
                    numMissed++;
                }
            });

            $(this).text(numMissed);
        });
    }

    // Check boxes, based on attendace records
    $.each(attendance, function (name, days) {
        var studentRow = $('tbody .name-col:contains("' + name + '")').parent('tr'),
            dayChecks = $(studentRow).children('.attend-col').children('input');

        dayChecks.each(function (i) {
            $(this).prop('checked', days[i]);
        });
    });

    // When a checkbox is clicked, update localStorage
    $allCheckboxes.on('click', function () {
        var studentRows = $('tbody .student'),
            newAttendance = {};

        studentRows.each(function () {
            var name = $(this).children('.name-col').text(),
                $allCheckboxes = $(this).children('td').children('input');

            newAttendance[name] = [];

            $allCheckboxes.each(function () {
                newAttendance[name].push($(this).prop('checked'));
            });
        });

        countMissing();
        localStorage.attendance = JSON.stringify(newAttendance);
    });

    countMissing();
}());






$("#month-year").text(new Date().getMonth() + 1 + "/" + new Date().getFullYear());

function update() {
    let a = $("#month-year").text().split("/");

    let FirstDay = new Date(parseInt(a[1]), parseInt(a[0]) - 1, 1);
    let LastDay = new Date(parseInt(a[1]), parseInt(a[0]), 0);

    let html1 = `<tr><th class="name-col">Employee Name</th>`;
    var templateModelDates = $('#model-dates').html();
    for (let i = FirstDay.getDate(); i <= LastDay.getDate(); i++) {
        html1 += Mustache.render(templateModelDates, {
            day: i
        });
    }
    html1 += `<th class="missed-col">Absent percent</th></tr>`;

    $('#thead').html(html1);




    $.ajax({
        url: '/ViewAttendance/LoadData',
        type: 'GET',
        data: {
            month: a[0],
            year: a[1]
        },
        dataType: 'json',
        success: function (response) {
            if (response.status) {
                let data = response.data;
                let rs = "";

                let id = -1;

                let tempHtml = '';
                let count = FirstDay.getDate();

                let temp = 0;
                let ab = 0;

                $.each(data, function (i, item) {

                    if (item.id !== id) {
                        if (i !== 0) {
                            for (let j = count; j <= LastDay.getDate(); j++) {

                                tempHtml += `<td class="attend-col">na</td>`;

                            }
                            rs += tempHtml + `<td class="missed - col">${(ab / temp)*100}%</td></tr>`;
                        }



                        ab = 0;
                        temp = 0;
                        tempHtml = "";
                        count = FirstDay.getDate();


                        let html2 = `<tr class="student">`;
                        html2 += `<td class="name-col">${item.name}</td>`;

                        for (let j = count; j <= item.day; j++) {
                            if (j === item.day) {
                                if (item.isPresent) {
                                    ++temp;

                                    html2 += `<td class="attend-col present"></td>`;
                                } else {
                                    ++ab;
                                    ++temp;
                                    html2 += `<td class="attend-col absent"></td>`;
                                }
                            } else {
                                html2 += `<td class="attend-col">na</td>`;
                            }
                        }

                        count = item.day + 1;

                        tempHtml = html2;
                    } else {
                        for (let j = count; j <= item.day; j++) {
                            if (j === item.day) {
                                if (item.isPresent) {
                                    ++temp;
                                    tempHtml += `<td class="attend-col present"></td>`;
                                } else {
                                    ++ab;
                                    ++temp;
                                    tempHtml += `<td class="attend-col absent"></td>`;
                                }
                            } else {
                                html2 += `<td class="attend-col">na</td>`;
                            }
                        }

                        count = item.day + 1;
                    }

                    id = item.id;

                });

                $('#attend').html(rs);

            }
        }
    });
}

update();

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
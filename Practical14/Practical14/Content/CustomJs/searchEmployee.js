$(document).ready(function () {
    $("#btnSearch").on("click", function () {
        var searchedName = $("#txtSearch").val();
        $.ajax({
            url: "/employee/GetEmployees",
            method: "GET",
            data: {
                name: searchedName
            },
            success: function (data) {                
                data = JSON.parse(data);
                console.log(data);

                var html = "";
                $.each(data, (index, item) => {

                    html += "<tr><td>" + item.Name + "</td><td>" + item.DOB.split("T")[0] + "</td><td>" + item.Age + "</td><td>" +
                        " <a href='/Employee/Edit/" + item.Id + "' class='btn btn-primary'>Edit</a> " +
                        " <a href='/Employee/Details/" + item.Id + "' class='btn btn-primary'>Details</a> " +
                        " <a href='/Employee/Delete/" + item.Id + "' class='btn btn-danger'>Delete</a> " + "</td></tr>";
                });

                $("#tblBody").html(html);
            },
            error: function (err) {
                console.log(err);
            }
        });
    });
});
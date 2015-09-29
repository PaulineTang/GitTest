
var uri = 'api/workers';
//页面加载完成后，显示所有员工信息
$(document).ready(tyzShow());

function tyzShow()
{ 
        // Send an AJAX request(使用get也可以)
        $.getJSON(uri)
            .done(function (data) {
                // On success, 'data' contains a list of products.
                // Add a list item for the product.
                //var table=$("<table border=\"1\">");
                //table.appendTo($("#AllWorkers"));
                var tr = $("<tr></tr>");
                //tr.appendTo(table);
                tr.appendTo($("#AllWorkers"));
                var td = $("<th>Id</th>" + "<th>Name</th>" + "<th>Gender</th>" + "<th>Age</th>" + "<th>Department</th>" + "<th>Position </th>" + "<th>Employed Date</th>");
                td.appendTo(tr);
                $.each(data, function (key, item) {
                    var tr = $("<tr  id="+item.Id+"></tr>");//定义id为变量？？？？？？？
                    tr.appendTo($("#AllWorkers"));
                    var td = $("<td>" + item.Id + "</td>" + "<td>" + item.Name + "</td>" + "<td>" + item.Gender + "</td>" + "<td>" + item.Age + "</td>" + "<td>" + item.Department + "</td>" + "<td>" + item.Position + "</td>" + "<td>" + item.EmployedDate + "</td>");
                    td.appendTo(tr);
                });
            });
}


//根据Id查询员工信息
function find() {
    $("#SearchResult").empty();
    var id = $('#workerId').val();
    $.getJSON(uri + '/' + id)
        .done(function (data) {
            var str = $("<P>" + "ID: " + data.Id + "</P>" + "<P>" + "Name: " + data.Name + "</P>" + "<P>" + "Gender: " + data.Gender + "</P>" + "<P>" + "Age: " + data.Age + "</P>" + "<P>" + "Department: " + data.Department + "</P>" + "<P>" + "Position: " + data.Position + "</P>" + "<P>" + "EmployedDate: " + data.EmployedDate + "</P>");
            $(str).appendTo($("#SearchResult"));
            //弹出小窗口，咨询删除（delete）or更新（update）or返回（无操作）
            var btn = $("<input type='button' value='Delete' onclick='deleteworker()' />");
            $(btn).appendTo($("#SearchResult"));
        })
        .fail(function (jqXHR, textStatus, err) {
            $('#SearchResult').text('Error: ' + err);
        });
}
//根据用户输入信息，添加员工信息
function addworker() {
    var id = $('#AddContent input[name="AddId"]').val();
    var name = $('#AddContent input[name="AddName"]').val();
    var gender = $('#AddContent input[name="AddSex"]:checked ').val();
    var age = $('#AddContent input[name="AddAge"]').val();
    var department = $('#AddContent select[name="AddDepartment"]').find("option:selected").val();
    var position = $('#AddContent select[name="AddPosition"]').find("option:selected").val();
    var employeddate = $('#AddContent input[name="AddEmployedDate"]').val();
    //var item = id + ' ' + name + ' ' + gender + ' ' + age + ' ' + department + ' ' + position + ' ' + employeddate;
    //alert("添加员工信息：" + item + "\n");//使用confirm
    confirm_ = confirm('This action will add one new worker informatiom!'+"\n"+ 'Are you sure?');
    if (confirm_) {
        var workerData = '{"Id":"' + id + '","Name":"' + name + '","Gender":"' + gender + '","Age":"' + age + '","Department":"' + department + '","Position":"' + position + '","EmployedDate":"' + employeddate + '"}';
        //var workerData = {
        //    "Id": id,
        //    "Name": name,
        //    "Gender": gender,
        //    "Age": age,
        //    "Department": department,
        //    "Position": position,
        //    "EmployedDate": employeddate
        //};

        $.ajax({
            type: 'POST',
            url: uri,
            contentType: "application/json; charset=utf-8",
            dataType: 'JSON',
            data: workerData,
            success: function () {
                var tr = $("<tr></tr>");
                tr.appendTo($("#AllWorkers"));
                var td = $("<td>" + id + "</td>" + "<td>" + name + "</td>" + "<td>" + gender + "</td>" + "<td>" + age + "</td>" + "<td>" + department + "</td>" + "<td>" + position + "</td>" + "<td>" + employeddate + "</td>");
                td.appendTo(tr);
                alert("Add success!");
            }
        });
    }
}
//根据查询Id删除对应的员工信息
function deleteworker() { 
    var id = $('#workerId').val();
    confirm_ = confirm('This action will delete the current worker informatiom!'+"\n"+ 'Are you sure?');
    if (confirm_) {
        $.ajax({
            type: 'DELETE',
            url: uri + '/' + id,
            success: function (result) {
                //动态刷新：删除表格的一行
                $("#AllWorkers").find("#" + id).remove();
                //刷新整个列表
                //$("#AllWorkers").empty();
                //tyzShow();
                alert("Delete success!");
            }
        });
    }  
}
//根据id更新员工数据
function updateworker() {
    var id = $('#AddContent input[name="AddId"]').val();
    var name = $('#AddContent input[name="AddName"]').val();
    var gender = $('#AddContent input[name="AddSex"]:checked ').val();
    var age = $('#AddContent input[name="AddAge"]').val();
    var department = $('#AddContent select[name="AddDepartment"]').find("option:selected").val();
    var position = $('#AddContent select[name="AddPosition"]').find("option:selected").val();
    var employeddate = $('#AddContent input[name="AddEmployedDate"]').val();
    confirm_ = confirm('This action will add one new worker informatiom!' + "\n" + 'Are you sure?');
    if (confirm_) {
        var workerData = '{"Id":"' + id + '","Name":"' + name + '","Gender":"' + gender + '","Age":"' + age + '","Department":"' + department + '","Position":"' + position + '","EmployedDate":"' + employeddate + '"}';
        $.ajax({
            type: 'PUT',
            url: uri + '/' + id,
            contentType: "application/json; charset=utf-8",
            dataType: 'JSON',
            data: workerData,
            success: function () {
                $("#AllWorkers").empty();
                tyzShow();
                alert("Update success!");
            }
        });
    }
}
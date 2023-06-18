// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/Home/Data",
        dataType: "json",
        success: function (data) {
            var table = $('#home_table')
            table.dataTable({
                data: data,
                pageLength: 10,
                columns: [
                    { 'data': 'SalesOrder' },
                    { 'data': 'SalesOrderItem' },
                    { 'data': 'WorkOrder' },
                    { 'data': 'ProductID' },
                    { 'data': 'ProductDes' },
                    { 'data': 'OrderQty' },
                    { 'data': 'OrderStatus' },
                    { 'data': 'Timestamp' },
                    ]
                })
        }
    })
})

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
                    {
                        'data': 'Timestamp',
                        render: function (data) {
                            return formatDatetime(data);
                        }
                    }
                    ]
                })
        }
    })
})

setTimeout(function () {
    $('.alert').alert('close');
}, 5000);

async function findOrder(item) {
    var sales_order = $('#SalesOrder_Find').val();
    var sales_order_item = $('#SalesOrderItem_Find').val();
    console.log(sales_order);
    console.log(sales_order_item);
    $.ajax({
        type: "POST",
        url: "/Home/Find",
        dataType:'json',
        data: {
            SalesOrder: sales_order,
            SalesOrderItem: sales_order_item
        },
        success: function (data) {
            $('#SalesOrder').val(data.SalesOrder)
            $('#SalesOrderItem').val(data.SalesOrderItem)
            $('#WorkOrder').val(data.WorkOrder)
            $('#OrderStatus').val(data.OrderStatus)
            $('#ProductID').val(data.ProductID)
            $('#ProductDes').val(data.ProductDes)
            $('#OrderQty').val(data.OrderQty)
            $('#Timestamp').val(data.Timestamp)

            if ($('#not_found_div').hasClass('d-none') == false) {
                $('#not_found_div').addClass('d-none')
            }
            $('#delete_info_div').removeClass('d-none')
        },
        error: function (result) {
            if ($('#delete_info_div').hasClass('d-none') == false) {
                $('#delete_info_div').addClass('d-none')
            }
            $('#not_found_div').removeClass('d-none')
        }
    })
}

function formatDatetime(datetime) {
    var date = new Date(datetime)

    var dd = date.getDate();
    var MM = date.getMonth();
    var yyyy = date.getFullYear();
    var HH = date.getHours();
    var mm = date.getMinutes();

    return MM + "/" + dd + "/" + yyyy + " " + HH + ":" + mm;
}

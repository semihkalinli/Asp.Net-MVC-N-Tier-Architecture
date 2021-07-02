$(document).ready(function () {
    $('#table').DataTable({
        "processing": true,
        "filter": true,
        "serverSide": true,
        "ajax": {
            "url": "/urunler",
            "type": "POST",
            "data": {
                "catId": catId,
                "subId": subcatId,
            }
        },
        "columns": [
            { "data": "Id", "name": "Id", "autoWidth": true },
            { "data": "Name", "name": "Name", "autoWidth": true },
            {
                "data": function (o) {
                    console.log(o)
                    return o.Price.Value
                }, "name": "Price", "autoWidth": true
            },
            { "data": "KdvRate", "name": "KdvRate", "autoWidth": true },
            {
                "data": function (o) {
                    return "<a href='/urun-duzenle?id=" + o.Id + "' class='btn btn-primary'>Düzenle</button>";
                }, "name": "Price", "autoWidth": true
            },
        ],
        lengthMenu: [[10, 25, 100, 1000], [10, 25, 100, "All"]],
        pageLength: 10,
        "order": [[0, "desc"]],     //Orderby desc column
        dom: 'Bfltipr',
        buttons: [
            {
                extend: 'copyHtml5',
                text: '<i class="fa fa-files-o"></i> Copy',
                titleAttr: 'Copy'
            },
            {
                extend: 'excelHtml5',
                text: '<i class="fa fa-file-excel-o"></i> Excel',
                titleAttr: 'Excel'
            },
            {
                extend: 'csvHtml5',
                text: '<i class="fa fa-file-text-o"></i> CSV',
                titleAttr: 'CSV'
            },
            {
                extend: 'pdfHtml5',
                text: '<i class="fa fa-file-pdf-o"></i> PDF',
                titleAttr: 'PDF'
            },
            {
                extend: 'print',
                text: '<i class="fa fa-print"></i> Print',
                titleAttr: 'Print',
                customize: function (win) {
                    $(win.document.body).addClass('white-bg');
                    $(win.document.body).css('font-size', '10px');
                    $(win.document.body).find('table')
                        .addClass('compact')
                        .css('font-size', 'inherit');
                }
            }
        ],

    });
});
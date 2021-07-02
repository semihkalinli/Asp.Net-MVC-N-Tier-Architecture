$(document).ready(function () {
    $('#table').DataTable({
        "processing": true,
        "filter": true,
        "serverSide": true,
        "ajax": {
            "url": "/kategoriler",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "Id", "name": "Id", "autoWidth": true },
            { "data": "Name", "name": "Name", "autoWidth": true },
            { "data": "Description", "name": "Description", "autoWidth": true },
            {
                "data": function (o) {
                    return "<a href='/kategori-duzenle?id=" + o.Id + "' class='btn btn-primary'>Düzenle</a> <a href='/alt-kategoriler?id=" + o.Id + "&categoryName=" + o.Name + "' class='btn btn-danger'>Alt Kategoriler</a> <a href='/urunler?catId=" + o.Id +"' class='btn btn-success'>Ürünler</a>";
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
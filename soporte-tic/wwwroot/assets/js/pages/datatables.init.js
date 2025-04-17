/*
Template Name: Samply - Admin & Dashboard Template
Author: Pichforest
Website: https://Pichforest.com/
Contact: Pichforest@gmail.com
File: Datatables Js File
*/

$(document).ready(function() {
    $('#datatable').DataTable();

    //Buttons examples
    var table = $('#datatable-buttons').DataTable({
        lengthChange: false,
        buttons: ['copy', 'excel', 'pdf', 'colvis']
    });

    table.buttons().container()
        .appendTo('#datatable-buttons_wrapper .col-md-6:eq(0)');

    $(".dataTables_length select").addClass('form-select form-select-sm');


    // Multi Selection Datatable
    $('#selection-datatable').DataTable({
        select: {
            style: 'multi'
        },
    });

    // Key Datatable
    $('#key-datatable').DataTable({
        keys: true
    });

    table.buttons().container()
        .appendTo('#datatable-buttons_wrapper .col-md-6:eq(0)');

    $(".dataTables_length select").addClass('form-select form-select-sm');


    // Alternative Pagination Datatable
    $('#alternative-page-datatable').DataTable({
        "pagingType": "full_numbers"
    });

    $(".dataTables_length select").addClass('form-select form-select-sm');

    // Scroll Vertical Datatable
    $('#scroll-vertical-datatable').DataTable({
        "scrollY":        "350px",
        "scrollCollapse": true,
        "paging":         false
    });

    $(".dataTables_length select").addClass('form-select form-select-sm');

    // Scroll horizontal Datatable
    $('#scroll-horizontal-datatable').DataTable({
        "scrollX": true
    });

    $(".dataTables_length select").addClass('form-select form-select-sm');

    // Complex headers with column visibility Datatable
    $('#complex-header-datatable').DataTable({
        "columnDefs": [ {
            "visible": false,
            "targets": -1
        } ]
    });

    $(".dataTables_length select").addClass('form-select form-select-sm');

    // Row created callback Datatable
    $('#row-callback-datatable').DataTable({
        "createdRow": function ( row, data, index ) {
            if ( data[5].replace(/[\$,]/g, '') * 1 > 150000 ) {
                $('td', row).eq(5).addClass('text-danger');
            }
        }
    });

    $(".dataTables_length select").addClass('form-select form-select-sm');

    // State saving Datatable
    $('#state-saving-datatable').DataTable({
        stateSave: true
    });

    $(".dataTables_length select").addClass('form-select form-select-sm');



} );
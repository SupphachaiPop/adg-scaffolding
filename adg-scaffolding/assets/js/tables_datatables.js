$(function() {
    $('.datatables-demo').dataTable({
        
    });

    $('#category-list').dataTable({
        deferRender: true,
        order: [[0, "desc"]],
        destroy: true,
        iDisplayLength: 50,
    });

    $('#brand-list').dataTable({
        deferRender: true,
        order: [[0, "asc"]],
        destroy: true,
        iDisplayLength: 50,
    });

});





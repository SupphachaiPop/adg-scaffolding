$(function() {
    $('.datatables-demo').dataTable({

    });


    $('#tblRoleMenu').dataTable({
        deferRender: true,
        order: [[0, "desc"]],
        destroy: true,
        iDisplayLength: 50,
    });

});





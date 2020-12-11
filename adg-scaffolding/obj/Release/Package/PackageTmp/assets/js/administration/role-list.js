var urlList = "/Backend/Administrator/Role/role-list.aspx";
var urlInfo = "/Backend/Administrator/Role/role-info.aspx";
var msg;
var tblId = '#tblRole';
var tblObj;

$(function () {
    var numberRenderer = $.fn.dataTable.render.number(',', '.', 0).display;

    tblObj = $(tblId).DataTable({
        paging: true,
        destroy: true,
        deferRender: true,
        processing: true,
        serverSide: true,
        iDisplayLength: 50,
        language:
        {
            processing: "<div class='progress m-3'><div class='progress-bar' style='width: 500%'> กำลังดำเนินการ ...</div></div>" +
                '<div class="modal-backdrop fade show"></div>',
        },
        ajax: {
            url: urlList + "/GetData",
            contentType: "application/json; charset=utf-8",
            type: "POST",
            dataType: "json",
            data: function (d) {
                d.is_active = $("select[id*=ddlStatus]").find(':selected').val();
                d.txtSearch = $("input[id*=txtSearch]").val();

                return JSON.stringify(d);
            },
            dataSrc: function (json) {
                json.draw = json.d.draw;
                json.recordsTotal = json.d.recordsTotal;
                json.recordsFiltered = json.d.recordsFiltered;
                json.data = json.d.data;

                var return_data = json;
                console.log(return_data.data);
                return return_data.data;
            }
        },
        columns: [
            {
                data: "role_code",
                orderable: false,
            },
            {
                data: "role_name",
                orderable: false,
            },
            {
                data: "comment",
                orderable: false,
            },
            {
                data: "is_active",
                className: "text-center",
                orderable: false,
                render: function (data, type, row) {
                    if (type === 'display') {
                        return '<label class="switcher switcher-sm" ><input id="chkStatus" type="checkbox" ' + (data ? "checked" : "") + ' class="switcher-input" value="' + row.id + '"><span class="switcher-indicator"><span class="switcher-yes"><span class="ion ion-md-checkmark"></span></span><span class="switcher-no"><span class="ion ion-md-close"></span></span></span></label>';
                    }

                    return data;
                }
            },
            {
                data: "id", className: "text-center", orderable: false,
                render: function (data, type, row, meta) {

                    return renderCol_Tools(data, type, row, meta);
                }
            }
        ],
        createdRow: function (row, data, dataIndex) {
            //$('td:eq(6)', row).css('min-width', '100px');

        },
    });

    function renderCol_CreateDate(data, type, row, meta) {
        if (type === "sort" || type === "type") {

            return data;
        }

        return moment(data).format("DD/MM/YYYY");
    };

    function renderCol_Number(data, type, row, meta) {

        return numberRenderer(data);
    }

    function renderCol_Tools(data, type, row, meta) {
        var ret = '';
        var btnDelete = '<button type="button" id="btnDelete"  rIndex="' + meta.row + '" class="btn btn-danger text-uppercase"><i class="ion ion-md-close"></i></button>';
        var aView = '<a href="' + urlInfo + '?ID=' + data + '" class="btn btn-primary mr-1"><i class="ion ion-md-search"></i></a>';

        if (type === 'display') {

            ret += '<div class="btn-group btn-group-sm">' + aView + btnDelete;
            ret += '</div>';

            return ret;
        }

        return data;
    };

    $("button[id*=btnSearch]").click(function () {
        tblObj.ajax.reload();
    });

    $(".dataTables_filter input")
        .unbind()
        .keyup(function (e) {
            if (e.keyCode === 13) {
                tblObj.search(this.value).draw();
            }
            if (this.value == "" && e.keyCode === 13) {
                tblObj.search("").draw();
            }

            return;

        });

    $(tblId + ' tbody').on('click', 'td button', function () {
        var _this = $(this);
        var rowIndex = _this.attr('rIndex');
        var role = tblObj.rows(rowIndex).data()[0];
        $("input[id*=hdfId]").val(role.id);
        ShowModalShared('div[id=DeleteRecord]');
    });

    $(tblId + ' tbody').on('change', 'input.switcher-input', function () {
        var id = $(this).attr('value');
        var checked = $(this).prop('checked');
        var entity = { id: id, is_active: checked };
        $.ajax({
            type: "POST",
            url: urlList + '/UpdateStatus',
            data: JSON.stringify(entity),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function () {
                swal({
                    title: '',
                    type: "success",
                    text: 'ทำรายการสำเร็จ'.toUpperCase(),
                    confirmButtonClass: "btn-success"
                },
                    function (isConfirm) {
                        if (isConfirm) {
                            tblObj.ajax.reload();
                        }
                    });
            }
        });
    });
});

function DeleteData() {
    var hdfId = $("input[id*=hdfId]");
    var entity = {
        id: hdfId.val()
    };

    $.ajax({
        type: "POST",
        url: urlList + '/DeleteData',
        data: JSON.stringify(entity),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            console.log(result);
            if (result.d == true) {
                msg = 'ลบรายการสำเร็จ';
                openModalSuccess(msg);
                tblObj.ajax.reload();
            }
            else {
                msg = 'ลบรายการไม่สำเร็จ<br/>เนื่องจากรายการนี้ถูกนำไปอ้างอิงในเอากสารอื่น';
                openModalFail(msg)
            }
            HideModal('div[id=DeleteRecord]');
            HideProgressBar('div[id*=UpdateProgress1]');
        },
        error: function () {
            msg = 'ลบรายการไม่สำเร็จ';
            openModalFail(msg)
            HideModal('div[id=DeleteRecord]');
            HideProgressBar('div[id*=UpdateProgress1]');
        },
    });
}
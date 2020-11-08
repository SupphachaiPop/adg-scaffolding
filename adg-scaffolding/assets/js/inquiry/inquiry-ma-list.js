var urlList = "/RegistorFrom/inquiry-ma-list.aspx";
var urlInfo = "/RegistorFrom/inquiry-ma-info.aspx";
var urlPrint = "/RegistorFrom/PrintFrom/print-computer.aspx";
var msg;
var tblId = '#tblSWInquiryMa';
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
                d.branchId = $("input[id*=hdfBranchId]").val();
                d.textFieldSearch = $("input[type=search]").val();
                d.textSearch = $("input[id*=txtSearch]").val();

                return JSON.stringify(d);
            },
            dataSrc: function (json) {
                json.draw = json.d.draw;
                json.recordsTotal = json.d.recordsTotal;
                json.recordsFiltered = json.d.recordsFiltered;
                json.data = json.d.data;

                var return_data = json;
                return return_data.data;
            }
        },
        columns: [
            {
                data: "document_no",
                name: "document_no"
            },
            {
                data: "create_date",
                className: "text-center",
                render: function (data, type, row, meta) {

                    return renderCol_CreateDate(data, type, row, meta);
                }
            },
            {
                data: "Sw_branch_name",
            },
            {
                data: "sw_customer_name",
            },
            {
                data: "sw_customer_phone",
                orderable: false
            },
            {
                data: "sw_model",
            },
            {
                data: "Sw_brand_name",
            },
            {
                data: "part_number",
            },
            {
                data: "sw_warranty_period",
                className: "text-center",
            },
            {
                data: "sw_inquiry_id", className: "text-center", orderable: false,
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
        var btnDelete = '<button type="button" id="btnDelete" rIndex="' + meta.row + '" class="btn btn-danger text-uppercase"><i class="ion ion-md-close"></i></button>';
        var aView = '<a href="' + urlInfo + '?ID=' + data + '" class="btn btn-primary mr-1">View</a>';
        var aPrint = '<a href="' + urlPrint + '?ID=' + data + '" class="btn btn-default mr-1" target="_blank"><i class="ion ion-md-print"></i></a>';

        if (type === 'display') {

            ret += '<div class="btn-group btn-group-sm">' + aView + aPrint + btnDelete;
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
        var inquiry = tblObj.rows(rowIndex).data()[0];

        $("input[id*=hdfInquiryId]").val(inquiry.sw_inquiry_id);
        ShowModalShared('div[id=DeleteRecord]');
    });
});

function DeleteData() {
    var hdfInquiryId = $("input[id*=hdfInquiryId]");
    var entity = {
        id: parseInt(hdfInquiryId.val())
    };

    $.ajax({
        type: "POST",
        url: urlList + '/DeleteData',
        data: JSON.stringify(entity),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function () {
            msg = 'ทำรายการสำเร็จ';

            SuccessAlertWithReloadTableObj(msg, tblObj);
        },
        error: function () {
            msg = 'ทำรายการไม่สำเร็จ';

            ErrorAlert(msg.toUpperCase());
        },
    });
}
var urlList = "/RegistorFrom/inquiry-admin.aspx";
var urlInfo = "/RegistorFrom/inquiry-info.aspx";
var urlPrint = "/RegistorFrom/PrintFrom/print-smartphone.aspx";
var msg;
var tblId = '#tblSWInquiryAdmin';
var tblObj;
var isExport = false;

$(function () {
    var numberRenderer = $.fn.dataTable.render.number(',', '.', 0).display;

    tblObj = $(tblId).DataTable({
        paging: true,
        destroy: true,
        deferRender: true,
        processing: true,
        serverSide: true,
        iDisplayLength: 50,
        scrollX: true,
        language:
        {
            processing: "<div class='progress m-3'><div class='progress-bar' style='width: 500%'> กำลังดำเนินการ ...</div></div>" +
                '<div class="modal-backdrop fade show"></div>',
        },
        scroller: {
            loadingIndicator: true
        },
        ajax: {
            url: urlList + "/GetData",
            contentType: "application/json; charset=utf-8",
            type: "POST",
            dataType: "json",
            data: function (d) {
                d.isExport = isExport;
                d.branchId = $("input[id*=hdfBranchId]").val();
                d.ddlCateName = $("select[id*=ddlCategory]").find(':selected').val();
                d.textFieldSearch = $("input[type=search]").val();
                d.textSearch = $("input[id*=txtSearch]").val();
                d.textStartDate = $("input[id*=txtStartDate]").val();
                d.textEndDate = $("input[id*=txtEndDate]").val();

                d.textClaimStartDate = $("input[id*=txtClaimStartDate]").val();
                d.textClaimEndDate = $("input[id*=txtClaimEndDate]").val();
                d.textConfirmStartDate = $("input[id*=txtConfirmStartDate]").val();
                d.textConfirmEndDate = $("input[id*=txtConfirmEndDate]").val();
                d.textTransportStartDate = $("input[id*=txtTransportStartDate]").val();
                d.textTransportEndDate = $("input[id*=txtTransportEndDate]").val();
                d.textCompensateStartDate = $("input[id*=txtCompensateStartDate]").val();
                d.textCompensateEndDate = $("input[id*=txtCompensateEndDate]").val();

                d.ddlBrandName = $("select[id*=ddlBrand]").find(':selected').val();
                d.ddlStatusName = $("select[id*=ddlStatus]").find(':selected').val();

                d.ddlBalanceDay = $("select[id*=ddlBalanceDay]").find(':selected').val();
                d.textBalanceDay = $("input[id*=txtBalanceDay]").val();

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
                data: "register_date",
                className: "text-center",
                render: function (data, type, row, meta) {

                    return renderCol_CreateDate(data, type, row, meta);
                }
            },
            {
                data: "Sw_branch_name",
                orderable: false,
            },
            {
                data: "sw_customer_name",
                orderable: false,
                render: function (data, type, row, meta) {

                    return renderCol_CustomerInfo(data, type, row, meta);
                }
            },
            {
                data: "sw_id_card",
                orderable: false,
            },
            {
                data: "sw_customer_address",
                orderable: false,
            },
            {
                data: "sw_customer_phone",
                orderable: false,
            },
            {
                data: "sw_customer_email",
                orderable: false,
            },
            {
                data: "Sw_brand_name",
                orderable: false,
            },
            {
                data: "sw_model",
                orderable: false,
            },
            {
                data: "part_number",
                orderable: false,
            },
            
            {
                data: "product_price",
                className: "text-center",
                render: function (data, type, row, meta) {

                    return renderCol_Number(data, type, row, meta);
                },
                orderable: false,
            },
            {
                data: "warranty_days",
                className: "text-center",
                orderable: false,
            },
            {
                data: "warranty_balance_day",
                className: "text-center",
                render: function (data, type, row, meta) {
                    return renderCol_Number(data, type, row, meta);
                },
                orderable: false,
            },
            {
                data: "warranty_status",
            },
            {
                data: "service_price",
                render: function (data, type, row, meta) {

                    return renderCol_Number(data, type, row, meta);
                },
                orderable: false,
            },
            {
                data: "repair_clam_date",
                className: "text-center",
                render: function (data, type, row, meta) {

                    return renderCol_CreateDate(data, type, row, meta);
                },
                orderable: false,

            },
            {
                data: "claim_count",
                className: "text-center",
                orderable: false,
            },
            {
                data: "bill_no",
                className: "text-center",
                orderable: false,
            },
            {
                data: "repair_accept_repair_date",
                className: "text-center",
                render: function (data, type, row, meta) {

                    return renderCol_CreateDate(data, type, row, meta);
                },
                orderable: false,

            },
            {
                data: "repair_transport_date",
                className: "text-center",
                render: function (data, type, row, meta) {

                    return renderCol_CreateDate(data, type, row, meta);
                },
                orderable: false,
            },
            {
                data: "compensate_price",
                render: function (data, type, row, meta) {

                    return renderCol_Number(data, type, row, meta);
                },
                orderable: false,
            },
            {
                data: "compensate_date",
                className: "text-center",
                render: function (data, type, row, meta) {

                    return renderCol_CreateDate(data, type, row, meta);
                },
                orderable: false,
            },
            {
                data: "transport_date",
                className: "text-center",
                render: function (data, type, row, meta) {

                    return renderCol_CreateDate(data, type, row, meta);
                },
                orderable: false,
            },
            {
                data: "transport_price",
                className: "text-center",
                render: function (data, type, row, meta) {

                    return renderCol_Number(data, type, row, meta);
                },
                orderable: false,
            },
            {
                data: "sw_inquiry_id", className: "text-center",
                render: function (data, type, row, meta) {

                    return renderCol_Tools(data, type, row, meta);
                },
                orderable: false
            }
        ],
        createdRow: function (row, data, dataIndex) {
            //$('td:eq(6)', row).css('min-width', '100px');

        },
        drawCallback: function (settings, json) {
            if (isExport) {
                $('button[id*=btnExportServer]').trigger('click');
            }
        }
    });

    function renderCol_CreateDate(data, type, row, meta) {
        if (type === "sort" || type === "type") {
            return data;
        }

        if (data == null) {
            return data;
        }

        return moment(data).format("DD/MM/YYYY");
    };

    function renderCol_CustomerInfo(data, type, row, meta) {
        var ret = '';
        var br = '<br />'
        //var idCard = '<b>เลขบัตรประชาชน: </b>' + row.sw_id_card;
        var name = '<b>ชื่อ-นามสกุล: </b>' + row.sw_customer_name;

        //ret += idCard;
        //ret += br + name;
        ret = name;

        return ret;
    }

    function renderCol_Number(data, type, row, meta) {

        return numberRenderer(data);
    }

    function renderCol_Tools(data, type, row, meta) {
        var ret = '';
        var btnDelete = '<button type="button" id="btnDelete" rIndex="' + meta.row + '" class="btn btn-danger text-uppercase"><i class="ion ion-md-close"></i></button>';
        var aView = '<a href="' + urlInfo + '?ID=' + data + '" class="btn btn-primary mr-1">View</a>';
        var aPrint = '<a href="' + urlPrint + '?ID=' + data + '" class="btn btn-default mr-1" target="_blank"><i class="ion ion-md-print"></i></a>';

        if (type === 'display') {

            ret += '<div class="btn-group btn-group-sm">' + aView + btnDelete;
            ret += '</div>';

            return ret;
        }

        return data;
    };

    $("button[id*=btnSearch]").click(function () {
        var msg;
        isExport = false;
        if (!IsValidCompareDate('input[id*=txtStartDate]', 'input[id*=txtEndDate]')) {
            msg = 'วันที่ลงทะเบียน (จากวันที่) ต้องน้อยกว่าหรือเท่ากับ วันที่ลงทะเบียน (ถึงวันที่)';
            WarningAlertWithFocus(msg, 'input[id*=txtStartDate]');

            return;
        }

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

    SetControlPicker('input[id*=txtStartDate]');
    SetControlPicker('input[id*=txtEndDate]');

    SetControlPicker('input[id*=txtClaimStartDate]');
    SetControlPicker('input[id*=txtClaimEndDate]');

    SetControlPicker('input[id*=txtConfirmStartDate]');
    SetControlPicker('input[id*=txtConfirmEndDate]');

    SetControlPicker('input[id*=txtTransportStartDate]');
    SetControlPicker('input[id*=txtTransportEndDate]');

    SetControlPicker('input[id*=txtCompensateStartDate]');
    SetControlPicker('input[id*=txtCompensateEndDate]');

});

function ExportData() {
    isExport = true;
    tblObj.ajax.reload();
    //$('button[id*=btnExportServer]').trigger('click');
}

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
            HideModal('div[id=DeleteRecord]');
            HideProgressBar('div[id*=UpdateProgress1]');
        },
        error: function () {
            msg = 'ทำรายการไม่สำเร็จ';

            ErrorAlert(msg.toUpperCase());
            HideProgressBar('div[id*=UpdateProgress1]');
        },
    });
}

function SyncData() {
    var entity = '{}';

    $.ajax({
        type: "POST",
        url: urlList + '/SyncData',
        data: entity,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function () {
            msg = 'ทำรายการสำเร็จ';

            SuccessAlertWithReloadTableObj(msg, tblObj);
            HideModal('div[id*=modalConfirmSync]');
        },
        error: function () {
            msg = 'ทำรายการไม่สำเร็จ';

            ErrorAlert(msg.toUpperCase());
            HideModal('div[id*=modalConfirmSync]');
        },
    });
}
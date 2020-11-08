var urlList = "/Backoffice/MasterData/Customer/customer-list.aspx";
var urlInfo = "/Backoffice/MasterData/Customer/customer-info.aspx";
var msg;
var tblId = '#tblSWCustomer';
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
            { data: "customer_code", name: "customer code" },
            {
                data: "customer_name",
                render: function (data, type, row) {

                    return row.first_name + ' ' + row.last_name;
                }
            },
            { data: "email" },
            { data: "mobile_primary" },
            {
                data: "is_active",
                render: function (data, type, row) {
                    if (type === 'display') {
                        return '<label class="switcher switcher-sm"><input id="chkStatus" type="checkbox" ' + (data ? "checked" : "") + ' class="switcher-input" value="' + row.customer_id + '"><span class="switcher-indicator"><span class="switcher-yes"><span class="ion ion-md-checkmark"></span></span><span class="switcher-no"><span class="ion ion-md-close"></span></span></span></label>';
                    }

                    return data;
                }
            },
            {
                data: "customer_id", className: "text-center", orderable: false,
                render: function (data, type, row, meta) {

                    return renderCol_Tools(data, type, row, meta);
                }
            }
        ]
        ,
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

    function renderCol_CustomerInfo(data, type, row, meta) {
        var ret = '';
        var br = '<br />'
        var idCard = '<b>เลขบัตรประชาชน: </b>' + row.sw_id_card;
        var name = '<b>ชื่อ-นามสกุล: </b>' + row.sw_customer_name;

        ret += idCard;
        ret += br + name;

        return ret;
    }

    function renderCol_Number(data, type, row, meta) {

        return numberRenderer(data);
    }

    function renderCol_Tools(data, type, row, meta) {
        var ret = '';
        var btnDelete = '<button type="button" id="btnDelete" rIndex="' + meta.row + '" class="btn btn-danger text-uppercase"><i class="ion ion-md-close"></i></button>';
        var aView = '<a href="' + urlInfo + '?ID=' + data + '" class="btn btn-primary mr-1">View</a>';
 

        if (type === 'display') {

            ret += '<div class="btn-group btn-group-sm">' + aView  + btnDelete;
            ret += '</div>';

            return ret;
        }

        return data;
    };

    $("button[id*=btnSearch]").click(function () {
        var msg;

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
        var customer = tblObj.rows(rowIndex).data()[0];

        $("input[id*=hdfCustomerId]").val(customer.sw_customer_id);
        ShowModalShared('div[id=DeleteRecord]');
    });

    SetControlPicker('input[id*=txtStartDate]');
    SetControlPicker('input[id*=txtEndDate]');
});

function DeleteData() {
    var hdfCustomerId = $("input[id*=hdfCustomerId]");
    var entity = {
        id: parseInt(hdfCustomerId.val())
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


function doExportCustomer() {

    //console.log("xxx");
    var d = {};



    d.textFieldSearch = $("input[type=search]").val();
    d.textSearch = $("input[id*=txtSearch]").val();


    var tmpAjax = {
        fileName: ('customer-list-' + new Date().toJSON().slice(0, 10).replace(/-/g, '-')),
        url: urlList + "/GetData",
        contentType: "application/json; charset=utf-8",
        type: "POST",
        dataType: "json",
        data: d,
        dataSrc: function (json) {
            json.draw = json.d.draw;
            json.recordsTotal = json.d.recordsTotal;
            json.recordsFiltered = json.d.recordsFiltered;
            json.data = json.d.data;
            var return_data = json;
            return return_data.data;
        }
    };
    exportFromDatatableAjax(tmpAjax, {
        cols: ["customer_code", "customer_type", "first_name", "last_name", "tax_no", "address_line1", "address_line2", "address_line3"],
        dateCols: []
    }, showProgress, hideProgress);
}


var urlInfo = '/RegistorFrom/Warrant-Check/warranty-check.aspx';
var tblObj;
var tblId = '#tblWarrantyCheck';
var nullClaimDate = '/Date(-62135596800000)/';

function SetWarrantyData() {
    var dayAmountFormat = $.fn.dataTable.render.number(',', '.', 0).display;

    tblObj = $(tblId).DataTable({
        paging: false,
        searching: false,
        info: false,
        destroy: true,
        deferRender: true,
        processing: true,
        serverSide: true,
        iDisplayLength: 1,
        ordering: false,
        language:
        {
            processing: "<div class='progress m-3'><div class='progress-bar' style='width: 500%'> กำลังดำเนินการ ...</div></div><div class='modal-backdrop fade show'></div>",
        },
        ajax: {
            url: urlInfo + "/GetData",
            contentType: "application/json; charset=utf-8",
            type: "POST",
            dataType: "json",
            data: function (d) {
                d.textSerial = $("input[key=txtSerial").val();

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
                data: "create_date",
                orderable: false,
                className: 'text-center',
                render: function (data, type, row, meta) {

                    return renderCol_Date(data, type, row, meta);
                }
            },
            {
                data: "warranty_days",
                orderable: false,
                className: 'text-center',
            },
            {
                data: "warranty_balance_day",
                orderable: false,
                className: 'text-center',
                render: function (data, type, row, meta) {

                    return renderCol_BalanceDay(data, type, row, meta);
                }
            },
            {
                data: "warranty_status_flag",
                orderable: false,
                className: 'text-center',
                render: function (data, type, row, meta) {

                    return renderCol_Status(data, type, row, meta);
                }
            },
            {
                data: "claim_date",
                orderable: false,
                className: 'text-center',
                render: function (data, type, row, meta) {

                    return renderCol_Date(data, type, row, meta);
                }
            },
            {
                data: "inquiry_claim_date",
                orderable: false,
                className: 'text-center',
                render: function (data, type, row, meta) {

                    return renderCol_Date(data, type, row, meta);
                }
            },
            {
                data: "confirm_repair_date",
                orderable: false,
                className: 'text-center',
                render: function (data, type, row, meta) {

                    return renderCol_Date(data, type, row, meta);
                }
            }
        ],
        createdRow: function (row, data, dataIndex) {
        },
        rowCallback: function (row, data) {

        },
    });

    function renderCol_Date(data, type, row, meta) {
        if (type === "sort" || type === "type") {
            return data;
        }
        if (data === nullClaimDate) {

            return '-';
        }

        return moment(data).format("DD/MM/YYYY");
    };

    function renderCol_BalanceDay(data, type, row, meta) {
        if (type === "sort" || type === "type") {
            return data;
        }

        return dayAmountFormat(data);
    };

    function renderCol_Status(data, type, row, meta) {
        var ret = '';

        if (type === 'display') {
            ret += '<div class="text-center">'

            switch (row.warranty_status_flag) {
                case true:
                    ret += '<button type="button" class="btn btn-sm btn-success cursor-default text-uppercase">' + row.warranty_status + '</button>';

                    break;

                default:
                    ret += '<button type="button" class="btn btn-sm btn-danger cursor-default text-uppercase">' + row.warranty_status + '</button>';

                    break;
            }

            ret += '</div">'

            return ret;
        }

        return data;
    };
};

function CheckWarranty() {
    var txtSerial = $("input[id*=txtSerial]");
    $('#result').hide();

    if (txtSerial.val() === '') {
        var msg = 'กรุณากรอกหมายเลขประจำเครื่อง';

        WarningAlertWithFocus(msg, txtSerial);

        return;
    }

    SetWarrantyData();
    $('#result').show();
}

$(function () {
    $('#result').hide();

    $("#btnSearch").click(function () {
        CheckWarranty();
    });

    $("input[id*=txtSerial]").keyup(function (e) {
        if (e.keyCode === 13) {
            CheckWarranty();
        }
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
});



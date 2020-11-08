var urlList = "/Backoffice/MasterData/Sparepart/sparepart-list.aspx";
var urlInfo = "/Backoffice/MasterData/Sparepart/sparepart-info.aspx";
var msg;
var tblId = '#tblSWSparepart';
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
        columns: [  //sku_code  sku_name  utq_name  iccat_name  icdept_thaidesc cost sell qty
          
            {
                data: "sku_code",
                orderable: false
            },
            {
                data: "sku_name",
                orderable: false
            },
            {
                data: "wh_name",
                orderable: false
            },
            {
                data: "utq_name",
                orderable: false
            },
            {
                data: "iccat_name",
                orderable: false
            },
            {
                data: "icdept_thaidesc",
                orderable: false
            },

            {
                data: "cost",
                render: function (data, type, row, meta) {

                    return renderCol_Number(data, type, row, meta);
                }
            },
            {
                data: "sell",
                render: function (data, type, row, meta) {

                    return renderCol_Number(data, type, row, meta);
                },
                orderable: false
            },
            {
                data: "qty",
            },
            {
                data: "sparepart_id", className: "text-center", orderable: false,
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
        var btnDelete = '';
        var aView = '<a href="' + urlInfo + '?ID=' + data + '" class="btn btn-primary mr-1">View</a>';
        var aPrint = '';

        if (type === 'display') {

            ret += '<div class="btn-group btn-group-sm">' + aView + aPrint + btnDelete;
            ret += '</div>';

            return ret;
        }

        return data;
    };

    $("button[id*=btnSearch]").click(function () {
        var msg;


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
        var sparepart = tblObj.rows(rowIndex).data()[0];

        $("input[id*=hdfSparepartId]").val(sparepart.sw_sparepart_id);
        ShowModalShared('div[id=DeleteRecord]');
    });

    SetControlPicker('input[id*=txtStartDate]');
    SetControlPicker('input[id*=txtEndDate]');
});

function DeleteData() {
    var hdfSparepartId = $("input[id*=hdfSparepartId]");
    var entity = {
        id: parseInt(hdfSparepartId.val())
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


function doExportExcel() {

    //console.log("xxx");
    var d = {};

    ////d.draw = 1;
    //d.store = $("select[key=ddlStore]").val();
    //d.serviceType = $("select[key=ddlServiceOption]").val();
    //d.textdateStart = $("input[key=txtStartDate]").val();
    //d.textdateEnd = $("input[key=txtEndDate]").val();


    d.textFieldSearch = $("input[type=search]").val();
    d.textSearch = $("input[id*=txtSearch]").val();

  
    var tmpAjax = {
        fileName: ('sparepart-list-' + new Date().toJSON().slice(0, 10).replace(/-/g, '-')),
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
        cols: ["wh_code", "wh_name", "sku_key", "sku_code", "sku_name", "utq_key", "utq_name", "brn_key", "brn_name", "iccat_key", "iccat_name", "icdept_key", "icdept_thaidesc", "skualt_key", "skualt_name", "cost", "sell", "qty"],
        dateCols: []
    }, showProgress, hideProgress);
}



 

	//[iccolor_key] [varchar](500) NULL,
	//[iccolor_name] [varchar](500) NULL,
	//[icsize_key] [varchar](500) NULL,
	//[icsize_name] [varchar](500) NULL,
 
 
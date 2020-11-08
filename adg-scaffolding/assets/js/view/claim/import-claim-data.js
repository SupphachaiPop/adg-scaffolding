var urlList = "/Backoffice/Claim/claim-list.aspx";
var urlMgmt = "/Backoffice/Claim/claim-management.aspx";
var urlUploadFile = "/UploadFileHandler/UploadFileClaimData.ashx";
var msg;
var tblId = '#tblInquiryClaim';
var tblObj;

function OnUploadFile() {
    ShowProgressBar('#UpdateProgress1');

    var fileUpload = $('input[id*=fileUploadClaim]').get(0);
    var files = fileUpload.files;
    var fileData = new FormData();

    if (files.length <= 0) {
        msg = 'เลือกอย่างน้อย 1 ไฟล์';

        WarningAlert(msg);
        HideProgressBar('#UpdateProgress1');

        return;
    }

    for (var i = 0; i < files.length; i++) {
        fileData.append(files[i].name, files[i]);
    }

    $.ajax({
        url: urlUploadFile,
        type: "POST",
        contentType: false,
        processData: false,
        data: fileData,
        success:
            function (result) {
                if (result === 'ทำรายการไม่สำเร็จ') {
                    ErrorAlert(result);
                }
                else if (result === 'ทำรายการสำเร็จ') {

                    bootbox.alert({
                        message: '<span class="text-success font-weight-bold">' + result + '</span>',
                        className: 'bootbox-sm text-center',
                        callback: function () {
                            GetClaimData();
                        },
                    });
                    //SuccessAlertWithRedirect(result, urlMgmt);
                }
                else {
                    msg = '<br /> กรุณาตรวสอบเอกสารใหม่'
                    ErrorAlert(result + msg);
                }

                fileUpload.value = '';
                HideProgressBar('#UpdateProgress1');
            },
        error:
            function (err) {
                msg = 'อัพโหลดไม่สำเร็จ';

                ErrorAlert(msg);
                HideProgressBar('#UpdateProgress1');

            }
    });
}

function GetClaimData() {

    tblObj = $(tblId).DataTable({
        autoWidth: true,
        paging: false,
        destroy: true,
        deferRender: true,
        processing: true,
        serverSide: true,
        iDisplayLength: 50,
        ordering: false,
        searching: false,
        dom: 'rt',
        language:
        {
            processing: "<div class='progress m-3'><div class='progress-bar' style='width: 500%'> กำลังดำเนินการ ...</div></div>" +
                '<div class="modal-backdrop fade show"></div>',
        },
        scroller: {
            loadingIndicator: true
        },
        ajax: {
            url: urlMgmt + "/GetData",
            contentType: "application/json; charset=utf-8",
            type: "POST",
            dataType: "json",
            data: function (d) {
                //d.isExport = isExport;

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
                data: "ref_repair",
                name: "ref_repair"
            },
            {
                data: "warranty_name",
                name: "warranty_name"
            },
            {
                data: "coverage_no",
            },
            {
                data: "part_number",
            },
            {
                data: "sw_model",
            },
            {
                data: "claim_date",
                className: "text-center",
                render: function (data, type, row, meta) {

                    return renderCol_CreateDate(data, type, row, meta);
                }
            },
            {
                data: "expire_date",
                className: "text-center",
                render: function (data, type, row, meta) {

                    return renderCol_CreateDate(data, type, row, meta);
                }
            },
            {
                data: "warranty_status",
            },
            {
                data: "sw_inquiry_id", className: "text-center",
                render: function (data, type, row, meta) {

                    return renderCol_Tools(data, type, row, meta);
                },
            }
        ],
        rowCallback: function (row, data) {
            $(row).addClass(data.row_color_class);
            $(row).attr('val-inquiry', data.sw_inquiry_id);
            $(row).attr('value-branch', data.sw_branch_id);
            $(row).attr('value-status', data.sw_status_claim_id);
            $(row).attr('value-ref-repair', data.ref_repair);
            $(row).attr('value-month', data.month);
            $(row).attr('value-year', data.year);

            $('span[id$=lblUploadDate]').html('-');
            //if (data.created_date) {
            //    $('span[id$=lblUploadDate]').html(moment(data.created_date).format("DD/MM/YYYY"));
            //}
        },
        createdRow: function (row, data, dataIndex) {
            //$('td:eq(6)', row).css('min-width', '100px');

        },
    });

    function renderCol_CreateDate(data, type, row, meta) {
        if (type === "sort" || type === "type") {
            return data;
        }

        if (data == null) {
            return "-";
        }

        return moment(data).format("DD/MM/YYYY");
    };

    function renderCol_Tools(data, type, row, meta) {
        var ret = '';
        var btnDelete = '<button type="button" id="btnDelete" rIndex="' + meta.row + '" class="btn btn-danger text-uppercase"><i class="ion ion-md-close"></i></button>';

        if (type === 'display') {

            ret += '<div class="btn-group btn-group-sm">' + btnDelete;
            ret += '</div>';

            return ret;
        }

        return data;
    };

    $(tblId + ' tbody').on('click', 'button[id$=btnDelete]', function (e) {
        $(this).closest('tr').addClass('d-none');
    });

}

function SaveData() {
    var urlPost = urlMgmt + "/SaveData";
    var data = [];
    ShowProgressBar('#UpdateProgress1');

    tblObj.$('tr:not(.d-none)').each(function (i, ele) {
        //console.log($(ele).attr('val-inquiry'));
        if ($(ele).attr('value-status') != '0') {
            data.push({
                sw_inquiry_id: $(ele).attr('val-inquiry'),
                ref_repair: $(ele).attr('value-ref-repair'),
                sw_status_claim_id: $(ele).attr('value-status'),
                sw_branch_id: $(ele).attr('value-branch'),
                //month: $(ele).attr('value-month'),
                //year: $(ele).attr('value-year'),
            });
        }
    });

    if (data.length <= 0) {
        WarningAlert('กรุณา อัพโหลดไฟล์ หรือไม่พบข้อมูลที่อยู่ในประกัน');
        HideProgressBar('#UpdateProgress1');

        return;
    }

    var DTO = { 'param': data }

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: urlPost,
        data: JSON.stringify(DTO),
        datatype: "json",
        success: function (result) {
            var res = result.d;

            console.log(res);

            if (res.IS_SUCCESS) {
                SuccessAlertWithRedirect(res.MESSAGE, urlList);

                return;
            }

            ErrorAlert(res.MESSAGE);

            HideProgressBar('#UpdateProgress1');
        },
        error: function (xmlhttprequest, textstatus, errorthrown) {
            ErrorAlert(" conection to the server failed ");
            console.log("error: " + errorthrown);
            HideProgressBar('#UpdateProgress1');
        }
    });//end of $.ajax()
}

$(function () {
    GetClaimData();
});

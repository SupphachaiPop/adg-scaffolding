var urlList = "/RegistorFrom/inquiry-admin.aspx";
var urlUploadFile = "/UploadFileHandler/UploadFile.ashx";
var msg;
var tblId = '#tblImgPromotion';
var tblObj;

function OnUploadFile() {
    ShowProgressBar('#UpdateProgress1');

    $(function () {

        var fileUpload = $('input[id*=fileUploadInquiry]').get(0);
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
                        SuccessAlertWithRedirect(result, urlList);
                    }
                    //else if (result === 'คอลัมน์ Branch(Code) เป็นค่าว่าง') {
                    //    msg = ' <br />กรุณาตรวสอบเอกสารใหม่'
                    //    WarningAlert(result + msg);
                    //    HideProgressBar('#UpdateProgress1');
                    //}
                    //else if (result === 'คอลัมน์ BRAND เป็นค่าว่าง') {
                    //    msg = ' <br />กรุณาตรวสอบเอกสารใหม่'
                    //    WarningAlert(result + msg);
                    //    HideProgressBar('#UpdateProgress1');
                    //}
                    //else if (result.includes('ไม่พบสาขา: ')) {
                    //    msg = ' <br />กรุณาตรวสอบเอกสารใหม่'
                    //    WarningAlert(result + msg);
                    //    HideProgressBar('#UpdateProgress1');
                    //}
                    //else if (result.includes('ไม่พบเเบรนด์: ')) {
                    //    msg = ' <br />กรุณาตรวสอบเอกสารใหม่'
                    //    WarningAlert(result + msg);
                    //    HideProgressBar('#UpdateProgress1');
                    //}
                    else {
                        msg = '<br /> กรุณาตรวจสอบเอกสารใหม่'
                        ErrorAlert(result + msg);
                    }

                    HideProgressBar('#UpdateProgress1');
                    fileUpload.value = '';
                },
            error:
                function (err) {
                    msg = 'อัพโหลดไม่สำเร็จ';

                    ErrorAlert(msg);
                    HideProgressBar('#UpdateProgress1');
                    fileUpload.value = '';
                }
        });
    })

}

$(function () {

});

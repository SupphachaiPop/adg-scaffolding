function WarningAlert(msg) {
    bootbox.alert({
        message: '<span class="text-warning font-weight-bold">' + msg.toUpperCase() + '</span>',
        className: 'bootbox-sm text-center',
        callback: function () {
            //
        },
    });
}

function WarningAlertWithFocus(msg, id) {
    bootbox.alert({
        message: '<span class="text-warning font-weight-bold">' + msg.toUpperCase() + '</span>',
        className: 'bootbox-sm text-center',
        callback: function () {
            setTimeout(function () {
                $(id).focus();

            }, 10);
        },
    });
}

function WarningAlertWithOpen(msg, id) {
    bootbox.alert({
        message: '<span class="text-warning font-weight-bold">' + msg.toUpperCase() + '</span>',
        className: 'bootbox-sm text-center',
        callback: function () {
            setTimeout(function () {
                $(id).click();

            }, 10);
        },
    });
}

function WarningAlertWithFocusAndClear(msg, id) {
    bootbox.alert({
        message: '<span class="text-warning font-weight-bold">' + msg.toUpperCase() + '</span>',
        className: 'bootbox-sm text-center',
        callback: function () {
            setTimeout(function () {
                $(id).focus();
                $(id).val('');
            }, 10);
        },
    });
}

function WarningAlertWithRedirect(msg, url) {
    bootbox.alert({
        message: '<span class="text-warning font-weight-bold">' + msg.toUpperCase() + '</span>',
        className: 'bootbox-sm text-center',
        callback: function () {
            window.location = url;
        },
    });
}

function SuccessAlertWithRedirect(msg, url) {
    bootbox.alert({
        message: '<span class="text-success font-weight-bold">' + msg.toUpperCase() + '</span>',
        className: 'bootbox-sm text-center',
        callback: function () {
            window.location = url;
        },
    });
}

function SuccessAlertWithReload(msg) {
    bootbox.alert({
        message: '<span class="text-success font-weight-bold">' + msg.toUpperCase() + '</span>',
        className: 'bootbox-sm text-center',
        callback: function () {
            window.location.reload(true);
        },
    });
}

function SuccessAlertWithReloadTableObj(msg, tblObj) {
    bootbox.alert({
        message: '<span class="text-success font-weight-bold">' + msg.toUpperCase() + '</span>',
        className: 'bootbox-sm text-center',
        callback: function () {
            tblObj.ajax.reload();
        },
    });
}

function ErrorAlert(msg) {
    bootbox.alert({
        message: '<span class="text-danger font-weight-bold">' + msg.toUpperCase() + '</span>',
        className: 'bootbox-sm text-center',
        callback: function () {
            //window.location.reload();
        },
    });
}

function ConfirmLogout(id) {
    var msg = 'confirm to logout.'
    bootbox.confirm({
        message: '<span class="text-warning font-weight-bold">' + msg.toUpperCase() + '</span>',
        className: "text-center",
        buttons: {
            confirm: {
                label: 'Yes',
                className: 'btn-success'
            },
            cancel: {
                label: 'No',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result) {
                $(id).click();
            }
        },
    });
}

function ConfirmDelete() {
    var msg = 'ยืนยันการลบรูปภาพ'

    bootbox.confirm({
        message: '<span class="text-danger font-weight-bold">' + msg.toUpperCase() + '</span>',
        className: "text-center",
        buttons: {
            confirm: {
                label: 'Yes',
                className: 'btn-success'
            },
            cancel: {
                label: 'No',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result) {
                $('button[id*=confirm]').trigger('click');
            }
        },
    });
}

function ValidateFormLogin(userName, passWord) {
    var msg;

    if ($(userName).val() === '' && $(passWord).val() === '') {
        msg = 'กรุณากรอก ' + $(userName).attr('placholder') + ' เเละ ' + $(passWord).attr('placholder');
        WarningAlertWithFocus(msg, userName);

        return;
    }
    if ($(userName).val() === '') {
        msg = 'กรุณากรอก ' + $(userName).attr('placholder');
        WarningAlertWithFocus(msg, userName);

        return;
    }
    if ($(passWord).val() === '') {
        msg = 'กรุณากรอก ' + $(passWord).attr('placholder');
        WarningAlertWithFocus(msg, passWord);

        return;
    }

    $('button[id*=btnSignin]').trigger('click');
}

function addCommas(inputText) {
    inputText += '';
    x = inputText.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}

function isNumber(inputText) {
    var input = $(inputText);
    var msg;

    if (inputText != '') {
        var num = parseFloat(input.val().replace(",", ""));

        if (isNaN(num)) {
            msg = 'จำนวนเงิน ไม่ถูกต้อง';
            WarningAlertWithFocus(msg, inputText);
            addCommas(parseFloat(input.val('0')).toFixed(0));

            return;
        }

        input.val(addCommas(num.toFixed(0)));
    }
}

function isValidDay(inputText) {
    var input = $(inputText);

    if (inputText != '') {
        var num = parseFloat(input.val().replace(",", ""));

        if (isNaN(num)) {
            input.val('');

            return;
        }

        input.val(num.toFixed(0));
    }
}

function SetControlPicker(id) {
    var isRtl = $('html').attr('dir') === 'rtl';

    $(id).datepicker({
        orientation: isRtl ? 'auto right' : 'auto left',
        format: 'dd/mm/yyyy',
        autoclose: true,
        todayHighlight: true,
    });

}

function SetControlMaterialPicker(id) {
    var isRtl = $('html').attr('dir') === 'rtl';

    $(id).bootstrapMaterialDatePicker({
        orientation: isRtl ? 'auto right' : 'auto left',
        weekStart: 0,
        time: false,
        clearButton: true,
        autoclose: true,
        format: 'DD/MM/YYYY',
    });

}

function SetControlTimePicker(id) {
    var isRtl = $('html').attr('dir') === 'rtl';

    $(id).bootstrapMaterialDatePicker({
        date: false,
        shortTime: false,
        format: 'HH:mm'
    });
}

function ShowModalShared(modalID) {
    $(modalID).modal({
        backdrop: 'static',
        keyboard: false
    });
    $(modalID).modal('show');
}

function HideModal(modalID) {
    $(modalID).modal('hide');
}

function ClearImagePreview(input, srcId) {
    $(srcId)
        .attr('src', '#')
        .hide();

    $('#btnDelete').hide();
    $(input).val('');
}


function ShowProgressBar(id) {
    $(id).show();
}

function HideProgressBar(id) {
    $(id).hide();
}

function IsValidCompareDate(startId, endId) {
    var startDate = moment($(startId).val(), 'DD/MM/YYYY');
    var endDate = moment($(endId).val(), 'DD/MM/YYYY');

    if (startDate != '' && endDate != '') {
        startDate = new Date(startDate);
        endDate = new Date(endDate);

        startDate.setHours(0, 0, 0, 0);
        endDate.setHours(0, 0, 0, 0);

        if (endDate < startDate) {

            return false;
        }
    }

    return true;
}
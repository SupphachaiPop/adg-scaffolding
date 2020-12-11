/**
 * 
 * @param {any} a ajax object of datatable
 * @param {Function} startLoading start loading progress function
 * @param {Function} endLoading end loading progress function
 */
var errMsg = "ไม่พบข้อมูล";
var exportFromDatatableAjax = function (a, colObj, startLoading, endLoading) {
    var tmpData = [];
    if (!a.data.order) {
        a.data.order = [{ column: 1, dir: "desc" }];
    }
    a.data.draw = 1;
    a.data.start = 1;
    a.data.length = 200;
    a.data.isExport = true;
    console.log(a);
    if (startLoading) {
        try {

            startLoading();
        } catch (ex) {
            //
        }

    }
    var max = -1;
    var total = -1;
    var loopAjax = function (a) {
        $.ajax({
            type: a.type,
            url: a.url,
            // The key needs to match your method's input parameter (case-sensitive).
            data: JSON.stringify(a.data),
            contentType: a.contentType,
            dataType: a.dataType
        }).done(function (data) {
            a.data.draw++;
            a.data.start = a.data.start + data.d.data.length;
            //console.log('start=' + a.data.start);
            //console.log('data=' + data.d.data.length);
            //console.log(data.d.recordsTotal);
            if (total == -1) {
                total = data.d.recordsTotal;
            }
            var colsDate = colObj.dateCols;
            if (colsDate.length) {

                for (var q = 0; q < data.d.data.length; ++q) {
                    colsDate.forEach(function (key) {
                        data.d.data[q][key] = renderDate(data.d.data[q][key]);
                    });
                    delete data.d.data[q]['TotalRecords'];
                }
            }
            //
            tmpData = tmpData.concat(data.d.data);

            if (total <= a.data.start) {
                try {
                    //console.log(a.data);
                    genXlsxFromData(a.fileName, tmpData, colObj.cols);

                } catch (e) {
                    alert(e);
                }
            } else {

                loopAjax(a);
            }
        }).fail(function (e) {
            alert(errMsg + e);
            if (endLoading) {
                try {

                    endLoading();
                } catch (ex) {
                    //
                }
            }
        });
    };
    //try {

    loopAjax(a);

    //} catch (e) {
    // alert("ajax error");
    //}
};

function renderDate(data) {
    return moment(data).format("DD/MM/YYYY HH:mm:ss");
};


/**
 * 
 * @param {string} exportName file name for export
 * @param {any} data 2dimension array
 * @param {any} cols columns from fields of data
 * @param {any} opt {title: title,subject: meta subject, author : author name, sheetName: sheet name}
 */
var genXlsxFromData = function (exportName, data, cols, opt) {
    if (XLSX === undefined) {
        alert("Missing SheetJs scripts");
        return;
    }
    if (saveAs === undefined) {
        alert("Missing FileSaver.js scripts");
        return;
    }

    var wb = XLSX.utils.book_new();

    var title = opt !== undefined && opt.title ? opt.title : "";
    var subject = opt !== undefined && opt.subject ? opt.subject : "";

    var author = opt !== undefined && opt.author ? opt.author : "";
    var sheetName = opt !== undefined && opt.sheetName ? opt.sheetName : exportName;
    wb.Props = {
        Title: title,
        Subject: subject,
        Author: author,
        CreatedDate: new Date()
    };
    wb.SheetNames.push(sheetName);
    //loop through query 
    //var ws_data = [['hello', 'world']];
    var ws = XLSX.utils.json_to_sheet(data, { header: cols });
    /*
    var i = 0, result = [];

    while (i < data.length) {
        result.push([]);
        if (cols) {

            for (var j = 0; j < cols.length; ++j) {
                var key = cols[j];
                //console.log(key);
                //console.log(data[i][key]);
                result[result.length - 1].push(data[i][key]);
            }
        } else {

            for (var key2 in data[i]) {
                result[result.length - 1].push(data[i][key2]);
            }
        }
        i++;
    }
    console.log(result);
    var ws_data = result;
    var ws = XLSX.utils.aoa_to_sheet(ws_data);
    */
    wb.Sheets[sheetName] = ws;
    var wbout = XLSX.write(wb, { bookType: 'xlsx', type: 'binary' });
    download(new Blob([s2ab(wbout)]), exportName + '.xlsx', "application/octet-stream");
    //saveAs(new Blob([s2ab(wbout)], { type: "application/octet-stream" }), exportName+'.xlsx');
};

function s2ab(s) {
    var buf = new ArrayBuffer(s.length); //convert s to arrayBuffer
    var view = new Uint8Array(buf);  //create uint8array as viewer
    for (var i = 0; i < s.length; i++) view[i] = s.charCodeAt(i) & 0xFF; //convert to octet
    return buf;
}
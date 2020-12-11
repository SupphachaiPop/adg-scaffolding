// Dropzone 

var myDropzone = new Dropzone("#dropzone", {
    url: "/TI/FileUploadHandle/FileUpload.ashx",
    autoProcessQueue: false,
    addRemoveLinks: true,
    uploadMultiple: true,
    autoQueue: true,
    parallelUploads: 100,
    acceptedMimeTypes: 'image/jpeg, image/jpg, image/png',
    success:
        function () {
            swal({
                title: '',
                text: 'Upload file complete',
                type: "success",
            });
            //function (isConfirm) {
            //    if (isConfirm) {
            //        window.location = '/TI/index.aspx';
            //    }
            //});
        },
});

$('#resetInquiryImg').on('click', function (e) {
    var files = myDropzone.files.length;
    if (files <= 0) {
        alertNofile('No File For Remove!');
    }
    else {
        e.preventDefault();
        myDropzone.removeAllFiles();
    }
});

async function saveDataThenUploadInquiry() {
    //await saveDataJobRequest()
    await uploadImageInquiry()
}

function uploadImageInquiry() {
    var files = myDropzone.files.length;
    if (files > 0) {
        //e.preventDefault();
        myDropzone.processQueue();
    }
    else {
        alertNofileAndRedirect('No File Selected!');
        //window.location = '/TI/index.aspx';
    }
};

function alertNofile(alertText) {
    swal({
        title: '',
        text: alertText,
        type: "warning",
        showCancelButton: false,
        confirmButtonClass: "btn-danger",
    });
}

function alertNofileAndRedirect(alertText) {
    swal({
        title: '',
        text: alertText,
        type: "warning",
        confirmButtonClass: "btn-danger",
    },
        function (isConfirm) {
            if (isConfirm) {
                window.location = '/TI/index.aspx';
            }
        });
}
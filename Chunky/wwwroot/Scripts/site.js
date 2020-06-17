let dropZone = null;
Dropzone.autoDiscover = false;

$(function () {
    $('#upload').on('click', upload);
    $(document).ready(instantiateDropZone);
});

function upload() {
    console.log("process");
    dropZone.processQueue();
}

function instantiateDropZone() {

    let element = document.getElementById("zone");

    // https://www.dropzonejs.com/
    dropZone = new Dropzone(element, {

        init: function () {
            const zone = this;
            zone.on("drop", function (data) {

            });

            zone.on("addedfile",
                function (data) {

                });

            // when processQueue is fired. this will keep the process running until all files are uploaded.
            zone.on("processing", function () {
                this.options.autoProcessQueue = true;
            });

            zone.on('error',
                function (file, errorMessage) {
                    // show try error again and hide spinner
                    console.log("error, call the po po", file);
                });

        },
        accept: function (file, done) {

            //If the done function is invoked without arguments, the file is "accepted" and will be processed. 
            //If you pass an error message, the file is rejected, and the error message will be displayed.
            if (file.size === 0) {
                done("Empty files will not be uploaded.");
            } else {
                done();
            }
        },
        ignoreHiddenFiles: true,
        url: "Home/UploadChunks",
        maxFileSize: 40000000000,       // max individual file size 2gig
        chunking: true,                 // enable chunking
        forceChunking: true,            // forces chunking when file.size < chunkSize
        capture: null,
        parallelChunkUploads: false,     //  allows chunks to be uploaded in parallel - true
        autoProcessQueue: false,
        chunkSize: 1000000,             // chunk size 1,000,000 bytes (~1MB)
        retryChunks: true,              // retry chunks on failure
        timeout: 120000,
        dictDefaultMessage: "Drop documents here or import from",
        parallelUploads: 3,
        retryChunksLimit: 3,            // retry maximum of 3 times (default is 3)
        createImageThumbnails: true,    //  create thumbnails
        chunksUploaded: function (file, done) {

                $.ajax({
                    type: "POST",
                    url: "/Home/SubmitChunks/",
                    data: {
                        "uuid": file.upload.uuid,
                        'fileName': file.name
                    },
                    success: function (data) {

                        if (data.success) {

                            $("#finished-files").append(`<li>${data.message}</li>`);
                            done();
                        }
                        else {
                            dropZone._errorProcessing([file], msg.responseText);
                            done();
                        }


                    },
                    error: function (msg) {
                        dropZone._errorProcessing([file], msg.responseText);
                        console.log("error");
                        done();
                    }
                });

        },
        queuecomplete: function () {

            // turn off processor to stop files from immediate upload
            this.options.autoProcessQueue = false;
        }

    });
}
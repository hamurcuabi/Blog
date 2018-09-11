var xhrNV;
var IsAbort = false;
var IsAjaxStarted = false;

function StopUploadingFile() {
    IsAbort = true;
    IsAjaxStarted = false;
    xhrNV.abort();
    if (document.getElementById("NVBtnPageRefresh"))
    {
        $("#NVBtnPageRefresh").click();
    }
}

function CreateProgressBar() {
var html = '<div id="dvUploadProgressPercent" class="col-xs-11 progress progress-striped active pos-rel no-margin no-padding" style="background: rgba(185, 202, 189, 0.84) !important;" data-percent="0%">';
    html += '<div id="dvUploadProgress" class="progress-bar progress-bar-success" style="width: 0%"></div></div>';
    html += '<div class="col-xs-1" style="padding-left: 8px;"><a class="btn btn-minier btn-danger pull-left" onclick="StopUploadingFile()" style="border-top-width: 0px; border-bottom-width: 0px;"><i class="fa fa-times"></i></a></div>';

    $("#NVProgressBar").html(html);
    $("#NVProgressBar").css("display", "block");
}
//dosya silme
function DeleteFileInTemp() {
    if ($("#hdFileUploadValues").val().length > 0)
    {
        var fileInfo = $("#hdFileUploadValues").val().split("~");
        var fileName = '';
        for (var i = 1; i < fileInfo.length; i++)
        {
            fileName += fileInfo[i];
        }
        
        var JSONDATA = {};
        JSONDATA.FILEID = fileInfo[0];
        JSONDATA.FILENAME = fileName;
        $.ajax({
            type: "POST",
            url: "/WebService/SystemFile.asmx/DeleteFileInTemp",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(JSONDATA),
            success: function (response) {
                if (response.d == "OK") {
                    $("#hdFileUploadValues").val('');
                    if (document.getElementById("NVUploadButton"))
                    {
                        $("#NVUploadButton").css("display", "block");
                    }
                    if (document.getElementById("NVUploadInfoClient"))
                    {
                        $("#NVUploadInfoClient").css("display", "none");
                    }
                    if (document.getElementById("NVUploadInfoServer"))
                    {
                        $("#NVUploadInfoServer").css("display", "none");
                    }
                    if (document.getElementById("NVFileName"))
                    {
                        $("#NVFileName").text('');
                    }
                    $("#NVBtnPageRefresh").click();
                }
            },
            failure: function (response) {
                
            }
        });
    }
}

function ControlFileWithSystem(fileSize, fileExtension) {
    var retval = true;
    var FileMaxQuota = parseFloat($("#hdFileMaxQuota").val().replace(',', '.'));
    var FileMaxUploadMaxSize = parseFloat($("#hdFileMaxUploadMaxSize").val().replace(',', '.'));
    var FileMaxUploadMaxSizeSystem = parseFloat($("#hdFileMaxUploadMaxSizeSystem").val().replace(',', '.'));
    var FileRemainingQuota = parseFloat($("#hdFileRemainingQuota").val().replace(',','.'));
    var FileUploadExtentions = $("#hdFileUploadExtentions").val().toLowerCase();
    var Extension = fileExtension.substring(1,fileExtension.length).toLowerCase();

    if (FileMaxQuota <= FileRemainingQuota) {
        retval = false;
        $("#hdFileErrorMessage").val('GNRLFileUploadQuota');
    }
    else if (FileMaxUploadMaxSize > FileMaxUploadMaxSizeSystem) {
        retval = false;
        $("#hdFileErrorMessage").val('GNRLFileUploadSystemSize');
    }
    else if (FileMaxUploadMaxSize < fileSize) {
        retval = false;
        $("#hdFileErrorMessage").val('GNRLFileUploadSize');
    }
    else if ((FileMaxQuota - FileRemainingQuota) < fileSize) {
        retval = false;
        $("#hdFileErrorMessage").val('GNRLFileUploadQuota');
    }
    else if (FileUploadExtentions.indexOf(Extension) == -1) {
        retval = false;
        $("#hdFileErrorMessage").val('GNRLFileUploadExtension~' + Extension);
    }
    return retval;
}

//dosya yükleme
function UploadFile(SYSTEMID, OTHERID, DOCTYPE) {
    var changeEventBinded = false;
    var responseSuccess;
    if (!changeEventBinded) {
        $('#NVFileUploader').change(function () {
            var input = document.getElementById('NVFileUploader');
            if (!input) {
                write("imgfile elementi bulunamadi");
            }
            else if (!input.files) {
                write("browser desteği ile ilgili sorun var");
            }
            if (input.files.length > 0) {
                var fileSize = (input.files[0].size / (1024 * 1024)); 
                var fileExtension = input.files[0].name.substring(input.files[0].name.lastIndexOf("."));

                if (ControlFileWithSystem(fileSize, fileExtension)) {//Client Control: hidden value'lerdeki değerler ile dosyanın içeriği, sistem paremetreleri ile uyuşuyor mu. 
                    var JSONPOSTDATA = {};
                    JSONPOSTDATA.SYSTEMID = SYSTEMID;
                    JSONPOSTDATA.OTHERID = OTHERID;
                    JSONPOSTDATA.EXTENSION = fileExtension;
                    JSONPOSTDATA.FILESIZE = fileSize;
                    JSONPOSTDATA.DOCTYPE = DOCTYPE;
                    $.ajax({//Server Control: dosyanın içeriği serverdaki bilgiler ile uyuşuyor mu
                        type: "POST",
                        url: "/WebService/SystemFile.asmx/GetSystemFileInfo",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: JSON.stringify(JSONPOSTDATA),
                        success: function (response) {//eğer uyuşuyorsa FileUploadStatus = true olacaktır
                            var JSONGETDATA = JSON.parse(response.d);
                            if (JSONGETDATA.FileUploadStatus) {
                                CreateProgressBar();

                                var form = $("form#NVFileUploadForm").ajaxForm({//dosya yükleme formu tetikleniyor
                                    beforeSend: function () {
                                        if (document.getElementById("NVUploadButton")) {
                                            $("#NVUploadButton").css("display", "none");
                                        }
                                        if (document.getElementById("NVUploadInfoClient")) {
                                            $("#NVUploadInfoClient").css("display", "none");
                                        }
                                        if (document.getElementById("NVUploadInfoServer")) {
                                            $("#NVUploadInfoServer").css("display", "none");
                                        }
                                        if (document.getElementById("NVUploadBlockPart")) {
                                            $("#NVUploadBlockPart").css("display", "none");
                                        }
                                        if (document.getElementById("btnCloseModal")) {
                                            $("#btnCloseModal").css("display", "none");
                                        }
                                        responseSuccess = false;
                                        IsAbort = false;
                                        IsAjaxStarted = true;
                                    },
                                    uploadProgress: function (olay, yuklenen, toplam, yuzde) {
                                        var Color = ChangeColor(yuzde);
                                        $(".progress-bar-success").css('background-color', Color);
                                        $("#dvUploadProgressPercent").attr('data-percent', yuzde + "%");
                                        $("#dvUploadProgress").css("width", yuzde + "%");
                                    },
                                    success: function (resp, status, xhr, wrapped) {
                                        var fileID = '';
                                        var fileName = '';
                                        var responseFile;
                                        if (resp.length <= 3 && resp == "NOK") {
                                            responseSuccess = false;
                                            $("#hdFileUploadValues").val('');
                                        } else if (resp != "NOK") {
                                            responseSuccess = true;
                                            responseFile = resp.split("~");
                                            fileID = responseFile[1];
                                            for (var i = 2; i < responseFile.length; i++) {
                                                fileName += responseFile[i];
                                            }
                                            $("#hdFileUploadValues").val(fileID + '~' + fileName);
                                            if (fileName.length > 30) {
                                                $("#NVFileName").text(fileName.substring(0, 29));
                                            } else {
                                                $("#NVFileName").text(fileName);
                                            }
                                        }
                                    },
                                    complete: function (xhr) {
                                        $("#NVProgressBar").text('');
                                        $("#NVProgressBar").css("display", "none");
                                        if (!IsAbort) {//dosya yükleme işlemi, yükleme sırasında iptal edilmemiş ise
                                            if (!responseSuccess) {//dosya yüklenememişse
                                                if (document.getElementById("NVUploadButton")) {
                                                    $("#NVUploadButton").css("display", "block");
                                                }
                                                $("#hdFileErrorMessage").val('GNRLFileUploadError');
                                                $("#NVSendError").click(); // Hata mesaj butonu tıklanır
                                            } else { //dosya doğuru yüklenmişse, ekranda gösterilir
                                                if (document.getElementById("NVUploadInfoClient")) {
                                                    $("#NVUploadInfoClient").css("display", "block");
                                                }
                                            }
                                        } else {//dosya yükleme işlemi, yükleme sırasında iptal edilmiş ise
                                            $("#NVUploadButton").css("display", "block");
                                        }

                                        if (document.getElementById("NVUploadBlockPart")) {
                                            $("#NVUploadBlockPart").css("display", "block");
                                        }
                                        if (document.getElementById("btnCloseModal")) {
                                            $("#btnCloseModal").css("display", "block");
                                        }
                                        changeEventBinded = true;
                                        IsAjaxStarted = false;
                                        if (document.getElementById("NVUploadClickTrigger")) {
                                            $("#NVUploadClickTrigger").click();
                                        }
                                    },
                                    resetForm: true,
                                    clearForm: true
                                }).submit();
                                xhrNV = form.data('jqxhr');
                            }
                            else
                            {//server tarafından dosya yüklenemez, durumu
                                $("#hdFileErrorMessage").val('GNRLFileUploadError');
                                $("#NVSendError").click();
                            }
                        },
                        failure: function (response) {
                            
                        }
                    });
                }
                else
                {
                    $("#NVSendError").click();
                }
            }
        });
    }
    $('#NVFileUploader').trigger('click');
}

//dosya renk değişikliği
var ColorArray = ['#428bca', '#4192c9', '#419cc9', '#41a7c9', '#41b2c9', '#41bec9', '#41c9c9', '#41c9c0', '#41c9b5', '#41c9a9', '#41c99e', '#41c992', '#41c989', '#41c97e', '#41c973', '#41c967', '#41c95c', '#41c953', '#41c947', '#45c941'];
function ChangeColor(percent) {
    var index = 0;
    var Color;
    if (percent < 5) index = 0;
    else if (percent < 10) index = 1;
    else if (percent < 15) index = 2;
    else if (percent < 20) index = 3;
    else if (percent < 25) index = 4;
    else if (percent < 30) index = 5;
    else if (percent < 35) index = 6;
    else if (percent < 40) index = 7;
    else if (percent < 45) index = 8;
    else if (percent < 50) index = 9;
    else if (percent < 55) index = 10;
    else if (percent < 60) index = 11;
    else if (percent < 65) index = 12;
    else if (percent < 70) index = 13;
    else if (percent < 75) index = 14;
    else if (percent < 80) index = 15;
    else if (percent < 85) index = 16;
    else if (percent < 90) index = 17;
    else if (percent < 95) index = 18;
    else if (percent <= 100) index = 19;
    return Color = ColorArray[index];
}
function SendFileMessage(MessageType) {
    var msg = '';
    if (MessageType == 'Error') {
        msg = LOCALIZEDSTRINGS.FailUploading['_' + GetCurrentCulture().toUpperCase()];
        ShowMessage('Error', htmlEncode(msg), '', 3000);
    }
}
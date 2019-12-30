var upladViewModule = function () {
    var self = this;
    self.FileArray = ko.observableArray([]);
    self.DirectoryUploadArray = ko.observableArray([]);
    self.uploadFile = function () {
        var count = 1;
        var flag;
        for (var i = 0; i <= self.FileArray().length; i++) {
            count = parseInt(i) + parseInt(1);
        }
        var fileUpload = $("#fileUpload").get(0);
        var files = fileUpload.files;
        if (files[0].type == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" || files[0].type == "application/vnd.ms-excel") {
            var UploadFileName = "";
            var data = new FormData();
            UploadFileName = files[0].name;
            for (var i = 0; i < files.length; i++) {
                data.append(files[i].name = UploadFileName, files[i]);
            }
        }
        else {
            msg("Not supported file found !!!", "ALERT");
            return false;
        }
        var flag = "";
        $.ajax({
            url: "/Handlers/FileUpload.ashx",
            type: "POST",
            async: false,
            data: data,
            contentType: false,
            processData: false,
            success: function (result) {
                if (result == "Invalid File!") {
                    flag = result;
                    msg(result, "ALERT");
                    flag = "1";
                }
                else {
                    console.log(result);
                }
            },
            error: function (err) {
                msg("Invalid File!!!", "ALERT")
            }
        });
    }
    self.onFileSelectedEvent = function (vm, evt) {
        ko.utils.arrayForEach(evt.target.files, function (file) {
            self.DirectoryUploadArray.push(file.name, file);
        });
    }
};



$(document).ready(function () {
   ko.applyBindings(new upladViewModule());
});

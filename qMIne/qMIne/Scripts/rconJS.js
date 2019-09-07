$("#btnClearLog").click(function () {
    $("#ulconsole").empty();
});

function logSuccess(Data) {

    datetime = getCurrentTime();
    datetime += " <- ";
    var fullLog = datetime + Data;
    $("#ulconsole").append('<li class="list-group-item list-group-item-info" >' + fullLog + '</li>');
    $("#btnSend").prop('disabled', false);
    clearOldLogs();
    if ($("#chkAutoScroll").is(':checked')) scrollLogsDown();
}

function logError(Data) {
    datetime = getCurrentTime();
    datetime += " <- ";
    datetime += "Error!";
    $("#ulconsole").append('<li class="list-group-item list-group-item-danger">' + Data + '</li>');
    $("#btnSend").prop('disabled', false);
    clearOldLogs();
    if ($("#chkAutoScroll").is(':checked')) scrollLogsDown();
}

function logWarning(Data) {
    datetime = getCurrentTime();
    datetime += " <- ";
    var fullLog = datetime + Data;
    $("#ulconsole").append('<li class="list-group-item list-group-item-warning">' + fullLog + '</li>');
    $("#btnSend").prop('disabled', false);
    clearOldLogs();
    if ($("#chkAutoScroll").is(':checked')) scrollLogsDown();
}

function clearOldLogs() {
    var logItemSize = $("#ulconsole li").size;
    if (logItemSize > 50) {
        $('#ulconsole li:first').remove();
    }
}

function getCurrentTime() {
    var currentdate = new Date();
    var datetime = currentdate.getDate() + "/"
        + (currentdate.getMonth() + 1) + "/"
        + currentdate.getFullYear() + " @@ "
        + currentdate.getHours() + ":"
        + currentdate.getMinutes() + ":"
        + currentdate.getSeconds();
    return datetime;
}

function scrollLogsDown() {
    $("#ulconsole").scrollTop($("#ulconsole").get(0).scrollHeight);
}
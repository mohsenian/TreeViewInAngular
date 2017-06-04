function GetParent(_subMainId) {
    var procemessage = "<option value='0'> Please wait...</option>";
    $("#childDropDown").html(procemessage).show();
    var url = "/Home/GetParentByParentId/";

    $.ajax({
        url: url,
        data: { subMainId: _subMainId },
        cache: false,
        type: "POST",
        success: function (data) {
            var markup = "<option value='0'>Select Car Model</option>";
            for (var x = 0; x < data.length; x++) {
                markup += "<option value=" + data[x].Value + ">" + data[x].Text + "</option>";
            }
            $("#childDropDown").html(markup).show();
        },
        error: function (reponse) {
            alert("error : " + reponse);
        }
    });
}
    $('#colorpalettediv').colorPalettePicker({
        bootstrap: 3,
        lines: 3,
        palette: ["azure", "beige", "darkkhaki", "gold", "khaki", "lightblue", "lightcyan", "lightyellow"],
        onSelected: function (color) {
            document.getElementById('ColorProject').value = color;
        }
    });

$(document).ready(function() {
    $("#alertMessageDiv").hide();
    $("#btnCreate").click(function() {
        var data = $("#myForm").serialize();
        $.ajax({
            type: "POST",
            url:   'Home/CreateProject'  ,//'@Url.Action("CreateProject", "Home")',
            data: data,
            success: function(result) {
                if (!result.hasError) {
                    location.href = 'Home/Index'; //'@Url.Action("Index", "Home")';
                } else {
                    $("#alertMessageDiv").html(result.message);
                    $("#alertMessageDiv").show();
                }
            }
        });
    });
});

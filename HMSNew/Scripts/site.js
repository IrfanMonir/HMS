$(".changeAccomodationType").click(function () {
    var accomodationTypeId = $(this).attr("data-id");
    $(".accomodationTypesRow").hide();
    $("div.accomodationTypesRow[data-id=" + accomodationTypeId + "]").show();

});
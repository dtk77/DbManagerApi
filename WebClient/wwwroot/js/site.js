// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(function () {

    var PlaceHolderElement = $('#PlaceHolderHere');
    $('[data-toggle="ajax-modal"]').click(function (event) {

        var url = $(this).data('url');
        $.get(url).done(function (data) {
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('.modal').modal('show');
        });
    });

    PlaceHolderElement.on('click', '[data-dismiss="modal"]', function (event) {

        PlaceHolderElement.find('.modal').modal('hide');
    });

    $('[data-toggle="filter"]').change(function (event) {

        var e = document.getElementById("filter");
        var nameProduct = e.options[e.selectedIndex].value;
        location.href = 'Products?nameProduct=' + nameProduct;
    })
});
$(function () {
    //jquery ui autocomplete
    var ajaxFormSubmit = function () {
        var $form = $(this);

        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        };

        $.ajax(options).done(function (data) {
            var $target = $($form.attr("data-otf-target"));
            var $newHtml = $(data);
            $target.replaceWith($newHtml);
            $newHtml.effect("highlight");
        });

        return false;
    };
    //when the users click on the autocomplete result it will be submitted
    var submitAutocompleteForm = function (event, ui) {
        var $input = $(this);
        $input.val(ui.item.label);
        //find the form
        var $form = $input.parents("form:first");
        $form.submit();
    }    

    var createAutocomplete = function () {
        var $input = $(this);

        var options = {
            source: $input.attr("data-otf-autocomplete"),
            select: submitAutocompleteForm
        };

        $input.autocomplete(options);
    };


    //reload the pageList portion only (asynchoronous)
    var getPage = function () {
        //this refer anchor tag that the user click
        var $a = $(this);

        var options = {
            //get the page value on click
            url: $a.attr("href"),
            data: $("form").serialize(),
            type: "get"
        };

        $.ajax(options).done(function (data) {
            var target = $a.parents("div.pagedList").attr("data-otf-target");
            $(target).replaceWith(data);
        });

        return false;
    };

    //reload only partial of the html, so that the input field dont looses the keyword that it searches.
    $("form[data-otf-ajax='true']").submit(ajaxFormSubmit);
    //auto complete the search field
    $("input[data-otf-autocomplete]").each(createAutocomplete);
    //wire a click event on anchor tag, in pageList
    $(".body-content").on("click", ".pagedList a", getPage);
});
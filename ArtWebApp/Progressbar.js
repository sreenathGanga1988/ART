//function ShowProgress() {
//    setTimeout(function () {

//        debugger;
//        var modal = $('<div />');
//        modal.addClass("modal");
//        $('body').append(modal);
//        var $div = $('<div align="center"> Loading. Please wait.<br />  <br />  <img src="/loader.gif" alt=""</div>').appendTo('body');
//        $div.addClass("loading");
//        var loading = $(".loading");       
//        loading.show();
//        var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
//        var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
//        loading.css({ top: top, left: left });
//    }, 200);
//}
//$(document).ready(function () {

//    debugger;
//    //$('form').on("submit", function () {
//    //    ShowProgress();
//    //});
//    $(document).on('submit', 'form', function () {
//        ShowProgress();
//    });
//});
daisyApp.factory('Func', function($location) {

    function redirect(path){
        $location.path('/'+path+'');
    }

    function alert(type, message) {
        //alerts Type: info, warning, success, danger
        window.scrollTo(0, 0);
        $('#alerts').append($('<div id="addSuccess" class="alert alert-' + type + '">' + message + '</div>'));
        $('#alerts').children().fadeIn().delay(3000).fadeOut('slow', function () {
            $(this).remove();
        });
    }

    return{
        redirect: redirect,
        alert:alert
    }
});
daisyApp.controller('PosterCtrl', function ($scope, $rootScope, Func, requester) {
    var type = $rootScope.type;
    var whereReq = type ? {where:{"category": type}}  : {};
    $rootScope.isLoad = false;

    requester.getPosters(
        whereReq,
        function (data) {
            $rootScope.posters = data;
            Func.alert('success', 'Get Posters Success. Can see it in HOME tab.');
            $rootScope.isLoad = true;
        },
        function (error) {
            Func.alert('danger', 'Get Posters failed. Please try again later.');
        }
    );
});
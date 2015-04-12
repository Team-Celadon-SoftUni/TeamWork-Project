daisyApp.controller('NewUserController', function ($scope, $rootScope, Func, requester) {

    $scope.register = function(newUser) {
        requester.register(
            newUser,
            function () {
                Func.alert('success', 'Get Posters Success. Can see it in HOME tab.');
                Func.redirect('home');
            },
            function (error) {
                Func.alert('danger', 'Get Posters failed. Please try again later.');
            }
        );
    }
});
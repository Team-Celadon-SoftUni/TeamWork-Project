daisyApp.factory('requester', function requester($http) {
    var content = 'application/json';
    var rootURL = 'http://faqsystem.apphb.com/';

    function request(method, path, data, success, error, params) {
        console.log(rootURL + path);
        $http({
                method: method,
                data: JSON.stringify(data),
                content: content,
                params: params,
                url: rootURL + path
            }
        )
            .success(function (data, status, headers, config) {
                success(data, status, headers(), config);
            })
            .error(function (data, status, headers, config) {
                error(data, status, headers(), config);
            }
        );
    }

    function register(data, success, error) {
        console.log(data);
        request("POST", "api/Account/Register", data, success, error);
    }

    function login(data, success, error) {
        request("GET", "login", null, success, error, data);
    }

    function addPoster(data, success, error) {

        request("POST", "classes/Poster", data, success, error);
    }

    function getPosters(query, success, error) {
        request("GET", "classes/Poster", null, success, error, query);
    }

    return {
        register: register,
        login: login,

        addPoster: addPoster,
        getPosters: getPosters
    }
});
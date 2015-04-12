daisyApp.factory('requester', function requester($http) {
    var content = 'application/json';
    var rootURL = 'http://localhost:32227/';
    $http.defaults.headers.put = {
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Methods': 'GET, POST, PUT, DELETE, OPTIONS',
        'Access-Control-Allow-Headers': 'Content-Type, X-Requested-With'
    };
    function request(method, path, data, success, error, params) {
        console.log(rootURL + path);
        $http({
                method: method,
                headers: {
                    "Cache-Control": "no-cache",
                    "Content-Length": "58",
                    "Content-Type": "application/json; charset=utf-8",
                    "Date": "Sun, 12 Apr 2015 19:06:42 GMT",
                    "Expires": "-1",
                    "Pragma": "no-cache",
                    "Server": "Microsoft-IIS/8.0",
                    "X-AspNet-Version": "4.0.30319",
                    "X-Powered-By": "ASP.NET",
                    "X-SourceFiles": "=?UTF-8?B?RDpcU29mdHdhcmVVbml2ZXJzaXR5XEdpVFByb2plY3RzXFdlYlNlcnZpY2VBbmRDbG91ZC1Qcm9qZWN0XHRydW5rXCFTb2Z0VW5pRkFRU3lzdGVtXFNvZnRVbmlGQVFTeXN0ZW0uV2ViXGFwaVxBY2NvdW50XFJlZ2lzdGVy?="
                },
                content: content,
                data: JSON.stringify(data),
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
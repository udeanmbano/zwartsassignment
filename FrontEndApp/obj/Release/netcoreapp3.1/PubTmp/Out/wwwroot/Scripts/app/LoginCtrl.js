var app = angular.module('app');
app.controller('LoginController', function ($window,$http,$scope,$location, $rootScope, $stateParams, $state, LoginService) {
    $rootScope.title = "FrontEnd Application";
    $rootScope.token = "";
    $rootScope.token_type ="";
    $rootScope.expires_in ="";

    $scope.formSubmit = function () {
        console.log("truus" + $scope.username);
        var loginUrl = "";
        var body = new FormData();
       var username=$scope.username;
       var password= $scope.password;
        var request = $http({
            method: "Post",
            url: "http://localhost/ZwartsApi/api/authenticate/login",
            headers: { 'Content-Type': 'application/json' },
            data: {'username':username,'password':password}
        });
        console.log("body" + JSON.stringify(body));

        request.success(function (data) {
            var response = data;
            console.log("truus" + JSON.stringify(response));
            if (response != '' || response != undefined && response != '') {
                $scope.error = '';
                $scope.username = '';
                $scope.password = '';
                $rootScope.token = response.token;
                $rootScope.userId = response.userId;
                $rootScope.expires_in = response.expires_in;
                $window.localStorage.setItem("token", $rootScope.token);
                $window.localStorage.setItem("userId", $rootScope.userId);
                $window.localStorage.setItem("apiUrl", "http://localhost/ZwartsApi/api");
                if ($rootScope.token != '' && $rootScope.token != undefined) {
                    console.log("truus" + LoginService.login($scope.username, $scope.password));
                    $state.transitionTo('todolists');
                } else {
                    $scope.error = "Incorrect username/password !";
                }
            } else {
                $scope.error = "Incorrect username/password !";
            }
        
        }).error(function (data) {
            console.log("error" + JSON.stringify(data));
            $scope.error = "Incorrect username/password !";

        });

       
    };
});
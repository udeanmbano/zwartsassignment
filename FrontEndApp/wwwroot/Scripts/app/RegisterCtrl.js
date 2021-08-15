var app = angular.module('app');
app.controller('RegisterController', function ($window,$http,$scope,$location, $rootScope, $stateParams, $state, LoginService) {
    $rootScope.title = "AngularJS Login Sample";
    $rootScope.token = "";
    $rootScope.token_type ="";
    $rootScope.expires_in ="";

    $scope.formSubmit = function () {
        console.log("truus" + $scope.username);
        var loginUrl = "http://localhost/SimplicityApi/token";
        $http.post(loginUrl,`username=${$scope.username}` +
            `&password=${$scope.password}` +
            '&grant_type=password'
        ).success(function (data) {
            var response = data;
            console.log("truus" + response);
            if (response != '' || response != undefined && response!='') {
                $scope.error = '';
                $scope.username = '';
                $scope.password = '';
                $rootScope.token = response.access_token;
                $rootScope.token_type = response.token_type;
                $rootScope.expires_in = response.expires_in;
                $window.localStorage.setItem("token", $rootScope.token);
                $window.localStorage.setItem("apiUrl","http://localhost/SimplicityApi/api");
                if ($rootScope.token != '' && $rootScope.token != undefined) {
                    console.log("truus" + LoginService.login($scope.username, $scope.password));
                    $state.transitionTo('vehicles');
                } else {
                    $scope.error = "Incorrect username/password !";
                }
            } else {
                $scope.error = "Incorrect username/password !";
            }
        }).error(function (data) {
            console.log("truus" + data.error);
            $scope.error = "Incorrect username/password !";
           // $scope.error = "An Error has occured while Adding Vehicle! " + data;
           // $scope.loading = false;
        });
      

       
    };
});
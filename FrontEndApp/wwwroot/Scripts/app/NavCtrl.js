var app = angular.module('app');
app.controller('NavController', function ($window,$http,$scope,$location, $rootScope, $stateParams, $state, LoginService) {
    $rootScope.title = "FrontEnd Application";
    $scope.login = function () {
        $state.transitionTo('login');
    };
    $scope.register = function () {
        $state.transitionTo('register');
    };
});
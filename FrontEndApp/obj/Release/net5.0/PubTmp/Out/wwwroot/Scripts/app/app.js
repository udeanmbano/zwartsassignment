var app = angular.module('app', ['ui.router', 'ngIdle', 'ngSanitize', 'ui.bootstrap',]);
console.log(window.location.origin);
angular.module('app').config(['KeepaliveProvider', 'IdleProvider', function (KeepaliveProvider, IdleProvider) {
    // configure Idle settings
    IdleProvider.idle(60 * 60);
    IdleProvider.timeout(5);
    KeepaliveProvider.interval(10);
    IdleProvider.interrupt('keydown wheel mousedown touchstart touchmove scroll');
}]);
app.run(function ($rootScope, $location, $state, LoginService) {
    console.clear();
    console.log('running');
    if (!LoginService.isAuthenticated()) {
        console.log('no login');
        $state.transitionTo('login');
    }
});
app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider',
    function ($stateProvider, $urlRouterProvider,$locationProvider) {
        $stateProvider
            .state('login', {
                url: '/login',
                templateUrl: './Login',
                controller: 'LoginController'
            })
            .state('register', {
                url: '/register',
                templateUrl:'./Register',
                controller: 'RegisterController'
            })
            .state('todolists', {
                url: '/todolists',
                templateUrl:'./ToDoList',
                controller: 'ToDoListController'
            })
             .state('todolistsitems', {
          url: '/todolistsitems',
                 templateUrl:'./ToDoListItem',
                 controller: 'ToDoListItemController'
            });
        $urlRouterProvider.otherwise('/login');
    }]);
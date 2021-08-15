var app = angular.module('app');
app.controller('ToDoListController', function ($window, $scope, $http, $rootScope, $stateParams, $state, LoginService, Idle, Keepalive, $uibModal) {


    //when login then call below function
    Idle.watch();
    $scope.$on('IdleTimeout', function () {
        $window.localStorage.clear();
        //Logout function or redirect to logout url
        $state.transitionTo('login');
    });
    //declare variable for mainain ajax load and entry or edit mode
    $scope.loading = true;
    $scope.addMode = false;
    var apiUrl = $window.localStorage.getItem("apiUrl");
    $rootScope.token = $window.localStorage.getItem("token");
    $rootScope.userId = $window.localStorage.getItem("userId");
    console.log('token is' + $rootScope.token);
    console.log('user is' + $rootScope.userId);
    console.log('api url is' + apiUrl);
    var token = $rootScope.token;
    var userId = $window.localStorage.getItem("userId");

    var request = $http({
        method: "get",
        headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json' },
        url: apiUrl + "/ToDoLists/" + userId
    });

    //get all vehicle information
    request.success(function (data) {
        if (data.toDoLists != null) {
            $scope.ToDoLists = data.toDoLists;
        } else {
            $scope.error = "No ToDoLists available!";

        }
        $scope.loading = false;
    })
        .error(function () {
            $scope.error = "An Error has occured while loading vehicles!";
            $scope.loading = false;
        });

    //by pressing toggleEdit button ng-click in html, this method will be hit
    $scope.toggleEdit = function () {
        this.ToDoLists.editMode = !this.ToDoLists.editMode;
    };

    //by pressing toggleAdd button ng-click in html, this method will be hit
    $scope.toggleAdd = function () {
        $scope.addMode = !$scope.addMode;
    };

    //Insert ToDoList

    $scope.add = function () {
        this.newToDoLists.userId = $rootScope.userId;
        $scope.loading = true;
        var request = $http({
            method: "post",
            headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json' },
            url: apiUrl + "/PostToDoList",
            data: this.newToDoLists
        });

        request.success(function (data) {
            console.log(request);
            alert("Added Successfully!!");
            $scope.form = {};
            this.newToDoLists = {};
            document.getElementById("addToDoLists").reset();
            $scope.addToDoLists.$setPristine(true);
            $scope.addToDoLists.$setUntouched();

            $scope.addMode = false;
            loadAllToDoLists();
            $scope.loading = false;


        }).error(function (data) {
            $scope.error = "An Error has occured while Adding Vehicle! " + data;
            $scope.loading = false;
        });
    };

    //Edit ToDoList
    $scope.save = function () {
        $scope.loading = true;
        this.ToDoLists.userId = $rootScope.userId;
        var request = $http({
            method: "put",
            headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json' },
            url: apiUrl + "/PutToDoList/" + this.ToDoLists.id,
            data: this.ToDoLists
        });

        request.success(function (data) {
            alert("Saved Successfully!!");
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Saving todo list! " + JSON.stringify(data);
            $scope.loading = false;
        });
    };

    //Delete ToDoList
    $scope.deleteToDoLists = function () {
        $scope.loading = true;
        var Id = this.ToDoLists.id;
        var request = $http({
            method: "delete",
            headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json' },
            url: apiUrl + "/DeleteToDoList/" + Id
        });

        request.success(function (data) {
            alert("Deleted Successfully!!");
            $.each($scope.ToDoLists, function (i) {
                if ($scope.ToDoLists[i].Id === Id) {
                    $scope.ToDoLists.splice(i, 1);
                    return false;
                }
            });
            loadAllToDoLists();
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Saving todolist! " + JSON.stringify(data);
            $scope.loading = false;
        });
    };

    //Go to Items ToDoList
    $scope.ItemToDoLists = function () {
        $scope.loading = true;
        var Id = this.ToDoLists.id;
        $window.localStorage.setItem("listId", this.ToDoLists.id);
        $window.localStorage.setItem("listName", this.ToDoLists.name);
        $state.transitionTo('todolistsitems');

    };
    function loadAllToDoLists() {
        var request = $http({
            method: "get",
            headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json' },
            url: apiUrl + "/ToDoLists/" + userId
        });

        //get all ToDoList information
        request.success(function (data) {
            $scope.ToDoLists = data.toDoLists;
            $scope.loading = false;
        }).error(function () {
            $scope.error = "An Error has occured while loading posts!";
            $scope.loading = false;
        });
    };



});

angular.module('app').controller('ModalInstanceCtrl', function (data) {
    var pc = this;
    pc.title = data;
});
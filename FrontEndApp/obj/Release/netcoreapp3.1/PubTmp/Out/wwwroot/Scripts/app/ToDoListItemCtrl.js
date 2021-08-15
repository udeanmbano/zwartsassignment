var app = angular.module('app');
app.controller('ToDoListItemController', function ($window, $scope, $http, $rootScope, $stateParams, $state, LoginService, Idle, Keepalive, $uibModal) {


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
    $rootScope.listId = $window.localStorage.getItem("listId");
    $rootScope.listName = $window.localStorage.getItem("listName");

    console.log('token is' + $rootScope.token);
    console.log('list id' + $rootScope.listId);
    console.log('api url is' + apiUrl);
    var token = $rootScope.token;
    var listId = $window.localStorage.getItem("listId");

    var request = $http({
        method: "get",
        headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json' },
        url: apiUrl + "/ToDoListItems/" + listId
    });

    //get all vehicle information
    request.success(function (data) {
        if (data.toDoListItems != null) {
            $scope.ToDoListItems = data.toDoListItems;
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
        this.ToDoListItems.editMode = !this.ToDoListItems.editMode;
    };

    //by pressing toggleAdd button ng-click in html, this method will be hit
    $scope.toggleAdd = function () {
        $scope.addMode = !$scope.addMode;
    };

    //Insert ToDoList

    $scope.add = function () {
        this.newToDoListItems.toDoListId = parseInt($rootScope.listId);
        $scope.loading = true;
        var request = $http({
            method: "post",
            headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json' },
            url: apiUrl + "/PostToDoListItem",
            data: this.newToDoListItems
        });
     console.log("log "+JSON.stringify(this.newToDoListItems));
       
        request.success(function (data) {
            console.log(request);
            alert("Added Successfully!!");
            $scope.form = {};
          this.newtoDoListItems = {};
            document.getElementById("addToDoListItems").reset();
            $scope.addMode = false;
            loadAlltoDoListItems();
            $scope.loading = false;


        }).error(function (data) {
            $scope.error = "An Error has occured while Adding to do list items! " + JSON.stringify(data);
            $scope.loading = false;
        });
    };

    //Edit ToDoList
    $scope.save = function () {
        $scope.loading = true;
        this.ToDoListItems.toDoListId = parseInt($rootScope.listId);
        var request = $http({
            method: "put",
            headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json' },
            url: apiUrl + "/PutToDoListItem/" + this.ToDoListItems.toDoListItemId,
            data: this.ToDoListItems
        });

        request.success(function (data) {
            alert("Saved Successfully!!");
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Saving todo list! " + JSON.stringify(data);
            $scope.loading = false;
        });
    };
    $scope.markToDoListItems = function () {
        $scope.loading = true;
        this.ToDoListItems.toDoListId = parseInt($rootScope.listId);
        var request = $http({
            method: "put",
            headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json' },
            url: apiUrl + "/MarkToDoListItem/" + this.ToDoListItems.toDoListItemId,
            data: this.ToDoListItems
        });

        request.success(function (data) {
            alert("Marked as done successfully!!");
            $scope.loading = false;
            loadAlltoDoListItems();
        }).error(function (data) {
            $scope.error = "An Error has occured while Saving todo list! " + JSON.stringify(data);
            $scope.loading = false;
        });
    };
    //Delete ToDoList
    $scope.deletetoDoListItems = function () {
        $scope.loading = true;
        var Id = this.ToDoListItems.toDoListItemId;
        var request = $http({
            method: "delete",
            headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json' },
            url: apiUrl + "/DeleteToDoListItem/" + Id
        });

        request.success(function (data) {
            alert("Deleted Successfully!!");
            $.each($scope.ToDoListItems, function (i) {
                if ($scope.ToDoListItems[i].Id === Id) {
                    $scope.ToDoListItems.splice(i, 1);
                    return false;
                }
            });
            loadAlltoDoListItems();
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Saving todolist! " + JSON.stringify(data);
            $scope.loading = false;
        });
    };


    function loadAlltoDoListItems() {
        var request = $http({
            method: "get",
            headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json' },
            url: apiUrl + "/ToDoListItems/" + listId
        });

        //get all ToDoList information
        request.success(function (data) {
            $scope.ToDoListItems = data.toDoListItems;
            $scope.loading = false;
        }).error(function () {
            $scope.error = "An Error has occured while loading posts!";
            $scope.loading = false;
        });
    };
    $scope.back = function () {
        $state.transitionTo('todolists');
    };



});

angular.module('app').controller('ModalInstanceCtrl', function (data) {
    var pc = this;
    pc.title = data;
});
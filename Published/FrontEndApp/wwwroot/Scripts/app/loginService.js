var app = angular.module('app');
app.factory('LoginService', function ($http) {

	var isAuthenticated = false;
	return {
		login: function (username, password) {
			isAuthenticated = true;
			return isAuthenticated;
		},
		isAuthenticated: function () {
			return isAuthenticated;
		}
	};
});
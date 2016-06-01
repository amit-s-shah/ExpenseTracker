(function (module) {
    'use strict';

    module
        .controller('userRegCtrl', userRegCtrl);

    userRegCtrl.$inject = ['apiService', 'notificationService', '$location'];

    function userRegCtrl(apiService, notificationService, $location) {
        var _this = this;

        _this.register = register;

        function register() {

            apiService.postData('Account/register', _this.user, registrationSuccess, registrationFail);
        }

        function registrationSuccess(response) {
            notificationService.displaySuccess(_this.user.username + ' is registered.');
            $location.path('/login');
        }

        function registrationFail(error) {

            notificationService.displayError(error.data);
        }

    }
})(angular.module('ExpenseTracker'));

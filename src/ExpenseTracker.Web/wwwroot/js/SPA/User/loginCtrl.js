(function (module) {
    'use strict';

    module
        .controller('loginCtrl', loginCtrl);

    loginCtrl.$inject = ['accountMangement', 'notificationService', '$cookieStore', '$rootScope', '$location'];

    function loginCtrl(accountMangement, notificationService, $cookieStore, $rootScope, $location) {
        /* jshint validthis:true */
        var _this = this;
        _this.login = login;

        function login() {
            accountMangement.login(_this.user, loginSuccess, loginFailuer);
        }

        function loginSuccess(response) {
            if (response.data) {
                accountMangement.saveUserDetails(_this.user);
                notificationService.displaySuccess(_this.user.userName + ' is logged in');
                if ($rootScope.previousPath) {
                    $location.path($rootScope.previousPath)
                    $rootScope.previousPath = "";
                }
                else
                    $location.path('/');
            }
            else
            {
                notificationService.displayError('Invalid user name or password');
            }
        }

        function loginFailuer(error) {
            notificationService.displayError(error.data);
        }
    }
})(angular.module('ExpenseTracker'));

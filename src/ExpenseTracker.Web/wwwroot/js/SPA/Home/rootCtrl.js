(function (module) {
    'use strict';

    module.controller('rootCtrl', rootCtrl);

    rootCtrl.$inject = ['accountMangement'];

    function rootCtrl(accountMangement) {

        var _this = this;
        _this.userData = {};

        _this.displayUserInfo = displayUserInfo;
        _this.isUserloggedIn = isUserloggedIn;
        _this.logout = logout;

        function displayUserInfo() {
            _this.userData = accountMangement.getUserDetails();
        }

        function isUserloggedIn() {
            displayUserInfo();
            return accountMangement.isUserloggedIn();
        }

        function logout() {
            accountMangement.logout(null, logoutSuccess, logoutFailuer);
        }

        function logoutSuccess() {

        }

        function logoutFailuer() {

        }
    }
})(angular.module('ExpenseTracker'));

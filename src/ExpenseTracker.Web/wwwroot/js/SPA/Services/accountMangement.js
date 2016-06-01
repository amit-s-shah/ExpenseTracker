(function (module) {
    'use strict';

    module
        .factory('accountMangement', accountMangement);

    accountMangement.$inject = ['apiService', '$cookieStore', '$rootScope'];

    function accountMangement(apiService, $cookieStore, $rootScope) {
        var service = {
            login: login,
            logout: logout,
            saveUserDetails: saveUserDetails,
            removeUserDetails: removeUserDetails,
            getUserDetails : getUserDetails,
            isUserloggedIn: isUserloggedIn
        };

        function login(user, loginSuccess, loginFailuer) {
            apiService.postData('Account/loginAsync', user, loginSuccess, loginFailuer);
        }

        function logout(user, logoutSuccess, logoutFailuer) {
            removeUserDetails();
            apiService.postData('Account/logoutAsync', user, logoutSuccess, logoutFailuer);
        }

        function saveUserDetails(user) {
            $rootScope.repository = {
                loggedUser: {
                    userName: user.userName
                }
            };
            $cookieStore.put('repository', $rootScope.repository);
        }

        function removeUserDetails() {
            $rootScope.repository = {};
            $cookieStore.remove('repository');
        }

        function getUserDetails() {
            return $rootScope.repository.loggedUser;
        }

        function isUserloggedIn() {
            return $rootScope.repository == null ? false : $rootScope.repository.loggedUser != null;
        }
        return service;
    }
})(angular.module('common.core'));
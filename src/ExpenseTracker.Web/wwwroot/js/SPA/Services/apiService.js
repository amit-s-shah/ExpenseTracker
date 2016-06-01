(function (module) {
    'use strict';

    module.factory('apiService', apiService);

    apiService.$inject = ['$http', '$location', 'notificationService', '$rootScope'];

    function apiService($http, $location, notificationService, $rootScope) {
        var service = {
            getData: getData,
            postData: postData,
        };

        return service;

        function getData(url, config, success, failure) {
            return $http.get(url, config)
                        .then(
                                function (result) {
                                    success(result);
                                },
                                function (error) {
                                    if (error.status == '401') {
                                        notificationService.displayError('Authentication Required');
                                        $rootScope.previousState = $location.path();
                                        $location.path('/login');
                                    }
                                    else if (failure != null) {
                                        failure(error);
                                    }
                                }
                            );
        }

        function postData(url, data, success, failure) {
            return $http.post(url, data)
                        .then(
                                function (result) {
                                    success(result);
                                },
                                function (error) {
                                    if (error.status == '401') {
                                        notificationService.displayError('Authentication Required');
                                        $rootScope.previousState = $location.path();
                                        $location.path('/login');
                                    }
                                    else if (failure != null) {
                                        failure(error);
                                    }
                                }
                            );
        }
    }
})(angular.module('common.core'));
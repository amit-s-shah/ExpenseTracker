(function (module) {
    'use strict';

    module.controller('indexCtrl', indexCtrl);

    indexCtrl.$inject = ['$scope', '$location', 'apiService', 'notificationService'];

    function indexCtrl($scope, $location, apiService, notificationService) {

        $scope.pageClass = 'page-home';
        $scope.loadingBillers = true;
        $scope.isReadOnly = true;

        $scope.billers = [];

        $scope.loadData = loadData;

        function loadData() {
            apiService.getData('/Biller/GET', null,
                                billerLoadCompleted,
                                billerLoadFailed);
        }
        function billerLoadCompleted(result) {
            $scope.billers = result.data;
            $scope.loadingBillers = false;
        }

        function billerLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        loadData();
    }
})(angular.module('ExpenseTracker'));

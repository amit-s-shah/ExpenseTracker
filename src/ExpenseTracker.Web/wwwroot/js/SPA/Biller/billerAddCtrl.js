(function (module) {
    'use strict';

    module
        .controller('billerAddCtrl', billerAddCtrl);

    billerAddCtrl.$inject = ['$scope', '$uibModalInstance', '$timeout', 'apiService', 'notificationService'];

    function billerAddCtrl($scope, $uibModalInstance, $timeout, apiService, notificationService) {
        $scope.addBiller = addBiller;
        $scope.cancelAddBiller = cancelAddBiller;

        function addBiller() {
            console.log($scope.newBiller);
            apiService.postData('/Biller/Add/', $scope.newBiller,
            updateBillerCompleted,
            updateBillerLoadFailed);
        }

        function cancelAddBiller() {
            $scope.isEnabled = false;
            $uibModalInstance.dismiss();
        }

        function updateBillerCompleted(response) {
            notificationService.displaySuccess($scope.newBiller.name + ' has been added');
            $scope.newBiller.Id = response.data;
            $scope.billers.push($scope.newBiller);
            $scope.newBiller = {};
            $uibModalInstance.dismiss();
        }

        function updateBillerLoadFailed(response) {
            notificationService.displayError(response.data);
        }
    }
})(angular.module('ExpenseTracker'));

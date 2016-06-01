(function (module) {
    'use strict';

    module
        .controller('billerEditCtrl', billerEditCtrl);

    billerEditCtrl.$inject = ['$scope', '$uibModalInstance', '$timeout', 'apiService', 'notificationService'];

    function billerEditCtrl($scope, $uibModalInstance, $timeout, apiService, notificationService) {

        $scope.UpdateBiller = UpdateBiller;
        $scope.CancelUpdateBiller = CancelUpdateBiller;

        function UpdateBiller() {
            //console.log($scope.selectedBiller);
            apiService.postData('/Biller/Update/', $scope.selectedBiller,
            updateBillerCompleted,
            updateBillerLoadFailed);
        }

        function CancelUpdateBiller() {
            $scope.isEnabled = false;
            $uibModalInstance.dismiss();
        }

        function updateBillerCompleted(response) {
            notificationService.displaySuccess($scope.selectedBiller.name + ' has been updated');
            $scope.selectedBiller = {};
            $uibModalInstance.close();
        }

        function updateBillerLoadFailed(response) {
            notificationService.displayError(response.data);
        }
    }
})(angular.module('ExpenseTracker'));

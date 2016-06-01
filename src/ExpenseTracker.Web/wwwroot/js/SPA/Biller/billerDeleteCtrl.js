/// <reference path="../services/apiservice.js" />
/// <reference path="../services/notificationservice.js" />

(function (module) {
    'use strict';

    module.controller('billerDeleteCtrl', billerDeleteCtrl);

    billerDeleteCtrl.$inject = ['$scope', '$modalInstance', 'apiService', 'notificationService'];

    function billerDeleteCtrl($scope, $modalInstance, apiService, notificationService) {

        $scope.deleteBiller = deleteBiller;
        $scope.cancelDeleteBiller = cancelDeleteBiller;

        function deleteBiller() {

            apiService.postData('/Biller/Delete/', $scope.billerSelectedForDelete,
                                            deleteBillerCompleted,
                                            deleteBillerLoadFailed);
        }

        function cancelDeleteBiller() {
            $scope.isEnabled = false;
            $modalInstance.dismiss();
        }

        function deleteBillerCompleted(response) {
            notificationService.displaySuccess('Selected biller(s) has been deleted');
            $scope.billerSelectedForDelete = {};
            $modalInstance.close();
        }

        function deleteBillerLoadFailed(error) {
            notificationService.displayError(response.data);
            cancelDeleteBiller();
        }

    }
})(angular.module('ExpenseTracker'));

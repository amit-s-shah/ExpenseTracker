(function (module) {
    'use strict';

    module
        .controller('billerEditCtrl', billerEditCtrl);

    billerEditCtrl.$inject = ['$mdDialog', 'apiService', 'notificationService'];

    function billerEditCtrl($mdDialog, apiService, notificationService) {
        var _this = this;
        _this.UpdateBiller = UpdateBiller;
        _this.CancelUpdateBiller = CancelUpdateBiller;

        function UpdateBiller() {
            //console.log(_this.selectedBiller);
            apiService.postData('/Biller/Update/', _this.selectedBiller,
            updateBillerCompleted,
            updateBillerLoadFailed);
        }

        function CancelUpdateBiller() {
            $mdDialog.cancel();
        }

        function updateBillerCompleted(response) {
            notificationService.displaySuccess(_this.selectedBiller.name + ' has been updated');
            _this.selectedBiller = {};
            $mdDialog.hide();
        }

        function updateBillerLoadFailed(response) {
            notificationService.displayError(response.data);
        }
    }
})(angular.module('ExpenseTracker'));

(function (module) {
    'use strict';

    module
        .controller('billerAddCtrl', billerAddCtrl);

    billerAddCtrl.$inject = ['$mdDialog', 'apiService', 'notificationService'];

    function billerAddCtrl($mdDialog, apiService, notificationService) {
        var _this = this;
        _this.addBiller = addBiller;
        _this.cancelAddBiller = cancelAddBiller;

        function addBiller() {
            console.log(_this.newBiller);
            apiService.postData('/Biller/Add/', _this.newBiller, addBillerCompleted, addBillerLoadFailed);
        }

        function cancelAddBiller() {
            $mdDialog.cancel();
        }

        function addBillerCompleted(response) {
            notificationService.displaySuccess(_this.newBiller.name + ' has been added');
            _this.newBiller.id = response.data;
            $mdDialog.hide(_this.newBiller);
        }

        function addBillerLoadFailed(response) {
            notificationService.displayError(response.data);
        }
    }
})(angular.module('ExpenseTracker'));

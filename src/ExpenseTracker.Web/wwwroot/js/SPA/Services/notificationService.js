(function (module) {
    'use strict';

    module.factory('notificationService', notificationService);

    notificationService.$inject = ['$mdToast'];

    function notificationService($mdToast) {

        //toastr.options = {
        //    "debug": false,
        //    "positionClass": "toast-top-center",
        //    "onclick": null,
        //    "fadeIn": 300,
        //    "fadeOut": 1000,
        //    "timeOut": 3000,
        //    "extendedTimeOut": 1000
        //};
        
        var service = {
            displaySuccess: displaySuccess,
            displayError: displayError,
            displayWarning: displayWarning,
            displayInfo: displayInfo
        };

        return service;

        function displaySuccess(message) {
            //var options = $mdToast.build();
            //options.template(message);
            //options.position = 'top right';
            $mdToast.show(
                $mdToast.simple()
                .textContent(message)
                .capsule(true)
                .hideDelay(30000)
                .position('bottom right')
                .theme('errorTheme')
            );
        }

        function displayError(error) {
            $mdToast.show(
                $mdToast.simple()
                .textContent(error)
                .capsule(true)
                //.theme()
                .position('bottom right')
            );
        }

        function displayWarning(message) {
            //toastr.warning(message);
        }

        function displayInfo(message) {
            //toastr.info(message);
        }

    }

})(angular.module('common.ui'));
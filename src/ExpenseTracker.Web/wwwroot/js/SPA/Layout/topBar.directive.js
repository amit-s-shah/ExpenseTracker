(function(module) {
    'use strict';

    module.directive('topBar', topBar);
    function topBar () {
        // Usage:
        //     <topBar></topBar>
        // Creates:
        //      top nevigation bar
        var directive = {
            restrict: 'E', 
            replace: true, 
            templateUrl: '/js/spa/layout/topBarMaterial.html'
        };
        return directive;
    }

})(angular.module('common.ui'));
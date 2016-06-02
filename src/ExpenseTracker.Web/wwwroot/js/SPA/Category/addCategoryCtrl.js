(function (module) {
    'use strict';

    module
        .controller('addCategoryCtrl', addCategoryCtrl);

    addCategoryCtrl.$inject = ['apiService', 'notificationService', '$mdDialog'];

    function addCategoryCtrl(apiService, notificationService, $mdDialog) {

        var _this = this;
        _this.newCategory = {};
        _this.addCategory = addCategory;
        _this.cancelAddCategory = cancelAddCategory;

        function addCategory() {
            console.log(_this.newCategory);
            apiService.postData('/category/Add/', _this.newCategory,
                                addCategoryCompleted,
                                addCategoryFailed);
        }

        function addCategoryCompleted(response) {

            notificationService.displaySuccess(_this.newCategory.name + ' has been added');
            _this.newCategory.id = response.data;
            $mdDialog.hide(_this.newCategory);
        }

        function addCategoryFailed(error) {
            notificationService.displayError(error.data);
        }

        function cancelAddCategory() {
            _this.newCategory = {};
            $mdDialog.cancel();
        }

    }
})(angular.module('ExpenseTracker'));

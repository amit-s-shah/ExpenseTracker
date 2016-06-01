(function (module) {
    'use strict';

    module
        .controller('addCategoryCtrl', addCategoryCtrl);

    addCategoryCtrl.$inject = ['apiService', 'notificationService', '$modalInstance', 'categories'];

    function addCategoryCtrl(apiService, notificationService, $modalInstance, categories) {

        var _this = this;

        _this.categories = categories;
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
            _this.categories.push(_this.newCategory);
            _this.newCategory = {};
            $modalInstance.close();
        }

        function addCategoryFailed(error) {
            notificationService.displayError(error.data);
        }

        function cancelAddCategory() {
            _this.newCategory = {};
            $modalInstance.dismiss();
        }

    }
})(angular.module('ExpenseTracker'));

(function (module) {
    'use strict';

    module
        .controller('editCategoryCtrl', editCategoryCtrl);

    editCategoryCtrl.$inject = ['apiService', 'notificationService', '$mdDialog']; 

    function editCategoryCtrl(apiService, notificationService, $mdDialog) {
        /* jshint validthis:true */
        var _this = this;

        _this.saveCategory = saveCategory;
        _this.cancelSaveCategory = cancelSaveCategory;

        function saveCategory() {

            apiService.postData('/category/edit/', _this.selectedCategory, updateSuccess, updateFailure);
        }

        function updateSuccess(response) {
            notificationService.displaySuccess(_this.selectedCategory.name + ' category has been updated');
            $mdDialog.hide();
        }

        function updateFailure(error) {
            notificationService.displayError(error.data)
            $mdDialog.cancel();
        }

        function cancelSaveCategory() {
            $mdDialog.cancel();
        }
    }
})(angular.module('ExpenseTracker'));

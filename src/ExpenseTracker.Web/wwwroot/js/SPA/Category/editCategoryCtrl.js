/// <reference path="../services/apiservice.js" />
(function (module) {
    'use strict';

    module
        .controller('editCategoryCtrl', editCategoryCtrl);

    editCategoryCtrl.$inject = ['categories', 'selectedCategory','apiService', 'notificationService', '$modalInstance']; 

    function editCategoryCtrl(categories, selectedCategory, apiService, notificationService, $modalInstance) {
        /* jshint validthis:true */
        var _this = this;

        _this.saveCategory = saveCategory;
        _this.selectedCategory = selectedCategory;
        _this.cancelSaveCategory = cancelSaveCategory;

        function saveCategory() {

            apiService.postData('/category/edit/', _this.selectedCategory, updateSuccess, updateFailure);
        }

        function updateSuccess(response) {
            notificationService.displaySuccess(_this.selectedCategory.name + ' category has been updated');
            $modalInstance.close();
        }

        function updateFailure(error) {
            notificationService.displayError(error.data)
            $modalInstance.dismiss();
        }

        function cancelSaveCategory() {
            $modalInstance.dismiss();
        }
    }
})(angular.module('ExpenseTracker'));

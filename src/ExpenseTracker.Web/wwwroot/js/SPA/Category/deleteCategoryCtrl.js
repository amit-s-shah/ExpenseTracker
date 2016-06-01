(function (module) {
    'use strict';

    module
        .controller('deleteCategoryCtrl', deleteCategoryCtrl);

    deleteCategoryCtrl.$inject = ['selectedCategories', 'apiService', 'notificationService', '$modalInstance'];

    function deleteCategoryCtrl(selectedCategories, apiService, notificationService, $modalInstance) {
        /* jshint validthis:true */
        var _this = this;
        _this.selectedCategories = selectedCategories;
        _this.deleteCategory = deleteCategory;
        _this.cancelDeleteCategory = cancelDeleteCategory;


        function deleteCategory() {
            apiService.postData('category/delete/', _this.selectedCategories, deleteSuccess, deleteFailed);
        }

        function deleteSuccess(response) {
            notificationService.displaySuccess('Selected categories are deleted');
            $modalInstance.close();
        }

        function deleteFailed(error) {
            notificationService.displayError(error.data);
            $modalInstance.dismiss();
        }

        function cancelDeleteCategory() {
            $modalInstance.dismiss();
        }
    }
})(angular.module('ExpenseTracker'));

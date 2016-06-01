(function (module) {
    'use strict';

    module
       .controller('indexCategoryCtrl', indexCategoryCtrl);

    indexCategoryCtrl.$inject = ['$modal', 'apiService', 'notificationService', '$filter'];

    function indexCategoryCtrl($modal, apiService, notificationService, $filter) {

        var _this = this;
        _this.categories = [];
        _this.search = search;
        _this.clearSearch = clearSearch;
        _this.openEditDialog = openEditDialog;
        _this.openAddDialog = openAddDialog;
        _this.openDeleteDialog = openDeleteDialog;
        _this.anythingSelectedForDelete = anythingSelectedForDelete;

        function search() {
            var config = {
                params: {
                    filter: _this.filterCategory
                }
            };
            apiService.getData('/Category/Search/', config, SearchCompleted, searchFailed);
        }

        function SearchCompleted(response) {

            _this.categories = response.data;

            if (_this.filterCategory && _this.filterCategory.length) {
                notificationService.displayInfo(response.data.length + ' categories found');
            }
        }

        function searchFailed(error) {
            notificationService.displayError(error.data);
        }

        function clearSearch() {
            _this.filterCategory = '';
            search();
        }

        function openEditDialog(category) {
            $modal.open({
                templateUrl: 'js/spa/category/editCategory.html',
                controller: 'editCategoryCtrl',
                controllerAs: 'editCtr',
                resolve: {
                    categories: function () { return _this.categories },
                    selectedCategory: function () { return category }
                }
            }).result.then(function () { }, function () { })
        }

        function openAddDialog() {
            $modal.open({
                templateUrl: 'js/spa/category/addCategory.html',
                controller: 'addCategoryCtrl',
                controllerAs: 'addCtr',
                resolve: {
                    categories: function () { return _this.categories }
                }
            }).result.then(function () {
                //search();
            }, function () {

            });
        }

        function openDeleteDialog() {
            $modal.open({
                templateUrl: 'js/spa/category/deleteCategory.html',
                controller: 'deleteCategoryCtrl',
                controllerAs: 'deleteCtrl',
                resolve: {
                    selectedCategories: function () { return $filter('filter')(_this.categories, { selected: true }) }
                }
            }).result.then(function () {
                search();
            }, function () { })
        }

        function anythingSelectedForDelete() {
            var selectedForDelete = $filter('filter')(_this.categories, { selected: true });
            return selectedForDelete == 'undefined' ? 0 : selectedForDelete.length == 0;
        }

        search();
    }
})(angular.module('ExpenseTracker'));

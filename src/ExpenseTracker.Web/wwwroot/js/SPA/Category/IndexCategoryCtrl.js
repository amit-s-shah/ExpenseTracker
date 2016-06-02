(function (module) {
    'use strict';

    module
       .controller('indexCategoryCtrl', indexCategoryCtrl);

    indexCategoryCtrl.$inject = ['$mdDialog', 'apiService', 'notificationService', '$filter'];

    function indexCategoryCtrl($mdDialog, apiService, notificationService, $filter) {

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

        function openEditDialog(category, ev) {
            $mdDialog.show({
                templateUrl: 'js/spa/category/editCategory.html',
                controller: 'editCategoryCtrl',
                controllerAs: 'editCtr',
                clickOutsideToClose: true,
                targetEvent: ev,
                locals : {
                    selectedCategory : category
                },
                bindToController: true,
                closeTo: ev.target
            }).then(function () { }, function () { })
        }

        function openAddDialog(ev) {
            $mdDialog.show({
                templateUrl: 'js/spa/category/addCategory.html',
                controller: 'addCategoryCtrl',
                controllerAs: 'addCtr',
                clickOutsideToClose: true,
                targetEvent: ev,
                closeTo: ev.target
            }).then(function (newCategory) {
                _this.categories.push(newCategory);
            }, function () {

            });

        }

        function openDeleteDialog(ev) {
            var textContent = '';
            var selectedCategoriesForDelete = $filter('filter')(_this.categories, { selected: true });
            var arrayMaxIndex = selectedCategoriesForDelete.length - 1;
            for (var i = 0; i < selectedCategoriesForDelete.length; i++) {
                if (i == arrayMaxIndex)
                    textContent = textContent + selectedCategoriesForDelete[i].name;
                else
                    textContent = textContent + selectedCategoriesForDelete[i].name + ', ';
            }

            var confirm = $mdDialog.confirm()
                          .title('sure , wanna delete following categories?')
                          .textContent(textContent)
                          .ariaLabel('Lucky day')
                          .targetEvent(ev)
                          .ok('Delete')
                          .cancel('Cancel');
            //console.log($scope.billerSelectedForDelete)
            $mdDialog.show(confirm).then(function () {
                deleteCategory(selectedCategoriesForDelete);
                }, function () {
                }
            );
        }

        function deleteCategory(selectedCategories) {
            apiService.postData('category/delete/', selectedCategories, deleteSuccess, deleteFailed);
        }

        function deleteSuccess(response) {
            notificationService.displaySuccess('Selected categories are deleted');
            search();
        }

        function deleteFailed(error) {
            notificationService.displayError(error.data);
        }

        function anythingSelectedForDelete() {
            var selectedForDelete = $filter('filter')(_this.categories, { selected: true });
            return selectedForDelete == 'undefined' ? 0 : selectedForDelete.length == 0;
        }

        search();
    }
})(angular.module('ExpenseTracker'));

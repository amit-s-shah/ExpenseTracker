(function (module) {
    'use strict';

    module
        .controller('billerCtrl', billerCtrl);

    billerCtrl.$inject = ['$scope', '$mdDialog', 'apiService', 'notificationService', '$filter'];

    function billerCtrl($scope, $mdDialog, apiService, notificationService, $filter) {

        $scope.pageClass = 'page-home';
        $scope.loadingBillers = true;
        $scope.isReadOnly = true;

        $scope.billers = [];

        $scope.loadData = loadData;

        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.search = search;
        $scope.clearSearch = clearSearch;

        $scope.openEditDialog = openEditDialog;
        $scope.openAddDialog = openAddDialog;
        $scope.openDeleteDialog = openDeleteDialog;

        $scope.anythingSelectedForDelete = anythingSelectedForDelete;

        function loadData() {
            apiService.getData('/Biller/GET', null,
                                billerLoadCompleted,
                                billerLoadFailed);
        }

        function billerLoadCompleted(result) {
            $scope.billers = result.data;
            $scope.loadingBillers = false;
        }

        function billerLoadFailed(response) {
            notificationService.displayError(response.data);
        }

        function search(page) {
            page = page || 0;

            $scope.loadingBillers = true;

            var config = {
                params: {
                    page: page,
                    pageSize: 20,
                    filter: $scope.filterBillers
                }
            };

            apiService.getData('/Biller/Search/', config,
            billerSearchCompleted,
            billerLoadFailed);
        }

        function clearSearch() {
            $scope.filterBillers = '';
            search();
        }

        function billerSearchCompleted(result) {
            $scope.billers = result.data.items;

            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loadingBillers = false;

            if ($scope.filterBillers && $scope.filterBillers.length) {
                notificationService.displayInfo(result.data.items.length + ' billers found');
            }
        }

        function openEditDialog(biller, ev) {
            $scope.selectedBiller = biller;
            $mdDialog.show({
                templateUrl: 'js/spa/Biller/editBillerModal.html',
                controller: 'billerEditCtrl',
                controllerAs: 'billerCtrl',
                targetEvent: ev,
                locals: { selectedBiller: biller },
                bindToController: true,
                closeTo: ev.target
            }).then(function () {
                clearSearch();
            }, function () {

            });
        }

        function openAddDialog(ev) {
            $mdDialog.show({
                templateUrl: 'js/spa/Biller/addBillerModal.html',
                controller: 'billerAddCtrl',
                controllerAs: 'billerCtrl',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: true
            }).then(function (newBiller) {
                $scope.billers.push(newBiller);
            }, function () {

            });
        }

        function openDeleteDialog(ev) {
            var textContent = '';
            $scope.billerSelectedForDelete = $filter('filter')($scope.billers, { selected: true });
            var arrayMaxIndex = $scope.billerSelectedForDelete.length - 1;
            for (var i = 0; i < $scope.billerSelectedForDelete.length; i++) {
                if (i == arrayMaxIndex)
                    textContent = textContent + $scope.billerSelectedForDelete[i].name;
                else
                    textContent = textContent + $scope.billerSelectedForDelete[i].name + ', ';
            }

            var confirm = $mdDialog.confirm()
                          .title('sure , wanna delete following billers?')
                          .textContent(textContent)
                          .ariaLabel('Lucky day')
                          .targetEvent(ev)
                          .ok('Delete')
                          .cancel('Cancel');
            //console.log($scope.billerSelectedForDelete)
            $mdDialog.show(confirm).then(function () {
                    deleteBiller($scope.billerSelectedForDelete);
                }, function () {
                }
            );
        }

        function anythingSelectedForDelete() {
            var billerSelectedForDelete = $filter('filter')($scope.billers, { selected: true });
            return billerSelectedForDelete == 'undefined' ? 0 : billerSelectedForDelete.length == 0;
        }

        function deleteBiller(billerSelectedForDelete) {

            apiService.postData('/Biller/Delete/', billerSelectedForDelete,
                                            deleteBillerCompleted,
                                            deleteBillerLoadFailed);
        }

        function deleteBillerCompleted(response) {
            notificationService.displaySuccess('Selected biller(s) has been deleted');
            $scope.billerSelectedForDelete = [];
            search();
        }

        function deleteBillerLoadFailed(error) {
            notificationService.displayError(response.data);
        }

        search();
    }
})(angular.module('ExpenseTracker'));

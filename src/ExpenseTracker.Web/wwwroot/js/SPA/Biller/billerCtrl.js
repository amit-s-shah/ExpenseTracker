(function (module) {
    'use strict';

    module
        .controller('billerCtrl', billerCtrl);

    billerCtrl.$inject = ['$scope', '$uibModal', 'apiService', 'notificationService', '$filter'];

    function billerCtrl($scope, $uibModal, apiService, notificationService, $filter) {

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
                notificationService.displayInfo(result.data.Items.length + ' billers found');
            }
        }

        function openEditDialog(biller) {
            $scope.selectedBiller = biller;
            $uibModal.open({
                templateUrl: 'js/spa/Biller/editBillerModal.html',
                controller: 'billerEditCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                clearSearch();
            }, function () {

            });
        }

        function openAddDialog()
        {
            $scope.newBiller = {};
            $uibModal.open({
                templateUrl: 'js/spa/Biller/addBillerModal.html',
                controller: 'billerAddCtrl',
                scope: $scope
            });
        }

        function openDeleteDialog() {
            $scope.billerSelectedForDelete = $filter('filter')($scope.billers, { selected: true });
            //console.log($scope.billerSelectedForDelete)
            $uibModal.open({
                animation: true,
                templateUrl: 'js/spa/Biller/deleteBillerModal.html',
                controller: 'billerDeleteCtrl',
                scope: $scope
            }).result.then(function ($scope) {
                search();
            }, function () {

            });
        }

        function anythingSelectedForDelete() {
            var billerSelectedForDelete = $filter('filter')($scope.billers, { selected: true });
            return billerSelectedForDelete == 'undefined' ? 0 : billerSelectedForDelete.length == 0;
        }

        search();
    }
})(angular.module('ExpenseTracker'));

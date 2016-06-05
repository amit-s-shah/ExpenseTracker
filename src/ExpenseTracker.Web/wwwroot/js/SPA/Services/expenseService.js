(function (module) {
    'use strict';

    module
        .factory('expenseService', expenseService);

    expenseService.$inject = ['$http', 'apiService'];

    function expenseService($http, apiService) {

        //#region functions to resolve 

        function paymentPromise() {
            var promise = $http.get('/PaymentMethod/GetAll/').success(function (response) {
                return response;
            });
            return promise;
        }

        function categoryPromise() {
            var promise = $http.get('/Category/Search/').success(function (response) {
                return response;
            });
            return promise;
        }

        function billerPromise() {
            var promise = $http.get('/Biller/Get/').success(function (response) {
                return response;
            });
            return promise;
        }
        //#endregion

        //#region core services
        function addExpense(expense, success, failure) {
            expense.categoryId = expense.category.id;
            expense.billerId = expense.biller.id;
            expense.paymentMethodId = expense.paymentMethod.id;
            apiService.postData('/ExpenseItem/AddExpese', expense, success, failure)
        }

        function getExpenses(purchaseddate, success, failure) {
            var config = {
                params: {
                    purchaseddate: purchaseddate
                }
            };
            apiService.getData('/ExpenseItem/GetExpenses', config, success, failure)
        }

        function editExpense(expense, success, failure) {
            expense.categoryId = expense.category.id;
            expense.billerId = expense.biller.id;
            expense.paymentMethodId = expense.paymentMethod.id;
            apiService.postData('/ExpenseItem/EditExpese', expense, success, failure)
        }

        function deleteExpenses(selectedForDelete, success, failure) {

            apiService.postData('/ExpenseItem/DeleteExpese/', selectedForDelete,
                                                        success,
                                                        failure);
        }

        //#endregion

        //#region AutoComplete filter functions

        function querySearch(itemsArray, query) {
            var result = query ? itemsArray.filter(createFilterFor(query)) : itemsArray;
            return result;
        }

        function createFilterFor(query) {
            var lowercaseQuery = angular.lowercase(query);
            return function filterFn(item) {
                return (angular.lowercase((item.name || '') + (item.address || '') + (item.description || '')).indexOf(lowercaseQuery) > -1);
            };
        }

        //#endregion

        var service = {
            paymentPromise: paymentPromise,
            categoryPromise: categoryPromise,
            billerPromise: billerPromise,
            querySearch: querySearch,
            addExpense: addExpense,
            getExpenses: getExpenses,
            editExpense: editExpense,
            deleteExpenses : deleteExpenses
        };

        return service;
    }
})(angular.module('common.core'));
(function (module) {
    'use strict';

    module
        .factory('expenseService', expenseService);

    expenseService.$inject = ['$http', 'apiService'];

    function expenseService($http, apiService) {

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

        function addExpense(expense, success, failure) {
            expense.categoryId = expense.category.id;
            expense.billerId = expense.biller.id;
            expense.paymentMethodId = expense.paymentMethod.id;
            apiService.postData('/ExpenseItem/AddExpese', expense, success, failure)
        }

        function getExpenses(purchaseddate, success, failure) {
            apiService.getData('/ExpenseItem/GetExpenses', purchaseddate, success, failure)
        }

        var service = {
            paymentPromise: paymentPromise,
            categoryPromise: categoryPromise,
            billerPromise: billerPromise,
            addExpense: addExpense,
            getExpenses : getExpenses
        };

        return service;
    }
})(angular.module('common.core'));
(function (module) {
    'use strict';

    module
        .controller('editExpenseCtrl', editExpenseCtrl);

    editExpenseCtrl.$inject = ['expenseService', '$mdDialog', 'notificationService'];

    function editExpenseCtrl(expenseService, $mdDialog, notificationService) {
        /* jshint validthis:true */
        var _this = this;
        _this.originalExpense = angular.copy(this.expense);
        _this.title = 'Edit ' + this.originalExpense.name;

        //#region Autocomplete filters
        _this.querySearchBiller = querySearchBiller;
        _this.querySearchCategory = querySearchCategory;
        _this.querySearchPaymentMethod = querySearchPaymentMethod;
        //#endregion

        //#region functions called from view
        _this.reset = reset;
        _this.cancel = cancel;
        _this.saveExpense = saveExpense;
        //#endregion

        function querySearchBiller(query) {
            return expenseService.querySearch(_this.billers, query);
        }

        function querySearchCategory(query) {
            return expenseService.querySearch(_this.categories, query);
        }

        function querySearchPaymentMethod(query) {
            return expenseService.querySearch(_this.paymentMethods, query);
        }

        function reset() {
            angular.forEach(_this.originalExpense, function (value, key) {
                this[key] = value;
            }, _this.expense);
        }

        function cancel() {
            reset();
            $mdDialog.cancel();
        }

        function saveExpense() {
            expenseService.editExpense(_this.expense, success, null);
        }

        function success(response) {
            if (response.data) {
                notificationService.displaySuccess(_this.expense.name + "is updated");
                $mdDialog.hide(_this.expense);
            }
        }
    }
})(angular.module("ExpenseTracker"));

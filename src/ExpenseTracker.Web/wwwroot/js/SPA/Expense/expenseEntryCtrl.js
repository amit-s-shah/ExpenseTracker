(function (module)
{
    module.controller('expenseEntryCtrl', expenseEntryCtrl);
    expenseEntryCtrl.$inject = ['expenseService', 'notificationService', '$filter', '$mdDialog'];

    function expenseEntryCtrl(expenseService, notificationService, $filter, $mdDialog) {
        var _this = this;
        _this.expense = {};
        _this.expense.purchaseddate = this.purchaseddate;
        _this.querySearchBiller = querySearchBiller;
        _this.querySearchCategory = querySearchCategory;
        _this.querySearchPaymentMethod = querySearchPaymentMethod;
        _this.reset = reset;

        function reset() {
            _this.expense = {};
            _this.expense.purchaseddate = _this.purchaseddate;
        }

        _this.cancel = cancel;

        function cancel() {
            $mdDialog.cancel();
        }

        _this.saveExpense = saveExpense;

        function saveExpense() {
            expenseService.addExpense(_this.expense, success, failure);
        }

        function success(response){
            if (response.data == true) {
                notificationService.displaySuccess(_this.expense.name + ' is saved');
                $mdDialog.hide(_this.expense);
            }
            else
                notificationService.displayError(_this.expense.name + ' is not saved.');
        }

        function failure(error) {
            notificationService.displayError(error.data);
        }

        function querySearchBiller(query) {
            return expenseService.querySearch(_this.billers, query);
        }

        function querySearchCategory(query) {
            return expenseService.querySearch(_this.categories, query);
        }
        
        function querySearchPaymentMethod(query) {
            return expenseService.querySearch(_this.paymentMethods, query);
        }
    }

})(angular.module('ExpenseTracker'));

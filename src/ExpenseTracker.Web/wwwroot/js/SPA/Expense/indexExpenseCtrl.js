(function (module) {
    'use strict';

    module
        .controller('indexExpenseCtrl', indexExpenseCtrl);

    indexExpenseCtrl.$inject = ['expenseService', 'paymentMethods', 'categories', 'billers', '$mdDialog', 'notificationService', '$filter'];

    function indexExpenseCtrl(expenseService, paymentMethods, categories, billers, $mdDialog, notificationService, $filter) {
        var _this = this;
        _this.title = 'Expenses';
        _this.purchaseddate = new Date();
        _this.expenses = [];
        _this.categories = categories.data;
        _this.paymentMethods = paymentMethods.data;
        _this.billers = billers.data;

        _this.openAddDialog = openAddDialog;
        _this.getExpenses = getExpenses;
        _this.anythingSelectedForDelete = anythingSelectedForDelete;
        _this.openEditDialog = openEditDialog;
        _this.openDeleteDialog = openDeleteDialog;
        _this.clearSearch = clearSearch;

        function clearSearch() {
            _this.filterExpense = '';
            getExpenses();
        }

        function getExpenses() {
            expenseService.getExpenses(_this.purchaseddate, success, null);
        }

        function success(response) {
            _this.expenses = response.data;
        }

        function anythingSelectedForDelete() {
            var selectedForDelete = $filter('filter')(_this.expenses, { selected: true });
            return selectedForDelete == 'undefined' ? 0 : selectedForDelete.length == 0;
        }

        function openAddDialog(ev) {
            $mdDialog.show({
                templateUrl: 'js/spa/expense/AddExpenses.html',
                controller: 'expenseEntryCtrl',
                controllerAs: 'expenseCtrl',
                targetEvent: ev,
                locals: {
                    categories: _this.categories,
                    paymentMethods: _this.paymentMethods,
                    billers: _this.billers,
                    purchaseddate: _this.purchaseddate,
                },
                bindToController: true,
                closeTo: ev.target
            }).then(function (expense) {
                _this.expenses.push(expense);
            }, function () {

            });
        }

        function openEditDialog(selectedItem, ev) {
            $mdDialog.show({
                templateUrl: 'js/spa/expense/editExpense.html',
                controller: 'editExpenseCtrl',
                controllerAs: 'expenseCtrl',
                targetEvent: ev,
                locals: {
                    categories: _this.categories,
                    paymentMethods: _this.paymentMethods,
                    billers: _this.billers,
                    expense: selectedItem
                },
                bindToController: true,
                closeTo: ev.target,
                escapeToClose: false
            }).then(function (expense) {
                var updateItem = $filter('filter')(_this.expenses, { id: expense.id });
                updateItem = expense;
            }, function () {

            });
        }

        function openDeleteDialog(ev) {
            var textContent = '';
            var selectedForDelete = $filter('filter')(_this.expenses, { selected: true });
            var arrayMaxIndex = selectedForDelete.length - 1;
            for (var i = 0; i < selectedForDelete .length; i++) {
                if (i == arrayMaxIndex)
                    textContent = textContent + selectedForDelete [i].name;
                else
                    textContent = textContent + selectedForDelete [i].name + ', ';
            }

            var confirm = $mdDialog.confirm()
                          .title('sure , wanna delete following items?')
                          .textContent(textContent)
                          .ariaLabel('Lucky day')
                          .targetEvent(ev)
                          .ok('Delete')
                          .cancel('Cancel');
            //console.log(selectedForDelete )
            $mdDialog.show(confirm).then(function () {
                deleteExpenses(selectedForDelete );
            }, function () {
            }
            );
        }

        function deleteExpenses(selectedForDelete) {
            expenseService.deleteExpenses(selectedForDelete,deleteCompleted, null);
        }

        function deleteCompleted(response) {
            notificationService.displaySuccess('Selected expense(s) has been deleted');
            getExpenses();
        }

        getExpenses();
    }
})(angular.module('ExpenseTracker'));

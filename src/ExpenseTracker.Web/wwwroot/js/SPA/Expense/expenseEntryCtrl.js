(function (module)
{
    module.controller('expenseEntryCtrl', expenseEntryCtrl);
    expenseEntryCtrl.$inject = ['paymentMethods', 'categories', 'billers', 'expenseService', 'notificationService'];
    function expenseEntryCtrl(paymentMethods, categories, billers, expenseService, notificationService) {
        var _this = this;
        _this.categories = categories.data;
        _this.paymentMethods = paymentMethods.data;
        _this.billers = billers.data;
        _this.expenses = [];
        _this.expense = {};
        _this.expense.purchaseddate = new Date();

        _this.reset = reset;

        function reset() {
            _this.expense = {};
            _this.expense.purchaseddate = new Date();
        }

        _this.saveExpense = saveExpense;

        function saveExpense() {
            expenseService.addExpense(_this.expense, success, failure);
        }

        function success(response){
            if (response.data == true) {
                notificationService.displaySuccess(_this.expense.name + ' is saved');
                _this.expenses.push(angular.copy(_this.expense));
                _this.reset();
            }
            else
                notificationService.displayError(_this.expense.name + ' is not saved.');
        }

        function getExpenses() {
            expenseService.getExpenses(_this.expense.purchaseddate, expensesFetched,  failure)
        }

        function expensesFetched(response) {
            _this.expenses = response.data;
        }

        function failure(error) {
            notificationService.displayError(error.data);
        }

        getExpenses();
    }

})(angular.module('ExpenseTracker'));

//(function (module) {
//    'use strict';
//    module.filter('dropDownFilter', function () {
//        return function (input, map) {
//            if (typeof map !== 'undefined') {
//                for (var i = 0; i < map.length; i++) {
//                    if (map[i].id == input) {
//                        return map[i].value || map[i].name;
//                    }
//                }
//            }
//            else
//                return input;
//        }
//    });

//    module
//        .controller('expenseEntryCtrl', expenseEntryCtrl);

//    expenseEntryCtrl.$inject = ['$location', '$q', '$scope', 'paymentMethods', 'categories', 'billers'];

//    function expenseEntryCtrl($location, $q, $scope, paymentMethods, categories, billers) {
//        var _this = this;
//        _this.addRow = addRow;
//        //_this.reset = reset;
//        _this.paymentOptions = paymentMethods.data;
//        _this.categories = categories.data;
//        _this.billers = billers.data;

//        _this.expenses = getNewExpenseRecord();
//        _this.gridOptions = {};
//        _this.gridOptions.enableCellEditOnFocus = true;

//        _this.gridOptions.columnDefs = [
//            { field: 'name', displayName: 'Name', enableCellEdit: true, width: '20%' },
//            { field: 'description', displayName: 'Description', enableCellEdit: true, width: '20%' },
//            { field: 'amount', displayName: 'Amount', enableCellEdit: true, type: 'number', width: '10%' },
//            { field: 'purchasedDate', displayName: 'Date', enableCellEdit: true, width: '10%', type: 'date', cellFilter: 'date:"dd-MM-yyyy"' },
//            {
//                field: 'category', displayName: 'Category', editableCellTemplate: 'ui-grid/dropdownEditor',
//                width: '15%', editDropdownOptionsArray: _this.categories, cellFilter: 'dropDownFilter:editDropdownOptionsArray',
//                editDropdownValueLabel : 'name'
//            },
//            {
//                field: 'biller', displayName: 'Biller', enableCellEdit: true, editableCellTemplate: 'ui-grid/dropdownEditor',
//                width: '15%', editDropdownOptionsArray: _this.billers, cellFilter: 'dropDownFilter:editDropdownOptionsArray',
//                editDropdownValueLabel: 'name'
//            },
//            {
//                field: 'paymentMethod', displayName: 'Payment using', editableCellTemplate: 'ui-grid/dropdownEditor',
//                width: '10%', editDropdownOptionsArray: _this.paymentOptions, cellFilter: 'dropDownFilter:editDropdownOptionsArray'
//            }
//        ];

//        _this.gridOptions.data = _this.expenses;

//        _this.gridOptions.onRegisterApi = function (gridApi) {
//            _this.gridApi = gridApi;
//            gridApi.rowEdit.on.saveRow($scope, _this.saveRow);
//        };

//        function addRow() {
//            _this.gridOptions.data.push(getNewExpenseRecord());
//        }

//        //function reset() {
//        //    _this.gridOptions.data = getNewExpenseRecord();
//        //    //_this.expenses = [{ name: "", amount: 0.0 }];
//        //}

//        function getNewExpenseRecord() {
//            return [{
//                name: "",
//                description: "",
//                amount: 0.0,
//                purchasedDate: Date.now(),
//                category: "",
//                biller: "",
//                paymentMethod: ""
//            }];
//        }

//        _this.saveRow = function myfunction(rowEntity) {
//            // create a fake promise - normally you'd use the promise returned by $http or $resource
//            var promise = $q.defer();
//            _this.gridApi.rowEdit.setSavePromise(rowEntity, promise.promise);
//            promise.resolve();
//        }
//    }
//})(angular.module('ExpenseTracker'));

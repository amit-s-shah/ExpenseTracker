(function (module) {
    'use strict';

    module
        .controller('homeCtrl', homeCtrl);

    homeCtrl.$inject = ['apiService']; 

    function homeCtrl(apiService) {
        
        var _this = this;
        _this.title = 'Expense Tracker';

        _this.labels = []; //['2006', '2007', '2008', '2009', '2010', '2011', '2012'];
        _this.series = []; //['Series A', 'Series B'];

        _this.data = [];
        //    [
        //  [65, 59, 80, 81, 56, 55, 40],
        //  [28, 48, 40, 19, 86, 27, 90]
        //];

        _this.GetAllExpense = GetAllExpense;

        function GetAllExpense() {
            apiService.getData('ExpenseChart/GetAllExpense', null, success, null);
        }

        function success(response) {
            _this.labels = response.data.labels;
            _this.series = response.data.series;
            _this.data = response.data.data;
        }

        GetAllExpense();

    }
})(angular.module('ExpenseTracker'));

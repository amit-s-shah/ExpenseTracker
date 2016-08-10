(function (module) {
    'use strict';

    module
        .controller('homeCtrl', homeCtrl);

    homeCtrl.$inject = ['apiService'];

    function homeCtrl(apiService) {

        var _this = this;
        _this.title = 'Expense Summary';
        _this.barResponse = {};
        _this.pieResponse = {};

        _this.labels = []; //['2006', '2007', '2008', '2009', '2010', '2011', '2012'];
        _this.series = []; //['Series A', 'Series B'];

        _this.data = [];
        //    [
        //  [65, 59, 80, 81, 56, 55, 40],
        //  [28, 48, 40, 19, 86, 27, 90]
        //];

        _this.GetAllExpense = GetAllExpense;
        _this.GetDataForPieChart = GetDataForPieChart;

        function GetAllExpense() {
            var config = {
                params: {
                    category: _this.selectedCategory
                }
            };
            apiService.getData('ExpenseChart/GetAllExpense', config, successBar, null);
        }

        function successBar(response) {
            _this.labels = response.data.labels;
            _this.series = response.data.series;
            _this.data = response.data.data;
            _this.barResponse = response.data;
        }

        GetAllExpense();

        function GetDataForPieChart() {

            apiService.getData('ExpenseChart/GetDataForPieChart', null, successPie, null);
        }

        function successPie(response) {
            _this.pieLabels = response.data.labels;
            _this.pieData = response.data.data[0];
            _this.pieResponse = response.data;
            _this.summary = response.data.summary;
        }

        GetDataForPieChart();

        function GetCategories() {
            var config = {
                params: {
                    filter: ""
                }
            };
            apiService.getData('/Category/Search/', config, SearchCompleted, searchFailed);
        }

        function SearchCompleted(response) {
            var defaultCategory = {name:"--All--", id : 0}
            _this.categories = response.data;
            _this.categories.unshift(defaultCategory);
        }

        function searchFailed(error) {
            notificationService.displayError(error.data);
        }

        GetCategories();
    }
})(angular.module('ExpenseTracker'));

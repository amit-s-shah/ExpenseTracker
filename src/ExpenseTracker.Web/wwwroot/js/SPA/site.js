/// <reference path="../../lib/angular-route/angular-route.min.js" />
/// <reference path="../../lib/angular/angular.min.js" />
(function () {
    "use strict";
    angular.module('ExpenseTracker', ['common.core', 'common.ui'])
           .config(configfn)
           .run(run);

    configfn.$inject = ['$routeProvider', '$mdThemingProvider'];

    function configfn($routeProvider, $mdThemingProvider) {
        $routeProvider.when("/", {
            templateUrl: "js/spa/home/index.html",
            controller: "homeCtrl",
            controllerAs: "homeCtl"
        })
        .when("/billers",
        {
            templateUrl: "js/spa/biller/index.html",
            controller: "billerCtrl",
            resolve: { previousState: previousState }
        })
        .when("/categories",
        {
            templateUrl: "js/spa/category/indexCategory.html",
            controller: "indexCategoryCtrl",
            controllerAs: "categoryCtrl",
            resolve: { previousState: previousState }
        })
        .when("/login",
        {
            templateUrl: "js/spa/User/login.html",
            controller: "loginCtrl",
            controllerAs: "loginCtrl"
        })
        .when("/Registration/",
        {
            templateUrl: "js/spa/User/UserRegistration.html",
            controller: "userRegCtrl",
            controllerAs: "regCtrl"
        })
        .when("/ExpenseEntry/",
        {
            templateUrl: "js/spa/Expense/Expenses.html",
            controller: "indexExpenseCtrl",
            controllerAs: "indexCtrl",
            resolve: {
                paymentMethods: function (expenseService) {
                    return expenseService.paymentPromise();
                },
                categories: function (expenseService) {
                    return expenseService.categoryPromise();
                },
                billers: function (expenseService) {
                    return expenseService.billerPromise();
                }
            }
        }).otherwise({ redirectTo: "/" });

        $mdThemingProvider.theme('default')
            .primaryPalette('light-blue')
            .accentPalette('grey');

        $mdThemingProvider
            .theme('errorTheme')
            .backgroundPalette('red');
    }

    run.$inject = ['$rootScope', '$cookieStore'];

    function run($rootScope, $cookieStore) {
        $rootScope.repository = $cookieStore.get('repository') || {};
    }

    previousState.$inject = ['$rootScope', '$location'];

    function previousState($rootScope, $location) {
        $rootScope.previousPath = $location.path();
    }
})();
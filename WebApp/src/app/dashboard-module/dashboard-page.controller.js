(function() {
    'use strict';

    angular
        .module('dashboard-module')
        .controller('DashboardPageController', DashboardPageController);

    /* @ngInject */
    function DashboardPageController() {
        var vm = this;
        vm.testData = ['triangular', 'is', 'great'];
    }
})();
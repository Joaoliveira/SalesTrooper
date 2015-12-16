(function() {
    'use strict';

    angular
        .module('salesmen-module')
        .controller('SalesmenPageController', SalesmenPageController);

    /* @ngInject */
    function SalesmenPageController() {
        var vm = this;
        vm.testData = ['triangular', 'is', 'great'];
    }
})();
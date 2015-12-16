(function() {
    'use strict';

    angular
        .module('salesman-module')
        .controller('SalesmanPageController', SalesmanPageController);

    /* @ngInject */
    function SalesmanPageController() {
        var vm = this;
        vm.testData = ['triangular', 'is', 'great'];
    }
})();
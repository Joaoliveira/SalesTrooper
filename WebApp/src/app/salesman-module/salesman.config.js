(function() {
    'use strict';

    angular
        .module('salesman-module')
        .config(moduleConfig);

    /* @ngInject */
    function moduleConfig($translatePartialLoaderProvider, $stateProvider, triMenuProvider) {
        $translatePartialLoaderProvider.addPart('app/salesman-module');

        $stateProvider
        .state('triangular.admin-default.salesman-page', {
            url: '/salesman-module/salesman-page',
            templateUrl: 'app/salesman-module/salesman-page.tmpl.html',
            // set the controller to load for this page
            controller: 'SalesmanPageController',
            controllerAs: 'vm'
        });

        triMenuProvider.addMenu({
            name: 'Salesman',
            icon: 'zmdi zmdi-grade',
            type: 'dropdown',
            priority: 1.1,
            
        });
    }
})();
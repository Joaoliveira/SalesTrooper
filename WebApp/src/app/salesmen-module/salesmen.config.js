(function() {
    'use strict';

    angular
        .module('salesmen-module')
        .config(moduleConfig);

    /* @ngInject */
    function moduleConfig($translatePartialLoaderProvider, $stateProvider, triMenuProvider) {
        $translatePartialLoaderProvider.addPart('app/salesmen-module');

        $stateProvider
        .state('triangular.admin-default.salesmen-page', {
            url: '/salesmen-module/salesmen-page',
            templateUrl: 'app/salesmen-module/salesmen-page.tmpl.html',
            // set the controller to load for this page
            controller: 'SalesmenPageController',
            controllerAs: 'vm'
        });

        triMenuProvider.addMenu({
            name: 'Salesmen',
            icon: 'zmdi zmdi-pin-account',
            type: 'dropdown',
            priority: 1.1,
            children: [{
                name: 'Profile',
                state: 'triangular.admin-default.salesmen-page',
                type: 'link'
            }]
        });
    }
  

})();
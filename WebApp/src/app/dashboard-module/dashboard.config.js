(function() {
    'use strict';

    angular
        .module('dashboard-module')
        .config(moduleConfig);

    /* @ngInject */
    function moduleConfig($translatePartialLoaderProvider, $stateProvider, triMenuProvider) {
        $translatePartialLoaderProvider.addPart('app/dashboard-module');

        $stateProvider
        .state('triangular.admin-default.dashboard-page', {
            url: '/dashboard-module/dashboard-page',
            templateUrl: 'app/dashboard-module/dashboard-page.tmpl.html',
            // set the controller to load for this page
            controller: 'DashboardPageController',
            controllerAs: 'vm'
        });

        triMenuProvider.addMenu({
            name: 'Dashboard',
            icon: 'zmdi zmdi-home',
            type: 'dropdown',
            priority: 1.1,
            children: [{
                name: 'Dashboard',
                state: 'triangular.admin-default.dashboard-page',
                type: 'link'
            }]
        });
    }
})();
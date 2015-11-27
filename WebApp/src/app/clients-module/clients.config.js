(function() {
    'use strict';

    angular
        .module('clients-module')
        .config(moduleConfig);

    /* @ngInject */
    function moduleConfig($translatePartialLoaderProvider, $stateProvider, uiGmapGoogleMapApiProvider, triMenuProvider) {
        $translatePartialLoaderProvider.addPart('app/clients-module');

        $stateProvider
        .state('triangular.admin-default.search-clients-page', {
            url: '/clients/search-clients',
            templateUrl: 'app/clients-module/search-clients-page.tmpl.html',
            controller: 'SearchClientsPageController',
            controllerAs: 'vm'    
        })
        .state('triangular.admin-default.clients-page', {
            url: '/clients/:clientID',
            templateUrl: 'app/clients-module/clients-page.tmpl.html',
            controller: 'ClientsPageController',
            controllerAs: 'vm'
        });

        uiGmapGoogleMapApiProvider.configure({
            v: '3.17',
            libraries: 'weather,geometry,visualization'
        });

        triMenuProvider.addMenu({
            name: 'Clients',
            icon: 'zmdi zmdi-account',
            type: 'dropdown',
            priority: 1.1,
            children: [{
                name: 'Search',
                state: 'triangular.admin-default.search-clients-page',
                type: 'link'
            }]
        });
    }
})();
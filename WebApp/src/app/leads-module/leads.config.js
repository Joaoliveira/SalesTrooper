(function() {
    'use strict';

    angular
        .module('leads-module')
        .config(moduleConfig);

    /* @ngInject */
    function moduleConfig($translatePartialLoaderProvider, $stateProvider, uiGmapGoogleMapApiProvider, triMenuProvider) {
        $translatePartialLoaderProvider.addPart('app/leads-module');

        $stateProvider
        .state('triangular.admin-default.search-leads-page', {
            url: '/leads/search-leads',
            templateUrl: 'app/leads-module/search-leads-page.tmpl.html',
            controller: 'SearchLeadsPageController',
            controllerAs: 'vm'    
        })
        .state('triangular.admin-default.leads-page', {
            url: '/leads/:leadID',
            templateUrl: 'app/leads-module/leads-page.tmpl.html',
            controller: 'LeadPageController',
            controllerAs: 'vm'
        });

        uiGmapGoogleMapApiProvider.configure({
            v: '3.17',
            libraries: 'weather,geometry,visualization'
        });

        triMenuProvider.addMenu({
            name: 'Leads',
            icon: 'zmdi zmdi-case',
            type: 'dropdown',
            priority: 1.1,
            children: [{
                name: 'Search',
                state: 'triangular.admin-default.search-leads-page',
                type: 'link'
            }]
        });
    }
})();
(function() {
    'use strict';

    angular
        .module('admin-module')
        .config(moduleConfig);

    /* @ngInject */
    function moduleConfig($translatePartialLoaderProvider, $stateProvider, triMenuProvider) {
        $translatePartialLoaderProvider.addPart('app/admin-module');

        $stateProvider
        .state('triangular.admin-default.create-client', {
            url: '/admin/create-client',
            templateUrl: 'app/admin-module/admin-create-client-page.tmpl.html',
            // set the controller to load for this page
            controller: 'AdminCreateClientPageController',
            controllerAs: 'vm'
        });

        triMenuProvider.addMenu({
            name: 'Admin',
            icon: 'zmdi zmdi-settings',
            type: 'dropdown',
            priority: 1.1,
            children: [{
                name: 'Create client',
                state: 'triangular.admin-default.create-client',
                type: 'link'
            }]
        });
    }
})();
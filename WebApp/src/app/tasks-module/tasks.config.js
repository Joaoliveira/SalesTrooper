(function() {
    'use strict';

    angular
        .module('tasks-module')
        .config(moduleConfig);

    /* @ngInject */
    function moduleConfig($translatePartialLoaderProvider, $stateProvider, triMenuProvider, uiGmapGoogleMapApiProvider) {
        $translatePartialLoaderProvider.addPart('app/tasks-module');

        $stateProvider
        .state('triangular.admin-default.search-tasks-page', {
            url: '/tasks/search-tasks',
            templateUrl: 'app/tasks-module/search-tasks-page.tmpl.html',
            controller: 'SearchTasksPageController',
            controllerAs: 'vm'    
        })        
        .state('triangular.admin-default.tasks-page', {
            url: '/tasks/:taskID',
            templateUrl: 'app/tasks-module/tasks-page.tmpl.html',
            controller: 'TasksPageController',
            controllerAs: 'vm'
        });

        uiGmapGoogleMapApiProvider.configure({
            v: '3.17',
            libraries: 'weather,geometry,visualization'
        });
        
        triMenuProvider.addMenu({
            name: 'Tasks',
            icon: 'zmdi zmdi-calendar-check',
            type: 'dropdown',
            priority: 1.1,
            children: [{
                name: 'Tasks',
                state: 'triangular.admin-default.search-tasks-page',
                type: 'link'
            }]
        });
    }
})();
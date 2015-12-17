(function() {
    'use strict';
    angular
    .module('tasks-module')
    .controller('SearchTasksPageController', SearchTasksPageController);

    /* @ngInject */
    function SearchTasksPageController($http) {
        var vm = this;

        var promise = $http.get('http://localhost:49822/api/tasks/');
        vm.tasks = [];
        promise.then(function requestDone (response) {
            vm.contents = [];
            vm.tasks = response.data;
            for(var i = 0; i < vm.tasks.length; i++) {
                var obj = {
                    thumb:'assets/images/avatars/avatar-1.png',
                    goto: vm.tasks[i].Id,
                    id: vm.tasks[i].Id,
                    name: vm.tasks[i].Resumo,
                    priority: vm.tasks[i].Prioridade,
                    endDate: vm.tasks[i].DataDeFim,
                    entity: vm.tasks[i].EntidadePrincipal
                };
                vm.contents.push(obj);
            }

        });

        vm.columns = [{
            title: '',
            field: 'thumb',
            sortable: false,
            filter: 'tableImage'
        },{
            title: 'Id',
            field: 'id',
            sortable: true
        },{
            title: 'Name',
            field: 'name',
            sortable: true
        },{
            title: 'Priority',
            field: 'priority',
            sortable: true
        },{
            title: 'End Date',
            field: 'endDate',
            sortable: true
        },{
            title: 'Entity',
            field: 'entity',
            sortable: true
        }];


    }
})();

(function() {
    'use strict';
    angular
    .module('leads-module')
    .controller('SearchLeadsPageController', SearchLeadsPageController);

    /* @ngInject */
    function SearchLeadsPageController($http) {
        var vm = this;

        var promise = $http.get('http://127.0.0.1:49822/api/leads/');
        vm.leads = [];
        promise.then(function requestDone (response) {
            vm.contents = [];
            vm.leads = response.data;
            for(var i = 0; i < vm.leads.length; i++) {
                var obj = {
                    thumb:'assets/images/avatars/avatar-1.png',
                    goto: vm.leads[i].idLead,
                    id: vm.leads[i].idLead,
                    name: vm.leads[i].Entidade,
                    description: vm.leads[i].DescLead,
                    type: vm.leads[i].TipoEntidade
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
            title: 'ID',
            field: 'id',
            sortable: true
        },{
            title: 'Name',
            field: 'name',
            sortable: true
        },{
            title: 'Description',
            field: 'description',
            sortable: true
        },{
            title: 'Entity Type',
            field: 'type',
            sortable: true
        }];


    }
})();

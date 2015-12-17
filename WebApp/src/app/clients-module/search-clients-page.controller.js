(function() {
    'use strict';
    angular
    .module('clients-module')
    .controller('SearchClientsPageController', SearchClientsPageController);

    /* @ngInject */
    function SearchClientsPageController($http) {
        var vm = this;

        var promise = $http.get('http://127.0.0.1:49822/api/salesmen/1/clients');
        vm.clients = [];
        promise.then(function requestDone (response) {
            vm.contents = [];
            vm.clients= response.data;
            for(var i = 0; i < vm.clients.length; i++) {
                var obj = {
                    thumb:'assets/images/avatars/avatar-1.png',
                    goto: vm.clients[i].CodCliente,
                    id: vm.clients[i].CodCliente,
                    name: vm.clients[i].NomeCliente,
                    description: vm.clients[i].Descricao,
                    currency: vm.clients[i].Moeda,
                    number: vm.clients[i].NumContribuinte

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

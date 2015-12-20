(function() {
    'use strict';

    angular
    .module('admin-module')
    .controller('AdminCreateTaskPageController', AdminCreateTaskPageController);

    /* @ngInject */
    function AdminCreateTaskPageController(uiGmapGoogleMapApi, $http, $stateParams, $mdToast) {

         var vm = this;
        
        function initTask() {
            vm.task = {
                IdTipoActividade: "",
                Resumo: "",
                Descricao: "",
                EntidadePrincipal: "",
                DataInicio: "",
                DataFim: "",
                LocalRealizacao: "",
                Utilizador: "",
                IdCabecOVenda: "9F832B71-08CF-4B4D-A31A-AA9C834E058E"
            }
        }

        initTask();

        vm.availableHours = Array.apply(null, {length: 23}).map(Number.call, Number);
        vm.availableMinutes = Array.apply(null, {length: 60}).map(Number.call, Number);
        

       
        var clientsRequest = $http.get('http://localhost:49822/api/clients/');

        clientsRequest.then(function(response){
            vm.clients = response.data;
        });

        var salesmenRequest = $http.get('http://localhost:49822/api/salesmen/');

        salesmenRequest.then(function(response){
            vm.salesmen = response.data;
        });

        var leadsRequest = $http.get('http://localhost:49822/api/leads/');

        leadsRequest.then(function(response){
            vm.leads = response.data;
        });


        vm.createTask = function() {
            var startDate = new Date(vm.date);
            var endDate = new Date(vm.date);

            startDate.setHours(vm.startTime.hour);
            startDate.setMinutes(vm.startTime.minute);
            endDate.setHours(vm.endTime.hour);
            endDate.setMinutes(vm.endTime.minute);

            vm.task.DataInicio = startDate;
            vm.task.DataFim = endDate;

            var request = $http.post('http://localhost:49822/api/tasks/', vm.task);

            request.then(
                function(response){ //success
                    initLead();
                    $mdToast.showSimple("Task created")
                },
                function(response) { //error
                    $mdToast.showSimple("Error creating task " + response.data)
                }
            );
        }
    }
})();

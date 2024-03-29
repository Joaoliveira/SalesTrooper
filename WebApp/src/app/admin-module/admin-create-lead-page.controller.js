(function() {
    'use strict';

    angular
        .module('admin-module')
        .controller('AdminCreateLeadPageController', AdminCreateLeadPageController);

    /* @ngInject */
    function AdminCreateLeadPageController($http, $mdToast) {
        var vm = this;

        function initLead() {
        	vm.lead = {
                Oportunidade: "",
                DescLead: "",
                Entidade: "",
                Resumo: "",
                Vendedor: "",
                TipoEntidade: "C"
	        }
        }

        initLead();

        var clientsRequest = $http.get('http://localhost:49822/api/clients/');

        clientsRequest.then(function(response){
        	vm.clients = response.data;
        });

        var salesmenRequest = $http.get('http://localhost:49822/api/salesmen/');

        salesmenRequest.then(function(response){
            vm.salesmen = response.data;
        });

        

        vm.createLead = function() {
        	var request = $http.post('http://localhost:49822/api/leads/', vm.lead);

        	request.then(
        		function(response){ //success
		    		initLead();
		    		$mdToast.showSimple("Lead created")
        		},
        		function(response) { //error
        			$mdToast.showSimple("Error creating lead " + response.data)
        		}
        	);
        }
    }
})();

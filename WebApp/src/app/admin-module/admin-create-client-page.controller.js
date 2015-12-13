(function() {
    'use strict';

    angular
        .module('admin-module')
        .controller('AdminCreateClientPageController', AdminCreateClientPageController);

    /* @ngInject */
    function AdminCreateClientPageController($http, $mdToast) {
        var vm = this;

        function initClient() {
        	vm.client = {
	        	CodCliente: "",
	        	NomeCliente: "",
	        	Morada: "",
	        	Telefone: "",
	        	NumContribuinte: "",
	        	Localidade: "",
	        	Descricao: "",
	        	Moeda: "EUR",
	        	Vendedor: ""
	        }
        }

        initClient();

        var salesmenRequest = $http.get('http://localhost:49822/api/salesmen/');

        salesmenRequest.then(function(response){
        	console.log(response.data);
        	vm.salesmen = response.data;
        });

        vm.createClient = function() {
        	var request = $http.post('http://localhost:49822/api/clients/', vm.client);
        	
        	request.then(
        		function(response){ //success
		    		initClient();
		    		$mdToast.showSimple("Client created")
        		},
        		function(response) { //error
        			$mdToast.showSimple("Error creating client")
        		}
        	);
        }
    }
})();
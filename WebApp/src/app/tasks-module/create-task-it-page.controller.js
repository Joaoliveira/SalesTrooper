(function() {
    'use strict';

    angular
        .module('tasks-module')
        .controller('CreateTaskIterationPageController', CreateTaskIterationPageController);

    /* @ngInject */
    function CreateTaskIterationPageController($http, $mdToast) {
        var vm = this;

        function initTasks() {

            var currentdate = new Date(); 
            var datetime = currentdate.getFullYear() + "-"
            + currentdate.getMonth() + "-"
            + currentdate.getDate() + " "
            + currentdate.getHours() + ":"
            + currentdate.getMinutes() + ":"
            + currentdate.getSeconds();

        	vm.taskIt = {
                IdTarefa: "",
                Descricao: "",
                Resumo: "",
                Utilizador: "",
                Data: datetime
	        }
        }

        initTasks();

        var taskRequest = $http.get('http://localhost:49822/api/tasks/');

        taskRequest.then(function(response){
            vm.tasks = response.data;
        });

        var salesmenRequest = $http.get('http://localhost:49822/api/salesmen/');

        salesmenRequest.then(function(response){
            vm.salesmen = response.data;
        });


        vm.createTaskIteration = function() {
        	var request = $http.put('http://localhost:49822/api/itertarefas/', vm.taskIt);
        	
        	request.then(
        		function(response){ //success
		    		initTasks();
		    		$mdToast.showSimple("Task iteration created")
        		},
        		function(response) { //error
        			$mdToast.showSimple("Error task iteration " + response.data)
        		}
        	);
        }
    }
})();
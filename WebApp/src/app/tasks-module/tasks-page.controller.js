(function() {
    'use strict';

    angular
    .module('tasks-module')
    .controller('TasksPageController', TasksPageController);

    /* @ngInject */
    function TasksPageController(uiGmapGoogleMapApi,$http, $stateParams) {
        
        var vm = this;
        vm.tasks = [];
        /*var promise = $http.get('http://localhost:49822/api/tasks/' + $stateParams.taskID);

        promise.then(function requestDone (response) {
            vm.tasks = response.data;
            promise = $http.get('http://localhost:49822/api/tasks/' + vm.task.Entidade);

            promise.then(function requestDone (response) {
                vm.tasks.client = response.data;
            });
        });*/


        var promise = $http.get('http://localhost:49822/api/tasks/' + $stateParams.taskID);

        promise.then(function requestDone (response) {
           vm.tasks = response.data;


        });


        promise = $http.get('http://localhost:49822/api/itertarefas/' + $stateParams.taskID);

          promise.then(function requestDone (response) {
           vm.tasks.iter = response.data;
        });



        uiGmapGoogleMapApi.then(function(maps) {
            vm.terrainMap = {
                center: {
                  latitude: 41.177875, 
                  longitude: -8.597895
              },
              zoom: 20,
              marker: {
                id:0,
                coords: {
                    latitude: 41.177875, 
                    longitude: -8.597895
                },
                options: {
                    icon: {
                        anchor: new maps.Point(36,36),
                        origin: new maps.Point(0,0),
                        url: 'assets/images/maps/blue_marker.png'
                    }
                }
            },
            options:{
                scrollwheel:false
            }
        };

        
    });
}
})();
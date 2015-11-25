(function() {
    'use strict';

    angular
    .module('leads-module')
    .controller('LeadPageController', LeadPageController);

    /* @ngInject */
    function LeadPageController(uiGmapGoogleMapApi,$http, $stateParams) {
        
        var vm = this;
        vm.tasks = [];
        var promise = $http.get('http://localhost:49822/api/leads/' + $stateParams.leadID);

        promise.then(function requestDone (response) {
            vm.lead = response.data;
            promise = $http.get('http://localhost:49822/api/clients/' + vm.lead.Entidade);

            promise.then(function requestDone (response) {
                vm.lead.client = response.data;
            });
        });


        promise = $http.get('http://localhost:49822/api/leads/' + $stateParams.leadID + '/tasks');

        promise.then(function requestDone (response) {
           vm.tasks = response.data;
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
                scrollwheel:false,
            }
        };

    });
        
    }
})();
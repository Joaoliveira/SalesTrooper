(function() {
    'use strict';

    angular
        .module('clients-module')
        .controller('ClientsPageController', ClientsPageController);

    /* @ngInject */
    function ClientsPageController(uiGmapGoogleMapApi, $http, $stateParams) {
          var vm = this;
        vm.leads = [];
        var promise = $http.get('http://localhost:49822/api/clients/' + $stateParams.clientsID);

        promise.then(function requestDone (response) {
            vm.client = response.data;
            promise = $http.get('http://localhost:49822/api/clients/' + $stateParams.clientsID + '/leads');

            promise.then(function requestDone (response) {
                vm.leads = response.data;
            });
        });

        /*
        promise = $http.get('http://localhost:49822/api/clients/' + 'sofrio');

        promise.then(function requestDone (response) {
           vm.client.invoices = response.data;
        });*/

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
})
();
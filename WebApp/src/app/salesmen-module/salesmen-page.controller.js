(function() {
    'use strict';

    angular
        .module('salesmen-module')
        .controller('SalesmenPageController', SalesmenPageController);

    /* @ngInject */
    function SalesmenPageController(uiGmapGoogleMapApi, $http, $stateParams) {
        var vm = this;
        vm.clientes = [];
        var promise = $http.get('http://localhost:49822/api/salesmen/' + 1/*$stateParams.salesmanID*/);

        promise.then(function requestDone (response) {
            vm.salesman = response.data;
            var address = vm.salesman.Localidade;

            if(!address) {
                address = vm.salesman.Morada;
            }
            promise = $http.get('http://maps.googleapis.com/maps/api/geocode/json?address="' + address + '"&sensor=false');

            promise.then(function requestDone (response) {
                var rsp = response.data;
                uiGmapGoogleMapApi.then(function(maps) {
                vm.terrainMap = {
                    center: {
                      latitude: rsp.results[0].geometry.location.lat, 
                      longitude: rsp.results[0].geometry.location.lng
                  },
                  zoom: 15,
                  marker: {
                    id:0,
                    coords: {
                        latitude: rsp.results[0].geometry.location.lat, 
                        longitude: rsp.results[0].geometry.location.lng
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


        });

        

            promise = $http.get('http://localhost:49822/api/salesmen/' + '1' + '/clients');

            promise.then(function requestDone (response) {
                vm.clients = response.data;
        });

    });

        
    }
})
();
(function() {
    'use strict';

    angular
    .module('leads-module')
    .controller('LeadPageController', LeadPageController);

    /* @ngInject */
    function LeadPageController(uiGmapGoogleMapApi,$http, $stateParams) {
        
        var vm = this;
        vm.tasks = [];
        var address;
        var promise = $http.get('http://localhost:49822/api/leads/' + $stateParams.leadID);

        promise.then(function requestDone (response) {
            vm.lead = response.data;
            promise = $http.get('http://localhost:49822/api/clients/' + vm.lead.Entidade);

            promise.then(function requestDone (response) {
                vm.lead.client = response.data;
                address = vm.lead.client.Localidade;

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
        });


        promise = $http.get('http://localhost:49822/api/leads/' + $stateParams.leadID + '/tasks');

        promise.then(function requestDone (response) {
           vm.tasks = response.data;
        });

    });
        
    }
})();
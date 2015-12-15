(function() {
    'use strict';

    angular
    .module('leads-module')
    .controller('LeadPageController', LeadPageController);

    /* @ngInject */
    function LeadPageController(uiGmapGoogleMapApi,$http, $stateParams) {
        /*teste*/
        var vm = this;
        vm.tasks = [];
        var promise = $http.get('http://127.0.0.1:49822/api/leads/' + $stateParams.leadID); //ye

        promise.then(function requestDone (response) {
            vm.lead = response.data;
            promise = $http.get('http://127.0.0.1:49822/api/clients/' + vm.lead.Entidade);

            promise.then(function requestDone (response) {
                vm.lead.client = response.data;

                var address = vm.lead.client.Localidade;

                if(!address) {
                    address = vm.lead.client.Morada;
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
        });


        promise = $http.get('http://127.0.0.1:49822/api/leads/' + $stateParams.leadID + '/tasks');

        promise.then(function requestDone (response) {
           vm.tasks = response.data;
        });

        vm.changeTaskState = function(Id, Estado) {
            var request = $http.put('http://127.0.0.1:49822/api/tasks/' + Id, '=' + Estado, { headers: {'Content-Type': 'application/x-www-form-urlencoded'}});
        }

    });

    }
})();

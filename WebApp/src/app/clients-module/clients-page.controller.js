(function() {
    'use strict';

    angular
        .module('clients-module')
        .controller('ClientsPageController', ClientsPageController);

    /* @ngInject */
    function ClientsPageController(uiGmapGoogleMapApi, $http, $stateParams) {
        var vm = this;
        vm.leads = [];
        var promise = $http.get('http://127.0.0.1:49822/api/clients/' + $stateParams.clientID);

        promise.then(function requestDone (response) {
            vm.client = response.data;
            var address = vm.client.Localidade;

            if(!address) {
                address = vm.client.Morada;
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

            promise = $http.get('http://127.0.0.1:49822/api/clients/' + $stateParams.clientID + '/leads');

            promise.then(function requestDone (response) {
                vm.leads = response.data;
            });

            promise = $http.get('http://127.0.0.1:49822/api/clients/' + $stateParams.clientID + '/invoices');

            promise.then(function requestDone (response) {
                vm.contents = [];
                vm.invoices= response.data;
                for(var i = 0; i < vm.invoices.length; i++) {
                    var obj = {
                        id: vm.invoices[i].Id,
                        goto: vm.invoices[i].NumDoc,
                        moeada: vm.invoices[i].Moeda,
                        numDoc: vm.invoices[i].NumDoc,
                        data: vm.invoices[i].Data,
                        dataVencimento: vm.invoices[i].DataVencimento,
                        total: vm.invoices[i].TotalIva + vm.invoices[i].TotalMerc

                    };
                    vm.contents.push(obj);
                }
            });
            vm.columns = [{
                title: 'ID',
                field: 'id',
                sortable: true
            },{
                title: 'Num Document',
                field: 'numDoc',
                sortable: true
            },{
                title: 'Date',
                field: 'data',
                sortable: true
            },{
                title: 'Due date',
                field: 'dataVencimento',
                sortable: true
            },{
                title: 'Total',
                field: 'total',
                sortable: true
            }]
        });


    });


    }
})
();

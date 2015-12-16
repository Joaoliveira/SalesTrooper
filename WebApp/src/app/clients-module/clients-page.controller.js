(function() {
    'use strict';

    angular
        .module('clients-module')
        .controller('ClientsPageController', ClientsPageController);

    /* @ngInject */
    function ClientsPageController(uiGmapGoogleMapApi,$scope, $http, $stateParams) {
        var vm = this;


        vm.salesData = {
                totalSales: 0,
                totalEarnings: 0,
                averageEarnings: 0
            };
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
                vm.salesData.totalSales = vm.invoices.length;
                vm.salesData.totalEarnings = 0;
                vm.labels = [];
                for(var i = 0; i < vm.invoices.length; i++) {
                    var obj = {
                        id: vm.invoices[i].Id,
                        goto: vm.invoices[i].NumDoc,
                        moeada: vm.invoices[i].Moeda,
                        numDoc: vm.invoices[i].NumDoc,
                        data: vm.invoices[i].Data,
                        dataVencimento: vm.invoices[i].DataVencimento,
                        total: vm.invoices[i].TotalIva + vm.invoices[i].TotalIEC + vm.invoices[i].TotalMerc - vm.invoices[i].TotalDesc
                    };
                    vm.salesData.totalEarnings += vm.invoices[i].TotalIva + vm.invoices[i].TotalIEC + vm.invoices[i].TotalMerc - vm.invoices[i].TotalDesc;
                    vm.contents.push(obj);
                    vm.labels.push(i+1);
                }
                vm.salesData.averageEarnings = vm.salesData.totalEarnings / vm.salesData.totalSales;
                console.log(vm.salesData);


                $("#totalSales").html(vm.salesData.totalSales);
                $("#averageEarnings").html("$"+vm.salesData.averageEarnings);
                $("#TotalEarnings").html("$"+vm.salesData.totalEarnings);

                        vm.series = ['Sales'];
                        vm.options = {
                            datasetFill: false
                        };

                        ///////////

                        function randomData() {
                            vm.data = [];
                            for(var series = 0; series < vm.series.length; series++) {
                                var row = [];
                                for(var label = 0; label < vm.labels.length; label++) {
                                    row.push(vm.invoices[label].TotalIva + vm.invoices[label].TotalIEC + vm.invoices[label].TotalMerc - vm.invoices[label].TotalDesc);
                                }
                                vm.data.push(row);
                            }
                        }

                        // init

                        randomData();


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
            }
            ]

        });
    });


    }

})
();

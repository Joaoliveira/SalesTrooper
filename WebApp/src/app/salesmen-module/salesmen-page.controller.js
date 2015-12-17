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


        // PIE CHART
         vm.labels = ['Leads Open ', 'Leads Lost', 'Leads Won'];
        vm.options = {
            datasetFill: false
        };


         vm.leadsRows=[];
        promise = $http.get('http://localhost:49822/api/salesmen/' + '1' + '/leads');

        promise.then(function requestDone (response) {

            vm.contents=[];
            vm.leads = response.data;

            var openLeads=0;
            var wonLeads=0;
            var lostLeads=0;


            for(var i = 0; i < vm.leads.length; i++) {

                var obj ={
                    descLead : vm.leads[i].DescLead,
                    entidade : vm.leads[i].Entidade,
                    estado : vm.leads[i].Estado,
                    tipoEntidade : vm.leads[i].TipoEntidade,
                    idlead : vm.leads[i].Idlead,
                    valorTotalOV : vm.leads[i].ValorTotalOV,
                    dataFecho : vm.leads[i].DataFecho
                };

                if(obj.estado == 0)
                {
                    openLeads++;
                }
                else if(obj.estado == 1)
                {
                    wonLeads++;
                    
                }
                else if(obj.estado == 2)
                {
                    lostLeads++;
                    vm.leadsRows.push(obj);
                }


           }

           function chartPieData() {
            vm.data = [];

           vm.data.push(openLeads);
           vm.data.push(wonLeads);
           vm.data.push(lostLeads);
        }

        // init

        chartPieData();

        // Simulate async data update
        $interval(chartPieData, 5000);


        });

vm.columns = [{
            title: 'Client',
            field: 'entidade',
            sortable: true
        },{
            title: 'Valor',
            field: 'valorTotalOV',
            sortable: true
        },{
            title: 'Closing Date',
            field: 'dataFecho',
            sortable: true
        }];


        // LINEAR CHART
        vm.chartData = {
            type: 'Line',
            data: {
                'cols': [
                    {id: 'day', label: 'Day', type: 'number'},
                    {id: 'nsales', label: 'Number of Sales', type: 'number'},
                    {id: 'salesincome', label: 'Sales Income', type: 'number'}
                ],
                'rows': [
                    {
                        c: [{v: 14}, {v: 37.8}, {v: 90.8}]
                    },
                    {
                        c: [{v: 13}, {v: 30.9}, {v: 69.5}]
                    },
                    {
                        c: [{v: 12}, {v: 25.4}, {v: 57}]
                    },
                    {
                        c: [{v: 11}, {v: 11.7}, {v: 18.8}]
                    },
                    {
                        c: [{v: 10}, {v: 11.9}, {v: 17.6}]
                    },
                    {
                        c: [{v: 9}, {v: 8.8}, {v: 13.6}]
                    },
                    {
                        c: [{v: 8}, {v: 7.6}, {v: 12.3}]
                    },
                    {
                        c: [{v: 7}, {v: 12.3}, {v: 29.2}]
                    },
                    {
                        c: [{v: 6}, {v: 16.9}, {v: 42.9}]
                    },
                    {
                        c: [{v: 5}, {v:12.8}, {v: 30.9}]
                    },
                    {
                        c: [{v: 4}, {v: 5.3}, {v: 7.9}]
                    },
                    {
                        c: [{v: 3}, {v: 6.6}, {v: 8.4}]
                    },
                    {
                        c: [{v: 2}, {v: 4.8}, {v: 6.3}]
                    },
                    {
                        c: [{v: 1}, {v: 4.2}, {v: 6.2}]
                    }
                ]
            },
            options: {
                chart: {
                    title: 'Number of Sales and Sales income from leads'
                    //subtitle: 'in millions of dollars (USD)'
                },
                width: 500,
                height: 375
            }
        };







    });


    }
})
();




 /* */

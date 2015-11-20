(function() {
    'use strict';

    angular
    .module('leads-module')
    .controller('LeadPageController', LeadPageController);

    /* @ngInject */
    function LeadPageController(uiGmapGoogleMapApi) {

        $.ajax({
            url:'http://127.0.0.1:49822/api/leads/816B85C7-98E3-11DC-A3E8-0020E024149C/tasks', 
            type:'get', 
            success: function (response) {
                for (var i = 0; i < response.length; i++) {
                    $('#task-div').before('<md-divider ></md-divider><md-list-item class="md-1-line"><div class="md-list-item-text"><p>' + response[i]['Resumo'] + '</p></div></md-list-item>');
                }
            }});
        var vm = this;
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
                mapTypeId:maps.MapTypeId.TERRAIN
            }
        };
    });
    }
})();
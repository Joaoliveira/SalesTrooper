(function() {
    'use strict';

    angular
        .module('dashboard-module')
        .controller('DashboardPageController', DashboardPageController);

    /* @ngInject */
    function DashboardPageController() {
        
        

        $.ajax({
            url:'http://localhost:49822/api/tasks/A7439F8F-FD04-11DD-953A-000C29F83A13', 
            type:'get', 
            success: function (response) {
                //response.header('Access-Control-Allow-Origin', "*");
            for (var i = 0; i < response.length; i++) {
                $('#task-div').before('<md-divider ></md-divider><md-list-item class="md-1-line"><div class="md-list-item-text"><p>' + response[i]['Resumo'] + '</p></div></md-list-item>');
            }
            }});
        var vm = this;
    }
})();
(function() {
    'use strict';

    angular
        .module('dashboard-module')
        .controller('DashboardPageController', DashboardPageController);

    /* @ngInject */
    function DashboardPageController($http) {
        var vm = this;
        vm.tasks = [];

        var config = {
            'Accept': 'application/json',
            params: {
                dataInicio: '2014-02-20',
                dataFim: '2014-02-21'

            }
        };

        var promise = $http.get('http://127.0.0.1:49822/api/salesmen/1/tasks/', config);

        promise.then(function requestDone (response) {
            console.log(response);
            vm.tasks = response.data;
        });
    }
})();
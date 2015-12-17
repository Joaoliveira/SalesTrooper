(function() {
    'use strict';

    angular
        .module('triangular.components')
        .directive('triTable', triTable);

    /* @ngInject */
    function triTable($filter, $location, $http) {
        var directive = {
            restrict: 'E',
            scope: {
                columns: '=',
                contents: '=',
                filters: '='
            },
            link: link,
            templateUrl: 'app/triangular/components/table/table-directive.tmpl.html'
        };
        return directive;

        function link($scope, $element, attrs) {
            var sortableColumns = [];
            var activeSortColumn = null;
            var activeSortOrder = false;

            // init page size if not set to default
            $scope.pageSize = angular.isUndefined(attrs.pageSize) ? 0 : attrs.pageSize;

            // init page if not set to default
            $scope.page = angular.isUndefined(attrs.page) ? 0 : attrs.page;

            // make an array of all sortable columns
            angular.forEach($scope.columns, function(column) {
                if(column.sortable) {
                    sortableColumns.push(column.field);
                }
            });

            $scope.refresh = function(resetPage) {
                if(resetPage === true) {
                    $scope.page = 0;
                }
                $scope.contents = $filter('orderBy')($scope.contents, activeSortColumn, activeSortOrder);
            };

            // if we have sortable columns sort by first by default
            if(sortableColumns.length > 0) {
                // sort first column by default
                activeSortOrder = false;
                activeSortColumn = sortableColumns[0];
                $scope.refresh();
            }

            $scope.goto = function(field){
                var locationPath;
                if($location.path() == '/tasks/search-tasks') {
                    locationPath = field.substring(1, field.length-1);
                    $location.path("/tasks/" + locationPath);
                }
                else if($location.path() == '/leads/search-leads') {
                    locationPath = field.substring(1, field.length-1);
                    $location.path("/leads/" + locationPath);
                }
                else if($location.path() == '/clients/search-clients'){
                	$location.path("/clients/" + field);
                }
                else if(field != null){
                    var promise = $http.get('http://localhost:49822/api' + $location.path() + "/invoices/" + field);

                    promise.then(function requestDone (response) {
                        if(response.data != "No invoice available")
                            window.open('http://localhost:49822/api' + $location.path() + "/invoices/" + field);
                        else
                            alert(response.data);
                    });

                }
                else {
                    console.log("lel");
                }

            };

            $scope.get = function(field){
                $location.path($location.path() + "/invoices/" + field);
            }

            $scope.sortClick = function(field) {
                if(sortableColumns.indexOf(field) !== -1) {
                    if(field === activeSortColumn) {
                        activeSortOrder = !activeSortOrder;
                    }
                    activeSortColumn = field;
                    $scope.refresh();
                }
            };

            $scope.showSortOrder = function(field, orderDown) {
                return field === activeSortColumn && activeSortOrder === orderDown;
            };

            $scope.headerClass = function(field) {
                var classes = [];
                if(sortableColumns.indexOf(field) !== -1) {
                    classes.push('sortable');
                }
                if(field === activeSortColumn) {
                    classes.push('sorted');
                }
                return classes;
            };

            $scope.cellContents = function(column, content) {
                if(angular.isDefined(column.filter)) {
                    return $filter(column.filter)(content[column.field]);
                }
                else {
                    return content[column.field];
                }
            };

            $scope.totalItems = function() {
                return $scope.contents.length;
            };

            $scope.numberOfPages = function() {
                return Math.ceil($scope.contents.length / $scope.pageSize);
            };

            $scope.pageStart = function() {
                return ($scope.page * $scope.pageSize) + 1;
            };

            $scope.pageEnd = function() {
                var end = (($scope.page + 1) * $scope.pageSize);
                if(end > $scope.contents.length) {
                    end = $scope.contents.length;
                }
                return end;
            };

            $scope.goToPage = function (page) {
                $scope.page = page;
            };
        }
    }
})();

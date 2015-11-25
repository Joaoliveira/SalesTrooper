(function() {
    'use strict';

    angular
        .module('dashboard-module')
        .controller('DashboardPageController', DashboardPageController);

    /* @ngInject */
    function DashboardPageController($http, $scope, $rootScope, $mdDialog, $mdToast, $filter, $element, triTheming, triLayout, uiCalendarConfig) {
        

        var vm = this;
        vm.tasks = [];


        vm.eventSources = [{
            events: []
        }];

        var config = {
            'Accept': 'application/json',
            params: {
                dataInicio: '1970-01-01',
                dataFim: '2015-11-30'

            }
        };

        $http.get('http://localhost:49822/api/salesmen/1/tasks/', config).then(function(response){
            vm.tasks = response.data;


            for (var i = 0; i < vm.tasks.length; i++) {
                vm.eventSources[0].events.push({
                    title: vm.tasks[i].Resumo,
                    start: vm.tasks[i].DataDeInicio,
                    end: vm.tasks[i].DataDeFim,
                    allDay: false
                });
            };
            
        });




        //CALENDAR

        vm.calendarOptions = {
            contentHeight: '500px',
            selectable: true,
            editable: true,
            header: false,
            defaultView: 'agendaDay',
            scrollTime: '08:00:00',
            slotDuration: '01:00:00',
            timeFormat: 'HH:mm',
            viewRender: function(view) {
                // change day
                vm.currentDay = view.calendar.getDate();
                vm.currentView = 'agendaDay';
                // update toolbar with new day for month name
                $rootScope.$broadcast('calendar-changeday', vm.currentDay);
            },
            dayClick: function(date, jsEvent, view) { //eslint-disable-line
                vm.currentDay = date;
            },
            eventClick: function(calEvent, jsEvent, view) { //eslint-disable-line
                $mdDialog.show({
                    controller: 'EventDialogController',
                    controllerAs: 'vm',
                    templateUrl: 'app/examples/calendar/event-dialog.tmpl.html',
                    targetEvent: jsEvent,
                    focusOnOpen: false,
                    locals: {
                        dialogData: {
                            title: 'CALENDAR.EDIT-EVENT',
                            confirmButtonText: 'CALENDAR.SAVE'
                        },
                        event: calEvent,
                        edit: true
                    }
                })
                .then(function(event) {
                    var toastMessage = 'CALENDAR.EVENT.EVENT-UPDATED';
                    if(angular.isDefined(event.deleteMe) && event.deleteMe === true) {
                        // remove the event from the calendar
                        uiCalendarConfig.calendars['triangular-calendar'].fullCalendar('removeEvents', event._id);
                        // change toast message
                        toastMessage = 'CALENDAR.EVENT.EVENT-DELETED';
                    }
                    else {
                        // update event
                        uiCalendarConfig.calendars['triangular-calendar'].fullCalendar('updateEvent', event);
                    }

                    // pop a toast
                    $mdToast.show(
                        $mdToast.simple()
                        .content($filter('translate')(toastMessage))
                        .position('bottom right')
                        .hideDelay(2000)
                    );
                });
            }
        };

        vm.viewFormats = {
            'month': 'MMMM YYYY',
            'agendaWeek': 'w',
            'agendaDay': 'Do MMMM YYYY'
        };
    }
})();
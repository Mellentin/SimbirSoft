var app = angular.module('AddressessApp', ['angularUtils.directives.dirPagination', 'daterangepicker']);

app.controller('AddressessCtrl', function ($scope, $http, $filter) {
    $scope.addressess = [];
    $scope.curpage = 1;
    $scope.loading = true;
    $http.get('/Home/LoadDbData').then(function (response) {
        var json = response.data;

        for (var i = 0; i <= json.length - 1; i++) {
            json[i].date = moment(json[i].date);
            json[i].date = new Date(json[i].date);
            json[i].house = Number(json[i].house);
        }

        $scope.addressess = json;
        $scope.loading = false;
    });

    $scope.search = function (row) {
        return (
            angular.lowercase(row.country).indexOf(angular.lowercase($scope.query) || '') !== -1 ||
            angular.lowercase(row.city).indexOf(angular.lowercase($scope.query) || '') !== -1 ||
            angular.lowercase(row.street).indexOf(angular.lowercase($scope.query) || '') !== -1
        );
    };

    $scope.datePicker = { startDate: null, endDate: null };

    $scope.opts = {
        "showDropdowns": false,
        "autoApply": true,
        "locale": {
            "format": "DD.MM.YYYY",
            "separator": " - ",
            "applyLabel": "Подтвердить",
            "cancelLabel": "Отменить",
            "weekLabel": "W",
            "daysOfWeek": [
                "Вс",
                "Пн",
                "Вт",
                "Ср",
                "Чт",
                "Пт",
                "Сб"
            ],
            "monthNames": [
                "Январь",
                "Февраль",
                "Март",
                "Апрель",
                "Май",
                "Июнь",
                "Июль",
                "Август",
                "Сентябрь",
                "Октябрь",
                "Ноябрь",
                "Декабрь"
            ],
            "firstDay": 1
        },
        "startDate": moment().startOf('month'),
        "endDate": moment().endOf('month'),
    };

    $scope.sort = function (keyname) {
        $scope.sortKey = keyname;
        $scope.reverse = !$scope.reverse;
    };

    $scope.lastColumn = '';

    $scope.activecol = function (keyname) {
        if (($scope.activeColumns.indexOf($scope.lastColumn) !== -1) == true)
            $scope.activeColumns.splice($scope.activeColumns.indexOf($scope.lastColumn));

        if (($scope.activeColumns.indexOf(keyname) !== -1) == false) {
            $scope.activeColumns.push(keyname);
        }
    }

    $scope.clearFilter = function () {
        $scope.search = {};
        $scope.sortKey = undefined;
        $scope.reverse = undefined;
        $scope.activeColumn = undefined;
        $scope.activeColumns = [];
        $scope.datePicker.startDate = undefined;
        $scope.datePicker.endDate = undefined;
        $scope.currentNumber = undefined;
        $('#drp').val("");
    };

    $scope.activeColumns = [];

    $('#country').on('input keyup', function (e) {
        if ($('#country').val().length == 0) {
            if (($scope.activeColumns.indexOf('country') !== -1) == true)
                $scope.activeColumns.splice($scope.activeColumns.indexOf('country'), 1);
        }
        else {
            if (($scope.activeColumns.indexOf('country') !== -1) == false)
                $scope.activeColumns.push('country');
        }
    });

    $('#city').on('input keyup', function (e) {
        if ($('#city').val().length == 0) {
            if (($scope.activeColumns.indexOf('city') !== -1) == true)
                $scope.activeColumns.splice($scope.activeColumns.indexOf('city'), 1);
        }
        else {
            if (($scope.activeColumns.indexOf('city') !== -1) == false)
                $scope.activeColumns.push('city');
        }
    });

    $('#street').on('input keyup', function (e) {
        if ($('#street').val().length == 0) {
            if (($scope.activeColumns.indexOf('street') !== -1) == true)
                $scope.activeColumns.splice($scope.activeColumns.indexOf('street'), 1);
        }
        else {
            if (($scope.activeColumns.indexOf('street') !== -1) == false)
                $scope.activeColumns.push('street');
        }
    });

    $('#house').on('input keyup', function (e) {
        if ($('#house').val().length == 0) {
            if (($scope.activeColumns.indexOf('house') !== -1) == true)
                $scope.activeColumns.splice($scope.activeColumns.indexOf('house'), 1);
        }
        else {
            if (($scope.activeColumns.indexOf('house') !== -1) == false)
                $scope.activeColumns.push('house');
        }
    });

    $('#zipcode').on('input keyup', function (e) {
        if ($('#zipcode').val().length == 0) {
            if (($scope.activeColumns.indexOf('zipcode') !== -1) == true)
                $scope.activeColumns.splice($scope.activeColumns.indexOf('zipcode'), 1);
        }
        else {
            if (($scope.activeColumns.indexOf('zipcode') !== -1) == false)
                $scope.activeColumns.push('zipcode');
        }
    });

    $('#drp').on('input onchange', function (e) {
        if ($('#drp').val().length == 0) {
            if (($scope.activeColumns.indexOf('drp') !== -1) == true)
                $scope.activeColumns.splice($scope.activeColumns.indexOf('drp'), 1);
        }
        else {
            if (($scope.activeColumns.indexOf('drp') !== -1) == false)
                $scope.activeColumns.push('drp');
        }
    });
});

app.filter('dateRange', function () {
    return function (items, startDate, endDate) {
        var result = [];

        if (!startDate && !endDate) {
            return items;
        }

        angular.forEach(items, function (obj) {
            var received = obj.date;
            if (moment(received).isAfter(startDate) && moment(received).isBefore(endDate)) {
                result.push(obj);
            }
        });

        return result;
    }
});

app.filter('numberInt', function () {
    return function (items, number) {
        var result = [];

        if (!number) {
            return items;
        }

        angular.forEach(items, function (obj) {
            var received = obj.house;
            if (number === received) {
                result.push(obj);
            }
        });

        return result;
    }
});
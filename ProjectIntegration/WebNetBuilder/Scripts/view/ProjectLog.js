/// <reference path="../jquery-2.2.3.min.js" />
/// <reference path="../JqueryFN.js" />
/// <reference path="../angular.min.js" />
var myApp = angular.module("myApp", []);
myApp.controller("controllerCtrl", function ($scope, $http) {
    $http.get("/api/project").success(function (rel) {
        $scope.list = rel;

        $scope.loadLogs = function (p) {
            if (p != null) {
                var path = "http://" + location.host + "/api/projectlog?name=" + p.ProjectName + "&take=-1";

                // alert("开始加载日志信息请稍后:" + path);
                var log = { Value: "", path: path };
                p.Logs = log;
                $http.get("/api/projectlog?name=" + p.ProjectName + "&take=50").success(function (value) {
                    if (value != null) {
                        log.Value = value;



                    }
                });


            }
        }

        ShowFirstTable();
    });
});

function ShowFirstTable() {
    setTimeout(function () {
        $('#myTab a:first').tab('show');
    }, 0);
}
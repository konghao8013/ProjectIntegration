/// <reference path="../jquery-2.2.3.min.js" />
/// <reference path="../JqueryFN.js" />
/// <reference path="../angular.min.js" />
var myApp = angular.module("myApp", []);
myApp.controller("controllerCtrl", function ($scope, $http) {
    $http.get("/api/project").success(function (rel) {
        $scope.list = rel;

        $scope.loadLogs = function (p) {
            if (p != null) {
                var path = location.href + "/api/projectlog?name=" + p.ProjectName;

                alert("开始加载日志信息请稍后:" + path);
                $http.get("/api/projectlog?name=" + p.ProjectName).success(function (value) {
                    if (value != null) {
                        alert("日志加载完成正在渲染");
                        p.Logs = { Value: value };


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
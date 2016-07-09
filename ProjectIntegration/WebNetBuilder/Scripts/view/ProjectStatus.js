/// <reference path="../angular.js" />
var app = angular.module('myApp', []);
app.controller('customersCtrl', function ($scope, $http) {

    $http.get("/api/project").success(function (rel) {
       
        $scope.list = rel;

        $scope.BuildProject = function (p) {
            $http.get("/api/project?name=" + p.ProjectName).success(function (rel) {
                var value = "";
                
                if (rel == "-1") {
                    value = "项目名称错误";
                } else if (rel.indexOf("Start") > -1) {
                    value = "项目初始化";
                } else if (rel.indexOf("Underway") > -1) {
                    value = "生成进行中";

                }
                else if (rel.indexOf("Stop")) {
                    value = "生成成功";
                }
                alert(value + "_code:" + rel);
                if (value.length > 1) {
                    p.Status = value;
                    
                }
               
            }).error(function () {
                alert("重新生成项目失败");
            });
        };
    }).error(function () {
        alert("项目信息获取错误");
    });
});
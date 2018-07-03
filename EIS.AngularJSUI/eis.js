    $("#menu-toggle").click(function (e) {
        e.preventDefault();
        $("#wrapper").toggleClass("active");
    });

    var appEIS = angular.module('appEIS', ['ngRoute', 'ngCookies', 'angularUtils.directives.dirPagination', 'angular-loading-bar']);

appEIS.run(function ($rootScope, $cookies, $http) {
        if ($cookies.get("Auth") == null) {
            $cookies.put("Auth", "false");
    }
    $rootScope.Auth = $cookies.get("Auth");
    $http.defaults.headers.common['my_Token'] = "123456789";
    $rootScope.ServiceUrl = "http://localhost:60736/api/";
   
});

appEIS.config(function ($routeProvider, $httpProvider) {
    $routeProvider.when('/Home', { templateUrl: 'Views/Common/Home/Home.html', controller: 'homeController' });
    $routeProvider.when('/Login', { templateUrl: 'Views/Common/Login/Login.html', controller: 'loginController' });
    $routeProvider.when('/RecoverPassword', { templateUrl: 'Views/Common/RecoverPassword/RecoverPassword.html', controller: 'recoverPasswordController' });
    $routeProvider.when('/EmployeeManagement', { templateUrl: 'Views/Employee/EmployeeMgmt/EmployeeMgmt.html', controller: 'employeeMgmtController' });
    $routeProvider.when('/EmployeeProfile/:EmployeeId?', { templateUrl: 'Views/Employee/EmployeeUpdate/EmployeeUpdate.html', controller: 'employeeUpdateController' });
    $routeProvider.when('/ProfileView', { templateUrl: 'Views/Employee/EmployeeView/EmployeeView.html', controller: 'employeeViewController' });
    $routeProvider.when('/Logout', {
        resolve: {
            auth: function ($rootScope, $location, $cookies) {

                $cookies.put("Auth", "false");
                $rootScope.Auth = $cookies.get("Auth");

                $cookies.put("EmpSignIn", null);
                $rootScope.EmpSignIn = $cookies.get("EmpSignIn");

                $location.path('/Login');
            }
        }
    });
    $routeProvider.otherwise({ redirectTo: '/Home' });
});

appEIS.directive('fileModel', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;

            element.bind('change', function () {
                scope.$apply(function () {
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
});

appEIS.factory("utilityService", function ($http) {
    utilityObj = {};

    utilityObj.myAlert = function () {
        $("#alert").fadeTo(2000, 500).slideUp(1000, function () {
            $("#alert").slideUp(1000);
        });
    };

    utilityObj.randomPassword = function () {
        return Math.random().toString(36).substr(2, 5);
    };

    utilityObj.uploadFile = function (file, uploadUrl, eid) {
        var fd = new FormData();
        fd.append('file', file);

        var Img;

        Img = $http({
            method: 'Post', url: uploadUrl + eid, data: fd, transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).
        then(function (response) {

            return response.data;

        }, function (error) {
            return error.data;
        });

        return Img;
    }

    utilityObj.getFile = function (getFileUrl, eid) {
        var Emps;

        Emps = $http({ method: 'Get', url: getFileUrl, params: { Id: eid } }).
        then(function (response) {
            return response.data;
        });

        return Emps;
    };

    return utilityObj;
})

appEIS.controller('appEISController', function ($scope, $rootScope, $location, $cookies, utilityService) {

   

    $rootScope.$on("$routeChangeStart", function (event, next, current) {
        var Guest = ['/Home', '/RecoverPassword']
        var User = ['/Home', '/Logout', '/EmployeeProfile/:EmployeeId?'];
        var Admin = ['/Home', '/Logout', '/EmployeeProfile/:EmployeeId?', '/EmployeeManagement'];

        if ($rootScope.Auth == 'false' && $.inArray(next.$$route.originalPath, Guest) == -1) {
            $location.path('/Login');
        }

        else {
            $rootScope.EmpSignIn = JSON.parse($cookies.get("EmpSignIn"));
            role = $rootScope.EmpSignIn.Role.RoleCode;         

            if (role == 'A' && $.inArray(next.$$route.originalPath, Admin) == -1) {               
                $location.path('/Home');
            }
            else if (role == 'U' && $.inArray(next.$$route.originalPath, User) == -1) {               
                $location.path('/Home');
            }

            utilityService.getFile($rootScope.ServiceUrl + "Upload", $rootScope.EmpSignIn.EmployeeId).then(function (result) {
                $rootScope.imgSideBar = result;
            });
        }

        
    });
});





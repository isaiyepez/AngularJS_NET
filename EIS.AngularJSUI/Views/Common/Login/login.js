appEIS.factory('loginService', function ($http, $rootScope) {
    loginObj = {};

    loginObj.getByEmp = function (employee) {
        var Emp;

        Emp = $http({
            method: 'POST', url: $rootScope.ServiceUrl +'Login', data: employee
        }).
        then(function (response) {
            return response.data;
        }, function (error) {
            return error.data;
        });

        return Emp;
    };

    return loginObj;
});

appEIS.controller('loginController', function ($scope, loginService, $cookies, $rootScope, $location) {
    $scope.Login = function (emp, IsValid) {
        if (IsValid) {
            loginService.getByEmp(emp).then(function (result) {
                if (result.ModelState == null) {

                    $scope.Emp = result;
                    $scope.errorMsgs = "";

                    $cookies.put("Auth", "true");
                    $rootScope.Auth = $cookies.get("Auth");

                    $cookies.put("EmpSignIn", JSON.stringify($scope.Emp));
                    $rootScope.EmpSignIn = JSON.parse($cookies.get("EmpSignIn"));

                    $location.path('/');
                }
                else {
                    $scope.serverErrorMsgs = result.ModelState;
                }
            });
        }
    }
});
























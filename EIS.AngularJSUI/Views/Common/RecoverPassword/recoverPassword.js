appEIS.factory('recoverPasswordService', function ($http,$rootScope) {
    recoverPasswordObj = {};

    recoverPasswordObj.getByEmp = function (employee) {
        var Emp;

        Emp = $http({
            method: 'GET', url:$rootScope.ServiceUrl+'Login/RecoverPassword', params: { empEmail: employee.Email } 
        }).
        then(function (response) {
            return response.data;
        }, function (error) {
            return error.data;
        });

        return Emp;
    };

    return recoverPasswordObj;
});

appEIS.controller('recoverPasswordController', function ($scope, recoverPasswordService, utilityService) {
    $scope.RecoverPassword = function (emp, IsValid) {
        console.log(emp);
        if (IsValid) {
            recoverPasswordService.getByEmp(emp).then(function (result) {
                if (result.ModelState == null) {
                    $scope.Msg = " You login credentials has been emailed. Kindly check you email.";
                    $scope.Flg = true;
                    $scope.serverErrorMsgs = "";
                    utilityService.myAlert();
                }
                else {
                    $scope.serverErrorMsgs = result.ModelState;
                }
            });
        }
    }
});
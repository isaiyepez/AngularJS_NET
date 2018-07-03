appEIS.factory('employeeMgmtService', function ($http,$rootScope) {
    empMgmtObj = {};

    empMgmtObj.getAll = function () {
        var Emps;

        Emps = $http({ method: 'Get', url:$rootScope.ServiceUrl+ 'Employee' }).
        then(function (response) {
            return response.data;

        });

        return Emps;
    };

    empMgmtObj.createEmployee = function (emp) {
        var Emp;

        Emp = $http({ method: 'Post', url:$rootScope.ServiceUrl+ 'Employee', data: emp }).
        then(function (response) {
            
            return response.data;
            
        }, function(error) {
            return error.data;
        });

        return Emp;
    };

    empMgmtObj.deleteEmployeeById = function (eid) {
        var Emps;

        Emps = $http({ method: 'Delete', url:$rootScope.ServiceUrl+ 'Employee', params: { id: eid } }).
        then(function (response) {
            return response.data;

        });

        return Emps;
    };

    return empMgmtObj;
});

appEIS.controller('employeeMgmtController', function ($scope, employeeMgmtService, utilityService, $window) {

    $scope.Sort = function (col) {
        $scope.key = col;
        $scope.AscOrDesc = !$scope.AscOrDesc;
    };

    employeeMgmtService.getAll().then(function (result) {
        $scope.Emps = result;
    });

    $scope.CreateEmployee = function (Emp, IsValid) {
        if (IsValid) {
            $scope.Emp.Password = utilityService.randomPassword();

            employeeMgmtService.createEmployee(Emp).then(function (result) {
                if (result.ModelState == null) {
                    $scope.Msg = " You have successfully created " + result.EmployeeId;
                    $scope.Flg = true;
                    utilityService.myAlert();

                    $scope.serverErrorMsgs = "";

                    employeeMgmtService.getAll().then(function (result) {
                        $scope.Emps = result;
                    });
                }
                else {
                    $scope.serverErrorMsgs = result.ModelState;
                }
            });
        }
    };

    $scope.DeleteEmployeeById = function (Emp) {
        if ($window.confirm("Do you want to delete Employee with Id:" + Emp.EmployeeId + "?")) {
            employeeMgmtService.deleteEmployeeById(Emp.EmployeeId).then(function (result) {
                if (result.ModelState == null) {
                    $scope.Msg = " You have successfully deleted " + result.EmployeeId;
                    $scope.Flg = true;
                    utilityService.myAlert();

                    employeeMgmtService.getAll().then(function (result) {
                        $scope.Emps = result;
                    });
                }
                else {
                    $scope.serverErrorMsgs = result.ModelState;
                }
            });
        }
    };


    $scope.CreateMultiEmployee = function () {
        var file = $scope.myFile;
        var uploadUrl = "http://localhost:60736/api/Upload/";
        utilityService.uploadFile(file, uploadUrl, $scope.eid).then(function (result) {
            $scope.image = result;
        });
    };
});
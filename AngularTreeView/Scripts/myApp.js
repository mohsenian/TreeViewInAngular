var app = angular.module('myApp', ['angularTreeview']);
app.controller('myController',['$scope','$http',function($scope,$http){
    $http.get('/home/GetCarStructure').then(function(response){
        $scope.List = response.data.treeList;
    })
}])
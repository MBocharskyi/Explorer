(function () {
    'use strict';
    var controllerId = 'ExplorerCtrl';

    angular.module('ExplorerApp')
    .controller(controllerId, ['$scope', 'explorerService', explorerController]);

    function explorerController($scope, explorerService) {

        $scope.Init = function () {
            getDirectoryContent('');
        }

        $scope.driveClick = function (drive) {
            getDirectoryContent(drive);
        }

        $scope.parentFolderClick = function (ParentPath) {
            getDirectoryContent(ParentPath);
        }

        $scope.directoryClick = function (directory) {
            getDirectoryContent(directory.FullPath);
        }

        function getDirectoryContent(path) {
            explorerService.getDirectoryContent(path)
                .success(function (data) {
                    $scope.ParentPath = data.ParentPath;
                    $scope.CurrentPath = data.CurrentPath;
                    $scope.CountOfFilesLessThanTenMb = data.CountOfFilesLessThanTenMb;
                    $scope.CountOfFilesMoreThanTenAndLessThanFiftyOneMb = data.CountOfFilesMoreThanTenAndLessThanFiftyOneMb;
                    $scope.CountOfFilesMoreThanOneHundredMb = data.CountOfFilesMoreThanOneHundredMb;
                    $scope.Drives = data.Drives;
                    $scope.Directories = data.Directories;
                    $scope.Files = data.Files;
                })
        .error(function (data) {
            alert("There was an error while loading the data.\r\n" + data.Message);
        });
        }
    }
})();
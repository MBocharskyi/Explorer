(function () {
    'use strict';

    var serviceId = 'explorerService';
    angular.module('ExplorerApp').factory(serviceId, ['$http', explorerService]);

    function explorerService($http) {
        function getDirectoryContent(pth) {
            return $http({
                url: '/api/FileExplorer/',
                method: 'GET',
                params: { path: angular.toJson(pth, true) }
            })
            //.get('/api/FileExplorer/?path="' + path + '"')
        }

        var service = {
            getDirectoryContent: getDirectoryContent
        }

        return service;
    }
})();
app.factory('backendHubProxy', [
    '$rootScope', 'backendServerUrl',
    function($rootScope, backendServerUrl) {

        function backendFactory(serverUrl, hubName) {
            var connection = $.hubConnection(backendServerUrl);
            var proxy = connection.createHubProxy(hubName);

            connection.start().done(function() {});

            return {
                on: function(eventName, callback) {
                    proxy.on(eventName, function(result) {
                        $rootScope.$apply(function() {
                            if (callback) {
                                callback(result);
                            }
                        });
                    });
                },
                invoke: function(methodName, callback) {
                    proxy.invoke(methodName)
                        .done(function(result) {
                            $rootScope.$apply(function() {
                                if (callback) {
                                    callback(result);
                                }
                            });
                        });
                }
            };
        };

        return backendFactory;
    }
]);


app.factory('AccountService', [
    '$resource', function($resource) {
        return $resource('/Account', null, {
            getList: { method: 'GET', url: '/Account/Get', isArray: true },
            internalTransfer: { method: 'POST', url: '/Account/TransferToMyAccount', isArray: false },
            externalTransfer: { method: 'POST', url: '/Account/TransferToOtherAccount', isArray: false }
        });
    }
]);
app.controller('AccountController', [
    'AccountService', '$uibModal', 'backendHubProxy', function (accountService, $uibModal, backendHubProxy) {
        this.accounts = accountService.getList();

        this.makeTransfer = function(templateName, controllerName) {
            var accounts = this.accounts;
            $uibModal.open({
                templateUrl: templateName,
                controller: controllerName,
                resolve: {
                    accounts: function () { return accounts; }
                }
            });
        }

        this.makeInternalTransfer = function () {
            this.makeTransfer('internalTransfer.html', 'InternalTransferController');
        }

        this.makeSpecialStransfer = function() {
            this.makeTransfer('externalTransfer.html', 'ExternalTransferController');
        }

        // subsribe on messages sent by server hub
        var hub = backendHubProxy(backendHubProxy.defaultServer, 'accountHub');
        hub.on('onAccountChanged', function (account) {
            var index = this.accounts.findIndex(function(el) {
                return el.AccountNumber === account.AccountNumber;
            });
            this.accounts.splice(index, 1);
            this.accounts.splice(index, 0, account);
            
        }.bind(this));
    }
]);

app.controller('InternalTransferController', [
    '$scope', '$uibModalInstance', 'AccountService', 'accounts', function ($scope, $uibModalInstance, accountService, accounts) {
        $scope.sourceAccounts = accounts;
        $scope.destAccounts = [];
        $scope.selectedSourceAccount = null;
        $scope.selectedDestAccount = null;
        $scope.amountToTransfer = 0;

        $scope.updateDestAccounts = function() {
            $scope.destAccounts = [];
            $scope.selectedDestAccount = null;
            for (var i = 0; i < $scope.sourceAccounts.length; i++) {
                if ($scope.sourceAccounts[i].AccountNumber !== $scope.selectedSourceAccount)
                    $scope.destAccounts.push($scope.sourceAccounts[i]);
            }
        }

        $scope.transfer = function() {
            accountService.internalTransfer({ sourceAccount: $scope.selectedSourceAccount, destAccount: $scope.selectedDestAccount, amount: $scope.amountToTransfer },
                function(response) {
                    $uibModalInstance.dismiss('ok');
                }.bind(this),
                function (error) {
                    $scope.errors = error.data.Details;
                    $scope.errors.Summary = error.data.Message;
                }.bind(this));
        }

        $scope.cancel = function() {
            $uibModalInstance.dismiss('cancel');
        }
    }
]);

app.controller('ExternalTransferController', [
    '$scope', '$uibModalInstance', 'AccountService', 'accounts', function ($scope, $uibModalInstance, accountService, accounts) {
        $scope.sourceAccounts = accounts;
        $scope.selectedSourceAccount = null;
        $scope.amountToTransfer = 0;
        $scope.destAccountNumber = null;

        $scope.transfer = function () {
            accountService.externalTransfer({ sourceAccount: $scope.selectedSourceAccount, destAccount: $scope.destAccountNumber, amount: $scope.amountToTransfer },
                function (response) {
                    $uibModalInstance.dismiss('ok');
                }.bind(this),
                function (error) {
                    $scope.errors = error.data.Details;
                    $scope.errors.Summary = error.data.Message;
                }.bind(this));
        }

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        }
    }
]);
app.controller('AccountController', [
    'AccountService', '$uibModal', 'backendHubProxy', function (accountService, $uibModal, backendHubProxy) {
        this.accounts = accountService.getAccounts();
        this.journals = accountService.getJournals();

        this.init = function(customerId) {
            this.customerId = customerId;
        }

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
        var hub = backendHubProxy(backendHubProxy.defaultServer, 'notificationHub');
        hub.on('onBalanceChanged', function (message) {
            var account = this.accounts.find(function(el) {
                return el.AccountNumber === message.AccountNumber;
            });
            if (typeof account !== "undefined" && account !== null) {
                account.Balance = message.CurrentBalance; 
            }
        }.bind(this));

        hub.on('onJournalCreated', function (message) {
            if (message.CustomerId === this.customerId) {
                this.journals.splice(0, 0, message);
            }
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
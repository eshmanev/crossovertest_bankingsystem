var accountModule = angular
    .module('AccountModule', ['ui.bootstrap', 'ngResource']);

accountModule.factory('AccountService', [
    '$resource', function ($resource) {
        return $resource('/Account', null, {
            getList: { method: 'GET', url: '/Account/Get', isArray: true },
            transfer: { method: 'POST', url: '/Account/Transfer', isArray: false }
        });
    }
]);

accountModule.controller('AccountController', [
    'AccountService', '$uibModal', function(accountService, $uibModal) {
        this.accounts = accountService.getList();

        this.makeInternalTransfer = function () {
            var accounts = this.accounts;
            $uibModal.open({
                templateUrl: 'internalTransfer.html',
                controller: 'AccountInternalTransferController',
                resolve: {
                    accounts: function() { return accounts; }
                }
            });

        }

        this.makeSpecialStransfer = function() {

        }
    }
]);

accountModule.controller('AccountInternalTransferController', [
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
            accountService.transfer({sourceAccount: $scope.selectedSourceAccount, destAccount: $scope.selectedDestAccount, amount: $scope.amountToTransfer},
                function(response) {
                    
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
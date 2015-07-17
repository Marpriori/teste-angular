angular.module('app').controller('VagaController',
        function vagaController($scope, $http) {
            $scope.loading = true;
            $scope.addMode = false;

            //Mostrar os dados
            $http.get('/api/Vaga/').success(function (data) {
                $scope.vagas = data;
                $scope.loading = false;
            })
                .error(function () {
                    $scope.error = "Desculpe. Tivemos um problema ao listar as vagas.";
                    $scope.loading = false;
                });

            //Ativar edição
            $scope.toggleEdit = function () {
                this.vaga.editMode = !this.vaga.editMode;
            };

            //Ativar inclusão
            $scope.toggleAdd = function () {
                $scope.addMode = !$scope.addMode;
            };

            //Salvar alteração depois editar
            $scope.save = function () {
               
                $scope.loading = true;
                var _vaga = this.vaga;
                
                $http.post('/api/Vaga/', _vaga).success(function (data) {
                    alert("Vaga salva com sucesso!!");
                    _vaga.editMode = false;
                    $scope.loading = false;
                }).error(function (data) {
                    $scope.error = "Opa. Tivemos um problema ao salvar a vaga. " + data.Message;
                    $scope.loading = false;
                });
            };

            //Inclusão de vaga  
            $scope.add = function () {
                $scope.loading = true;
                $http.post('/api/Vaga/', this.newvaga).success(function (data) {
                    alert("Vaga registrada com sucesso!!");
                    $scope.addMode = false;
                    $scope.vagas.push(data);
                    $scope.loading = false;
                }).error(function (data) {
                    $scope.error = "Opa. Tivemos um problema ao salvar a vaga. " + data.Message;
                    $scope.loading = false;

                });
            };

            //Apagar de vaga
            $scope.deletevaga = function () {
                if (confirm("Excluir vaga?")) {
                    $scope.loading = true;
                    var vagaid = this.vaga.Id;
                    $http.delete('/api/Vaga/' + vagaid).success(function (data) {
                        alert("Vaga removida com sucesso!!");
                        $.each($scope.vagas, function (i) {
                            if ($scope.vagas[i].Id === vagaid) {
                                $scope.vagas.splice(i, 1);
                                return false;
                            }
                        });
                        $scope.loading = false;
                    }).error(function (data) {
                        $scope.error = "Opa. Tivemos um problema ao remover a vaga. " + data.Message;
                        $scope.loading = false;

                    });
                };
            }
        }
);


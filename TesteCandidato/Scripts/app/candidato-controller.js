angular.module('app').controller('CandidatoController',
        function candidatoController($scope, $http) {
            $scope.loading = true;
            $scope.addMode = false;

            //Mostrar os dados
            $http.get('/api/Candidato/').success(function (data) {
                $scope.candidatos = data;
                $scope.loading = false;
            })
                .error(function () {
                    $scope.error = "Desculpe. Tivemos um problema ao listar os candidatos.";
                    $scope.loading = false;
                });

            //Ativar edição
            $scope.toggleEdit = function () {
                this.candidato.editMode = !this.candidato.editMode;
            };

            //Ativar inclusão
            $scope.toggleAdd = function () {
                $scope.addMode = !$scope.addMode;
            };

            //Salvar alteração depois editar
            $scope.save = function () {
               
                $scope.loading = true;
                var _candidato = this.candidato;
                
                $http.post('/api/Candidato/', _candidato).success(function (data) {
                    alert("Candidato salvo com sucesso!!");
                    _candidato.editMode = false;
                    $scope.loading = false;
                }).error(function (data) {
                    $scope.error = "Opa. Tivemos um problema ao salvar o candidato. " + data.Message;
                    $scope.loading = false;
                });
            };

            //Inclusão de candidato  
            $scope.add = function () {
                $scope.loading = true;
                $http.post('/api/Candidato/', this.newcandidato).success(function (data) {
                    alert("Candidato registrado com sucesso!!");
                    $scope.addMode = false;
                    $scope.candidatos.push(data);
                    $scope.loading = false;
                }).error(function (data) {
                    $scope.error = "Opa. Tivemos um problema ao salvar o candidato. " + data.Message;
                    $scope.loading = false;

                });
            };

            //Apagar candidato
            $scope.deletecandidato = function () {
                if (confirm("Excluir candidato?")) {
                    $scope.loading = true;
                    var candidatoid = this.candidato.Id;
                    $http.delete('/api/Candidato/' + candidatoid).success(function (data) {
                        alert("Candidato removido com sucesso!!");
                        $.each($scope.candidatos, function (i) {
                            if ($scope.candidatos[i].Id === candidatoid) {
                                $scope.candidatos.splice(i, 1);
                                return false;
                            }
                        });
                        $scope.loading = false;
                    }).error(function (data) {
                        $scope.error = "Opa! Tivemos um problema ao remover o candidato. " + data.Message;
                        $scope.loading = false;

                    });
                };
            }
        }
);


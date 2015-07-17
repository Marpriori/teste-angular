angular.module('app').controller('VagaCandidatosController',
        function vagaCandidatosController($scope, $http) {
            $scope.loading = true;
            $scope.addMode = false;
            var url = window.location.pathname;
            var id = url.substring(url.lastIndexOf('/') + 1);

            //Mostrar os dados
            $http.get('/api/Vaga/' + id).success(function (data) {
                $scope.vaga = data;

                $http.get('/api/Candidato/').success(function (data) {
                    $scope.candidatos = data;
                    $scope.loading = false;
                }).error(function () {
                    $scope.error = "Desculpe. Tivemos um problema ao listar as candidatos.";
                    $scope.loading = false;
                });
                
                $http.get('/api/VagaCandidatos/' + id).success(function (data) {
                    $scope.vagaCandidatos = data;
                    $scope.loading = false;
                }).error(function () {
                    $scope.error = "Desculpe. Tivemos um problema ao listar as candidatos.";
                    $scope.loading = false;
                });
                $scope.loading = false;
            })
                .error(function () {
                    $scope.error = "Desculpe. Tivemos um problema ao carregar a vaga.";
                    $scope.loading = false;
                });

            //Ativar edição
            $scope.toggleEdit = function () {
                this.vagaCandidato.editMode = !this.vagaCandidato.editMode;
            };

            //Ativar inclusão
            $scope.toggleAdd = function () {
                $scope.addMode = !$scope.addMode;
            };

            //Inclusão de vagaCandidato  
            $scope.add = function () {
                $scope.loading = true;
                this.newvagaCandidato.VagaID = $scope.vaga.Id;
                $http.post('/api/vagaCandidato/', this.newvagaCandidato).success(function (data) {
                    alert("Candidato concorrendo a vaga!!");
                    $scope.addMode = false;
                    $scope.vagaCandidatos.push(data);
                    $scope.loading = false;
                }).error(function (data) {
                    $scope.error = "Opa. Tivemos um problema ao salvar o candidato. " + data.Message;
                    $scope.loading = false;

                });
            };

            //Apagar de vagaTecnologia
            $scope.deletevagaCandidato = function () {
                if (confirm("Excluir candidatura?")) {
                    $scope.loading = true;
                    var vagaCandidatoid = this.vagaCandidato.Id;
                    $http.delete('/api/vagaCandidato/' + vagaCandidatoid).success(function (data) {
                        alert("Vaga removida com sucesso!!");
                        $.each($scope.vagaCandidatos, function (i) {
                            if ($scope.vagaCandidatos[i].Id === vagaCandidatoid) {
                                $scope.vagaCandidatos.splice(i, 1);
                                return false;
                            }
                        });
                        $scope.loading = false;
                    }).error(function (data) {
                        $scope.error = "Opa. Tivemos um problema ao remover o candidato da vaga. " + data.Message;
                        $scope.loading = false;

                    });
                };
            }
        }
);


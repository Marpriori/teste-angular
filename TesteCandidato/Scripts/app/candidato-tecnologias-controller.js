angular.module('app').controller('CandidatoTecnologiasController',
        function candidatoTecnologiasController($scope, $http) {
            $scope.loading = true;
            $scope.addMode = false;
            var url = window.location.pathname;
            var id = url.substring(url.lastIndexOf('/') + 1);

            //Mostrar os dados
            $http.get('/api/Candidato/' + id).success(function (data) {
                $scope.candidato = data;

                $http.get('/api/Tecnologia/').success(function (data) {
                    $scope.tecnologias = data;
                    $scope.loading = false;
                }).error(function () {
                    $scope.error = "Desculpe. Tivemos um problema ao listar as tecnologias.";
                    $scope.loading = false;
                });
                
                $http.get('/api/CandidatoTecnologias/' + id).success(function (data) {
                    $scope.candidatoTecnologias = data;
                    $scope.loading = false;
                }).error(function () {
                    $scope.error = "Desculpe. Tivemos um problema ao listar as tecnologias.";
                    $scope.loading = false;
                });
                $scope.loading = false;
            })
                .error(function () {
                    $scope.error = "Desculpe. Tivemos um problema ao carregar o candidato.";
                    $scope.loading = false;
                });

            //Ativar edição
            $scope.toggleEdit = function () {
                this.candidatoTecnologia.editMode = !this.candidatoTecnologia.editMode;
            };

            //Ativar inclusão
            $scope.toggleAdd = function () {
                $scope.addMode = !$scope.addMode;
            };

            //Salvar alteração depois editar
            //$scope.save = function () {

            //    $scope.loading = true;
            //    var _vagaTecnologia = this.vagaTecnologia;

            //    $http.post('/api/VagaTecnologia/', _vagaTecnologia).success(function (data) {
            //        alert("Tecnologia salva na vaga com sucesso!!");
            //        _vagaTecnologia.editMode = false;
            //        $scope.loading = false;
            //    }).error(function (data) {
            //        $scope.error = "Opa. Tivemos um problema ao salvar a tecnologia na vaga. " + data.Message;
            //        $scope.loading = false;
            //    });
            //};

            //Inclusão de candidatoTecnologia  
            $scope.add = function () {
                $scope.loading = true;
                this.newcandidatoTecnologia.CandidatoID = $scope.candidato.Id;
                $http.post('/api/CandidatoTecnologia/', this.newcandidatoTecnologia).success(function (data) {
                    alert("Tecnologia registrada com sucesso!!");
                    $scope.addMode = false;
                    $scope.candidatoTecnologias.push(data);
                    $scope.loading = false;
                }).error(function (data) {
                    $scope.error = "Opa. Tivemos um problema ao salvar a tecnologia. " + data.Message;
                    $scope.loading = false;

                });
            };

            //Apagar de vagaTecnologia
            $scope.deletecandidatoTecnologia = function () {
                if (confirm("Excluir tecnologia?")) {
                    $scope.loading = true;
                    var candidatoTecnologiaid = this.candidatoTecnologia.Id;
                    $http.delete('/api/candidatoTecnologia/' + candidatoTecnologiaid).success(function (data) {
                        alert("Tecnologia removida com sucesso!!");
                        $.each($scope.candidatoTecnologias, function (i) {
                            if ($scope.candidatoTecnologias[i].Id === candidatoTecnologiaid) {
                                $scope.candidatoTecnologias.splice(i, 1);
                                return false;
                            }
                        });
                        $scope.loading = false;
                    }).error(function (data) {
                        $scope.error = "Opa. Tivemos um problema ao remover a tecnologia do candidato. " + data.Message;
                        $scope.loading = false;

                    });
                };
            }
        }
);


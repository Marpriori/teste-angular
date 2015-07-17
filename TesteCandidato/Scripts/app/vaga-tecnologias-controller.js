angular.module('app').controller('VagaTecnologiasController',
        function vagaTecnologiasController($scope, $http) {
            $scope.loading = true;
            $scope.addMode = false;
            var url = window.location.pathname;
            var id = url.substring(url.lastIndexOf('/') + 1);

            //Mostrar os dados
            $http.get('/api/Vaga/' + id).success(function (data) {
                $scope.vaga = data;

                $http.get('/api/Tecnologia/').success(function (data) {
                    $scope.tecnologias = data;
                    $scope.loading = false;
                }).error(function () {
                    $scope.error = "Desculpe. Tivemos um problema ao listar as tecnologias.";
                    $scope.loading = false;
                });
                
                $http.get('/api/VagaTecnologias/' + id).success(function (data) {
                    $scope.vagaTecnologias = data;
                    $scope.loading = false;
                }).error(function () {
                    $scope.error = "Desculpe. Tivemos um problema ao listar as tecnologias.";
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
                this.vagaTecnologia.editMode = !this.vagaTecnologia.editMode;
            };

            //Ativar inclusão
            $scope.toggleAdd = function () {
                $scope.addMode = !$scope.addMode;
            };

            //Salvar alteração depois editar
            $scope.save = function () {

                $scope.loading = true;
                var _vagaTecnologia = this.vagaTecnologia;

                $http.post('/api/VagaTecnologia/', _vagaTecnologia).success(function (data) {
                    alert("Tecnologia salva na vaga com sucesso!!");
                    _vagaTecnologia.editMode = false;
                    $scope.loading = false;
                }).error(function (data) {
                    $scope.error = "Opa. Tivemos um problema ao salvar a tecnologia na vaga. " + data.Message;
                    $scope.loading = false;
                });
            };

            //Inclusão de vagaTecnologia  
            $scope.add = function () {
                $scope.loading = true;
                this.newvagaTecnologia.VagaID = $scope.vaga.Id;
                $http.post('/api/vagaTecnologia/', this.newvagaTecnologia).success(function (data) {
                    alert("Tecnologia registrada com sucesso!!");
                    $scope.addMode = false;
                    $scope.vagaTecnologias.push(data);
                    $scope.loading = false;
                }).error(function (data) {
                    $scope.error = "Opa. Tivemos um problema ao salvar a tecnologia. " + data.Message;
                    $scope.loading = false;

                });
            };

            //Apagar de vagaTecnologia
            $scope.deletevagaTecnologia = function () {
                if (confirm("Excluir tecnologia?")) {
                    $scope.loading = true;
                    var vagaTecnologiaid = this.vagaTecnologia.Id;
                    $http.delete('/api/vagaTecnologia/' + vagaTecnologiaid).success(function (data) {
                        alert("Vaga removida com sucesso!!");
                        $.each($scope.vagaTecnologias, function (i) {
                            if ($scope.vagaTecnologias[i].Id === vagaTecnologiaid) {
                                $scope.vagaTecnologias.splice(i, 1);
                                return false;
                            }
                        });
                        $scope.loading = false;
                    }).error(function (data) {
                        $scope.error = "Opa. Tivemos um problema ao remover a tecnologia da vaga. " + data.Message;
                        $scope.loading = false;

                    });
                };
            }
        }
);


angular.module('app').controller('TecnologiasController',
        function tecnologiaController($scope, $http) {
            $scope.loading = true;
            $scope.addMode = false;

            //Mostrar os dados
            $http.get('/api/Tecnologia/').success(function (data) {
                $scope.tecnologias = data;
                $scope.loading = false;
            })
                .error(function () {
                    $scope.error = "Desculpe. Tivemos um problema ao listar as tecnologias.";
                    $scope.loading = false;
                });

            //Ativar edição
            $scope.toggleEdit = function () {
                this.tecnologia.editMode = !this.tecnologia.editMode;
            };

            //Ativar inclusão
            $scope.toggleAdd = function () {
                $scope.addMode = !$scope.addMode;
            };

            //Salvar alteração depois editar
            $scope.save = function () {

                $scope.loading = true;
                var _tecnologia = this.tecnologia;

                $http.post('/api/Tecnologia/', _tecnologia).success(function (data) {
                    alert("Tecnologia salva com sucesso!!");
                    _tecnologia.editMode = false;
                    $scope.loading = false;
                }).error(function (data) {
                    $scope.error = "Opa. Tivemos um problema ao salvar a tecnologia. " + data.Message;
                    $scope.loading = false;
                });
            };

            //Inclusão de tecnologia  
            $scope.add = function () {
                $scope.loading = true;
                $http.post('/api/Tecnologia/', this.newtecnologia).success(function (data) {
                    alert("Tecnologia registrada com sucesso!!");
                    $scope.addMode = false;
                    $scope.tecnologias.push(data);
                    $scope.loading = false;
                }).error(function (data) {
                    $scope.error = "Opa. Tivemos um problema ao salvar a tecnologia. " + data.Message;
                    $scope.loading = false;

                });
            };

            //Apagar de tecnologia
            $scope.deletetecnologia = function () {
                if (confirm("Excluir tecnologia?")) {
                    $scope.loading = true;
                    var tecnologiaid = this.tecnologia.Id;
                    $http.delete('/api/Tecnologia/' + tecnologiaid).success(function (data) {
                        alert("Tecnologia removida com sucesso!!");
                        $.each($scope.tecnologias, function (i) {
                            if ($scope.tecnologias[i].Id === tecnologiaid) {
                                $scope.tecnologias.splice(i, 1);
                                return false;
                            }
                        });
                        $scope.loading = false;
                    }).error(function (data) {
                        $scope.error = "Opa. Tivemos um problema ao remover a tecnologia. " + data.Message;
                        $scope.loading = false;

                    });
                };
            }
        }
);
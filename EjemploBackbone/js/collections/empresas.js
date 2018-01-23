/*global Backbone */
var app = app || {};

(function () {
    'use strict';

    var TodosEmpresas = Backbone.Collection.extend({
        // Reference to this collection's model.
        model: app.Empresa,

        // Filter down the list of all todo items that are finished.
        empresa_activa: function () {
            return this.where({completed: true});
        },

        // Busca la empresa que se indique en el input
        buscar: function (empBuscar) {
            return this.where({nombre: empBuscar});
        }
    });

    var datosEmp  = [
        {
            "nombre": "axesor",

            "cif": "A11111111",
            "direccion": "Calle En un lugar de la mancha 1",
            "telefono": "123456789",
            "activa": 1

        },
        {
            "nombre": "ejemplo1",

            "cif": "B22222222",
            "direccion": "Calle Inventada sin numero",
            "telefono": "123645234",
            "activa": 1

        },
        {
            "nombre": "prueba1",

            "cif": "C33333333",
            "direccion": "Calle No existe 23",
            "telefono": "9673451324",
            "activa": 1

        }
    ];
    app.empresas = new TodosEmpresas(datosEmp);
})();



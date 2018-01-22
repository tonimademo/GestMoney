var Empresa = Backbone.Model.extend({
    defaults: {
        nombre: "",
        cif: "",
        direccion: "",
        telefono: "",
        activo: 0
    },

    validate: function(attributes){
        if (attributes.cif.length !== 9) {
            return 'CIF Incorrecto';
        }

        if ( !attributes.nombre || attributes.nombre === ""){
            return 'La empresa debe tener un nombre';
        }
    }
});


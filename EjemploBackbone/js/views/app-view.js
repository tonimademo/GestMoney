/*global Backbone, jQuery, _, ENTER_KEY */
var app = app || {};

(function ($) {
    'use strict';

    // The Application
    // ---------------

    // Our overall **AppView** is the top-level piece of UI.
    app.AppView = Backbone.View.extend({

        // Instead of generating a new element, bind to the existing skeleton of
        // the App already present in the HTML.
        el: '.global',

        // Our template for the line of statistics at the bottom of the app.
        //statsTemplate: _.template($('#stats-template').html()),

        // Delegated events for creating new items, and clearing completed ones.
        events: {
            'click #btnBuscar': 'buscar'
        },

        initialize: function () {
            this.$input = this.$('#filtrar');
            this.$tbody = this.$('.emp-list');
            this.addAll();
            //app.empresas.fetch();
        },

        // Re-rendering the App just means refreshing the statistics -- the rest
        // of the app doesn't change.
        render: function () {

        },

        // Add all items in the **Todos** collection at once.
        addAll: function () {
            this.$tbody.html('');
            app.empresas.each(function (empresa) {
                var view = new app.EmpresaView({ model: empresa });
                this.$tbody.append(view.render().el);
            }, this);
        },

        buscar: function () {
            this.$tbody.html('');
            app.empresas.each(function (empresa) {
                if (empresa.attributes.nombre === this.$input.val()){
                    var view = new app.EmpresaView({ model: empresa });
                    this.$tbody.append(view.render().el);
                }
            }, this);
        }

    });
})(jQuery);

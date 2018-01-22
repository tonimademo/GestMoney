
var Vista = Backbone.View.extend({
    tagName: 'li',

    
    initialize: function(){
        this.render();
    },

    render: function(){
        this.$el.html( this.model.get('name') + ' (' + this.model.get('age') + ') - ' + this.model.get('occupation') );
    }


});

var v = new Vista();


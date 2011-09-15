var express = require('express'), path = require('path');

var app = module.exports = express.createServer().configure(function() {
    // Root folder for views
    this.set('views', path.join(__dirname, "views"));
    
    // Default filename extension and corresponding view engine
    this.set('view engine', 'jade.txt');
    this.register('jade.txt', require('jade'));
});

require('./routes.js')(app);
app.listen(process.env.PORT || 8080);
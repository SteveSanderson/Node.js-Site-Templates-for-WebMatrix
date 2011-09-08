module.exports = function(app) {

    app.get('/', function (req, res) {
        res.render('index', { 
            message: 'Welcome to my site!' 
        });
    });
    
    app.get('/about', function (req, res) {
        res.render('about');
    });

}
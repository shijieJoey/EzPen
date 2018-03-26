var express = require('express');
const bodyParser = require('body-parser');
var app = express();

// set the port of our application
// process.env.PORT lets the port be set by Heroku
var port = process.env.PORT || 8080;

// set the view engine to ejs
app.set('view engine', 'ejs');

// make express look in the public directory for assets (css/js/img)
app.use(express.static(__dirname + '/public'));

app.use(bodyParser.urlencoded({
	extended: true
}));

app.use(bodyParser.json());

// set the home page route
app.get('/', function(req, res) {

    // ejs render automatically looks in the views folder
    res.render('index');
});

var x = "";
var y = "";
var z = "";
app.get('/search',(req,res,next)=>{
	res.json(x);

});


app.post("/test", function(req, res) {
	x = req.body;
	console.log("request: " + JSON.stringify(req.body));
	res.end();
});


app.listen(port, function() {
    console.log('The app is running on http://localhost:' + port);
});
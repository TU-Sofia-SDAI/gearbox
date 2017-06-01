const express = require('express');
const bodyParser = require('body-parser');
const app = express();

app.use( bodyParser.json() );       // to support JSON-encoded bodies
app.use(bodyParser.urlencoded({     // to support URL-encoded bodies
  extended: true
})); 

app.get('/', function (req, res) {
  res.send('Hello World!')
});

app.post('/', function (req, res) {
  console.log(req.body);
  res.end();
});

app.listen(3000, function () {
  console.log('Example app listening on port 3000!')
});

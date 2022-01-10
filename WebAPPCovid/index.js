const http = require('http')
const router = require('./routes/myRouter')
const fs = require('fs')
const express = require('express')
const app = express()
const path = require('path')
// const homePage = fs.readFileSync(`${__dirname}/webpages/home.html`)

app.set('views', path.join(__dirname, 'views'))
app.set('view engine', 'ejs')
app.use(router)
app.listen(8080, ()=>
{
console.log('start server port : 8080')
})

module.exports = app
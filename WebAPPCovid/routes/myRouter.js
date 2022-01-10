const express = require('express')
const router = express.Router()
const https = require('https')
const axios = require('axios')
const instance = axios.create({
    httpsAgent: new https.Agent({  
      rejectUnauthorized: false
    })
  });
  const agent = new https.Agent({  
    rejectUnauthorized: false
  });
router.get('/view',(req,res)=>{
    var view = req.query.view;
    var date = req.query.date;
 
    if(view === 'view')
    {
        var today = new Date(parseInt(date.toString().substring(0,4)), parseInt(date.toString().substring(5,7))-1, parseInt(date.toString().substring(8,10)))
        axios.get('https://localhost:44360/GetWithDate', { httpsAgent: agent,params: { date: today }  }) 
        .then(response => {
          // do something about response
          var resp  = response.data
            res.render('view.ejs', 
            {
                data: resp
            })
        })
        .catch(err => {
          console.error(err)
        })
    }   
})
router.get('/add_edit',(req,res)=>{
    let edit = req.query.edit;
    let date = req.query.date;
    let new_Amount = req.query.new_Amount;
    let old_GetWell_Amount = req.query.old_GetWell_Amount;
    let died_Amount = req.query.died_Amount;
    let covidID = req.query.covidID;
    var today = new Date(date)
    var day = today.getDate().toString().padStart(2, "0");
    var month = (today.getMonth() + 1) .toString().padStart(2, "0");
    var year = today.getFullYear()
    var newDay = day + "/" + month + "/" + year   
    
    if(edit === 'edit')
    {
        var _date = date.substring(9,11) + '/' + date.substring(6,8) + '/' + date.substring(1,5)//date.substring(6,8) 01 //date.substring(0,5) 2022
        // var _today = new Date(parseInt(date.toString().substring(0,4)), parseInt(date.toString().substring(5,7))-1, parseInt(date.toString().substring(8,10))+1)
        res.render('add_edit.ejs', 
        {
            date: _date,
            new_Amount: new_Amount,
            old_GetWell_Amount: old_GetWell_Amount,
            died_Amount: died_Amount,
            covidID: covidID,
            flag: ""
        })
    }
    else if(edit === 'update')
    {
        const Agent = new https.Agent({
            rejectUnauthorized: false
        })
        var _today = new Date(parseInt(date.toString().substring(0,4)), parseInt(date.toString().substring(5,7))-1, parseInt(date.toString().substring(8,10))+1)
        const param = JSON.stringify({ 
            Date: _today,
            New_Amount: parseInt(new_Amount),
            Old_GetWell_Amount: parseInt(old_GetWell_Amount),
            Died_Amount : parseInt(died_Amount),
            CovidID : parseInt(covidID)
         });
         var config = {
            method: 'post',
            url: 'https://localhost:44360/UpdateCovidData',
            httpsAgent: Agent,
            headers: { 
              'Content-Type': 'application/json'
            },
            data : param
          };
        axios(config)
        .then(function (response) {
            res.redirect('/')
        })
        .catch(function (error) {
            console.log(error);
        });
    }
    else if(edit === 'add')
    {
        var todayForAdd = new Date()
        var dayForAdd = todayForAdd.getDate().toString().padStart(2, "0");
        var monthForAdd = (todayForAdd.getMonth() + 1) .toString().padStart(2, "0");
        var yearForAdd = todayForAdd.getFullYear()
        var newDayForAdd = dayForAdd + "/" + monthForAdd + "/" + yearForAdd

        var today = new Date(parseInt(newDayForAdd.toString().substring(6,10)), parseInt(newDayForAdd.toString().substring(3,5))-1, parseInt(newDayForAdd.toString().substring(0,2)))
        // res.redirect('home')
        res.render('add_edit.ejs', 
        {
            date: today,
            new_Amount: new_Amount,
            old_GetWell_Amount: old_GetWell_Amount,
            died_Amount: died_Amount,
            covidID: covidID,
            flag: "add"
        })
    }
})
router.get('/',(req,res)=>{
    var today = new Date()
    var day = today.getDate().toString().padStart(2, "0");
    var month = (today.getMonth() + 1) .toString().padStart(2, "0");
    var year = today.getFullYear()
    var newDay = day + "/" + month + "/" + year
    var _today = new Date(parseInt(newDay.toString().substring(6,10)), parseInt(newDay.toString().substring(3,5))-1, parseInt(newDay.toString().substring(0,2)))
  
    axios.get('https://localhost:44360/GetWithDate', { httpsAgent: agent,params: { date: _today }  }) 
    .then(response => {
      // do something about response
        var resp  = response.data
        res.render('home.ejs', 
        {
            data: resp
        })
    })
    .catch(err => {
      console.error(err)
    })
})
router.get('/delete',(req,res)=>{
    let covidID = req.query.covidID;
    var today = new Date()
    var day = today.getDate().toString().padStart(2, "0");
    var month = (today.getMonth() + 1) .toString().padStart(2, "0");
    var year = today.getFullYear()
    var newDay = day + "/" + month + "/" + year
    var _today = new Date(parseInt(newDay.toString().substring(6,10)), parseInt(newDay.toString().substring(3,5))-1, parseInt(newDay.toString().substring(0,2)))   

    const Agent = new https.Agent({
        rejectUnauthorized: false
    })
    const param = JSON.stringify({ 
        CovidID: parseInt(covidID)
    });
    var config = {
        method: 'post',
        url: 'https://localhost:44360/DeleteCovidData',
        httpsAgent: Agent,
        headers: { 
            'Content-Type': 'application/json'
        },
        data : param
    };

    axios(config)
    .then(function (response) {
        axios.get('https://localhost:44360/GetWithDate', { httpsAgent: agent,params: { date: _today }  }) 
        .then(response => {    
            res.redirect('/')
            
        })
        .catch(err => {
        console.error(err)
        })
    })
    .catch(function (error) {
        console.log(error);
    });    
})
router.get('/add',(req,res)=>{
    let covidID = req.query.covidID;
    let date = req.query.date;
    let new_Amount = req.query.new_Amount;
    let old_GetWell_Amount = req.query.old_GetWell_Amount;
    let died_Amount = req.query.died_Amount;
     
    var today = new Date(parseInt(date.toString().substring(0,4)), parseInt(date.toString().substring(5,7))-1, parseInt(date.toString().substring(8,10))+1)
    const Agent = new https.Agent({
        rejectUnauthorized: false
    })
    const param = JSON.stringify({ 
        Date: today,
        New_Amount: parseInt(new_Amount),
        Old_GetWell_Amount: parseInt(old_GetWell_Amount),
        Died_Amount : parseInt(died_Amount)
     });

    var config = {
        method: 'post',
        url: 'https://localhost:44360/CreateCovidData',
        httpsAgent: Agent,
        headers: { 
            'Content-Type': 'application/json'
        },
        data : param
    };

    axios(config)
    .then(function (response) {
        axios.get('https://localhost:44360/GetWithDate', { httpsAgent: agent,params: { date: today }  }) 
        .then(response => {    
            res.redirect('/')
            
        })
        .catch(err => {
        console.error(err)
        })
    })
    .catch(function (error) {
        console.log(error);
    });    
})
module.exports = router
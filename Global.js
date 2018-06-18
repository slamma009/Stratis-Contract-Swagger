$(document).ready(function () {
    $.ajax({

        url : 'http://localhost:5000/api/contracts/GetContractInfo',
        type : 'POST',
        contentType: "application/json; charset=utf-8",
        data : JSON.stringify({
            request: 'n1tq1p5W6yBETVKeJwX1qzxe7Y4CNenUfd'
        }),
        dataType:'json',
        success : function(data) {              
            console.log(data);
        },
        error : function(request,error)
        {
            console.log("Request: "+JSON.stringify(request));
        }
    });
    
});
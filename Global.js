$(document).ready(function () {$.ajax({

    url : 'http://localhost:5000/api/contracts',
    type : 'GET',
    data : {
    },
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
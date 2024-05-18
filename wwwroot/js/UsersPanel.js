document.addEventListener("DOMContentLoaded", function() {
    
    var basvuruButton = document.querySelector("#basvuruButton");
    

   
    basvuruButton.addEventListener("click", function() {

        var basvuruPanel = document.querySelector("#basvuruPanel");

        basvuruPanel.style.display = "block";
        
    });
 
    document.querySelector("#basvuruPanel-close").addEventListener("click", function() {
        
        var basvuruPanel = document.querySelector("#basvuruPanel");

        basvuruPanel.style.display = "none";
          
    });


});


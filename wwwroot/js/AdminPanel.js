document.addEventListener("DOMContentLoaded", function() {
    var kullaniciButton = document.querySelector("#kullaniciButton");
    var basvuruButton = document.querySelector("#basvuruButton");
    var ekleButton = document.querySelector("#ekleButton");
    var registerForm = document.querySelector("#registerForm");

    kullaniciButton.addEventListener("click", function() {
        var kullaniciPanel = document.querySelector("#kullaniciPanel");
        var basvuruPanel = document.querySelector("#basvuruPanel");

        kullaniciPanel.style.display = "block";
        basvuruPanel.style.display = "none";
    });

    basvuruButton.addEventListener("click", function() {
        var kullaniciPanel = document.querySelector("#kullaniciPanel");
        var basvuruPanel = document.querySelector("#basvuruPanel");

        basvuruPanel.style.display = "block";
        kullaniciPanel.style.display = "none";
    });
    
    ekleButton.addEventListener("click", function() {

        var kayitFormPanel = document.querySelector(".kayitFormPanel");
        var kullaniciTable = document.querySelector(".kullaniciTable");
        var inputFields = kayitFormPanel.querySelectorAll("input");

        kullaniciTable.style.display = "none";
        kayitFormPanel.style.display = "block"; 
        inputFields.forEach(function(input) {
            input.value = "";
        }); 
       
    });
    registerForm.addEventListener("submit", function(event) {
        event.preventDefault(); 
        
        var kullaniciTable = document.querySelector(".kullaniciTable");
        var kayitFormPanel = document.querySelector(".kayitFormPanel");
        
        kullaniciTable.style.display = "block";
        kayitFormPanel.style.display = "none";
          
    });

    document.querySelector("#kayitFormPanel-close").addEventListener("click", function() {
        
        var kullaniciTable = document.querySelector(".kullaniciTable");
        var kayitFormPanel = document.querySelector(".kayitFormPanel");
        
        kullaniciTable.style.display = "block";
        kayitFormPanel.style.display = "none";
          
    });
    document.querySelector("#kullaniciPanel-close").addEventListener("click", function() {
        
        var kullaniciPanel = document.querySelector("#kullaniciPanel");
        var kayitFormPanel = document.querySelector(".kayitFormPanel");
        kayitFormPanel.style.display = "none";
        kullaniciPanel.style.display = "none";
          
    });
    document.querySelector("#basvuruPanel-close").addEventListener("click", function() {
        
        var kullaniciPanel = document.querySelector("#kullaniciPanel");
        var basvuruPanel = document.querySelector("#basvuruPanel");

        basvuruPanel.style.display = "none";
        kullaniciPanel.style.display = "none";
          
    });

});


document.addEventListener("DOMContentLoaded", function() {
    var kullaniciButton = document.querySelector("#kullaniciButton");
    var basvuruButton = document.querySelector("#basvuruButton");
    var ekleButton = document.querySelector("#ekleButton");
    var editButton = document.querySelector(".editButton");
    var kayitForm = document.querySelector("#kayitForm");
    var editForm = document.querySelector("#editForm");
    var kullaniciPanel = document.querySelector("#kullaniciPanel");
    var basvuruPanel = document.querySelector("#basvuruPanel");
    var kayitFormPanel = document.querySelector(".kayitFormPanel");
    var editFormPanel = document.querySelector(".editFormPanel");
    var kullaniciTable = document.querySelector(".kullaniciTable");
    var kullaniciPanelClose = document.querySelector("#kullaniciPanel-close");
    var kayitFormPanelClose = document.querySelector("#kayitFormPanel-close");
    var editFormPanelClose = document.querySelector("#editFormPanel-close");
    var basvuruPanelClose = document.querySelector("#basvuruPanel-close");
    var filtreSelect = document.getElementById('filtreSelect');

    kullaniciButton.addEventListener("click", function() {
        kullaniciPanel.style.display = "block";
        basvuruPanel.style.display = "none";
        kayitFormPanel.style.display = "none"; 
        editFormPanel.style.display = "none";

    });

    basvuruButton.addEventListener("click", function() {
        basvuruPanel.style.display = "block";
        kullaniciPanel.style.display = "none";
    });

    kullaniciPanelClose.addEventListener("click", function() {
        kullaniciPanel.style.display = "none";
    });

    ekleButton.addEventListener("click", function() {
        var inputFields = kayitFormPanel.querySelectorAll("input");
        kullaniciTable.style.display = "none";
        kayitFormPanel.style.display = "block"; 
        editFormPanel.style.display = "none"; 
        kullaniciPanelClose.style.display = "none";
    
        inputFields.forEach(function(input) {
            input.value = "";
        });
    });

    editButton.addEventListener("click", function() {
        kullaniciTable.style.display = "none";
        kayitFormPanel.style.display = "none"; 
        editFormPanel.style.display = "block"; 
        kullaniciPanelClose.style.display = "none";
    });


    kayitForm.addEventListener("submit", function(event) {
       
        kullaniciTable.style.display = "block";
        kayitFormPanel.style.display = "none";
        kullaniciPanelClose.style.display = "block";
        
    });
    editForm.addEventListener("submit", function(event) {
        
        kullaniciTable.style.display = "block";
        editFormPanel.style.display = "none";
        kullaniciPanelClose.style.display = "block";
        
    });

    kayitFormPanelClose.addEventListener("click", function() {
        kullaniciTable.style.display = "block";
        kayitFormPanel.style.display = "none";
        kullaniciPanelClose.style.display = "block";
    });

    editFormPanelClose.addEventListener("click", function() {
        kullaniciTable.style.display = "block";
        editFormPanel.style.display = "none";
        kullaniciPanelClose.style.display = "block";
    });

    basvuruPanelClose.addEventListener("click", function() {
        basvuruPanel.style.display = "none";
    });

    filtreSelect.addEventListener('change', function() {
        var filterValue = this.value.toLowerCase();
        var rows = document.querySelectorAll('#basvuruTable tbody tr');
        rows.forEach(function(row) {
            var department = row.cells[2].textContent.toLowerCase();
            row.style.display = (filterValue === "" || department.includes(filterValue)) ? "" : "none";
        });
    });
});

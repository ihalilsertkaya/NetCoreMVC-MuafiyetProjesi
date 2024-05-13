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

        kullaniciTable.style.display = "none";
        kayitFormPanel.style.display = "block";  
    });

    registerForm.addEventListener("submit", function(event) {
        event.preventDefault(); 
        
        var kullaniciTable = document.querySelector(".kullaniciTable");
        var kayitFormPanel = document.querySelector(".kayitFormPanel");
        
        kullaniciTable.style.display = "block";
        kayitFormPanel.style.display = "none";
          
    });
});

/* Database Başvuran Bilgileri */
var basvuru = [
    { id: 1, ad: "John", soyad: "Doe", eposta: "john@example.com", bolum: "IT", basvuruBelgesi: "/documents/Basvuru.pdf", transkriptBelgesi: "/documents/Transkript.pdf", dersDokumBelgesi: "/documents/DersDokum.pdf" },
    { id: 2, ad: "Jane", soyad: "Smith", eposta: "jane@example.com", bolum: "HR", basvuruBelgesi: "/documents/1.pdf", transkriptBelgesi: "/documents/2.pdf", dersDokumBelgesi: "/documents/3.pdf" }
   
];

function createBasvuruTable() {
    var tableBody = document.getElementById('basvuruTableBody');
    tableBody.innerHTML = ''; // Önceki içeriği temizle

    basvuru.forEach(function(user) {
        var row = document.createElement('tr');
        row.innerHTML = `
            <td>${user.ad}</td>
            <td>${user.soyad}</td>
            <td>${user.eposta}</td>
            <td>${user.bolum}</td>
            <td style="width:1rem;padding:0;border: none;">
            <button class="pdf-download-button" data-path="${user.basvuruBelgesi}" style="font-size:14px; display: flex;align-items: center ;justify-content:center;color:black;margin-left: 1rem;border-radius: 8px;border:1px solid black;width:8rem;height:42px;"><i class="far fa-file-pdf" style="font-size:2rem;color:red;"></i>Başvuru Belgesi</button>
            </td> 
            <td style="width: 1rem;padding:0;border: none;">
            <button class="pdf-download-button" data-path="${user.transkriptBelgesi}" style=" font-size:14px;display: flex;align-items: center ;justify-content:center;color:black;margin-left: 1rem;border-radius: 8px;border:1px solid black;width:8rem;height:42px;"><i class="far fa-file-pdf" style="font-size:2rem;color: red;"></i>Transkript Belgesi</button>
            </td> 
            <td style="width: 1rem;padding:0;border: none;">
            <button class="pdf-download-button" data-path="${user.dersDokumBelgesi}" style=" font-size:14px;display: flex;align-items: center ;justify-content:center;color:black;margin-left: 1rem;border-radius: 8px;border:1px solid black;width:8rem;height:42px;"><i class="far fa-file-pdf" style="font-size:2rem;color: red;"></i>Ders Döküm Belgesi</button>
            </td>
        `;
        tableBody.appendChild(row);
    });

    document.querySelectorAll('.pdf-download-button').forEach(button => {
        button.addEventListener('click', function() {
            var path = button.getAttribute('data-path');
            console.log('Butona tıklandı, dosya yolu:', path);
            window.open(path, '_blank');
        });
    });
}

/* Database Admin Bilgileri */
var admin = [
    { id: 1, ad: "John", soyad: "Doe", eposta: "john@example.com", bolum: "IT"},
    { id: 2, ad: "Jane", soyad: "Smith", eposta: "jane@example.com", bolum: "HR"}
];

function createKullaniciTable() {
    var tableBody = document.getElementById('kullaniciTableBody');
    tableBody.innerHTML = '';

    admin.forEach(function(user) {
        var row = document.createElement('tr');
        row.innerHTML = `
            <td>${user.ad}</td>
            <td>${user.soyad}</td>
            <td>${user.eposta}</td>
            <td>${user.bolum}</td>
        `;
        tableBody.appendChild(row);
    });
}

window.onload = function() {
    createBasvuruTable();
    createKullaniciTable();
};


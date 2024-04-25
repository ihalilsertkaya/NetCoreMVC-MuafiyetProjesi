/* ! Tarih */
window.onload = function () {
  var simdikiTarih = new Date();
  var gun = simdikiTarih.getDate();
  var ay = simdikiTarih.getMonth() + 1;
  var yil = simdikiTarih.getFullYear();
  if (ay < 10) {
    document.getElementById("date").innerText =
      gun + "/" + "0" + ay + "/" + yil;
  } else {
    document.getElementById("date").innerText = gun + "/" + ay + "/" + yil;
  }
};

// function showSignaturePreview(event) {
//   const file = event.target.files[0];
//   const reader = new FileReader();

//   reader.onload = function (event) {
//     const signaturePreview = document.getElementById("signaturePreview");
//     signaturePreview.src = event.target.result;
//     var imzaInput=document.getElementById("signatureInput");
//     imzaInput.style.display="none";
//     signaturePreview.style.display = "block";
//   };
//   reader.readAsDataURL(file);
// }

/* DersTablosu Hücre Ekleme */
// document.getElementById('ekleButton').addEventListener('click', function() {
//   event.preventDefault();
//   var derslerBody = document.getElementById('derslerBody');
//   var newRow = document.createElement('tr');

//   // Dersler için gerekli input tiplerini tanımla
//   var inputTypes = ['text', 'number', 'number', 'text', 'text', 'number', 'number', 'text'];

//   for (var i = 0; i < 8; i++) {
//     var cell = document.createElement('td');
//     var input = document.createElement('input');

//     // Input tipini belirle
//     input.setAttribute('type', inputTypes[i]);
//     input.setAttribute('class', 'inputDers');

//     cell.appendChild(input);
//     newRow.appendChild(cell);
//   }

//   derslerBody.appendChild(newRow);
// });

document
  .getElementById("ekleButton")
  .addEventListener("click", function (event) {
    event.preventDefault(); // Sayfanın yenilenmesini engelle

    // Yeni bir satır oluştur
    var derslerBody = document.getElementById("derslerBody");
    var newRow = document.createElement("tr");

    // Dersler için gerekli input tiplerini tanımla
    var inputTypes = [
      "text",
      "number",
      "number",
      "text",
      "text",
      "number",
      "number",
      "text",
    ];

    for (var i = 0; i < 9; i++) {
      // Silme butonu için 9. sütun eklendi
      var cell = document.createElement("td");

      if (i === 8) {
        // 9. sütun (sonuncusu) için silme butonunu ekle
        var deleteButton = document.createElement("button");
        deleteButton.textContent = "x";
        deleteButton.className = "silButton";
        deleteButton.style.background = "red"; // Kırmızı arka plan rengi
        deleteButton.style.textAlign = "center"; // Metin hizalaması
        deleteButton.addEventListener("click", function () {
          // Silme butonuna tıklandığında ilgili satırı kaldır
          derslerBody.removeChild(newRow);
        });
        cell.appendChild(deleteButton);
      } else {
        // Diğer sütunlar için input alanlarını ekle
        var input = document.createElement("input");
        input.setAttribute("type", inputTypes[i]);
        input.setAttribute("class", "inputDers");
        cell.appendChild(input);
      }

      newRow.appendChild(cell);
    }

    derslerBody.appendChild(newRow);
  });

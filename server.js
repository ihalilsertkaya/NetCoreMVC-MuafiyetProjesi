const express = require('express');
const fs = require('fs');
const path = require('path');

const app = express();
app.use(express.json({ limit: '50mb' }));

// Diğer route'larınız...

// Yeni /save-pdf endpointini ekleyin
app.post('/save-pdf', (req, res) => {
    const { data, filename } = req.body;

    // PDF verisini base64'ten dönüştür
    const pdfBuffer = Buffer.from(data, 'base64');

    // PDF dosyasını kaydet
    const filePath = path.join(__dirname, 'pdfs', filename);
    fs.writeFile(filePath, pdfBuffer, (err) => {
        if (err) {
            console.error('PDF kaydedilirken hata oluştu:', err);
            return res.status(500).json({ message: 'PDF kaydedilemedi' });
        }

        console.log('PDF başarıyla kaydedildi:', filePath);
        res.json({ message: 'PDF kaydedildi', filePath });
    });
});

// Sunucuyu başlat
const port = process.env.PORT || 80;
app.listen(port, () => {
    console.log(`Sunucu ${port} numaralı portta çalışıyor.`);
});

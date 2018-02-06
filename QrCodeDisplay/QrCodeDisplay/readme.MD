## Display QRCode ##

Using QRCoder generate QRCode image.
[QRCode Wiki](https://github.com/codebude/QRCoder/wiki)

```
QRCodeGenerator qrGenerator = new QRCodeGenerator();
QRCodeData qrCodeData = qrGenerator.CreateQrCode("The text which should be encoded.", QRCodeGenerator.ECCLevel.Q);
QRCode qrCode = new QRCode(qrCodeData);
Bitmap qrCodeImage = qrCode.GetGraphic(20);
```

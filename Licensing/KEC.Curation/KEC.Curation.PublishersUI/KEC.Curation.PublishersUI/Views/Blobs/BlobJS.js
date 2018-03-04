
function uploadBlobFromText() {
     // your account and SAS information

    // get the Azure Storage JavaScript Client Library for Browsers from https://aka.ms/downloadazurestoragejs
     var sasKey ="https://keccuration.blob.core.windows.net/?sv=2017-07-29&ss=bfqt&srt=sco&sp=rwdlacup&se=2019-01-05T06:25:18Z&st=2018-03-03T22:25:18Z&spr=https,http&sig=C4bE23Bha53TfRsTmnGHz1Y%2FeZ8v%2BcnhKwj5YSZDosQ%3D";
     var blobUri = "http://keccuration.blob.core.windows.net";
     var blobService = AzureStorage.createBlobServiceWithSas(blobUri, sasKey).withFilter(new AzureStorage.ExponentialRetryPolicyFilter());
     var text = document.getElementById('text');
     var btn = document.getElementById("upload-button");
     blobService.createBlockBlobFromText('publications', 'myblob', text.value,  function(error, result, response){
         if (error) {
             alert('Upload filed, open browser console for more detailed info.');
             console.log(error);
         } else {
             alert('Upload successfully!');
         }
     });
}

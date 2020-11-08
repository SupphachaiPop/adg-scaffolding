using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.Numerics;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Definitions
{
    public enum Accesskeys
    {
        techheadecommerce = 0
    }

    public class AzureBlobEntity
    {
        public Stream Stream { get; set; }
        public String FileName { get; set; }
        public String ContainerName { get; set; }
        public Accesskeys ConnectionString { get; set; }
    }

    public class AzureBlobHelper
    {
        public enum Accesskeys
        {
            swKey = 0
        }

        public String UploadFile(AzureBlobEntity azureBlobEntity)
        {

            Byte[] bytes = null;
            MemoryStream memoryStream = new MemoryStream();
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(getConnectionString((Int32)azureBlobEntity.ConnectionString));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(azureBlobEntity.ContainerName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(azureBlobEntity.FileName);
            azureBlobEntity.Stream.Position = 0;

            azureBlobEntity.Stream.CopyTo(memoryStream);
            bytes = memoryStream.ToArray();

            blockBlob.UploadFromByteArrayAsync(bytes, 0, bytes.Length).Wait();
            return blockBlob.Uri.AbsoluteUri;
        }

        public MemoryStream DownloadFile(AzureBlobEntity azureBlobEntity)
        {
            MemoryStream memStream = new MemoryStream();
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(getConnectionString((Int32)azureBlobEntity.ConnectionString));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(azureBlobEntity.ContainerName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(azureBlobEntity.FileName);
            blockBlob.DownloadToStreamAsync(memStream).Wait();
            return memStream;
        }

        public void DeleteFile(AzureBlobEntity azureBlobEntity)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(getConnectionString((Int32)azureBlobEntity.ConnectionString));
            CloudBlobClient _blobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer _cloudBlobContainer = _blobClient.GetContainerReference(azureBlobEntity.ContainerName);
            CloudBlockBlob _blockBlob = _cloudBlobContainer.GetBlockBlobReference(azureBlobEntity.FileName);
            _blockBlob.DeleteAsync();
        }

        private String getConnectionString(Int32 accesskeys)
        {
            String value = String.Empty;
            switch (accesskeys)
            {
                case 0: { value = "DefaultEndpointsProtocol=https;AccountName=aladinxcite;AccountKey=FrTw2v3OeJOf2AyrDHVvZuZTwauJmyX3p5mflO96y0NKFcCEvyj9aE32Gld5q+BO4yVaT7IhPHfm0mBCBEKGnw==;EndpointSuffix=core.windows.net"; } break;
            }
            return value;
        }
    }
}

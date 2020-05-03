appsettings.local.json
======================

To run the application in this folder, you need to add the proper
configuration to the application. You need to add the `appsettings.local.json`
file to the same folder with this documenation.

The `appsettings.local.json` contains sensitive information and has been excluded
from source control.

The contents of that file are:

``` JSON
{
  "AzureAd": {
    "Instance": "https://login.microsoftonline.com/common",
    "ClientId": "",
    "CallbackPath": "/signin-oidc"
  },
  "Hpr": {
    "Storage": {
      "ConnectionString": "",
      "AttachmentsContainerName": ""
    },
    "LogicApps": {
      "ReceiveImageUrl": ""
    }
  }
}
```

The values above are described in the list below.

- `AzureAd:ClientId` - The client ID of the Azure AD application that you want to use as identity for the ASP.NET Core application.
- `Hpr:Storage:ConnectionString` - The connection string to the Azure Storage account containing the attachments to display in the application.
- `Hpr:Storage:AttachmentsContainerName` - The name of the container in the storage account specified by the previous setting that holds the attachments.
- `Hpr:LogicApps:ReceiveImageUrl` - The full URL to the Logic Apps HTTP triggered endpoint for uploading and processing images with. The HTTP trigger for that Logic App must support a request body that matches the `HprAbstractions.Messaging.ReceiveImageRequest` request message class.
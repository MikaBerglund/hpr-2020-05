local.settings.json
===================

In order to be able to run the application in this folder, you need to make sure
you have a `local.settings.json` file in the same folder with this documentation.

The contents of that file should be:

``` JSON
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",

    "hpr:sendgrid:apikey": "",
    "hpr:sendgrid:fromname": "",
    "hpr:sendgrid:fromaddress": "",

    "hpr:computervision:key": "",
    "hpr:computervision:endpoint": "",

    "hpr:map:key": ""
  }
}
```

The settings above are described in more detail in the list below.

- `hpr:sendgrid:apikey` - The API key to your SendGrid account you want to send e-mails with.
- `hpr:sendgrid:fromname` - The default sender's display name when sending e-mails.
- `hpr:sendgrid:fromaddress` - The default sender-s e-mail address when sending e-mails.
- `hpr:computervision:key` - The key to your computer vision service you want to use with the application.
- `hpr:computervision:endpoint` - The endpoint to your computer vision service.
- `hpr:map:key` - The key to your Azure Maps account you want to use in the application.


# OneDrive File Backuper

Tool for backing up files from **OneDrive**.

## Description

The application is developed in .NET 9. Using the **Entra ID**, the user generates a device token, which is then cached. For proper functionality, it is necessary to have the correct permissions set in the **Entra ID**. This token is used to authenticate against the **Microsoft Graph API**, allowing access to **OneDrive**. Files are downloaded from **OneDrive**, saved to a **local path**, and then deleted if set to do so.

## Getting Started

### Dependencies
* .NET 9 SDK
* Microsoft Entra ID (Azure AD) application registration
* OneDrive account

### Installing
#### Azure
Create app registration.
![1.Azure_AppRegistration](/Doc/Images/Azure/1.Azure_AppRegistration.png)

Setting up the app registration.
![2.Azure_CreateApp](/Doc/Images/Azure/2.Azure_CreateApp.png)

The `client id` must be inserted into the program settings.
![3.Azure_ClientId](/Doc/Images/Azure/3.Azure_ClientId.png)

For app registration you need to enable **Allow public client flows**, which enables **Device code flow**.
![4.Azure_Authentication_AllowDeviceCode](/Doc/Images/Azure/4.Azure_Authentication_AllowDeviceCode.png)


You need to add the following permissions to the app registration to be able to work with files on **One Drive**.
![5.Azure_AppAddPermission](/Doc/Images/Azure/5.Azure_AppAddPermission.png)
![6.Azure_RequestApiPermission_General](/Doc/Images/Azure/6.Azure_RequestApiPermission_General.png)
![7.Azure_RequestApiPermission_Selected.png](/Doc/Images/Azure/7.Azure_RequestApiPermission_Selected.png)

> If there is no need to delete files, just enable `Files.Read` permissions.

#### One Drive
No need to set up anything in OneDrive. Only the `folder id` and `driver cid` must be obtained from the **url address**. It is necessary to go to the folder from which the files will be **backed up**.
![1.OneDrive_Url](/Doc/Images/OneDrive/1.OneDrive_Url.png)

#### Program
Copy `appsettings.json` to your project root and fill in required values or set up `user secrets`.

```
{
  "EntraId": {
    "ClientId": ""
  },
  "OneDrive": {
    "Drive": "",
    "FileFolder": ""
  },
  "BackupSettings": {
    "DeleteAfterBackup": true
  },
  "LocalStorage": {
    "Path": ""
  }
}
```

* EntraId:ClientId - Retrieved from **Azure portal**.
* OneDrive:Drive - Retrieved from a url in **OneDrive**.
* OneDrive:FileFolder - Retrieved from a url in **OneDrive**.
* LocalStorage:Path - Path to local storage.
* BackupSettings:DeleteAfterBackup - Whether the files should be deleted after the backup.

#### Executing program

Start the program.
```
dotnet run --project ./Src
```

## Project Structure

* Auth/ – Authentication providers for **Entra ID** and **JWT tokens**.
* Configurations/ – Dependency injection and options configuration.
* GraphClients/ – **Microsoft Graph API** client abstractions.
* Handlers/ – File synchronization logic.
* OneDrive/ – **OneDrive** service integration.
* Options/ – Strongly-typed options and validators.
* Storages/ – File storage abstractions.
* App.cs – Application entry point logic.
* Program.cs – Main program bootstrap.

## Version History

* 0.2
  * File downloads have been accelerated thanks to asynchronous processing. 
  * Downloading large numbers of files has been fixed by limiting parallelism.
  * The timeout for downloading large files has been extended to 10 minutes.  
* 0.1
  * Initial release

## License

This project is licensed under the **MIT** License - see the LICENSE.md file for details.


# MSEntraID-POC

## General Overview
### WebService-A

**Microsoft EntraID Configurations** 
|Info|Value|
|:--------------|:-----------------|
|Application Name|MyWeatherAPIService|
|Certificates & Secrets|None|
|Token Configuration|None|
|API Permissions|Microsoft.Graph.UserRead|
|Expose API|None|
|App Roles|Reader|

#### Script to create the Application
**myWeatherAPIService.permissions.json**:
```json
[
    {
        "resourceAppId": "00000003-0000-0000-c000-000000000000",
        "resourceAccess": [
            {
                "id": "c79f8feb-a9db-4090-85f9-90d820caa0eb",
                "type": "Scope"
            }
        ]
    }
]
```

**myWeatherAPIService.roles.json**:
```json
[
    {
        "allowedMemberTypes": [
            "User"
        ],
        "description": "Approvers can mark documents as approved",
        "displayName": "Approver",
        "isEnabled": "true",
        "value": "approver"
    }
]
```

**command to run**:
```pwsh
az ad app create --display-name MyWeatherAPIService --sign-in-audience AzureADMyOrg `
                 --required-resource-accesses myWeatherAPIService.permissions.json `
                 --app-roles myWeatherAPIService.roles.json
```

### Client-01 App
**Microsoft EntraID Configurations**
|Info|Value|
|:--------------|:-----------------|
|Application Name|MyWeatherConsumerApp01|
|Certificates & Secrets|Client Secret (1)|
|Token Configuration|None|
|API Permissions|Microsoft.Graph.UserRead|
||MyWeatherAPIService.Reader|
|Expose API|None|
|App Roles|Reader|

#### Script to create the Application
**myWeatherConsumerApp01.permissions.json**:
```json
[
  {
    "resourceAppId": "00000003-0000-0000-c000-000000000000",
    "resourceAccess": [
      {
        "id": "e1fe6dd8-ba31-4d61-89e7-88639da4683d",
        "type": "Scope"
      }
    ]
  },
  {
    "resourceAppId": "<MyWeatherAPIService_AZURE_ENTRA_ID_APPID>",
    "resourceAccess": [
      {
        "id": "28b28aa9-d49b-4f04-8b1a-b1cbb70ab012",
        "type": "Role"
      }
    ]
  }
]
```

**command to run**:
```pwsh
az ad app create --display-name MyWeatherConsumerApp01 --sign-in-audience AzureADMyOrg `
                 --required-resource-accesses MyWeatherConsumerApp01.permissions.json
```

## Next Step:
- Create a Client Secret
- Configure your application to Authentication with the certificate you just assign to *MyWeatherConsumerApp01* application
- Get JwtBearer Token from the *MyWeatherAPIService* resource
- Reach MyWeatherAPIService Endpoint with JwtBearer Token


### Client-02 App
**Microsoft EntraID Configurations**
|Info|Value|
|:--------------|:-----------------|
|Application Name|MyWeatherConsumerApp02|
|Certificates & Secrets|Certificate (1)|
|Token Configuration|None|
|API Permissions|Microsoft.Graph.UserRead|
||MyWeatherAPIService.Reader|
|Expose API|None|
|App Roles|Reader|

#### Script to create the Application
**myWeatherConsumerApp02.permissions.json**:
```json
[
  {
    "resourceAppId": "00000003-0000-0000-c000-000000000000",
    "resourceAccess": [
      {
        "id": "e1fe6dd8-ba31-4d61-89e7-88639da4683d",
        "type": "Scope"
      }
    ]
  },
  {
    "resourceAppId": "<MyWeatherAPIService_AZURE_ENTRA_ID_APPID>",
    "resourceAccess": [
      {
        "id": "28b28aa9-d49b-4f04-8b1a-b1cbb70ab012",
        "type": "Role"
      }
    ]
  }
]
```

**command to run**:
```pwsh
az ad app create --display-name MyWeatherConsumerApp02 --sign-in-audience AzureADMyOrg `
                 --required-resource-accesses MyWeatherConsumerApp02.permissions.json
```

## Next Step:
- Create a Client Secret
- Configure your application to Authentication with the certificate you just assign to *MyWeatherConsumerApp02* application
- Get JwtBearer Token from the *MyWeatherAPIService* resource
- Reach MyWeatherAPIService Endpoint with JwtBearer Token
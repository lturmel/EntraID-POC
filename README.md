# MSEntraID-POC

## General Overview
### WebService-A

**Microsoft EntraID Configurations** 
    |Info|Value|
    |:--------------:|:-----------------:|
    |Application Name|MyWeatherAPIService|
    |Certificates & Secrets|None|
    |Token Configuration|None|
    |API Permissions|Microsoft.Graph.UserRead|
    |Expose API|None|
    |App Roles|Reader|


### Client-01 App
**Microsoft EntraID Configurations**
    |Info|Value|
    |:--------------:|:-----------------:|
    |Application Name|MyWeatherConsumerApp01|
    |Certificates & Secrets|Client Secret (1)|
    |Token Configuration|None|
    |API Permissions|Microsoft.Graph.UserRead|
    ||MyWeatherAPIService.Reader|
    |Expose API|None|
    |App Roles|Reader|

### Client-02 App
**Microsoft EntraID Configurations**
    |Info|Value|
    |:--------------:|:-----------------:|
    |Application Name|MyWeatherConsumerApp02|
    |Certificates & Secrets|None|
    |Token Configuration|None|
    |API Permissions|Microsoft.Graph.UserRead|
    ||MyWeatherAPIService.Reader|
    |Expose API|None|
    |App Roles|Reader|
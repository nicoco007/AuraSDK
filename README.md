# AuraSDK
[![Build Status](https://img.shields.io/jenkins/s/https/ci.gnyra.com/job/AuraSDK/job/master.svg?style=flat-square)](https://ci.gnyra.com/blue/organizations/jenkins/AuraSDK)
![Jenkins tests](https://img.shields.io/jenkins/t/https/ci.gnyra.com/job/AuraSDK/job/master.svg?style=flat-square)
[![GitHub license](https://img.shields.io/github/license/nicoco007/AuraSDK.svg?style=flat-square)](https://github.com/nicoco007/AuraSDK/blob/master/LICENSE)
[![NuGet](https://img.shields.io/nuget/v/AuraSDK.svg?style=flat-square)](https://www.nuget.org/packages/AuraSDK)


C# wrapper for the Asus Aura SDK. Currently only supports motherboards and GPUs.

# Usage
You can [use NuGet](https://www.nuget.org/packages/AuraSDK/) or [download from the releases page](https://github.com/nicoco007/AuraSDK/releases) to add this DLL to your project. You will also need to download the Aura SDK from [the official website](https://www.asus.com/campaign/aura/us/SDK.html) and put it somewhere (preferably in your project folder).

Create a new instance of the SDK class using

```cs
var sdk = new AuraSDK();
```

or, if you want to put the `AURA_SDK.dll` file somewhere else, use

```cs
var sdk = new AuraSDK("path/to/your/AURA_SDK.dll");
```

Devices are automatically loaded and should be ready to use. Most functions are at least somewhat documented or relatively self-explanatory. I might add some more documentation here at some point.

# Todo
* Don't use SDK as a class name (too generic)
* Add support for mice & keyboards

Resource Compiler (RC) VERSIONINFO auto-patcher for Unity made with [rcedit](https://github.com/electron/rcedit) (Windows).

## Installation

Open `Project Settings/Package Manager` and add new scoped registry:

* Name: `KNOT`
* URL: `https://registry.npmjs.com`
* Scope(s): `com.knot`

![image](https://github.com/user-attachments/assets/ca20c30a-3ac3-494b-9e44-690630faf9db)

Open `Window/Package Manager` and install package from `Packages: My Registries`

![image](https://github.com/user-attachments/assets/8e552498-7faa-4996-87a0-2a784679ee87)

## Usage

1. Open `Project Settings/KNOT/RC Patcher`
2. Turn on `Patch on Build Post Process` and add new `Build Post Processor`
3. Modify `Target Files` filter if neccesary. Only Target Files will be patched on build postprocess
4. Create and assign `Patcher Profile` asset by pressing `New` button or via `Create/KNOT/RC Patcher/RC Patcher Profile`
5. Make Windows build and check `Target Files` properties details. Done

See all possible properties:

https://learn.microsoft.com/en-us/windows/win32/menurc/versioninfo-resource?redirectedfrom=MSDN

```C#
//KnotRcPatcherProfile built-in property value placeholders
sb.Replace("<ProductVersion>", Application.version);
sb.Replace("<UnityVersion>", Application.unityVersion);
sb.Replace("<CompanyName>", Application.companyName);
sb.Replace("<BuildGuid>", Application.buildGUID);
sb.Replace("<ProductName>", Application.productName);
sb.Replace("<CurrentYear>", DateTime.UtcNow.Year.ToString());
```

[1mdiff --git a/.vs/Mango/DesignTimeBuild/.dtbcache.v2 b/.vs/Mango/DesignTimeBuild/.dtbcache.v2[m
[1mindex 97f1192..037f000 100644[m
Binary files a/.vs/Mango/DesignTimeBuild/.dtbcache.v2 and b/.vs/Mango/DesignTimeBuild/.dtbcache.v2 differ
[1mdiff --git a/.vs/Mango/FileContentIndex/63eda64e-a844-46d0-845e-618e92f3cf51.vsidx b/.vs/Mango/FileContentIndex/63eda64e-a844-46d0-845e-618e92f3cf51.vsidx[m
[1mdeleted file mode 100644[m
[1mindex 35d161a..0000000[m
Binary files a/.vs/Mango/FileContentIndex/63eda64e-a844-46d0-845e-618e92f3cf51.vsidx and /dev/null differ
[1mdiff --git a/.vs/Mango/FileContentIndex/7bc28087-cfcf-45fa-9428-67f44b23c879.vsidx b/.vs/Mango/FileContentIndex/7bc28087-cfcf-45fa-9428-67f44b23c879.vsidx[m
[1mdeleted file mode 100644[m
[1mindex 03e2a91..0000000[m
Binary files a/.vs/Mango/FileContentIndex/7bc28087-cfcf-45fa-9428-67f44b23c879.vsidx and /dev/null differ
[1mdiff --git a/.vs/Mango/FileContentIndex/d8c35b53-f0fa-471c-852f-69461e5a78d8.vsidx b/.vs/Mango/FileContentIndex/d8c35b53-f0fa-471c-852f-69461e5a78d8.vsidx[m
[1mdeleted file mode 100644[m
[1mindex 5743e30..0000000[m
Binary files a/.vs/Mango/FileContentIndex/d8c35b53-f0fa-471c-852f-69461e5a78d8.vsidx and /dev/null differ
[1mdiff --git a/.vs/Mango/v17/.suo b/.vs/Mango/v17/.suo[m
[1mindex 6de3e07..6136207 100644[m
Binary files a/.vs/Mango/v17/.suo and b/.vs/Mango/v17/.suo differ
[1mdiff --git a/AuthApi/obj/AuthApi.csproj.nuget.dgspec.json b/AuthApi/obj/AuthApi.csproj.nuget.dgspec.json[m
[1mindex 89ba5e9..23541a3 100644[m
[1m--- a/AuthApi/obj/AuthApi.csproj.nuget.dgspec.json[m
[1m+++ b/AuthApi/obj/AuthApi.csproj.nuget.dgspec.json[m
[36m@@ -138,6 +138,10 @@[m
               "target": "Package",[m
               "version": "[7.16.2, )"[m
             },[m
[32m+[m[32m            "Microsoft.Extensions.Configuration.Abstractions": {[m
[32m+[m[32m              "target": "Package",[m
[32m+[m[32m              "version": "[7.0.0, )"[m
[32m+[m[32m            },[m
             "Newtonsoft.Json": {[m
               "target": "Package",[m
               "version": "[13.0.3, )"[m
[1mdiff --git a/AuthApi/obj/project.assets.json b/AuthApi/obj/project.assets.json[m
[1mindex b212f73..c5e0930 100644[m
[1m--- a/AuthApi/obj/project.assets.json[m
[1m+++ b/AuthApi/obj/project.assets.json[m
[36m@@ -1328,6 +1328,7 @@[m
         "framework": ".NETCoreApp,Version=v6.0",[m
         "dependencies": {[m
           "Azure.Messaging.ServiceBus": "7.16.2",[m
[32m+[m[32m          "Microsoft.Extensions.Configuration.Abstractions": "7.0.0",[m
           "Newtonsoft.Json": "13.0.3"[m
         },[m
         "compile": {[m
[1mdiff --git a/AuthApi/obj/project.nuget.cache b/AuthApi/obj/project.nuget.cache[m
[1mindex e4827f3..b6ffdfb 100644[m
[1m--- a/AuthApi/obj/project.nuget.cache[m
[1m+++ b/AuthApi/obj/project.nuget.cache[m
[36m@@ -1,6 +1,6 @@[m
 {[m
   "version": 2,[m
[31m-  "dgSpecHash": "kgf9kSi7GpqLtoLXOmO789y9t7/+dkVDnpkaodlXTgo5Pt2vFvrNQi2O6KlZAFAf9oUDjymSPl5QRgQpLOPATA==",[m
[32m+[m[32m  "dgSpecHash": "OJsIXYw7HkOwIlu0nuxELAtTW638jGCb/BqsMjdgzsCCEIygIeVelsN4iJLNnqaClfm5Pn7w1t4yxeHPdxMeWg==",[m
   "success": true,[m
   "projectFilePath": "C:\\WebAPI\\Villa\\Mango\\AuthApi\\AuthApi.csproj",[m
   "expectedPackageFiles": [[m
[1mdiff --git a/CouponWeb/Controllers/AuthController.cs b/CouponWeb/Controllers/AuthController.cs[m
[1mindex 08180bb..762e125 100644[m
[1m--- a/CouponWeb/Controllers/AuthController.cs[m
[1m+++ b/CouponWeb/Controllers/AuthController.cs[m
[36m@@ -40,7 +40,7 @@[m [mnamespace MangoWeb.Controllers[m
                new SelectListItem() { Text = SD.RoleCustomer, Value = SD.RoleCustomer }[m
             };[m
             ViewBag.RoleList = RoleListItem;[m
[31m-            RegistrationRequestDto registrationRequestDto = new RegistrationRequestDto();[m
[32m+[m[32m            RegistrationRequestDto registrationRequestDto = new RegistrationRequestDto() ;[m
             return View(registrationRequestDto);[m
         }[m
 [m
[36m@@ -53,6 +53,7 @@[m [mnames
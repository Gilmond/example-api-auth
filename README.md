# example-api-auth
This repository is an out-of-hours repository for spitballing api authorisation techniques

# notes

This section just details some dev notes as I work through this.

** See [this link](https://github.com/smudge202/IdentityServer4.Samples) for reference. It's the one I'm working through but tweaking to our preference!

* I've decided to go with IdentityServer4 for OAuth implementation
* To make a certificate, in command prompt:
    * `set path=%path%;"C:\Program Files (x86)\Windows Kits\10\bin\x64\"` - _this will put makecert.exe on path; change the path if your makecert is elsewhere_
    * `makecert -sv yourprivatekeyfile.pvk -n "cert name" yourcertfile.cer -b mm/dd/yyyy -e mm/dd/yyyy -r` where:
        * -sv yourprivatekeyfile.pvk is the name of the file containing the private key.
        * -n "cert name" is the name that will appear on the certificate (and in the certificate store).
        * yourcertfile.cer is the name of the certificate file.
        * -b mm/dd/yyyy is the date when the certificate becomes valid.
        * -e mm/dd/yyyy is the date when the certificate expires.
        * -r indicates that this will be a self-signed certificate.
        * _So I ended up using `makecert -sv gilmond-auth.pvk -n "cn=Gilmond Auth" gilmond-auth.cer -b 02/16/2016 -e 04/01/2016 -r`_
        * _NB: For the time being I'm using the password "g|lM0nd" for both private key and cert (it's only a test/dev password!)_
    * `PVK2PFX –pvk yourprivatekeyfile.pvk –spc yourcertfile.cer –pfx yourpfxfile.pfx –po yourpfxpassword` where:
        * -pvk yourprivatekeyfile.pvk is the private key file that you created in step 4.
        * -spc yourcertfile.cer is the certificate file you created in step 4.
        * -pfx yourpfxfile.pfx is the name of the .pfx file that will be created.
        * -po yourpfxpassword is the password that you want to assign to the .pfx file. You will be prompted for this password when you add the .pfx file to a project in Visual Studio for the first time.
        * _So I used the following: `pvk2pfx -pvk gilmond-auth.pvk -spc gilmond-auth.cer -pfx gilmond-auth.pfx -po "g|lM0nd"`_
        * _NB: Using a pipe in a password when using the commandline is not smart :) _
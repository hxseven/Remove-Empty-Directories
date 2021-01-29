# RED Installer

This directory contains all resources related to the RED installer.

I wrote this document mostly for myself because I forgot how I did it last time.

Maybe it can be helpful to others too.


## How-to generate a new installer

### Step 1: Build binaries

* Install Microsoft Visual Studio (Community), I used version 2019
* Open the project file "red2_project.sln"
* Change to build configuration to "Release-x86" and build the solution
* Then change the build configuration to "Release-x64" and build the solution again

### Step 2: Compile installer

* Install Inno Setup (Version 6.1+)
* Make sure you build the binaries first (see step 1)
* Open red_installer-v2.iss with Inno Setup
* Select "Compile" in the "Build" menu
  * Inno Setup should automatically find and use the newly generated binaries, otherwise, paths in the iss file might need to be fixed
* Inno Setup should now compile a new installer and save it into the bin/ folder below this directory
  * Check the compiler output of Inno Setup to see if there were any problems
* Done :)

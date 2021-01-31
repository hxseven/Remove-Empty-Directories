Remove Empty Directories
========================

RED finds, displays, and deletes empty directories recursively below a given start folder. Furthermore, 
it allows you to create custom rules for keeping and deleting folders (e.g. treat directories with empty files as empty).


## Features

- Simple user interface
- Shows empty directories before deleting them
- Supports multiple delete modes (including Delete to recycle bin)
- Allows white and blacklisting of directories by using filter lists
- Can detect directories with empty files as empty


## System requirements

- Windows 7 or 10
  - Version 2.2 should also still work with older Windows versions like XP or Vista
- Microsoft .NET Framework 4.6.2 or later
  - The installer checks for the right version and installs it if missing

## How to contribute to the project

You are very welcome to contribute code, translations, or anything else :)

Here are some tasks you can help with:
- Look at open issues/bugs and try to fix them
- Fix typos and general wording
- Optimize user interface
- Create a strategy/foundation for translations, are there any existing frameworks/concepts?
- Add unit tests

## History

The first version of RED was created by [Jonas John](http://www.jonasjohn.de/) around 2005. 

Since then [a small team of contributors](https://github.com/hxseven/Remove-Empty-Directories/graphs/contributors) helped to fix bugs and add new features.

## Changelog

2.3
- Disabled settings during active search or deletion process
- Refactored the interface to improve the design and usability
  - Divided packed settings tab into three separate tabs
  - Renamed some options and captions to make more sense 
  - Added more descriptions and examples to explain settings
  - Increased default window size and added more whitespace to make it look less crowded
- Optimized config defaults
  - Set pause between deletions to zero because the default delete to recycle bin method is slow enough to not overwhelm the GUI
  - Deleted some unnecessary entries and updated some values
  - Removed *.tmp as default pattern to make the default settings safer because those files could still contain valuable data in some cases.
- Long paths support and other improvements (contributed by gioccher, see #5)
  - Fix crash due to case sensitivity of paths 
  - Speed up crawl and deletion by disabling UI updates (dubbed fast mode)
  - Long path support by switching to AlphaFS 
  - And more minor improvements (see closed pull request #5 for details)
- Ignore folders newer than N hours #3 (contributed by jsytniak, see #3)

2.2
- Improved error handling
- Added logging of errors and deleted directories
- Added multiple delete modes (e.g. delete to recycle bin)
- Implemented a function to delete a single empty directory
- Implemented optional function to detect paths in clipboard
- Infinite loop detection
- Added a few new configuration settings
- Removed counting method to increase speed
- Replaced old custom settings module with the default settings framework of .NET to be more standard-compliant (This should fix problems some users had when starting RED)

2.1
- Implemented a "Protect" and "Unprotect" function to let the user choose folders to keep
- Implemented an update button for a fast update check

2.0
- Created the installer (using NSIS)
- Updated this readme file

1.9
- Added better-looking icons to the GUI
- Corrected and updates some texts

1.8
- Finished the main parts of the application
- Added XML configuration file

1.7
- Removed some main parts of the new application and started
using the "BackgroundWorker" for threading support.

1.6
- Finished the first prototype of the C# version

1.5
- Started the development of an entirely new version of RED by using Microsoft Visual C# (.NET 2.0)

1.4
- Updated the readme and changed the license from GPL to LGPL
- fixed some small issues

1.2
- Fixed the gauge in the process window
- implemented a second safety check to prevent deleting filled folders

1.1
- renamed the program to RED (Remove empty directories)
- made a new icon

1.0
- changes some structure things, renamed functions, renamed variables
- corrected code, fixed some issues...
- optional logfile implemented
- other minor changes
- updated version history -> complete rewrite ;)

0.9
- Added a readme, the licenses
- Translated the readme into English

0.8
- Cleaned the directories and sorted the files
- Renamed some functions and variables, to make it look better

0.6 + 0.7
- I learned about WinBinder (A native Windows binding for PHP) and converted
the program to PHP with a Windows GUI using WinBinder

0.5
- Used NSIS Install System (http://nsis.sourceforge.net/) to create a
GUI for the perl script

0.2 - 0.4
- Minor changes
- Added filters to exclude some folders like the recycler

0.1
- I made a simple perl script to delete empty folders, I called it "DEF" (Delete Empty Folders)


## Credits

Third-party components
- File system calls are powered by the [AlphaFS library](https://github.com/alphaleonis/AlphaFS)
- The Installer is made by using [Inno Setup](https://jrsoftware.org/isinfo.php) & [Inno Setup Dependency Installer](https://github.com/DomGries/InnoDependencyInstaller)

Icon sources
- Nuvola icons (GNU LGPL 2.1. license)
- NuoveXT icons (GPL license)
- [famfamfam silk icons](http://www.famfamfam.com/lab/icons/ "famfamfam silk icons") (Creative Commons Attribution license) 
- [Coffee icon](https://www.freeimages.com/de/photo/coffee-and-desserts-1571223 "Coffee icon") by Ivan Freaner
- Ignore list icon taken from "Primo Icon Set" made by [Webdesigner Depot](http://www.webdesignerdepot.com/)
  - License: Free of charge for personal or commercial purposes


## License

RED is free software; you can redistribute it and/or modify it under the terms of the
[GNU Lesser General Public License](http://www.gnu.org/licenses/lgpl.html) as published by the Free Software Foundation; either version 3 of the License, or (at your option) any later version.
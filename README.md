# Prison Roleplay

## Overview
Prison Roleplay is a Roleplay SA:MP gamemode. The aim of the game is to survive and prosper in the prison harh prison conditions.

Gameplay is centered around roleplaying with other players in order to provide a more realistic feel to the life in prison. As you have to work, earn money and become the nicest or the evil prisoner, choice is yours.

## Getting started

### Requirements

To get started with Prison Roleplay, you need the following tools installed on your computer:

- [Visual Studio](https://visualstudio.microsoft.com/vs/community/) To edit and compile the project
- [.NET 5 Runtime x86 Binaries](https://dotnet.microsoft.com/download/dotnet/5.0) In order for project to run
- [Git](https://git-scm.com) To clone the repository
- [SampSharp](https://github.com/ikkentim/SampSharp) To be ble to access SA:MP API from C#
- [SA:MP Server](https://www.sa-mp.com/download.php) To be able to run the server
- [MariaDB](https://mariadb.org/download/) or [MySQL](https://dev.mysql.com/downloads/mysql/) For the server data persistency

### First Time Build & Run

Clone the repository to your computer using Git (Or the GitHub desktop app, if
you prefer that)

```
git clone 
```

##### Windows

Download SA:MP Server archive and put all executables inside env folder of the project.

Download .NET 5 Runtime x86 binaries, create a folder called "dotnet" (or how you wish, remeber to edit coreclr line in server.cfg) and extract the archive in the folder

Now, open Visual Studio click on "Open project or solution" and browse to the clone of the repository and look for src/PrisonRP/PrisonRP.sln

After the project is open and loaded, right click on PrisonRP.GameMode project, go to properties "Debug" tab and set "Executable" path to samp-server.exe inside env folder and "Working directory" to env folder in order to be able to debug the project

Now, Compile and Run the project right from inside Visual Studio

##### Linux

Coming soon

### Don't Be Selfish

When you fix something, don't keep it to yourself. This is an open source
project. An important part of open source is sharing, that's why this code is
free of charge and available to all.

Please respect this. Feel free to keep your unique features private, just submit
_all_ fixes to the base code as pull requests or just email them to me/post them
as issues here.
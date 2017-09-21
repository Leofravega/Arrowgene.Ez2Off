PLEASE HELP!
===

IF YOU ARE A REVERSE ENGINEER, PLEASE HELP TO FIND A WAY TO START THE EXECUTABLE.  

We would like to develop the server against the latest official client (ez2on reboot).
The executable was packet with VMProtect 2.07 and we were able to unpack the executable,
so it can be loaded into a debugger.  

BUT we have not found out how to start the game or which parameters 
it requires, we always get a message stating "Please start the game from the web".
If you can find out how to correctly start this version of ez2on reboot, please let us know.
For more details feel free to contact us or open an issue.

Thanks alot!


Arrowgene.Ez2Off
===
Server Emulator for the Online Game Ez2On.


Client
===
Thanks to a great effort we collected all ez2on clients from various publishers.
You can find them here: mega.nz/#F!n88A1BjA!APvm7Sq9ILpcd3qZy-K4aQ

If you happen to have any files (Songs, Installer, Launcher (RetroLauncher.exe, etc.)) that are not currently in this repository please provide the files so we can complete the collection!

The client that is compatible with this server is hosted here:
Client Download: https://mega.nz/#!AF4TQJ5L!sHoa76UNDys1ZBxzy3wrNVrgwRDS9eZtcXXULkbrPAI
This is similar to the solista, and we are currently in the process of changing the focus to the 2nd 
version of ez2on so this server can support the properly installed version ez2on, until we find 
a way to start the 3rd version (see notice at top).

Running under OSX
===

### OSX - Server
1) Ensure you have .NET Core 2.0 Preview 1 or higher
- [Download Page](https://www.microsoft.com/net/core/preview#macos)
- [Direct Download .dmg](https://go.microsoft.com/fwlink/?linkid=848729)

2) Clone the project:
```
git clone https://github.com/Arrowgene/Arrowgene.Ez2Off.git
```

3) Change to the 'Command Line Interface'-Project:
```
cd Arrowgene.Ez2Off/Arrowgene.Ez2Off.CLI
```

4) Restore the project dependencies:
```
dotnet restore
```

5) Run the Server:
```
dotnet run
```

### OSX - Game

To Run the game on OSX you will need wine

1) Follow the tutorial until you completed Part 4:  
https://www.davidbaumgold.com/tutorials/wine-mac/

2) Copy the game directory to your wine 'C' drive.
The 'C'-drive can usually be found at your home directory
```
cd ~/.wine/drive_c
```

3) Copy the 'start.sh'-file inside the game directory

4) Start the game:
```
~/.wine/drive_c/ez2on/start.sh
```

If the server is running the game should connect to the Server.

### OSX - Development - VSCode

1) Download VSCode [https://code.visualstudio.com/](https://code.visualstudio.com/)
2) Install the C# Extension [https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp](https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp)
3) Make changes :)


Running under Windows
===

### Windows - Server
1) Ensure you have .NET Core 2.0 Preview 1 or higher  
[https://www.microsoft.com/net/core#windowsvs2017](https://www.microsoft.com/net/core#windowsvs2017)

2) Clone the project:
```
git clone https://github.com/Arrowgene/Arrowgene.Ez2Off.git
```

3) Open to the Project in Visual Studio and run the 'Command Line Interface'-Project.
I'm not sure about the exact steps as I havent tested this on a Windows box yet.
I Will update this tutorial once I know the exact steps

### Windows - Game

After downloadig the game execute the 'start.cmd'-file.
If the server is running the game should connect to the Server.
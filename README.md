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

For a quick look at the binaries, to see the message you can find them here:
[https://mega.nz/#!OgcmzagY!srLTDB1aFxUEIjLz-2Pa9acRd8s0_H0ZBA7R_5XvfDM](https://mega.nz/#!OgcmzagY!srLTDB1aFxUEIjLz-2Pa9acRd8s0_H0ZBA7R_5XvfDM)

But the executable will probably crash if you happen to start it correctly due to missing game files,
you will probably need the whole client at that point. If you get there please let us know!

Thanks alot!


Arrowgene.Ez2Off
===
Server Emulator for the Online Game Ez2On.


Clients
===
Thanks to a great effort we collected all ez2on clients from various publishers.
You can find them here: [https://mega.nz/#F!n88A1BjA!APvm7Sq9ILpcd3qZy-K4aQ](https://mega.nz/#F!n88A1BjA!APvm7Sq9ILpcd3qZy-K4aQ)

If you happen to have any files (Songs, Installer, Launcher (RetroLauncher.exe, etc.)) that are not currently in this repository please provide the files so we can complete the collection!

Supported Client
===
The client that is compatible with this server is hosted here:
[https://mega.nz/#!AF4TQJ5L!sHoa76UNDys1ZBxzy3wrNVrgwRDS9eZtcXXULkbrPAI](https://mega.nz/#!AF4TQJ5L!sHoa76UNDys1ZBxzy3wrNVrgwRDS9eZtcXXULkbrPAI)

Running under OSX
===

### OSX - Server
1) Ensure you have .NET Core 2.0 or higher
- [https://www.microsoft.com/net/download/macos](https://www.microsoft.com/net/download/macos)

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
1) Ensure you have .NET Core 2.0 or higher 
[https://www.microsoft.com/net/download/windows](https://www.microsoft.com/net/download/windows)

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


### Startup Parameter

osx ez.exe 127.0.0.1^|session^|account^|9999
win ez.exe 127.0.0.1\|session\|account\|9999


### TODO
Login Flow:
- Launcher with login -> authenticate against server API (create session) -> launch client with session
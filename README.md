# Fallout_4_VR_Unifier

# Application is used to merge Fallout 4 and Fallout 4 VR installations into one usable installation with a shared Data folder.

# I've tested this a fair amount, but your mileage may vary.  
I also take no responsibility for explosions, use at your own risk.

# Things you'll need or need to do both before and after installation -
* Fallout 4
* Fallout 4 VR
* Both installations must be on same hdd/ssd
* Run the EXE as admin, it'll fail if not more than likely.
* NMM or a mod manager (Optional)
* If switching between the two, you'll need to ensure that Fallout4_VR.esm is selected or unselected in your plugins list.

# Command line usage, if you want to make a shortcut or something, otherwise just use the GUI - 
Fallout_4_VR_Unifier.exe /m=<mode>
<mode> being 'vr' for FO4VR and 'flat' for FO4.

# INI files settings (Generated on first run)
ShareSaves = True // Save folder is shared between both installations, no copying saves back and forth if switching clients<br />
NMMCompatible = True // Plugins.txt/DLCList.txt is redirected from Fallout 4 to Fallout 4 VR automatically<br />
DataPath = c:\program files (x86)\steam\steamapps\common\fallout 4 VR\data // Fallout 4 VR's data path, not sure what'll happen if this is way different<br />
Fallout4Path = c:\program files (x86)\steam\steamapps\common\fallout 4\ // Installation path for Fallout 4<br />
Fallout4VRPath = c:\program files (x86)\steam\steamapps\common\fallout 4 vr\ // Installation path for Fallout 4 VR<br />
Fallout4Exe = Fallout4.exe // Executable launched when clicking Fallout 4<br />
Fallout4LaunchOptions = // Command line arguments, though Steam might not like this<br />
Fallout4VRExe = Fallout4Vr.exe // Executable launched when clicking Fallout 4 VR<br />
Fallout4VRLaunchOptions = // Command line arguments, though Steam might not like this<br />
Fallout4AppId = 377160 // Steam's AppID for Fallout 4, not currently used here<br />
Fallout4VrAppId = 611660 // Steam's AppID for Fallout 4 VR, not currently used here<br />
SteamPath = c:\program files (x86)\steam // Installation path for Steam, not currently used here<br />

To install -
1) Install Fallout 4

2) Install Fallout 4 VR

3) Place the VR Unifier folder in the Data folder of Fallout 4 VR

4) Copy the following files from the Fallout 4\Data folder to the VR Unifier\FO4 folder -
* Fallout4.esm
* Fallout4 - Interface.ba2
* Fallout4 - Materials.ba2
* Fallout4 - Meshes.ba2
* Fallout4 - Misc.ba2
* Fallout4 - Startup.ba2

5) Copy the following files from the Fallout 4 VR\Data folder to the VR Unifier\FO4VR folder -
* Fallout4.esm
* Fallout4 - Interface.ba2
* Fallout4 - Materials.ba2
* Fallout4 - Meshes.ba2
* Fallout4 - Misc.ba2
* Fallout4 - Startup.ba2
* Fallout4 - Misc - Beta.ba2
* Fallout4 - Misc - Debug.ba2
* Fallout4_VR.esm
* Fallout4_VR - Main.ba2
* Fallout4_VR - Shaders.ba2
* Fallout4_VR - Textures.ba2

6) Copy any mods from the Fallout 4\Data directory to the Fallout 4 VR\Data directory 
* At this point, you can delete the Fallout 4\Data directory if you wish, this is on you though,
* I'd suggest keeping this until everything is confirmed working, though. 
* If you want to keep it, leave it alone and it'll automatically archive.

7) Run VR Unifier\Fallout_4_VR_Unifier.exe --->as admin!!!<---

8) Click either the FO4 or FO4VR button and the setup will complete and either game will launch.
* To roll back the changes, click "Uninstall". This will restore any archived directories and separate the installations again.

9) At this point you're good to go, you can install mods normally as you would with Fallout 4, any installation specific mods will go into either VR Unifier\FO4 or VR Unifier\FO4VR.
* I haven't tested, but you should be able to have F4SE mods installed separately from VR by doing this also.

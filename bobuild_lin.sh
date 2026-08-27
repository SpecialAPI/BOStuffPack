modfolder="BOStuffPack"
gamepath="/home/$USER/.steam/steam/steamapps/common/Brutal Orchestra"
modinfofolder="ModInfo"
dllfolder="plugins"

#===================================================================
modpath="$gamepath/BrutalOrchestra_Data/StreamingAssets/mods/$modfolder"
dlldest="$modpath/$dllfolder"
echo Mod folder: $modpath

#FOLDERS
if [ ! -d "$modpath" ]; then
  echo Creating mod folder...
  mkdir "$modpath"
fi
if [ ! -d "$dlldest" ]; then
  echo Creating $dllfolder folder...
  mkdir "$dlldest"
fi

#DLL
dllname="$ASSEMBLYNAME.dll"
echo Copying DLL...
cp "$OUTDIR$dllname" "$dlldest/$dllname"

#MODINFO
if [ -d "$modinfofolder" ]; then
  if [ -f "$modinfofolder/modinfo.config" ]; then
    echo Copying modinfo.config...
    cp "$modinfofolder/modinfo.config" "$modpath/modinfo.config"
  fi
  if [ -f "$modinfofolder/thumbnail.png" ]; then
    echo Copying thumbnail...
    cp "$modinfofolder/thumbnail.png" "$modpath/thumbnail.png"
  fi
fi
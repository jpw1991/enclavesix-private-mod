#!/bin/bash

DEBUGDIR=ChebsModStub/bin/Debug
DLL=$DEBUGDIR/ChebsModStub.dll
LIB=../chebs-valheim-library/ChebsValheimLibrary/bin/Release/ChebsValheimLibrary.dll
BUN=../chebs-necromancy/ChebsNecromancyUnity/Assets/AssetBundles/chebsmodstub
PLUGINS=/home/$USER/.local/share/Steam/steamapps/common/Valheim/BepInEx/plugins
TRANSLATIONS=Translations

# Check that source files exist and are readable
if [ ! -f "$DLL" ]; then
    echo "Error: $DLL does not exist or is not readable."
    exit 1
fi

if [ ! -f "$LIB" ]; then
    echo "Error: $LIB does not exist or is not readable."
    exit 1
fi

if [ ! -f "$BUN" ]; then
    echo "Error: $BUN does not exist or is not readable."
    exit 1
fi

if [ ! -d "$TRANSLATIONS" ]; then
    echo "Error: $TRANSLATIONS does not exist or is not readable."
    exit 1
fi

# Check that target directory exists and is writable
if [ ! -d "$PLUGINS" ]; then
    echo "Error: $PLUGINS directory does not exist."
    exit 1
fi

if [ ! -w "$PLUGINS" ]; then
    echo "Error: $PLUGINS directory is not writable."
    exit 1
fi


cp -f "$DLL" "$PLUGINS" || { echo "Error: Failed to copy $DLL"; exit 1; }
cp -f "$LIB" "$PLUGINS" || { echo "Error: Failed to copy $LIB"; exit 1; }
cp -f "$BUN" "$PLUGINS" || { echo "Error: Failed to copy $BUN"; exit 1; }
cp -f "$BUN.manifest" "$PLUGINS" || { echo "Error: Failed to copy $BUN.manifest"; exit 1; }
cp -rf "$TRANSLATIONS" "$PLUGINS" || { echo "Error: Failed to copy $TRANSLATIONS"; exit 1; }
# WackysDatabase
WackysDatabase by Wackymole
Version 2.0

<!--
1) switch to yaml instead of json - Done
2) make more OOP - Done
3) AutoGenerated Icons - Done blax
3a- Custom Icons through png/jpeg - only on client side for now - Done
4) modular on yml files. - Done
5) support color and material changes on armor now. - Rexabit Done can be incorporated or separate 
6) VFX, SFX, FX - GET IT ALL - Done
7) Set Effects. - Done 
8) load slowly, so no lag on reload - Done
10) cloned items show up on mainscreen, will need some sort of cache - Done
11) Add a ton more config options for everything - Done
12) Catch up with wackydb 1.0 commits - Done
13) Fix Converter - Done
-->

Only this left is materials and visuals and load slowly. And any effectlist data if I ever want to do that.
Beta over 2-4 weeks probably.


Wackydatabase features: SetEffects! - ALL of them

4x more item configurations at least, Primary and Secondary Attacks

CustomIcons, autogenerated or jpeg/png(64x64)

Able to add or remove conversions on smelter pieces

MaxCraftingStationLevel, YML, Mainscreen cloned items, Water Resistance is back

Slow Reload!, Visuals Modifier by Rexabit!, Seteffect by Azu!

Most components can be removed from YML

sizeMultiplier! - Make a small or HUGE world

Future Proof - Can add or remove componets without much fuss


Materials is currently disabled in BETA! Future- https://github.com/Rexabit/valheim-visuals-modifier



<!-- <img src="https://wackymole.com/hosts/lightblue%20Sword.webp" width="248"/> <img src="https://wackymole.com/hosts/1825-1648309710-715635595.png" width="230"/> <img src="https://wackymole.com/hosts/orangeish%20bow.jpg" width="215"/> -->

The short summary Wackydatabase or Wackydb allows you do almost anything with items/pieces/recipes without coding. </br>
From cloning to color changes, set effects and even something as simple as language translating, as well as so much more. <br />


Features:
- WackysDatabase is a mod for Valheim it requires BepInEx for Valheim.
- With this mod you are able to control all items/recipes/pieces/effects via YML files.
- WackysDatabase also allows you to make clone/mock of these objects as well. 
- This mod is one of the last to load in the game. 
- As such it can touch almost all normal and modded objects.
- Can color items or changes icons of items/pieces/set effects

- You can create new items with this mod and make them exclusive. 


<details><summary> General Knowledge </summary>

    There are three (4) Objects that WackyDB touches. Items, Recipes, Pieces, Item Effects

    Items are things in your inventory, you can pickup and maybe equip them. 

    Recipes are used to construct items, CraftingStations can be workbench, forge, or hand crafted (craftingStation": "")

    Pieces are what you use in your hammer and hoe to construct or plant. (Piecehammers)

    Item Effects - Can be set Effects or Indiviudal effects - IE burning or frost, or an armor set effect

</details> 

<details><summary> Installation</summary>

Download and extract the latest version of WackysDatabase into the BepInEx plugin folder (usually Valheim/BepInEx/plugins )

Now run Valheim and join a world. After that go to Valheim/BepInEx/config/. There should be a folder called wackysDatabase,</br>
inside of that folder are currently three folders /Items/  /Recipes/ and /Pieces/

Put the mod on the Server to force Server Sync. The YML files only have to be on the Server. No need to share the YML. 

For Multiplayer, the mod has been locked down to prevent easy cheating, but I recommend https://valheim.thunderstore.io/package/Azumatt/AzuAntiCheat/ and https://valheim.thunderstore.io/package/Smoothbrain/ServerCharacters/ as well.


</details> 

<details><summary> FAQ</summary>


</details> 


<details><summary> Configuration cfg</summary>

## Configuration file BepInEx/config/WackyMole.WackysDatabase.cfg

The configs and their defaults are:

Force Server Config = true // forces server sync 

Enable this mod = true

IsDebug = true // tells you what is being loaded/ other basic actions

StringisDebug = false  // debugs your strings.. extra logs

IsAutoReload = false // auto reloads instead of wackydb_reload

NexusModID = 1825 // doesn't do much

DedServer load Memory = false // Dedicated Server will load objects into the game like a client would.

ExtraSecurity on Servers = true // - You cannot load into singleplayer and then load into Multiplayer. -.0.0.1 Error

FileWatcher for YMLs = true // wackydb_reloads on any changes to the wackydatabase folder on the server

</details>

<details><summary> Console Commands</summary>


- You will need to reference https://valheim-modding.github.io/Jotunn/data/objects/item-list.html for Prefab names. Thank you JVL team
- While in game press F5 to open the game console then type help for more informations. To enable console for valheim - launch options add "-console"

wackydb_reload  - Primary way to reload all the YML files in wackysDatabase folder.  </br> Can now be done remotely by an admin client

wackydb_reload_fast - No slow reload - will stutter the game

wackydb_save_recipe [ItemName] - saves a Recipe YML in wackysDatabase Recipe Folder

wackydb_save_piece [ItemName] - saves a Piece YML in wackysDatabase Piece Folder

wackydb_save_item [ItemName] - saves a Item YML in wackysDatabase Item Folder

wackydb_all_items - saves all items in game into wackyDatabase-BulkYML

wackydb_all_recipes - saves all recipes in game in wackyDatabase-BulkYML

wackydb_all_pieces [Hammer] [Optionally: Category] - Use 'Hammer' for default, should work with other modded hammers. </br>
You can optionally set what category to only get like 'Misc' </br>
- wackydb_all_pieces Hammer Misc

wackydb_se_all - Gets almost all SE_Effects in game, will get modded Effects (be careful) - Will save all into the Effects folder

wackydb_se [effectname] - get a singular effect, will save in Effect folder.

wackydb_se_create - Creates a clone of SetEffect_FenringArmor in Status Folder. You can edit to your liking and it should work.
You should be able to delete existing m_mods, by
</br>m_mods:
</br> -

wackydb_help -- commands

wackydb_clone  [recipe/item/piece] [Prefab to clone] [Unique name for the clone]  - clone an object and change it differently than a base game object. 

- For Example: wackydb_clone item SwordIron WackySword

<details><summary>optional 4th parameter</summary>
--There is a optional 4th parameter for clone RECIPES ONLY [original item prefab to use for recipe](Optional 4th parameter for a cloned item's recipes ONLY)
--For example you can already have item WackySword loaded in game, but now want a recipe. WackySword Uses SwordIron  - wackydb_clone recipe WackySword RWackySword SwordIron - otherwise manually edit
</details>

wackydb_clone_recipeitem [Prefab to clone] [clone name](clones item and recipe at same time)( Recipe name will be Rname) - instead of cloning an item and then recipe, do both at once. Saves you the trouble of manually editing recipe name and prefab.

wackydb_vfx - saves a vfx.txt file with all vfx effects of base game

wackydb_sfx - saves a sfx.txt file with all sfx effects of base game

wackydb_fx - saves a FX.txt file with all FX effects of base game

wackydb_material - saves a Materials.txt file in wackysDatabase for the different types of materials in the base game.

</details>

<details><summary> YML Knowledge</summary>

YML is easier to edit and change without getting confused on the syntac. 

You can use https://www.yamllint.com/ to validate any yml code

Almost every componet of items/pieces/recipes/effects can be deleted.

Some components are multilined where you can actually add your own stuff the ymls.

</details>

<details><summary> Item components</summary>


## Item Options:

![Glowing Red BronzeSword ](https://wackymole.com/hosts/redsword.png)

Hang on to your butts, items got an overhaul.  ONLY 2 components are required for ITEMS, 3 if it is a clone

name: item name in database, has to be unique (REQUIRED)

m_weight: weight of item (REQUIRED)

m_name: in game name

m_description: in game description

clonePrefabName: name of the item you want to clone (REQUIRED if clone)

cloneMaterial: You can change the material(colorish) of a cloned object.</br>
Images on nexus https://www.nexusmods.com/valheim/mods/1825 of the various changes you can make. </br>
Use wackydb_material to view a list of materials. Probably up to a 1/3 don't work or make the object invisible.

customIcon: You can set a custom icon for this item, use a PNG or Jpeg 64 x 64 px. Icon needs to be in the Icon folder ( doesn't server sync)

sizeMultiplier: Is a float, you can go from .01 to 1000.5 if you want. Have fun!

scale_weight_by_quality: scales weight by quality or something

## Attacks, Primary and Secondary
Most weapons have two attacks, you can control each independantly now. </br>
Primary_Attack and Secondary_Attack</br>
 Each one has 30+ things you can change, you can delete the whole section if you don't want to change anything.

 <details><summary>Attack Components</summary>

  Probably have to move to wackymole.com due to character count 32,000 - but github has no restriction so post away 


  AEffects - VFX,SFX, FX currently in view only mode for only Hit_Effects, </br> There is 
  Hit_Terrain_Effects, Start_Effect, Hold_Start_Effects, Trigger_Effect,Trail_Effect,Burst_Effect availble for both Primary and Secondary if people show interest.
 </details>

 Damage: dmg</br>
 DamagePerLevel: how much extra dmg you get for upgrading item</br> // add brs at end, once done editing

 Armor: armor doesn't do much on non clothing items
 ArmorPerLevel:

 m_foodHealth: health gained from food
 m_foodStamina: stamina gained from food
 m_foodRegen: regen from food tick
 m_foodBurnTime: how long it lasts
 m_FoodEitr: Eitr you get from food

 m_movementModifier: equip movement mod, can be neg
 m_EitrRegen: equip eitr regen, extra special stuff

 SE_Equip - EffectName: If you want an Item to have an Effect by itself, put the effect name here
 SE_SET_Equip: - Set Effect - All this Should be the same accorss all items that have this set
 SetName: What you call this Set
 Size: how many items share this set
 EffectName: What effect does this give when all items are equipped.

 m_blockPower
 m_blockPowerPerLevel
 m_timedBlockBonus: Perfect Parry
 m_deflectionForce
 m_deflectionForcePerLevel

 m_maxStackSize: how many can you stack in 1 slot
 m_canBeReparied: 
 m_destroyBroken: like tourch
 m_dodgeable: 
 m_questItem: doesn't really do anything now
 m_teleportable: tele or not
 m_knockback:
 m_useDurability: Durability goes down
 m_useDurabilityDrain: drain on use
 m_durabilityDrain: on equip?
 m_maxDurability: actual dura
 m_durabilityPerLevel:
 m_equipDuration: how long to equip item
 m_skillType: what skill this item belongs to
 m_animationState: 
 m_toolTier: what can it break?
 m_maxQuality: how much can you upgrade it
 m_value: if value is >0. Then the object becomes salable at Trader. </br>
 The Object Description gets a yellow Valuable notice. Just like base game you don't know what object you are selling to Trader.

damageModifiers: - 
Damage modifiers etc

The first value is the damage type, the second value is the resistance level.</br>
Blunt Slash Pierce Chop Pickaxe Physical Fire Frost Lightning Elemental Poison Spirit Water 
 
    Normal - no change
    Resistant - increases Wet status countdown speed by 100%
    Weak - decreases Wet status countdown speed by 1/3
    Immune - prevents Wet status effect
    Ignore - prevents Wet status effect
    VeryResistant - prevent wet status effect application except when swimming, increases Wet status countdown speed by 100%
    VeryWeak - decreases Wet status countdown speed by 2/3


GEffects Like Attack Effects above, only showing VFX, SFX and FX effects for Hit_Effects, </br>
Hit_Terrain_Effects, Start_Effect, Hold_Start_Effects, Trigger_Effect, Trail_Effect
</br> If there is interest I will allow users to change/remove/add them.

</details>

<details><summary> Piece components</summary>

<img src="https://wackymole.com/hosts/red%20walls.png" width="450"/>

Most of these components can be deleted if you don't need them

name: Database name, must be unique per hammer (Required)

piecehammer: hammer that the piece is located in - Default Hammer (Required)

m_name: in game name

sizeMultiplier: Probably the coolest feature, make a whole world of giant pieces or very small pieces. Float any number range .05 to 100000

m_description: in game:

customIcon: You can set a custom icon for this piece, use a PNG or Jpeg 64 x 64 px. Icon needs to be in the Icon folder ( doesn't server sync)

clonePrefabName: name of the piece you would like to clone (Required if clone)

material:

damagedMaterial: material change of damaged (50% piece)

craftingStation: What craftingstation needs to be near you to build the piece. Default: $piece_workbench

piecehammerCategory: You can change this, but things will be wonky if you add or remove any mods (maybe in future will fix)

minStationLevel: Min crafting station for construction, you could require a lvl 4 forge for example for Portals

amount: Probably best if you don't change this

disabled: disable this piece for everyone, (Can't build new ones)

adminonly: enable this piece only for admins, automically disables for everyone else, 

comfort:
    confort: amount
    ComfortGroup: like a category
    comfortObject:


groundPiece: idk
ground: idk
waterPiece: idk
noInWater: cannot be place in water
notOnFloor: not inside on wood floor
onlyinTeleportArea: not sure how big a teleport area is, I think those rock formations are teleport zone, could be fun for an advanced Portal
allowedInDungeons: use wisely
canBeRemoved: Infinity Hammer go burrr
wearNTearData:
    health: 0 or very high health makes stuff invincible due to rounding
    noRoofWear- no weather wear for roof stuff
    noSupportWear- idk
    supports:
    triggerPrivateArea- can't attack this thing inside bubble

craftingStationData:
    CraftingStationName: too risky to touch, removed
    cStationCustomIcon: You can set a custom icon for this piece, use a PNG or Jpeg 64 x 64 px. Icon needs to be in the Icon folder ( doesn't server sync)
    discoveryRange: range that you discovery the piece for the first time
    buildRange: how far build radius goes
    craftRequiresRoof:
    craftRequiresFire: cooking stations,
    showBasicRecipes: idk
    useDistance: how far away you can be while interacting
    useAnimation:

cSExtensionData: pieces that upgrade craftstations levels
    MainCraftingStationName: Should be craftingstation name
    maxStationDistance:
    continousConnection: animation of dots
    stack: idk

smelterData:
    smelterName:
    addOreTooltip: 
    emptyOreTooltip:
    fuelItem: You can change the fuel used to power your furance or whatever
    maxOre: capacity of ore
    maxFuel: capacity of fuel
    fuelPerProduct: how much fuel per product
    secPerProduct: seconds it takes
    spawnStack: Spawn stack on completion
    requiresRoof: 
    addOreAnimationLength:
    smelterConversion: You can edit/delete or add conversions here, which in my opionion is realllly cool


build: requirements to build: Item:amount:amountPerLevel:refundable,


Put this somewhere
cloneMaterial: You can change the material(colorish) of any (1.2.4) object. Images on nexus https://www.nexusmods.com/valheim/mods/1825 of the various changes you can make. 
- Use wackydb_material to view a list of materials. Probably up to a 1/3 don't work or make the object invisible. "material1,material2" (full,half health)(no spaces)
- Otherwise "material1", one material results in material being pasted for both full health and half-health. "same_mat" or "no_wear" sets pieces to have no wear material. 
- Should work for any piece at full health, some pieces change textures and models at 3/4 and 1/2 health, this won't stop them from changing. Maybe in future.

</details>

<details><summary> Recipe Components</summary>


<img src="https://wackymole.com/hosts/red%20forge.webp" width="700"/>

name: (Required must be Unique)

clonePrefabName: (Required if clone)

craftingStation: "" is hand crafted

minStationLevel: minstation required

maxStationLevelCap: Caps the station level to stop it from going outside possiblity: not working

repairStation: Where you can repair piece, people should be careful with this one

amount: obvious

disabled: disables recipe for everyone

reqs: (Required) requirements to build: Item:amount:amountPerLevel:refundable,

    Arrows x50 will be put above Arrow x20


</details>

<details><summary> Item Effect Components</summary>

You can delete an Effect or seteffect from item using EffectName : delete 

You should be able to delete existing m_mods, by
</br>m_mods:
</br> -

Use wackydb_se_create as a "template" to create a new status effect

customIcon, jpeg or png. ie wacky.png 64 x 64


</details>


<details><summary> 1.xx ChangeLog</summary>
        
        Version 1.4.2
            Had to disable Piece snapshot because of hovering pieces stacking up on each other, hopefully someone fixes it someday.
            You might have to destroy the existing pieces at (0,0) with infinity hammer quite a lot depending on reloads and players joining.    
        Version 1.4.1
            Some items don't like snapshot icons - Added extra checks and only items with material changes get new icons
        Version 1.4.0
            Added DedServer load Memory config to allow people to see if loading Wackydb on DedServer helps or hinders. 
            extraSecurity - Allows people who don't want the extra cheat protection to disable it and not get 0.0.1 Error
            Big News! Added auto Icon Generation to cloned Items, and all Pieces with custom material(pieces angles are a little wonky or wacky if you will) - Thx Blaxx for code
        Version 1.3.6
            Added m_attackHealthPercentage and m_secAttackHealthPercentage- Warning any Items that uses this Must be recreated. s
            Otherwise default will go to 0. These items include the staffs that use a percentage of player health to power. 
        Version 1.3.5
            I have decided to add more parameters to Json file, so please do not use older version of Wackydb after upgrading. Wackydb 2.0 is not coming soon
            Existing Jsons are fine to use, you can regenerate them to get new values. 
            Added m_EitrCost, m_secEitrCost - These are attack costs for primary and secondary weapon attacks, no Eitr, no swing. 
            m_FoodEitr - Food Eitr amount, m_EitrRegen- Modifier to Eitr Regen - Very powerful on clothes, weapons, added more warnings. 
            Seperated out m_attackStamina and secondary m_attackStamina
        Version 1.3.2
            Mistlands Update: Removed extra Wet effect/restance since Mistlands adds its own. 
            Removed FoodColor, as it was removed from game and didn't really do anything.
        Version 1.3.0
            Hopefully fixed Co-Op hosting bug again..
        Version 1.2.9
            Updated ServerSync for 211.11
        Version 1.2.8
            Hopefully fixed issue with Co-op hosting. Added 0.0.1 Custom message back. 
        Version 1.2.7
            Updated ServerSync for crossplay - Custom Message for Ver 0.0.1 is not displayed. No Singleplayer before multiplayer without restart.
            Known issue of TrophyDraugr is not able to be set (targets TrophyDraugrFem) use Fem or clone TrophyDraugr. 
            Destroyer is spelled with "troy", also now a loginfo instead of warning
        Version 1.2.5
            Moved wackysDatabase to Config instead of Plugins folder to stop r2mod from deleting folder on updates.
            Warning 1.2.4 and Lower will delete wackysDatabase folder in Plugins on Update, please backup.
        Version 1.2.4 
            Expanded Recipe Compatibility to Recipe_ ( Modification only, no cloning),  Can now change any material's type,
            category, craftingstation instead of just clones. Cannot change piecehammer of non clones. You can now set the piece's 
            material at 50% health. If you only set 1 it sets to both "full health" and "half health" otherwise,
            "material1, material2" (full, half health). "same_mat" or "no_wear" sets pieces to have no wear material. 
            Updated ServerSync and PieceManager
        Version 1.1.9
            Bug Fixes. Cleaned up Logs
        Version 1.1.8
            Fixed two main bugs,
            Properly unloading cloned assets on logout.
            Made it so some errors are caught better.
            Incorporated Water Resistance as done by aedenthorn.
        Version 1.1.5
            Cleaned up the code a lot. Fixed Pieces from getting null values from Server.
            Fixed the piece disable/admin for custom pieces.
            Made it so you can clone stone_floor (4x4 stone prefab) - editing it probably won't make it add to Hammer
            Added special case list for objects that have multiple Gameobjects. (Bow, SpearBronze)
        Version 1.10
            All About Pieces with this Update!
            Adds ability to clone an existing CraftingStation piece and make it a new CraftingStation 
                - The CraftingStation name is "name", add recipes to it with this name.
            Fixed other mods custom pieces. You should be able access and even clone other mods pieces now.
            Added piecehammerCategory so you can change the category where piece appears on the hammer. 
                - Mods might use numbers instead of words though.
            Added m_knockback Added m_backstabbonus Made m_attackStamina set both Primary and Secondary attacks.
        Version 1.05
            Mod Release


</details>

<details><summary> Last notes</summary>

## Last notes:

This mod should load last. It needs to so it can touch all other mods. 

> You can make changes to that OP bow and make it more realistic on damage or build requirements. Or even set a build piece to adminonly.

> clone the Item and change the material to make it a more appealing color. 

Submit pull requests to https://github.com/Wacky-Mole/WackysDatabase . The primary purpose of this mod is to edit objects, not to create clones/mocks. 


(Note!: If you want the game to have default values, close the game and delete the wackysDatabase folder).

</details>


<details><summary>Full Features</summary>

Planned features
- [x] Able to modify item data.
- [x] Able to modify recipes.
- [x] Able to modify pieces.
- [x] Able to modify materials on clones
- [x] Custom items/pieces
- [x] Custom recipes
- [x] Able to modify Set effects 
- [x] Cloned Items show up on MainScreen
- [x] Adjust attack values of items
- [x] Able to add or remove conversions on smelter pieces
- [x] Able to change the size of anything
Wackymole

</details>

<summary><b><span style="color:aqua;font-weight:200;font-size:20px">2.xx ChangeLog</span></b></summary>

| Version | Changes                                                                                                                                                                                                                                                                                                                                |
|----------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| 2.0.0 | - Release 2.0 <br/>




<details><summary>Credits</summary>

Credits:
aedenthorn and all of his Many Mods! https://github.com/aedenthorn/ValheimMods </br>
Thank you AzumattDev for the template. It is very good https://github.com/AzumattDev/ItemManagerModTemplate </br>
Thanks to the Odin Discord server, for being active and good for the valheim community.</br>
CustomArmor code from https://github.com/aedenthorn/ValheimMods/blob/master/CustomArmorStats/BepInExPlugin.cs </br>
Thank you to Azumatt and Aedenthorn and the JVL team. </br>
A Huge thank you to Rexabit and his Visual Modifier https://github.com/Rexabit/valheim-visuals-modifier
Azumatt for Status Editor contributions. 
Do whatever you want with this mod.</br>
</details>
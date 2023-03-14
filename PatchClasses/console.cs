﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using System.IO;
using System.Security.Cryptography;
using wackydatabase.Datas;
using wackydatabase.Util;
using wackydatabase.GetData;
using wackydatabase.Read;
using YamlDotNet.Serialization;
using YamlDotNet.Core;
using System.Threading;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization.EventEmitters;

namespace wackydatabase.PatchClasses
{

    public class QuoteSurroundingEventEmitter : ChainedEventEmitter
    {
        private int _itemIndex;

        public QuoteSurroundingEventEmitter(IEventEmitter nextEmitter) : base(nextEmitter) { }

        public override void Emit(ScalarEventInfo eventInfo, IEmitter emitter)
        {
            if (eventInfo.Source.StaticType == typeof(object) && _itemIndex++ % 2 == 1)
            {
                eventInfo.Style = ScalarStyle.SingleQuoted;
            }
            base.Emit(eventInfo, emitter);
        }
    }

    [HarmonyPatch(typeof(Terminal), nameof(Terminal.InitTerminal))]
    public static class Console_Patch
    {

        /*
         * 
         * 		new ConsoleCommand("spawn", "[amount] [level] [p/e/i] - spawn something. (End word with a star (*) to create each object containing that word.) Add a 'p' after to try to pick up the spawned items, adding 'e' will try to use/equip, 'i' will only spawn and pickup if you don't have one in your inventory.", delegate(ConsoleEventArgs args)
		{
			if (args.Length <= 1 || !ZNetScene.instance)
			{
				return false;
			}
			string text4 = args[1];
			int count = args.TryParameterInt(2);
			int level2 = args.TryParameterInt(3);
			bool pickup = args.HasArgumentAnywhere("p", 2);
			bool use = args.HasArgumentAnywhere("e", 2);
			bool onlyIfMissing = args.HasArgumentAnywhere("i", 2);
			DateTime now = DateTime.Now;
			if (text4.Length >= 2 && text4[text4.Length - 1] == '*')
			{
				text4 = text4.Substring(0, text4.Length - 1).ToLower();
				foreach (string prefabName in ZNetScene.instance.GetPrefabNames())
				{
					string text5 = prefabName.ToLower();
					if (text5.Contains(text4) && (text4.Contains("fx") || !text5.Contains("fx")))
					{
						spawn(prefabName);
					}
				}
			}
			else
			{
				spawn(text4);
			}
			ZLog.Log("Spawn time :" + (DateTime.Now - now).TotalMilliseconds + " ms");
			Gogan.LogEvent("Cheat", "Spawn", text4, count);
			return true;
			void spawn(string name)
        */
        private static void Postfix()
        {
            WMRecipeCust.WLog.LogDebug("Patching Updated Console Commands");

            if (!WMRecipeCust.modEnabled.Value)
                return;
            if (SceneManager.GetActiveScene().name != "main") return; // can't do anything from main


            Terminal.ConsoleCommand WackyShowcommands =
                new("wackydb", "Display Help ",
                    args =>
                    {
                        string output = $"wackydb_reload\r\n"
                        // + $"wackydb_reset \r\n"
                        + $"wackydb_dump [item/recipe/piece] [ObjectName]\r\n"
                        + $"wackydb_dump_all (dumps all info already loaded - may not work with clones very well)\r\n"
                        + $"wackydb_save_recipe  [RecipeName](recipe output)\r\n"
                        + $"wackydb_save_piece [PieceName](piece output) \r\n"
                        + $"wackydb_save_item [ItemName](item Output)\r\n"
                        + $"wackydb_help\r\n"
                        + $"wackydb_clone  [item/recipe/piece] [Prefab to clone] [Unique name for the clone] \r\n"
                        + $"4th paramater for recipes - you can already have item WackySword loaded in game, but now want a recipe. WackySword Uses SwordIron  - wackydb_clone recipe WackySword RWackySword SwordIron - otherwise manually edit\r\n"
                        + $"wackydb_clone_recipeitem  [Prefab to clone] [Unique name for the clone](Recipe name will be Rname) (clones item and recipe at same time)\r\n"
                        + $"wackydb_vfx (outputs future Vfx gameobjects available)\r\n"
                        + $"wackydb_material (outputs Materials available)\r\n"

                        ;

                        args.Context?.AddString(output);
                    });

            Terminal.ConsoleCommand WackyShowcommandshelp =
                 new("wackydb_help", "Display Help ",
                     args =>
                     {
                         string output = $"wackydb_reload\r\n"
                        // + $"wackydb_reset \r\n"
                        + $"wackydb_dump [item/recipe/piece] [ObjectName]\r\n"
                        + $"wackydb_dump_all (dumps all info already loaded - may not work with clones very well)\r\n"
                        + $"wackydb_save_recipe  [RecipeName](recipe output)\r\n"
                        + $"wackydb_save_piece [PieceName](piece output) \r\n"
                        + $"wackydb_save_item [ItemName](item Output)\r\n"
                        + $"wackydb_help\r\n"
                        + $"wackydb_clone  [item/recipe/piece] [Prefab to clone] [Unique name for the clone] \r\n"
                        + $"4th paramater for recipes - you can already have item WackySword loaded in game, but now want a recipe. WackySword Uses SwordIron  - wackydb_clone recipe WackySword RWackySword SwordIron - otherwise manually edit\r\n"
                        + $"wackydb_clone_recipeitem  [Prefab to clone] [Unique name for the clone](Recipe name will be Rname) (clones item and recipe at same time)\r\n"
                        + $"wackydb_vfx (outputs future Vfx gameobjects available)\r\n"
                        + $"wackydb_material (outputs Materials available)\r\n"

                      ;

                         args.Context?.AddString(output);
                     });
            /*
            Terminal.ConsoleCommand WackyReset =
                 new("wackydb_reset", "reload the whole config files", // I should probably delete this one?
                     args =>
                     {
                         context.Config.Reload();
                         context.Config.Save();
                         args.Context?.AddString("Configs reloaded");
                     });
            */
            Terminal.ConsoleCommand WackyReload =
                     new("wackydb_reload", "reload the whole config files",
                         args =>
                         {
                             // GetRecipeDataFromFiles(); called in loadallrecipes
                             if (ObjectDB.instance && wackydatabase.WMRecipeCust.issettoSinglePlayer)
                             {

                                 ReadFiles readnow = new ReadFiles();
                                 readnow.GetDataFromFiles();
                                 WMRecipeCust.readFiles = readnow;

                                 SetData.Reload josh = new SetData.Reload();
                                 WMRecipeCust.CurrentReload = josh;

                                 josh.LoadAllRecipeData(true);



                                 args.Context?.AddString($"WackyDatabase reloaded recipes/items/pieces from files");
                                 wackydatabase.WMRecipeCust.Dbgl("WackyDatabase reloaded recipes/items/pieces from files");
                             }
                             else
                             {
                                 args.Context?.AddString($"WackyDatabase did NOT reload recipes/items/pieces from files"); // maybe?
                                 wackydatabase.WMRecipeCust.Dbgl("WackyDatabase did NOT reload recipes/items/pieces from files");
                             }

                         });
            /*
                        Terminal.ConsoleCommand WackyDump =
                             new("wackydb_dump", "dump the item or recipe into the logs",
                                 args =>
                                 {
                                     if (args.Length - 1 < 2)
                                     {
                                         args.Context?.AddString("Not enough arguments");

                                     }
                                     else
                                     {
                                         string recipe = args[1];
                                         string comtype = args[2];
                                         if (recipe == "item" || recipe == "Item")
                                         {
                                             WItemData_json recipeData = GetData.GetData.GetItemDataByName(comtype);
                                             if (recipeData == null)
                                                 return;
                                             WMRecipeCust.Dbgl(JsonUtility.ToJson(recipeData));

                                         }
                                         else if (recipe == "piece" || recipe == "Piece")
                                         {
                                             PieceData_json data = GetData.GetData.GetPieceRecipeByName(comtype);
                                             if (data == null)
                                                 return;
                                             WMRecipeCust.Dbgl(JsonUtility.ToJson(data));
                                         }
                                         else
                                         {
                                             RecipeData_json recipeData = GetData.GetData.GetRecipeDataByName(comtype);
                                             if (recipeData == null)
                                                 return;
                                             WMRecipeCust.Dbgl(JsonUtility.ToJson(recipeData));
                                         }
                                         args.Context?.AddString($"WackyDatabase dumped {comtype}");
                                     }
                                 });

                        Terminal.ConsoleCommand WackyDumpAll =
                             new("wackydb_dump_all", "dump all",
                                 args =>
                                 {
                                     string TheStringMaster = "";
                                     string temp = "";
                                     if (WMRecipeCust.issettoSinglePlayer)
                                     {
                                         foreach (var data in wackydatabase.WMRecipeCust.ItemDatas)
                                         {
                                             if (data != null)
                                             {
                                                 WItemData_json output1 = GetData.GetData.GetItemDataByName(data.name);
                                                 if (output1 == null)
                                                     continue;
                                                 output1.clone = data.clone;
                                                 output1.cloneMaterial = data?.cloneMaterial;
                                                 output1.clonePrefabName = data?.clonePrefabName;
                                                 temp = JsonUtility.ToJson(output1);
                                                 TheStringMaster = TheStringMaster + temp + System.Environment.NewLine;
                                                 WMRecipeCust.Dbgl(temp);
                                             }
                                         }
                                         foreach (var data2 in WMRecipeCust.PieceDatas)
                                         {
                                             if (data2 != null)
                                             {
                                                 PieceData_json output2 = GetData.GetData.GetPieceRecipeByName(data2.name, false);
                                                 if (output2 == null)
                                                     continue;
                                                 output2.clone = data2.clone;
                                                 output2.cloneMaterial = data2.cloneMaterial;
                                                 output2.clonePrefabName = data2?.clonePrefabName;
                                                 output2.piecehammer = data2.piecehammer;
                                                 temp = JsonUtility.ToJson(output2);
                                                 TheStringMaster = TheStringMaster + temp + System.Environment.NewLine;
                                                 WMRecipeCust.Dbgl(temp);
                                             }
                                         }
                                         foreach (var data3 in WMRecipeCust.recipeDatas)
                                         {
                                             if (data3 != null)
                                             {
                                                 RecipeData_json output3 = GetData.GetData.GetRecipeDataByName(data3.name);
                                                 if (output3 == null)
                                                     continue;
                                                 output3.clone = data3.clone;
                                                 //output3.cloneColor = data3.cloneColor;
                                                 output3.clonePrefabName = data3.clonePrefabName;
                                                 temp = JsonUtility.ToJson(output3);
                                                 TheStringMaster = TheStringMaster + temp + System.Environment.NewLine;
                                                 WMRecipeCust.Dbgl(temp);
                                             }
                                         }
                                         File.WriteAllText(Path.Combine(WMRecipeCust.assetPathconfig, "DumpAll.txt"), TheStringMaster);
                                         args.Context?.AddString($"WackyDatabase dumped all, created file DumpAll.txt");
                                     }
                                     else
                                     {
                                         args.Context?.AddString($"In Multiplayer, so no all dump");
                                     }

                                 });
                        */

            Terminal.ConsoleCommand WackyitemSave =
                new("wackydb_save_item", "Save an Item ",
                    args =>
                    {
                        string file = args[1];
                        GetDataYML ItemCheck = new GetDataYML();

                        WItemData recipData = ItemCheck.GetItemDataByName(file, ObjectDB.instance);
                        if (recipData == null)
                            return;
                        WMRecipeCust.CheckModFolder();
                        var serializer = new SerializerBuilder()
                            .Build();
                        File.WriteAllText(Path.Combine(WMRecipeCust.assetPathItems, "Item_" + recipData.name + ".yml"), serializer.Serialize(recipData));
                        args.Context?.AddString($"saved item data to Item_{file}.yml");

                    }, isCheat: false, isNetwork: false, onlyServer: false, isSecret: false, allowInDevBuild: false, () => (!ZNetScene.instance) ? new List<string>() : ZNetScene.instance.GetPrefabNames());
            Terminal.ConsoleCommand WackyPieceSave =
                new("wackydb_save_piece", "Save a piece ",
                    args =>
                    {
                        string file = args[1];
                        GetDataYML PieceCheck = new GetDataYML();
                        PieceData recipData = PieceCheck.GetPieceRecipeByName(file, ObjectDB.instance);
                        if (recipData == null)
                            return;
                        WMRecipeCust.CheckModFolder();
                        var serializer = new SerializerBuilder()
                            .Build();
                        File.WriteAllText(Path.Combine(WMRecipeCust.assetPathPieces, "Piece_" + recipData.name + ".yml"), serializer.Serialize(recipData));
                        args.Context?.AddString($"saved data to Piece_{file}.yml");

                    }, isCheat: false, isNetwork: false, onlyServer: false, isSecret: false, allowInDevBuild: false, () => (!ZNetScene.instance) ? new List<string>() : ZNetScene.instance.GetPrefabNames());
            Terminal.ConsoleCommand WackyRecipeSave =
                new("wackydb_save_recipe", "Save a recipe ",
                    args =>
                    {
                        string file = args[1];
                        GetDataYML RecipeCheck = new GetDataYML();
                        RecipeData recipData = RecipeCheck.GetRecipeDataByName(file, ObjectDB.instance);
                        if (recipData == null)
                            return;
                        WMRecipeCust.CheckModFolder();
                        var serializer = new SerializerBuilder()
                            .Build();
                        File.WriteAllText(Path.Combine(WMRecipeCust.assetPathRecipes, "Recipe_" + recipData.name + ".yml"), serializer.Serialize(recipData));
                        args.Context?.AddString($"saved data to Recipe_{file}.yml");

                    }, isCheat: false, isNetwork: false, onlyServer: false, isSecret: false, allowInDevBuild: false, () => (!ZNetScene.instance) ? new List<string>() : ZNetScene.instance.GetPrefabNames());

            Terminal.ConsoleCommand WackyMaterials =
                new("wackydb_material", "Create txt file of materials",
                    args =>
                    {
                        string theString = Functions.GetAllMaterialsFile();
                        WMRecipeCust.CheckModFolder();
                        File.WriteAllText(Path.Combine(WMRecipeCust.assetPathconfig, "Materials.txt"), theString);
                        args.Context?.AddString($"saved data to Materials.txt");

                    });

            Terminal.ConsoleCommand Wackyvfx =
                new("wackydb_vfx", "Create txt file of VFX",
                    args =>
                    {
                        string theString2 = Functions.GetAllVFXFile();
                        WMRecipeCust.CheckModFolder();
                        File.WriteAllText(Path.Combine(WMRecipeCust.assetPathconfig, "vfx.txt"), theString2);
                        args.Context?.AddString($"saved data to VFX.txt");

                    });

            Terminal.ConsoleCommand WackySE =
                new("wackydb_se_all", "Get all SE effects in game and create your own",
                    args =>
                    {
                        var tod = ObjectDB.instance;
                        var max = tod.m_StatusEffects.Count();
                        GetDataYML SEcheck = new GetDataYML();
                        int count = 0;

                        var serializer = new SerializerBuilder()
                                        .Build();
                       // var deserialized = new DeserializerBuilder()
                          //      .Build();

                        while (count != max)
                        {
                           var temp = SEcheck.GetStatusEByNum(count, tod);
                            count++;
                            if (temp == null)
                                continue;
                            var part1 = serializer.Serialize(temp);
                            // deserialize yml into dictionary
                            /*
                            var deserialized = new DeserializerBuilder()
                            .Build()
                            .Deserialize<Dictionary<string, string>>(part1);
                            deserialized.Remove("m_icon");

                            var finalYml = new SerializerBuilder()
                            .WithEventEmitter(nextEmitter => new QuoteSurroundingEventEmitter(nextEmitter))
                            .Build()
                            .Serialize(deserialized);
                            */


                            File.WriteAllText(Path.Combine(WMRecipeCust.assetPathEffects, "SE_" +temp.Name+".yml"), part1);
                            
                        }
                        args.Context?.AddString($"saved all Status Effects to folder Effects");

                    });

            Terminal.ConsoleCommand WackySEOne =
                new("wackydb_se", "Get one SE effect by name",
                    args =>
                    {

                        string name = args[1];
                        var tod = ObjectDB.instance;
                        GetDataYML SEcheck = new GetDataYML();

                        var serializer = new SerializerBuilder()
                                        .Build();


                       var temp = SEcheck.GetStatusEByName(name, tod);
                        if (temp == null)
                        {
                            args.Context?.AddString($"No SE effect by that name");
                            return;
                        }                      

                        File.WriteAllText(Path.Combine(WMRecipeCust.assetPathEffects, "SE_" + temp.Name + ".yml"), serializer.Serialize(temp));
                                                    
                        args.Context?.AddString($"saved SE effect {name} to SE_{name}.yml in Effects folder");

                    });

            /* syntax for cloning
             * wackydb_clone <item/recipe/piece> <prefab to clone> <nameofclone>(has to be unquie otherwise we would have to check) 
             * 
             */
            Terminal.ConsoleCommand WackyClone =
                new("wackydb_clone", "Clone an item or piecce with different status, names, effects ect... ",
                    args =>
                    {
                        if (args.Length - 1 < 3)
                        {
                            args.Context?.AddString("<color=lime>Not enough arguments</color>");

                        }
                        else
                        {
                            string commandtype = args[1];
                            string prefab = args[2];
                            string newname = args[3];
                            string file = args[3];
                            if (newname == "SwordTest")
                            {
                                args.Context?.AddString($"<color=red>{newname} is already a ingame name. -Bad </color>");
                                return;
                            }
                            var serializer = new SerializerBuilder()
                            .Build();
                            GetDataYML RecipeCheck = new GetDataYML();

                            if (commandtype == "recipe" || commandtype == "Recipe")
                            {
                                WMRecipeCust.CheckModFolder();
                                if (args.Length - 1 < 4)
                                {
                                    RecipeData clone = RecipeCheck.GetRecipeDataByName(prefab, ObjectDB.instance);// actually it could be a different prefab if cloned item
                                    if (clone == null)
                                        return;
                                    clone.name = newname;
                                    clone.clone = true;
                                    clone.clonePrefabName = prefab;
                                    File.WriteAllText(Path.Combine(WMRecipeCust.assetPathRecipes, "Recipe_" + clone.name + ".yml"), serializer.Serialize(clone));
                                    file = "Recipe" + clone.name;
                                }
                                else
                                {
                                    string prefabitem = args[4];
                                    RecipeData clone = RecipeCheck.GetRecipeDataByName(prefabitem, ObjectDB.instance);//  prefab of cloned item
                                    if (clone == null)
                                        return;
                                    clone.name = newname;
                                    clone.clone = true;
                                    clone.clonePrefabName = prefab; // cloned item
                                    File.WriteAllText(Path.Combine(WMRecipeCust.assetPathRecipes, "Recipe_" + clone.name + ".yml"), serializer.Serialize(clone));
                                    file = "Cloned Item " + clone.name + " Clone Recipe from " + prefabitem;

                                } // added optional arugment for cloned items


                            }
                            if (commandtype == "item" || commandtype == "Item")
                            {
                                WItemData clone = RecipeCheck.GetItemDataByName(prefab, ObjectDB.instance);
                                if (clone == null)
                                    return;
                                clone.name = newname;
                                clone.clonePrefabName = prefab;
                                clone.m_name = newname;


                                if (clone == null)
                                    return;
                                WMRecipeCust.CheckModFolder();
                                File.WriteAllText(Path.Combine(WMRecipeCust.assetPathItems, "Item_" + clone.name + ".yml"), serializer.Serialize(clone));
                                file = "Item_" + clone.name;



                            }
                            if (commandtype == "piece" || commandtype == "Piece")
                            {
                                PieceData clone = RecipeCheck.GetPieceRecipeByName(prefab, ObjectDB.instance);
                                if (clone == null)
                                    return;
                                clone.name = newname;
                                clone.clone = true;
                                clone.clonePrefabName = prefab;



                                if (clone == null)
                                    return;
                                WMRecipeCust.CheckModFolder();
                                File.WriteAllText(Path.Combine(WMRecipeCust.assetPathPieces, "Piece_" + clone.name + ".yml"), serializer.Serialize(clone));
                                file = "Piece_" + clone.name;

                            }
                            args.Context?.AddString($"saved cloned data to {file}.yml");
                        }
                    });
            Terminal.ConsoleCommand WackyCloneRecipe =
                new("wackydb_clone_recipeitem", "Clone recipe and item with the orginal prefab ",
                    args =>
                    {

                        WMRecipeCust.CheckModFolder();
                        if (args.Length - 1 < 2)
                        {
                            args.Context?.AddString("<color=lime>Not enough arguments</color>");
                        }
                        else
                        {
                            var serializer = new SerializerBuilder()
                            .Build();
                            GetDataYML RecipeCheck = new GetDataYML();
                            string prefab = args[1];
                            string newname = args[2];
                            string file = args[2];
                            WItemData itemclone = RecipeCheck.GetItemDataByName(prefab, ObjectDB.instance);
                            if (itemclone == null)
                                return;
                            itemclone.name = newname;
                            itemclone.clonePrefabName = prefab;
                            itemclone.m_name = newname;
                            File.WriteAllText(Path.Combine(WMRecipeCust.assetPathItems, "Item_" + itemclone.name + ".yml"), serializer.Serialize(itemclone));

                            RecipeData clone = RecipeCheck.GetRecipeDataByName(prefab, ObjectDB.instance);//  prefab of cloned item
                            if (clone == null)
                                return;
                            clone.name = "R" + newname;
                            clone.clone = true;
                            clone.clonePrefabName = itemclone.name; // cloned item
                            File.WriteAllText(Path.Combine(WMRecipeCust.assetPathRecipes, "Recipe_" + clone.name + ".yml"), serializer.Serialize(clone));

                            file = "Cloned Item saved as Item_" + itemclone.name + ".yml, cloned Recipe saved as Recipe_" + clone.name + ".yml which is from the Orginal Recipe " + prefab;
                            args.Context?.AddString($"{file}");
                        }

                    });


            Terminal.ConsoleCommand Wackyadmin =    //dont look :)
                 new("wackydb_backdoor", "Gives you reload powers if you can guess the password",  // just for fun. Doesn't really give you admin powers
               args =>
               {
                   if (!WMRecipeCust.issettoSinglePlayer && WMRecipeCust.kickcount < 3)// backdoor for funizes only availble when on multiplayer mode.. hahaaa
                   {
                       string passguess = "";
                       try
                       {
                           passguess = args[1];
                       }
                       catch
                       {
                           WMRecipeCust.WLog.LogWarning("Congrats on finding the backdoor... You have 3 chances to guess the password or you will be called out that your a dirty cheater in chat and probably being kicked by Azu or an admin");
                           return;
                       }

                       WMRecipeCust.WLog.LogWarning($"guess {WMRecipeCust.kickcount + 1}");

                       string file = passguess;
                       string hash = Functions.ComputeSha256Hash(file);
                       string secrethash = "f289b4717485d90d9dee6ce2a9992e4fcfa4317a9439c148053d52c637b0691b"; // real hash is entered
                       if (hash == secrethash)
                       {
                           WMRecipeCust.WLog.LogWarning("Congrats you cheater,  Enjoy nothin");

                       }
                       else
                       {
                           WMRecipeCust.kickcount++;
                           if (WMRecipeCust.kickcount >= 3)
                           {
                               //List<string> stringList = ZNet.instance.GetPlayerList().Select(player => player.m_name).ToList();
                               string name = Player.m_localPlayer.name; // someday make this so it shouts to other players
                               Chat.m_instance.AddString("[WackysDatabase]",
                        $"<color=\"red\">Cheater Cheater, pants on fire. {name} tried to get admin access and failed. Laugh at this person or kick them.</color>",
                             Talker.Type.Normal);
                               WMRecipeCust.WLog.LogWarning("Cheater Cheater, pants on fire");
                           }

                       }

                   }
                    
               }, isCheat: false, isNetwork: false, onlyServer: false, isSecret: true, allowInDevBuild: false );



        }
    }
}
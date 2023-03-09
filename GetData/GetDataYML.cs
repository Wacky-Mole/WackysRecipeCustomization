﻿using System.Linq;
using HarmonyLib;
using UnityEngine;
using wackydatabase.Datas;
using BepInEx.Bootstrap;
using System.Reflection;
using static ItemSets;

namespace wackydatabase.GetData
{
    public class GetDataYML     {
        internal  RecipeData GetRecipeDataByName(string name, ObjectDB tod)
        {
            GameObject go = DataHelpers.CheckforSpecialObjects(name);// check for special cases
            if (go == null)
                go = tod.GetItemPrefab(name);

            if (go == null)
            {
                foreach (Recipe recipes in tod.m_recipes)
                {
                    if (!(recipes.m_item == null) && recipes.name == name)
                    {
                        WMRecipeCust.Dbgl($"An actual Recipe_ {name} has been found!-- Only Modification - No Cloning");
                        var dataRec = new RecipeData()
                        {
                            name = name,
                            amount = recipes.m_amount,
                            craftingStation = recipes.m_craftingStation?.m_name ?? "",
                            minStationLevel = recipes.m_minStationLevel,
                        };
                        foreach (Piece.Requirement req in recipes.m_resources)
                        {
                            dataRec.reqs.Add($"{Utils.GetPrefabName(req.m_resItem.gameObject)}:{req.m_amount}:{req.m_amountPerLevel}:{req.m_recover}");
                        }

                        return dataRec;
                    }
                }
            }

            if (go == null)
            {
                WMRecipeCust.Dbgl($"Recipe {name} not found!");
                return null; //GetPieceRecipeByName(name);
            }

            ItemDrop.ItemData item = go.GetComponent<ItemDrop>().m_itemData;
            if (item == null)
            {
                WMRecipeCust.Dbgl("Item data not found!");
                return null;
            }
            Recipe recipe = tod.GetRecipe(item);
            if (!recipe)
            {
                if (Chainloader.PluginInfos.ContainsKey("com.jotunn.jotunn"))
                {
                    object itemManager = Chainloader.PluginInfos["com.jotunn.jotunn"].Instance.GetType().Assembly.GetType("Jotunn.Managers.ItemManager").GetProperty("Instance", BindingFlags.Public | BindingFlags.Static).GetValue(null);
                    object cr = AccessTools.Method(itemManager.GetType(), "GetRecipe").Invoke(itemManager, new[] { item.m_shared.m_name });
                    if (cr != null)
                    {
                        recipe = (Recipe)AccessTools.Property(cr.GetType(), "Recipe").GetValue(cr);
                        WMRecipeCust.Dbgl($"Jotunn recipe: {item.m_shared.m_name} {recipe != null}");
                    }
                }

                if (!recipe)
                {
                    WMRecipeCust.Dbgl($"Recipe not found for item {item.m_shared.m_name}!");
                    return null;
                }
            }

            var data = new RecipeData()
            {
                name = name,
                amount = recipe.m_amount,
                craftingStation = recipe.m_craftingStation?.m_name ?? "",
                minStationLevel = recipe.m_minStationLevel,
            };
            foreach (Piece.Requirement req in recipe.m_resources)
            {
                data.reqs.Add($"{Utils.GetPrefabName(req.m_resItem.gameObject)}:{req.m_amount}:{req.m_amountPerLevel}:{req.m_recover}");
            }

            return data;
        }

        internal  RecipeData GetRecipeDataByNum(int count, ObjectDB tod)
        {
            var rep = tod.m_recipes[count];
            var dataRec = new RecipeData()
            {
                name = rep.name,
                amount = rep.m_amount,
                craftingStation = rep.m_craftingStation?.m_name ?? "",
                minStationLevel = rep.m_minStationLevel,
            };
            return dataRec;
        }





        internal PieceData GetPieceRecipeByName(string name, ObjectDB tod, bool warn = true)
        {
            Piece piece = null;
            WMRecipeCust.selectedPiecehammer = null; // makes sure doesn't use an old one. 
            GameObject go = DataHelpers.GetPieces(tod).Find(g => Utils.GetPrefabName(g) == name); // vanilla search  replace with FindPieceObjectName(data.name) in the future
            if (go == null)
            {
                go = DataHelpers.GetModdedPieces(name); // known modded Hammer search
                if (go == null)
                {
                    go = DataHelpers.CheckforSpecialObjects(name);// check for special cases
                    if (go == null) // 3th layer
                    {
                        WMRecipeCust.Dbgl($"Piece {name} not found! 3 layer search");
                        return null;
                    }
                }
                else // 2nd layer
                    WMRecipeCust.Dbgl($"Piece {name} from known hammer {WMRecipeCust.selectedPiecehammer}");
            }
            piece = go.GetComponent<Piece>();
            if (piece == null) // final check
            {
                WMRecipeCust.Dbgl("Piece data not found!");
                return null;
            }
            string piecehammer = null;
            if (WMRecipeCust.selectedPiecehammer != null)
                piecehammer = WMRecipeCust.selectedPiecehammer.name;

            if (piecehammer == null)
                piecehammer = "Hammer"; // default

            // these are kind of reduntant. // But are helpful for existing configs
            ItemDrop hammer = tod.GetItemPrefab("Hammer")?.GetComponent<ItemDrop>();
            if (hammer && hammer.m_itemData.m_shared.m_buildPieces.m_pieces.Contains(go))
                piecehammer = "Hammer";

            ItemDrop hoe = tod.GetItemPrefab("Hoe")?.GetComponent<ItemDrop>();
            if (hoe && hoe.m_itemData.m_shared.m_buildPieces.m_pieces.Contains(go))
                piecehammer = "Hoe";


            WMRecipeCust.WLog.LogWarning("Hammer selector needs helkp! in getdata GetPieceRecipeByName");
            return GetPiece(hammer, piecehammer, go, tod);
            

            string wackyname = "";
            string wackydesc = "";
            wackydesc = piece.m_description;
            wackyname = piece.m_name;
            string wackycatSring = piece.m_category.ToString();

            var data = new PieceData()
            {
                name = name,
                amount = 1,
                craftingStation = piece.m_craftingStation?.m_name ?? "",
                minStationLevel = 1,
                piecehammer = piecehammer,
                adminonly = false,
                m_name = wackyname,
                m_description = wackydesc,
                piecehammerCategory = wackycatSring,
            };
            foreach (Piece.Requirement req in piece.m_resources)
            {
                data.reqs.Add($"{Utils.GetPrefabName(req.m_resItem.gameObject)}:{req.m_amount}:{req.m_amountPerLevel}:{req.m_recover}");
            }

            return data;
        }

        internal PieceData GetPieceRecipeByNum(int count, string hammer, ObjectDB tod, ItemDrop itemD = null)
        {
            ItemDrop HamerItemdrop = null;
            if (itemD == null)
            {
                HamerItemdrop = tod.GetItemPrefab(hammer).GetComponent<ItemDrop>();
            }else
            {
                HamerItemdrop = itemD;
            }


            int PCount = HamerItemdrop.m_itemData.m_shared.m_buildPieces.m_pieces.Count();

            GameObject pieceSel = HamerItemdrop.m_itemData.m_shared.m_buildPieces.m_pieces[count];
            Piece actPiece = pieceSel.GetComponent<Piece>();

            return GetPiece(HamerItemdrop, hammer, pieceSel, tod);

        }

        private PieceData GetPiece (ItemDrop HammerID,string Hammername, GameObject PieceID, ObjectDB tod)
        {

            Piece piece = PieceID.GetComponent<Piece>();
            string wackydesc = piece.m_description;
            string wackyname = piece.m_name;
            string wackycatSring = piece.m_category.ToString();

            var data = new PieceData()
            {
                name = PieceID.name,
                amount = 1,
                craftingStation = piece.m_craftingStation?.m_name ?? "",
                minStationLevel = 1,
                piecehammer = Hammername,
                adminonly = false,
                m_name = wackyname,
                m_description = wackydesc,
                piecehammerCategory = wackycatSring,
            };
            foreach (Piece.Requirement req in piece.m_resources)
            {
                data.reqs.Add($"{Utils.GetPrefabName(req.m_resItem.gameObject)}:{req.m_amount}:{req.m_amountPerLevel}:{req.m_recover}");
            }

            return data;


        }





        internal WItemData GetItemDataByName(string name, ObjectDB tod)
        {
            GameObject go = tod.GetItemPrefab(name);
            if (go == null)
            {
                WMRecipeCust.Dbgl("GetItemDataByName data not found!");
                return null;
            }

            ItemDrop.ItemData data = go.GetComponent<ItemDrop>().m_itemData;
            if (data == null)
            {
                WMRecipeCust.Dbgl("Item GetItemDataByName not found! - componets");
                return null;
            }

            return GetItem(go, tod);
            
        }

        internal WItemData GetItemDataByCount(int count, ObjectDB tod)
        {
            var go = tod.m_items[count];
            return GetItem(go, tod);

        }

        private WItemData GetItem(GameObject go, ObjectDB tod) { 
            ItemDrop.ItemData data = go.GetComponent<ItemDrop>().m_itemData;
            if (data == null)
            {
                WMRecipeCust.Dbgl("Item GetItemDataByName not found! - componets");
                return null;
            }
            WDamages damages = null;
            if (data.m_shared.m_damages.m_blunt > 0f || data.m_shared.m_damages.m_chop > 0f || data.m_shared.m_damages.m_damage > 0f || data.m_shared.m_damages.m_fire > 0f || data.m_shared.m_damages.m_frost > 0f || data.m_shared.m_damages.m_lightning > 0f || data.m_shared.m_damages.m_pickaxe > 0f || data.m_shared.m_damages.m_pierce > 0f || data.m_shared.m_damages.m_poison > 0f || data.m_shared.m_damages.m_slash > 0f || data.m_shared.m_damages.m_spirit > 0f)
            {
                WMRecipeCust.Dbgl("Item " + go.GetComponent<ItemDrop>().name + " damage on ");

                damages = new WDamages 
                {

                    Blunt = data.m_shared.m_damages.m_blunt,
                    Chop = data.m_shared.m_damages.m_chop,
                    Damage = data.m_shared.m_damages.m_damage,
                    Fire = data.m_shared.m_damages.m_fire,
                    Frost = data.m_shared.m_damages.m_frost,
                    Lightning = data.m_shared.m_damages.m_lightning,
                    Pickaxe = data.m_shared.m_damages.m_pickaxe,
                    Pierce = data.m_shared.m_damages.m_pierce,
                    Poison = data.m_shared.m_damages.m_poison,
                    Slash = data.m_shared.m_damages.m_slash,
                    Spirit = data.m_shared.m_damages.m_spirit
                };
            }
            WDamages damagesPerLevel = null;
            if (data.m_shared.m_damagesPerLevel.m_blunt > 0f || data.m_shared.m_damagesPerLevel.m_chop > 0f || data.m_shared.m_damagesPerLevel.m_damage > 0f || data.m_shared.m_damagesPerLevel.m_fire > 0f || data.m_shared.m_damagesPerLevel.m_frost > 0f || data.m_shared.m_damagesPerLevel.m_lightning > 0f || data.m_shared.m_damagesPerLevel.m_pickaxe > 0f || data.m_shared.m_damagesPerLevel.m_pierce > 0f || data.m_shared.m_damagesPerLevel.m_poison > 0f || data.m_shared.m_damagesPerLevel.m_slash > 0f || data.m_shared.m_damagesPerLevel.m_spirit > 0f)
            {
                damagesPerLevel = new WDamages
                {
                    Blunt = data.m_shared.m_damagesPerLevel.m_blunt,
                    Chop = data.m_shared.m_damagesPerLevel.m_chop,
                    Damage = data.m_shared.m_damagesPerLevel.m_damage,
                    Fire = data.m_shared.m_damagesPerLevel.m_fire,
                    Frost = data.m_shared.m_damagesPerLevel.m_frost,
                    Lightning = data.m_shared.m_damagesPerLevel.m_lightning,
                    Pickaxe = data.m_shared.m_damagesPerLevel.m_pickaxe,
                    Pierce = data.m_shared.m_damagesPerLevel.m_pierce,
                    Poison = data.m_shared.m_damagesPerLevel.m_poison,
                    Slash = data.m_shared.m_damagesPerLevel.m_slash,
                    Spirit = data.m_shared.m_damagesPerLevel.m_spirit
                };

            }

            AttackArm Primary_Attack = new AttackArm
            {
                m_eitrCost = data.m_shared.m_attack.m_attackEitr,
                m_attackHealthPercentage = data.m_shared.m_attack.m_attackHealthPercentage,
                m_attackStamina = data.m_shared.m_attack.m_attackStamina,
            };

            AttackArm Secondary_Attack = new AttackArm
            {
                m_eitrCost = data.m_shared.m_secondaryAttack.m_attackEitr,
                m_attackHealthPercentage = data.m_shared.m_secondaryAttack.m_attackHealthPercentage,
                m_attackStamina = data.m_shared.m_secondaryAttack.m_attackStamina,
            };


            WItemData ItemData = new WItemData
            {
                name = go.GetComponent<ItemDrop>().name,
                m_armor = data.m_shared.m_armor,
                //clone = false,
                clonePrefabName = "",
                m_armorPerLevel = data.m_shared.m_armorPerLevel,
                m_blockPower = data.m_shared.m_blockPower,
                m_blockPowerPerLevel = data.m_shared.m_blockPowerPerLevel,
                m_deflectionForce = data.m_shared.m_deflectionForce,
                m_deflectionForcePerLevel = data.m_shared.m_deflectionForcePerLevel,
                m_description = data.m_shared.m_description,
                m_durabilityDrain = data.m_shared.m_durabilityDrain,
                m_durabilityPerLevel = data.m_shared.m_durabilityPerLevel,
                m_backstabbonus = data.m_shared.m_backstabBonus,
                m_equipDuration = data.m_shared.m_equipDuration,
                m_foodHealth = data.m_shared.m_food,
                // m_foodColor = ColorUtil.GetHexFromColor(data.m_shared.m_foodColor),
                m_foodBurnTime = data.m_shared.m_foodBurnTime,
                m_foodRegen = data.m_shared.m_foodRegen,
                m_foodStamina = data.m_shared.m_foodStamina,
                m_FoodEitr = data.m_shared.m_foodEitr,
                m_holdDurationMin = data.m_shared.m_attack.m_drawDurationMin,
                m_holdStaminaDrain = data.m_shared.m_attack.m_drawStaminaDrain,
                m_maxDurability = data.m_shared.m_maxDurability,
                m_maxQuality = data.m_shared.m_maxQuality,
                m_maxStackSize = data.m_shared.m_maxStackSize,
                m_toolTier = data.m_shared.m_toolTier,
                m_useDurability = data.m_shared.m_useDurability,
                m_useDurabilityDrain = data.m_shared.m_useDurabilityDrain,
                m_value = data.m_shared.m_value,
                m_weight = data.m_shared.m_weight,
                m_destroyBroken = data.m_shared.m_destroyBroken,
                m_dodgeable = data.m_shared.m_dodgeable,
                m_canBeReparied = data.m_shared.m_canBeReparied,
                m_name = data.m_shared.m_name,
                m_questItem = data.m_shared.m_questItem,
                m_teleportable = data.m_shared.m_teleportable,
                m_timedBlockBonus = data.m_shared.m_timedBlockBonus,
                m_movementModifier = data.m_shared.m_movementModifier,
                m_EitrRegen = data.m_shared.m_eitrRegenModifier,
                m_knockback = data.m_shared.m_attackForce,
                Primary_Attack = Primary_Attack,
                Secondary_Attack = Secondary_Attack,
                Damage = damages,
                Damage_Per_Level = damagesPerLevel,

                damageModifiers = data.m_shared.m_damageModifiers.Select(m => m.m_type + ":" + m.m_modifier).ToList(),

            };
            if (ItemData.m_foodHealth == 0f && ItemData.m_foodRegen == 0f && ItemData.m_foodStamina == 0f)
            {
                ItemData.m_foodColor = null;
            }

            return ItemData;

        }




    }
}

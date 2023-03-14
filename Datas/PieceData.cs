﻿using System;
using UnityEngine;
using System.Collections.Generic;

namespace wackydatabase.Datas
{
    [Serializable]
    public class PieceData
    {
#nullable enable
        public string name; //must have
        public string piecehammer; // must have
        public string? m_name;
        public float? sizeMultiplier;
        public string? m_description;
        public string? customIcon;
        public string? clonePrefabName;
        //public string cloneEffects;
        public string? material;
        public string? damagedMaterial;
        public string? craftingStation;
        public string? piecehammerCategory;
        public int? minStationLevel;
        public int? amount;
        public bool? disabled;
        public bool? adminonly;
        public List<string>? reqs = new List<string>();

        public ComfortData? comfort;

        //Placement
        public bool? groundPiece;
        public bool? ground;
        public bool? waterPiece;
        public bool? noInWater;
        public bool? notOnFloor;
        public bool? onlyinTeleportArea;
        public bool? allowedInDungeons;
        public bool? canBeRemoved;


        public WearNTearData? wearNTearData;

        public CraftingStationData? craftingStationData;

        public CSExtension? cSExtension;

        public SmelterData? smelterData;



    }
    public class ComfortData
    {
        //Confort
        public int? confort;
        public Piece.ComfortGroup? confortGroup;
        public GameObject? comfortObject;
    }

    public class WearNTearData {
        //WearNTear
        public float? health;
        public HitData.DamageModifiers damageModifiers; 
        public bool? noRoofWear;
        public bool? noSupportWear;
        public bool? supports;
        public bool? triggerPrivateArea;


    }


    public class CraftingStationData {

        //CraftingStation
        public string? cStationName;
        public string? cStationCustionIcon;
        public float? discoveryRange;
        public float? buildRange;
        public bool? craftRequiresRoof;
        public bool? craftRequiresFire;
        public bool? showBasicRecipes;
        public float? useDistance;
        public int? useAnimation;

    }
    public class CSExtension
    {
        //Station Extension
        public CraftingStation? stationExtensionCraftingStation;
        public float? maxStationDistance;
        public bool? continousConnection;
        public bool? stack;


    }

    public class SmelterData
    {
        //Smelter Script
        public string? smelterName;
        public string? addOreTooltip;
        public string? emptyOreTooltip;

        public Switch? addFuelSwitch;
        public Switch? addOreSwitch;
        public Switch? emptyOreSwitch;

        public ItemDrop? fuelItem;
        public int? maxOre;
        public int? maxFuel;
        public int? fuelPerProduct;
        public float? secPerProduct;
        public bool? spawnStack;
        public bool? requiresRoof;
        public float? addOreAnimationLength;
        public List<Smelter.ItemConversion>? smelterConversion;
    }


}
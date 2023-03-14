﻿using System;
using UnityEngine;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace wackydatabase.Datas
{
	[Serializable]
    [CanBeNull]
	public class WItemData 
	{
        #nullable enable
        public string name; // must have

        public string? m_name;

        public string? m_description;

        //public bool? clone;

        public string? clonePrefabName;

        public string? customIcon;

        //public string cloneEffects;

        public string? cloneMaterial;

        public float? sizeMultiplier;

        public float? m_weight; // must have

        public float? scale_weight_by_quality;

        public AttackArm? Primary_Attack;

        public AttackArm? Secondary_Attack;

        public WDamages? Damage;

        public WDamages? Damage_Per_Level;

        public ArmorData? Armor;

        public FoodData? FoodStats;

        public StatMods? Moddifiers;

        public SE_Equip? SE_Equip;

        public SE_SET_Equip? SE_SET_Equip;

        public ShieldData? ShieldStats;

        public int? m_maxStackSize;

        public bool? m_canBeReparied;

        public bool? m_destroyBroken;

        public bool? m_dodgeable;

        public bool? m_questItem;

        public bool? m_teleportable;

        public float? m_backstabbonus;

        public float? m_knockback;


        public bool? m_useDurability;

        public float? m_useDurabilityDrain;

        public float? m_durabilityDrain;

        public float? m_maxDurability;

        public float? m_durabilityPerLevel;

        public float? m_equipDuration;


        public Skills.SkillType? m_skillType; 

        public ItemDrop.ItemData.AnimationState? m_animationState; 

        
        public int? m_toolTier;

        public int? m_maxQuality;

        public int? m_value;

        public List<string>? damageModifiers = new List<string>();


#nullable disable
    }


    [Serializable]
    public class AttackArm  //:Attack
        {
        public Attack.AttackType? AttackType;
        public string? Attack_Animation;
        public int? Attack_Random_Animation;
        public int? Chain_Attacks;
        public bool? Hit_Terrain;

		public float? m_attackStamina;
        public float? m_eitrCost;
        public float? AttackHealthCost;
        public float? m_attackHealthPercentage;

        public float? SpeedFactor;
        public float? DmgMultiplier;
        public float? ForceMultiplier;
        public float? StaggerMultiplier;
        public float? RecoilMultiplier;

        public float? AttackRange;
        public float? AttackHeight;
        public GameObject? Spawn_On_Trigger;

        public bool? Requires_Reload;
        public string? Reload_Animation;
        public float? ReloadTime;
        public float? Reload_Stamina_Drain;

        public bool? Bow_Draw;
        public float? Bow_Duration_Min;
        public float? Bow_Stamina_Drain;
        public string? Bow_Animation_State;

        public float? Attack_Angle;
        public float? Attack_Ray_Width;
        public bool? Lower_Dmg_Per_Hit;
        public bool? Hit_Through_Walls;
        public bool? Multi_Hit;
        public bool? Pickaxe_Special;
        public float? Last_Chain_Dmg_Multiplier;

        public GameObject? Attack_Projectile;
        public float? Projectile_Vel;
        public float? Projectile_Accuraccy;
        public int? Projectiles;
    }

    [Serializable]
    public class FoodData
    {
        public float? m_foodHealth;
        public float? m_foodStamina;
        public float? m_foodRegen;
        public float? m_foodBurnTime;
        //public string? m_foodColor;
        public float? m_FoodEitr;
    }
    [Serializable]
    public class StatMods
    {
        public float? m_movementModifier;
        public float? m_EitrRegen;

    }

    [Serializable]
    public class SE_Equip
    {
        public string? EffectName;
    }

    [Serializable]
    public class SE_SET_Equip
    {
        public string? SetName;
        public int? Size;
        public string? EffectName; 

    }

    
    [Serializable]
    public class ShieldData
    {
        public float? m_blockPower;
        public float? m_blockPowerPerLevel;
        public float? m_timedBlockBonus;
        public float? m_deflectionForce;
        public float? m_deflectionForcePerLevel;
    }



    [Serializable]
	public class ArmorData { 
	
		//public string? name;

		public float? armor;
		public float? armorPerLevel;
		//public float movementModifier;

	}

	[Serializable]
	public class WDamages // can't get the inhertance in json to sterilize
	{
		public float Blunt;

		public float Chop;

		public float Damage;

		public float Fire;

		public float Frost;

		public float Lightning;

		public float Pickaxe;

		public float Pierce;

		public float Poison;

		public float Slash;

		public float Spirit;


    }



    [Serializable]
	public class WIngredients
	{
		public string id;
		public int amount;
		public int amountPerLevel;

	}

}
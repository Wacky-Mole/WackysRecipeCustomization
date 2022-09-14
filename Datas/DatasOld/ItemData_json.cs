﻿using System;
using UnityEngine;
using System.Collections.Generic;

namespace wackydatabase.Datas
{
	[Serializable]
	public class WItemData_json
	{

		public string name;

		public string m_name;

		public string m_description;

		public bool clone;

		public string clonePrefabName;

		//public string cloneEffects;

		public string cloneMaterial;

		public float m_weight;

		//public string m_skillType;

		//public string m_animationState;

		public int m_maxStackSize;

		public float m_foodHealth;

		public float m_foodStamina;

		public float m_foodRegen;

		public float m_foodBurnTime;

		public string m_foodColor;

		public float m_armor;

		public float m_armorPerLevel;

		public float m_movementModifier;

		public float m_blockPower;

		public float m_blockPowerPerLevel;

		public bool m_canBeReparied;

		//public WDamages m_damages;

		// public WDamages m_damagesPerLevel;

		public Dictionary<string, Attack> PrimaryAttack;

		public Dictionary<string, Attack> SecondaryAttack;

		public float m_timedBlockBonus;

		public float m_deflectionForce;

		public float m_deflectionForcePerLevel;

		public float m_backstabbonus;

		public float m_knockback;

		public bool m_destroyBroken;

		public bool m_dodgeable;

		public float m_maxDurability;

		public float m_durabilityDrain;

		public float m_durabilityPerLevel;

		public float m_equipDuration;

		public float m_holdDurationMin;

		public float m_holdStaminaDrain;

		//public string m_holdAnimationState;

		public int m_maxQuality;

		public bool m_useDurability;

		public float m_useDurabilityDrain;

		public bool m_questItem;

		public bool m_teleportable;

		public int m_toolTier;

		public int m_value;

		public string m_damages; // not sure what I am doing with my life

		public string m_damagesPerLevel;

		public List<string> damageModifiers = new List<string>();

		public string m_skilltype;


	}

	[Serializable]
	public class ArmorData_json
	{
		public string name;

		public float armor;
		public float armorPerLevel;
		public float movementModifier;

		public List<string> damageModifiers = new List<string>();
	}


}
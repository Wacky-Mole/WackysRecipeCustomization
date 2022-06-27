﻿using System;
using UnityEngine;
using System.Collections.Generic;

namespace wackydatabase.RD
{
    [Serializable]
    public class RecipeData
    {
        public string name;
        public bool clone;
        public string clonePrefabName;
        //public string cloneColor;
        public string craftingStation;
        public int minStationLevel;
        public int amount;
        public bool disabled;
        public List<string> reqs = new List<string>();
    }


}
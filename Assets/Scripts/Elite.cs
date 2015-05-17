﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public abstract class Elite: Character
    {
        protected List<Viech> activeViecher;


        public List<Viech> getActiveViecher()
        {
            return activeViecher;
        }

        public override bool isElite()
        {
            return true;
        }
    }
}

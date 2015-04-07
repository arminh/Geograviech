﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public abstract class Elite: Character
    {
        protected List<Viech> viecher;
        protected List<Viech> activeViecher;

        protected abstract void chooseViech();

        protected abstract void switchViech();

    }
}

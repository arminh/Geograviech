using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Linq;

using Assets.Scripts.Utils;
using Assets.Scripts.Items;
using Assets.Scripts.Items.Consumables;
using Assets.Scripts.Character;
using Assets.Scripts.Effects;

namespace Assets.Scripts {
    public static class SavefileHandler {

        public static void writeSavefile(Player player, string path)
        {
            XmlDocument doc = new XmlDocument();

            XmlElement savefile = (XmlElement)doc.AppendChild(doc.CreateElement("player"));

            XmlElement name = doc.CreateElement("name");
            name.InnerText = player.Name;
            savefile.AppendChild(name);

            XmlElement maxHealth = doc.CreateElement("maxHealth");
            maxHealth.InnerText = player.MaxHealth.ToString();
            savefile.AppendChild(maxHealth);

            XmlElement currentHealth = doc.CreateElement("currentHealth");
            currentHealth.InnerText = player.CurrentHealth.ToString();
            savefile.AppendChild(currentHealth);

            XmlElement speed = doc.CreateElement("speed");
            speed.InnerText = player.Speed.ToString();
            savefile.AppendChild(speed);

            XmlElement strength = doc.CreateElement("strength");
            strength.InnerText = player.Strength.ToString();
            savefile.AppendChild(strength);

            XmlElement level = doc.CreateElement("level");
            level.InnerText = player.Level.ToString();
            savefile.AppendChild(level);

            XmlElement xp = doc.CreateElement("xp");
            xp.InnerText = player.Xp.ToString();
            savefile.AppendChild(xp);

            XmlElement atts = doc.CreateElement("attacks");
            savefile.AppendChild(atts);
            addAttacks(ref doc, ref atts, player.Attacks);

            XmlElement viecher = doc.CreateElement("viecher");
            savefile.AppendChild(viecher);
            addViecher(ref doc, ref viecher, player.Viecher);

            XmlElement activeViecher = doc.CreateElement("activeViecher");
            savefile.AppendChild(activeViecher);
            var query = from slot in player.ActiveViecher select slot.Value;
            addViecher(ref doc, ref activeViecher, query.ToList());

            XmlElement weapons = doc.CreateElement("weapons");
            savefile.AppendChild(weapons);
            addWeapons(ref doc, ref weapons, player.Weapons);

            XmlElement activeWeapon = doc.CreateElement("activeWeapon");
            savefile.AppendChild(activeWeapon);
            addWeapon(ref doc, ref activeWeapon, player.ActiveWeapon);

            XmlElement items = doc.CreateElement("items");
            savefile.AppendChild(items);
            addItems(ref doc, ref items, player.Items);

            XmlElement prefabId = doc.CreateElement("prefabId");
            prefabId.InnerText = player.PrefabId;
            savefile.AppendChild(prefabId);

            XmlElement iconId = doc.CreateElement("iconId");
            iconId.InnerText = player.IconId;
            savefile.AppendChild(iconId);

            doc.Save(path);
        }

        private static void addEffect(ref XmlDocument doc, ref XmlElement el, Effect effect)
        {
            XmlElement eff = doc.CreateElement("type");
            eff.InnerText = effect.Type.ToString();
            el.AppendChild(eff);

            XmlElement inf = doc.CreateElement("inflictChance");
            eff.InnerText = effect.InflictChance.ToString();
            el.AppendChild(eff);
        }

        private static void addAttack(ref XmlDocument doc, ref XmlElement el, Attack attack)
        {
            XmlElement attName = doc.CreateElement("name");
            attName.InnerText = attack.Name;
            el.AppendChild(attName);

            XmlElement type = doc.CreateElement("type");
            type.InnerText = attack.Type.ToString();
            el.AppendChild(type);

            XmlElement minDamage = doc.CreateElement("minDamage");
            minDamage.InnerText = attack.MinDamage.ToString();
            el.AppendChild(minDamage);

            XmlElement maxDamage = doc.CreateElement("maxDamage");
            maxDamage.InnerText = attack.MaxDamage.ToString();
            el.AppendChild(maxDamage);

            XmlElement cooldownRounds = doc.CreateElement("cooldownRounds");
            cooldownRounds.InnerText = attack.CooldownRounds.ToString();
            el.AppendChild(cooldownRounds);

            XmlElement level = doc.CreateElement("level");
            level.InnerText = attack.Level.ToString();
            el.AppendChild(level);

            XmlElement effect = doc.CreateElement("effect");
            addEffect(ref doc, ref effect, attack.Effect);
        }

        private static void addAttacks(ref XmlDocument doc, ref XmlElement el, List<Attack> attacks)
        {
            foreach (Attack attack in attacks)
            {
                XmlElement att = doc.CreateElement("attack");
                el.AppendChild(att);

                addAttack(ref doc, ref att, attack);
            }
        }

        private static void addViecher(ref XmlDocument doc, ref XmlElement el, List<Viech> viecher)
        {
            foreach (Viech viech in viecher)
            {
                XmlElement v = doc.CreateElement("viech");
                el.AppendChild(v);

                XmlElement vname = doc.CreateElement("name");
                vname.InnerText = viech.Name;
                v.AppendChild(vname);

                XmlElement vtype = doc.CreateElement("type");
                vtype.InnerText = viech.Type.ToString();
                v.AppendChild(vtype);

                XmlElement vmaxHealth = doc.CreateElement("maxHealth");
                vmaxHealth.InnerText = viech.MaxHealth.ToString();
                v.AppendChild(vmaxHealth);

                XmlElement currentHealth = doc.CreateElement("currentHealth");
                currentHealth.InnerText = viech.CurrentHealth.ToString();
                v.AppendChild(currentHealth);

                XmlElement vspeed = doc.CreateElement("speed");
                vspeed.InnerText = viech.Speed.ToString();
                v.AppendChild(vspeed);

                XmlElement vstrength = doc.CreateElement("strength");
                vstrength.InnerText = viech.Strength.ToString();
                v.AppendChild(vstrength);

                XmlElement vlevel = doc.CreateElement("level");
                vlevel.InnerText = viech.Level.ToString();
                v.AppendChild(vlevel);

                XmlElement vxp = doc.CreateElement("xp");
                vxp.InnerText = viech.Xp.ToString();
                v.AppendChild(vxp);

                XmlElement vattacks = doc.CreateElement("attacks");
                v.AppendChild(vattacks);

                XmlElement prefabId = doc.CreateElement("prefabId");
                prefabId.InnerText = viech.PrefabId;
                v.AppendChild(prefabId);

                XmlElement iconId = doc.CreateElement("iconId");
                iconId.InnerText = viech.IconId;
                v.AppendChild(iconId);

                addAttacks(ref doc, ref vattacks, viech.Attacks);
            }
        }

        private static void addWeapon(ref XmlDocument doc, ref XmlElement el, Weapon weapon)
        {
            XmlElement wname = doc.CreateElement("name");
            wname.InnerText = weapon.Name;
            el.AppendChild(wname);

            XmlElement iname = doc.CreateElement("icon");
            iname.InnerText = weapon.Icon.name;
            el.AppendChild(iname);

            XmlElement att = doc.CreateElement("attack");
            el.AppendChild(att);

            addAttack(ref doc, ref att, weapon.Attack);
        }

        private static void addWeapons(ref XmlDocument doc, ref XmlElement el, List<Weapon> weapons)
        {
            foreach (Weapon weapon in weapons)
            {
                XmlElement w = doc.CreateElement("weapon");
                el.AppendChild(w);
                addWeapon(ref doc, ref w, weapon);
            }
        }

        private static void addItems(ref XmlDocument doc, ref XmlElement el, List<IConsumable> items)
        {
            foreach (IConsumable item in items)
            {
                XmlElement i = doc.CreateElement("item");
                el.AppendChild(i);
               
                XmlElement type = doc.CreateElement("type");
                type.InnerText = item.GetType().Name;
                i.AppendChild(type);

                XmlElement quantity = doc.CreateElement("quantity");
                quantity.InnerText = item.Quantity.ToString();
                i.AppendChild(quantity);
            }
        }

        public static Player readSavefile(string path)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(path);

            XmlNode savefile = xml.SelectSingleNode("player");

            string name = savefile.SelectSingleNode("name").InnerText;

            int maxHealth = 0;
            int currentHealth = 0;

            if (!int.TryParse(savefile.SelectSingleNode("maxHealth").InnerText, out maxHealth))
            {
                //Savefile corrupt
            }

            if (!int.TryParse(savefile.SelectSingleNode("currentHealth").InnerText, out currentHealth))
            {
                //Savefile corruptcu
            }

            int speed = 0;
            if (!int.TryParse(savefile.SelectSingleNode("speed").InnerText, out speed))
            {
                //Savefile corrupt
            }

            int strength = 0;
            if (!int.TryParse(savefile.SelectSingleNode("strength").InnerText, out strength))
            {
                //Savefile corrupt
            }

            int level = 0;
            if (!int.TryParse(savefile.SelectSingleNode("level").InnerText, out level))
            {
                //Savefile corrupt
            }

            int xp = 0;
            if (!int.TryParse(savefile.SelectSingleNode("xp").InnerText, out xp))
            {
                //Savefile corrupt
            }

            XmlNode atts = savefile.SelectSingleNode("attacks");
            List<Attack> attacks = getAttacks(atts);

            XmlNode vs = savefile.SelectSingleNode("viecher");
            List<Viech> viecher = getViecher(vs);

            XmlNode activeVs = savefile.SelectSingleNode("activeViecher");
            int i = 0;
            Dictionary<int, Viech> activeViecher = new Dictionary<int, Viech>();
            getViecher(activeVs).ForEach(m => activeViecher.Add(i++, m));

            XmlNode its = savefile.SelectSingleNode("items");
            List<IConsumable> items = getItems(its);

            XmlNode ws = savefile.SelectSingleNode("weapons");
            List<Weapon> weapons = getWeapons(ws);

            XmlNode w = savefile.SelectSingleNode("activeWeapon");
            Weapon activeWeapon = getWeapon(w);

            string prefabId = savefile.SelectSingleNode("prefabId").InnerText;
            string iconId = savefile.SelectSingleNode("iconId").InnerText;

            //int maxHealth, int currentHealth, int speed, int strength, string name, int xp, int level, List<Viech> viecher, List<Viech> activeViecher, List<Weapon> weapons, Weapon activeWeapon, List<IConsumable> items, List<Attack> attacks, string prefabId, string iconId
            Player player = new Player(maxHealth, currentHealth, speed, strength, name, xp, level, viecher, activeViecher, weapons, activeWeapon, items, attacks, prefabId, iconId);

            return player;
        }

        private static List<Attack> getAttacks(XmlNode atts)
        {
            List<Attack> attacks = new List<Attack>();

            foreach (XmlNode att in atts.ChildNodes)
            {
                attacks.Add(getAttack(att));
            }

            return attacks;
        }

        private static Attack getAttack(XmlNode att)
        {
            string name = att.SelectSingleNode("name").InnerText;
            Enums.ElementType type = (Enums.ElementType)Enum.Parse(typeof(Enums.ElementType), att.SelectSingleNode("type").InnerText);
            
            int minDamage = 0;
            if (!int.TryParse(att.SelectSingleNode("minDamage").InnerText, out minDamage))
            {
                //Savefile corrupt
            }

            int maxDamage = 0;
            if (!int.TryParse(att.SelectSingleNode("maxDamage").InnerText, out maxDamage))
            {
                //Savefile corrupt
            }

            int cooldownRounds = 0;
            if (!int.TryParse(att.SelectSingleNode("cooldownRounds").InnerText, out maxDamage))
            {
                //Savefile corrupt
            }

            Effect effect = getEffect(att.SelectSingleNode("effect"));

            //TODO Read Effect
            return new Attack(name, type, minDamage, maxDamage, cooldownRounds, effect, null);    
        }

        private static Effect getEffect(XmlNode eff)
        {
            Effect.EffectType type = (Effect.EffectType)Enum.Parse(typeof(Effect.EffectType), eff.SelectSingleNode("type").InnerText);

            int inflictChance = 0;
            if (!int.TryParse(eff.SelectSingleNode("inflictChance").InnerText, out inflictChance))
            {
                //Savefile corrupt
            }

            switch(type)
            {
                case Effect.EffectType.BURN:
                    return new BurnEffect(inflictChance);
                case Effect.EffectType.FREEZE:
                    return new FreezeEffect(inflictChance);
                case Effect.EffectType.POISON:
                    return new PoisonEffect(inflictChance);
                case Effect.EffectType.SLEEP:
                    return new SleepEffect(inflictChance);
                case Effect.EffectType.STUN:
                    return new StunEffect(inflictChance);
                default:
                    return null;
            }
        }

        private static List<Viech> getViecher(XmlNode vs)
        {
            List<Viech> viecher = new List<Viech>();

            foreach (XmlNode v in vs.ChildNodes)
            {
                string name = v.SelectSingleNode("name").InnerText;

                Enums.ElementType type = (Enums.ElementType)Enum.Parse(typeof(Enums.ElementType), v.SelectSingleNode("type").InnerText);

                int maxHealth = 0;
                int currentHealth = 0;

                if (!int.TryParse(v.SelectSingleNode("maxHealth").InnerText, out maxHealth))
                {
                    //Savefile corrupt
                }

                if (!int.TryParse(v.SelectSingleNode("currentHealth").InnerText, out currentHealth))
                {
                    //Savefile corruptcu
                }

                int speed = 0;
                if (!int.TryParse(v.SelectSingleNode("speed").InnerText, out speed))
                {
                    //Savefile corrupt
                }

                int strength = 0;
                if (!int.TryParse(v.SelectSingleNode("strength").InnerText, out strength))
                {
                    //Savefile corrupt
                }

                int level = 0;
                if (!int.TryParse(v.SelectSingleNode("level").InnerText, out level))
                {
                    //Savefile corrupt
                }

                int xp = 0;
                if (!int.TryParse(v.SelectSingleNode("xp").InnerText, out xp))
                {
                    //Savefile corrupt
                }

                XmlNode atts = v.SelectSingleNode("attacks");
                List<Attack> attacks = getAttacks(atts);

                viecher.Add(new Viech(maxHealth, currentHealth, speed, strength, name, level, xp, attacks, type, null, null));
            }

            return viecher;
        }

        private static List<Weapon> getWeapons(XmlNode node)
        {
            List<Weapon> weapons = new List<Weapon>();

            foreach (XmlNode w in node.ChildNodes)
            {
                weapons.Add(getWeapon(w));
            }

            return weapons;
        }

        private static Weapon getWeapon(XmlNode node)
        {
            string name = node.SelectSingleNode("name").InnerText;
            string iconName = node.SelectSingleNode("icon").InnerText;

            XmlNode att = node.SelectSingleNode("attack");
            Attack attack = getAttack(att);

            return new Weapon(name, attack, iconName);
        }

        private static List<IConsumable> getItems(XmlNode node)
        {
            List<IConsumable> items = new List<IConsumable>();

            foreach (XmlNode i in node.ChildNodes)
            {
                int quantity = 0;
                if (!int.TryParse(i.SelectSingleNode("quantity").InnerText, out quantity))
                {
                    //Savefile corrupt
                }

                string type = node.SelectSingleNode("type").InnerText;

                Type elementType = Type.GetType(type);
                IConsumable instance = (IConsumable)Activator.CreateInstance(elementType, new object[quantity]);

            }
            return null;
        }

    }
}
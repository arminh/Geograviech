using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace Assets.Scripts {
    public static class SavefileHandler {

        public static void writeSavefile(Player player, string path)
        {
            XmlDocument doc = new XmlDocument();

            XmlElement savefile = (XmlElement)doc.AppendChild(doc.CreateElement("player"));

            XmlElement name = doc.CreateElement("name");
            name.InnerText = player.Name;
            savefile.AppendChild(name);

            XmlElement identifier = doc.CreateElement("identifier");
            identifier.InnerText = player.Identifier;
            savefile.AppendChild(identifier);

            XmlElement maxHealth = doc.CreateElement("maxHealth");
            maxHealth.InnerText = player.MaxHealth.ToString();
            savefile.AppendChild(maxHealth);

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
            addViecher(ref doc, ref activeViecher, player.ActiveViecher);

            XmlElement weapons = doc.CreateElement("weapons");
            savefile.AppendChild(weapons);
            addWeapons(ref doc, ref weapons, player.Weapons);

            XmlElement activeWeapon = doc.CreateElement("activeWeapon");
            savefile.AppendChild(activeWeapon);
            addWeapon(ref doc, ref activeWeapon, player.ActiveWeapon);

            doc.Save(path);
        }

        private static void addAttack(ref XmlDocument doc, ref XmlElement el, Attack attack)
        {
            XmlElement attName = doc.CreateElement("name");
            attName.InnerText = attack.Name;
            el.AppendChild(attName);

            XmlElement type = doc.CreateElement("type");
            type.InnerText = attack.Type.ToString();
            el.AppendChild(type);

            XmlElement damage = doc.CreateElement("damage");
            damage.InnerText = attack.Damage.ToString();
            el.AppendChild(damage);

            /* XmlElement effect = doc.CreateElement("effect");
             effect.InnerText = attack.Effect.ToString();
             el.AppendChild(effect);*/
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

                XmlElement videntifier = doc.CreateElement("identifier");
                videntifier.InnerText = viech.Identifier;
                v.AppendChild(videntifier);

                XmlElement vmaxHealth = doc.CreateElement("maxHealth");
                vmaxHealth.InnerText = viech.MaxHealth.ToString();
                v.AppendChild(vmaxHealth);

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

                addAttacks(ref doc, ref vattacks, viech.Attacks);
            }
        }

        private static void addWeapon(ref XmlDocument doc, ref XmlElement el, Weapon weapon)
        {
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

       /* public static Player readSavefile(string path)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(path);

            XmlNodeList savefile = xml.GetElementsByTagName("player");

            Player player = new Player();

            return player;
        }*/
    }
}
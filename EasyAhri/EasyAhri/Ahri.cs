﻿using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyAhri
{
    class Ahri : Champion
    {
        public Ahri() : base("Ahri")
        {

        }

        protected override void InitializeSkins(ref SkinManager Skins)
        {
            Skins.Add("Ahri");
            Skins.Add("Dynasty Ahri");
            Skins.Add("Midnight Ahri");
            Skins.Add("Foxfire Ahri");
            Skins.Add("Popstar Ahri");
        }

        protected override void InitializeSpells()
        {
            Spell Q = new Spell(SpellSlot.Q, 880);
            Q.SetSkillshot(0.50f, 100f, 1100f, false, SkillshotType.SkillshotLine);

            Spell W = new Spell(SpellSlot.W, 800);

            Spell E = new Spell(SpellSlot.E, 975);
            E.SetSkillshot(0.50f, 60f, 1200f, true, SkillshotType.SkillshotLine);

            Spells.Add("Q", Q);
            Spells.Add("W", W);
            Spells.Add("E", E);
        }

        protected override void CreateMenu()
        {
            Menu.AddSubMenu(new Menu("Combo", "Combo"));
            Menu.SubMenu("Combo").AddItem(new MenuItem("Combo_q", "Use Q").SetValue(true));
            Menu.SubMenu("Combo").AddItem(new MenuItem("Combo_w", "Use W").SetValue(true));
            Menu.SubMenu("Combo").AddItem(new MenuItem("Combo_e", "Use E").SetValue(true));

            Menu.AddSubMenu(new Menu("Harass", "Harass"));
            Menu.SubMenu("Harass").AddItem(new MenuItem("Harass_q", "Use Q").SetValue(true));
            Menu.SubMenu("Harass").AddItem(new MenuItem("Harass_w", "Use W").SetValue(false));
            Menu.SubMenu("Harass").AddItem(new MenuItem("Harass_e", "Use E").SetValue(true));

            Menu.AddSubMenu(new Menu("Auto", "Auto"));
            Menu.SubMenu("Auto").AddItem(new MenuItem("Auto_q", "Use Q").SetValue(false));
            Menu.SubMenu("Auto").AddItem(new MenuItem("Auto_w", "Use W").SetValue(false));
            Menu.SubMenu("Auto").AddItem(new MenuItem("Auto_e", "Use E").SetValue(false));

            Menu.AddSubMenu(new Menu("Drawing", "Drawing"));
            Menu.SubMenu("Drawing").AddItem(new MenuItem("Drawing_q", "Q Range").SetValue(new Circle(true, Color.FromArgb(100, 0, 255, 0))));
            Menu.SubMenu("Drawing").AddItem(new MenuItem("Drawing_w", "W Range").SetValue(new Circle(true, Color.FromArgb(100, 0, 255, 0))));
            Menu.SubMenu("Drawing").AddItem(new MenuItem("Drawing_e", "E Range").SetValue(new Circle(true, Color.FromArgb(100, 0, 255, 0))));
        }

        protected override void Combo()
        {
            if (Menu.Item("Combo_e").GetValue<bool>()) Cast("E", SimpleTs.DamageType.Magical, true);
            if (Menu.Item("Combo_q").GetValue<bool>()) Cast("Q", SimpleTs.DamageType.Magical, true);
            if (Menu.Item("Combo_w").GetValue<bool>()) CastSelf("W", SimpleTs.DamageType.Magical);
        }
        protected override void Harass()
        {
            if (Menu.Item("Harass_e").GetValue<bool>()) Cast("E", SimpleTs.DamageType.Magical, true);
            if (Menu.Item("Harass_q").GetValue<bool>()) Cast("Q", SimpleTs.DamageType.Magical, true);
            if (Menu.Item("Harass_w").GetValue<bool>()) CastSelf("W", SimpleTs.DamageType.Magical);
        }
        protected override void Auto()
        {
            if (Menu.Item("Auto_e").GetValue<bool>()) Cast("E", SimpleTs.DamageType.Magical, true);
            if (Menu.Item("Auto_q").GetValue<bool>()) Cast("Q", SimpleTs.DamageType.Magical, true);
            if (Menu.Item("Auto_w").GetValue<bool>()) CastSelf("W", SimpleTs.DamageType.Magical);
        }

        protected override void Drawing()
        {
            Circle qCircle = Menu.Item("Drawing_q").GetValue<Circle>();
            Circle wCircle = Menu.Item("Drawing_w").GetValue<Circle>();
            Circle eCircle = Menu.Item("Drawing_e").GetValue<Circle>();

            if (qCircle.Active)
                Utility.DrawCircle(Player.Position, Spells["Q"].Range, qCircle.Color);
            if (wCircle.Active)
                Utility.DrawCircle(Player.Position, Spells["W"].Range, wCircle.Color);
            if (eCircle.Active)
                Utility.DrawCircle(Player.Position, Spells["E"].Range, eCircle.Color);
        }
    }
}
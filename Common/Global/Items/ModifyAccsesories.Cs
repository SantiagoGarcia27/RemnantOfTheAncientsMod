using CalamityMod;
using RemnantOfTheAncientsMod.Common.ModCompativilitie;
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Accesories;
using RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos;
using RemnantOfTheAncientsMod.Content.Items.Accesories.Fargos.Eternity;
using RemnantOfTheAncientsMod.Content.Items.Armor.Cosmetic.Sangar;
using RemnantOfTheAncientsMod.Content.Items.Armor.Cosmetic.TTIM;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace RemnantOfTheAncientsMod.Common.Global.Items
{
    public class ModifyAccsesories : GlobalItem
    {
        static Mod Calamity = RemnantOfTheAncientsMod.CalamityMod;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
        public override void SetDefaults(Item item)
        {
            bool WeaponConf = ModContent.GetInstance<ConfigServer>().VanillaWeaponsChangesConf;

            if (WeaponConf)
            {
                if (item.type == ItemID.BandofRegeneration)
                {
                    item.lifeRegen += 2;
                }
                if (item.type == ItemID.PlatinumWatch)
                {
                    item.defense = 1;
                }
                if (item.type == ItemID.GoldWatch)
                {
                    item.defense = 1;
                }
                if (item.type == ItemID.PortableStool)
                {
                    item.defense = 2;
                }
                if (item.type == ItemID.HandOfCreation)
                {
                    item.defense += 2;
                }
                if (item.type == ItemID.RedCape || item.type == ItemID.MysteriousCape || item.type == ItemID.PrinceCape || item.type == ItemID.WinterCape || item.type == ItemID.CrimsonCloak)
                {
                    item.GetGlobalItem<RemnantGlobalItem>().StyleStat = 6;
                }
                if (item.type == ItemID.DiamondRing)
                {
                    item.GetGlobalItem<RemnantGlobalItem>().StyleStat = 7;
                }
                if (item.type == ItemID.DiamondRing)
                {
                    item.GetGlobalItem<RemnantGlobalItem>().StyleStat = 6;
                }
                if (item.type == ModContent.ItemType<Sangar_Head>() || item.type == ModContent.ItemType<Sangar_Body>() || item.type == ModContent.ItemType<Sangar_Legs>() 
                || item.type == ModContent.ItemType<Ttim_Head>() || item.type == ModContent.ItemType<Ttim_Body>() || item.type == ModContent.ItemType<Ttim_Legs>())
                {
                    item.GetGlobalItem<RemnantGlobalItem>().StyleStat = 10;
                }
                
                item.GetGlobalItem<RemnantGlobalItem>().fillMusicBox();
                if (item.GetGlobalItem<RemnantGlobalItem>().MusicBox.Contains(item.type))
                {     
                    item.GetGlobalItem<RemnantGlobalItem>().StyleStat = 4;
                }
                if (item.vanity)
                {
                    if (RemnantPlayer.DevSuits.Contains(item.type))
                    {
                        item.GetGlobalItem<RemnantGlobalItem>().StyleStat = 6;
                    }
                    if (item.GetGlobalItem<RemnantGlobalItem>().StyleStat == 0)
                    {
                        item.GetGlobalItem<RemnantGlobalItem>().StyleStat = 5;
                    }    
                }     
            }
        }

        public static List<int> FastFall = new List<int>()
        {
        };
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            bool WeaponConf = ModContent.GetInstance<ConfigServer>().VanillaWeaponsChangesConf;
            if (item.type == ItemID.HandOfCreation)
            {
                player.pickSpeed -= 0.45f;
            }
            if (item.GetGlobalItem<RemnantGlobalItem>().StyleStat != 0)
            {
                player.GetModPlayer<RemnantPlayer>().StyleStat += item.GetGlobalItem<RemnantGlobalItem>().StyleStat;     
            }
            if (WeaponConf)
            {
                if (item.type == ItemID.JungleRose)
                {
                    player.GetCritChance(DamageClass.Generic) += 5;
                }
                if (item.type == ItemID.CritterShampoo)
                {
                    player.statLifeMax2 += DificultyUtils.ReaperMode ? 10 : 30;
                }
                if (item.type == ItemID.LuckyHorseshoe)
                {
                    player.luck += DificultyUtils.ReaperMode ? 0.2f : 0.4f;
                    player.luckMaximumCap = 2;
                }
                if (item.type == ItemID.FlurryBoots)
                {
                    if (DistanceUtils.GetPlayerFlour(player) == TileID.SnowBlock)
                    {
                        player.moveSpeed += 2f;
                    }
                }
               
                if (FastFall.Contains(item.type))
                {
                   // string ResultItemName = new Item(item.type).Name;
                    player.maxFallSpeed *= 2f;
                }
            }
            if(RemnantOfTheAncientsMod.FargosSoulMod != null)
            {
                UpdateAccessoryFargosSoulMod(item, player, hideVisual);
            }
            base.UpdateAccessory(item, player, hideVisual);
        }

        [JITWhenModsEnabled("CalamityMod")]
        public void UpdateAccessoryCalamityMod(Item item, Player player, bool hideVisual)
        {
            if (item.type == ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "EtherealTalisman"))
            {
                player.buffImmune[BuffID.ManaSickness] = true;
                player.Calamity().eTalisman = true;
                player.manaMagnet = true;
                if (!hideVisual)
                {
                    player.manaFlower = true;
                }
                player.statManaMax2 += 150;
                player.GetDamage<MagicDamageClass>() += 0.10f;
                player.manaCost *= 0.85f;
                player.GetCritChance<MagicDamageClass>() += 5f;
            }
        }
        public void UpdateAccessoryFargosSoulMod(Item item, Player player, bool hideVisual)
        {
            if (item.type == ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.FargosSoulMod, "MasochistSoul"))
            {
                ModContent.GetInstance<GuardiansShield>().UpdateAccessory(player, hideVisual);
            }
            if (item.type == ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.FargosSoulMod, "TerrariaSoul"))
            {
                ModContent.GetInstance<ForceOfRemants>().UpdateAccessory(player, hideVisual);
            }
            if (item.type == ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.FargosSoulMod, "EternitySoul"))
            {
                ModContent.GetInstance<ForceOfRemants>().UpdateAccessory(player, hideVisual);
                ModContent.GetInstance<GuardiansShield>().UpdateAccessory(player, hideVisual);
            }
        }

        public override void UpdateEquip(Item item, Player player)
        {
            if (item.GetGlobalItem<RemnantGlobalItem>().StyleStat != 0)
            {
                player.GetModPlayer<RemnantPlayer>().StyleStat += item.GetGlobalItem<RemnantGlobalItem>().StyleStat;
            }
            base.UpdateEquip(item, player);
        }


        public static void UpdateFallSpeedList()
        {
            RecipeUtils.GetUpgradeTree(ModContent.ItemType<Boot>(),FastFall);
        }

        public static void ModifyCalamity()
        {
            Utils1.AddSecure(FastFall, ExternalModCallUtils.GetItemFromMod(Calamity, "AngelTreads"));
            Utils1.AddSecure(FastFall, ExternalModCallUtils.GetItemFromMod(Calamity, "TracersCelestial"));
            Utils1.AddSecure(FastFall, ExternalModCallUtils.GetItemFromMod(Calamity, "TracersElysian"));
            Utils1.AddSecure(FastFall, ExternalModCallUtils.GetItemFromMod(Calamity, "TracersSeraph"));
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            FixDuplicatedPrefix(tooltips);

            if (item.type == ItemID.HandOfCreation)
            {
                var tip = tooltips.FirstOrDefault(tip => tip.Name == "Tooltip0" && tip.Mod == "Terraria");
                if (tip != null)
                {
                    var text = tip.Text.Replace("25", "45");
                    tip.Text = text;
                }
            }
            if (item.type == ModContent.ItemType<TrowerEmblem>()|| item.type == ModContent.ItemType<TrowerEmblemV2>())
            {
                var tip = tooltips.FirstOrDefault(tip => tip.Name == "Tooltip0" && tip.Mod == "Terraria");
                if (tip != null)
                {
                    var text = tip.Text.Replace("throwing", "rogue");
                    tip.Text = text;
                }
            }
            if (RemnantOfTheAncientsMod.FargosSoulMod != null)
            {
                ModifyTooltipsFargosSoulMod(item, tooltips);
            }
            if (RemnantOfTheAncientsMod.CalamityMod != null)
            {
                ModifyTooltipsCalamityMod(item, tooltips);
            }
        }
        public void ModifyTooltipsFargosSoulMod(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.FargosSoulMod, "MasochistSoul") || item.type == ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.FargosSoulMod, "EternitySoul"))
            {
                int ProjectileSizeBonus = 50;
                int ProjectileSpeedBonus = 10;
                int MovmentSpeedBonusBonus = 10;
                int DurationSeconds = 10;


                var t = tooltips;
                var tip = tooltips.FirstOrDefault(tip => tip.Name == "Tooltip15" && tip.Mod == "Terraria");
                if (tip != null)
                {
                    var text = tip.Text.Insert(tip.Text.LastIndexOf(tip.Text), Language.GetTextValue("Mods.RemnantOfTheAncientsMod.Items.GuardiansShield.Tooltip", ProjectileSizeBonus, ProjectileSpeedBonus, MovmentSpeedBonusBonus) + "\n"); //tip.Text.Replace("25", "45");
                    tip.Text = text;
                }
                Utils1.FindAndReplaceTooltip(tooltips, "&0", Utils1.TooltipHotkeyString(FargosKeybindSystem.TogleFrostBarrier));
                Utils1.FindAndReplaceTooltip(tooltips, "&1", DurationSeconds.ToString());
            }
        }
        public void ModifyTooltipsCalamityMod(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ExternalModCallUtils.GetItemFromMod(RemnantOfTheAncientsMod.CalamityMod, "EtherealTalisman"))
            {
                var tip = tooltips.FirstOrDefault(tip => tip.Name == "Tooltip0" && tip.Mod == "Terraria");
                if (tip != null)
                {
                    tip.Text = tip.Text.Replace("15", "10");

                    tip.Text = tip.Text.Replace("d 10", "d 15");
                }
            }
        }

            string tooltip = "";
        public void FixDuplicatedPrefix(List<TooltipLine> tooltips)
        {
            var tip = tooltips.FirstOrDefault(tip => tip.Mod != "a");
            int[] index = new int[2] { TextUtils.GetTooltipDuplicatedIndex(tooltips) ,0};
           
            if (index[0] != -1)
            {
                tooltips.RemoveAt(index[0]);
            }
            else
            {
                for (int a = 0; a < tooltips.Count(); a++)
                {
                    string tooltipt = tooltips[a].Text;
                    if (tooltips[a].Text.Contains("\n"))
                    {
                        int l = TextUtils.SeparateSringOnList(tooltipt).Count;
                        for (int i = 0; i < l - 1; i++)
                        {
                            if (i > 0)
                            {
                                if (TextUtils.SeparateSringOnList(tooltipt)[i] != TextUtils.SeparateSringOnList(tooltipt)[i - 1])
                                {
                                    if (TextUtils.SeparateSringOnList(tooltipt)[i] != TextUtils.SeparateSringOnList(tooltipt)[l - 1])
                                    {
                                        tooltip += TextUtils.SeparateSringOnList(tooltipt)[i] + "\n";
                                    }
                                }           
                            }
                            else
                            {
                                tooltip += TextUtils.SeparateSringOnList(tooltipt)[i] + "\n";
                            }
                        }
                        if (l - 2 >= 0)
                        {
                            tooltip += TextUtils.SeparateSringOnList(tooltipt)[l - 1];
                        }
                        else
                        {
                            tooltip += TextUtils.SeparateSringOnList(tooltips[l - 1].Text);
                        }

                        index[1] = TextUtils.GetTooltipIndexWithaWord(tooltips, tooltip);
                        if (index[1] != -1)
                        {
                            tooltips[index[1]].Text = tooltip;
                        }
                        tooltip = "";
                    }
                }
            }  
        }
        public override bool InstancePerEntity => true;
    }
}
using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Items;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace RemnantOfTheAncientsMod.Common.Global.Items
{
    public class AcendedReforgeRework : GlobalItem
    {
        public static Mod calamityMod = RemnantOfTheAncientsMod.CalamityMod;
        public static Mod RemnantOfTheAncient = RemnantOfTheAncientsMod.RemnantOfTheAncients;
        public static int storedPrefix = -1;
        public static int AcendedPrefixSelected = -1;
        public Dictionary<int, int> Reforges = new Dictionary<int, int>();

        public override void PreReforge(Item item)
        {
            storedPrefix = item.prefix;
        }

        public override bool InstancePerEntity => true;
        public override int ChoosePrefix(Item item, UnifiedRandom rand)
        {
            FillReforgeList();
            if(RemnantOfTheAncientsMod.CalamityMod != null)
            {
                FillCalamityList();
            }
           // storedPrefix = item.prefix;
            if (Reforges.ContainsKey(storedPrefix) && Main.LocalPlayer.HasItem(ModContent.ItemType<Terracoin>()))
            {
                if (Utils1.SearchNPC(NPCID.GoblinTinkerer, true) != null)
                {
                    if (NPC.CountNPCS(NPCID.GoblinTinkerer) > 0 && Utils1.SearchNPC(NPCID.GoblinTinkerer, true).active)
                    {
                        AcendedPrefixSelected = Reforges[storedPrefix];

                        return AcendedPrefixSelected;
                    }
                    else
                    {
                        return base.ChoosePrefix(item, rand);
                    }
                }
                return base.ChoosePrefix(item, rand);
            }
            else
            {
                return base.ChoosePrefix(item, rand);
            }
        }
        public override void PostReforge(Item item)
        {
            if (Reforges.ContainsValue(item.prefix))
            {
                if (NPC.CountNPCS(NPCID.GoblinTinkerer) > 0 && Utils1.SearchNPC(NPCID.GoblinTinkerer,true).active && Main.LocalPlayer.HasItem(ModContent.ItemType<Terracoin>()))
                {
                   
                    int ItemIndex = Main.LocalPlayer.FindItem(ModContent.ItemType<Terracoin>());
                    Main.LocalPlayer.inventory[ItemIndex].stack--;
                    item.prefix = AcendedPrefixSelected;
                }
            }
            else
            {
                base.PostReforge(item);
            }
        }
        public void FillReforgeList()
        {
            Reforges = new Dictionary<int, int>()
            {
                { PrefixID.Legendary,  ExternalModCallUtils.GetModPrefix(RemnantOfTheAncient, "Berserk") },
                { PrefixID.Legendary2,  ExternalModCallUtils.GetModPrefix(RemnantOfTheAncient, "Berserk") },
                { PrefixID.Unreal,  ExternalModCallUtils.GetModPrefix(RemnantOfTheAncient, "Veteran") },
                { PrefixID.Mythical,  ExternalModCallUtils.GetModPrefix(RemnantOfTheAncient, "Relic") },
                { PrefixID.Menacing,  ExternalModCallUtils.GetModPrefix(RemnantOfTheAncient, "Sharp") },
                { PrefixID.Warding,  ExternalModCallUtils.GetModPrefix(RemnantOfTheAncient, "Impenetrable") },
                { PrefixID.Lucky,  ExternalModCallUtils.GetModPrefix(RemnantOfTheAncient, "Acurate") },
                { PrefixID.Quick,  ExternalModCallUtils.GetModPrefix(RemnantOfTheAncient, "Supersonic") },
                { PrefixID.Violent,  ExternalModCallUtils.GetModPrefix(RemnantOfTheAncient, "Uncontrolled") },
                { ExternalModCallUtils.GetModPrefix(RemnantOfTheAncient, "Gigant"),  ExternalModCallUtils.GetModPrefix(RemnantOfTheAncient, "Titanic") }
            };
        }

        [JITWhenModsEnabled("CalamityMod")]
        public void FillCalamityList()
        {
            Reforges.Add(ExternalModCallUtils.GetModPrefix(calamityMod, "Flawless"), ExternalModCallUtils.GetModPrefix(RemnantOfTheAncient, "Exquisite"));
            Reforges.Add(ExternalModCallUtils.GetModPrefix(calamityMod, "Silent"), ExternalModCallUtils.GetModPrefix(RemnantOfTheAncient, "Shadow"));
        }
    }
}

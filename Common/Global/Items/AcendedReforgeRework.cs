/*using RemnantOfTheAncientsMod.Common.UtilsTweaks;
using RemnantOfTheAncientsMod.Content.Items.Items;
using SangarUtilities.Common.UtilsTweaks;
using System.Collections.Generic;
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
        public Dictionary<int, int> Reforges = [];

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
            return base.ChoosePrefix(item, rand);
        }
        public override void PostReforge(Item item)
        {
            base.PostReforge(item);
        }
        public void FillReforgeList()
        {
            Reforges = new Dictionary<int, int>()
            {
                { PrefixID.Legendary,  CallUtils.TryGetPrefixFromMod(RemnantOfTheAncient, "Berserk") },
                { PrefixID.Legendary2,  CallUtils.TryGetPrefixFromMod(RemnantOfTheAncient, "Berserk") },
                { PrefixID.Unreal,  CallUtils.TryGetPrefixFromMod(RemnantOfTheAncient, "Veteran") },
                { PrefixID.Mythical,  CallUtils.TryGetPrefixFromMod(RemnantOfTheAncient, "Relic") },
                { PrefixID.Menacing,  CallUtils.TryGetPrefixFromMod(RemnantOfTheAncient, "Sharp") },
                { PrefixID.Warding,  CallUtils.TryGetPrefixFromMod(RemnantOfTheAncient, "Impenetrable") },
                { PrefixID.Lucky,  CallUtils.TryGetPrefixFromMod(RemnantOfTheAncient, "Acurate") },
                { PrefixID.Quick2,  CallUtils.TryGetPrefixFromMod(RemnantOfTheAncient, "Supersonic") },
                { PrefixID.Violent,  CallUtils.TryGetPrefixFromMod(RemnantOfTheAncient, "Uncontrolled") },
                { CallUtils.TryGetPrefixFromMod(RemnantOfTheAncient, "Gigant"),  CallUtils.TryGetPrefixFromMod(RemnantOfTheAncient, "Titanic") }
            };
        }

        [JITWhenModsEnabled("CalamityMod")]
        public void FillCalamityList()
        {
            Reforges.Add(CallUtils.TryGetPrefixFromMod(calamityMod, "Flawless"), CallUtils.TryGetPrefixFromMod(RemnantOfTheAncient, "Exquisite"));
            Reforges.Add(CallUtils.TryGetPrefixFromMod(calamityMod, "Silent"), CallUtils.TryGetPrefixFromMod(RemnantOfTheAncient, "Shadow"));
        }
    }
}
*/
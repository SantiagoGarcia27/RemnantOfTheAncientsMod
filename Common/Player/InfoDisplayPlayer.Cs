using Terraria.ModLoader;
using Terraria;
using RemnantOfTheAncientsMod.Content.Items.Tools.Utilidad;
using System.Linq;
using RemnantOfTheAncientsMod.Common.Global.Items;

public class InfoDisplayPlayer : ModPlayer
{
    // Flag checking when information display should be activated
    public bool showMaxMinion = false;
    public bool showMaxSentry = false;
    public bool showDamageReduction = false;
    public bool showLifeRegen = false;
    public bool showManaRegen = false;
    public bool showArmorPenetration = false;
    public bool showMeleeAttackspeed = false;
    public bool showWingsTime = false;
    public bool showMiningSpeedBonus = false;
    public bool showMovmentSpeedBonus = false;
    public bool showLuck = false;
    public bool showDamageBonus = false;
    public bool showCritBonus = false;




    public override void PreUpdate()
    {
        bool n = Main.LocalPlayer.inventory.Any(item => RemnantGlobalItem.Phones.Contains(item.type) || item .type == ModContent.ItemType<PlayerStatViewer>())? true: false;

        showMaxMinion = n;
        showMaxSentry = n;
        showDamageReduction = n;
        showLifeRegen = n; 
        showManaRegen = n;      
        showArmorPenetration = n;
        showMeleeAttackspeed = n;
        showWingsTime = n;
        showMiningSpeedBonus = n;
        showLuck= n;
        showDamageBonus = n;
        showCritBonus= n;
        showMovmentSpeedBonus = n;

        base.PostUpdate();
    }
}
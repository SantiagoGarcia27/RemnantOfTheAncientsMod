using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.GameContent.Creative;
using RemnantOfTheAncientsMod.Content.Buffs.Debuff;

namespace RemnantOfTheAncientsMod.Content.Items.Accesories.Core
{
    [AutoloadEquip(EquipType.Balloon)]
    public class Infernal_core: ModItem
    {
        public override void SetStaticDefaults()
        {
           // //DisplayName.SetDefault("Infernal Tyrant Core");
            //Tooltip.SetDefault("Increases melee damage by 10%"
            //+ "\n Inflict Hellfire on attack"
            //+ "\n Movement speed increases and you become immune to fire blocks and On Fire debuffs");

           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Noyau du tyran infernal");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Augmente les dégâts magiques de 10%"
            //+ "\n La vitesse de déplacement est augmentée et vous êtes immunisé contre le debuff Frozen et Chilled lorsque vous êtes dans la neige");

           // //DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Núcleo de Tirano infernal");
            //Tooltip.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Aumenta el daño cuerpo a cuerpo en un 10%"
            //+ "\n Inflige fuego del infierno al atacar"
            //+ "\n La velocidad de movimiento aumenta y te vuelves inmune a los bloques de fuego y las desventajas de en llamas");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public static readonly int MoveSpeedBonus = 50;
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(10, MoveSpeedBonus);
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 14;
            Item.value = 45000;
            Item.rare = ItemRarityID.Red;
            Item.expert = true;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Melee) *= 1.10f;
            player.GetModPlayer<RemnantPlayer>().MeleeBuffInflict.Add(ModContent.BuffType<Hell_Fire>());
            player.moveSpeed += MoveSpeedBonus/100f;
            player.buffImmune[BuffID.OnFire] = true;
        }
    }
}
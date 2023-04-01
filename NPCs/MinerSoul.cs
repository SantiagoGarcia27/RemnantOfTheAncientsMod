using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using RemnantOfTheAncientsMod.Items.Tools;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ModLoader.Utilities;

namespace RemnantOfTheAncientsMod.NPCs
{
	// This NPC inherits from the Hover abstract class included in ExampleMod, which is a more customizable copy of the vanilla Hovering AI.
	// It implements the `CustomBehavior` and `ShouldMove` virtual methods being overridden here, as well as the `acceleration` and `accelerationY` field being set in the class constructor.
	public class MinerSoul : Hover
	{
		public MinerSoul()
		{
acceleration = 0.06f;
accelerationY = 0.025f;
		}

		public override void SetStaticDefaults()
		{
DisplayName.SetDefault("Miner Soul");
DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), "Alma minera");
DisplayName.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), "Âme de mineur");
Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Wraith];

		}

		public override void SetDefaults()
		{
NPC.CloneDefaults(NPCID.Wraith);
NPC.width = 28;
NPC.height = 36;
NPC.aiStyle = -1;
NPC.damage = 0;
NPC.friendly = true;
NPC.dontTakeDamageFromHostiles = true;
AnimationType = NPCID.Wraith;
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
return SpawnCondition.Cavern.Chance * 0.09f;//0.0009
		}

		// Allows hitting the NPC with melee type weapons, even if it's friendly.
		public override bool? CanBeHitByItem(Player player, Item item)
		{
return false;
		}

		// Same as the above but with projectiles.
		public override bool? CanBeHitByProjectile(Projectile projectile)
		{
return projectile.friendly && projectile.owner < 255;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
if (NPC.life > 0)
{
	for (int i = 0; i < damage / NPC.lifeMax * 100; i++)
	{
		Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Ghost, hitDirection, -1f, 100, new Color(100, 100, 100, 100), 1f);
		dust.noGravity = true;
	}
	return;
}
for (int i = 0; i < 50; i++)
{
	Dust dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Ghost, 2 * hitDirection, -2f, 100, new Color(100, 100, 100, 100), 1f);
	dust.noGravity = true;
}
		}

		// Allows the NPC to talk with the player, even if it isn't a town NPC.
		public override bool CanChat()
		{
return true;
		}   

        public override string GetChat()
		{
// NPC.SpawnedFromStatue value is kept when the NPC is transformed.
switch (Main.rand.Next(3))
{
	case 0:
		return "Hello, could you bring an iron pickaxe?";
	case 1:
		return "I remember those days when I was looking for gold in these parts."
		+ "\n But now I've run out of a iron pick, could you get me one?";
	case 2:
		return "Hello, could you bring an iron pickaxe?";
	default:
		return "...";
}
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
if (firstButton)
{
	shop = true;
}
else
{
	// Hit the NPC for about 500 damage
	if (Main.LocalPlayer.HasItem(ItemID.IronPickaxe))
	{
		Main.npcChatText = $"Thanks you";
		int IronPickaxeItemIndex = Main.LocalPlayer.FindItem(ItemID.IronPickaxe);
		Main.LocalPlayer.inventory[IronPickaxeItemIndex].TurnToAir();
		Main.LocalPlayer.QuickSpawnItem(NPC.GetSource_Loot(), ItemType<Ghost_Pickaxe>());
		Main.LocalPlayer.QuickSpawnItem(NPC.GetSource_Loot(), ItemID.GoldCoin, 10);
		Main.LocalPlayer.ApplyDamageToNPC(NPC, Main.DamageVar(500), 5f, Main.LocalPlayer.direction, true);
		return;
	}
	// Hit the NPC for about 500 damage
	if (Main.LocalPlayer.HasItem(ItemID.LeadPickaxe))
	{
		Main.npcChatText = $"Thanks you";
		int LeadPickaxeItemIndex = Main.LocalPlayer.FindItem(ItemID.LeadPickaxe);
		Main.LocalPlayer.inventory[LeadPickaxeItemIndex].TurnToAir();
		Main.LocalPlayer.QuickSpawnItem(NPC.GetSource_Loot(), ItemType<Ghost_Pickaxe>());
		Main.LocalPlayer.QuickSpawnItem(NPC.GetSource_Loot(), ItemID.GoldCoin, 10);
		Main.LocalPlayer.ApplyDamageToNPC(NPC, Main.DamageVar(500), 5f, Main.LocalPlayer.direction, true);
		return;

	}
	if (Main.LocalPlayer.HasItem(ItemID.EnchantedSword))
	{
		Main.npcChatText = $"...";
		int EnchantedSwordItemIndex = Main.LocalPlayer.FindItem(ItemID.EnchantedSword);
		Main.LocalPlayer.inventory[EnchantedSwordItemIndex].TurnToAir();
		Main.LocalPlayer.QuickSpawnItem(NPC.GetSource_Loot(), ItemID.Arkhalis, 1);
		Main.LocalPlayer.QuickSpawnItem(NPC.GetSource_Loot(), ItemID.GoldCoin, 100);
		Main.LocalPlayer.ApplyDamageToNPC(NPC, Main.DamageVar(500), 5f, Main.LocalPlayer.direction, true);
		return;

	}
}

		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
button = Language.GetTextValue("LegacyInterface.28");

if (Main.LocalPlayer.HasItem(ItemID.IronPickaxe))
	button2 = "Deliver Pickaxe";

if (Main.LocalPlayer.HasItem(ItemID.LeadPickaxe))
	button2 = "Deliver Pickaxe";

if (Main.LocalPlayer.HasItem(ItemID.EnchantedSword))
	button2 = "Increase Power";
		}

		public override void SetupShop(Chest shop, ref int nextSlot)
		{

shop.item[nextSlot].SetDefaults(ItemType<ironstonepickaxe>());
            shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 3, 0, 0);
            nextSlot++;

shop.item[nextSlot].SetDefaults(ItemID.Torch);
            shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 0, 30);
            nextSlot++;

shop.item[nextSlot].SetDefaults(ItemID.IronBar);
            shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 5, 0);
            nextSlot++;

shop.item[nextSlot].SetDefaults(ItemID.Wood);
            shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 0, 20);
            nextSlot++;

shop.item[nextSlot].SetDefaults(ItemID.DirtBlock);
            shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 0, 2);
            nextSlot++;

shop.item[nextSlot].SetDefaults(ItemID.StoneBlock);
            shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 0, 5);
            nextSlot++;

if (Main.hardMode)
{
	shop.item[nextSlot].SetDefaults(ItemID.PinkGel);
                shop.item[nextSlot].shopCustomPrice = Item.buyPrice(0, 0, 50, 0);
                nextSlot++;
}
		}



		// Only show health bar of the NPC when close to the player
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
float distance = NPC.Distance(Main.player[NPC.target].Center);
if (distance <= 200)
{
	if (distance > 100)
	{
		// Make the health bar become smaller the farther away the NPC is.
		scale *= (100 - (distance - 100)) / 100;
	}
	return null;
}
return false;
		}

		// Make the NPC invisible when far away from the player.
		public override void CustomBehavior(ref float ai)
		{
float distance = NPC.Distance(Main.player[NPC.target].Center);
if (distance <= 250)
{
	NPC.alpha = 100;
	if (distance > 100)
	{
		// Make the NPC fade out the farther away the NPC is.
		NPC.alpha += (int)(155 * ((distance - 100) / 150));
	}
	return;
}
NPC.alpha = 255;
		}

		// Make the NPC stop moving if it is close to the player.
		public override bool ShouldMove(float ai)
		{
NPC.ai[2] = 0; // Prevents the NPC from stopping following their target.
if (NPC.Distance(Main.player[NPC.target].Center) < 150f)
{
	NPC.velocity *= 0.95f;
	if (Math.Abs(NPC.velocity.X) < 0.1f)
	{
		NPC.spriteDirection = Main.player[NPC.target].Center.X > NPC.Center.X ? 1 : -1;
		NPC.velocity.X = 0;
	}
	return false;
}
return true;
		}
	}

	public class PurificationPowder : GlobalProjectile
	{
		// Make purification powder transform wraiths into purified ghosts.
		public override void PostAI(Projectile projectile)
		{
if (projectile.type != ProjectileID.PurificationPowder || Main.netMode == NetmodeID.MultiplayerClient)
{
	return;
}

for (int i = 0; i < Main.maxNPCs; i++)
{
	if (Main.npc[i].active && Main.npc[i].type == NPCID.Wraith && projectile.Hitbox.Intersects(Main.npc[i].Hitbox))
	{
		Main.npc[i].Transform(NPCType<MinerSoul>());
	}
}
		}
	}
}

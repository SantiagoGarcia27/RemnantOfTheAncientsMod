using System;
using opswordsII.Buffs;
using opswordsII.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.Enums;


namespace opswordsII
{

    public class Player1 : ModPlayer
	{
		public bool FrozenMinion;
		public bool DesertMinion;
		public bool SaplingMinion;
		public bool SaplingGoldMinion;
		public bool SaplingPlatinumMinion;
		public bool SaplingSilverMinion;
		public bool SaplingTungstenMinion;
		public bool StardustMinion;
		public bool StardustDragonV2Minion;
		public bool TyrantMinion;
		public bool qArenosa;
		public bool Hell_Fire;
		public bool hasInfernal_core;
		public int healHurt;
        public bool TortugaPet;
		public bool TwitchPet;
		public bool YtPet;
		public static bool Fmode;
		public bool ModPlayer = true;

		public override void ResetEffects() {
			qArenosa = false;
			Hell_Fire = false;
			healHurt = 0;
			TortugaPet = false;
			TwitchPet = false;
			YtPet = false;
			FrozenMinion = false;
			SaplingMinion = false;
			SaplingGoldMinion = false;
			SaplingPlatinumMinion = false;
			SaplingSilverMinion = false;
			SaplingTungstenMinion = false;
			StardustMinion = false;
			StardustDragonV2Minion = false;
			TyrantMinion = false;
			hasInfernal_core = false;
		}
		public override void UpdateDead() {
			qArenosa = false;
			Hell_Fire = false;
		}
     public override void UpdateBadLifeRegen() {
			if (qArenosa) {
				// These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
				if (Player.lifeRegen > 0) {
					Player.lifeRegen = 0;
				}
				Player.lifeRegenTime = 0;
				// lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
				Player.lifeRegen -= 16;
			}
			if (Hell_Fire) {
				// These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
				if (Player.lifeRegen > 0) {
					Player.lifeRegen = 0;
				}
				Player.lifeRegenTime = 0;
				// lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
				Player.lifeRegen -= 16;
			}
			if (healHurt > 0) {
				if (Player.lifeRegen > 0) {
					Player.lifeRegen = 0;
				}
				Player.lifeRegenTime = 0;
				Player.lifeRegen -= 120 * healHurt;
				}
	 }
			/*public override void OnEnterWorld(Player player) {
				Main.NewText("");
			}*/



					public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright) {
			if (qArenosa) {
				if (Main.rand.NextBool(4) && drawInfo.shadow == 0f) {
					int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, DustType<QuemaduraA>(), Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 3f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
					//drawInfo.Add(dust);		
				}
			}
			if (Hell_Fire) {
				if (Main.rand.NextBool(4) && drawInfo.shadow == 0f) {
					int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, DustType<Hell_Fire_P>(), Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 3f);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 1.8f;
					Main.dust[dust].velocity.Y -= 0.5f;
				//	Main.playerDrawDust.Add(dust);		
				}
			}
        }	
		   
            /*Check for accessory, probably don't need this if you have it in your acessory*/
           
            //Initialize bool for checking if accessory exists
            
		public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
		{
			if (hasInfernal_core && item.DamageType == DamageClass.Melee && !item.noMelee && !item.noUseGraphic) //Checks if the player has the flask buff and if the item is a melee weapon.
			{
				
				target.AddBuff(ModContent.BuffType<Hell_Fire>(), 300);
				//Main.NewText("true", Color.Orange);; //if the condition is true, then apply the ethereal flames debuff to the targeted NPC for 5 seconds.
			
			}else{
			//Main.NewText("false", Color.Orange);	
				

				
			}
			
		}	
		public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit) //This is the same as the one in OnHitNPC, but for melee projectiles.
		{
			if (hasInfernal_core && proj.DamageType == DamageClass.Melee) //Checks if the player has the flask buff and if the projectile is a melee Projectile.
			{
				target.AddBuff(ModContent.BuffType<Hell_Fire>(), 300); //if the condition is true, then apply the ethereal flames debuff to the targeted NPC for 5 seconds.
			}
		}
		/*public static void CalamityConvert(){
			Mod CalamityMod = ModLoader.GetMod("CalamityMod");
			if (CalamityMod != null){
			ModPlayer CalamityPlayer = player.GetModPlayer(CalamityMod, "CalamityPlayer");
			Type CalamityPlayerType = CalamityPlayer.GetType();
			FieldInfo rogueStealthMax = CalamityPlayerType.GetField("rogueStealthMax", BindingFlags.Instance | BindingFlags.Public);
			FieldInfo wearingRogueArmor = CalamityPlayerType.GetField("wearingRogueArmor", BindingFlags.Instance | BindingFlags.Public);
			int oldResource = (int)rogueStealthMax.GetValue(CalamityPlayer);
			int oldResource2 = (int)wearingRogueArmor.GetValue(CalamityPlayer);
			}
		}*/
	}
}
		
 	
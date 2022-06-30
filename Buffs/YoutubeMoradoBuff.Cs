using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using opswordsII.Projectiles.Pets;
using Terraria.DataStructures;

namespace opswordsII.Buffs
{
	public class YoutubeMoradoBuff : ModBuff
	{
		public override void SetStaticDefaults() {
			// DisplayName and Description are automatically set from the .lang files, but below is how it is done normally.
			// DisplayName.SetDefault("Paper Airplane");
			// Description.SetDefault("\"Let this pet be an example to you!\"");
			Main.buffNoTimeDisplay[Type] = true;
			Main.vanityPet[Type] = true;
			 DisplayName.SetDefault("Twitch");
			 Description.SetDefault("Thanks to -febo1apolo -eltonyhelou -Jormanas_gt"
			 +"\n -LaGalletaOtaku -licenciado_carlos -Millogp -JMessiahs -starlord101094"
			 +"\n -RBTTIM -Arvid_7 -Marcusfenix8593 -vitocox411 -zero-greyhound"
			 +"\n -Vic38878 -SudsierBard -perrito_coca -Dialex1205 -Josesillo606"
			 +"\n -gaonawithg -Samuchamulol -Chuckysin -PAT0MALO -perrito_coca -Frikiceoo"
			+"\n -Marqui020 -ScarletMoonGA -tomasgrox -Mr_Scolex -Jhotx_24 -natillon -R3V4NTV"
			+"\n -Rockerheadd -mateoamii -JuanitoLive -DDS0210 -charlestwi -zakzar01 -Wuchang234"
			+"\n -WHYonex -zun32 -bickelo -sakarok4 -Mr_D2D2 -Brunix_117 -kire -LixFTO -tuamigojoker1234"
			+"\n -Juanoma2912 -mrbelieved -musytari1 -Mau518 -The_Manquequito -¡RazorOnUwU -1v1SinCamisa"
			+"\n -JoseRoyalty_ -TheMalk11 -kevinprogame06 -omarshdio -gamerhuefe -Andrucastell -Fernastors"
			+"\n -LBJ_XD -d4rckn3 -ommyalolok0 -yellowpop -wirytiox -Johy315 -IVasher -poshosupremo -elmizu"
			 +"\n for testing my mod ");
		}

		public override void Update(Player player, ref int buffIndex)
		{ // This method gets called every frame your buff is active on your player.
			player.buffTime[buffIndex] = 18000;

			int projType = ModContent.ProjectileType<TwitchPet>();

			// If the player is local, and there hasn't been a pet projectile spawned yet - spawn it.
			if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[projType] <= 0)
			{
				Projectile.NewProjectile(player.GetSource_Buff(buffIndex),player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ProjectileType<Projectiles.Pets.TwitchPet>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
} 
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using RemnantOfTheAncientsMod.Projectiles.Pets;
using Terraria.Localization;

namespace RemnantOfTheAncientsMod.Buffs
{
	public class YoutubeMoradoBuff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[Type] = true;
			Main.lightPet[Type] = true;

			string names = "-febo1apolo - eltonyhelou - Jormanas_gt \n -LaGalletaOtaku -licenciado_carlos -Millogp -JMessiahs -starlord101094 \n -RBTTIM -Arvid_7 -Marcusfenix8593 -vitocox411 -zero-greyhound"
			+ "\n-Vic38878 -SudsierBard -perrito_coca -Dialex1205 -Josesillo606 \n -gaonawithg -Samuchamulol -Chuckysin -PAT0MALO -perrito_coca -Frikiceoo \n -Marqui020 -ScarletMoonGA -tomasgrox -Mr_Scolex -Jhotx_24 -natillon -R3V4NTV"
			+ "\n-Rockerheadd -mateoamii -JuanitoLive -DDS0210 -charlestwi -zakzar01 -Wuchang234 \n -WHYonex -zun32 -bickelo -sakarok4 -Mr_D2D2 -Brunix_117 -kire -LixFTO -tuamigojoker1234 \n -Juanoma2912 -mrbelieved -musytari1 -Mau518 -The_Manquequito -¡RazorOnUwU -1v1SinCamisa"
			+ "\n-JoseRoyalty_ -TheMalk11 -kevinprogame06 -omarshdio -gamerhuefe -Andrucastell -Fernastors \n -LBJ_XD -d4rckn3 -ommyalolok0 -yellowpop -wirytiox -Johy315 -IVasher -poshosupremo -elmizu -StillJuno";
            //Main.vanityPet[Type] = true;
            DisplayName.SetDefault("Twitch");
			Description.SetDefault($"Many thanks to {names}\n for being the first to give this mod a chance");
			Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.French), $"Un grand merci à {names}\n d'avoir été le premier à donner une chance à ce mod");
            Description.AddTranslation(GameCulture.FromCultureName(GameCulture.CultureName.Spanish), $"Muchas gracias a {names}\n por ser los primeros en darle una oportunidad a este mod.");
        }

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 18000;
			if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[ProjectileType<TwitchPet>()] <= 0)
			{
				Projectile.NewProjectile(player.GetSource_Buff(buffIndex), player.position.X + player.width / 2, player.position.Y + player.height / 2, 0f, 0f, ProjectileType<TwitchPet>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}
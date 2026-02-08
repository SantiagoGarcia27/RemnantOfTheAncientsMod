using RemnantOfTheAncientsMod.Common.Global.Items;
using RemnantOfTheAncientsMod.Common.Global.NPCs;
using System.Linq;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ExampleMod.Common.Commands
{
	public class MaxStyleCommand : ModCommand
	{
		public static LocalizedText DescriptionText { get; private set; }

		public override void SetStaticDefaults() {
			DescriptionText = Mod.GetLocalization($"Commands.{nameof(MaxStyleCommand)}.Description");
		}

		// CommandType.Chat means that command can be used in Chat in SP and MP
		public override CommandType Type
			=> CommandType.Chat;

		// The desired text to trigger this command
		public override string Command
			=> "maxStyle";

		// A short description of this command
		public override string Description
			=> DescriptionText.Value;

		public override void Action(CommandCaller caller, string input, string[] args) {
			string text = "";
            foreach (int id in ModifyAccsesories.mayoresEstiloArmadurasID)
            {
                text += $" [i:{id}]";
            }
            foreach (int id in ModifyAccsesories.mayoresEstiloAccesoriosID)
			{
				text += $" [i:{id}]";
			}

			Main.NewText("Max Style: "+ text);

        }
	}
}
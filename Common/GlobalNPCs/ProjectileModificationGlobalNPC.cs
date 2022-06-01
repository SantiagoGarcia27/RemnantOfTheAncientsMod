﻿using Terraria.ModLoader;

namespace opswordsII.Common.GlobalNPCs
{
	// This is a class for functionality related to ExampleProjectileModifications.
	public class ProjectileModificationGlobalNPC : GlobalNPC {
		public override bool InstancePerEntity => true;
		//public override bool CloneNewInstances => true;
		public int timesHitByModifiedProjectiles;
	}
}

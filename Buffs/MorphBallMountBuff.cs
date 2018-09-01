using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace MetroidMod.Buffs
{
	public class MorphBallMountBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Morph Ball");
			Description.SetDefault("Because you apparently can't crawl");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
			Main.debuff[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.mount.SetMount(mod.MountType<Mounts.MorphBallMount>(), player);
			player.buffTime[buffIndex] = 10;
		}
	}
}
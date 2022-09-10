﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MetroidMod.Common.Systems;

namespace MetroidMod.Content.Tiles
{
	public class ChozoBrickNatural : ModTile
	{
		public override string Texture => $"{nameof(MetroidMod)}/Content/Tiles/ChozoBrick";
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			//Main.tileDungeon[Type] = true;
			//Main.tileMerge[Type][TileID.Sand] = true;
			//Main.tileMerge[TileID.Sand][Type] = true;

			DustType = 87;
			MinPick = 65;
			HitSound = SoundID.Tink;
			ItemDrop = ModContent.ItemType<Items.Tiles.ChozoBrick>();

			AddMapEntry(new Color(200, 160, 72));
		}

		public override bool CanExplode(int i, int j)
		{
			return MSystem.bossesDown.HasFlag(MetroidBossDown.downedTorizo);
		}

		public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
		{
			if (!MSystem.bossesDown.HasFlag(MetroidBossDown.downedTorizo) && !WorldGen.generatingWorld)
			{
				fail = true;
			}
			base.KillTile(i, j, ref fail, ref effectOnly, ref noItem);
		}
	}
}

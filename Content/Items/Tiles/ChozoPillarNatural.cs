﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MetroidMod.Content.Items.Tiles
{
	public class ChozoPillarNatural : ModItem
	{
		public override string Texture => $"{nameof(MetroidMod)}/Content/Items/Tiles/ChozoPillar";
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chozite Pillar (Natural)");

			SacrificeTotal = 100;
		}
		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.maxStack = 999;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.createTile = ModContent.TileType<Content.Tiles.ChozoPillarNatural>();
		}
	}
}
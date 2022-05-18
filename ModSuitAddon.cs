﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using MetroidModPorted.Default;
using MetroidModPorted.ID;

namespace MetroidModPorted
{
	public abstract class ModSuitAddon : ModType
	{
		public int Type { get; private set; }
		internal void ChangeType(int type) => Type = type;
		/// <summary>
		/// The <see cref="ModItem"/> this addon controls.
		/// </summary>
		public ModItem ModItem;
		/// <summary>
		/// The <see cref="ModTile"/> this addon controls.
		/// </summary>
		public ModTile ModTile;
		/// <summary>
		/// The <see cref="Item"/> this addon controls.
		/// </summary>
		public Item Item;
		public int ItemType { get; internal set; }
		public int TileType { get; internal set; }

		/// <summary>
		/// The translations for the display name of this item.
		/// </summary>
		public ModTranslation DisplayName { get; internal set; }

		/// <summary>
		/// The translations for the tooltip of this item.
		/// </summary>
		public ModTranslation Tooltip { get; internal set; }

		public abstract string ItemTexture { get; }

		public virtual string ArmorTextureHead { get; set; }
		public virtual string ArmorTextureTorso { get; set; }
		public virtual string ArmorTextureArmsGlow { get; set; }
		public virtual string ArmorTextureLegs { get; set; }

		public abstract string TileTexture { get; }

		public abstract bool AddOnlyAddonItem { get; }

		public int SacrificeTotal { get; set; } = 1;

		public bool ItemNameLiteral { get; set; } = true;

		public virtual int AddonSlot { get; set; } = SuitAddonSlotID.None;
		internal bool IsArmor => ArmorTextureHead != null && ArmorTextureHead != "" && ArmorTextureTorso != null && ArmorTextureTorso != "" && ArmorTextureLegs != null && ArmorTextureLegs != "";
		public string GetAddonSlotName() => SuitAddonLoader.GetAddonSlotName(AddonSlot);
		/// <summary>
		/// Determines if the addon can generate on Chozo Statues during world generation.
		/// </summary>
		public virtual bool CanGenerateOnChozoStatue(Tile tile) => false;
		public override sealed void SetupContent()
		{
			SetStaticDefaults();
			ModItem.SetStaticDefaults();
			SetupDrawing();
		}
		public override void Load()
		{
			ModItem = new SuitAddonItem(this);
			ModTile = new SuitAddonTile(this);
			if (ModItem == null) { throw new Exception("WTF happened here? SuitAddonItem is null!"); }
			if (ModTile == null) { throw new Exception("WTF happened here? SuitAddonTile is null!"); }
			Mod.AddContent(ModItem);
			Mod.AddContent(ModTile);
			if (IsArmor && Main.netMode != NetmodeID.Server)
			{
				Mod.AddEquipTexture(ModItem, EquipType.Head, ArmorTextureHead);
				Mod.AddEquipTexture(ModItem, EquipType.Body, ArmorTextureTorso);
				Mod.AddEquipTexture(ModItem, EquipType.Legs, ArmorTextureLegs);
			}
		}

		private void SetupDrawing()
		{
			if (Main.netMode == NetmodeID.Server || !IsArmor) { return; }
			int equipSlotHead = Mod.GetEquipSlot(ModItem.Name, EquipType.Head);
			int equipSlotBody = Mod.GetEquipSlot(ModItem.Name, EquipType.Body);
			int equipSlotLegs = Mod.GetEquipSlot(ModItem.Name, EquipType.Legs);

			ArmorIDs.Head.Sets.DrawHead[equipSlotHead] = false;
			ArmorIDs.Body.Sets.HidesTopSkin[equipSlotBody] = true;
			ArmorIDs.Body.Sets.HidesArms[equipSlotBody] = true;
			ArmorIDs.Legs.Sets.HidesBottomSkin[equipSlotLegs] = true;
		}

		protected override sealed void Register()
		{
			DisplayName = LocalizationLoader.CreateTranslation(Mod, $"SuitAddonName.{Name}");
			Tooltip = LocalizationLoader.CreateTranslation(Mod, $"SuitAddonTooltip.{Name}");
			if (!AddOnlyAddonItem)
			{
				Type = SuitAddonLoader.AddonCount;
				if (Type > 127)
				{
					throw new Exception("Suit Addons Limit Reached. (Max: 128)");
				}
				SuitAddonLoader.addons.Add(this);
			}
			Mod.Logger.Info("Register new Suit Addon: " + FullName + ", OnlyAddonItem: " + AddOnlyAddonItem);
		}

		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
		}

		/// <inheritdoc cref="ModItem.SetDefaults()"/>
		public virtual void SetItemDefaults(Item item) { }

		/// <inheritdoc cref="ModItem.UpdateAccessory(Player, bool)"/>
		public virtual void UpdateAccessory(Player player, bool hideVisual) { UpdateInventory(player); }

		/// <inheritdoc cref="ModItem.UpdateInventory(Player)"/>
		public virtual void UpdateInventory(Player player) { }

		/// <inheritdoc cref="ModItem.UpdateArmorSet(Player)"/>
		public virtual void OnUpdateArmorSet(Player player, int stack) { }

		/// <inheritdoc cref="ModItem.UpdateVanitySet(Player)"/>
		public virtual void OnUpdateVanitySet(Player player) { }

		/// <inheritdoc cref="ModItem.ArmorSetShadows(Player)"/>
		public virtual void ArmorSetShadows(Player player) { }

		public virtual string GetSetBonusText()
		{
			return null;
		}

		/// <inheritdoc cref="ModItem.AddRecipes"/>
		public virtual void AddRecipes() { }

		public Recipe CreateRecipe(int amount = 1) => ModItem.CreateRecipe(amount);
	}
}

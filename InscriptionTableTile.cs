using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace NT.Items.Jutsu
{
    internal class InscriptionTableTile : ModTile
    {
		public override void SetStaticDefaults()
		{
			// Properties
			Main.tileSolidTop[Type] = false;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			Main.tileFrameImportant[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;
			TileID.Sets.IgnoredByNpcStepUp[Type] = true; // This line makes NPCs not try to step up this tile during their movement. Only use this for furniture with solid tops.

			DustType = DustID.WoodFurniture;
			AdjTiles = new int[] { TileID.WorkBenches };

			// Placement
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			//TileObjectData.newTile.CoordinateHeights = new[] { 54, 34 };
			TileObjectData.addTile(Type);

			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);

			// Etc
			AddMapEntry(new Color(200, 200, 200), Language.GetText("Inscription Table"));
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 54, 34, ModContent.ItemType<Jutsu.InscriptionTable>());
		}

		public override void NumDust(int x, int y, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

	}
}

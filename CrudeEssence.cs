using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;

namespace NT.Items.Essences
{
    public class CrudeEssence : ModItem
    {

        public override void SetStaticDefaults()
        {
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
            ItemID.Sets.ItemIconPulse[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 4));

        }

        public override void SetDefaults()
        {
            Item refItem = new Item();
            refItem.SetDefaults(ItemID.SoulofSight);
            Item.width = refItem.width;
            Item.height = refItem.height;
            Item.maxStack = 999;
            Item.value = Item.sellPrice(copper: 20); // How many coins the item is worth
            Item.rare = ItemRarityID.Blue;
        }

    }

    public class CrudeEssenceNPC : GlobalNPC
    {
        public override void ModifyGlobalLoot(GlobalLoot globalLoot)
        {
            
            // This is where we add global rules for all NPC. Here is a simple example:
            globalLoot.Add(ItemDropRule.Common(ModContent.ItemType<CrudeEssence>(), 10, 1, 2));

        }
    }

    

}

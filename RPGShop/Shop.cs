using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace RPGShop
{
    class Shop
    {
        private int _gold;
        private Item[] _inventory;

        public Item[] Inventory
        {
            get { return _inventory; }
        }

        public Shop(Item[] shopInventory)
        {
            _inventory = shopInventory;
        }

        /// <summary>
        /// Looks to see if a player has enough gold to buy an item.
        /// </summary>
        /// <param name="player"> The player attempting to buy the item. </param>
        /// <param name="gold"> The cost of the item. </param>
        /// <returns> Whether or not the player has enough gold for the item. </returns>
        public bool Sell(Player player, int gold)
        {
            if(player.Gold < gold)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string[] GetItemNames()
        {
            string[] itemList = new string[_inventory.Length];

            for(int i = 0; i < _inventory.Length; i++)
            {
                itemList[i] = (i + 1) + ". " + _inventory[i].Name + " - " + _inventory[i].Cost + "GP";
            }

            return itemList;
        }
    }
}

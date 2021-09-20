using System;
using System.Collections.Generic;
using System.Text;

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

        public bool Sell(Player player, int gold)
        {

        }

        public string[] GetItemNames()
        {

        }
    }
}

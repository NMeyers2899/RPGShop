using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace RPGShop
{
    class Player
    {
        private int _gold;
        private Item[] _inventory;

        public int Gold
        {
            get { return _gold; }
        }

        public Item[] Inventory
        {
            get { return _inventory; }
        }

        public Player(int gold)
        {
            _gold = gold;
        }

        public void Buy(Item item)
        {

        }

        public string[] GetItemNames()
        {

        }

        public void Save()
        {

        }

        public bool Load()
        {

        }
    }
}

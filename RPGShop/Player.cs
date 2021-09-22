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

            _inventory = new Item[1];
        }

        /// <summary>
        /// The player will attempt to buy an item.
        /// </summary>
        /// <param name="item"> The item that the player wishes to buy. </param>
        public void Buy(Item item)
        {
            // Creates a copy of the players inventory with a new space to add the item to.
            Item[] newPlayerInventory = new Item[_inventory.Length + 1];

            // Creates a new shop in order to sell the item.
            Shop shop = new Shop(_inventory);

            // Checks to see if the shop can sell the item...
            if (!shop.Sell(this, item.Cost))
            {
                // ...if it can't, it tells the player that they do not have enough gold for it.
                Console.WriteLine("Sorry friend, I do not go lower in my prices.");
                Console.ReadKey(true);
                Console.Clear();
            }
            else
            {
                // ...if it can, it tells the player they purchased the item.
                Console.WriteLine("You purchased " + item.Name + "!");

                // Adds each item from the player's inventory to the new array.
                for (int i = 0; i < _inventory.Length; i++)
                {
                    newPlayerInventory[i] = _inventory[i];
                }

                // Subtracts the item's cost from the player's gold.
                _gold -= item.Cost;

                // Appends the new item to the end of the player's inventory.
                newPlayerInventory[_inventory.Length] = item;

                // Sets the old inventory equal to the new one so it adds the new item.
                _inventory = newPlayerInventory;

                Console.ReadKey(true);
                Console.Clear();
            }
        }

        // Gets the names of the items in the player's inventory.
        public string[] GetItemNames()
        {
            string[] itemList = new string[_inventory.Length];

            for (int i = 0; i < _inventory.Length; i++)
            {
                itemList[i] = _inventory[i].Name;
            }

            return itemList;
        }

        /// <summary>
        /// Saves the player's current gold and inventory.
        /// </summary>
        /// <param name="writer"> What writes to the file. </param>
        public void Save(StreamWriter writer)
        {
            writer.WriteLine(_gold);

            for(int i = 0; i < _inventory.Length; i++)
            {
                writer.WriteLine(_inventory[i].Name);
            }
        }

        /// <summary>
        /// Loads a player's gold and inventory.
        /// </summary>
        /// <param name="reader"> What reads the file. </param>
        /// <returns></returns>
        public bool Load(StreamReader reader)
        { 
            if(!int.TryParse(reader.ReadLine(), out _gold))
            {
                return false;
            }

            int i = 0;

            while(!(reader.ReadLine() == null))
            {
                i++;
            }

            return true;
        }
    }
}

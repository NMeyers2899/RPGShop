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

        public Player(int gold)
        {
            _gold = gold;

            _inventory = new Item[0];
        }

        /// <summary>
        /// The player will attempt to buy an item. If they can, their gold will decrement by the item's cost
        /// and the item is added to their inventory.
        /// </summary>
        /// <param name="item"> The item that the player wishes to buy. </param>
        public void Buy(Item item)
        {
            // Creates a copy of the players inventory with a new space to add the item to.
            Item[] newPlayerInventory = new Item[_inventory.Length + 1];
                
            // Tells the player they purchased the item.
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

            writer.WriteLine(_inventory.Length);

            for(int i = 0; i < _inventory.Length; i++)
            {
                writer.WriteLine(_inventory[i].Name);
                writer.WriteLine(_inventory[i].Cost);
            }
        }

        /// <summary>
        /// Loads a player's gold and inventory.
        /// </summary>
        /// <param name="reader"> What reads the file. </param>
        /// <returns> Whether or not the loading could be completed. </returns>
        public bool Load(StreamReader reader)
        { 
            // Checks to see if the next line is a number, if it is it set the player's gold to it. If it can't...
            if(!int.TryParse(reader.ReadLine(), out _gold))
            {
                // ...it returns false.
                return false;
            }

            int inventoryLength = 0;

            // Checks to see if the next line is a number, if it is set the length of the player's inventory
            // equal to it. If it cannot...
            if(!int.TryParse(reader.ReadLine(), out inventoryLength))
            {
                // ...it returns false.
                return false;
            }

            // Sets the inventory length equal to the number read.
            _inventory = new Item[inventoryLength];

            int i = 0;

            while(!(reader.EndOfStream))
            {
                _inventory[i].Name = reader.ReadLine();

                if(!int.TryParse(reader.ReadLine(), out _inventory[i].Cost))
                {
                    return false;
                }

                i++;
            }

            return true;
        }
    }
}

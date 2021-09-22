using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace RPGShop
{
    struct Item
    {
        public int Cost;
        public string Name;
    }

    class Game
    {
        private Player _player;
        private Shop _shop;
        private bool _gameOver = false;
        private int _currentScene = 0;

        /// <summary>
        /// Runs the main game loop.
        /// </summary>
        public void Run()
        {
            Start();

            while (!_gameOver)
            {
                Update();
            }

            End();
        }

        /// <summary>
        /// Is called at the start of the game, initializing everything the code needs.
        /// </summary>
        private void Start()
        {
            // Initalizes the items and creates a new shop with those items in it.
            InitializeItems();
        }

        /// <summary>
        /// Updates the current scene.
        /// </summary>
        private void Update()
        {
            DisplayCurrentScene();
        }

        /// <summary>
        /// Is called when the game ends.
        /// </summary>
        private void End()
        {
            Console.WriteLine("Come again, friend!");
        }

        private void InitializeItems()
        {
            // Initializes the player with 100 gold.
            _player = new Player(100);

            // Initializes every item in the shop.
            Item healthPotion = new Item { Name = "Health Potion", Cost = 22 };

            Item bigStick = new Item { Name = "Big Stick", Cost = 2 };

            Item bigShield = new Item { Name = "Big Shield", Cost = 26 };

            Item noveltyStatue = new Item { Name = "Novelty Statue", Cost = 6 };

            Item wompusPlushy = new Item { Name = "Wompus Plushy", Cost = 12 };

            Item freshJs = new Item { Name = "Fresh J's", Cost = 53 };

            // Puts every item in the list.
            Item[] itemList = new Item[] { healthPotion, bigStick, bigShield, noveltyStatue, wompusPlushy, freshJs };

            // Constructs a new shop with the item list as its inventory.
            _shop = new Shop(itemList);
        }

        /// <summary>
        /// Gets an input from the player based on some given decision
        /// </summary>
        /// <param name="description"> The context for the input </param>
        /// <param name="options"> The options given to the player. </param>
        /// <returns> The users input of a given choice. </returns>
        private int GetInput(string description, params string[] options)
        {
            string input = "";
            int inputRecieved = -1;

            while (inputRecieved == -1)
            {
                // Print out all options.
                Console.WriteLine(description);
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine((i + 1) + ". " + options[i]);
                }
                Console.Write("> ");

                input = Console.ReadLine();

                // If a player typed an int...
                if (int.TryParse(input, out inputRecieved))
                {
                    // ...decrement the input and check if it's within bounds of the array.
                    inputRecieved--;
                    if (inputRecieved < 0 || inputRecieved >= options.Length)
                    {
                        // Sets inputRecieved to the default value.
                        inputRecieved = -1;
                        //Display error message.
                        Console.WriteLine("Invalid Input");
                        Console.ReadKey(true);
                    }
                }
                // If the user didn't type an int.
                else
                {
                    // Sets inputRecieved to the default value.
                    inputRecieved = -1;
                    //Display error message.
                    Console.WriteLine("Invalid Input");
                    Console.ReadKey(true);
                }

                Console.Clear();
            }

            return inputRecieved;
        }

        /// <summary>
        /// Saves the players inventory and how much gold they currently have.
        /// </summary>
        private void Save()
        {
            // Creates the save file for the game.
            StreamWriter writer = new StreamWriter("SaveData.txt");

            // Saves the player's information.
            _player.Save(writer);

            // Closes the file.
            writer.Close();
        }

        /// <summary>
        /// Loads a previous save.
        /// </summary>
        /// <returns> If the player can load the game or not. </returns>
        private bool Load()
        { 
            // If the file doesn't exist...
            if (!File.Exists("SaveData.txt"))
            {
                // ...it sets loadingSuccessful to false.
                return false;
            }

            // Creates a reader.
            StreamReader reader = new StreamReader("SaveData.txt");

            // Loads the player's gold and their previous inventory.
            if (!_player.Load(reader))
            {
                return false;
            }

            //Closes the file.
            reader.Close();

            // Returns loadingSuccessful.
            return true;
        }

        /// <summary>
        /// Finds the current scene and displays it to the player.
        /// </summary>
        private void DisplayCurrentScene()
        {
            switch (_currentScene)
            {
                // Displays the opening menu where the player can either start a new game or load a previous save.
                case 0:
                    DisplayOpeningMenu();
                    break;
                // Displays the shop menu where a player can buy items, save, or quit.
                case 1:
                    DisplayShopMenu();
                    break;
            }
        }

        /// <summary>
        /// Allows the player to either start a new game or load a previous save.
        /// </summary>
        private void DisplayOpeningMenu()
        {
            // Gives the player the option to either start shopping, or to load their previous inventory.
            int choice = GetInput("Welcome to my shop! You've come to browse, yes?", "Start Shopping",
                "Load Inventory");

            switch (choice)
            {
                // The player starts a new game.
                case 0:
                    Console.WriteLine("Come! Have a look around!");
                    Console.ReadKey(true);
                    Console.Clear();
                    _currentScene = 1;
                    break;
                    // The player attempts to load their previous inventory.
                case 1:
                    Console.WriteLine("A previous customer, eh? Let me see.");
                    Console.ReadKey(true);
                    // If they can't the game brings them to the opening menu.
                    if (!Load())
                    {
                        Console.WriteLine("You have no record here. But come, shop away!");
                        Console.ReadKey(true);
                        Console.Clear();
                        return;
                    }
                    // If they can, the player's previous inventory and gold are loaded and they are taken into
                    // the shop.
                    else
                    {
                        Console.WriteLine("Welcome back to the store, friend!");
                        Console.ReadKey(true);
                        Console.Clear();
                        _currentScene = 1;
                    }
                    break;
            }
        }

        /// <summary>
        /// Gets the item names from the shop and adds a save and quit option.
        /// </summary>
        /// <returns> The string for the menu options. </returns>
        private string[] GetShopMenuOptions()
        {
            // Grabs the item names and their costs from the shop.
            string[] shopItems = _shop.GetItemNames();

            // Creates a new array that will append the save and quit feature to the items.
            string[] shopOptions = new string[shopItems.Length + 2];

            // Sets all of the items as menu options.
            for (int i = 0; i < shopItems.Length; i++)
            {
                shopOptions[i] = shopItems[i];
            }

            // Appends the new options to the item list from the shop.
            shopOptions[shopItems.Length] = "Save Game";
            shopOptions[shopItems.Length + 1] = "Quit Game";

            // Returns the new list.
            return shopOptions;
        }

        /// <summary>
        /// Displays the shop menu to the player, allowing them to buy the items and save or quit.
        /// </summary>
        private void DisplayShopMenu()
        {
            string[] playerInventory = _player.GetItemNames();

            // Displays the player's gold and items to the screen.
            Console.WriteLine("Your Gold: " + _player.Gold);
            Console.WriteLine("Your Inventory: ");
            for(int i = 0; i < playerInventory.Length; i++)
            {
                Console.WriteLine(playerInventory[i]);
            }

            Console.WriteLine();

            // Gets the menu options from the get function.
            string[] menuOptions = GetShopMenuOptions();

            // Asks the player which item they would like to buy, and if they would like to save or quit.
            int choice = GetInput("What will you be buying?", menuOptions);

            // If the player picks...
            if (choice == menuOptions.Length - 1)
            {
                _gameOver = true;
            }
            else if (choice == menuOptions.Length - 2)
            {
                Console.WriteLine("I'll keep these here for now.");
                Console.ReadKey(true);
                Console.Clear();
                Save();
            }
            else if(!_shop.Sell(_player, choice))
            {
                Console.WriteLine("Very sorry. I do not go lower in my prices.");
                Console.ReadKey(true);
                Console.Clear();
            }
        }
    }
}

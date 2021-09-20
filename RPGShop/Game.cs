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
        private int __currentScene = 0;

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

        }

        /// <summary>
        /// Loads a previous save.
        /// </summary>
        /// <returns> If the player can load the game or not. </returns>
        private bool Load()
        {

        }

        private void DisplayCurrentScene()
        {
            switch (__currentScene)
            {
                case 0:
                    DisplayOpeningMenu();
                    break;
            }
        }

        private void DisplayOpeningMenu()
        {
            int choice = GetInput("Welcome to my shop! You've come to browse, yes?", "Start Shopping",
                "Load Inventory");

            switch (choice)
            {
                case 0:
                    Console.WriteLine("Come! Have a look around!");
                    Console.ReadKey(true);
                    Console.Clear();
                    break;
                case 1:
                    Console.WriteLine("A previous customer, eh? Let me see.");
                    Console.ReadKey(true);
                    if (!Load())
                    {
                        Console.WriteLine("You have no record here. But come, shop away!");
                        Console.ReadKey(true);
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("Welcome back to the store, friend!");
                        Console.ReadKey(true);
                        Console.Clear();
                    }
                    break;
            }
        }

        private string[] GetShopMenuOptions()
        {

        }

        private void DisplayShopMenu()
        {

        }
    }
}

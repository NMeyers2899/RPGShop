using System;
using System.Collections.Generic;
using System.Text;

namespace RPGShop
{
    public enum Scene
    {
        
    }

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
        private int _currentScene;

        public void Run()
        {
            Start();

            while (!_gameOver)
            {
                Update();
            }

            End();
        }

        private void Start()
        {

        }

        private void Update()
        {

        }

        private void End()
        {

        }

        private void InitializeItems()
        {

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

        private void Save()
        {

        }

        private bool Load()
        {

        }

        private void DisplayCurrentScene()
        {

        }

        private void DisplayOpeningMenu()
        {

        }

        private string[] GetShopMenuOptions()
        {

        }

        private void DisplayShopMenu()
        {

        }
    }
}

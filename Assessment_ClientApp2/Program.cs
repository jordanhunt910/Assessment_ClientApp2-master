using System;
using System.Collections.Generic;
using System.IO;

namespace Assessment_ClientApp2
{

    // **************************************************
    //
    // Assessment: Client App 2.0
    // Author: Jordan Hunt
    // Dated: 30 November 2019
    // Level (Novice, Apprentice, or Master): Master
    //
    // **************************************************    

    class Program
    {
        /// <summary>
        /// Main method - app starts here
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //
            // initialize monster list from method
            //
            List<Monster> monsters = ReadFromDataFile();

            //
            // read monsters from data file
            //
            //List<Monster> monsters = ReadFromDataFile();

            //
            // application flow
            //
            DisplayWelcomeScreen();
            DisplayMenuScreen(monsters);
            DisplayClosingScreen();
        }

        #region UTILITY METHODS

        /// <summary>
        /// initialize a list of monsters
        /// </summary>
        /// <returns>list of monsters</returns>
        static List<Monster> InitializeMonsterList()
        {
            //
            // create a list of monsters
            // note: list and object initializers used
            //
            List<Monster> monsters = new List<Monster>()
            {

                new Monster()
                {
                    Name = "Sid",
                    Age = 145,
                    Attitude = Monster.EmotionalState.happy,
                    Tribe = Monster.MonsterTribe.giant,
                    Active = true
                },

                new Monster()
                {
                    Name = "Lucy",
                    Age = 125,
                    Attitude = Monster.EmotionalState.bored,
                    Tribe = Monster.MonsterTribe.dragon,
                    Active = true
                },

                new Monster()
                {
                    Name = "Bill",
                    Age = 934,
                    Attitude = Monster.EmotionalState.sad,
                    Tribe = Monster.MonsterTribe.orc,
                    Active = true
                },

                new Monster()
                {
                    Name = "Steven",
                    Age = 625,
                    Attitude = Monster.EmotionalState.angry,
                    Tribe = Monster.MonsterTribe.werewolf,
                    Active = true
                },
                new Monster()
                {
                    Name = "Georgina",
                    Age = 25,
                    Attitude = Monster.EmotionalState.bored,
                    Tribe = Monster.MonsterTribe.yeti,
                    Active = true
                }


            };

            return monsters;
        }

        #endregion

        #region SCREEN DISPLAY METHODS

        /// <summary>
        /// SCREEN: display and process menu options
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayMenuScreen(List<Monster> monsters)
        {
            bool quitApplication = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) List All Monsters");
                Console.WriteLine("\tb) View Monster Detail");
                Console.WriteLine("\tc) Add Monster");
                Console.WriteLine("\td) Delete Monster");
                Console.WriteLine("\te) Update Monster");
                Console.WriteLine("\tf) Write Monsters to File");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayAllMonsters(monsters);
                        break;

                    case "b":
                        DisplayViewMonsterDetail(monsters);
                        break;

                    case "c":
                        DisplayAddMonster(monsters);
                        break;

                    case "d":
                        DisplayDeleteMonster(monsters);
                        break;

                    case "e":
                        DisplayUpdateMonster(monsters);
                        break;

                    case "f":
                        DisplayWriteToDataFile(monsters);
                        break;
                        
                    case "q":
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("Please enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }
            } while (!quitApplication);
        }

        /// <summary>
        /// SCREEN: list all monsters
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayAllMonsters(List<Monster> monsters)
        {
            DisplayScreenHeader("All Monsters");
            Console.Write($"Name".PadRight(10));
            Console.Write($"Age".PadRight(5));
            Console.Write($"Attitude".PadRight(10));
            Console.Write($"Tribe".PadRight(10));
            Console.Write("Greeting".PadRight(50));
            Console.WriteLine("Status".PadLeft(20));
            Console.WriteLine("************************************************************" +
                "**************************************************");
            foreach (Monster monster in monsters)
            {
                if (monster.Active)
                {
                    MonsterInfo(monster);
                    Console.WriteLine();
                }
                
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// SCREEN: monster detail
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayViewMonsterDetail(List<Monster> monsters)
        {
            DisplayScreenHeader("Monster Detail");

            //
            // display all monster names
            //
            Console.WriteLine("\tMonster Names");
            Console.WriteLine("\t-------------");
            foreach (Monster monster in monsters)
            {
                Console.WriteLine("\t" + monster.Name);
            }

            //
            // get user monster choice
            //
            Console.WriteLine();
            Console.Write("\tEnter name:");
            string monsterName = Console.ReadLine();

            //
            // get monster object
            //
            Monster selectedMonster = null;
            foreach (Monster monster in monsters)
            {
                if (monster.Name == monsterName)
                {
                    selectedMonster = monster;
                    break;
                }
            }

            //
            // display monster detail
            //
            Console.WriteLine();
            DisplayScreenHeader("All Monsters");
            Console.Write($"Name".PadRight(10));
            Console.Write($"Age".PadRight(5));
            Console.Write($"Attitude".PadRight(10));
            Console.Write($"Tribe".PadRight(10));
            Console.Write("Greeting".PadRight(50));
            Console.WriteLine("Status".PadLeft(20));
            Console.WriteLine("************************************************************" +
                "**************************************************");
            MonsterInfo(selectedMonster);
            

            DisplayContinuePrompt();
        }

        /// <summary>
        /// SCREEN: add a monster
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayAddMonster(List<Monster> monsters)
        {
            Monster newMonster = new Monster();

            DisplayScreenHeader("Add Monster");

            //
            // add monster object property values
            //
            Console.Write("\tName: ");
            newMonster.Name = Console.ReadLine();
            bool validAge = false;
            do
            {
                Console.Write("\tAge: ");
                validAge = int.TryParse(Console.ReadLine(), out int age);
                newMonster.Age = age;

            } while (!validAge);
            

            bool validChoice = false;
            do
            {
                Console.Write("\tAttitude: ");
                validChoice = Enum.TryParse(Console.ReadLine(), out Monster.EmotionalState attitude);
                newMonster.Attitude = attitude;
                if (!validChoice)
                {
                    Console.WriteLine("Invalid attitude. Please select from the following: ");
                    foreach (Monster.EmotionalState MonsterAttitude in Enum.GetValues(typeof(Monster.EmotionalState)))
                    {
                        Console.WriteLine(MonsterAttitude);
                    }
                }
            } while (!validChoice);



            do
            {
                Console.Write("\tTribe: ");
                validChoice = Enum.TryParse(Console.ReadLine(), out Monster.MonsterTribe tribe);
                newMonster.Tribe = tribe;
                if (!validChoice)
                {
                    Console.WriteLine("Invalid tribe. Please select from the following: ");
                    foreach (Monster.MonsterTribe monsterTribe in Enum.GetValues(typeof(Monster.MonsterTribe)))
                    {
                        Console.WriteLine(monsterTribe);
                    }
                }
            } while (!validChoice);

            Console.Write("\tActive: ");
            newMonster.Active = true;


            //
            // echo new monster properties
            //
            Console.WriteLine();
            Console.WriteLine("\tNew Monster's Properties");
            Console.WriteLine("\t-------------");
            MonsterInfo(newMonster);
            Console.WriteLine();
            Console.WriteLine("\t-------------");

            DisplayContinuePrompt();

            monsters.Add(newMonster);
        }

        /// <summary>
        /// SCREEN: delete monster
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayDeleteMonster(List<Monster> monsters)
        {
            DisplayScreenHeader("Delete Monster");

            //
            // display all monster names
            //
            Console.WriteLine("\tMonster Names");
            Console.WriteLine("\t-------------");
            foreach (Monster monster in monsters)
            {
                Console.WriteLine("\t" + monster.Name);
            }

            //
            // get user monster choice
            //
            Console.WriteLine();
            Console.Write("\tEnter name:");
            string monsterName = Console.ReadLine();

            //
            // get monster object
            //
            Monster selectedMonster = null;
            foreach (Monster monster in monsters)
            {
                if (monster.Name == monsterName)
                {
                    selectedMonster = monster;
                    break;
                }
            }

            //
            // delete monster
            //
            if (selectedMonster != null)
            {
                monsters.Remove(selectedMonster);
                Console.WriteLine();
                Console.WriteLine($"\t{selectedMonster.Name} deleted");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"\t{monsterName} not found");
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// SCREEN: update monster
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        /// 
        static void DisplayUpdateMonster(List<Monster> monsters)
        {
            bool validResponse = false;
            Monster selectedMonster = null;

            do
            {
                DisplayScreenHeader("Update Monster");

                //
                // display all monster names
                //
                Console.WriteLine("\tMonster Names");
                Console.WriteLine("\t-------------");
                foreach (Monster monster in monsters)
                {
                    Console.WriteLine("\t" + monster.Name);
                }

                //
                // get user monster choice
                //
                Console.WriteLine();
                Console.Write("\tEnter name:");
                string monsterName = Console.ReadLine();

                //
                // get monster object
                //

                foreach (Monster monster in monsters)
                {
                    if (monster.Name == monsterName)
                    {
                        selectedMonster = monster;
                        validResponse = true;
                        break;
                    }
                }

                //
                // feedback for wrong name choice
                //
                if (!validResponse)
                {
                    Console.WriteLine("\tPlease select a correct name.");
                    DisplayContinuePrompt();
                }

                //
                // update monster
                //

            } while (!validResponse);


            //
            // update monster properties
            //
            string userResponse;
            Console.WriteLine();
            Console.WriteLine("\tReady to update. Press the Enter to keep the current info.");
            Console.WriteLine();
            Console.Write($"\tCurrent Name: {selectedMonster.Name} New Name: ");
            userResponse = Console.ReadLine();
            if (userResponse != "")
            {
                selectedMonster.Name = userResponse;
            }

            Console.Write($"\tCurrent Age: {selectedMonster.Age} New Age: ");
            userResponse = Console.ReadLine();
            if (userResponse == "")
            {
                validResponse = true;
            }
            else
            {
                bool validAge = false;
                do
                {
                    Console.Write("\tNew Age: ");
                    validAge = int.TryParse(Console.ReadLine(), out int age);
                    selectedMonster.Age = age;

                } while (!validAge);
            }
               
            


            bool validChoice = false;
            Console.Write($"\tCurrent Attitude: {selectedMonster.Attitude} New Attitude: ");
            userResponse = Console.ReadLine();

            if (userResponse == "")
            {
                validChoice = true;
            }
            else 
            {
                
                do
                {
                    validChoice = Enum.TryParse(userResponse, out Monster.EmotionalState attitude);
                    selectedMonster.Attitude = attitude;
                    
                    if (!validChoice)
                    {
                        Console.WriteLine("Invalid choice, please choose from list below");
                        foreach (Monster.EmotionalState MonsterAttitude in Enum.GetValues(typeof(Monster.EmotionalState)))
                        {
                            Console.WriteLine(MonsterAttitude);
                        }
                        userResponse = Console.ReadLine();
                    }
                    

                } while (!validChoice);
              
            }
    

            
        
             Console.Write($"\tCurrent Tribe: {selectedMonster.Tribe} New Tribe: ");
             userResponse = Console.ReadLine();
             
            if (userResponse == "")
            {
                validChoice = true;
            }
            else
            {

                do
                {
                    validChoice = Enum.TryParse(userResponse, out Monster.MonsterTribe tribe);
                    selectedMonster.Tribe = tribe;

                    if (!validChoice)
                    {
                        Console.WriteLine("Invalid choice, please choose from list below");
                        foreach (Monster.MonsterTribe Tribe in Enum.GetValues(typeof(Monster.MonsterTribe)))
                        {
                            Console.WriteLine(Tribe);
                        }
                        userResponse = Console.ReadLine();
                    }


                } while (!validChoice);

            }

            bool validActive = false;
            do
            {
                Console.Write($"\tActive: {selectedMonster.Active} Type active or inactive ");
                userResponse = Console.ReadLine().ToLower();
                if (userResponse == "active")
                {
                    selectedMonster.Active = true;
                    validActive = true;
                }
                else if (userResponse == "inactive")
                {
                    selectedMonster.Active = false;
                    validActive = true;
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                    validActive = false;
                }
            } while (!validActive);
            

        
            //
            // echo updated monster properties
            //
            Console.WriteLine();
            Console.WriteLine("\tNew Monster's Properties");
            DisplayScreenHeader("All Monsters");
            Console.Write($"Name".PadRight(10));
            Console.Write($"Age".PadRight(5));
            Console.Write($"Attitude".PadRight(10));
            Console.Write($"Tribe".PadRight(10));
            Console.Write("Greeting".PadRight(50));
            Console.WriteLine("Status".PadLeft(20));
            Console.WriteLine("************************************************************" +
                "**************************************************");
            MonsterInfo(selectedMonster);
            Console.WriteLine();
            

            DisplayContinuePrompt();
        }

        /// <summary>
        /// SCREEN: write list of monsters to data file
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void DisplayWriteToDataFile(List<Monster> monsters)
        {
            DisplayScreenHeader("Write to Data File");

            //
            // prompt the user to continue
            //
            Console.WriteLine("\tThe application is ready to write to the data file.");
            Console.Write("\tEnter 'y' to continue or 'n' to cancel.");
            if (Console.ReadLine().ToLower() == "y")
            {
                DisplayContinuePrompt();
                WriteToDataFile(monsters);
                //
                // TODO process I/O exceptions
                //

                Console.WriteLine();
                Console.WriteLine("\tList written to data the file.");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("\tList not written to the data file.");
            }

            DisplayContinuePrompt();
        }

        #endregion

        #region FILE I/O METHODS

        /// <summary>
        /// write monster list to data file
        /// </summary>
        /// <param name="monsters">list of monsters</param>
        static void WriteToDataFile(List<Monster> monsters)
        {
            string[] monstersString = new string[monsters.Count];

            //
            // create array of monster strings
            //
            for (int index = 0; index < monsters.Count; index++)
            {
                string monsterString =
                    monsters[index].Name + "," +
                    monsters[index].Age + "," +
                    monsters[index].Attitude + "," +
                    monsters[index].Tribe + "," +
                    monsters[index].Active;

                monstersString[index] = monsterString;
            }

            File.WriteAllLines("Data\\Data.txt", monstersString);
        }

        /// <summary>
        /// read monsters from data file and return a list of monsters
        /// </summary>
        /// <returns>list of monsters</returns>        
        static List<Monster> ReadFromDataFile()
        {
            List<Monster> monsters = new List<Monster>();

            //
            // read all lines in the file
            //
            string[] monstersString = File.ReadAllLines("Data\\Data.txt");

            //
            // create monster objects and add to the list
            //
            foreach (string monsterString in monstersString)
            {
                //
                // get individual properties
                //
                string[] monsterProperties = monsterString.Split(',');

                //
                // create monster
                //
                Monster newMonster = new Monster();

                //
                // update monster property values
                //
                newMonster.Name = monsterProperties[0];

                int.TryParse(monsterProperties[1], out int age);
                newMonster.Age = age;

                Enum.TryParse(monsterProperties[2], out Monster.EmotionalState attitude);
                newMonster.Attitude = attitude;

                Enum.TryParse(monsterProperties[3], out Monster.MonsterTribe tribe);
                newMonster.Tribe = tribe;

                bool.TryParse(monsterProperties[4], out bool Active);
                newMonster.Active = Active;

                //
                // add new monster to list
                //
                monsters.Add(newMonster);
            }

            return monsters;
        }

        #endregion

        #region CONSOLE HELPER METHODS

        /// <summary>
        /// display all monster properties
        /// </summary>
        /// <param name="monster">monster object</param>
        static void MonsterInfo(Monster monster)
        {
                Console.Write($"{monster.Name}".PadRight(10));
                Console.Write($"{monster.Age}".PadRight(5));
                Console.Write($"{monster.Attitude}".PadRight(10));
                Console.Write($"{monster.Tribe}".PadRight(10));
                Console.Write(monster.Greeting().PadRight(50));
            if (monster.Active)
            {
                Console.Write("Active".PadLeft(20));
            }
            else
            {
                Console.Write("Inactive".PadLeft(20));
            }
            
           
        }

        /// <summary>
        /// display welcome screen
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThe Monster Tracker");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display closing screen
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using the Monster Tracker!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;
            Console.WriteLine();
            Console.Write("\tPress any key to continue.");
            Console.ReadKey();
            Console.CursorVisible = true;
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion
    }
}

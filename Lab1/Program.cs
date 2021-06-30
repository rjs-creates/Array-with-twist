//***********************************************************************************
//Program: Lab1_.cs
//Description: Creating an Array and performing operations on it
//Date: Jan. 29/2020
//Author: Rajeshwar Singh(r25)          
//Course: CMPE1600
//Class: CNTA03
//***********************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDIDrawer;
using System.Drawing;
using System.IO;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Calling main method
            menu();
        }

        //********************************************************************************************
        //Method: static void menu()
        //Purpose: Displays all the possible actions and performs on user's selection
        //Parameters: none
        //Returns: void
        //*********************************************************************************************

        static void menu()
        {
            string ans;             //Contains the response from user
            bool created;           //Variable storing whether the array has been created or not
            int[] nGrades = { };    //Array containg the grades
            bool bplay;

            //Since array hasn't been created set bool val to false
            created = false;

            //loop till the user wants to leave
            do
            {
                //setting loop conditional to true
                bplay = true; 

                //Menu shown to the user
                Console.WriteLine("Actions available...");
                Console.WriteLine("1. Create random array.");
                Console.WriteLine("2. Array stats.");
                Console.WriteLine("3. Draw histogram.");
                Console.WriteLine("4. Save Array to file.");
                Console.WriteLine("5. Load Array to file.");
                Console.WriteLine("q. Exit the program");
                Console.Write("\nYour selection:");

                //Response from the user
                ans = Console.ReadLine().ToLower();

                //Space
                Console.WriteLine();

                //Completing suitable action for the response
                switch (ans)
                {
                    //Creating Array if the response is 1
                    case "1":

                        //Changing bool value to true since the array has been created
                        created = true;

                        //Calling Create Array to create an array
                        CreateArray(out nGrades);

                        //Displaying the created array
                        DisplayArray(nGrades, ans, created);
                        break;

                    //Displaying stats of array if the array  has been created
                    case "2":

                        //Calling Display Array method
                        DisplayArray(nGrades, ans, created);
                        break;

                    //Drawing histogram of array if the response is 3
                    case "3":

                        //If the array has been already created
                        if (created == true)
                        {
                            //creating drawing object
                            CDrawer cDrawer = new CDrawer();

                            //Calling Draw histogram to make the histogram
                            DrawHistogram(nGrades, cDrawer);
                        }
                        //If the array hasn't been created earlier
                        else
                        {
                            //Printing default response and playing beep sound
                            Console.WriteLine("Array not created");
                            Console.Beep(1000, 500);
                        }
                        break;

                    //Saving created array if user reponse is 4
                    case "4":

                        //If the array has been already created
                        if (created == true)
                        {
                            //Calling SaveFile to save the array
                            SaveFile(nGrades);
                        }
                        //If the array hasn't been created earlier
                        else
                        {
                            //Printing default response and playing beep sound
                            Console.WriteLine("Array not created");
                            Console.Beep(1000, 500);
                        }
                        break;

                    //Loading a previously save Array of grades
                    case "5":

                        //Calling LoadFile to load the array
                        LoadFile();
                        break;

                    //Quiting if user writes q
                    case "q":

                        //Changing loop conditional to false
                        bplay = false;
                        break;

                    //If the response doesn't match any valid response
                    default:

                        //Printing invlid prompt
                        Console.WriteLine("Invalid Input. Please enter a valid input");
                        break;
                }

                //if the loop is still continuing
                if (bplay)
                {
                    //Prompting user to click any key to continue
                    Console.Write("\n\nPress any key to continue...");
                    Console.ReadKey();

                    //Clearing the whole screen
                    Console.Clear();
                }
            }
            //Loop while answer is not q
            while (bplay);
        }

        //********************************************************************************************
        //Method: public static void CreateArray(out int[] dArrayGrades)
        //Purpose: Create Array of 1 to 10000 values with random integer values inside
        //Parameters: out int[] dArrayGrades - Array with reference Containg the grades
        //Returns: void
        //*********************************************************************************************
        public static void CreateArray(out int[] dArrayGrades)
        {
            int nArrayLength;             //Length of Array
            int incrementor;              //increment variable for for loop
            Random rand = new Random();   //Random object intialization

            //Calling the utilities library to get user to input the size of array
            Utilities.Cutilities.GetValue(out nArrayLength, "Enter the size of the array: ", 1, 10000);

            //Creating Grade array with user's length
            dArrayGrades = new int[nArrayLength];

            //Loop to fill all the index of array with random values
            for (incrementor = 0; incrementor < nArrayLength; incrementor++)
            {
                //Getting a random value by converting the double value gotten from multiplyin random double with 100
                dArrayGrades[incrementor] = Convert.ToInt32(rand.NextDouble() * 100);
            }

            //Additional space
            Console.WriteLine();
        }

        //********************************************************************************************
        //Method: public static void DisplayArray(int[] nGrades, string ans, bool created)
        //Purpose:Display Array stats.
        //Parameters: int[] nGrades - Array containing grades.
        //string ans - String containg response of user
        //bool created - bool containg whether the array was created or not
        //Returns: void
        //*********************************************************************************************
        public static void DisplayArray(int[] nGrades, string ans, bool created)
        {
            int incrementor;         //increment variable for for loop
            int counter;             //counting the number of values in one row

            //If the Array has been previously created
            if (created == true)
            {
                //Setting counter's intial value to  0
                counter = 0;

                //If the user is displaying Array stats
                if (ans == "2")
                {
                    //Sorting the contents
                    Array.Sort(nGrades);
                }

                Console.WriteLine("The current contents of the array:");
                for (incrementor = 0; incrementor < nGrades.Length; incrementor++)
                {
                    //incrementing the number of values in a row
                    counter++;

                    //Printing individual Index
                    Console.Write($"{nGrades[incrementor]:D3}  ");

                    //if the array is 20 vals long
                    if (counter == 20)
                    {
                        //setting couter back to 0
                        counter = 0;

                        //Adding a new line
                        Console.WriteLine("");
                    }
                }

                //If the user is displaying Array stats
                if (ans == "2")
                {
                    //Printing min, max and average if displaying the stats
                    Console.WriteLine($"\n\nthe minimum value is {nGrades.Min()}");
                    Console.WriteLine($"the average value is {nGrades.Average():n1}");
                    Console.WriteLine($"the maximum value is {nGrades.Max()} \n");
                }
            }

            //If the array hasn't been created earlier
            else
            {
                //Printing default response and playing beep sound
                Console.WriteLine("Array has not been created");
                Console.Beep(1000, 500);
            }
        }

        //********************************************************************************************
        //Method: public static void DrawHistogram(int[] nGrades, CDrawer cDrawer)
        //Purpose:Draw histogram of the array contents
        //Parameters: int[] nGrades - Array containing grades.
        //CDrawer cDrawer - Reference of the drawing window
        //Returns: void
        //*********************************************************************************************

        public static void DrawHistogram(int[] nGrades, CDrawer cDrawer)
        {
            int incrementor;             //increment variable for for loop          
            double maxVal;               //Maxval of binArray
            string num;                  //value of binArray at one index
            double ratio;                //ratio between the current value and max value of Array

            //Creating binArray
            int[] binArray = new int[11];

            //Creating color array
            Color[] colors = new Color[11] { Color.Red, Color.Green, Color.Blue, Color.Cyan, Color.Magenta, Color.Yellow, Color.Orange, Color.Pink, Color.AntiqueWhite, Color.DarkMagenta, Color.AliceBlue };

            //Clearing the drawing window
            cDrawer.Clear();

            //Sorting the contents
            Array.Sort(nGrades);

            //for loop to find the range of the binArray value
            for (incrementor = 0; incrementor < nGrades.Length; incrementor++)
            {
                //find the range of the value
                //Incrementing the value on the corresponding index

                if (nGrades[incrementor] >= 0 && nGrades[incrementor] <= 9)
                {
                    binArray[0]++;
                }
                if (nGrades[incrementor] >= 10 && nGrades[incrementor] <= 19)
                {
                    binArray[1]++;
                }
                if (nGrades[incrementor] >= 20 && nGrades[incrementor] <= 29)
                {
                    binArray[2]++;
                }
                if (nGrades[incrementor] >= 30 && nGrades[incrementor] <= 39)
                {
                    binArray[3]++;
                }
                if (nGrades[incrementor] >= 40 && nGrades[incrementor] <= 49)
                {
                    binArray[4]++;
                }
                if (nGrades[incrementor] >= 50 && nGrades[incrementor] <= 59)
                {
                    binArray[5]++;
                }
                if (nGrades[incrementor] >= 60 && nGrades[incrementor] <= 69)
                {
                    binArray[6]++;
                }
                if (nGrades[incrementor] >= 70 && nGrades[incrementor] <= 79)
                {
                    binArray[7]++;
                }
                if (nGrades[incrementor] >= 80 && nGrades[incrementor] <= 89)
                {
                    binArray[8]++;
                }
                if (nGrades[incrementor] >= 90 && nGrades[incrementor] <= 99)
                {
                    binArray[9]++;
                }
                if (nGrades[incrementor] == 100)
                {
                    binArray[10]++;
                }
            }

            //Finding the max value of the binArray
            maxVal = binArray.Max();

            //Drawing the whole histogram
            for (incrementor = 0; incrementor < 11; incrementor++)
            {
                //for the first 10 ranges
                if (incrementor < 10)
                {
                    //higest upper range of one index
                    num = (incrementor * 10 + 9).ToString();

                    //Printing the range labels on the bottom of the window
                    cDrawer.AddText($"{(incrementor * 10).ToString()} to {num}", 8, incrementor * 70 + 10, 580, 55, 15, Color.White);
                }
                
                //for Last Index
                else
                {
                    //Just printing 100 for the last index
                    cDrawer.AddText($"100", 8, incrementor * 70 + 10, 580, 55, 15, Color.White);
                }

                //if the value of the binArray index is not equal to zero
                if (binArray[incrementor] != 0)
                {
                    //Calculating the ratio between the value and max value
                    ratio = (binArray[incrementor] / maxVal);

                    //Drawing one rectangle depending on the value of that index of Array
                    cDrawer.AddRectangle(incrementor * 70, 570 - Convert.ToInt32(570 * ratio), 70, Convert.ToInt32(570 * ratio), colors[incrementor]);

                    //Labels for value of particular index of Array on the middle of rectangle
                    cDrawer.AddText($"{binArray[incrementor]}", 13, incrementor * 70 + 10, 550-(Convert.ToInt32(570 * ratio)) / 2, 55, 15, Color.Black);
                }
            }

            //Prompting user to close the Draw window once its drawn
            Console.Write("Press any key to close the Drawing window: ");
            Console.ReadKey();
            cDrawer.Close();
        }
        //********************************************************************************************
        //Method: public static void SaveFile(int[] nGrades)
        //Purpose:Save the array on a text file
        //Parameters: int[] nGrades - Array containing grades.
        //Returns: void
        //*********************************************************************************************
        public static void SaveFile(int[] nGrades)
        {
            int incremenator;          //increment variable for for loop  
            string fileName;           //Filename to save the grades on

            //try and catching unwanted errors
            try
            {
                //Prompting user to enter the file to save on
                Console.Write("Enter the name of the save file:");
                fileName = Console.ReadLine();

                //If the user does not specify the text file
                if (fileName.Contains(".txt") == false)
                {
                    //Add text file extension to the filename
                    fileName += ".txt";
                }

                //Creating streamwriter reference
                StreamWriter sWriter = new StreamWriter(fileName);

                //Writing the contents of Array to the text file
                for (incremenator = 0; incremenator < nGrades.Length; incremenator++)
                {
                    sWriter.WriteLine(nGrades[incremenator]);
                }

                //Closing the reference
                sWriter.Close();
            }
            catch(Exception error)
            {
                //Printing the error
                Console.WriteLine($"{error.Message}");
            }
        }
        //********************************************************************************************
        //Method: public static void LoadFile()
        //Purpose:Loading previously saved Array on a text file
        //Parameters: none
        //Returns: void
        //*********************************************************************************************
        public static void LoadFile()
        {
            int incremenator;               //increment variable for for loop  
            string fileName;                //Filename to save the grades on
            int arrayLength;                //Length of Array



            //Prompting user to enter the name of file to be loaded
            Console.Write("Enter the name of the file to be loaded: ");
            fileName = Console.ReadLine();

            //If the user does not specify the text file
            if (fileName.Contains(".txt") == false)
            {
                fileName += ".txt";
            }

            //Creating streamreader reference
            StreamReader sReader = new StreamReader(fileName);

            //Finding the number of values in the text files
            arrayLength = File.ReadLines(fileName).Count();

            //Creating an Array with length we just found
            int[] loadArray = new int[arrayLength];

            //Loop  to read the contents of text file
            for (incremenator = 0; incremenator < arrayLength; incremenator++)
            {
                //Converting the values from string to integer
                int.TryParse(sReader.ReadLine(),out loadArray[incremenator]);
            }

            //Calling displayArray to display the contents of loadArray
            DisplayArray(loadArray, "1", true);
        }
    }
}

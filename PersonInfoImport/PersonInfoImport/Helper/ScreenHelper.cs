using PersonInfoImport.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonInfoImport.Helper
{
    public class ScreenHelper
    {
        private static readonly string mainScreenText = @"
*** Person Info App: Please select the following options:
1. Add data.
2. List existing records sorted by favorite color then by last name ascending.
3. List existing records sorted by birth date, ascending.
4. List existing records sorted by last name, descending.
5. Rest API calls information.
6. Exit Application.
=> Please enter 1-6: ";
        private static readonly string restAPIScreenText = @"
*** Rest API calls information with the following methods and endpoints:
- Base Url: " + RestAPIHelper.serviceBaseAddress + @"
- POST " + EndPointList.postRecord + @" - Post a single data line in any of the 3 supported formats.
- GET " + EndPointList.color + @" - returns records sorted by favorite color. Enter 1 to view this in the browser.
- GET " + EndPointList.birthdate + @" - returns records sorted by birthdate. Enter 2 to view this in the browser.
- GET " + EndPointList.name + @" - returns records sorted by last name. Enter 3 to view this in the browser.
=> Enter 1, 2, 3 to get the data in browser or 0 to go back: ";
        private static readonly string importScreenText = @" 
*** Add Data: Records can be added by importing the text file(s) or posting a single record.  
Each text file must have each record listed in a single line and no header is included.  
Each record must be either in the pipe-delimited format (LastName | FirstName | Email | FavoriteColor | DateOfBirth),
or in the comma-delimited (LastName, FirstName, Email, FavoriteColor, DateOfBirth)
or in space-delimited (LastName FirstName Email FavoriteColor DateOfBirth)
There are 3 options to add data:
- Run a command Prompt as an Administrator and enter: PersonInfoImport.exe file1.txt file2.txt file3.txt.
- Use Rest API client to post a record using url: " + RestAPIHelper.serviceBaseAddress + @".
=> Enter the fileName(s) seperated by a comma here, or enter Test to import test data, or enter C to cancel: ";
        private static readonly string mainScreenInvalidText = "=> Invalid value entered. Please enter from 1 to 6: ";
        private static readonly string restAPIScreenInvalidText = "=> Invalid value entered. Please Enter from 0 to 3:";

        private enum mainOptions
        {
            ImportFile = 1,
            ListByColor = 2,
            ListByBirthDate = 3,
            ListByLastName = 4,
            RestAPIInfo = 5,
            ExitApp = 6

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sortedBy"></param>
        public static void ShowListByScreen(SortedBy sortedBy)
        {
            List<Person> personList = PersonHelper.GetSortedList(sortedBy, out string header);
            Console.WriteLine("");
            Console.WriteLine(header + " (Total: " + personList.Count.ToString() + ")");

            Console.WriteLine(new String('-', 100));
            Console.WriteLine("{0,-17} {1,-17} {2,-30} {3,-15} {4,15}", "Last Name", "First Name", "Email", "Favorite Color", "Date of Birth");
            Console.WriteLine("{0,-17} {1,-17} {2,-30} {3,-15} {4,15}", "---------", "----------", "-----", "--------------", "-------------");
            foreach (Person person in personList)
            {
                Console.WriteLine("{0,-17} {1,-17} {2,-30} {3,-15} {4,15}", person.LastName, person.FirstName, person.Email, person.FavoriteColor, person.DateOfBirth.ToString("M/d/yyyy"));
            }
            Console.WriteLine(new String('-', 100));
            Console.WriteLine("");


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="showText"></param>
        public static void ShowMainScreen(bool showText)
        {
            if (showText)
                Console.Write(mainScreenText);

            try
            {
                string line = Console.ReadLine();

                if (!int.TryParse(line, out int key))
                {
                    Console.Write(mainScreenInvalidText);
                    ShowMainScreen(false);
                }
                else
                {
                    if (key == (int)mainOptions.ImportFile)
                    {
                        ReadImportScreen();
                    }
                    else if (key == (int)mainOptions.ListByColor)
                    {
                        ShowListByScreen(SortedBy.ColorLastNameAsc);
                    }
                    else if (key == (int)mainOptions.ListByBirthDate)
                    {
                        ShowListByScreen(SortedBy.BirthDateAsc);
                    }
                    else if (key == (int)mainOptions.ListByLastName)
                    {
                        ShowListByScreen(SortedBy.LastNameDsc);
                    }
                    else if (key == (int)mainOptions.RestAPIInfo)
                    {
                        ShowRestAPIScreen(true);
                    }
                    else if (key == (int)mainOptions.ExitApp)
                    {
                        return;
                    }
                    else
                    {
                        Console.Write(mainScreenInvalidText);
                        ShowMainScreen(false);
                    }
                }

                ShowMainScreen(true);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="showText"></param>
        public static void ShowRestAPIScreen(bool showText)
        {
            if (showText)
            {
                Console.WriteLine("");
                Console.Write(restAPIScreenText);
            }

            try
            {
                string line = Console.ReadLine();

                if (!int.TryParse(line, out int key))
                {
                    Console.Write(restAPIScreenInvalidText);
                    ShowRestAPIScreen(false);
                }
                else
                {
                    if (key == 0)
                        return;
                    else if (key > 0 && key <= 3)
                    {
                        string url = RestAPIHelper.serviceBaseAddress + RestAPIHelper.GetEndpoint(key);

                        System.Diagnostics.Process.Start(url);
                        ShowRestAPIScreen(true);
                    }
                    else
                    {
                        Console.Write(restAPIScreenInvalidText);
                        ShowRestAPIScreen(false);
                    }
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void ReadImportScreen()
        {
            Console.WriteLine("");
            Console.Write(importScreenText);

            try
            {
                string line = Console.ReadLine();

                if (line.ToLower() == "c")
                    return;
                else if (line.ToLower() == "test")
                {
                    int countCurr = PersonHelper.personList.Count;
                    PersonHelper.LoadTestData();
                    ShowOutputScreen(PersonHelper.personList.Count - countCurr);

                }
                else
                {
                    int countCurr = PersonHelper.personList.Count;
                    PersonHelper.ParseFiles(line);
                    ShowOutputScreen(PersonHelper.personList.Count - countCurr);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="totalAdded"></param>
        public static void ShowOutputScreen(int totalAdded)
        {
            Console.WriteLine("");
            Console.WriteLine("Import Result: " + totalAdded.ToString() + " records have been imported successfully.");

            ShowListByScreen(SortedBy.ColorLastNameAsc);
            ShowListByScreen(SortedBy.BirthDateAsc);
            ShowListByScreen(SortedBy.LastNameDsc);
        }

    }
}

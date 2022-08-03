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
  
        private enum mainOptions
        {
            ImportFile = 1,
            ListByColor = 2,
            ListByBirthDate = 3,
            ListByLastName = 4,
            RestAPIInfo = 5,
            ExitApp = 6

        }
        public static void ShowListByScreen(SortedBy sortedBy)
        {
            List<Person> personList = PersonHelper.GetSortedList(sortedBy,out string header);
            Console.WriteLine("");
            Console.WriteLine(header + ": ");

            Console.WriteLine(new String('-', 90));
            Console.WriteLine("{0,-12} {1,-12} {2,-25} {3,-20} {4,15}", "Last Name", "First Name", "Email", "Favorite Color", "Date of Birth");
            foreach (Person person in personList)
            {
                Console.WriteLine("{0,-12} {1,-12} {2,-25} {3,-20} {4,15}", person.LastName, person.FirstName, person.Email, person.FavoriteColor, person.DateOfBirth.ToString("M/d/yyyy"));
            }
            Console.WriteLine(new String('-', 90));
            Console.WriteLine("");


        }

        public static void ShowMainScreen()
        {
            Console.Write(
                @"Please select the following options:
1. Import data.
2. List existing records sorted by favorite color then by last name ascending.
3. List existing records sorted by birth date, ascending.
4. List existing records sorted by last name, descending.
5. Rest API calls information.
6. Exit Application.
=> Please enter 1-6: ");
        }
        public static void ReadMainScreen()
        {
            ShowMainScreen();

            try
            {
                string line = Console.ReadLine();

                if (!int.TryParse(line, out int key))
                {
                    Console.WriteLine("Invalid value entered. Please enter 1-6: ");
                    ReadMainScreen();
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
                        ReadRestAPIScreen();
                    }
                    else if (key == (int)mainOptions.ExitApp)
                    {
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Invalid value entered. Please enter 1-6: ");
                        ReadMainScreen();
                    }
                }

                ReadMainScreen();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void ShowRestAPIScreen()
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.Write(
@"Rest API calls information with the following methods and endpoints:
* Base Url: http://localhost:8080
* POST /records - Post a single data line in any of the 3 formats supported by your existing code
* GET /records/color - returns records sorted by favorite color. Enter 1 to see this in the browser.
* GET /records/birthdate - returns records sorted by birthdate. Enter 2 to see this in the browser.
* GET /records/name - returns records sorted by last name. Enter 3 to see this in the browser.
=> Enter 1, 2, 3 to get the data in browser or 0 to go back: ");
        }
        public static void ReadRestAPIScreen()
        {
            ShowRestAPIScreen();

            try
            {
                string line = Console.ReadLine();

                if (!int.TryParse(line, out int key))
                {
                    Console.WriteLine("Invalid value entered. try again: ");
                    ReadMainScreen();
                }
                else
                {
                    if (key == 0)
                        return;
                    else if (key>0 && key<=3)
                    {
                        string url = RestAPIHelper.serviceBaseAddress;
                        if (key == (int)SortedBy.ColorLastNameAsc)
                            url += "/records/color";
                        else if (key == (int)SortedBy.BirthDateAsc)
                            url += "/records/birthdate";
                        else if (key == (int)SortedBy.LastNameDsc)
                            url += "/records/name";

                        System.Diagnostics.Process.Start(url);
                        ReadRestAPIScreen();
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please try again: ");
                        ReadRestAPIScreen();
                    }
                }
               

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public static void ShowImportScreen()
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.Write(
@"Import Data: 3 options below. 
1. Run a command line: PersonInfoImport.exe [FileName(s)] where the FileNames are seperated by a comma. 
2. Use Rest API client to post a record using url: http://localhost:8080/records.
3. Enter the fileName(s) here, or enter Test to import test data, or enter C to cancel: ");
        }

        public static void ReadImportScreen()
        {
            ShowImportScreen();

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

        private static void ShowOutputScreen(int totalAdded)
        {
            Console.WriteLine("");
            Console.WriteLine("Import Result: " + totalAdded.ToString() + " records have been imported successfully.");

            ShowListByScreen(SortedBy.ColorLastNameAsc);
            ShowListByScreen(SortedBy.BirthDateAsc);
            ShowListByScreen(SortedBy.LastNameDsc);
        }

    }
}

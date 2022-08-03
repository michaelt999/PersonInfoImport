﻿using PersonInfoImport.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonInfoImport.Helper
{
    public enum SortedBy
    {
        ColorLastNameAsc = 1,
        BirthDateAsc = 2,
        LastNameDsc = 3
    }
    public static class PersonHelper
    {
        public static List<Person> personList = new List<Person>();

        public static List<char> allowedDelimiters = new List<char>() {'|', ',',' '};
        private const char parsedFileDelmiter = ',';

        public static List<Person> GetSortedList(SortedBy sortedBy, out string header)
        {
            try
            {
                header = "";
                if (sortedBy == SortedBy.ColorLastNameAsc) //sorted by favorite color then by last name ascending.
                {
                    header = "Person list sorted by favorite color then by last name ascending";
                    return personList.OrderBy(n => n.FavoriteColor).ThenBy(m => m.LastName).ToList();
                }
                else if (sortedBy == SortedBy.BirthDateAsc) //sorted by birth date, ascending.
                {
                    header = "Person list sorted by birth date, ascending";
                    return personList.OrderBy(n => n.DateOfBirth).ToList();
                }
                else if (sortedBy == SortedBy.LastNameDsc) //sorted by last name, descending.
                {
                    header = "Person list sorted by last name, descending";
                    return personList.OrderByDescending(n => n.LastName).ToList();
                }
                else
                    return personList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void LoadTestData()
        {
            try
            {
                string fileNames = "TestDataComma.txt, TestDataPipe.txt, TestDataSpace.txt";
                ParseFiles(fileNames);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public static void ParseFiles(string fileNames)
        {
            try
            {
                if (string.IsNullOrEmpty(fileNames))
                    throw new ArgumentNullException("Filename is empty");

                string[] aryFileName = fileNames.Split(parsedFileDelmiter);
                foreach(var fileName in aryFileName)
                {
                    if (!string.IsNullOrEmpty(fileName.Trim()))
                        ParseFile(fileName.Trim());
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public static void ParseFile(string fileName)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName))
                    throw new ArgumentNullException("Filename is empty");

                //add path for test data
                if (!fileName.Contains("\\"))
                {
                    string pathParent = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                    fileName = Path.Combine(pathParent,"Data\\" + fileName);
                }


                if (!File.Exists(fileName))
                    throw new Exception("File [" + fileName + "] doesn't exist");


                var lines = File.ReadLines(fileName);
               
                foreach (var line in lines)
                {
                    try
                    {
                        var person = ParsePersonData(line);

                        if (person != null)
                            personList.Add(person);
                    }
                    catch (Exception ex)
                    {
                        //skip error and continue to add the rest. Can be added to log or output if needed.
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error on ParseFile: " + ex.Message);
            }
        }

        public static Person ParsePersonData(string data)
        {
            try
            {
            
                foreach(var allowedDim in allowedDelimiters)
                {
                    if (data.Contains(allowedDim))
                    {
                        string[] parsedData = data.Split(allowedDim);
                        if (parsedData.Length >= 5)
                        {
                            Person person = new Person();
                            person.LastName = parsedData[0].Trim();
                            person.FirstName = parsedData[1].Trim();
                            person.Email = parsedData[2].Trim();
                            person.FavoriteColor = parsedData[3].Trim();
                            person.DateOfBirth = Convert.ToDateTime(parsedData[4].Trim());

                            return person;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error on ParsePersonData: " + ex.Message);
            }

            return null;
        }



    }
}
using PersonInfoImport.Helper;
using PersonInfoImport.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonInfoImport.RestAPI
{
    public class PersonRestService: IPersonRestService
    {
        public string PostPerson(string personData)
        {
            try
            {
                var person = PersonHelper.ParsePersonData(personData);
                if (person != null)
                    return person.FirstName + " " + person.LastName + " data has been added successfully.";
                else
                    return "Adding data has failed.";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
         
        public PersonList GetListBy(string sortBy)
        {
            try
            {
                PersonList list = new PersonList();


                if (sortBy.ToLower() == "color")
                {
                    list.Persons = PersonHelper.GetSortedList(SortedBy.ColorLastNameAsc, out string header);
                    list.Title = header;
                }

                else if (sortBy.ToLower() == "birthdate")
                {
                    list.Persons = PersonHelper.GetSortedList(SortedBy.BirthDateAsc, out string header);
                    list.Title = header;
                }
                else if (sortBy.ToLower() == "name")
                {
                    list.Persons = PersonHelper.GetSortedList(SortedBy.LastNameDsc, out string header);
                    list.Title = header;
                }
                else
                {
                    list.Persons = PersonHelper.personList;
                    list.Title = "Person List";
                }

                list.Total = list.Persons.Count;

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
    }
}

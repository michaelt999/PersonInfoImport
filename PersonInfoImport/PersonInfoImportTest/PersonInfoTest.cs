using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersonInfoImport.Helper;
using PersonInfoImport.Model;
using System;
using System.Collections.Generic;

namespace PersonInfoImportTest
{
    [TestClass]
    public class PersonInfoTest: TestBase
    {
        [ClassInitialize()]
        public static void ClassInitialize(TestContext tc)
        {
            RestAPIHelper.StartRestAPIHost();
            AddTestData();
        }
        [ClassCleanup()]
        public static void ClassCleanup()
        {
            RemoveTestData();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            PersonHelper.ClearData();
        }

        [TestMethod]
        [Description("Test importing good file with pipe delimeters.")]
        [TestCategory("Good Data")]
        public void GoodParsingPipeFile()
        {
            PersonHelper.ParseFile(testDataPipePath);
            Assert.AreEqual("Pipe3", PersonHelper.personList[2].FirstName);
            Assert.AreEqual(3, PersonHelper.personList.Count);
        }

        [TestMethod]
        [Description("Test importing good file with comma delimeters.")]
        [TestCategory("Good Data")]
        public void GoodParsingCommaFile()
        {
            PersonHelper.ParseFile(testDataCommaPath);
            Assert.AreEqual("Comma6", PersonHelper.personList[2].FirstName);
            Assert.AreEqual(3, PersonHelper.personList.Count);
        }

        [TestMethod]
        [Description("Test importing good file with space delimeters.")]
        [TestCategory("Good Data")]
        public void GoodParsingSpaceFile()
        {
            PersonHelper.ParseFile(testDataSpacePath);
            Assert.AreEqual("Space9", PersonHelper.personList[2].FirstName);
            Assert.AreEqual(3, PersonHelper.personList.Count);
        }

        [TestMethod]
        [Description("Test importing multiple good files with all 3 allowed delimeters.")]
        [TestCategory("Good Data")]
        public void GoodParsingMultipleFiles()
        {
            PersonHelper.ParseFiles(testDataMultipleFiles);
            Assert.AreEqual("Space9", PersonHelper.personList[8].FirstName);
            Assert.AreEqual(9, PersonHelper.personList.Count);
        }

        [TestMethod]
        [Description("Test importig good file with some bad records. Make sure that bad records are skipped and good records are added.")]
        [TestCategory("Good Data")]
        public void GoodParsingSomeBadRecordsFile()
        {
            PersonHelper.ParseFile(testDataSomeBadRecordPath);
            Assert.AreEqual("Pipe3", PersonHelper.personList[0].FirstName);
            Assert.AreEqual(1, PersonHelper.personList.Count);
        }

        [TestMethod]
        [Description("Test importing a single record")]
        [TestCategory("Good Data")]
        public void GoodParsingSingleRecord()
        {
            PersonHelper.ParseRecord(null, testDataPipe1);
            Assert.AreEqual("Pipe1", PersonHelper.personList[0].FirstName);
            Assert.AreEqual(1, PersonHelper.personList.Count);
        }

        [TestMethod]
        [Description("Test getting the test list sorted by favorite color then by last name ascending.")]
        [TestCategory("List By Order")]
        public void ListOrderByColor()
        {
            PersonHelper.ParseFiles(testDataMultipleFiles);
            List<Person> list = PersonHelper.GetSortedList(SortedBy.ColorLastNameAsc, out string header);
            Assert.AreEqual("B3", list[0].LastName);
            Assert.AreEqual(9, list.Count);
        }

        [TestMethod]
        [Description("Test getting the test list sorted by favorite color then by birth date, ascending.")]
        [TestCategory("List By Order")]
        public void ListOrderByBirthDate()
        {
            PersonHelper.ParseFiles(testDataMultipleFiles);
            List<Person> list = PersonHelper.GetSortedList(SortedBy.BirthDateAsc, out string header);
            Assert.AreEqual("E5", list[0].LastName);
            Assert.AreEqual(9, list.Count);
        }

        [TestMethod]
        [Description("Test getting the test list sorted by favorite color then by last name, descending.")]
        [TestCategory("List By Order")]
        public void ListOrderByLastName()
        {
            PersonHelper.ParseFiles(testDataMultipleFiles);
            List<Person> list = PersonHelper.GetSortedList(SortedBy.LastNameDsc, out string header);
            Assert.AreEqual("I9", list[0].LastName);
            Assert.AreEqual(9, list.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [Description("Test importing a empty filename.  Expected a null exception.")]
        [TestCategory("Bad Data")]
        public void BadParsingEmptyFile()
        {
            PersonHelper.ParseFile("");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [Description("Test importing a null filename.  Expected a null exception.")]
        [TestCategory("Bad Data")]
        public void BadParsingNullFile()
        {
            PersonHelper.ParseFile(null);
        }

        [TestMethod]
        [Description("Test importing an non-existing file.  Expected an exception below.")]
        [TestCategory("Bad Data")]
        public void BadParsingNonExistingFile()
        {
            try
            {
                PersonHelper.ParseFile(testDataNonExistPath);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("doesn't exist"));
            }
        }

        [TestMethod]
        [Description("Test importing an file with unsupported delimeter.  Expected an exception below.")]
        [TestCategory("Bad Data")]
        public void BadParsingUnsupportedFile()
        {
            try
            {
                PersonHelper.ParseFile(testDataUnsupportedPath);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("doesn't have supported delimeters"));
            }
        }

        [TestMethod]
        [Description("Test parsing a record with unsupported delimeter.  Expected no record is added.")]
        [TestCategory("Bad Data")]
        public void BadParsingBadRecord()
        {
            try
            {
                PersonHelper.ParseRecord(null, testDataMissing11);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Bad data"));
            }
            Assert.AreEqual(0, PersonHelper.personList.Count);
        }

        [TestMethod]
        [Description("Test parsing a record with missing field.  Expected no record is added.")]
        [TestCategory("Bad Data")]
        public void BadParsingMissingRecord()
        {
            try
            {
                PersonHelper.ParseRecord(null,testDataMissing11);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Bad data"));
            }
            
            Assert.AreEqual(0, PersonHelper.personList.Count);
        }
        

        [TestMethod]
        [Description("Test post a record using Rest API endpoint.  Expected 1 record is added.")]
        [TestCategory("Rest Api")]
        public void RestAPIPostGoodRecord()
        {
            string result = PersonHelper.AddRecordUsingRestAPI(testDataPipe1);
            Assert.AreEqual("Pipe1", PersonHelper.personList[0].FirstName);
            Assert.AreEqual(1, PersonHelper.personList.Count);
        }


        [TestMethod]
        [Description("Test getting the test list using Rest API endpoint and it's sorted by favorite color then by last name ascending.")]
        [TestCategory("Rest Api")]
        public void RestAPIGetByColor()
        {
            PersonHelper.ParseFiles(testDataMultipleFiles);
            List<Person> list = PersonHelper.GetSortedListUsingRestAPI(SortedBy.ColorLastNameAsc, out string header);
            Assert.AreEqual("B3", list[0].LastName);
            Assert.AreEqual(9, list.Count);
        }

        [TestMethod]
        [Description("Test getting the test list using Rest API endpoint and it's sorted by favorite color then by birth date, ascending.")]
        [TestCategory("Rest Api")]
        public void RestAPIGetByBirthDate()
        {
            PersonHelper.ParseFiles(testDataMultipleFiles);
            List<Person> list = PersonHelper.GetSortedListUsingRestAPI(SortedBy.BirthDateAsc, out string header);
            Assert.AreEqual("E5", list[0].LastName);
            Assert.AreEqual(9, list.Count);
        }

        [TestMethod]
        [Description("Test getting the test list using Rest API endpoint and it's sorted by favorite color then by last name, descending.")]
        [TestCategory("Rest Api")]
        public void RestAPIGetByName()
        {
            PersonHelper.ParseFiles(testDataMultipleFiles);
            List<Person> list = PersonHelper.GetSortedListUsingRestAPI(SortedBy.LastNameDsc, out string header);
            Assert.AreEqual("I9", list[0].LastName);
            Assert.AreEqual(9, list.Count);
        }

        [TestMethod]
        [Description("Test post a record using Rest API endpoint.  Expected 1 record is added.")]
        [TestCategory("Rest Api")]
        public void RestAPIPostBadRecord()
        {
            string result = PersonHelper.AddRecordUsingRestAPI(testDataBad10);
            Assert.IsTrue(result.Contains("Bad data"));
            Assert.AreEqual(0, PersonHelper.personList.Count);
        }

    }
}

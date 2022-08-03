using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonInfoImportTest
{
    public class TestBase
    {
        //Do Not Change the test data as results are based on it
        protected static readonly string testDataPipe1 = "A1 | Pipe1 | Pipe1A@test.com | Red | 1/11/2001";
        protected static readonly string testDataPipe2 = "C2 | Pipe2 | Pipe2C@test.com | Blue | 1/12/2001";
        protected static readonly string testDataPipe3 = "B3 | Pipe3 | Pipe3B@test.com | Blue | 1/13/2001";
        protected static readonly string testDataComma4 = "F4, Comma4, Comma4F@test.com, Green, 1/14/2001";
        protected static readonly string testDataComma5 = "E5, Comma5, omma5E@test.com, Cyan, 1/5/2001";
        protected static readonly string testDataComma6 = "D6, Comma6, Comma6D@test.com, Red, 1/6/2001";
        protected static readonly string testDataSpace7 = "H7 Space7 Space7H@test.com Violet 1/7/2001";
        protected static readonly string testDataSpace8 = "G8 Space8 Space8G@test.com White 1/8/2001";
        protected static readonly string testDataSpace9 = "I9 Space9 Space9I@test.com Brown 1/9/2001";
        protected static readonly string testDataBad10 = "K10_Bad10_Bad10K@test.com_Black_1/10/2001";
        protected static readonly string testDataMissing11 = "L11 | Missing11 | Missing11L@test.com | Orange";

        protected static string testDataPipePath = Path.GetTempFileName();
        protected static string testDataCommaPath = Path.GetTempFileName();
        protected static string testDataSpacePath = Path.GetTempFileName();
        protected static string testDataMultipleFiles = testDataPipePath + ", " + testDataCommaPath + ", " + testDataSpacePath;
        protected static string testDataNonExistPath = @"c:\dkj23a5ldsik\test.txt";
        protected static string testDataUnsupportedPath = Path.GetTempFileName();
        protected static string testDataSomeBadRecordPath = Path.GetTempFileName();

        protected static void AddTestData()
        {
            try
            {   
                File.AppendAllLines(testDataPipePath, new[] { testDataPipe1, testDataPipe2, testDataPipe3 });
                File.AppendAllLines(testDataCommaPath, new[] { testDataComma4, testDataComma5, testDataComma6 });
                File.AppendAllLines(testDataSpacePath, new[] { testDataSpace7, testDataSpace8, testDataSpace9 });
                File.AppendAllText(testDataUnsupportedPath, testDataBad10);
                File.AppendAllLines(testDataSomeBadRecordPath, new[] { testDataMissing11, testDataBad10, testDataPipe3 });
 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected static void RemoveTestData()
        {
            try
            {
                if (File.Exists(testDataPipePath))
                    File.Delete(testDataPipePath);
                if (File.Exists(testDataCommaPath))
                    File.Delete(testDataCommaPath);
                if (File.Exists(testDataSpacePath))
                    File.Delete(testDataSpacePath);
                if (File.Exists(testDataUnsupportedPath))
                    File.Delete(testDataUnsupportedPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

# Person Info Import
A console application that takes as input a file or multiple files, each with a set of records in one of the allowed formats. The imported records can be displayed in predefined sorted orders. The application also creates service endpoints at http://localhost:8080 to access the system via Rest Api Post/Get calls.

## Requirements

### 1. Allowed Formats:
* The pipe-delimited file lists each record as follows: 
LastName | FirstName | Email | FavoriteColor | DateOfBirth
* The comma-delimited file looks like this: 
LastName, FirstName, Email, FavoriteColor, DateOfBirth
* The space-delimited file looks like this: 
LastName FirstName Email FavoriteColor DateOfBirth

### 2. Outputs:
Create and display 3 different views of the data you read in:
* Output 1 – sorted by favorite color then by last name ascending.
* Output 2 – sorted by birth date, ascending.
* Output 3 – sorted by last name, descending.
Display dates in the format M/D/YYYY.

### 3. Rest API endpoints:
Within the same code base, build a standalone REST API with the following endpoints:
* POST /records - Post a single data line in any of the 3 formats supported by your existing code
* GET /records/color - returns records sorted by favorite color
* GET /records/birthdate - returns records sorted by birthdate
* GET /records/name - returns records sorted by last name
These endpoints should return JSON. To keep it simple, don't worry about using a persistent datastore.

### 4 Unit Tests: 
* Provide unit tests. The cleanliness and readability of tests is just as important as your production code.

## Developments

### 1. Assumptions:
* All the functionalities are in the same code base per requirements.  So, there's no need to create a seperate library or Web API project.
* The imported file has no header.  If it has a header with the same column names, the header will be checked and ignored.  
* All the fields of the record must in the same order: LastName, FirstName, Email, FavoriteColor, and DateOfBirth.
* The allowed delimiters (commas, pipes and spaces) do not appear anywhere in the data values themselves
* The Rest API Get sorted lists are the same as those used in the Outputs (#2 in the requirements).
* If the imported file contains some bad records with unsupported delimiters or missing field(s), they would be skipped.  The good records in that file would be still added.
* The command line can take one file or multiple files seperated by a space.
* The console app can take one file, multiple files, or load sample test files.
* Rest API based address:port is http://localhost:8080.  If the port is blocked, please change it in Helper/RestAPIHelper.cs.
* User must run the Console App or VS Studio as Administrator for Rest API host to work.

### 1. Console App:

* Main Screen:

![image](https://user-images.githubusercontent.com/110483918/182724102-9adcef19-53a6-4c02-9752-f656080d8417.png)

* Add data screen:

![image](https://user-images.githubusercontent.com/110483918/182724200-df9795d4-e94c-41d2-8455-77e064281680.png)

* Load Test data: All 3 test files in Data folder would be imported when "test" is entered in the add data screen.
```
//Test file 1: Data\TestDataComma.txt
Lehmann, Dana, danal@test.com, orange, 5/3/1982
Harrison, Vanessa, vanessah@test.com, pink, 8/1/1991
Cordova, Maria, mariac@test.com, violet, 7/22/1978

//Test file 2: Data\TestDataPipe.txt
Silver | Robin | robins@test.com | rose | 2/5/1969
Stewart | Anthony | anthonys@test.com | green | 3/29/1989
Pierce | Matthew | matthewp@test.com | blue | 11/25/1975

//Test file 3: Data\TestDataSpace.txt
Jensen Chris chrisj@test.com blue 7/21/1979
Russell Meghan meghanr@test.com pink 12/5/1987
Rankin Mark markr@test.com red 1/15/1982
```
![image](https://user-images.githubusercontent.com/110483918/182724320-10697ddc-1b76-46eb-bcc7-bab2a265c74e.png)

* Rest API screen:

![image](https://user-images.githubusercontent.com/110483918/182724509-abd6162c-c2d8-4d72-a264-fbaebf6bc63f.png)

* Rest API GET in Browser screen:

![image](https://user-images.githubusercontent.com/110483918/182724780-6c003bc7-8b15-43d1-953a-321d510c4e76.png)


### 1. Unit Tests: 
There are 20 tests to check good data, bad data, order lists, and Rest Api calls.

* Test Result Screen: All passed!

![image](https://user-images.githubusercontent.com/110483918/182926499-d1a6b081-93a7-49c2-94af-f5ace1e20352.png)

* Last Updated: 8/4/2022 By Michael Tran.




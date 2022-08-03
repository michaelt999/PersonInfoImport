# Person Info Import
A console application that takes as input a file or multiple files, each with a set of records in one of the allowed formats.

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
* All the functionalities are in the same code base.  So, there's no need to create a seperate library or Web API.
* The imported file has no header.  If it has a header with the same  column, the header will be be ignored.  
* All the fields of the record must in the same order: LastName, FirstName, Email, FavoriteColor, and DateOfBirth.
* The allowed delimiters (commas, pipes and spaces) do not appear anywhere in the data values themselves
* The Rest API Get sorted lists are the same as those used in the Outputs (#2 in the requirements).
* If the file contain some bad records with unsupported delimiters or missing field(s), they would be skipped.  The good records in the same file are still imported.
* The command line can take one file or multiple files seperated by a space.
* The console app can take one file, multiple files, or load sample test files.

### 1. Console App Screen:




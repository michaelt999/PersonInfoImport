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






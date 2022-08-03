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

### 2. Rest API endpoints:
Within the same code base, build a standalone REST API with the following endpoints:
* POST /records - Post a single data line in any of the 3 formats supported by your existing code
* GET /records/color - returns records sorted by favorite color
* GET /records/birthdate - returns records sorted by birthdate
* GET /records/name - returns records sorted by last name

### 3 Unit Tests: 
* Provide unit tests. The cleanliness and readability of tests is just as important as your production code.






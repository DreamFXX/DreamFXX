# Substance Usage Tracker App

My first personal project in progress,  using SQLite database, Dapper, MS.Extensions.Configuration -> for connectionStrings. Written in C#.

## My vision // Tasks to do

- [x] Create Models folder with model of Substance Type Habits.
  - [x] Create the model of SubstanceHabit with attributes -> Id, SubstanceTaken, DateAndTime, DoseQuantity and DoseUnits.
- [ ] Write and implement references to each operation displayed on the menu.
  - [ ] Implement SQLite Create (done), Read (done), Modify and Delete "CRUD" operations.
- [x] Work and write a code where the user will have to use DateTime format.
  - [x] Validations.
- [ ] The user shouldn't input the day of the session if(Not Admin for now).
  - [ ] The App should calculate one month average dosing values! -> CalculateAvg method.
- [ ] The user will be able to input the values including Quantity or DateTime, manually through change records method, which will be only in one version of this app.
- [x] Try to learn markdown aside from doing this App.

### Completed Tasks ✓

- [x] To show the data on the console, I used the "Spectre.Console" library.
- [x] Have a different classes in different files, synch.
  - [ ] Create more Classes to this project - (Take advantage of the OOP).
- [x] [MOST DIFFICULT PART] -> I have connectionString to SQL hidden in projects secrets and Configuration file.

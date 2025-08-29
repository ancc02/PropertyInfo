// PROJECT PropertyInfo 

TECNOLOGY: NET CORE MVC API 
SDK: net8.0

Before run the project execute the next command

Update-database in (Package Manager Console) to create database


// SQL Lite database

if you want test the project with the SQLlite provider you should take the next steps

1. uncomment line 50 to 52 in program.cs and comment line 54 to 56
2. remove all content from the Migrations folder
3. run add-migration --name in (Package Manager Console) to add initial migration
4. run Update-database in (Package Manager Console) to create DB file

//unit test Property info

1. Compile the project PropertyInfoTest
2. execute all test (rigth click over project and selet execute test)

//end-to-end test in POSTMAN

1. install POSTMAN software https://www.postman.com/downloads/
2. import the collection PropertyInfoAPI.postman_collection.json in POSTMAN
3. execute POST action "POST Authentication info to get a token"
4. test the API

//end-to-end test in SWAGGER
1. execute POST action "POST Authentication info to get a token"
3. navigate the next link https://localhost:7169/swagger/index.html
2. copy and paste the token generate in authentication section

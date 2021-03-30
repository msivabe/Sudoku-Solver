
##########################################################################################################
			Sudoku resolver 
##########################################################################################################


Technologies used:
==================
- VS 2019 IDE 
- WPF
- C#
- .Net Core 3.1  (Application can be published in Self-Dependent or Framework dependent modes for the deployment )
- Serilogs - Application logging
- MSunit test - For unit testig
- FakeItEasy - For mocking the test data
- Nuget packages - Updated the dependent framework as nuget modules

Application Information, structure and architecture
====================================================
 - For solving the sudoku, Used the backtracking algorithm with recursive function programming. Here numbers assigned to each cell after the unique validation of the number against row, column and the corresponding matrix blocks. In case none of the numbers passed the validation to assign to the cell - go for the previous cell value update like back tracking the correct value.  

 - Input data to this application passed with the following validation 
	- Unique value based on the row,column and corresponding matrix blocks
	- Valid integer in the input
	
 - Sudoku solver application structured into main modules like DataProvider, InputValidator, SudoKuResolver. 
 - SubMatrix3X3 - To hold sub blocks of the matrix values for solving the sudoku
 
 - This application developed with interface based approach for unit testing the modules. Dependency injection used for passing these interface based modules into the main process, as it helps to mock the external dependent objects for testing and plug and play kind of design. Each modules instances are created using Factory pattern
 
 - .net core 3.1 used - As the application can be published in Self-Dependent or Framework dependent modes for the deployment 
 - In this application , Serilogs used - As it can be exported to Seq and Splunk reports for easy analysis
 - MsUnit test used for unit testing the modules
 - FakeItEasy - For facking the dependency objects for the test
 - Methods are commented properly including the expected exceptions
 
 gitignore file
 ===============
 Added gitignore file for avoiding to checking libraries

 Running the application:
 ========================
 - Build/Publish "SudokuSolver" project (In Self-Dependent or Framework dependent modes) 
 - For the application to load input data for sudoku solver - Check "data.txt" file in the application root. So the application can be easily tested with different inputs.
 - "logs" folder in the application root has application related logs with roll over based on tbe date
  -In "data.txt", to have blank cell value,Set corresponding cell value to 0
 - Click on "Solve" in the UI to solve the sudoku
 - Click on "Reset" button to re initialize ui with data from the "data.txt" file.


 
Key interface information:
========================== 
 DataProvider - To read the data for the application 
   IDataProvider
   
 InputValidation - To validate the data passed for the processing 
   IInputValidator
 
 SudoKuResolver - To solve sudoku 
	Sudoku9X9Solver
	SubMatrix3X3  - To hold matrix blocks that is used for sudoku solver
 
 ##########################################################################################################
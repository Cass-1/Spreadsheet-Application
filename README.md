# CptS321-HWs

Isaac Dahle
11775278

## Structure of the Project

### Spreadsheet_Isaac_Dahle
This contains all of the UI implementation.

- **ViewModels**
This is layer of the UI has access to the actual logic layer of the application and serves as the in-between between the Views and the actual logic of the application.

- **Views**
These are the front of the UI. There is no contact between the Views and the logic layer of the application.

### SpreadsheetEngine
This contains all of the actual classes and logic for the program outside of the UI.

### SpreadsheetEngine_Tests
This contains tests for classes in the SpreadsheetEngine.

## Current State of Project
### Spreadsheet
The basic speadsheet is working and can have values inputted. Additionally, the spreadsheet supports basic references.

### ExpressionTree
Not finished. I still need to implement the following
- Converting from tokenized infix expression to tokenized postfix expression
- Creating the nodes and building the expression tree from the tokenized list of the expression.

### Integration
The Spreadsheet and the ExpressionTree are not yet integrated.
# Rename Files
This project takes a series of files and renames them according to a specific format required for end of semester submission.

![UI Image](https://github.com/Crashnorun/RenameFiles/blob/master/Rename_Files_01/Images/UI_01.jpg)
## Required inputs
* The user selects a folder directory. Ex. **C:\My Documents\2018_Fall\Student Submissions**
* Course number Ex. **ARCH1234**
* Instructor initials Ex. **AR**
* Semester abbreviation Ex. **FL18**
* Assigment name - this is dynamically taken from the folder name. It's best if the folder containing the files 
is named according to the assignment. ex. **Assignment 02 - Site Plan**
* Select student name from dropdown menu. The first item in the dropdown menu is left blank intentionally. 
The text box adjacent to the dropdown menu is disabeled untill the dropdown menu is blank. When the text box is enabled
it allows the user to enter a custom name. The newly created file will contain the same file extension as the original 
file (ex: .jpg, or .pdf)
* Copy file by default, this create a copy of the original file as to not destory the orignal file.
* Create zip file is intended to create a zip file from the newly created files. (All files combined into one zip file).

## TextBlocks
* **Current File Name**: displays the full file path with directory, of the current file being operated on
* **New File Name**: displays the proposed file name

Ex: **ARCH1235_CP_FL18_Assignment 02 - Site Plan_LastName_FirstName.jpg**

## TO DO:
* Need to add a button to skip the current file
* Need to add the ability to create a zip file from the newly created files

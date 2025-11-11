# Unity Game Design Document Creator & Analyzer

This project is one that allows a Unity developer to create a game design document(GDD) while inside the Unity editor. It creates a new window that has input forms to allow the addition of editor information. The user can export the GDD as a formatted .txt file and get an AI analysis on the project for how the GDD looks.


## Instructions for Build and Use

Steps to build and/or run the software:

1. Take the /Editor folder and put it in the /Assets folder of a Unity project
2. You can open the project by clicking on the window tab and selecting the 'Game Design Document Creator' option from the dropdown.
3. You can doc this window just like any others in Unity

Instructions for using the software:

1. Open the window up
2. Use the text inputs and button inputs to create data for your game design document
3. Once your data has been completed, click to export button at the bottom of the window
4. If you want an AI analysis, you will need to place an API key from https://openrouter.ai/
5. Note that the hard coded AI model that is used is a free model, so you may run into rate limits when trying to get your analysis. If that happens, keep trying to get your analysis until your response is successful

## Development Environment 

To recreate the development environment, you need the following software and/or libraries with the specified versions:

* If you don't have Unity, download it onto your computer (This software uses Unity v6000.2.9f1)
* Create a new Unity project from the Unity hub or open an existing project
* place the /Editor folder in your project
* The files can be edited from there

## Useful Websites to Learn More

I found these websites useful in developing this software:

* https://openrouter.ai/
* https://learn.unity.com/

## Future Work

The following items I plan to fix, improve, and/or add to this project in the future:

* [ ] I will continue adding more GDD elements to the window creator to allow for more robust GDDs
* [ ] I would like to allow for users to select which model they would like to use (with warnings on models that cost money)
* [ ] I have some formatting bugs to fix with how the AI response looks in the saved file

# How the time controller works

## Time Controller
Put this into the player and write controllers for ```timeController.IncrementTime(x);``` and ```      timeController.setIsControlling(x);``` to control the time controller

## Time Recorder
throw this onto an object, it will work. 
### If you want script compatability:
* Set ```useExternalComponents``` to ```true``` 
* Put the script into the ```componentsToSave``` list. 
* In the component you added, make a ```getData()``` function that returns type ```Data``` and ```setData(Data data)``` function
* Create a class that extends ```TimeData``` and has instance variables of what you want to save
* Implement the actual code in the ```getData``` and ```setData```
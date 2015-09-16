# HelloAutoCADPlugIn
PlugIn for AutoCAD to change some properties of layers and some objects.

Develeped for AutoCAD 2016, with API ObjectARX2016 for any CPU, in Visual Studio 2015

To load in AutoCAD use command NETLOAD, and select HelloAutoCADPlugIn.dll
Start in command line, with command CHANGEPROPERTIES

To debug in AutoCAD need change application reference on yours acad.exe, and set at StartUp project.

PlugIn can change:
Layer:
- Visibility
- Name
- Color

Line:
- Start point
- End point

Circe:
- Coordinates of centre
- Radius

Point:
- Coordinates

Can change even layer blocked or objects frozen.
All changes come with one transaction.

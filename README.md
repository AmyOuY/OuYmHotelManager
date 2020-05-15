# Hotel Management System

A desktop app that runs a hotel management system to handle User authorization and permission, manage Rooms, Clients, CheckIn and CheckOut. A middle WebAPI layer that can service a desktop application was created to allow the app to grow. Technologies that involved in this app development include: Dependency Injection(Bootstrapper), ASP.NET MVC, Git, SQL database(SSDT), WebAPI with authentication, Async, WPF(with MVVM using Caliburn Micro) that logs into the API, JavaScript, HTML, CSS.

--------Swagger was used to document and demonstrate the WebAPI--------
<img src="./Images/WebAPI.png">


--------Successful login allows the display of app menus on top of the shell window--------
<img src="./Images/login.png">


--------Only User with Admin role is allowed to manage the User information--------
<img src="./Images/user.png">


--------Unauthorized User is banned from manipulating the User information--------
<img src="./Images/unauthorizedUser.png">


--------Room View displays all room information and allows Add, Edit and Remove room information from the SQL database--------
<img src="./Images/room.png">


--------Client View displays all client information and allows Add, Edit and Remove client information from the SQL database. Select a client in the data grid and hit the "Switch To Check In" button allows switching to the CheckIn View and automatically filling in available CheckIn information for the selected client--------
<img src="./Images/client.png">


--------Room View displays all room information and allows Add, Edit and Remove room information from the SQL database--------
<img src="./Images/room.png">


--------CheckIn View allows displaying information for already checked-in client if typing in the client name. For new checkIn, client needs to register in Client view first and then fill in required information to check in. CheckIn View also allows clearing the filled-in fields and removing CheckIn information from the SQL database--------
<img src="./Images/checkedIn.png">


--------Select one room type from the Type drop-down allows display of the available room numbers in the Number drop-down--------
<img src="./Images/checkIn.png">


--------CheckOut View allows display of the checkOut information when typing in the room number that needs to check out--------
<img src="./Images/checkedOut.png">


--------A warning message will show up if typing in wrong room number for CheckOut--------
<img src="./Images/checkOut.png">

# Prin-Z!

###Background
In KidZania, children are able to free roam the city to experience work life. Various photos will be taken throughout the establishments which can be bought in the form of hardcopy, digital, magnet, etc. Customers can make their order in the photo booth portal available onsite. Although, there are some design and workflow flaws such as no product description, shopping cart, and order duplication bug.
Throughout the business operations, the staffs are facing limitations in some areas. Marketing department requests to allow managing souvenir, while the printing staffs request to automate the printing process. In addition, the printing operations should be monitored in order to allow both staffs and customers to be aware of their respective orders.


###Description
Prin-Z! consists of three different systems. First of all, for KidZania's existing Customer Portal, which is set up with ASP.NET WebForm, the team has introduced more features i.e. shopping cart, enabled retrieval product details from database, resolved navigation issues in the current system, created responsive designs for mobile and tablet users, as well as generated QR code for order code. 
Secondly, the team has developed an admin-portal with ASP.NET Web API and MySQL database, which is going to facilitate marketing and printing staff by introducing a souvenir management system with CRUD functions as well as GST calculation feature, export data, etc; and developing automated printing system for hardcopy orders, including printer management, printing status monitoring, exception handling features. Besides, the team has converted the existing windows application of keychain & magnet printing system to web application with photo calibration features. 
Last but not least, the team has also developed a notification system to remind customers to collect orders once all the souvenir items are ready.
With Prin-Z!, improved printing productivity, less labour demand for printing, better user experience with shorter waiting queue, and increased revenue for souvenir sales are expected.

####Customer Portal
The first system is a Customer Portal that allows users to order souvenirs. In this system, the following functions have to be completed:
1.	Retrieve and display souvenir products from database 
2.	Calculate total cost in the back end 
3.	Validation as well as prevent clicking the back button
4.	Responsive Web Design
5.	Display Order code using QR Code

####Admin Portal
The second system is an Admin Portal for the marketing staff to manage souvenir items and for the printing staff to handle print jobs. The features covered in this system are: 
1.	Introduce a new souvenir management system 
2.	Automate the existing manual A5 hardcopy printing system 
3.	Develop an order status monitoring system 
4.	Upgrade the existing console application into a web application
5.	Sales Dashboard
6.	Authorization and authentication

####Notification System
Lastly, is the Order Notification system to display the order status for customers. The system allows customers to view the order in two categories, orders that are being processed and orders that are ready to collect. Once the customer has collected his order, the staff will be able to “finish” the order by simply checking a box.

###Potential Opportunities 
Prin-Z! will be deployed and put into actual usage in Kuala Lumpur KidZania company, and it is expected to improve the productivity of printing operations, relieve the workload for staff, reduce labour demand as well as provide better customer experience with one of its major feature, Automated printing system. If all the expectations are met in the future, this project can be further introduced to other branches of KidZania all around the world, such as Japan, USA, etc.
Apart from being implemented in KidZania, the automated printing system is practical in other real-life situations, i.e. school / office printing room. For example, with Prin-Z!, it will auto create the printing jobs and assign them to the optimal printers to maximise the productivity of printers.
As an E-Commerce web application with specialised features, i.e. shopping cart, admin dashboard; some of these features can also be implemented in other similar web applications that require the fundamental CRUD functions with neat layout. 
For further improvements, some of the features for souvenir management system had not been developed due to the time constraint, for an instance, special conditions of products, i.e. colour, size, etc. Although it is not in the list of requirements, it would be useful in the future if KidZania would like to introduce more customised products, i.e. cup with photo, T-Shirt with photo, etc.

 of Contents









Introduction
	E-receipt’s conceptual goal is to provide customers with a digital receipt repository of all their purchases.  The project has two different design portions to achieve this goal; a pure software end (which will be used to register users, hold customer information, and display unique user data), and a series of hardware components combined to scan receipt documents and transmit its digital representation to the software end. 
	The process will be completely automated, meaning as soon as a customer inserts a receipt to be scanned, the data will be sent to the website hosting a database for storage. As soon as the process is completed the customer would be able to see their most recent and previous receipt submissions on the website and the corresponding link to download a copy for their own storage.
	To achieve this degree of automation several systems will have to be created. That must be able to communicate with one another to know when to start and stop certain operations. Some particular technical challenges for both the hardware and software are: creating a hardware component to take separate pictures of a receipt and send this array of information to a website script to combine the images and add it to the specific tables in a database. This process will be broken down into its subcomponents throughout the remainder of this report. 
	







Application Flow Diagram
	








Functional Bock Diagram


	The diagram shown in Figure 2 is a block level interpretation of the flow chart from Figure 1. Each block will be shown in greater detail below.


Block Diagram Camera and Description
	The camera receives its instructions from the microcontroller, only powering on and taking pictures when the sensor detects a receipt. Several commands must be sent after taking the picture for storage.  After taking the picture, its file size read and used to read the file content and save to a certain address on the SD card. This save process is completed once the Read JPEG File Content returns a FF D9 hex value.  It will continue taking and saving pictures until the sensor has returned to its maximum value.   Once the image data is collected, it is then transferred to a terminal PC that will up loaded from a web script. 
The files have a specific naming convention, so each user is uploaded accordingly. The SD class on arduino uses File 8.3 naming conventions. This limits the length of a file name to 8 characters. To keep track of multiple receipts we use a currentReceiptID and currentPictureID variables. These variables will be increment accordingly. For currentReceiptID it will be incremented only after the entire receipt has been printed and captured by the camera, as for currentPictureID it will be incremented for each picture for the currentReceiptID. The naming convention for a file is: "currentReceiptID + userName + currentPictureID". Due to the 8 character limit this would force uses when registering to have a maximum username of 6 characters. 





Block Diagram Image Upload Web Script
To save an image from the SD card to the web application several steps are required. Using the naming convention from the SD file we are able to parse out usernames of each file, the amount of files on the card, and the images for a specific receipt. By iterating through the SD directory, and creating a substring of each file names, currentReceiptID, the maximum ID is found by storing the highest value throughout the total iteration. This will control the for-loop for how much iteration it will run based on the maximum currentReceiptID found. Inside the for-loop is a while-loop that will only terminates after the next file in SD Directory no longer has a matching currentReceiptID value. As long as the currentReceiptID Value is the same it will be added to a Bitmap array list. An array list is used due to not knowing how many images are associated with a specific receipt number. Once the next file currentReceiptID value has changed, the while loop is stepped out of and the Array List sends its data to an array, this array is used as parameter for the Combine Image method. The combine image method will return a file location of the unified image. To upload the image to the website a Web client instance is created; web client requires a URL that allows data to be posted to, and the file location. Web client will return successfully with HTML data from the webpage. 





Block Diagram Image Unification and Description
	The camera is taking separate pictures of the receipt. When transmitting the data the web application must create a new combined image from these individual pictures. Since the camera's resolution is constant at 320 x 240. The final image's width will still be the same; the height will be multiplied by 640 by the amount of pictures taken to create the combined images final height and width settings. This only creates a blank image file each picture must be copied into the new final image picture and then next sequential picture copied directly below it and so on. The code below copies each individual pixel of its current x and y coordinates and maps that to the final image. When completed the image is unified. 




Block Diagram Web Application and User Database
	The database is comprised of three tables:  Customer, Receipt, CustomerReceipts. USER_ID and RECEIPT_ID are the primary keys in there receptive tables. A new row is added when a user is created and receipt is created. It allows you to search for your own ID. With every upload that is created a new row is created in the CustmoerReceipt Table, as well as the RECEIPT_ID.  The USER_ID portion for this application/database includes the USER_NAME, FIRST_NAME, LAST_NAME, EMAIL, SALT_PASSWORD, and HASED_PASSWORD. The SALT_PASSWORD and HASED_PASSWORD are used for security purposes for the individual. The RECEIPT_ID portion for this application/database includes the UPLOAD_DATE, and the RECEIPT_URL. Each new USER_ID and RECEIPT_ID is automatically saved in the CustomerReceipts, which allows one to find all of their receipts by just clicking on their USER_ID.  


	
Save Receipt to Database
Images are saved when a postback occurs from the webscript. The webscript saves images in the naming convention of 'username_idValue'. First the username is parsed out using a substring from the start of the string to the index of '_' character. Since uploading an image will affect two tables of the database Receipt, CustomerReceipts, we must find the maximum value of Receipt_ID's from the Receipt Table. Using the maximum function from LINQ to SQL we obtain the highest value in the table currently. This value is stored in a local variable. A string save path is then created:
"~IMG/receiptImage/userName/receipt/maxReceiptIDValue + 1"
This is the location of where the image will be saved on the web server.  A Check will also be made to ensure that the directory for the image exists. Once the directory has been checked or made the file is saved in that location. We now need to update the database, so the user will be informed when this receipt was uploaded, and the URL of where to view / download it. The Receipt Table will auto increment the receiptID value, but needs to know the save path and current Date. The CustomerReceipt Table also requires a USER_ID, and RECEIPT_ID. The USER_ID is found from querying the Customer table and finding the USER_ID that matches userName. Customer Receipt is then updated with the USER_ID returned from the query and the ReceiptID is used from the maxReceiptIDValue + 1. 

 
Figure 8: Save Image to Database






Block Diagram Front End Web Application
	The front end application is the product of many back end systems put in place to give the user a regular website experience. The users are brought to a login page, where they can either login using their credentials or register to the website. When logged in the user is displayed their unique receipt data where they can ort by day, month, year, or all. The website is created using CSS, ASP.NET, HTML, and C#. The images and website frame are based on a Adobe Illustration rendering. 
















 Login Page

	
To login users input their credentials, if they are blank and press submit a error message will appear informing the user data is required. When using correct credentials the database confirms that the username and encrypted password matches what was inputted and brought to the personal site. If the user does not exist, they must first register.




Registration
Users input their data for username, first name, last name, email, password. Each field is required if one is not inputted and error message is displayed. If there is not a match for the passwords an error message is also displayed. If the user name is already taken the user is informed or if the username is longer than 6 characters.
These fields are checked to first ensure that the data is correct, and the username is queried throughout the customer table of the database to ensure it does not exist. Upon completion and successful registration the user is brought to the logged in view. 






Personal Page
The personal page displays unique user data by using either the day, or month, or year, or all. Currently the only implementation shown is the all feature. This is done by querying through the database using the username to find the hardware ID for that user. Knowing the hardware ID a query of the receipt table to display only information for that ID is shown. Later revisions will also be specific on the date range as well as the hardware ID.

  


Conclusion
	Many of the feasibility challenges presented at the beginning of the projects inception have been solved and proven that the overall goal of the project is possible. The combination of hardware and software proved complicated and difficult, and took many hours of research in different langauges to find the optimal solution. While only some features of the E-Receipt will be shown at the demonstration we believe that the overall goal is achievable as well. 
References and Appendix

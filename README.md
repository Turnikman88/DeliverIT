# DeliverIT

**Freight Forwarding Management System** 


# Project Description 
Your task is to develop **DeliverIT** - a web application that serves the needs of a 
freight forwarding company. 
**DeliverIT**'s customers can place orders on international shopping sites that don’t 
provide delivery to their location (like Amazon.de or eBay.com) and have their 
parcels delivered either to the company's warehouses or their own address. 
**DeliverIT** has two types of users - customers and employees. The customers can 
trach the status of their parcels. The employees have a bit more capabilities. They 
can add new parcels to the system group them in shipments and track their location 
and status. 
# Functional Requirements 

### Entities 
* Each customer must have a first and last name, email, and address for 
delivery (country, city, and a street name). 
* First name and last name must be between 2 and 20 symbols. 
* Email must be valid email and unique in the system. 
* Each employee must have a first and last name, email, and may have an 
address. 
* First name and last name must be between 2 and 20 symbols. 
* Email must be valid email and unique in the system. 
* Each parcel must have a customer who purchased it, a warehouse to which 
it should be delivered, weight and a category. Also, parcels have a field that 
indicates whether the customer wants the parcel delivered to his address or 
he will pick it up from the warehouse. By default, this field will be “pick up 
from warehouse”. 
* Each warehouse must have an address (country, city, and street name). 
* Each category must have a name e.g. Electronics, Clothing, Medical, etc. 
* Category name must be unique and between 2 and 20 symbols. 
* One shipment must have an origin warehouse, a destination warehouse, a 
departure and arrival date, a status (the status is one of preparing, on the 
way, completed), and a collection of all the parcels that will be delivered with 
this shipment. A shipment without any parcels cannot depart. 
* Each country must have a name. 
* Each city must have a name and a country. 

### Public Part 
The public part must be accessible without authentication i.e. for anonymous users. 
Anonymous users must be able to see how many customers DeliverIT has and what 
and where are the available warehouse locations. 
Also, anonymous users must have the ability to register and login.
Private part 
Accessible only if the user is authenticated. 
Customers must be able to see their past parcels as well as the parcels they have on 
the way. 
Customers should have the ability to see the status of the shipment that holds a 
given parcel of theirs. 
Customers should have the ability to pick between “pick up” or “delivery” for each 
parcel, but only if it has not departed yet (the status of its shipment is not 
“completed”).
Administrative part 
Accessible to employees only. 
* Employees must be able to list/create/modify parcels. 
* Employees must be able to list/create/modify shipments. 
* Employees must be able to add/remove parcels from a shipment.
* Employees must be able to see shipments that are on the way. 
* Employees should be able to list/create/modify warehouses. 
* Employees should be able to see which the next arriving shipment is for a given 
warehouse. 

# Additional Feautures
* AJAX reload on all tables
* AJAX Search
* Warehouse auto-generated locations with google maps pin on Index page, everytime you create a new one
* Warehouse auto-generated navigation button with google maps pin on Employee page
* Settings menu to edit your profile information
* Showing only the cities and towns that are in the correct country drop-down menu
* Contact us automated email send

### REST API 
To provide other developers with your service, you need to develop a REST API. It 
should leverage HTTP as a transport protocol and clear text JSON for the request and 
response payloads. 
A great API is nothing without a great documentation. The documentation holds the 
information that is required to successfully consume and integrate with an API. You 
must use Swagger to document yours. 
The REST API provides the following capabilities: 
1. Countries 
* Read operations (must) 
2. Cities 
* Read operations (must) 
3. Warehouses 
* Read operations (must) 
* Create, Update, Delete operations (should) 
4. Shipments 
* CRUD operations (must) 
* Filter by warehouse (must) 
* Filter by customer (should) 
5. Parcels 
* CRUD operations (must) 
* Filter by weight (must) 
* Filter by customer (must) 
* Filter by warehouse (must) 
* Filter by category (must)  
* Filter by multiple criteria (e.g., customer and category) (should) 
* Sort by weight or arrival date (should) 
* Sort by weight and arrival date (could) 
6. Customers 
- CRUD Operations (must) 
- Search by email (full or part of an email) (must) 
- Search by first/last name (must) 
- List customer’s incoming parcels (should) 
- Search by multiple criteria (should)  
- Search all fields from one word (e.g., “john” will search in the email, first 
and last name fields) (could) 

# Database Diagram
![Diagram](DeliverIT.Web\wwwroot\images\db-diagram.png)

### Use Cases 

* Basic Order 

> You’ve ordered something online from a foreign shopping site and you wish to leave 
handling, customs fees, and transportation to somebody else. Here is where 
DeliverIT comes in. Their main warehouse is in Bulgaria, for example, and they have 
other warehouses in different countries, like Germany, Spain, USA, etc. When placing 
your order, you address it to a DeliverIT warehouse. When the package arrives, the 
employees see who it is for and create a parcel in the system. If your parcel appears 
on the site, then it has arrived at the origin warehouse successfully and the next step 
is for it to depart to the destination warehouse.


* Checking the status on an order 

> A customer has order something and wants to check its status. He logs on into the 
application and heads to the “My Orders” panel. He has a lot of other orders and that 
is why there is a button “Filter By status… on the way”. He selects it and sees his 
order.

* A new parcel arrives to a warehouse 
> A new parcel has arrived to one of the warehouses, let us say the one in Dublin. The 
employees see the label on it, it says “John Doe 1337”. They look up the id “1337” to 
verify it belongs to “John Doe”. Once they have made sure all the information is 
correct, they look for the next shipment that goes to John Doe’s country (for 
example, Bulgaria). There is one that still has free room for more parcels, and they 
assign the newly arrived parcel to it. 


* A shipment departs 
> The employees at a warehouse decide a shipment is ready to depart and they set its 
“departure time” to the corresponding date. Once the shipment reaches its 
destination, the employees there set its “arrival date”. That way customers can keep 
up with different parcels. If they log on and see that the status of their order is 
“preparing”, they know that the shipment that carries that order has not departed 
yet. In other words, the status of a parcel is derived from the departure and arrival 
dates of its shipment. 


### Technical Requirements 
1. General 
* Follow OOP principles when coding 
* Follow KISS, SOLID, DRY principles when coding 
* Follow REST API design best practices when designing the REST API (see 
Appendix) 
* Use tiered project structure (separate the application in layers) 
* The service layer (i.e., "business" functionality) must have at least 80% unit 
test code coverage 
* Follow BDD when writing unit tests 
* You should implement proper exception handling and propagation 
* Try to think ahead. When developing something, think – “How hard would it 
be to change/modify this later?”
2. Database 
> The data of the application must be stored in a relational database. You need to 
identify the core domain objects and model their relationships accordingly. 
Database structure should avoid data duplication and empty data (normalize your 
database). 
Your repository must include two scripts – one to create the database and one to fill 
it with data. 

3. Git 
> Commits in the GitLab repository should give a good overview of how the project 
was developed, which features were created first and the people who contributed. 
Contributions from all team members must be evident through the git commit 
history! The repository must contain the complete application source code and any 
scripts (database scripts, for example). 
Provide a link to a GitLab repository with the following information in the 
README.md file: 
* Project description 
* Link to the Swagger documentation (must)
* Link to the hosted project (if hosted online) 
* Instructions how to setup and run the project locally 
* Images of the database relations (must)

## 🏁 Areas
* Public - users are able to register, find warehouse location and know how many customers we have
* Private - available after registration, can see orders, can edit profile
* Administrative - available for employees only

## 👷 Built with:
* ASP .NET Core
* MsSQL Server
* Entity Framework
* Swagger
* Javascript/jQuery/AJAX
* HTML5/CSS/Bootstrap

## 🤝 Authors
* Georgi Petrov [GitHub](https://github.com/Turnikman88) | [LinkedIn](https://www.linkedin.com/in/georgi-petrov-88a259225/)

![Georgi Petrov](images\georgi-avatar.jpg)

* Kalin Balimezov [GitHub](https://github.com/balimka) | [LinkedIn](https://linkedin.com/in/kalin-balimezov-6755a8204)

![Kalin Balimezov](images\kalin-avatar.jpg)

## 📃 License
*This project was developed for educational purposes  __only__ during Telerik Academy Program C# 35 (Jul - Dec 2021)*

## 🤖 Project status
☑️ **DONE** 
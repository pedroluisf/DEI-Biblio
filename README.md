# DEI Biblio
Academic ASP.NET Project for a online book store

This is a project that implements an online book store and allow users to perform purchases.
The company is called IDEI Biblio and is focused on the sale of books and magazines. This company is implemented in .NET

One important goal of this project is to allow communication between projects that implement different technologies. Communication is done from .NET to PHP, but also the other way around, PHP to .NET.

Customers will have to be logged in in order to perform purchases. The customer adds the selected products on to a cart. This cart is persistent, therefore even if the customer logs out, the information is kept until the next successful login.
There is an Admin area that allows for product maintenance.

The company IDEI Biblio uses the companies LogisticaSA and ShippingAll for shipping the orders. 
These companies are implemented on a .NET and LAMP platform (respectively). They provide a web service that will return a price for the shipping depending on the package.

There is also a partnership with the company AnalisaMercados which will provide daily market analysis on the competition and supply with a list of products whose prices should be updated. 
This will be done in a LAMP platform and consists in a single web service that receives a product list.

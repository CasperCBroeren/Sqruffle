# Sqruffle
This project is a test area for working in an event based enterprise environment. Below are design choices 


## No Object orientation
One should avoid Object Orientation as much as possible. Composition over inherintance

## Structure
Onion, N-tier, clean are all hard to grasp and multi interpertable terms. Best is to define your own and explain instead of asume
Inner layer
- Domain = holds all domain objects and logic. Holds no third party references because these objects need to be transferable
- Data = integrates the domain to persistance (DB, File, Services) holds a lot of third party references
- Utilities = holds utilities to make life easier, holds some third party reference (if its really needed) 
Middle layer
- Application = the whole application as a virtual non runnable. This gives all the outerlayer the same setup
Outer layer
- Web = Accessible part of the application through the web
- Service = Processor of commands, events and features

!! all code is also seperated in slices/subjects/features so if we're doing products. All data related to products is put in this folder/namespace


## Feature 
A product is a flexibel structure of not nullable properties which can be enriched with *features*. An *feature* is a set of data to which *reactions* can be run against.
Since in a lot of domains prodcuts are never fixed but are highly flexible to the business needs, we need to be adoptable to cater to this.
We want to limit the amount of optional fields in db and have composable solution. 

For instance if we want to create a product with a expiration we can just add only that *feature*. If we want to add a product with PeriodiYield and expiration we can just add those *features*  all products are evenly possible and valid.
If one of the products are in need of a new *feature* during their existince it can be applied and adjusted. This mitigates problems with clasical inheritance product solutions
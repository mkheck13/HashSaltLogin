### Day one scaffolding the file of our API, DB creation, DTOs & IActionResult

## Goals Create an Account and Login

# Intro to DTO

DTO stands for Data Transfer Object

DTO's are lightweight focused objects to transfer data between layers or systems securely
This helps limit data exposure.

### Day Two Account Creation, Hash & Salt

## What is Hash & Salt

# Hash:

A Hash is the result of running Data (Password in our case) through a special algorithm that converts it into a fixed length string of characters

example "MyPassword" = "5asjnfas87sf6a8sdfnasdf

Hash is "one-way" meaning that you can't easily convert it back to the original data.

## Salt:

So salt is a random piece of data added to the end of our password before hashing
 
Example "MyPassword" = "MyPassword+RandomSalt"

The salt insures that no two users have the same password to hash. We always Salt before we Hash.

### Day Three Covering IActionResult , CORS, and Login Logic

## Intro to the return type IActionResult.

IActionResult lets us Controller our HTTP status 200 Ok 4oo Unothorized as well as Error and then 500 Internal Error


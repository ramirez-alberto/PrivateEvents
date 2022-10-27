# Project: Private Eventa
## Briefing
You want to build a site similar to a private Eventbrite which allows users to 
create events and then manage user signups.

A user can create events. 
A user can attend many events. 
An event can be attended by many users. 
Events take place at a specific date and at a location (which you can just store
 as a string, like “Andy’s House”).

## Requirements
`Stack`: Net 6 , MYSQL or MSSQL, Angular
### Stories    
    As a User
    I need a service for create events 
    So that I can organize events
**Acceptance Criteria**
* Need a way to list all events created


## Design
**To Do**

[ ] Create the infrastructure

[ ] Home Page, Register and Login Form.

[ ] Create Event View and List All Events View

[ ] Follow Event capabilities 



### Models

***Users***

| Column      | DataType  | Constraint                   |  
|-------------|-----------|------------------------------|
| Userid      | int       | unique                       |
| Name        | string    | not-null , 10 char max       |
| Email       | string    | not-null , 20 char max       |
| Password    | string    | not-null , min:8 char max:30 |
| CreatedDate | date      | not-null                     |

***Events***

| Column        | DataType | Constraint              |  
|---------------|----------|-------------------------|
| EventId       | int      | unique                  |
| Name          | string   | not-null, max:20 char   |
| Description   | string   | nullable                |
| Date          | date     | not-null                |
| Location      | string   | not-null, max: 30 char  |
| CreatedDate   | date     | not-null                |

***Attendees***

| Column  | DataType  | Constraint  |  
|---------|-----------|-------------|
| EventId | int       | not-null    |
| UserId  | int       | not-null    |


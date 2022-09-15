Feature: AdoptionsRequestsServiceTests
As a Developer
I want to add new Adoption Requests through API
So that it is available when the user make a adoption requests
	Background: 
		Given The Endpoint https://localhost:5001/api/v1/AdoptionsRequests is available
		And A User is already stored for AdoptionsRequests
		| Id | Type | UserNick	| Ruc      	| Dni		| Phone     | Email            		| Name      | LastName  | Pass		|
		| 1  | VET  | Frank 	| A12345 	| 70258688	| 946401234 | frank@outlook.com 	| Francisco | Voularte  |  123456	|
		And A Second User is already stored for AdoptionsRequests
		| Id | Type | UserNick	| Ruc 		| Dni		| Phone 	| Email             	| Name   	| LastName 	| Pass 		|
		| 2  | VET  | Hector	|     		|    		|       	| Hector@outlook.com	| Hector 	| Voularte 	|  123456 	|
		And A Third User is already stored for AdoptionsRequests
		| Id | Type | UserNick	| Ruc      	| Dni      	| Phone     | Email             	| Name      | LastName 	| Pass    	|
		| 3  | VET  | Pablin	| A12345 	| 70258688 	| 946401234 | pablin@outlook.com 	| Pablo 	| Marmol 	|  123456 	|
		And A Report already stored for AdoptionsRequests
		| UserId | Type  | Description | DateTime         |
		| 3      | Acoso | Testing     | 14/09/2022 11:35 |
		And A Pet already stored for AdoptionsRequests
		| Id | Type | Name | Attention | Race 		| Age 	| isAdopted	| UserId	| PublicationId |
		| 101| Cat  | Lolo |Required   | Catitus 	| 	2   | false		| 1 		| 1 			|
		And A Publication already stored for AdoptionsRequests
		| Id | UserId 	| DateTime 		| PetId | Comment 		| 
		| 1  | 2  		| 29/09/2021    | 101  	| Comentario	|
    	
@adoptionsrequests-adding
	Scenario: A AdoptionsRequests is sent from not Authenticated User
		When A adoption request is sent from not Authenticated User
			| Message | Status  | UserIdFrom | UserIdAt | PublicationId |
			| hello   | pending | 2          | 1        |1              |
		Then A Response with Message "This user is not authenticated." and Status 400 is received
	Scenario: A AdoptionsRequests is sent from Reported User
		When A adoption request is sent from Reported User
			| Message | Status  | UserIdFrom | UserIdAt | PublicationId |
			| hello   | pending | 3          | 1        |1              |
		Then A Response with Message "This user has at least one report." and Status 400 is received
	Scenario: A AdoptionsRequests is sent from Authenticated and not Reported User 
		When A adoption request is sent
			| Message | Status  | UserIdFrom | UserIdAt | PublicationId |
			| hello   | pending | 1          | 2        |1              |
		Then A Response with Status 200 is received
	Scenario: A AdoptionsRequests is sent from Authenticated and not Reported User another time
		When A adoption request is sent
			| Message | Status  | UserIdFrom | UserIdAt | PublicationId |
			| hello   | pending | 1          | 2        |1              |
		Then A Response with Message "This user already has an existing adoption request." and Status 400 is received
		
#	Scenario: Add Adoption Request with empty data
#		When A post adoption request is sent
#		| Message | Status  |
#		| hello   |  	      |
#		Then AAdoptionRequests With Status 400 is received
	
#	Scenario: Add Adoption Request with same data
#		When A post adoption request is sent
#		| Message | Status  | UserIdFrom | UserId | PublicationId |
#		| hello   | pending | 2          |1       |1              |
#		Then AAdoptionRequests With Status 200 is received
#		
#	Scenario: Delete a adoptions request dont available
#		When An a delete request of adoptions requests is sent   
#		| Message | Status  | UserIdFrom | UserId | PublicationId |
#		| hello   | pending | 2          |2       |18              |
#		Then a response with status 400 is received
#	    
#	Scenario: Database update its information when there is a new candidate for adopting
#		Given the endpoint https://localhost:5001/api/v1/AdoptionsRequests/1 is available
#		And A User is already stored for AdoptionsRequests
#		| Id | Type | UserNick | Pass     | Ruc      | Dni      | Phone     | Email             | Name      | LastName | DistrictId |
#		| 2  | client  | an    | Password | A12345rf | 70258688 | 946401234 |ana@gmail.com | Ana | Araoz | 1          |
#		When An update  adoption request is sent    
#		| Message | Status  | UserIdFrom | UserId | PublicationId |
#		| hello   | pending | 2          |1       |1              |
#		Then a response with status 200 is received
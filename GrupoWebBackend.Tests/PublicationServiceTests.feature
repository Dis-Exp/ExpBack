Feature: PublicationServiceTests
As a Developer
I wan to add a new Publication through API
So that it is available when wanting to see my publications.
    Background: 
        Given the endpoint https://localhost:5001/api/v1/publications is available
        And A User is already stored for Publication
        | Id | Type | UserNick | Ruc    | Dni      | Phone     | Email             | Name      | LastName | Pass    |
        | 1  | VET  | Frank    | A12345 | 70258688 | 946401234 | frank@outlook.com | Francisco | Voularte |  123456 |
        And A User is already stored for Publication
        | Id | Type | UserNick | Ruc    | Dni      | Phone     | Email             | Name      | LastName | Pass    |
        | 2  | VET  | Frank2   | A12345 | 70258688 | 946401234 | frank1@outlook.com | Francisco | Voularte|  123456 |
        And A User is already stored for Publication
        | Id | Type | UserNick | Ruc    | Dni      | Phone     | Email              | Name      | LastName | Pass    |
        | 3  | VET  | Frank3   | A12345 | 70258688 | 946401234 | frank2@outlook.com | Francisco | Voularte |  123456 |
        And A User is already stored for Publication
        | Id | Type | UserNick | Ruc    | Dni      | Phone     | Email              | Name      | LastName | Pass    |
        | 4  | VET  | Frank4   | A12345 | 70258688 | 946401234 | frank3@outlook.com | Francisco | Voularte |  123456 |
        And a Report is already stored for Publication
        | UserId | Type  | Description | DateTime         |
        | 2      | Acoso | Testing     | 14/09/2022 11:35 |
        And a Subscription is already stored for Publication
        | UserId | NumPosts | Expired | StartDate  | EndDate   |
        | 1      | 5    | 0 | 14-09-2022    | 25-09-2022   |
        And a Pet is already stored for Publication
        | Id  | Type | Name | Attention | Race    | Age   | isAdopted | UserId | PublicationId |
        | 101 | Cat  | Lolo |Required   | Catitus | 	2 | false     | 1      | 1             |
        And a Pet is already stored for Publication
        | Id  | Type | Name | Attention | Race    | Age | isAdopted | UserId | PublicationId |
        | 102 | Dog  | Lolo |Required   | Catitus | 	2 | false     | 1      | 1             |
 
    @publication-adding
    Scenario: Post a publication 
        When A publication is sent
        | PetId | UserId | DateTime         | Comment                |
        | 101   | 1      | 29/09/2021 16:20 | This is a test comment |    
        Then a response with status 200 is received
    Scenario: Post a publication of the same pet
        When A publication is sent
          | PetId | UserId | DateTime         | Comment                |
          | 101   | 1      | 29/09/2021 16:20 | This is a test comment |    
        Then a response with status 400 is received
    Scenario: Post a publication from reported user
        When A publication is sent
        | PetId | UserId | DateTime         | Comment                |
        | 102   | 2      | 29/09/2021 16:20 | This is a test comment | 
        Then a response with Message "This user has at least one report." and status 400 is received
    Scenario: Post a publication from not subscribed user
        When A publication is sent
        | PetId | UserId | DateTime         | Comment                |
        | 102   | 3      | 29/09/2021 16:20 | This is a test comment | 
        Then a response with Message "This user not have a subscription." and status 400 is received
      
#    Scenario: Post a publication with incorrect json body
#        When A publication is sent
#          | petId | UserId | DateTime         | Comment                |
#          | 1     | 1      | 29/09/2021 16:20 | This is a test comment |    
#        Then a response with status 400 is received
#    #NOTA-SOLO FUNCIONA SI LA BASE DE DATOS TIENE LOS MISMOS DATOS DEL USER ID Y PET ID Y PUBLICATION ID
#    Scenario: Update a publication with correct id and body
#        Given the endpoint https://localhost:5001/api/v1/publications/2 is available
#        When An update  publication is sent    
#          | PetId | UserId | DateTime     | Comment                        |                    
#          | 2     | 4      | date updated | This is a test comment updated |    
#        Then a response with status 200 is received  
#         
#    Scenario: Update a publication with wrong publication id
#        Given the endpoint https://localhost:5001/api/v1/publications/1 is not available
#        When An update  publication is sent    
#         | PetId | UserId | DateTime         | Comment                        |                    
#         | 2      | 0     | date updated     | This is a test comment updated |    
#        Then a response with status 400 is received
#        
#  Scenario: Delete a publication
#      Given the endpoint https://localhost:5001/api/v1/publications/2 is available
#      When An a delete request is sent   
#      Then a response with status 200 is received
#      
#  Scenario: Delete a publication go wrong
#        Given the endpoint https://localhost:5001/api/v1/publications/1 is not available
#        When An a delete request is sent   
#        Then a response with status 400 is received
#        
#    Scenario: Get all current publications as a vet
#        Given the endpoint https://localhost:5001/api/v1/Users/3/publications is available now
#        When A get publications by user request is sent   
#        Then a response with status 200 is received
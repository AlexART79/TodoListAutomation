Feature: ApiCrudOperations
	Verify API CRUD

Scenario: API CRUD operations	
	When user inserting new item with text "API new uncomplete item"
	Then DB should contain uncomplete item with text "API new uncomplete item"

	When user updating item with text "API new uncomplete item" - set complete to true
	Then DB should contain complete item with text "API new uncomplete item"

	When user deleting item with text "API new uncomplete item"
	Then DB should not contain item with text "API DB new uncomplete item"

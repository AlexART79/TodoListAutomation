Feature: DbCrudOperations
	Verify DB CRUD

Scenario: DB CRUD operations	
	When user inserting new uncomplete item with text "DB new uncomplete item"
	Then DB should contain uncomplete item with text "DB new uncomplete item"

	When user updating item with text "DB new uncomplete item" with new text "(Edited) DB new uncomplete item"
	Then DB should contain uncomplete item with text "(Edited) DB new uncomplete item"

	When user updating item with text "(Edited) DB new uncomplete item" - set complete to true
	Then DB should contain complete item with text "(Edited) DB new uncomplete item"

	When user deleting item with text "(Edited) DB new uncomplete item"
	Then DB should not contain item with text "(Edited) DB new uncomplete item"

Feature: DeleteItem
	Ensure item can be deleted

Background:
	Given there is an uncomplete item with text "This item should be deleted!"

Scenario: Delete Item
	Given home page is loaded	
	When user click on item's right X icon
	Then item should be deleted
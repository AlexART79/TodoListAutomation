Feature: AddItem
	Verify TodoList home page

Scenario: Verify new item added	
	Given home page is loaded
	And todo list with few items is displayed
	When user clicked into new item input
	And typed text "My new item"
	And pressed Enter key
	Then new item should be added to the list
	And added item status should not be complete
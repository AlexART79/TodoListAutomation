Feature: AddItem
	Ensure item can be added

Scenario: Add Item
	Given home page is loaded
	And todo list with few items is displayed	
	When typed text "My new item" into new item form
	And pressed Submit button
	Then new item should be added to the list
	And added item status should not be complete
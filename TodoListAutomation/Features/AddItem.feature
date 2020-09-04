Feature: AddItem
	Ensure item can be added

Scenario: Add Item
	Given home page is loaded
	And todo list with few items is displayed
	When user clicked into new item input
	And typed text "My new item"
	And pressed Enter key
	Then new item should be added to the list
	And added item status should not be complete
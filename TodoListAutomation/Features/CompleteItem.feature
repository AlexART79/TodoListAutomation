Feature: CompleteItem
	Ensure item can be completed

Background:
	Given there is an uncomplete item with text "This item should be completed"

Scenario: Complete Item
	Given home page is loaded	
	When user click on item's left circle icon
	Then item should be complete
Feature: UnCompleteItem
	Ensure item can be UN-completed

Background:
	Given there is a completed item with text "This item is NOT completed yet"

Scenario: Uncomplete Item
	Given home page is loaded	
	When user click on item's left circle icon
	Then item should be not complete
Feature: HomePage
	Verify TodoList home page

Scenario: Verify Home Page	
	Given home page is loaded
	Then header should be displayed
	And header should have text "My Todo List"
	And todo list should be displayed
	And add new item form should be displayed
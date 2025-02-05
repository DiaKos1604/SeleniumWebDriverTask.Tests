Feature: Search term on magnifier icon page

Background:
   Given The user is on the EPAM home page

@TestFilter
Scenario Outline: Navigate to magnifier icon and input search term
    When The user navigates to the Magnifier icon page
    Then The user enters in the search field "<searchTerm>"
     And The user clicks the 'Find' button
    Then The searching results should contain "<searchTerm>"

Examples:
| searchTerm |
| Cloud      |
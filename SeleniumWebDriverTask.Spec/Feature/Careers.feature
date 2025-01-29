Feature: Careers page job search
  As a user, I want to search for job positions on the Careers page and filter results effectively

  Background:
  Given The user is on the EPAM home page

  Scenario Outline: View and verify the latest job posting details after searching
    When The user navigates to the Careers page
    Then The user clicks the 'Find Your Dream Job' link
    Then The user enters "<ProgrammingLanguage>" in the search field
     And The user clicks 'Sort by Date'
    When The user clicks on the first job posting
    Then The job description should contain "<ProgrammingLanguage>"

    Examples:
      | ProgrammingLanguage |
      | C#                  |
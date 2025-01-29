Feature: Insights page interaction
    As a user, I want to navigate to the Insights page, interact with the carousel, and select an article to read

  Background:
   Given The user is on the EPAM home page

  Scenario Outline: User navigates to the Insights page and interacts with the carousel
    When The user navigates to the Insights page
     And The user moves to the slider
     And The user swipes the carousel 2 times
    Then The user clicks the 'Read More' button
    Then The user should see an article titled
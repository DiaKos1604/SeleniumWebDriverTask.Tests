Feature: Services section navigation and validation  
        As a user, I want to navigate to the Services page, select different AI categories  

  Background:
    Given The user is on the EPAM home page  

  @TestFilter
  Scenario Outline: Navigate to Services Page and validate AI categories  
    When The user navigates to the Services page  
     And The user moves to the Artificial Intelligence link
    When The user selects "<serviceCategory>" category
    Then The "Our Related Expertise" section should be displayed for "<serviceCategory>"

    Examples:
    | serviceCategory  |
    | Responsible AI   |
    | Generative AI    |
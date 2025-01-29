Feature: Download EPAM Corporate Overview
  As a user, I want to download the EPAM Corporate Overview file from the About page

   Background:
  Given The user is on the EPAM home page

  Scenario Outline: User can download the EPAM Corporate Overview file
    When The user navigates to the About page
     And The user clicks the download button in the "EPAM at a Glance" section
    Then The file "EPAM_Corporate_Overview_Q4_EOY.pdf" should be successfully downloaded
     And The downloaded file should match the expected content and format
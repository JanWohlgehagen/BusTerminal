Feature: Validate Time
  To ensure the booking time is valid
  As a user
  I want to validate the start and end times for bookings.

  Scenario: Valid time range
    Given the start time is 2 days and 3 hours in the future
    And the end time is 2 days and 6 hours in the future
    When the time is validated
    Then the result should be the date 2 days in the future

  Scenario: End time before start time
    Given the start time is 2 days and 6 hours in the future
    And the end time is 2 days and 3 hours in the future
    When the time is validated
    Then an error with message "End time cannot be before start time." should be thrown

  Scenario: Start and end times on different days
    Given the start time is 1 days and 22 hours in the future
    And the end time is 2 days and 2 hours in the future
    When the time is validated
    Then an error with message "Start and end time must be on the same day." should be thrown

  Scenario: Start time in the past
    Given the start time is 1 days and 3 hours in the past
    And the end time is 1 days and 6 hours in the past
    When the time is validated
    Then an error with message "End time cannot be before start time." should be thrown

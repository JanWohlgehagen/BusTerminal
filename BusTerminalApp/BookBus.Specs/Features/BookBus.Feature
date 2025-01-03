Feature: Book a Bus
  To reserve a bus for a given time
  As a user
  I want to book a bus if it is available.

  Scenario: Invalid booking date should throw an error
    Given the start time is 1 days and 3 hours in the past
    And the end time is 1 days and 6 hours in the past
    And the bus ID is "123"
    And a bus with no booked times exists
    When I book the bus
    Then I should see an error "End time cannot be before start time."

  Scenario: Bus not found should throw an error
    Given the start time is 1 days and 3 hours in the future
    And the end time is 1 days and 6 hours in the future
    And the bus ID is "invalidId"
    And a bus with no booked times exists
    When I book the bus
    Then I should see an error "Bus not found."

  Scenario: Bus already booked at the requested time should throw an error
    Given the start time is 1 days and 3 hours in the future
    And the end time is 1 days and 6 hours in the future
    And the bus ID is "123"
    And a bus with booked times
    When I book the bus
    Then I should see an error "Bus is already booked at that time."

  Scenario: Booking should be successful
    Given the start time is 1 days and 3 hours in the future
    And the end time is 1 days and 6 hours in the future
    And the bus ID is "123"
    And a bus with no booked times exists
    When I book the bus
    Then booking should be successful
    And the booking should be added to the bus' booked times

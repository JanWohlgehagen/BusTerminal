Feature: Get Available Buses
  To find buses available for booking
  As a user
  I want to retrieve a list of available buses based on time and distance.

Scenario: Invalid distance should throw an error
    Given the distance is -1 kilometers
    And the start time is 1 days and 3 hours in the future
    And the end time is 1 days and 6 hours in the future
    When I search for available buses
    Then I should see an error "We need a distance to calculate the price."


Scenario: Invalid booking date should throw an error
    Given the distance is 100 kilometers
    And the start time is 1 days and 3 hours in the past
    And the end time is 1 days and 6 hours in the past
    When I search for available buses
    Then I should see an error "End time cannot be before start time."

Scenario: Bus is already booked at the requested time
    Given the distance is 100 kilometers
    And the start time is 1 days and 3 hours in the future
    And the end time is 1 days and 6 hours in the future
    And a bus with booked times
    When I search for available buses
    Then I should see an empty list

Scenario: Available buses should be returned
    Given the distance is 100 kilometers
    And the start time is 1 days and 3 hours in the future
    And the end time is 1 days and 6 hours in the future
    And a bus with no booked times exists
    When I search for available buses
    Then I should see a list of available buses
    And each bus should have a price

Scenario: Available buses should be returned even if some are already booked
    Given the distance is 100 kilometers
    And the start time is 2 days and 3 hours in the future
    And the end time is 2 days and 6 hours in the future
    And a bus with booked times
    When I search for available buses
    Then I should see a list of available buses
    And each bus should have a price

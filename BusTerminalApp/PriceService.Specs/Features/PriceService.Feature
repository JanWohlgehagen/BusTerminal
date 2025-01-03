Feature: Calculate price for booking
  As a customer
  I want to know the total price for a booking
  So that I can understand the cost based on the booking day and distance


Scenario: Calculate price for a weekday
  Given the booking day is Monday
  And the distance is 100 kilometers
  When the price is calculated
  Then the total price should be 1500

Scenario: Calculate price for Saturday
  Given the booking day is Saturday
  And the distance is 100 kilometers
  When the price is calculated
  Then the total price should be 2000

Scenario: Calculate price for Sunday
  Given the booking day is Sunday
  And the distance is 100 kilometers
  When the price is calculated
  Then the total price should be 2500

Scenario: Distance is zero or negative
  Given the booking day is Monday
  And the distance is 0 kilometers
  When the price is calculated
  Then an error with message "We need a distance to calculate the price." should be thrown

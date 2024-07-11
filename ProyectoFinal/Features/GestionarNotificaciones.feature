Feature: GestionarNotificaciones

As a user, I want to be notified of my login status so that I can take appropriate actions.

@tag2
Scenario: Successful login notification
    Given I enter a valid username "testuser"
    And I enter a valid password "password"
    When I click the login button
    Then I should see the main menu based on my user state

@tag2
Scenario: Failed login notification
    Given I enter an invalid username "invaliduser"
    And I enter an invalid password "invalidpassword"
    When I click the login button
    Then I should see an error message

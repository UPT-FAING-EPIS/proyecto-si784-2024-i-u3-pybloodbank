Feature: RegistrarUsuario

As a new user, I want to be able to register so that I can access the application.

@tag3
Scenario: Successful user registration
    Given I enter a new username "newuser"
    And I enter a password "password"
    And I confirm the password "password"
    When I click the register button
    Then I should see a success message

@tag3
Scenario: Passwords do not match
    Given I enter a new username "newuser"
    And I enter a password "password"
    And I confirm the password "differentpassword"
    When I click the register button
    Then I should see an error message about password mismatch

@tag3
Scenario: User already exists
    Given I enter an existing username "existinguser"
    And I enter a password "password"
    And I confirm the password "password"
    When I click the register button
    Then I should see an error message about user already existing

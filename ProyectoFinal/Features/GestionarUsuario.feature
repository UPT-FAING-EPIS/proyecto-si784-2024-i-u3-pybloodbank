Feature: GestionarUsuario

As a user, I want to manage personal records, so that I can update their status.

@tag1
Scenario: Load personal records into grid
    Given I open the personal form
    When the form loads
    Then the personal grid should be filled with data

@tag1
Scenario: Ascend a personal record
    Given I have selected a personal record with a user
    When I press the ascend button
    Then the user's status should be updated to 'P'
    And I should see a success message

@tag1
Scenario: Descend a personal record
    Given I have selected a personal record with a user
    When I press the descend button
    Then the user's status should be updated to 'N'
    And I should see a success message

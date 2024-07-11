Feature: AcomodarGrilla
  A short summary of the feature

@tag9
Scenario: Successful login
    Given I have a valid username "testuser"
    And I have a valid password "password"
    When I attempt to log in
    Then I should be logged into the system

@tag9
Scenario: Failed login
    Given I have an invalid username "invaliduser"
    And I have an invalid password "invalidpassword"
    When I attempt to log in
    Then I should see an error message
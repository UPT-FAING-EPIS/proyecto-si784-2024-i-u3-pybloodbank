Feature: Login

  As a registered user,
  I want to be able to log into the application,
  So that I can access my account.

  @tag4
  Scenario: Successful login
    Given I am on the login page
    And I enter my username "existinguser"
    And I enter my password "password"
    When I click the login button
    Then I should be redirected to the options page

  @tag4
  Scenario: Invalid username or password
    Given I am on the login page
    And I enter an invalid username "nonexistinguser"
    And I enter an invalid password "invalidpassword"
    When I click the login button
    Then I should see an error message about invalid credentials

  @tag4
  Scenario: Empty username or password
    Given I am on the login page
    And I leave the username field empty
    And I leave the password field empty
    When I click the login button
    Then I should see an error message about empty fields

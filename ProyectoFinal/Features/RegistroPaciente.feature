Feature: Modificar datos del paciente

  Como usuario del sistema,
  Quiero poder modificar los datos de un paciente registrado,
  Para mantener la información actualizada.

  @tag5
  Scenario: Modificar datos de un paciente existente
    Given Estoy en la página de registro de pacientes
    And tengo un paciente registrado con DNI "12345678"
    When modifico el nombre a "Juan"
    And modifico el apellido a "Pérez"
    And modifico la dirección a "Calle Nueva 123"
    And modifico el teléfono a "999888777"
    And modifico el tipo de sangre a "A"
    And modifico el RH a "+"
    And presiono el botón modificar
    Then debería ver un mensaje de confirmación de la modificación

  @tag5
  Scenario: Intentar modificar con campos vacíos
    Given Estoy en la página de registro de pacientes
    And tengo un paciente registrado con DNI "12345678"
    When dejo el campo de nombre vacío
    And presiono el botón modificar
    Then debería ver un mensaje de error sobre campos vacíos

  @tag5
  Scenario: Intentar modificar con DNI incorrecto
    Given Estoy en la página de registro de pacientes
    And tengo un paciente registrado con DNI "12345678"
    When modifico el DNI a "87654321"
    And presiono el botón modificar
    Then debería ver un mensaje de error sobre DNI incorrecto

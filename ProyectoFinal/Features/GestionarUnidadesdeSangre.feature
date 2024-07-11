Feature: GestionarUnidadesdeSangre
  A short summary of the feature

  @tag11
  Scenario Outline: Manage Patients
    Given the user is on the patient registration screen
    When they enter patient details <DNI>, <Nombre>, <Apellido>, <Direccion>, <Telefono>, <TipoSangre>, <RH>
    And they click on the registrar button
    Then the patient should be successfully registered

    Examples:
      | DNI       | Nombre    | Apellido  | Direccion       | Telefono   | TipoSangre | RH |
      | 12345678  | Juan      | Perez     | Av. Libertador  | 123456789  | A          | +  |

describe('Login', () => {
  beforeEach(() => {
    cy.visit('/login');
  });

  it('should display the login form', () => {
    cy.get('form').should('be.visible');
    cy.get('input[name="cpf"]').should('be.visible');
    cy.get('input[name="password"]').should('be.visible');
    cy.get('button[type="submit"]').should('be.visible');
  });

  it('should login successfully with correct credentials', () => {
    cy.get('input[name="cpf"]').type('99999999995');
    cy.get('input[name="password"]').type('Teste@Stefanini{enter}');

    cy.url().should('include', '/');
    cy.get('h1').should('contain', 'Bem-vindo');
  });

  it('should show error message when fields are empty and form is submitted', () => {
    cy.get('input[name="cpf"]').type('{enter}');

    cy.get('.error-message').should('contain', 'Cpf and Password fields must not be empty.');
  });

  it('should show error message when CPF is incorrect', () => {
    cy.get('input[name="cpf"]').type('11111111111');
    cy.get('input[name="password"]').type('Teste@Stefanini{enter}');

    cy.get('.error-message').should('contain', 'CPF or password is incorrect.');
  });

  it('should show error message when password is incorrect', () => {
    cy.get('input[name="cpf"]').type('99999999995');
    cy.get('input[name="password"]').type('WrongPassword{enter}');

    cy.get('.error-message').should('contain', 'CPF or password is incorrect.');
  });
});

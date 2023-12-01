
  function submitCadastro() {
    debugger
    var nome = document.getElementById('nome').value
    var doc = document.getElementById('doc').value
    var numero = document.getElementById('numero').value
    var cep = document.getElementById('cep').value
    var email = document.getElementById('email').value
    var password = document.getElementById('password').value
    var confirmPassword = document.getElementById('confirmPassword').value



    const data = {
      nome: nome,
      doc: doc,
      numero: numero,
      cep: cep,
      email: email,
      password: password,
      confirmPassword: confirmPassword,
    };
    const validationErrors = validateFields(data);

    if (validationErrors.length > 0) {
      displayError(validationErrors.join(', '));
      return;
    }

    fetch('https://localhost:7094/nova-conta-mobile', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(data),
    })
    .then(response => {
      if (!response.ok) {
        throw new Error('Erro na requisição');
      }
      return response.json();
    })
    .then(data => {
      localStorage.setItem('token', data.accessToken);
      window.location.href = 'login.html';
    })
    .catch(error => {
      alert('Erro: ' + error.message);
      displayError(error.message);
    });
  }

  function displayError(errorMessage) {
    const errorContainer = document.getElementById('errorContainer');
    errorContainer.innerHTML = '';
  
    const errorElement = document.createElement('div');
    errorElement.className = 'error-message';
    const exclamationPoint = document.createElement('span');
    exclamationPoint.className = 'exclamation-point';
    exclamationPoint.textContent = '!';
    errorElement.appendChild(exclamationPoint);
  
    const errorMessageElement = document.createElement('span');
    errorMessageElement.textContent = errorMessage;
    errorElement.appendChild(errorMessageElement);

    errorContainer.appendChild(errorElement);
  }
  

  function validateFields(data) {
    const errors = [];

    if (!data.nome) {
      errors.push('O campo Nome é obrigatório');
    }

    if (!data.doc) {
      errors.push('O campo Doc é obrigatório');
    }

    if (!data.numero) {
      errors.push('O campo Numero é obrigatório');
    }

    if (!data.cep) {
      errors.push('O campo Cep é obrigatório');
    }

    if (!data.email) {
      errors.push('O campo Email é obrigatório');
    }

    if (!data.password) {
      errors.push('O campo Password é obrigatório');
    }

    if (!data.confirmPassword) {
      errors.push('O campo ConfirmPassword é obrigatório');
    }

    return errors;
  }

  function redirectLogin() {
    window.location.href = 'login.html';
  }


    function login() {
        var email = document.getElementById('email').value;
        var password = document.getElementById('password').value;

        var data = {
            email: email,
            password: password
        };

        fetch('https://localhost:7094/entrar-mobile', 
        {
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
            debugger
            const accessToken = findAccessToken(data);
            localStorage.setItem('accessToken', accessToken);
            window.location.href = 'sucesso.html';
        })
        
        .catch(error => {
            document.getElementById('errorContainer').innerText = 'Usuário ou Senha incorretos';
        });
    }
    function findAccessToken(obj) {
        for (let key in obj) {
            if (obj.hasOwnProperty(key)) {
                if (typeof obj[key] === 'object' && obj[key] !== null) {
                    const accessToken = findAccessToken(obj[key]);
                    if (accessToken !== undefined) {
                        return accessToken;
                    }
                } else if (key === 'accessToken') {
                    return obj[key];
                }
            }
        }
        return undefined;
    }
    function redirectCadastro() {
        window.location.href = 'cadastro.html';
    }
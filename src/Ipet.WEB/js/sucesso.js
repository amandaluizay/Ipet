const token = localStorage.getItem('accessToken');

if (token) {
    document.getElementById('tokenDisplay').innerText = token;
    document.getElementById('tokenContainer').style.display = 'block';

    fetch('https://localhost:7094/test-token', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`,
        },
    })
    .then(response => {
        if (response.ok) {
            console.log('Sucesso: Status 200');
        } else if (response.status === 415) {
            console.error('Falha: Status 415 - Unsupported Media Type');
        } else {
            console.error('Falha: Outro código de status -', response.status);
        }
    })
    .catch(error => {
        console.error('Erro:', error.message);
    });
}
const baseUrl = 'https://localhost:44364/api';

async function showLoggedUserName(id) {
    const results = await fetch(`${baseUrl}/Usuario/${id}`);
    const json = await results.json();
    if (results.ok) {
        const showNameLink = document.querySelector('.show-name');
        const fullName = json.Nome;
        const nome = fullName.split(' ');
        showNameLink.innerHTML = 'Olá ' + nome[0];
    }
}


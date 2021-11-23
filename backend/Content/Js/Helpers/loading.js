document.querySelector('#loading').classList.add('is-hidden');
setTimeout(() => {
    document.querySelector('#loading').classList.remove('is-hidden');
}, 200)
clearTimeout();
let jValidator = {
    handleSubmit: (e) => {
        e.preventDefault();
        let send = true;

        let inputs = form.querySelectorAll('input');
        let selects = form.querySelectorAll('select');
        let textareas = form.querySelectorAll('textarea');

        jValidator.clearErrors();

        if (inputs) {
            for (let i = 0; i < inputs.length; i++) {
                let input = inputs[i];
                let check = jValidator.checkInput(input);
                if (check !== true) {
                    send = false;
                    jValidator.showError(input, check);
                }
            }
        }

        if (textareas) {
            for (let i = 0; i < textareas.length; i++) {
                let textarea = textareas[i];
                let check = jValidator.checkInput(textarea);
                if (check !== true) {
                    send = false;
                    jValidator.showError(textarea, check);
                }
            }
        }

        if (selects) {
            for (let i = 0; i < selects.length; i++) {
                let select = selects[i];
                let check = jValidator.checkInput(select);
                if (check !== true) {
                    send = false;
                    jValidator.showError(select, check);
                }
            }
        }

        // verifica se o check tá on e chama o método
        if (checkbox) {
            checkToggle();
        }

        // caso seja o form de experiencia profissional
        if (document.querySelector('#admissao')) {
            let check = jValidator.checkProfessionalExperienceDate();
            if (check) {
                send = false;
            }
        }

        // caso seja o form de formação
        if (document.querySelector('#inicio')) {
            let check = jValidator.checkStartConclusionFormationDate();
            if (check) {
                send = false;
            }
        }

        if (send) {
            form.submit();
        }
    },
    checkInput: (input) => {
        let rules = input.getAttribute('data-rules');
        if (rules !== null) {
            rules = rules.split('|');
            for (let k in rules) {
                let rulesDetails = rules[k].split(':');
                switch (rulesDetails[0]) {
                    case 'required':
                        if (input.value.trim() == '') {
                            return 'O campo não pode ser vazio'
                        }
                        break;
                    case 'min':
                        if (input.value.trim().length > 0 && input.value.trim().length < rulesDetails[1]) {
                            return 'O campo precisa ter pelo menos ' + rulesDetails[1] + ' caracteres';
                        }
                        break;
                    case 'max':
                        if (input.value.trim().length > rulesDetails[1]) {
                            return 'O campo precisa ter no máximo ' + rulesDetails[1] + ' caracteres';
                        }
                        break;
                    case 'email':
                        if (input.value.trim() != '') {
                            let regex = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                            if (!regex.test(input.value.toLowerCase())) {
                                return 'O campo precisa ser preenchido com um email válido';
                            }
                        }
                        break;
                    case 'pick':
                        if (input.value.trim() == '') {
                            return 'Selecione uma opção válida';
                        }
                        break;
                }
            }
        }
        return true;
    },
    showError: (input, error) => {

        input.classList.add('is-invalid');

        let errorElement = document.createElement('div');
        errorElement.classList.add('invalid-feedback');
        errorElement.innerHTML = error;
        input.parentElement.insertBefore(errorElement, input.ElementSibling);
    },
    clearErrors: () => {
        let errorElements = document.querySelectorAll('.invalid-feedback');
        for (let i = 0; i < errorElements.length; i++) {
            errorElements[i].remove();
        }

        let errorInputs = document.querySelectorAll('.is-invalid');
        for (let i = 0; i < errorInputs.length; i++) {
            errorInputs[i].classList.remove('is-invalid');
        }
    },
    checkProfessionalExperienceDate: () => {
        let admissionDate = document.querySelector('#admissao');
        let resignationDate = document.querySelector('#demissao');
        if (resignationDate.value !== '') {
            if (resignationDate.value < admissionDate.value) {
                jValidator.showError(resignationDate, 'A data de demissão não pode ser anterior a data de admissão');
                return true;
            }
        }
    },
    checkStartConclusionFormationDate: () => {
        let startDate = document.querySelector('#inicio');
        let conclusionDate = document.querySelector('#conclusao');
        if (conclusionDate.value < startDate.value) {
            jValidator.showError(conclusionDate, 'A data de conclusão não pode ser anterior a data de inicio');
            return true;
        }
    }
};

let form = document.querySelector('.jValidator');
form.addEventListener('submit', jValidator.handleSubmit);


// Bloqueando o campo de demissão caso seja emprego atual

let checkbox = document.querySelector('#empregoAtual');
let demissao = document.querySelector('#demissao');

function checkToggle() {
    if (checkbox.checked) {
        demissao.setAttribute('disabled', 'disabled');
        demissao.removeAttribute('data-rules', 'required');
        demissao.value = '';
    } else {
        demissao.removeAttribute('disabled', 'disabled');
        demissao.setAttribute('data-rules', 'required');
    }
}
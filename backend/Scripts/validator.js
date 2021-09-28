let jValidator = {
    handleSubmit: (e) => {
        e.preventDefault();
        let send = true;

        let inputs = form.querySelectorAll('input');

        jValidator.clearErrors();

        for (let i = 0; i < inputs.length; i++) {
            let input = inputs[i];
            let check = jValidator.checkInput(input);
            if (check !== true) {
                send = false;
                jValidator.showError(input, check);
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
                        if (input.value == '') {
                            return 'Não pode ser vazio'
                        }
                        break;
                    case 'min':
                        if (input.value.length < rulesDetails[1]) {
                            return 'O campo precisa ter pelo menos ' + rulesDetails[1] + ' caracteres';
                        }
                        break;
                    case 'max':
                        if (input.value.length > rulesDetails[1]) {
                            return 'O campo precisa ter no máximo ' + rulesDetails[1] + ' caracteres';
                        }
                        break;
                    case 'email':
                        if (input.value != '') {
                            let regex = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                            if (!regex.test(input.value.toLowerCase())) {
                                return 'O campo precisa ser preenchido com um email válido';
                            }
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
    }
};

let form = document.querySelector('.jValidator');
form.addEventListener('submit', jValidator.handleSubmit);
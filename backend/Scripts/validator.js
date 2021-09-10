//let loginValidator = {
//    handleSubmit: (e) => {
//        e.preventDefault();
//        let send = false;

//        let inputs = formLogin.querySelectorAll('input');
//        for (let i = 0; i < inputs.length; i++) {
//            let input = inputs[i];
//            let check = loginValidator.checkInput(input);
//            if (check !== true) {
//                send = false;
//                loginValidator.showError(input, check)
//            }
//        }

//        if (send) {
//            formLogin.submit();
//        }
//    },
//    checkInput: (input) => {
//        let rules = input.getAttribute('data-rules');
//        if (rules !== null) {
//            rules = rules.split('/');
//            for (let k in rules) {
//                let ruleDetails = rules[k].split(':');
//                switch (ruleDetails[0]) {
//                    case 'required':
//                        if (input.value == '') {
//                            return 'Não pode ser vazio'
//                        }
//                        break;
//                    case 'min':

//                        break;

//                }
//            }
//        }
//        return true;
//    },
//    showError: (input, error) => {
//        input.style.borderColor = '#FF0000';
//        let errorElement = document.createElement('div');
//        errorElement.classList.add('error');
//        errorElement.innerHTML = error;
//        input.parseElement.insertBefore(errorElement, input.elementSibling);
//    }
//};

//let formLogin = document.querySelector('.login-form');
//formLogin.addEventListener('submit', loginValidator.handleSubmit);
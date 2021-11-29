function successAlertWithButton(title, message) {
    Swal.fire({
        icon: 'success',
        title: title,
        text: message,
        confirmButtonColor: '#d90204',
        showConfirmButton: true
    })
}

function successAlert(title, message) {
    Swal.fire({
        icon: 'success',
        title: title,
        text: message,
        confirmButtonColor: '#d90204',
        showConfirmButton: false,
        timer: 2000
    })
}

function errorAlertWithButton(title, message) {
    Swal.fire({
        icon: 'error',
        title: title,
        text: message,
        confirmButtonColor: '#d90204',
        showConfirmButton: true
    })
}

function errorAlert(title, message) {
    Swal.fire({
        icon: 'error',
        title: title,
        text: message,
        confirmButtonColor: '#d90204',
        showConfirmButton: false,
        timer: 2000
    })
}

function deleteAlert(url) {
    Swal.fire({
        title: 'Você tem certeza?',
        text: "Esta ação é irreverssível!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d90204',
        cancelButtonColor: '#b6c1d4',
        confirmButtonText: 'Confirmar',
        cancelButtonText: 'Cancelar',
        reverseButtons: true
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = url;
        }
    })
}
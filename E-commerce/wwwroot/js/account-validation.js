function checkPassword(password) {
    if (password.length < 8) {
        return "Password must be at least 8 characters long.";
    }

    if (!/[A-Z]/.test(password)) {
        return "Password must contain at least one uppercase letter.";
    }

    if (!/[a-z]/.test(password)) {
        return "Password must contain at least one lowercase letter.";
    }

    if (!/\d/.test(password)) {
        return "Password must contain at least one number.";
    }

    return null; 
}

function validatePassword() {
    var password = document.getElementById('Password').value;
    var errorMessage = checkPassword(password);

    if (errorMessage) {
        document.getElementById('Password').setCustomValidity(errorMessage);
    } else {
        document.getElementById('Password').setCustomValidity('');
    }
}

document.getElementById('Password').addEventListener('input', validatePassword);

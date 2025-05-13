const API_BASE_URL = 'http://localhost:5148';

window.login = async function (name, password) {
    try {
        const response = await fetch(`${API_BASE_URL}/api/auth/login?name=${encodeURIComponent(name)}&password=${encodeURIComponent(password)}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            credentials: 'include'
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const data = await response.text();
        return { success: true, message: data };
    } catch (error) {
        console.error('Ошибка при авторизации:', error);
        return { success: false, message: 'Ошибка при авторизации' };
    }
};

window.register = async function(name, password) {
    try {
        const response = await fetch(`${API_BASE_URL}/api/auth/register?name=${encodeURIComponent(name)}&password=${encodeURIComponent(password)}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            credentials: 'include'
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const data = await response.text();
        return { success: true, message: data };
    } catch (error) {
        console.error('Ошибка при регистрации:', error);
        return { success: false, message: 'Ошибка при регистрации' };
    }
};

window.logout = async function() {
    try {
        const response = await fetch(`${API_BASE_URL}/api/auth/logout`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            credentials: 'include'
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }

        const data = await response.text();
        return { success: true, message: data };
    } catch (error) {
        console.error('Ошибка при выходе:', error);
        return { success: false, message: 'Ошибка при выходе' };
    }
};
window.getUser = async function () {
    try {
        const response = await fetch(`${API_BASE_URL}/api/auth/current-user`, {
            method: "GET",
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include'
        });

        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        const data = await response.text();
        return { success: true, message: data };
    } catch (error) {
        console.error('Ошибка при получении пользователя', error);
        return { success: false, message: 'Ошибка при получении пользователя' };
    }
}

window.checkAuth = async function() {
    try {
        const response = await fetch(`${API_BASE_URL}/api/auth/main`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
            credentials: 'include'
        });

        return response.ok;
    } catch (error) {
        console.error('Ошибка при проверке авторизации:', error);
        return false;
    }
}; 
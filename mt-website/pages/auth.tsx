import { useState } from 'react';
import {router} from "next/client";
import { useAuth } from '../components/AuthContext';

export default function AuthPage() {
    const [isRegistering, setIsRegistering] = useState(false);
    const [formData, setFormData] = useState({
        username: '',
        email: '',
        password: ''
    });

    const { setUsername } = useAuth();
    const { setUserId } = useAuth();

    const handleChange = (e) => {
        setFormData((prev) => ({
            ...prev,
            [e.target.name]: e.target.value
        }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            if (isRegistering) {
                const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/Auth/Register?`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({
                        username: formData.username,
                        email: formData.email,
                        password: formData.password
                    })
                });

                if (!response.ok) throw new Error(await response.text());
                alert('Регистрация прошла успешно');
                setIsRegistering(false);
            } else {
                const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/Auth/Login`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    credentials: 'include',
                    body: JSON.stringify({
                        email: formData.email,
                        password: formData.password
                    })
                });

                const result = await response.json();
                const token = result.token;
                localStorage.setItem('jwt', token);
                
                if (!response.ok) throw new Error(await response.text());
                
                const auth = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/Auth/GetUser`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'text/plain',
                        'Authorization': `Bearer ${token}`
                    },
                    credentials: 'include'
                });
                if (!auth.ok) throw new Error('Не удалось получить пользователя');

                const user = await auth.json();
                localStorage.setItem('username', user.username);
                localStorage.setItem('userid', user.id);
                setUsername(user.username);
                setUserId(user.id);
                
                await router.push('/');
                
            }
        } catch (err) {
            alert(err.message || 'Произошла ошибка');
        }
    };

    return (
        <div className="form-container">
            <h1 className="form-title">{isRegistering ? 'Регистрация' : 'Вход'}</h1>
            <form onSubmit={handleSubmit} className="form" encType="multipart/form-data">
                {isRegistering && (
                    <div>
                        <input
                            type="text"
                            name="username"
                            placeholder="Имя пользователя"
                            value={formData.username}
                            onChange={handleChange}
                            required
                            className="form-input"
                        />
                    </div>
                )}
                <div>
                    <input
                        type="email"
                        name="email"
                        placeholder="Email"
                        value={formData.email}
                        onChange={handleChange}
                        required
                        className="form-input"
                    />
                </div>
                <div>
                    <input
                        type="password"
                        name="password"
                        placeholder="Пароль"
                        value={formData.password}
                        onChange={handleChange}
                        required
                        className="form-input"
                    />
                </div>
                <button type="submit" className="register-button">
                    {isRegistering ? 'Зарегистрироваться' : 'Войти'}
                </button>
            </form>
            <div className="form-title">
                <button onClick={() => setIsRegistering(!isRegistering)} className="register-button">
                    {isRegistering ? 'Уже есть аккаунт? Войти' : 'Нет аккаунта? Зарегистрироваться'}
                </button>
            </div>

        </div>
    );
}
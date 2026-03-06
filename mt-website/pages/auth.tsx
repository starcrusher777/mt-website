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
                const response = await
                    fetch('/api/auth/register', {
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
                alert('Registration completed successfully');
                setIsRegistering(false);
            } else {
                const response = await
                    fetch('/api/auth/login', {
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
                
                const auth = await
                    fetch('/api/auth/user', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'text/plain',
                        'Authorization': `Bearer ${token}`
                    },
                    credentials: 'include'
                });
                if (!auth.ok) throw new Error('Failed to get user');

                const user = await auth.json();
                localStorage.setItem('username', user.username);
                localStorage.setItem('userid', user.id);
                setUsername(user.username);
                setUserId(user.id);
                
                await router.push('/');
                
            }
        } catch (err) {
            alert(err.message || 'An error occurred');
        }
    };

    return (
        <div className="form-container">
            <h1 className="form-title">{isRegistering ? 'Register' : 'Log in'}</h1>
            <form onSubmit={handleSubmit} className="form" encType="multipart/form-data">
                {isRegistering && (
                    <div>
                        <input
                            type="text"
                            name="username"
                            placeholder="Username"
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
                        placeholder="Password"
                        value={formData.password}
                        onChange={handleChange}
                        required
                        className="form-input"
                    />
                </div>
                <button type="submit" className="register-button">
                    {isRegistering ? 'Register' : 'Log in'}
                </button>
            </form>
            <div className="form-title">
                <button onClick={() => setIsRegistering(!isRegistering)} className="register-button">
                    {isRegistering ? 'Already have an account? Log in' : 'No account? Register'}
                </button>
            </div>

        </div>
    );
}
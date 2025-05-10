import { createContext, useContext, useEffect, useState, ReactNode } from 'react';

type AuthContextType = {
    username: string | null;
    setUsername: (username: string | null) => void;
    userId: number | null;
    setUserId: (id: number | null) => void;
};

const AuthContext = createContext<AuthContextType>({
    username: null,
    setUsername: () => {},
    userId: null,
    setUserId: () => {},
});

export const useAuth = () => useContext(AuthContext);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
    const [username, setUsername] = useState<string | null>(null);
    const [userId, setUserId] = useState<number | null>(null);

    useEffect(() => {
        const storedUsername = localStorage.getItem('username');
        const storedUserId = localStorage.getItem('userId');

        if (storedUsername) setUsername(storedUsername);
        if (storedUserId) setUserId(Number(storedUserId));
    }, []);

    useEffect(() => {
        if (username !== null) {
            localStorage.setItem('username', username);
        } else {
            localStorage.removeItem('username');
        }
    }, [username]);

    useEffect(() => {
        if (userId !== null) {
            localStorage.setItem('userId', String(userId));
        } else {
            localStorage.removeItem('userId');
        }
    }, [userId]);

    return (
        <AuthContext.Provider value={{ username, setUsername, userId, setUserId }}>
            {children}
        </AuthContext.Provider>
    );
};
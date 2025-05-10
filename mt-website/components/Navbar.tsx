import Link from 'next/link';
import styles from '../styles/Navbar.module.css';
import { useAuth } from '../components/AuthContext';

const Navbar = () => {
    const { username, setUsername } = useAuth();
    const { userId, setUserId } = useAuth();
    

    const handleLogout = () => {
        localStorage.removeItem('username');
        localStorage.removeItem('userid');
        setUsername(null);
        setUserId(null);
    };
    
    return (
        <div className={styles.navbar}>
            <div className={styles['nav-title']}>MerchTrade</div>
            <div className={styles['nav-links']}>
                <Link href="/">На главную</Link>
                <Link href="/AdCard">Объявления</Link>
                <Link href="/createNew">Создать</Link>
                {username ? (
                    <>
                        <span className={styles['nav-links']}>Привет, {username}</span>
                        <Link href={`/user/${userId}?userId=${userId}`}>Профиль</Link>
                        <button className={styles['nav-button']} onClick={handleLogout}>Выйти</button>
                    </>
                ) : (
                    <>
                        <Link href="/auth">Авторизация</Link>
                        
                    </>
                )}
            </div>
        </div>
    );
};

export default Navbar;
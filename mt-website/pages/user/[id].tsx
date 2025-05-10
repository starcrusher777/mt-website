import { useRouter } from 'next/router';
import { useEffect, useState } from 'react';
import styles from '../../styles/UserProfile.module.css';

interface User {
    id: number;
    username: string;
    contacts: {
        telephone: string | null;
        email: string | null;
        address: string | null;
    };
    socials: {
        telegram: string | null;
        vkontakte: string | null;
        instagram: string | null;
        twitter: string | null;
    };
    personals: {
        firstName: string | null;
        lastName: string | null;
    };
}

export default function UserProfilePage() {
    const router = useRouter();
    const { id } = router.query;

    const [user, setUser] = useState<User | null>(null);

    useEffect(() => {
        if (!id) return;
        fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/User/GetUser/${id}?userId=${id}`)
            .then(res => res.json())
            .then(data => {
                setUser(data);
            });
    }, [id]);

    if (!user) return <div className="ad-loading">Загрузка профиля...</div>;

    return (
        <div className={styles.profileContainer}>
            <div className={styles.page}>
                <h1 className={styles.title}>Профиль пользователя: {user.username}</h1>
                {(user.personals.firstName || user.personals.lastName) && (
                    <p className="ad-description">
                        Имя: {user.personals.firstName || '-'} {user.personals.lastName || ''}
                    </p>
                )}

                <div className={styles.info}>
                    <h2 className={styles.title}>Контакты</h2>
                    <ul className="ad-list">
                        <li>Email: {user.contacts.email || '—'}</li>
                        <li>Телефон: {user.contacts.telephone || '—'}</li>
                        <li>Адрес: {user.contacts.address || '—'}</li>
                    </ul>
                    <br/>
                    <h2 className={styles.title}>Соцсети</h2>
                    <ul className="ad-list">
                        <li>Telegram: {user.socials.telegram || '—'}</li>
                        <li>VK: {user.socials.vkontakte || '—'}</li>
                        <li>Instagram: {user.socials.instagram || '—'}</li>
                        <li>Twitter: {user.socials.twitter || '—'}</li>
                    </ul>
                </div>
            </div>
            </div>
            );
            }
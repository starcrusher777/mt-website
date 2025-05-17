import { useEffect, useState } from 'react';
import { useRouter } from 'next/router';
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

export default function EditProfilePage() {
    const [user, setUser] = useState<User | null>(null);
    const [loading, setLoading] = useState(true);
    const router = useRouter();
    const { id } = router.query;
    

    useEffect(() => {
        fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/User/GetUser/${id}?userId=${id}`)
            .then(res => res.json())
            .then(data => {
                setUser(data);
                setLoading(false);
                console.log(data);
            });
    }, []);
    

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (!user) return;
        const { name, value } = e.target;

        if (name in user.contacts) {
            setUser({ ...user, contacts: { ...user.contacts, [name]: value } });
        } else if (name in user.socials) {
            setUser({ ...user, socials: { ...user.socials, [name]: value } });
        } else if (name in user.personals) {
            setUser({ ...user, personals: { ...user.personals, [name]: value } });
        }
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        console.log('Отправляемый JSON:', JSON.stringify(user, null, 2));
        const response = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/User/UpdateUser/${id}?userId=${id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(user),
        });
        console.log(response);
        console.log(id);

        if (response.ok) {
            alert('Профиль обновлен');
            router.push(`/user/${id}?userId=${id}`);
        } else {
            alert('Ошибка обновления');
        }
    };

    if (loading || !user) return <div>Загрузка...</div>;

    return (
        <div className={styles.page}>
            <h1>Редактировать профиль</h1>
            <form onSubmit={handleSubmit} className={styles.editForm}>
                <h3>Персональные данные</h3>
                <input type="text" name="firstName" placeholder="Имя" value={user.personals.firstName || ''} onChange={handleChange} />
                <input type="text" name="lastName" placeholder="Фамилия" value={user.personals.lastName || ''} onChange={handleChange} />

                <h3>Контакты</h3>
                <input type="email" name="email" placeholder="Email" value={user.contacts.email || ''} onChange={handleChange} />
                <input type="tel" name="telephone" placeholder="Телефон" value={user.contacts.telephone || ''} onChange={handleChange} />
                <input type="text" name="address" placeholder="Адрес" value={user.contacts.address || ''} onChange={handleChange} />

                <h3>Социальные сети</h3>
                <input type="text" name="telegram" placeholder="Telegram" value={user.socials.telegram || ''} onChange={handleChange} />
                <input type="text" name="vkontakte" placeholder="VK" value={user.socials.vkontakte || ''} onChange={handleChange} />
                <input type="text" name="instagram" placeholder="Instagram" value={user.socials.instagram || ''} onChange={handleChange} />
                <input type="text" name="twitter" placeholder="Twitter" value={user.socials.twitter || ''} onChange={handleChange} />

                <br />
                <button type="submit">Сохранить изменения</button>
            </form>
        </div>
    );
}
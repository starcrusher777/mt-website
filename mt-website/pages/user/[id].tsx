import { useRouter } from 'next/router';
import { useEffect, useState } from 'react';
import Link from 'next/link';
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

interface Order {
    id: number;
    orderId: number;
    orderName: string;
    userId: number;
    item: {
        name: string;
        description: string;
        price: number;
        images: { imageUrl: string }[];
    };
}

export default function UserProfilePage() {
    const router = useRouter();
    const { id } = router.query;

    const [user, setUser] = useState<User | null>(null);
    const [orders, setOrders] = useState<Order[]>([]);

    const [currentUserId, setCurrentUserId] = useState<number | null>(null);

    useEffect(() => {
        const storedUserId = localStorage.getItem('userId');
        if (storedUserId) {
            setCurrentUserId(Number(storedUserId));
        }
    }, []);

    useEffect(() => {
        if (!id) return;
        
        fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/User/GetUser/${id}?userId=${id}`)
            .then(res => res.json())
            .then(data => {
                setUser(data);
                
        fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/Order/GetOrdersByUserId/${id}?userId=${id}`)
                    .then(res => res.json())
                    .then(data => setOrders(data));      
            });
    }, [id]);

    if (!user) return <div className="ad-loading">Загрузка профиля...</div>;

    return (
        <div className={styles.profileContainer}>
            <div className={styles.page}>
                <h1 className={styles.title}>Профиль пользователя: {user.username}</h1>
                {currentUserId === user.id && (
                    <Link href={`/profile/edit?id=${user.id}`}>
                        <button className="anime-button">Редактировать профиль</button>
                    </Link>
                )}
                <div className={styles.info}>
                    <h2 className={styles.title}>Личная информация</h2>
                    {(user.personals.firstName || user.personals.lastName) && (
                        <ul className={styles.infoTitle}>
                            Имя: {user.personals.firstName || '-'} {user.personals.lastName || ''}
                        </ul>
                    )}
                    <br/>
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
                <br/><br/>
                <div className={styles.info}>
                        <h2 className={styles.title}>Объявления пользователя</h2>
                        {orders.length === 0 ? (
                            <p className={styles.noOrders}>Пользователь пока не добавил объявления.</p>
                        ) : (
                            <div className={styles.orderGrid}>
                                {orders.map(order => (
                                    <Link href={`/ads/${order.orderId}?orderId=${order.id}`}
                                          key={order.id} passHref>
                                        <div key={order.id} className={styles.orderCard}>
                                            <img
                                                src={
                                                    order.item.images.length > 0
                                                        ? `${process.env.NEXT_PUBLIC_API_URL}${order.item.images[0]
                                                            .imageUrl}`
                                                        : '/placeholder.jpg'
                                                }
                                                alt="Изображение товара"
                                                className={styles.orderImage}
                                            />
                                            <div className={styles.orderContent}>
                                                <h3 className={styles.orderTitle}>{order.orderName}</h3>
                                                <p className={styles.orderDescription}>{order.item.description}</p>
                                                <p className={styles.orderPrice}>{order.item.price} ₽</p>
                                            </div>

                                        </div>
                                    </Link>
                                ))}
                            </div>
                        )}
                    </div>
                </div>
            </div>
            );
            }
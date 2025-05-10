import { useEffect, useState } from 'react';
import Link from 'next/link';
import Navbar from '../components/Navbar';


interface Order {
    id: number;
    orderId: number;
    orderName: string;
    item: {
        name: string;
        price: number;
        images: { imageUrl: string }[];
    };
}

export default function HomePage() {
    const [orders, setOrders] = useState<Order[]>([]);

    useEffect(() => {
        fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/Order/GetOrders`)
            .then(res => res.json())
            .then(data => {
                const shuffled = data.sort(() => 0.5 - Math.random());
                setOrders(shuffled.slice(0, 3));
            });
    }, []);

    return (
        <div className="homepage-main-container">
            <div className="homepage">
                <h1 className="homepage-title">Welcome to MerchTrade</h1>
                
                <div className="order-grid">
                    {orders.map(order => (
                        <Link href={`/ads/${order.orderId}?orderId=${order.id}`} key={order.orderId}>
                            <div className="order-card">
                                <img
                                    src={
                                        order.item.images.length > 0
                                            ? `${process.env.NEXT_PUBLIC_API_URL}${order.item.images[0].imageUrl}`
                                            : '/placeholder.jpg'
                                    }
                                    alt={order.item.name}
                                    className="order-image"
                                />
                                <h2 className="order-title">{order.orderName}</h2>
                                <p className="order-item-name">{order.item.name}</p>
                                <p className="order-price">{order.item.price} ₽</p>
                            </div>
                        </Link>
                    ))}
                </div>
            </div>
        </div>
    );
}
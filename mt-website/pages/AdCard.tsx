import { useEffect, useState } from 'react';
import AdCard from '../components/AdCard';
import Link from 'next/link';

export default function AllAdsPage() {
    const [orders, setOrders] = useState([]);

    useEffect(() => {
        fetch('/api/orders')
            .then(res => res.json())
            .then(data => {
                console.log("ORDERS:", data);
                setOrders(data);
            });
    }, []);

    const apiBase = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5202';

    return (
        <div className="ads-page-container">
            {orders.map((order: any) => (
                <Link
                    href={`/ads/${order.id}`}
                    key={order.orderId}
                    className="ad-card-link"
                >
                    <AdCard
                        title={order.orderName}
                        price={order.item.price}
                        description={order.item.description}
                        imageUrl={
                            order.item.images.length > 0
                                ? `${apiBase}${order.item.images[0].imageUrl}`
                                : '/placeholder.jpg'
                        }
                    />
                </Link>
            ))}
        </div>
    );
}
import { useEffect, useState } from 'react';
import AdCard from '../components/AdCard';
import Link from 'next/link';

export default function AllAdsPage() {
    const [orders, setOrders] = useState([]);

    useEffect(() => {
        fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/Order/GetOrders`)
            .then(res => res.json())
            .then(data => {
                console.log("ORDERS:", data);
                setOrders(data);
            });
    }, []);

    return (
        <div className="p-6 grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-4 max-w-6xl mx-auto">
            {orders.map((order: any) => {
                console.log("Order item images:", order.item.images);
                return (
                    <Link href={`/ads/${order.orderId}?orderId=${order.id}`}>
                    <AdCard
                        key={order.orderId}
                        title={order.orderName}
                        price={order.item.price}
                        description={order.item.description}
                        imageUrl={
                            order.item.images.length > 0
                                ? `${process.env.NEXT_PUBLIC_API_URL}${order.item.images[0].imageUrl}`
                                : '/placeholder.jpg'
                        }
                    />
                    </Link>      
                );
            })}
        </div>
    );
}
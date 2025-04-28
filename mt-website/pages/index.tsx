import { useEffect, useState } from 'react';
import Link from 'next/link';

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
                setOrders(shuffled.slice(0, 3)); // показываем 3 случайных
            });
    }, []);

    return (
        <div className="max-w-4xl mx-auto p-6">
            <h1 className="text-3xl font-bold mb-6 text-center">Добро пожаловать в MerchTrade</h1>

            <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                {orders.map(order => (
                    <Link href={`/ads/${order.orderId}?orderId=${order.id}`}>
                    <div key={order.orderId} className="border p-4 rounded shadow">
                        <img 
                            src={
                            order.item.images.length > 0
                                ? `${process.env.NEXT_PUBLIC_API_URL}${order.item.images[0].imageUrl}`
                                : '/placeholder.jpg'} 
                            alt={order.item.name} className="h-48 w-full object-cover mb-2 rounded" />
                        <h2 className="text-xl font-semibold">{order.orderName}</h2>
                        <p className="text-gray-600">{order.item.name}</p>
                        <p className="text-green-600 font-bold">{order.item.price} ₽</p>
                    </div>
                    </Link>
                ))}
            </div>

            <div className="flex justify-center mt-6 space-x-4">
                <Link href="/AdCard" className="bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded">Смотреть все</Link>
                <Link href="/createNew" className="bg-green-600 hover:bg-green-700 text-white px-4 py-2 rounded">Создать</Link>
            </div>
        </div>
    );
}
import { useRouter } from 'next/router';
import { useEffect, useState } from 'react';
import { Swiper, SwiperSlide } from 'swiper/react';
import 'swiper/css';
import 'swiper/css/navigation';

import { Navigation } from 'swiper/modules';

interface Order {
    id: number;
    orderId: number;
    orderName: string;
    item: {
        name: string;
        description: string;
        price: number;
        images: { imageUrl: string }[];
    };
}

export default function AdDetailPage() {
    const router = useRouter();
    const { id, orderId } = router.query;

    const [order, setOrder] = useState<Order | null>(null); // <--- исправили тут

    useEffect(() => {
        if (!id || !orderId) return;
        fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/Order/GetOrder/${id}?orderId=${orderId}`)
            .then(res => res.json())
            .then(data => {
                console.log("Полученные данные:", data); // 👈 добавь сюда
                setOrder(data);
            });
    }, [id, orderId]);

    if (!order) return <div>Загрузка...</div>;

    return (
        <div className="max-w-2xl mx-auto p-6">
            <Swiper
                modules={[Navigation]}
                navigation
                spaceBetween={10}
                slidesPerView={1}
                className="rounded mb-4"
            >
                {order.item.images.map((img, index) => (
                    <SwiperSlide key={index}>
                        <img
                            src={`${process.env.NEXT_PUBLIC_API_URL}${img.imageUrl}`}
                            alt={`Изображение ${index + 1}`}
                            className="h-auto w-full object-cover rounded"
                        />
                    </SwiperSlide>
                ))}
            </Swiper>
            <h1 className="text-3xl font-bold mb-2">{order.orderName}</h1>
            <p className="text-gray-600 mb-2">{order.item.description}</p>
            <p className="text-green-600 text-xl font-bold">{order.item.price} ₽</p>
        </div>
    );
}
﻿import { useRouter } from 'next/router';
import { useEffect, useState } from 'react';
import { Swiper, SwiperSlide } from 'swiper/react';
import 'swiper/css';
import 'swiper/css/navigation';
import { Navigation } from 'swiper/modules';
import Link from "next/link";

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
    user: {
        username: string;
    }
}

export default function AdDetailPage() {
    const router = useRouter();
    const { id } = router.query;

    const [order, setOrder] = useState<Order | null>(null);

    useEffect(() => {
        if (!id) return;
        fetch(`${process.env.NEXT_PUBLIC_API_URL}/api/Order/GetOrder/${id}`)
            .then(res => res.json())
            .then(data => {
                setOrder(data);
            });
    }, [id]);
    
    if (!order) return <div className="ad-loading">Загрузка...</div>;

    return (
        <div className="ad-container">
            <Swiper
                modules={[Navigation]}
                navigation
                spaceBetween={10}
                slidesPerView={1}
                className="ad-swiper"
            >
                {order.item.images.map((img, index) => (
                    <SwiperSlide key={index}>
                        <img
                            src={`${process.env.NEXT_PUBLIC_API_URL}${img.imageUrl}`}
                            alt={`Изображение ${index + 1}`}
                            className="ad-image"
                        />
                    </SwiperSlide>
                ))}
            </Swiper>
            <h1 className="ad-title">{order.orderName}</h1>
            <p className="ad-description">{order.item.description}</p>
            <p className="ad-price">{order.item.price} ₽</p>
            <br/>
            <p className="ad-user">
                Объявление пользователя:{' '}
                <a href={`/user/${order.userId}`} className="ad-user-link">
                    {order.user.username}
                </a>
            </p>
            <Link href={`/order/edit?id=${id}`}>
                <button className="anime-button">Редактировать объявление</button>
            </Link>
            

        </div>
    );
}